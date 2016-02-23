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

		// local settings
		public static string local_dbpath = "";
		public static string local_dbschema = "CoreDataService.";
		public static string[] local_tables = { "organization","staffaccount","taskupdatetypes","notification_type","notifications",
			"client_organization_rel","projects","tasks","project_support_rel","clientaccount",
			"projectstatus","projecttype","projectphase","supportpackage"};

		// global functions
		public static void ShowMsg(string msg) {
			UIAlertView msgview = new UIAlertView ("Notification", msg, null, "Ok", null);
			msgview.Show ();
		}

	}
}

