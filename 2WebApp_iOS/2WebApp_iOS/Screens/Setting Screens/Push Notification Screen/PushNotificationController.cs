using System;
using UIKit;
using System.Collections.Generic;
using System.Drawing;
using CoreDataService;

namespace WebApp_iOS
{
	public class PushNotificationController: UIViewController
	{
		//views
		PushNotificationView pushNotificationView;

		//object
		public accountsummary theaccountsummary;
		public accountsummary OriginAccountSummary;


		public PushNotificationController (accountsummary theaccountsummary)
		{
			this.theaccountsummary = theaccountsummary;
			this.OriginAccountSummary = CloneAccountSummary (theaccountsummary);
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
			if (ValueChanged ()) {
				string errmsg;
				ActionParameters ap = new ActionParameters ();
				ap.IN.type = ActionType.UPDATESETTINGS;
//				ap.IN.data = theaccountsummary;
//				ap.IN.func = (o, e) => {};
//				theaccountsummary.settings.usersetting_updated="1";
				ap.IN.data = new AccountInfo();
//				ap.IN.data.usersetting_updated = "1";
				ap.IN.data.username = theaccountsummary.client_email;
				ap.IN.data.password = theaccountsummary.client_password;
				ap.IN.data.remember_password = theaccountsummary.remember_password;
				ap.IN.data.settings = theaccountsummary.settings;
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
		}
		/********************************************************************************
		*Views initializations
		********************************************************************************/
		public void initView(){
			var statusbar=UIApplication.SharedApplication.StatusBarFrame.Size.Height;
			var navigationbarHeight = NavigationController.NavigationBar.Frame.Size.Height;
			var y = statusbar + navigationbarHeight;
			pushNotificationView = new PushNotificationView (
				new RectangleF(0f,(float)y,(float)UIScreen.MainScreen.Bounds.Width,(float)(UIScreen.MainScreen.Bounds.Height-y-66.0f)));
			pushNotificationView.TitleLabel.Text="Push Notifications";
			pushNotificationView.TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			pushNotificationView.TableView.Source = new PushNotificationSource (this);
			View.Add (pushNotificationView);
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
					theaccountsummary.settings.push_new_event = theaccountsummary.settings.push_new_event=="1"?"0":"1";
					break;
				case 1:
					theaccountsummary.settings.push_news_update = theaccountsummary.settings.push_news_update=="1"?"0":"1";
					break;
				default:
					break;
				}
				break;
			case 1:
				switch (Row) {
				case 0:
					theaccountsummary.settings.push_project_update = theaccountsummary.settings.push_project_update=="1"?"0":"1";
					break;
				case 1:
					theaccountsummary.settings.push_approval_doc = theaccountsummary.settings.push_approval_doc=="1"?"0":"1";
					break;
				case 2:
					theaccountsummary.settings.push_release_doc = theaccountsummary.settings.push_release_doc=="1"?"0":"1";
					break;
				default:
					break;
				}
				break;
			case 2:
				switch (Row) {
				case 0:
					theaccountsummary.settings.push_support_update = theaccountsummary.settings.push_support_update=="1"?"0":"1";
					break;
				case 1:
					theaccountsummary.settings.push_website_audit = theaccountsummary.settings.push_website_audit=="1"?"0":"1";
					break;
				case 2:
					theaccountsummary.settings.push_yearly_analysis = theaccountsummary.settings.push_yearly_analysis=="1"?"0":"1";
					break;
				default:
					break;
				}
				break;
			default:
				break;
			}
		}
		/********************************************************************************
		*functions
		********************************************************************************/
		public bool ValueChanged(){
			if (OriginAccountSummary.client_email != theaccountsummary.client_email ||
				OriginAccountSummary.client_password != theaccountsummary.client_password ||
				OriginAccountSummary.client_firstname != theaccountsummary.client_firstname ||
				OriginAccountSummary.client_lastname != theaccountsummary.client_lastname ||
				OriginAccountSummary.status != theaccountsummary.status ||
				OriginAccountSummary.settings.client_accountid != theaccountsummary.settings.client_accountid ||
//				OriginAccountSummary.settings.remember_password != theaccountsummary.settings.remember_password ||
				OriginAccountSummary.settings.push_new_event != theaccountsummary.settings.push_new_event ||
				OriginAccountSummary.settings.push_news_update != theaccountsummary.settings.push_news_update ||
				OriginAccountSummary.settings.push_project_update != theaccountsummary.settings.push_project_update ||
				OriginAccountSummary.settings.push_approval_doc != theaccountsummary.settings.push_approval_doc ||
				OriginAccountSummary.settings.push_release_doc != theaccountsummary.settings.push_release_doc ||
				OriginAccountSummary.settings.push_support_update != theaccountsummary.settings.push_support_update ||
				OriginAccountSummary.settings.push_website_audit != theaccountsummary.settings.push_website_audit ||
				OriginAccountSummary.settings.push_yearly_analysis != theaccountsummary.settings.push_yearly_analysis ||
				OriginAccountSummary.settings.email_new_event != theaccountsummary.settings.email_new_event ||
				OriginAccountSummary.settings.email_news_update != theaccountsummary.settings.email_news_update ||
				OriginAccountSummary.settings.email_project_update != theaccountsummary.settings.email_project_update ||
				OriginAccountSummary.settings.email_approval_doc != theaccountsummary.settings.email_approval_doc ||
				OriginAccountSummary.settings.email_release_doc != theaccountsummary.settings.email_release_doc ||
				OriginAccountSummary.settings.email_support_update != theaccountsummary.settings.email_support_update ||
				OriginAccountSummary.settings.email_website_audit != theaccountsummary.settings.email_website_audit ||
				OriginAccountSummary.settings.email_yearly_analysis != theaccountsummary.settings.email_yearly_analysis ||
				OriginAccountSummary.settings.email_blasts != theaccountsummary.settings.email_blasts) {
				return true;
			}

			return false;
		}

		public accountsummary CloneAccountSummary(accountsummary oldaccountsummary){
			accountsummary newAccountSummary=new accountsummary();

			newAccountSummary.client_email = oldaccountsummary.client_email;
			newAccountSummary.client_password = oldaccountsummary.client_password;
			newAccountSummary.client_firstname = oldaccountsummary.client_firstname;
			newAccountSummary.client_lastname = oldaccountsummary.client_lastname;
			newAccountSummary.status = oldaccountsummary.status;

			newAccountSummary.settings = new usersettings();
			newAccountSummary.settings.client_accountid = oldaccountsummary.settings.client_accountid;
//			newAccountSummary.settings.remember_password = oldaccountsummary.settings.remember_password;
			newAccountSummary.settings.push_new_event = oldaccountsummary.settings.push_new_event;
			newAccountSummary.settings.push_news_update = oldaccountsummary.settings.push_news_update;
			newAccountSummary.settings.push_project_update = oldaccountsummary.settings.push_project_update;
			newAccountSummary.settings.push_approval_doc = oldaccountsummary.settings.push_approval_doc;
			newAccountSummary.settings.push_release_doc = oldaccountsummary.settings.push_release_doc;
			newAccountSummary.settings.push_support_update = oldaccountsummary.settings.push_support_update;
			newAccountSummary.settings.push_website_audit = oldaccountsummary.settings.push_website_audit;
			newAccountSummary.settings.push_yearly_analysis = oldaccountsummary.settings.push_yearly_analysis;

			newAccountSummary.settings.email_new_event = oldaccountsummary.settings.email_new_event;
			newAccountSummary.settings.email_news_update = oldaccountsummary.settings.email_news_update;
			newAccountSummary.settings.email_project_update = oldaccountsummary.settings.email_project_update;
			newAccountSummary.settings.email_approval_doc = oldaccountsummary.settings.email_approval_doc;
			newAccountSummary.settings.email_release_doc = oldaccountsummary.settings.email_release_doc;
			newAccountSummary.settings.email_support_update = oldaccountsummary.settings.email_support_update;
			newAccountSummary.settings.email_website_audit = oldaccountsummary.settings.email_website_audit;
			newAccountSummary.settings.email_yearly_analysis = oldaccountsummary.settings.email_yearly_analysis;
			newAccountSummary.settings.email_blasts = oldaccountsummary.settings.email_blasts;

			newAccountSummary.organizations = new List<userorg> ();
			foreach (var x in oldaccountsummary.organizations) {
				userorg newuserorg = new userorg ();
				newuserorg.name = x.name;
				newAccountSummary.organizations.Add (newuserorg);
			}	

			newAccountSummary.projects = new List<userproj> ();
			foreach (var x in oldaccountsummary.projects) {
				userproj newProj = new userproj ();
				newProj.name = x.name;
				newProj.primary_contact = x.primary_contact;
			}

			return newAccountSummary;
		}	
	}
}

