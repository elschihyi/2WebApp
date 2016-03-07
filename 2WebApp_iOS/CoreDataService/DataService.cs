using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using SQLite;



namespace CoreDataService
{

	public class DataService : Connection
	{

		// Table synchronization control
		// Key: table name
		// Value: true | false		true, to sync
		public Dictionary<string, Boolean> SyncTable;

		public DataService (string dbpath)
		{
			Settings.local_dbpath = dbpath;

			SyncTable = new Dictionary<string, Boolean> ();
			foreach(var item in Settings.local_tables) {
				SyncTable.Add (item, true);
			}
		}

		#region Public Functions

		public Boolean ProjectInfo (out List<projectsummary> projsum, out string errmsg)
		{
			projsum = null;
			errmsg = "";
			projsum = new List<projectsummary> ();

			if (!IsLocalDataValid()) {
				// return default data
				projsum.Add(Settings.DemoProject());

			} else {
				
				// obtain all projects
				object projdata;
				string sql = "select project_id as id, project_name as name, b.project_type_name as type, c.proj_status_name as status, d.org_name as org_name," +
				            "e.client_firstname || ' ' || e.client_lastname as client_name, e.client_email as client_email, f.staff_firstname || " +
				            " ' ' || f.staff_lastname as staff_name, f.staff_email as staff_email from projects a, projecttype b, " +
				            "projectstatus c, organization d, clientaccount e, staffaccount f where a.project_type_id = " +
				            "b.project_type_id and a.proj_status_id = c.proj_status_id and a.org_id = d.org_id and a.client_accountid = e.client_accountid " +
				            "and a.staff_id = f.staff_id";
				if (!LocalDB.ExeSQL (sql, "project", out projdata, out errmsg)) {
					projsum = null;
					return false;
				}

				// obtain the related tasks and supportpackages
				foreach (var item in projdata as IEnumerable<object>) {

					project proj = (project)item;
					projectsummary sum = new projectsummary ();
					sum.name = proj.name;
					sum.type = proj.type;
					sum.status = proj.status;
					sum.org_name = proj.org_name;
					sum.client_name = proj.client_name;
					sum.client_email = proj.client_email;
					sum.staff_name = proj.staff_name;
					sum.staff_email = proj.staff_email;

					// for tasks
					object taskdata;
					sql = "select b.task_update as name, b.row_update_date as date, b.task_fileurl from projects a, tasks b" +
					" where a.project_id = b.project_id and a.project_id = " + proj.id;
					if (!LocalDB.ExeSQL (sql, "task", out taskdata, out errmsg)) {
						projsum = null;
						return false;
					}
					sum.update = (List<task>)taskdata;

					// for support packages
					object suppkgdata;
					sql = "select c.sup_package_name as name, c.sup_package_hours as totalhour, b.proj_supp_rel_hours as hourused," +
					" b.proj_supp_rel_status as status, b.proj_supp_rel_backupdate as lastbackup, b.row_update_date as lastpost" +
					" from projects a, project_support_rel b, supportpackage c where a.project_id = b.project_id" +
					" and b.sup_package_id = c.sup_package_id and a.project_id = " + proj.id;
					if (!LocalDB.ExeSQL (sql, "support", out suppkgdata, out errmsg)) {
						projsum = null;
						return false;
					}
					sum.support_package = (List<support>)suppkgdata;

					projsum.Add (sum);
				}
			}

			return true;
		}



		public Boolean ContactInfo (out contact info, out string errmsg)
		{
			info = null;
			errmsg = "";

			// obtain contact record
			object contobj;
			string sql = "select * from contact";
			if ( !LocalDB.ExeSQL(sql, "contact", out contobj, out errmsg) ) {
				info = null;
				return false;
			}

			info = ((List<contact>)contobj)[0];
			return true;
		}



		// Set all tables to sync
		public void SetSyncTagForAll (Boolean on)
		{
			foreach (var item in SyncTable) {
				SyncTable [item.Key] = on;
			}
		}
		
		
		
		// Do table synchronization
		public void Sync (SyncCallback func)
		{
			//
			// WARNING: MAKE SURE SETUP PROPER TABLE'S FLAG BEFORE CALL THIS
			//

			Thread syncThread = new Thread(() => SyncThread(func));
			syncThread.Start();
		}



		#endregion


		
		#region Internal Functions
		
		// Synchronize table data at background
		private void SyncThread (SyncCallback func) {

			// check out the synctable
			string tables = "";
			foreach (var item in SyncTable) {
				if (item.Value && item.Key != "userinfo") {
					tables += item.Key + ",";
				}
			}
			tables = tables.TrimEnd (',');
			
			// create all tables
			LocalDB.CreateAllTables();

			// download and load the table(s)
			SyncCallback callback = new SyncCallback(func);
			string errmsg;
			object data;
			if (!DownloadTable (tables, out data, out errmsg)){
				callback(false, errmsg);
				return;
			}
			if (!LocalDB.ExeSQL ("INSERT", data, out errmsg)) {
				callback(false, errmsg);
				return;
			}

			Settings.ws_Synced = true;
			callback(true, errmsg);
		}



		// Make decision if or not the default data should be used
		private Boolean IsLocalDataValid() {

			if (!Settings.ws_Synced) {

				// cases fall in:
				// - user is NOT logged in AND
				// - data is NOT synced

				// check if the connection is resumed
				string errmsg="";
				if (!CheckConnection(out errmsg)) {

					// still not connected yet
					return false;
				} else {
					
					// try sync() again
					SyncThread(null);

					if (!Settings.ws_Synced) {
						// not lucky again
						return false;

					} else 
						return true;

				}
			}

			return true;
		}
		
		
		
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
				errmsg = "Not given table name to synchronize";
				return false;
			}

			// check the connection
			if (!CheckConnection (out errmsg)) {
				return false;
			}
			
			// send the request and obtain the data
			string json;
			Dictionary<string, string> request = BuildRequest ("TableData?table_id=" + tables);
			if (!MakeRequest (request, out json)) {
				return false;
			}

			// build a data object based on the downloaded JSON
			if (!JSONtoObject (json, out datasets, out errmsg))
				return false;

			return true;
		}



		// return an object from JSON string
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
					var listOfType = list.MakeGenericType (System.Type.GetType (LocalDB.WithSchemaName (item.Key)));
					tableobj [x] = JsonConvert.DeserializeObject (item.Value.ToString (), listOfType);
					x++;
				}
				obj = tableobj;
				return true;

			} catch (JsonException e) {
				errmsg = e.Source + "->" + e.Message;
				errmsg += "\nJSON: " + json;
				return false;
			}
		}



		#endregion
	}


}
