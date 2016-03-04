using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using SQLite;


namespace CoreDataService
{
	////////////////////////////////////////////////////////////////
	// TABLE DEFINITIONS
	////////////////////////////////////////////////////////////////

	[Serializable]
	public class organization
	{
		public string org_id { get; set; } = "";

		public string org_name { get; set; } = "";

		public string org_status { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}

	[Serializable]
	public class staffaccount
	{
		public string staff_id { get; set; } = "";

		public string staff_firstname { get; set; } = "";

		public string staff_lastname { get; set; } = "";

		public string staff_email { get; set; } = "";

		public string staff_password { get; set; } = "";

		public string staff_status { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}

	[Serializable]
	public class taskupdatetypes
	{
		public string task_updatetype_id { get; set; } = "";

		public string task_updatetype_name { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}

	[Serializable]
	public class notification_type
	{
		public string notification_type_id { get; set; } = "";

		public string notification_type_name { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}

	[Serializable]
	public class notifications
	{
		public string notification_id { get; set; } = "";

		public string notification_name { get; set; } = "";

		public string staff_id { get; set; } = "";

		public string notification_type_id { get; set; } = "";

		public string project_id { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}

	[Serializable]
	public class client_organization_rel
	{
		public string client_accountid { get; set; } = "";

		public string org_id { get; set; } = "";

		public string client_org_status { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}

	[Serializable]
	public class projects
	{
		public string project_id { get; set; } = "";

		public string project_name { get; set; } = "";

		public string org_id { get; set; } = "";

		public string client_accountid { get; set; } = "";

		public string project_type_id { get; set; } = "";

		public string proj_status_id { get; set; } = "";

		public string proj_phase_id { get; set; } = "";

		public string staff_id { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}

	[Serializable]
	public class tasks
	{
		public string task_id { get; set; } = "";

		public string task_update { get; set; } = "";

		public string task_showclient { get; set; } = "";

		public string task_fileurl { get; set; } = "";

		public string project_id { get; set; } = "";

		public string staff_id { get; set; } = "";

		public string proj_phase_id { get; set; } = "";

		public string task_updatetype_id { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}

	[Serializable]
	public class project_support_rel
	{
		public string proj_supp_rel_id { get; set; } = "";

		public string proj_supp_rel_hours { get; set; } = "";

		public string proj_supp_rel_backupdate { get; set; } = "";

		public string proj_supp_rel_status { get; set; } = "";

		public string proj_supp_rel_start_date { get; set; } = "";

		public string proj_supp_rel_end_date { get; set; } = "";

		public string project_id { get; set; } = "";

		public string sup_package_id { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}

	[Serializable]
	public class clientaccount
	{
		public string client_accountid { get; set; } = "";

		public string client_firstname { get; set; } = "";

		public string client_lastname { get; set; } = "";

		public string client_email { get; set; } = "";

		public string client_password { get; set; } = "";

		public string client_status { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}

	[Serializable]
	public class projectstatus
	{
		public string proj_status_id { get; set; } = "";

		public string proj_status_name { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}

	[Serializable]
	public class projecttype
	{
		public string project_type_id { get; set; } = "";

		public string project_type_name { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}

	[Serializable]
	public class projectphase
	{
		public string proj_phase_id { get; set; } = "";

		public string proj_phase_name { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}

	[Serializable]
	public class supportpackage
	{
		public string sup_package_id { get; set; } = "";

		public string sup_package_name { get; set; } = "";

		public string sup_package_hours { get; set; } = "";

		public string sup_package_backup { get; set; } = "";

		public string sup_package_desc { get; set; } = "";

		public string row_adddate { get; set; } = "";

		public string row_added_by { get; set; } = "";

		public string row_update_date { get; set; } = "";

		public string row_udpate_by { get; set; } = "";
	}


	[Serializable]
	public class contact
	{
		public string address1 { get; set; } = "";

		public string address2 { get; set; } = "";

		public string email { get; set; } = "";

		public string phone { get; set; } = "";

		public string visit { get; set; } = "";

		public string facebook { get; set; } = "";

		public string twitter { get; set; } = "";

		public string google { get; set; } = "";

		public string linkedIn { get; set; } = "";

		public string youtube { get; set; } = "";
	}



	////////////////////////////////////////////////////////////////
	// LOCAL PRIVATE TABLE CLASS
	////////////////////////////////////////////////////////////////

	[Serializable]
	public class userinfo
	{
		public string user { get; set; } = "";

		public string cred { get; set; } = "";
	}



	////////////////////////////////////////////////////////////////
	// DATA INTERFACE CLASS
	////////////////////////////////////////////////////////////////

	[Serializable]
	public class project
	{
		public string id { get; set; } = "";

		public string name { get; set; } = "";

		public string type { get; set; } = "";

		public string status { get; set; } = "";

		public string org_name { get; set; } = "";

		public string client_name { get; set; } = "";

		public string client_email { get; set; } = "";

		public string staff_name { get; set; } = "";

		public string staff_email { get; set; } = "";
	}

	[Serializable]
	public class task
	{
		public string name { get; set; } = "";

		public string date { get; set; } = "";

		public string file_url { get; set; } = "";
	}

	[Serializable]
	public class support
	{
		public string name { get; set; } = "";

		public string status { get; set; } = "";

		public string hourused { get; set; } = "";

		public string totalhour { get; set; } = "";

		public string lastbackup { get; set; } = "";

		public string lastpost { get; set; } = "";
	}

	[Serializable]
	public class projectsummary
	{
		public string name { get; set; } = "";

		public string type { get; set; } = "";

		public string status { get; set; } = "";

		public string org_name { get; set; } = "";

		public string client_name { get; set; } = "";

		public string client_email { get; set; } = "";

		public string staff_name { get; set; } = "";

		public string staff_email { get; set; } = "";

		public List<task> update { get; set; } = null;

		public List<support> support_package { get; set; } = null;
	}



	////////////////////////////////////////////////////////////////
	// DATABASE OPERATIONS
	////////////////////////////////////////////////////////////////

	public class DBRequest {

		public string sql;
		public string tablename;
		public object dataset;
	}



	public static class LocalDB
	{
		private static LocalDB_JSON dbjson = null;
		private static LocalDB_Sqlite dbsqlite = null;

		private static Boolean Init( out string  errmsg ) {

			errmsg = "";

			try {

				if ( Settings.local_dbtype == DatabaseType.Sqlite ) {

					dbsqlite = new LocalDB_Sqlite();

					dbsqlite.CreateAllTables();

				} else if ( Settings.local_dbtype == DatabaseType.Json ) {

					dbjson = new LocalDB_JSON();
				}

			} catch (Exception e) {
				errmsg = e.Message;
				return false;
			}

			return true;
		}



		public static Boolean SaveData (DBRequest request, out string errmsg) {

			errmsg = "";

			if ( Settings.local_dbtype == DatabaseType.Sqlite ) {

				if (dbsqlite == null) {
					if ( !Init (out errmsg) )
						return false;
				}

				return dbsqlite.ExeSQL("INSERT", request.dataset, out errmsg);

			} else if ( Settings.local_dbtype == DatabaseType.Json ) {

				if (dbjson == null) {
					if ( !Init (out errmsg) )
						return false;
				}

				return dbjson.SaveJSON (request.dataset, out errmsg);

			} else {

				errmsg = "Database type specified is not support";
				return false;
			}

		}



		public static Boolean ReadData (DBRequest request, out object data, out string errmsg) {

			data = null;
			errmsg = "";

			if ( Settings.local_dbtype == DatabaseType.Sqlite ) {

				if (dbsqlite == null) {
					if ( !Init (out errmsg) )
						return false;
				}

				return dbsqlite.ExeSQL(request.sql, request.tablename, out data, out errmsg);

			} else if ( Settings.local_dbtype == DatabaseType.Json ) {

				if (dbjson == null) {
					if ( !Init (out errmsg) )
						return false;
				}

				return dbjson.ReadJSON (out data, out errmsg);

			} else {
				
				errmsg = "Database type specified is not support";
				return false;
			}

		}

	}



	public class LocalDB_Sqlite
	{

		private SQLiteConnection dbconn = null;


		public LocalDB_Sqlite ()
		{
			try {
				dbconn = new SQLiteConnection (Settings.local_dbpath);
			} catch ( Exception e ) {
				
			}
		}


		// Execute SQL with exception handler
		// input:
		//		mode			INSERT | UPDATE | DELETE
		//		rows			returned object
		//		queryset		returned objects only if in the SELECT mode
		//		errmsg			error message
		// return:
		//		IsSucceed		true | false
		public Boolean ExeSQL (string mode, object rows, out string errmsg)
		{
			errmsg = "";
			try {
				lock (dbconn) {
					// process multiple tables
					object[] tables = (object[])rows;
					foreach (var table in tables) {
						// obtain an actual type string
						string tablename = table.GetType ().ToString ();
						tablename = tablename.Substring (tablename.IndexOf (Settings.local_dbschema));
						tablename = tablename.Replace ("]", "");

						switch (mode.ToUpper ()) {
						case "INSERT":
							// Drop existing table
							dbconn.Execute ("DROP TABLE IF EXISTS \"" + TableShortName (tablename) + "\"");
							// Create a new table
							dbconn.CreateTable (System.Type.GetType (WithSchemaName (tablename)));
							// populate the table
							dbconn.InsertAll (table as IEnumerable<object>, true);
							break;
						case "UPDATE":
							dbconn.Update (table);
							break;
						case "DELETE":
							dbconn.Delete (table);
							break;
						}
					}
				}
			} catch (SQLiteException e) {
				errmsg = e.Source + " -> " + e.Message;
				errmsg += "\nObject Type is " + rows.GetType ().ToString ();
				return false;
			}

			return true;
		}



		// Execute a customized query with given return class
		// input:
		//		sql				a customized SQL statement
		//		rettable		customized data class name
		//		result			returned objects
		//		errmsg			error message
		// return:
		//		IsSucceed		true | false
		public Boolean ExeSQL (string sql, string rettable, out object result, out string errmsg)
		{
			errmsg = "";
			result = null;
			try {
				lock (dbconn) {
					MethodInfo method = dbconn.GetType ().GetMethod ("Query", new [] { typeof(string), typeof(object[]) });
					MethodInfo generic = method.MakeGenericMethod (Type.GetType (WithSchemaName (rettable)));
					object[] args = { };
					result = generic.Invoke (dbconn, new object[] { sql, args });
				}
			} catch (SQLiteException e) {
				errmsg = e.Source + " -> " + e.Message;
				errmsg += "\nReturn Type: " + result.GetType ().ToString ();
				errmsg += "\nStatement: " + sql;
				return false;
			}

			return true;
		}



		// return table name with schema
		private string WithSchemaName (string tablename)
		{
			tablename = tablename.Replace ("[]", "");
			if (tablename.IndexOf (Settings.local_dbschema) == -1)
				return Settings.local_dbschema + tablename;
			else
				return tablename;
		}



		// remove schema from table name
		private string TableShortName (string tablename)
		{
			tablename = tablename.Replace ("[]", "");
			return tablename.Replace (Settings.local_dbschema, "");
		}



		// create all the tables
		public void CreateAllTables ()
		{

			if (dbconn == null)
				return;

			foreach (var item in Settings.local_tables) {
				dbconn.CreateTable (System.Type.GetType (WithSchemaName (item)));	
			}

			foreach (var item in Settings.local_privatetables) {
				dbconn.CreateTable (System.Type.GetType (WithSchemaName (item)));	
			}
		}

	}





	public class LocalDB_JSON
	{
		
		// save JSON string to the local database file
		public Boolean SaveJSON (object data, out string errmsg)
		{

			errmsg = "";

			try {

				FileStream fs = new FileStream (Settings.local_dbpath, FileMode.Create);
				BinaryFormatter formatter = new BinaryFormatter ();
				formatter.Serialize (fs, data);
				fs.Close ();

			} catch (Exception e) {
				errmsg = e.Message;
				return false;
			}

			return true;
		}



		// read JSON string from the local database file
		public Boolean ReadJSON (out object data, out string errmsg)
		{

			data = null;
			errmsg = "";

			try {

				FileStream fs = new FileStream (Settings.local_dbpath, FileMode.Open);
				BinaryFormatter formatter = new BinaryFormatter ();
				data = formatter.Deserialize (fs);
				fs.Close ();

			} catch (Exception e) {
				errmsg = e.Message;
				return false;
			}

			return true;
		}
	}

}

