using System;
using System.Collections.Generic;
using UIKit;
using System.IO;


namespace CoreDataService
{

	// callback function definition
	// Parameters:
	// succeed		true | false
	// errmsg		error message
	public delegate void SyncCallback (Boolean succeed, string errmsg);


	public static class Settings
	{
		// remote settings
		public static string ws_address = "http://www.2web.cc.php53-4.ord1-1.websitetestlink.com";
		public static string ws_basepath = "/webservice/common/";
		public static string ws_username = "";
		public static string ws_password = "";
		public static Boolean ws_SyncRequest = true;
		public static Boolean ws_Logined = false;
		public static Boolean ws_Synced = false;
		public static int ws_timeout = 15000;

		// local settings
		public static string local_dbpath = "";
		public static string local_dbschema = "CoreDataService.";
		public static string[] local_tables = { "organization","staffaccount","taskupdatetypes","notification_type","notifications",
			"client_organization_rel","projects","tasks","project_support_rel","clientaccount",
			"projectstatus","projecttype","projectphase","supportpackage","contact",
			"userinfo"};


		// Return the demo project
		public static projectsummary DemoProject() {

			projectsummary demo = new projectsummary();
			demo.name = "";
			demo.type = "";
			demo.status = "";
			demo.org_name = "";
			demo.client_name = "";
			demo.client_email = "";
			demo.staff_name = "";
			demo.staff_email = "";

			task newtask = new task ();
			newtask.name = "";
			newtask.date = "";
			newtask.file_url = "";

			support newsupp = new support ();
			newsupp.name = "";
			newsupp.status = "";
			newsupp.hourused = "";
			newsupp.totalhour = "";
			newsupp.lastbackup = "";
			newsupp.lastpost = "";

			demo.update =new List<task> ();
			demo.update.Add (newtask);
			demo.support_package = new List<support> ();
			demo.support_package.Add (newsupp);

			return demo;
		}


		// global functions
		public static void ShowMsg(string msg) {
			UIAlertView msgview = new UIAlertView ("Notification", msg, null, "Ok", null);
			msgview.Show ();
		}


	}
}

