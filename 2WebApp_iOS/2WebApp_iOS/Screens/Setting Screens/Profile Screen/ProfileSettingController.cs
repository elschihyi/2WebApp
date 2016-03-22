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
		ProfileSettingView profileSettingView;

		//object
		public accountsummary theaccountsummary;
		public string oldpassword="";
		public string newpassword="";
		public string confirmpassword="";

		public ProfileSettingController (accountsummary theaccountsummary)
		{
			this.theaccountsummary = theaccountsummary;
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
			NavigationItem.Title="Settings";
			initView ();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}
		/********************************************************************************
		*Views initializations
		********************************************************************************/
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
			if (!string.IsNullOrEmpty (oldpassword) || !string.IsNullOrEmpty (newpassword) || !string.IsNullOrEmpty (confirmpassword)) {
				if (!string.Equals (oldpassword, theaccountsummary.client_password)) {
					UIAlertController Alert = UIAlertController.Create ("Error",
						"Please re-enter your old password.", UIAlertControllerStyle.Alert);
					Alert.AddAction (UIAlertAction.Create ("OK",
						UIAlertActionStyle.Cancel, null
					));
					PresentViewController (Alert, true, null);
					return;
				}

				if (!string.Equals (newpassword, confirmpassword)) {
					UIAlertController Alert = UIAlertController.Create ("Error",
						"New password did not match.", UIAlertControllerStyle.Alert);
					Alert.AddAction (UIAlertAction.Create ("OK",
						UIAlertActionStyle.Cancel, null
					));
					PresentViewController (Alert, true, null);
					return;
				}

				theaccountsummary.client_password = newpassword;
			}

			string errmsg;
			ActionParameters ap = new ActionParameters ();
			ap.IN.type = ActionType.UPDATEACCOUNT;
			ap.IN.data = theaccountsummary;
			ap.IN.func = (o,e) => {};
			if (GlobalAPI.GetDataService ().Action (ref ap)) {
				UIAlertController Alert = UIAlertController.Create ("Success",
					"", UIAlertControllerStyle.Alert);
				Alert.AddAction (UIAlertAction.Create ("OK",
					UIAlertActionStyle.Cancel, action=>{
						NavigationController.PopViewController(true);
					}		
				));
				PresentViewController (Alert, true, null);
				NavigationController.PopViewController(true);
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
		*Edit end
		********************************************************************************/
		public void EditEnd(int Section,int Row,string Text){
			switch (Section) {
			case 0:
				switch (Row) {
				case 0:
					theaccountsummary.client_firstname=Text;
					break;
				case 1:
					theaccountsummary.client_lastname=Text;
					break;
				case 2:
					break;
				default:
					break;
				}
				break;
			case 1:
				switch (Row) {
				case 0:
					oldpassword=Text;
					break;
				case 1:
					newpassword=Text;
					break;
				case 2:
					confirmpassword=Text;
					break;
				default:
					break;
				}
				break;
			default:
				break;
			}
		}
	}
}

