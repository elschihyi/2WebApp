using System;
using UIKit;
using System.Drawing;
using System.Collections.Generic;
using CoreDataService;
using MessageUI;

namespace WebApp_iOS
{
	public class ProjectSettingsController: UIViewController
	{

		//views
		LoadingOverlay2 loadingOverlayView;
		ProjectSettingsView projectSettingsView;

		//object
		public List<projectsummary> theProjectList;

		public ProjectSettingsController ()
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
			initLoadingScreenView ("Loading...");
			NavigationItem.Title="Settings";
			GetProjectList();
			//initView ();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		/********************************************************************************
		*Views initializations
		********************************************************************************/
		public void initLoadingScreenView(string Text){
			loadingOverlayView=new LoadingOverlay2 (Text);
			View.Add (loadingOverlayView);
		}

		public void initView(){
			var statusbar=UIApplication.SharedApplication.StatusBarFrame.Size.Height;
			var navigationbarHeight = NavigationController.NavigationBar.Frame.Size.Height;
			var y = statusbar + navigationbarHeight;
			projectSettingsView = new ProjectSettingsView (
				new RectangleF(0f,(float)y,(float)UIScreen.MainScreen.Bounds.Width,(float)(UIScreen.MainScreen.Bounds.Height-y-66.0f)));
			projectSettingsView.TitleLabel.Text="Project Settings";
			projectSettingsView.TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			projectSettingsView.TableView.Source = new ProjectSettingsSource (this);
			View.Add (projectSettingsView);
		}

		/********************************************************************************
		*Btn clicks
		********************************************************************************/
		public void RequestBtnClick(int Row)
		{
			Console.WriteLine ("Project Setting Row " + Row + " Clicked");
		}

		public void RequestBtn2Click(int Row)
		{
			if (MFMailComposeViewController.CanSendMail) {;
				MFMailComposeViewController mailController = new MFMailComposeViewController (); 
				mailController.SetToRecipients (new string[]{""}); 
				mailController.SetSubject (""); 
				mailController.SetMessageBody ("", false);
				mailController.Finished += (object s1, MFComposeResultEventArgs args) => {
					args.Controller.DismissViewController (true, null);
				};
				PresentViewController (mailController, true, null);
			}
		}

		/********************************************************************************
		*Load data from database
		********************************************************************************/
		public void GetProjectList(){
			DataService dataService = GlobalAPI.GetDataService();
			string errmsg;
			if (!dataService.ProjectInfo (out theProjectList, out errmsg)) {
				if(theProjectList==null){
					theProjectList = new List<projectsummary> ();
				}
				InvokeOnMainThread (() => {
					loadingOverlayView.Hide ();
					initView ();
					GlobalAPI.Manager ().PageDefault (this, "Settings", true, false);
					//alert
					UIAlertController Alert = UIAlertController.Create ("Error",
						errmsg, UIAlertControllerStyle.Alert);
					Alert.AddAction (UIAlertAction.Create ("OK",
						UIAlertActionStyle.Cancel, null
					));
					PresentViewController (Alert, true, null);
				});
			} else {
				if(theProjectList==null){
					theProjectList = new List<projectsummary> ();
				}
				InvokeOnMainThread (() => {
					initView ();
					loadingOverlayView.Hide ();
					GlobalAPI.Manager ().PageDefault (this, "Settings", true, false);
				});
			}	

		}
	}
}

