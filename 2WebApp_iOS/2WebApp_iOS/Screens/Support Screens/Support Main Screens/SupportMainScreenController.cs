using System;
using UIKit;
using CoreDataService;
using System.Collections.Generic;
using System.Drawing;

namespace WebApp_iOS
{
	public class SupportMainScreenController: UIViewController
	{

		//Views
		LoadingOverlay2 loadingOverlayView;
		UITableView TableView;

		//objects
		public List<projectsummary> projectList;

		public SupportMainScreenController ()
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
			GetProjectList();
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
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.BackgroundColor = UIColor.Clear;
			TableView.Source = new SupportMainScreenSource (this);
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
				if(projectList==null){
					projectList = new List<projectsummary> ();
				}
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
					GlobalAPI.Manager ().PageDefault (this, "Supports", true, true);
					loadingOverlayView.Hide ();
				});
			}	

		}

		/********************************************************************************
		*Btn clicks
		********************************************************************************/
		public void CellViewClick(int Row)
		{
			GlobalAPI.Manager().PushPage(NavigationController,
				new ProjectSupportScreenController(projectList[Row],FromScreenToSupport.Support));	
		}
	}
}

