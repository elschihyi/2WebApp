using System;
using UIKit;
using System.Collections.Generic;
using CoreDataService;
using System.Drawing;

namespace WebApp_iOS
{
	public class ProjectMainController: UIViewController
	{
		//Views
		LoadingOverlay2 loadingOverlayView;
		UITableView TableView;

		//objects
		public List<projectsummary> projectList;

		public ProjectMainController ()
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			AutomaticallyAdjustsScrollViewInsets = false;

			//check if loggin screen need to pop or not
			initLoadingScreenView ("Loading...");
			bool userisloggedin = true;
			if (userisloggedin) {
				//sync data and get project
				GetProjectList();
				//projectList = myProjects ();
			} else {
				//insert login page
			}	
		}
		/********************************************************************************
		*Views initializations
		********************************************************************************/
		public void initLoadingScreenView(string Text){
			loadingOverlayView=new LoadingOverlay2 (Text);
			View.Add (loadingOverlayView);
		}

		public void initTableView(){
			TableView = new UITableView ();
			var statusbar=UIApplication.SharedApplication.StatusBarFrame.Size.Height;
			var navigationbarHeight = NavigationController.NavigationBar.Frame.Size.Height;
			var y = statusbar + navigationbarHeight;
			TableView.Frame=new RectangleF(0f,(float)y,(float)UIScreen.MainScreen.Bounds.Width,(float)(UIScreen.MainScreen.Bounds.Height-y));
			TableView.BackgroundColor = UIColor.Clear;
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.Source = new ProjectMainScreenScource (this);
			TableView.AllowsSelection = true;
			View.Add (TableView);
		}
		/********************************************************************************
		*Load data from database
		********************************************************************************/
		public void GetProjectList(){
			DataService dataService = GlobalAPI.GetDataService();
			string errmsg;
			if (!dataService.ProjectInfo (out projectList, out errmsg)) {
				projectList = new List<projectsummary> ();
				InvokeOnMainThread (() => {
					initTableView ();
					//put menu and setting
					GlobalAPI.Manager ().PageDefault (this, "Projects", true, true);
					loadingOverlayView.Hide ();
					//alert
					UIAlertController Alert = UIAlertController.Create ("Error",
						                         errmsg, UIAlertControllerStyle.Alert);
					Alert.AddAction (UIAlertAction.Create ("OK",
						UIAlertActionStyle.Cancel, null
					));
					PresentViewController (Alert, true, null);
				});
			} else {
				if(projectList==null){
					projectList = new List<projectsummary> ();
				}
				InvokeOnMainThread (() => {
					initTableView ();
					//put menu and setting
					GlobalAPI.Manager ().PageDefault (this, "Projects", true, true);
					loadingOverlayView.Hide ();
				});
			}	
	
		}	
		

		//***********************************************************************
		public List<projectsummary> myProjects(){
			List<projectsummary> projectList = new List<projectsummary>();
			//p1******************************************
			projectsummary p1 = new projectsummary ();
			p1.name = "Sarc";
			p1.type  = "Redesign";
			p1.status  = "design";
			p1.org_name  = "Sarc";
			p1.client_name  = "Sydney Smith";
			p1.client_email  = "ssmith@clientorg.com";
			p1.staff_name  = "Jillian Hare";
			p1.staff_email  = "jHare@2web.com";

			p1.update = new List<task> ();
			task p1t1 = new task ();
			p1t1.name="update name 1";
			p1t1.date = "2015/12/01";
			p1t1.file_url="https://www.google.com";
			p1.update.Add (p1t1);

			task p1t2 = new task ();
			p1t2.name="update name 2";
			p1t2.date = "2015/12/02";
			p1t2.file_url="https://www.google.com";
			p1.update.Add (p1t2);

			p1.support_package = null;
			projectList.Add (p1);

			//p1******************************************
			projectsummary p2 = new projectsummary ();
			p2.name = "Sarcan";
			p2.type  = "Redesign";
			p2.status  = "design";
			p2.org_name  = "Sarc";
			p2.client_name  = "Sydney Smith";
			p2.client_email  = "ssmith@clientorg.com";
			p2.staff_name  = "Jillian Hare";
			p2.staff_email  = "jHare@2web.com";

			p2.update = new List<task> ();
			task p2t1 = new task ();
			p2t1.name="update name 1";
			p2t1.date = "2015/12/03";
			p2t1.file_url="https://www.google.com";
			p2.update.Add (p2t1);

			task p2t2 = new task ();
			p2t2.name="update name 2";
			p2t2.date = "2015/12/04";
			p2t2.file_url="https://www.google.com";
			p2.update.Add (p2t2);

			p2.support_package = null;
			projectList.Add (p2);
			return projectList;
		}	
	}
}

