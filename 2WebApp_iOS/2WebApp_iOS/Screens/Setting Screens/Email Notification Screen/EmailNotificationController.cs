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
		EmailNotificationView emailNotificationView;

		//object
		public accountsummary theaccountsummary;

		public EmailNotificationController (accountsummary theaccountsummary)
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


		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			string errmsg;
			ActionParameters ap = new ActionParameters ();
			ap.IN.type = ActionType.UPDATESETTINGS;
			ap.IN.data = theaccountsummary;
			ap.IN.func = (o,e) => {};
			if (GlobalAPI.GetDataService ().Action (ref ap)) {
				//do nothing if success
			} else {
				//alert
				errmsg = ap.OUT.errmsg;
				UIAlertController Alert = UIAlertController.Create ("Error",
					errmsg, UIAlertControllerStyle.Alert);
				Alert.AddAction (UIAlertAction.Create ("OK",
					UIAlertActionStyle.Cancel, null
				));
				PresentViewController (Alert, true, null);
			}
		}
		/********************************************************************************
		*Views initializations
		********************************************************************************/
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
			switch (Section) {
			case 0:
				switch (Row) {
				case 0:
					theaccountsummary.settings.email_new_event = theaccountsummary.settings.email_new_event=="1"?"0":"1";
					break;
				case 1:
					theaccountsummary.settings.email_news_update = theaccountsummary.settings.email_news_update=="1"?"0":"1";
					break;
				default:
					break;
				}
				break;
			case 1:
				switch (Row) {
				case 0:
					theaccountsummary.settings.email_project_update = theaccountsummary.settings.email_project_update=="1"?"0":"1";
					break;
				case 1:
					theaccountsummary.settings.email_approval_doc = theaccountsummary.settings.email_approval_doc=="1"?"0":"1";
					break;
				case 2:
					theaccountsummary.settings.email_release_doc = theaccountsummary.settings.email_release_doc=="1"?"0":"1";
					break;
				default:
					break;
				}
				break;
			case 2:
				switch (Row) {
				case 0:
					theaccountsummary.settings.email_support_update = theaccountsummary.settings.email_support_update=="1"?"0":"1";
					break;
				case 1:
					theaccountsummary.settings.email_website_audit = theaccountsummary.settings.email_website_audit=="1"?"0":"1";
					break;
				case 2:
					theaccountsummary.settings.email_yearly_analysis = theaccountsummary.settings.email_yearly_analysis=="1"?"0":"1";
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

