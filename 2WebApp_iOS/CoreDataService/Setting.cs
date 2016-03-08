using System;
using System.Collections.Generic;
using UIKit;
using System.Drawing;
using System.IO;


namespace CoreDataService
{

	// callback function definition
	// Parameters:
	// succeed		true | false
	// errmsg		error message
	public delegate void SyncCallback (Boolean succeed, string errmsg);

	public enum RequestType { GET, POST };
	public enum RequestOption { Auth, Sync };		// should be conform with the backend system
	public enum DatabaseType { Sqlite, Json };



	public static class Settings
	{
		// remote settings
		public static string ws_address = "http://www.2web.cc.php53-4.ord1-1.websitetestlink.com";
		public static string ws_basepath = "/webservice/common/";
		public static string ws_svcname = "Service";
		public static RequestType ws_reqtype = RequestType.POST;
		public static int ws_timeout = 15000;

		// for test purpose
		public static string test_username = "test@test.com";
		public static string test_password = "test";

		// local status
		public static Boolean local_ismemsynced = false;
		public static Boolean local_isdisksynced = false;

		// local database settings
		public static string local_dbpath = "";
		public static DatabaseType local_dbtype = DatabaseType.Json;
		// for json version
		// for sqlite version
		public static string local_dbschema = "CoreDataService.";
		public static string[] local_tables = { "organization","staffaccount","taskupdatetypes","notification_type","notifications",
			"client_organization_rel","projects","tasks","project_support_rel","clientaccount",
			"projectstatus","projecttype","projectphase","supportpackage","contact"};
		public static string[] local_privatetables = {"userinfo"};



		// Return default contact
		public static contact DefaultContact() {

			contact info = new contact();
			info.address1 = "116-116 Research Drive, Saskatoon,";
			info.address2 = "SK S7N3R3";
			info.email = "info@2webdesign.com";
			info.phone = "306.664.2932";
			info.visit = "www.2webdesign.com";
			info.facebook = "https://www.facebook.com/2webdesign";
			info.twitter = "https://twitter.com/2webdesign";
			info.google = "https://plus.google.com/u/0/+2webdesign/posts";
			info.linkedIn = "https://www.linkedin.com/company/2webdesign-com";
			info.youtube = "https://www.pinterest.com/2webdesign";

			return info;
		}
		
		
		// Return the demo project
		public static projectsummary DemoProject() {

			projectsummary demo = new projectsummary();
			demo.name = "2 Web Demo Project";
			demo.type = "DEMO";
			demo.status = "In Process";
			demo.org_name = "2 Web Design Demo";
			demo.client_name = "0";
			demo.client_email = "test@test.com";
			demo.staff_name = "0";
			demo.staff_email = "hardik@2webdesign.com";

			demo.update =new List<task> ();
			task newtask = new task ();
			newtask.name = "Project Proposal delivered to client";
			demo.update.Add (newtask);
			newtask = new task ();
			newtask.name = "Client Agreement Signed";
			demo.update.Add (newtask);
			newtask = new task ();
			newtask.name = "Project Charter delivered";
			demo.update.Add (newtask);
			newtask = new task ();
			newtask.name = "Project Charter Signed";
			demo.update.Add (newtask);
			newtask = new task ();
			newtask.name = "Homepage Designs started";
			demo.update.Add (newtask);
			newtask = new task ();
			newtask.name = "Internal Page designs started";
			demo.update.Add (newtask);
			newtask = new task ();
			newtask.name = "Designs sent to client for feedback";
			demo.update.Add (newtask);
			newtask = new task ();
			newtask.name = "Design Review Meeting";
			demo.update.Add (newtask);
			newtask = new task ();
			newtask.name = "Design Approval Signed";
			demo.update.Add (newtask);
			newtask = new task ();
			newtask.name = "Database setup complete";
			demo.update.Add (newtask);
			newtask = new task ();
			newtask.name = "Navigation Design Change Requested";
			demo.update.Add (newtask);
			newtask = new task ();
			newtask.name = "Slice Updated for Navigation Change";
			demo.update.Add (newtask);
			newtask = new task ();
			newtask.name = "New Slice for Navigation change applied";
			demo.update.Add (newtask);
			
			demo.support_package = new List<support> ();
			support newsupp = new support ();
			newsupp.name = "360 Website Success Plan A";
			newsupp.status = "1";
			newsupp.hourused = "3";
			newsupp.totalhour = "12";
			newsupp.lastbackup = "2016-02-01 00:00:00";
			newsupp.lastpost = "2016-03-01 00:00:00";
			demo.support_package.Add (newsupp);

			return demo;
		}

		
		// global functions
		public static void ShowMsg(string msg) {
			UIAlertView msgview = new UIAlertView ("Notification", msg, null, "Ok", null);
			msgview.Show ();
		}


	}



	// Customized Test UI for CoreDataService
	public partial class CoreDataServiceTestUI : UIViewController
	{
		// private member
		private static DataService ds = null;
		private UITextView testView = null;


		//public CoreDataServiceTestUI (IntPtr handle) : base (handle)
		public CoreDataServiceTestUI () 
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			// initialize DataService
			string sqliteFilename = "CoreDataServiceTest.db";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string dbPath = Path.Combine(documentsPath, sqliteFilename);
			ds = new DataService(dbPath);

			// metric calculation
			RectangleF screensize = (RectangleF)UIScreen.MainScreen.Bounds;
			float width = screensize.Width;
			float height = 100;
			float statusbarheight = (float)UIApplication.SharedApplication.StatusBarFrame.Height;
			float navbarheight = (float)this.NavigationController.NavigationBar.Bounds.Height;
			float logicTop = statusbarheight + navbarheight;

			// UIButton
			var testBtn = UIButton.FromType(UIButtonType.RoundedRect);
			testBtn.SetTitle ("Test", UIControlState.Normal);
			testBtn.Frame = new RectangleF(0, logicTop, width, height);
			testBtn.BackgroundColor = UIColor.Orange;
			testBtn.SetTitleColor (UIColor.White, UIControlState.Normal);
			Add (testBtn);

			// UITextView
			testView = new UITextView (new RectangleF (0, logicTop+height+10, width, height+100));
			testView.Text = "Output Area";
			testView.Editable = false;
			testView.BackgroundColor = UIColor.Gray;
			testView.TextAlignment = UITextAlignment.Center;
			Add (testView);


			// Button click event
			testBtn.TouchUpInside += (s, e) =>  {

				List<projectsummary> projs;
				string errmsg;
				if ( !ds.ProjectInfo(out projs, out errmsg) ) {
					testView.Text = "Error: \n" + errmsg;	
				} else {
					testView.Text = "Project Information:\n";
					foreach(var item in projs) {
						testView.Text += String.Format("Name: {0}\nType: {1}\nStatus: {2}\n",item.name,item.type,item.status);
					}
				}

			};


			// Start synchronization
			SyncCallback x = new SyncCallback(callBack);
			user info = new user ();
			info.username = "test@test.com";
			info.password = "test";
			ds.Sync(info, true, x);

		}


		void callBack (Boolean succeed, string errmsg)
		{
			InvokeOnMainThread(()=>{
				testView.Text += "\nBackground synchronization is done\n\n";
			});
		}

	}

}

