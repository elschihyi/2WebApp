﻿using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using SQLite;
using System.Xml.Linq;



namespace CoreDataService
{

	public class DataService : Connection
	{

		// Table synchronization control
		// Key: table name
		// Value: true | false		true, to sync
		public static Dictionary<string, Boolean> SyncTable;

		// data cache
		private static DBCache cache;

		// synced call controller
		private Boolean blocking_succeed;
		private string blocking_errmsg;
		

		public DataService (string dbpath)
		{
			Settings.local_dbpath = dbpath;
			cache = new DBCache();
			blocking_succeed = false;
			blocking_errmsg = "";

			SyncTable = new Dictionary<string, Boolean> ();
			foreach (var item in Settings.local_tables) {
				SyncTable.Add (item, true);
			}
		}



		#region Interfaces

		// Set all tables to sync
		public void SetSyncTagForAll (Boolean on)
		{
			foreach (var item in SyncTable) {
				SyncTable [item.Key] = on;
			}
		}



		// Single action interface for upper layers
		public Boolean Action( ref ActionParameters actioninfo ) {

			ActionCallback func = actioninfo.IN.func;

			switch (actioninfo.IN.type) {

			case ActionType.LOGOUT:

				// remove all local data
				cache.acctinfo = null;
				cache.projects = null;
				// update local data
				string errmsg;
				if ( !SaveCacheData(false, out errmsg) ) {
					return false;
				}
				break;
			
			case ActionType.SYNCATSTARTUP:
			case ActionType.LOGIN:
			case ActionType.SYNC:

				//
				// WARNING: MAKE SURE SETUP PROPER TABLE'S FLAG BEFORE CALL THIS
				//

				if (actioninfo.IN.func == null)
					return false;

				AccountInfo data = actioninfo.IN.data;

				if (actioninfo.IN.func == null) {
					// synced call case
					SyncThread ( data, new ActionCallback (CoreDataServiceCallback) );
					errmsg = blocking_errmsg;
					return blocking_succeed;
				} else {
					Thread syncThread = new Thread (() => SyncThread (data, func));
					syncThread.Start ();
				}
				break;

			case ActionType.CREATEACCOUNT:
			case ActionType.UPDATEACCOUNT:
			case ActionType.UPDATESETTINGS:
			case ActionType.SAVETOKEN:

				// check user password if update profile
				if (actioninfo.IN.type == ActionType.UPDATEACCOUNT && actioninfo.IN.data.password.Length < 24) {

					string hashedcode = GetMD5 (actioninfo.IN.data.password);
					if (hashedcode != cache.acctinfo.client_password) {

						errmsg = ErrorMessage.Login;
						return false;
					}

					actioninfo.IN.data.password = cache.acctinfo.client_password;

				} else if (actioninfo.IN.type == ActionType.UPDATESETTINGS && actioninfo.IN.data.remember_password == "1") {

					// save setting regardless online/offline if UPDATESETTINGS
					cache.acctinfo.settings = actioninfo.IN.data.settings;
					cache.acctinfo.usersetting_updated = "1";
					if (!SaveCacheData (true, out errmsg)) {
						return false;
					}
				
				// check if there is an account info for new tokens
				} else if ( actioninfo.IN.type == ActionType.SAVETOKEN && ( cache.acctinfo == null || cache.acctinfo.client_email == "" ) ) {

					// save it to local until an account info is provided
					if ( cache.acctinfo == null ) {
						cache.acctinfo = new accountsummary ();
					}
					cache.acctinfo.notification_token = actioninfo.IN.data.notification_token;
					if (!SaveCacheData (true, out errmsg)) {
						return false;
					}
					errmsg = "";
					return true;
				}

				// checkout network connection
				if (!CheckConnection(out errmsg)) {

					errmsg = ErrorMessage.Connection;
					if (actioninfo.IN.func == null) {
						return false;
					} else {
						ActionCallback callback = new ActionCallback (actioninfo.IN.func);
						callback (false, errmsg);
					}

				}

				ActionParameters para = actioninfo;
				// determine if blocking is needed
				if (para.IN.func == null) {
					// synced call case
					para.IN.func = new ActionCallback (CoreDataServiceCallback);
					RequestThread (para);
					errmsg = blocking_errmsg;
					return blocking_succeed;
				}
				else {
					Thread reqThread = new Thread (() => RequestThread ( para ));
					reqThread.Start ();
				}
				break;

			case ActionType.LOADRSS:

				if (actioninfo.IN.func == null) {
					// synced call case
					RssThread (new ActionCallback (CoreDataServiceCallback));
					errmsg = blocking_errmsg;
					return blocking_succeed;
				} else {
					Thread rssThread = new Thread (() => RssThread ( func ));
					rssThread.Start ();
				}
				break;

			case ActionType.GETPROJINFO:

				// check the cache first
				// no matter the cached is synced or not, either global sync or local
				// to identify if it is a global sync, check Settings.local_ismemsynced
				if ( cache.projects != null ) {

					actioninfo.OUT.dataset = cache.projects;

				} else {
					// return default data
					actioninfo.OUT.dataset = new List<projectsummary> ();
					((List<projectsummary>)actioninfo.OUT.dataset).Add (DefaultDataSet.Project ());
				}
				break;

			case ActionType.GETCONTINFO:

				// check the cache first
				// no matter the cached is synced or not, either global sync or local
				// to identify if it is a global sync, check Settings.local_ismemsynced
				if ( cache.continfo != null ) {
					actioninfo.OUT.dataset = cache.continfo;
					return true;	

				} else {
					// return default data
					actioninfo.OUT.dataset = DefaultDataSet.Contact();
				}
				break;

			case ActionType.GETACCTINFO:

				// check the cache first
				// no matter the cached is synced or not, either global sync or local
				// to identify if it is a global sync, check Settings.local_ismemsynced
				if ( cache.acctinfo != null ) {
					actioninfo.OUT.dataset = cache.acctinfo;
					return true;

				} else {
					// no default data
					actioninfo.OUT.dataset = new accountsummary();
					((accountsummary)actioninfo.OUT.dataset).settings = new usersettings ();
					((accountsummary)actioninfo.OUT.dataset).organizations = new List<userorg>();
					((accountsummary)actioninfo.OUT.dataset).projects = new List<userproj>();
				}
				break;

			case ActionType.GETRSSINFO:

				if ( cache.rssres != null ) {
					actioninfo.OUT.dataset = cache.rssres;
					return true;
				} else {
					actioninfo.OUT.dataset = new RSSResource();
				}
				break;
			}

			return true;
		}

		#endregion


		
		#region Internal Functions - Common

		// callback function for synced calls
		private void CoreDataServiceCallback(Boolean succeed, string errmsg) {
		
			blocking_succeed = succeed;
			blocking_errmsg = errmsg;
			return;
		}



		// Synchronize data at background
		private void SyncThread (AccountInfo userinfo, ActionCallback func)
		{

			string errmsg;
			ActionCallback callback = new ActionCallback (func);

			// handle user information
			if (userinfo.username == "" && userinfo.password == "") {
				// check if the credential is saved
				if ( !GetSavedUserInfo (out errmsg) || cache.acctinfo.client_email == "" ) {
					// no given and no saved
					// use the default project
					cache.projects = null;
					callback (true, errmsg);
					return;
				}
				
			} else if ( userinfo.username == "" ) {
				// no saved and try to login
				cache.acctinfo = null;
				cache.projects = null;

				if ( Settings.runmode == RunMode.Normal ) {

					callback(false, ErrorMessage.Login);

				} else if ( Settings.runmode == RunMode.Debug ) {

					callback(false, ErrorMessage.Login_Missing);
				}

				return;
				
			} else {
				
				if ( cache.acctinfo == null ) {
					cache.acctinfo = new accountsummary();
					cache.acctinfo.settings = new usersettings ();
					cache.acctinfo.organizations = new List<userorg> ();
					cache.acctinfo.projects = new List<userproj> ();
				}
				cache.acctinfo.client_email = userinfo.username;
				cache.acctinfo.client_password = GetMD5(userinfo.password);
				cache.acctinfo.remember_password = userinfo.remember_password;
				cache.acctinfo.status = UserStatus.NULL;
			}


			if (CheckConnection (out errmsg)) {

				// connected and try to sync
				object data = null;

				if ( Settings.local_dbtype == DatabaseType.Sqlite ) {
					// check out the synctable
					string tables = "";
					foreach (var item in SyncTable) {
						if (item.Value) {
							tables += item.Key + ",";
						}
					}
					tables = tables.TrimEnd (',');

					// download the table(s)

				} else if ( Settings.local_dbtype == DatabaseType.Json ) {
					
					// download stream data
					AccountInfo acctinfo = new AccountInfo();
					acctinfo.username = cache.acctinfo.client_email;
					acctinfo.password = cache.acctinfo.client_password;
					acctinfo.notification_token = cache.acctinfo.notification_token;
					if (cache.acctinfo.usersetting_updated == "1") {
						acctinfo.usersetting_updated = "1";
						acctinfo.settings = cache.acctinfo.settings;
					}
					if (!DownloadData (acctinfo, ActionType.SYNC, out data, out errmsg)) {
						callback (false, errmsg);
						return;
					}
				}

				// check out user validation and update cache
				DBCache tmpcache;
				if (!ParseData (data, out tmpcache, out errmsg) || tmpcache == null ) {
					callback (false, errmsg);
					return;
				}

				// check authentication status
				if ( tmpcache.acctinfo.status != UserStatus.VALID ) {

					cache.acctinfo = null;
					cache.projects = null;
					
					if ( !SaveCacheData(false, out errmsg) ) {
						callback(false, errmsg);
					}

					if ( Settings.runmode == RunMode.Normal ) {

						callback(false, ErrorMessage.Login);

					} else if ( Settings.runmode == RunMode.Debug ) {

						callback(false, ErrorMessage.Login_Failed);
					}

					return;
				}

				if ( tmpcache.projects != null && tmpcache.continfo != null ) {

					// before the new value applied, reassign the local vars
					tmpcache.acctinfo.client_password = cache.acctinfo.client_password;
					tmpcache.acctinfo.remember_password = cache.acctinfo.remember_password;
					tmpcache.acctinfo.usersetting_updated = cache.acctinfo.usersetting_updated;

					// reset status for user settings
					tmpcache.acctinfo.usersetting_updated = "0";
					tmpcache.acctinfo.notification_token = "";

					cache = tmpcache;
					Settings.local_ismemsynced = true;

					if ( cache.acctinfo.remember_password == "1" && !SaveCacheData(true, out errmsg) ) {
						callback(false, errmsg);
					}
				}

			} else {

				// if not connected and then check local if any data is available to use

				if ( Settings.local_dbtype == DatabaseType.Sqlite ) {

					// obtain all projects
					object projdata;
					string sql = "select project_id as id, project_name as name, b.project_type_name as type, c.proj_status_name as status, d.org_name as org_name," +
						"e.client_firstname || ' ' || e.client_lastname as client_name, e.client_email as client_email, f.staff_firstname || " +
						" ' ' || f.staff_lastname as staff_name, f.staff_email as staff_email from projects a, projecttype b, " +
						"projectstatus c, organization d, clientaccount e, staffaccount f where a.project_type_id = " +
						"b.project_type_id and a.proj_status_id = c.proj_status_id and a.org_id = d.org_id and a.client_accountid = e.client_accountid " +
						"and a.staff_id = f.staff_id";
					DBRequest req = new DBRequest ();
					req.sql = sql;
					req.tablename = "project";
					if (!LocalDB.ReadData (req, out projdata, out errmsg)) {
						cache.projects = null;
						callback (false, errmsg);
						return;
					}

					// obtain the related tasks and supportpackages
					foreach (var item in projdata as IEnumerable<object>) {

//						project proj = (project)item;
//						projectsummary sum = new projectsummary ();
//						sum.name = proj.name;
//						sum.type = proj.type;
//						sum.phase = proj.phase;
//						sum.org_name = proj.org_name;
//						sum.client_name = proj.client_name;
//						sum.client_email = proj.client_email;
//						sum.staff_name = proj.staff_name;
//						sum.staff_email = proj.staff_email;
//
//						// for tasks
//						object taskdata;
//						sql = "select b.task_update as name, b.row_update_date as date, b.task_fileurl from projects a, tasks b" +
//							" where a.project_id = b.project_id and a.project_id = " + proj.id;
//						req.sql = sql;
//						req.tablename = "task";
//						if (!LocalDB.ReadData (req, out taskdata, out errmsg)) {
//							cache.projects = null;
//							callback (false, errmsg);
//							return;
//						}
//						sum.tasks = (List<task>)taskdata;
//
//						// for support packages
//						object suppkgdata;
//						sql = "select c.sup_package_name as name, c.sup_package_hours as totalhour, b.proj_supp_rel_hours as hourused," +
//							" b.proj_supp_rel_status as status, b.proj_supp_rel_backupdate as lastbackup, b.row_update_date as lastpost" +
//							" from projects a, project_support_rel b, supportpackage c where a.project_id = b.project_id" +
//							" and b.sup_package_id = c.sup_package_id and a.project_id = " + proj.id;
//						req.sql = sql;
//						req.tablename = "support";
//						if (!LocalDB.ReadData (req, out suppkgdata, out errmsg)) {
//							cache.projects = null;
//							callback (false, errmsg);
//							return;
//						}
//						sum.support_package = (List<support>)suppkgdata;
//
//						cache.projects.Add (sum);

					}

					// obtain contact record
					object contobj;
					req = new DBRequest ();
					req.sql = "select * from contact limit 1";
					req.tablename = "contact";
					if (!LocalDB.ReadData (req, out contobj, out errmsg)) {
						cache.continfo = null;
						callback(false, errmsg);
						return;
					}

					if (((List<contact>)contobj).Count != 1) {

						if ( Settings.runmode == RunMode.Normal ) {

							callback(false, ErrorMessage.DataAccess);

						} else if ( Settings.runmode == RunMode.Debug ) {

							callback(false, ErrorMessage.DataAccess_WrongNumRecord);
						}

						return;
					}

					cache.continfo = ((List<contact>)contobj) [0];

				} else if ( Settings.local_dbtype == DatabaseType.Json ) {

					// offline cases

					if (cache.acctinfo.status == UserStatus.SAVED && cache.projects != null) {
						// do nothing
						callback (true, "");
						return;

					} else if ( cache.projects != null )  {

						// new user login at offline
						cache.acctinfo = null;
						cache.projects = null;
						if ( Settings.runmode == RunMode.Normal ) {

							callback(false, ErrorMessage.Connection);

						} else if ( Settings.runmode == RunMode.Debug ) {

							callback(false, ErrorMessage.Login_Offline);
						}
						return;
					}
				}
			}

			// always make sure the cache and disk are synced
			if ( Settings.local_ismemsynced && !Settings.local_isdisksynced ) {
				
				Boolean SaveUser = (cache.acctinfo.status == UserStatus.SAVED || (cache.acctinfo.status == UserStatus.VALID && cache.acctinfo.remember_password == "1"));
				if ( !SaveCacheData(SaveUser, out errmsg) ) {
					callback(false, errmsg);
				}
			}

			callback(true, errmsg);
		}



		// Make network request at background
		private void RequestThread (ActionParameters actioninfo) {

			string errmsg;
			ActionCallback callback = new ActionCallback (actioninfo.IN.func);

			// send the request and obtain the data
			string json;
			Dictionary<string, string> request = BuildRequest (actioninfo.IN.data, actioninfo.IN.type);
			if (!MakeRequest (request, out json)) {
				callback(false, ErrorMessage.DataAccess);
				return;
			}

			// build a data object based on the downloaded JSON
			object retobj;
			if (!JSONtoObject (json, out retobj, out actioninfo.OUT.errmsg)) {
				callback(false, ErrorMessage.DataAccess);
				return;
			}

			DBCache info;
			if (!ParseData (retobj, out info, out actioninfo.OUT.errmsg) || info == null || info.acctinfo == null) {
				callback(false, ErrorMessage.DataAccess);
				return;
			}

			UserStatus status;
			if (actioninfo.IN.type == ActionType.CREATEACCOUNT)
				status = UserStatus.CREATED;
			else
				status = UserStatus.UPDATED;
			if (info.acctinfo.status != status) {

				if (actioninfo.IN.type == ActionType.SAVETOKEN) {

					cache.acctinfo.notification_token = actioninfo.IN.data.notification_token;

				} else if (actioninfo.IN.type == ActionType.UPDATEACCOUNT) {
					
					cache.acctinfo.usersetting_updated = "1";

				}

				if ( !SaveCacheData(true, out errmsg) ) {
					callback(false, errmsg);
					return;
				}

				if (Settings.runmode == RunMode.Normal) {

					actioninfo.OUT.errmsg = ErrorMessage.DataAccess;

				} else if (Settings.runmode == RunMode.Debug) {

					actioninfo.OUT.errmsg = ErrorMessage.Account_Update;
				}

				callback(false, actioninfo.OUT.errmsg);
				return;

			} else {

				// update and save the cache
				if ( actioninfo.IN.type == ActionType.UPDATEACCOUNT ) {
					cache.acctinfo.client_firstname = actioninfo.IN.data.firstname;
					cache.acctinfo.client_lastname = actioninfo.IN.data.lastname;
					cache.acctinfo.client_email = actioninfo.IN.data.username;

					if ( actioninfo.IN.data.new_password != "" ) {
						cache.acctinfo.client_password = GetMD5(actioninfo.IN.data.new_password);
					}
				} else if ( actioninfo.IN.type == ActionType.UPDATESETTINGS ) {

					cache.acctinfo.settings = actioninfo.IN.data.settings;
				}

				if ( cache.acctinfo.remember_password == "1" && !SaveCacheData(true, out errmsg) ) {
					callback(false, ErrorMessage.DataAccess);
					return;
				}
			}

			errmsg = "";
			callback(true, errmsg);
		}




		// Load RSS resources at background
		private void RssThread (ActionCallback func) {

			ActionCallback callback = new ActionCallback (func);

			if ( cache.rssres == null )
				cache.rssres = new RSSResource();
			cache.rssres.blogs = XDocument.Load (Settings.rss_blogs);
			cache.rssres.events = XDocument.Load (Settings.rss_events);
			cache.rssres.emailblasts = XDocument.Load (Settings.rss_emailblasts);
			cache.rssres.marketfeeds = XDocument.Load (Settings.rss_marketfeeds);

			callback(true,"");
		}



		// return MD5 code by given string
		public string GetMD5(string text) {
			
			MD5 hash = new MD5CryptoServiceProvider ();
			byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(text));
			StringBuilder result = new StringBuilder();
			for (int i=0;i<data.Length;i++)
			{
				result.Append(data[i].ToString("x2"));
			}
			return result.ToString();
		}

		
		
		// obtain the user record from the database
		// regardless if the current user is same or has been authenticated
		private Boolean GetSavedUserInfo (out string errmsg)
		{
			errmsg = "";
			object obj = null;

			if ( Settings.local_dbtype == DatabaseType.Sqlite ) {

//				DBRequest req = new DBRequest ();
//				req.sql = "select * from userinfo";
//				req.tablename = "userinfo";
//				if (!LocalDB.ReadData (req, out obj, out errmsg)) {
//					return false;
//				}
//				if ( obj == null )
//					return false;
//
//				cache.acctinfo = (accountsummary)obj;
//				return true;

			} else if ( Settings.local_dbtype == DatabaseType.Json ) {

				if (!LocalDB.ReadData (null, out obj, out errmsg) || obj == null ) {
					return false;
				}

				DBCache info;
				if (!ParseData (obj, out info, out errmsg) || info == null || info.acctinfo == null ) {
					return false;
				}

				// update cache if possible for performance purpopse
				// be very careful otherwise the cache data will replace the synced data
				// don't touch both local_ismemsynced and local_isdisksynced tags
				if ( info.continfo != null && cache.continfo == null ) cache.continfo = info.continfo;
				if ( info.projects != null && cache.projects == null ) cache.projects = info.projects;

				if ( info.acctinfo != null && info.acctinfo.client_email != "" )
					info.acctinfo.status = UserStatus.SAVED;
				cache.acctinfo = info.acctinfo;
				return true;
			}

			return false;
		}



		// save chache data into disk
		private Boolean SaveCacheData ( Boolean SaveUser, out string errmsg ) {

			errmsg = "";

			// save the data to local
			object[] cleaneddata;
			if ( SaveUser )
				cleaneddata = new object[]{new object[]{cache.acctinfo}, cache.projects,new object[]{cache.continfo} };
			else
				cleaneddata = new object[]{new object[]{null}, new object[]{null}, new object[]{cache.continfo} };
			DBRequest req = new DBRequest ();
			req.dataset = cleaneddata;
			if (!LocalDB.SaveData (req, out errmsg)) {
				return false;
			}

			// synchronization is successful only if data are both loaded into cache and written to disk
			Settings.local_isdisksynced = true;
			return true;
		}



		// return object(s) from JSON string
		private Boolean JSONtoObject (string json, out object obj, out string errmsg)
		{
			obj = null;
			errmsg = "";

			if (json == "" || json == "{}")
				return true;

			try {
				int x = 0;
				JObject tables = JObject.Parse (json);
				object[] tableobj = new object[tables.Count];
				foreach (var item in tables) {
					var list = typeof(List<>);
					var listOfType = list.MakeGenericType (System.Type.GetType (Settings.local_dbschema + item.Key));
					tableobj [x] = JsonConvert.DeserializeObject (item.Value.ToString (), listOfType);
					x++;
				}
				obj = tableobj;
				return true;

			} catch (JsonException e) {
				errmsg = e.Source + "->" + e.Message;
				return false;
			}
		}



		// parse object list
		public Boolean ParseData (object data, out DBCache cache, out string errmsg)
		{
			cache = new DBCache();
			errmsg = "";

			try {

				foreach (var item in data as IEnumerable<object>) {

					if ( item == null ) continue;
					foreach (var subitem in item as IEnumerable<object>) {

						if ( subitem == null ) break;
						if (subitem.GetType () == Type.GetType (Settings.local_dbschema + "accountsummary"))
							cache.acctinfo = (accountsummary)subitem;
						else if (subitem.GetType () == Type.GetType (Settings.local_dbschema + "contact"))
							cache.continfo = (contact)subitem;
						else if (subitem.GetType () == Type.GetType (Settings.local_dbschema + "projectsummary")) {
							if ( cache.projects == null )
								cache.projects = new List<projectsummary>();
							cache.projects.Add ((projectsummary)subitem);
						}
					}
				}					

			} catch (Exception e) {
				errmsg = e.Message;
				return false;
			}

			return true;
		}

		#endregion


		#region Internal Functions - Json


		// DownloadData Function
		// Obtain data for a specified table from remote
		// Parameters:
		//		userinfo	for backend login
		//		type		type of service request Auth | Sync
		//		dataset		returned data set
		//		errmsg		error message
		// Return:
		//		true successful
		//		false return with error as result
		private Boolean DownloadData (AccountInfo userinfo, ActionType type, out object dataset, out string errmsg)
		{
			errmsg = "";
			dataset = null;

			// send the request and obtain the data
			string json;
			Dictionary<string, string> request = BuildRequest (userinfo, type);
			if (!MakeRequest (request, out json)) {
				return false;
			}

			// build a data object based on the downloaded JSON
			if (!JSONtoObject (json, out dataset, out errmsg))
				return false;

			return true;
		}


		#endregion


		#region Internal Functions - Sqlite


		// DownloadTable Function
		// Obtain data for a specified table from remote
		// Parameters:
		//		tablename	single table | multi-table(separated by ,) | ALL
		//		rows		returned data set
		//		errmsg		error message
		// Return:
		//		true successful
		//		false return with error as result
		private Boolean DownloadTable (string tables, out object datasets, out string errmsg)
		{
			errmsg = "";
			datasets = null;

			if (tables == "") {
				if ( Settings.runmode == RunMode.Normal ) {

					errmsg = ErrorMessage.DataAccess;

				} else if ( Settings.runmode == RunMode.Debug ) {

					errmsg = ErrorMessage.DataAccess_NoTableName;
				}
				return false;
			}

			// check the connection
			if (!CheckConnection (out errmsg)) {
				return false;
			}

			// send the request and obtain the data
//			string json;
//			accountsummary info = new accountsummary ();
//			info.client_email = Settings.test_username;
//			info.client_password = Settings.test_password;
//			Dictionary<string, string> request = BuildRequest (info, RequestOption.Sync);
//			if (!MakeRequest (request, out json)) {
//				return false;
//			}
//
//			// build a data object based on the downloaded JSON
//			if (!JSONtoObject (json, out datasets, out errmsg))
//				return false;
//
			return true;
		}


		#endregion
	}


}
