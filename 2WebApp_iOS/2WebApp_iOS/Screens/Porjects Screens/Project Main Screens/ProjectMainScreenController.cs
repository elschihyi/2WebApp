using System;
using UIKit;
using System.Collections.Generic;
using CoreDataService;
using System.Drawing;
using MessageUI;

namespace WebApp_iOS
{
	public class ProjectMainController: UIViewController
	{
		//Views
		LoadingOverlay2 loadingOverlayView;
		LinkProjectView LinkProjectView;
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
			GetProjectList();
		}
		/********************************************************************************
		*Views initializations
		********************************************************************************/
		public void initLoadingScreenView(string Text){
			loadingOverlayView=new LoadingOverlay2 (Text);
			View.Add (loadingOverlayView);
		}

		public void initView(bool isDemo){
			TableView = new UITableView ();
			LinkProjectView = new LinkProjectView ();
			LinkProjectView.Button1.TouchUpInside += (s, e) => {
				LinkProjectBtnClick();
			};	
			var statusbar=UIApplication.SharedApplication.StatusBarFrame.Size.Height;
			var navigationbarHeight = NavigationController.NavigationBar.Frame.Size.Height;
			var y = statusbar + navigationbarHeight;
			if (isDemo) {
				LinkProjectView.Hidden = false;
				LinkProjectView.Frame=new RectangleF ((float)LinkProjectView.Frame.X,(float)y + 5f , (float)LinkProjectView.Frame.Width, (float)LinkProjectView.Frame.Height);
				TableView.Frame = new RectangleF (0f, (float)y + 20f+90f, (float)UIScreen.MainScreen.Bounds.Width, (float)(UIScreen.MainScreen.Bounds.Height - y - 20f-90f));
			} else {
				LinkProjectView.Hidden = true;
				TableView.Frame = new RectangleF (0f, (float)y + 5f, (float)UIScreen.MainScreen.Bounds.Width, (float)(UIScreen.MainScreen.Bounds.Height - y - 5f));
			}	
			TableView.BackgroundColor = UIColor.Clear;
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.Source = new ProjectMainScreenScource (this);
			TableView.AllowsSelection = true;
			View.Add (TableView);
			View.Add (LinkProjectView);
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
					loadingOverlayView.Hide ();
					Random rnd1 = new Random();
					initView (rnd1.Next(2)==0);

					//put menu and setting
					GlobalAPI.Manager ().PageDefault (this, "Projects", true, true);

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
					Random rnd1 = new Random();
					initView (rnd1.Next(2)==0);
					//put menu and setting
					GlobalAPI.Manager ().PageDefault (this, "Projects", true, true);
					loadingOverlayView.Hide ();
				});
			}	
	
		}
		/********************************************************************************
		*Btn clicks
		********************************************************************************/
		public void LinkProjectBtnClick(){
			contact contactInfo;
			string errmsg="";
			GlobalAPI.GetDataService ().ContactInfo (out contactInfo, out errmsg);
			if (MFMailComposeViewController.CanSendMail && !String.IsNullOrEmpty (contactInfo.email)) {;
				MFMailComposeViewController mailController = new MFMailComposeViewController (); 
				mailController.SetToRecipients (new string[]{ contactInfo.email }); 
				mailController.SetSubject (""); 
				mailController.SetMessageBody ("", false);
				mailController.Finished += (object s1, MFComposeResultEventArgs args) => {
					args.Controller.DismissViewController (true, null);
				};
				PresentViewController (mailController, true, null);
			}
		}	

	}
}

