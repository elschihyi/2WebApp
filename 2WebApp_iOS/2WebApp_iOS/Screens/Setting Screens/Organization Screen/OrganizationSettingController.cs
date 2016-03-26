using System;
using UIKit;
using System.Collections.Generic;
using System.Drawing;
using CoreDataService;
using MessageUI;

namespace WebApp_iOS
{
	public class OrganizationSettingController: UIViewController
	{
		//views
		OrganizationSettingView organizationSettingView;

		//object
		public accountsummary theaccountsummary;
		public List<userorg> theOrganizationList;

		public OrganizationSettingController (accountsummary theaccountsummary)
		{
			this.theaccountsummary = theaccountsummary;
			theOrganizationList = theaccountsummary.organizations;
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
			organizationSettingView = new OrganizationSettingView (
				new RectangleF(0f,(float)y,(float)UIScreen.MainScreen.Bounds.Width,(float)(UIScreen.MainScreen.Bounds.Height-y-66.0f)));
			organizationSettingView.TitleLabel.Text="Organiztion Settings";
			organizationSettingView.TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			organizationSettingView.TableView.Source = new OrganizationSettingSource (this);
			View.Add (organizationSettingView);
		}

		/********************************************************************************
		*Btn clicks
		********************************************************************************/
		public void RequestBtnClick(int Row)
		{
			contact contactInfo;
			//string errmsg="";
			ActionParameters ap = new ActionParameters ();
			ap.IN.type = ActionType.GETCONTINFO;
			ap.IN.data = new AccountInfo ();
			ap.IN.func = (o,e) => {};
			if (GlobalAPI.GetDataService ().Action (ref ap)&&MFMailComposeViewController.CanSendMail) {
				contactInfo = (contact)ap.OUT.dataset;
				MFMailComposeViewController mailController = new MFMailComposeViewController (); 
				mailController.SetToRecipients (new string[]{contactInfo.support_email}); 
				mailController.SetSubject ("Organization Request access"+theOrganizationList[Row].name); 
				mailController.SetMessageBody ("", false);
				mailController.Finished += (object s1, MFComposeResultEventArgs args) => {
					args.Controller.DismissViewController (true, null);
				};
				PresentViewController (mailController, true, null);
			}
		}
			
		public void RequestBtn2Click(int Row)
		{
			contact contactInfo;
			//string errmsg="";
			ActionParameters ap = new ActionParameters ();
			ap.IN.type = ActionType.GETCONTINFO;
			ap.IN.data = new AccountInfo ();
			ap.IN.func = (o,e) => {};
			if (GlobalAPI.GetDataService ().Action (ref ap)&&MFMailComposeViewController.CanSendMail) {
				contactInfo = (contact)ap.OUT.dataset;
				MFMailComposeViewController mailController = new MFMailComposeViewController (); 
				mailController.SetToRecipients (new string[]{contactInfo.support_email}); 
				mailController.SetSubject ("Join new organization"); 
				mailController.SetMessageBody ("", false);
				mailController.Finished += (object s1, MFComposeResultEventArgs args) => {
					args.Controller.DismissViewController (true, null);
				};
				PresentViewController (mailController, true, null);
			}
		}
	}
}

