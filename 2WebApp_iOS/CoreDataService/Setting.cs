using System;
using System.Collections.Generic;
using UIKit;
using System.Drawing;
using System.IO;
using System.Xml.Linq;


namespace CoreDataService
{

	// cutomized data type
	public enum RunMode { Normal, Debug };
	public enum DatabaseType { Sqlite, Json };
	public enum RequestType { GET, POST };
	public enum ActionType { LOGIN, LOGOUT,
							SYNCATSTARTUP, SYNC,
							CREATEACCOUNT, UPDATEACCOUNT, UPDATESETTINGS, SAVETOKEN,
							GETPROJINFO, GETCONTINFO, GETACCTINFO, GETRSSINFO,
							LOADRSS };	// should be conform with the backend system
	public enum UserStatus { VALID, INVALID, CREATED, UPDATED, FAILED, SAVED, NULL };


	// parameter class for data service action interface
	public class ActionParameters {

		public parain IN;
		public paraout OUT;

		public ActionParameters() {
			IN = new parain ();
			OUT = new paraout ();
		}

		// declare internal classes
		public class parain {
			public ActionType type;
			public AccountInfo data;
			public ActionCallback func;
		}

		public class paraout {
			public object dataset;
			public string errmsg;
		}
	}

	public class AccountInfo
	{
		public string username { get; set; } = "";

		public string password { get; set; } = "";

		public string new_password { get; set; } = "";

		public string firstname { get; set; } = "";

		public string lastname { get; set; } = "";

		public string remember_password { get; set; } = "0";

		public string usersetting_updated { get; set; } = "0";

		public string notification_token { get; set; } = "";

		public usersettings settings = null;

	}

	public class RSSResource
	{
		public XDocument blogs { get; set; } = null;

		public XDocument events { get; set; } = null;

		public XDocument marketfeeds { get; set; } = null;

		public XDocument emailblasts { get; set; } = null;
	}



	// callback function definition
	// Parameters:
	// succeed		true | false
	// errmsg		error message
	public delegate void ActionCallback (Boolean succeed, string errmsg);



	public static class Settings
	{
		// runtime control
		public static RunMode runmode = RunMode.Normal;

		// remote settings
		public static string ws_address = "http://www.2web.cc.php53-4.ord1-1.websitetestlink.com";
		public static string ws_basepath = "/webservice/common/";
		public static string ws_svcname = "Service";
		public static string ws_reqname = "Request";
		public static RequestType ws_reqtype = RequestType.POST;
		public static int ws_timeout = 15000;

		// RSS resources
		public static string rss_blogs = "http://www.2webdesign.com/blog/feed/";
		public static string rss_emailblasts = "http://www.2webdesign.com/blog/category/email-blasts/feed/";
		public static string rss_marketfeeds = "https://www.2webdesign.com/blog/category/marketing-resources/feed/";
		public static string rss_events = "http://www.2webdesign.com/blog/category/event/feed/";

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
			"projectstatus","projecttype","projectphase","supportpackage","contact","usersettings"};
		public static string[] local_privatetables = {""};

	}



	// error message table
	public static class ErrorMessage {

		// user-friendly messages
		public static string Login = "Username and Password Provided do not match. Please check your entries and try again.";
		public static string Connection = "No data or wifi connection available. Please check your connection to login. App will continue to operate in offline mode.";
		public static string DataAccess = "The app has encountered a sync error. Please try again later or contact support@2webdesign.com if the issue continues.";

		// messages with technical details
		public static string DataAccess_RequestType = "The given request type is not supported";
		public static string DataAccess_WrongNumRecord = "Record number in Contact is wrong";
		public static string DataAccess_NoTableName = "Not given table name to synchronize";
		public static string DataAccess_WrongDBType = "Database type specified is not support";
		public static string Login_Missing = "Username is reqired";
		public static string Login_Failed = "User login is failed";
		public static string Login_Offline = "Offline login doesn't support";
		public static string Account_Update = "Failed to create/update user account";

	}



	// default data set
	public static class DefaultDataSet {

		// Return default contact
		public static contact Contact() {

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
		public static projectsummary Project() {

			projectsummary demo = new projectsummary();
			demo.name = "2 Web Demo Project";
			demo.type = "DEMO";
			demo.phase = "Discovery";
			demo.org_name = "2 Web Design Demo";
			demo.client_name = "John Smith";
			demo.client_email = "test@test.com";
			demo.staff_name = "Hardik Patel";
			demo.staff_email = "hardik@2webdesign.com";
			demo.status = "DEMO";

			demo.tasks =new List<task> ();
			task newtask = new task ();
			newtask.name = "Project Proposal delivered to client";
			newtask.display = "1";
			newtask.status = "1";
			newtask.date = "2015-04-01 00:00:00";
			demo.tasks.Add (newtask);
			newtask = new task ();
			newtask.name = "Client Agreement Signed";
			newtask.display = "1";
			newtask.status = "1";
			newtask.date = "2015-04-17 00:00:00";
			demo.tasks.Add (newtask);
			newtask = new task ();
			newtask.name = "Project Charter delivered";
			newtask.display = "0";
			newtask.status = "1";
			newtask.date = "2015-04-24 00:00:00";
			demo.tasks.Add (newtask);
			newtask = new task ();
			newtask.name = "Project Charter Signed";
			newtask.display = "1";
			newtask.status = "1";
			newtask.date = "2015-04-30 00:00:00";
			demo.tasks.Add (newtask);
			newtask = new task ();
			newtask.name = "Homepage Designs started";
			newtask.display = "0";
			newtask.status = "2";
			newtask.date = "2015-05-04 00:00:00";
			demo.tasks.Add (newtask);
			newtask = new task ();
			newtask.name = "Internal Page designs started";
			newtask.display = "0";
			newtask.status = "2";
			newtask.date = "2015-05-15 00:00:00";
			demo.tasks.Add (newtask);
			newtask = new task ();
			newtask.name = "Designs sent to client for feedback";
			newtask.display = "0";
			newtask.status = "2";
			newtask.date = "2015-05-20 00:00:00";
			demo.tasks.Add (newtask);
			newtask = new task ();
			newtask.name = "Design Review Meeting";
			newtask.display = "0";
			newtask.status = "2";
			newtask.date = "2015-05-22 00:00:00";
			demo.tasks.Add (newtask);
			newtask = new task ();
			newtask.name = "Design Approval Signed";
			newtask.display = "1";
			newtask.status = "2";
			newtask.date = "2015-05-29 00:00:00";
			demo.tasks.Add (newtask);
			newtask = new task ();
			newtask.name = "Database setup complete";
			newtask.display = "0";
			newtask.status = "3";
			newtask.date = "2015-06-03 00:00:00";
			demo.tasks.Add (newtask);
			newtask = new task ();
			newtask.name = "Navigation Design Change Requested";
			newtask.display = "0";
			newtask.status = "2";
			newtask.date = "2015-06-04 00:00:00";
			demo.tasks.Add (newtask);
			newtask = new task ();
			newtask.name = "Slice Updated for Navigation Change";
			newtask.display = "0";
			newtask.status = "2";
			newtask.date = "2015-06-11 00:00:00";
			demo.tasks.Add (newtask);
			newtask = new task ();
			newtask.name = "New Slice for Navigation change applied";
			newtask.display = "0";
			newtask.status = "3";
			newtask.date = "2015-06-16 00:00:00";
			demo.tasks.Add (newtask);

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

	}



	// Customized Test UI for CoreDataService
	// will be removed eventually
	public partial class CoreDataServiceTestUI : UIViewController
	{
		// private member
		private static DataService ds = null;
		private UITextView testView = null;

		// credential groups
		private string[] username = { "test@test.com", "alicia.hanwell@gmail.com" };
		private string[] password = { "test", "2WebDesign" };


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
			float height = 50;
			float statusbarheight = (float)UIApplication.SharedApplication.StatusBarFrame.Height;
			float navbarheight = (float)this.NavigationController.NavigationBar.Bounds.Height;
			float logicTop = statusbarheight + navbarheight;
			float currentTop = 0;

			// UITextField
			var frame = new RectangleF(0, logicTop, width, height);
			var testField1 = new UITextField(frame);
			testField1.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			testField1.Placeholder = "username";
			testField1.BackgroundColor = UIColor.White;
			testField1.KeyboardType = UIKeyboardType.EmailAddress;
			testField1.MinimumFontSize = 17f;
			testField1.AdjustsFontSizeToFitWidth = true;
			testField1.ShouldReturn += (field) => {
				field.ResignFirstResponder ();
				return true;
			};
			Add (testField1);

			// UITextField
			currentTop = logicTop + height + 10;
			frame = new RectangleF(0, currentTop, width, height);
			var testField2 = new UITextField(frame);
			testField2.AutoresizingMask = UIViewAutoresizing.FlexibleTopMargin;
			testField2.Placeholder = "password";
			testField2.BackgroundColor = UIColor.White;
			testField2.KeyboardType = UIKeyboardType.ASCIICapable;
			testField2.MinimumFontSize = 17f;
			testField2.AdjustsFontSizeToFitWidth = true;
			testField2.ShouldReturn += (field) => {
				field.ResignFirstResponder ();
				return true;
			};
			Add (testField2);

			// UISwitch
			currentTop += height + 10;
			frame = new RectangleF(width - 100, currentTop, width, height);
			var testSwitch = new UISwitch(frame);
			Add (testSwitch);

			// UIButton
			currentTop += height + 10;
			var testBtn = UIButton.FromType(UIButtonType.RoundedRect);
			testBtn.SetTitle ("Test", UIControlState.Normal);
			testBtn.Frame = new RectangleF(0, currentTop, width, height);
			testBtn.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			testBtn.BackgroundColor = UIColor.Orange;
			testBtn.SetTitleColor (UIColor.White, UIControlState.Normal);
			Add (testBtn);

			// UITextView
			currentTop += height+10;
			testView = new UITextView (new RectangleF (0, currentTop, width, screensize.Height-currentTop));
			testView.Text = "Output Area";
			testView.Editable = false;
			testView.BackgroundColor = UIColor.Gray;
			testView.TextAlignment = UITextAlignment.Center;
			Add (testView);

			// Button click event
			testBtn.TouchUpInside += (s, e) =>  {

				if ( testField1.Text != "" ) {

					int useridx = 0;

					if ( !Int32.TryParse(testField1.Text, out useridx) ||
							useridx < 0 || 
							useridx >= username.Length ) { 

						var msgController = UIAlertController.Create ("Error Message", "Either the input is not a number or the number is out of range 0-" + (username.Length - 1), UIAlertControllerStyle.Alert);
						msgController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
						PresentViewController (msgController, true, null);

						return; 
					}
						
					testField1.Text = username[useridx];
					testField2.Text = password[useridx];

					// collect the inputs
					string text1 = testField1.Text;
					string text2 = testField2.Text;
					Boolean switchbox = testSwitch.On;

					// Start an action
					AccountInfo info = new AccountInfo ();
					info.username = text1;
					info.password = ds.GetMD5(text2);

					ActionParameters para = new ActionParameters ();
					para.IN.data = info;

					// for sync
//					para.IN.type = ActionType.SYNC;
//					para.IN.func = new ActionCallback(callBack);;

					// for create profile
//					para.IN.type = ActionType.CREATEACCOUNT;
//					para.IN.data.username = "new email";
//					para.IN.data.password = "new passord";
//					para.IN.data.firstname = "new firstname";
//					para.IN.data.lastname = "new lastname";

					// for update profile
//					para.IN.type = ActionType.UPDATEACCOUNT;
//					para.IN.data.firstname = "updated firstname";
//					para.IN.data.lastname = "update lastname";

					// for update settings
//					para.IN.type = ActionType.UPDATESETTINGS;
//					para.IN.data.settings = new usersettings();
//					para.IN.data.settings.push_new_event = "1";
//					para.IN.data.settings.push_news_update = "1";

					// for save token
					para.IN.type = ActionType.SAVETOKEN;
					para.IN.data.notification_token = "12345qwer";


					if ( !ds.Action(ref para) ) {
						testView.Text += "\nThere is an error happened during the action\n\n Error: " + para.OUT.errmsg + "\n\n";
					} else {
						testView.Text += "\n\nThe action is successful\n\n";
					}

				}

			};

		}


		void callBack (Boolean succeed, string errmsg)
		{
			InvokeOnMainThread(()=>{
				
				testView.Text += "\n-----Background synchronization is done-----\n\n";

				List<projectsummary> projs;
				ActionParameters para = new ActionParameters ();
				para.IN.type = ActionType.GETPROJINFO;
				if ( !ds.Action(ref para) ) {
					
					testView.Text += "Error: \n\n" + para.OUT.errmsg;

				} else {

					projs = (List<projectsummary>)para.OUT.dataset;
					testView.Text += "Project Information:\n\n";
					foreach(var item in projs) {
						testView.Text += String.Format("Name: {0}\nType: {1}\nStatus: {2}\n\n",item.name,item.type,item.phase);
					}

				}


			});
		}

	}

}

