using System;
using UIKit;
using System.Drawing;
using CoreDataService;
using Foundation;
using CoreGraphics;

namespace WebApp_iOS
{
	public class ProfileSettingController: UIViewController
	{
		//views
		LoadingOverlay2 loadingOverlayView;
		ProfileSettingView profileSettingView;

		//object
		public UserProfile userProfile;
		public string oldpassword="";
		public string newpassword="";
		public string confirmpassword="";
		//public UserProfile newUserProfile;

		public ProfileSettingController ()
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
			GetUserProfile();
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
			profileSettingView = new ProfileSettingView (
				new RectangleF(0f,(float)y,(float)UIScreen.MainScreen.Bounds.Width,(float)(UIScreen.MainScreen.Bounds.Height-y-66.0f)));
			profileSettingView.TitleLabel.Text="Profile Setting";
			profileSettingView.TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			profileSettingView.TableView.Source = new ProfileSettingSource (this);
			//move the view when the keyboard is up
			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.DidShowNotification,KeyBoardUpNotification);
			View.Add (profileSettingView);
		}


		private void KeyBoardUpNotification(NSNotification notification)
		{
			UITableView TableView = profileSettingView.TableView;

			UIView activeview=new UIView();

			// get the keyboard size
			CGRect r = UIKeyboard.BoundsFromNotification (notification);

			// Find what opened the keyboard
			foreach (UIView view in TableView.VisibleCells) {
				var myObject = view as ProfileSettingCell;
				if (myObject != null&&myObject.TextField.IsFirstResponder)
				{
					activeview = myObject;
				}
			}

			if(TableView.ContentOffset.Y<activeview.Frame.Y-2.0f*(float)activeview.Frame.Height)
			{	
				TableView.SetContentOffset (new CGPoint(0.0f,activeview.Frame.Y-2.0f*(float)activeview.Frame.Height),true);
			}
		}

		/********************************************************************************
		*Btn clicks
		********************************************************************************/
		public void UpdateClick()
		{
			Console.WriteLine ("First Name:"+userProfile.firstName);
			Console.WriteLine ("Last Name:"+userProfile.lastName);
			Console.WriteLine ("Email:"+userProfile.email);
			Console.WriteLine ("Old Password:"+oldpassword);
			Console.WriteLine ("New Password:"+newpassword);
			Console.WriteLine ("Confirm Password:"+confirmpassword);
		}

		/********************************************************************************
		*Load data from database
		********************************************************************************/
		public void GetUserProfile(){
			DataService dataService = GlobalAPI.GetDataService();
			string errmsg;
			//if (!dataService.ProjectInfo (out theProjectList, out errmsg)) {
			if(!true){
				if(userProfile==null){
					userProfile = new UserProfile();
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
				if(userProfile==null){
					//userProfile = new UserProfile();
					userProfile=MyProfile();
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


		//temp functions*********************************************
		public UserProfile MyProfile(){
			UserProfile returnValue = new UserProfile ();
			returnValue.firstName="Terence";
			returnValue.lastName="Hunag";
			returnValue.email="chh990@mail.usask.ca";
			returnValue.password="123456";
			return returnValue;
		}	
	}

	public class UserProfile{
		public string firstName="";
		public string lastName="";
		public string email="";
		public string password="";
	}	
}

