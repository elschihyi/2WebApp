using System;
using UIKit;
using System.Collections.Generic;
using System.Drawing;
using CoreDataService;

namespace WebApp_iOS
{
	public class EmailNotificationController: UIViewController
	{
		//views
		LoadingOverlay2 loadingOverlayView;
		EmailNotificationView emailNotificationView;

		//object
		public List<bool> EmailNotificationList;

		public EmailNotificationController ()
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
			GetEmailNotificationList();
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
			emailNotificationView = new EmailNotificationView (
				new RectangleF(0f,(float)y,(float)UIScreen.MainScreen.Bounds.Width,(float)(UIScreen.MainScreen.Bounds.Height-y-66.0f)));
			emailNotificationView.TitleLabel.Text="Email Notifications";
			emailNotificationView.TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			emailNotificationView.TableView.Source = new EmailNotificationSource (this);
			View.Add (emailNotificationView);
		}

		/********************************************************************************
		*Btn clicks
		********************************************************************************/
		public void SwitchValueChanges(int Section,int Row)
		{
			Console.WriteLine ("Email Notification section:"+Section+" Row:" + Row + " Clicked");
		}

		/********************************************************************************
		*Load data from database
		********************************************************************************/
		public void GetEmailNotificationList(){
			DataService dataService = GlobalAPI.GetDataService();
			string errmsg;
			//if (!dataService.ProjectInfo (out theProjectList, out errmsg)) {
			if(!true){
				if(EmailNotificationList==null){
					EmailNotificationList = new List<bool> ();
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
				if(EmailNotificationList==null){
					//EmailNotificationList = new List<string> ();
					EmailNotificationList=MyEmailNotificationList();
				}
				/*
				InvokeOnMainThread (() => {
					initView ();
					loadingOverlayView.Hide ();
				});
				*/
				initView ();
				loadingOverlayView.Hide ();
				GlobalAPI.Manager ().PageDefault (this, "Settings", true, false);
			}	

		}

		public List<bool> MyEmailNotificationList(){
			List<bool> returnList = new List<bool> ();
			returnList.Add (true);
			returnList.Add (false);
			returnList.Add (true);
			returnList.Add (false);
			returnList.Add (true);
			returnList.Add (false);
			returnList.Add (true);
			returnList.Add (false);
			return returnList;
		}	
	}
}

