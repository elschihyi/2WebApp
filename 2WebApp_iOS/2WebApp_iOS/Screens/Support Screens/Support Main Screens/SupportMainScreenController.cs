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
			GetProjectList();
		}
		/********************************************************************************
		*Views initializations
		********************************************************************************/
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
			string errmsg;
			ActionParameters ap = new ActionParameters ();
			ap.IN.type = ActionType.GETPROJINFO;
			ap.IN.data = new accountsummary ();
			ap.IN.func = (o,e) => {};
			if (GlobalAPI.GetDataService ().Action (ref ap)) {
				projectList = (List<projectsummary>)ap.OUT.dataset;
				errmsg = ap.OUT.errmsg;
				if(projectList==null){
					projectList = new List<projectsummary> ();
				}
				initTableView ();
			
				//put menu and setting
				GlobalAPI.Manager ().PageDefault (this, "Projects", true, true);
			} else {
				//alert
				errmsg = ap.OUT.errmsg;
				UIAlertController Alert = UIAlertController.Create ("Error",
					errmsg, UIAlertControllerStyle.Alert);
				Alert.AddAction (UIAlertAction.Create ("OK",
					UIAlertActionStyle.Cancel, action=>{
						NavigationController.PopViewController(true);
					}		
				));
				PresentViewController (Alert, true, null);
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

