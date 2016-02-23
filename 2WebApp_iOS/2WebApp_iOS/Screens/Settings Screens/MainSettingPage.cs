
using System;
using System.Linq;
using System.Collections.Generic;

using MonoTouch.Dialog;

using Foundation;
using UIKit;
using System.Drawing;
using CoreGraphics;

namespace WebApp_iOS
{
	public partial class MainSettingPage : DialogViewController
	{
		_2WebDesignEntryElement profileSettingName; 
		_2WebDesignEntryElement profileSettingEmail; 



		_2WebDesignBooleanElement notificationPushWebNewEvent; 
		_2WebDesignBooleanElement notificationPushWebNewsUpdate; 

		_2WebDesignBooleanElement notificationPushProjectProjectUpdates;
		_2WebDesignBooleanElement notificationPushProjectApprovalDocument;
		_2WebDesignBooleanElement notificationPushProjectReleaseDocument; 

		_2WebDesignBooleanElement notificationPushSupportSupportUpdates;
		_2WebDesignBooleanElement notificationPushSupportWebsiteAudit; 
		_2WebDesignBooleanElement notificationPushSupportYearlyAnalysis; 



		_2WebDesignBooleanElement notificationEmailWebNewEvent; 
		_2WebDesignBooleanElement notificationEmailWebNewsUpdate; 

		_2WebDesignBooleanElement notificationEmailProjectProjectUpdates;
		_2WebDesignBooleanElement notificationEmailProjectApprovalDocument;
		_2WebDesignBooleanElement notificationEmailProjectReleaseDocument; 

		_2WebDesignBooleanElement notificationEmailSupportSupportUpdates;
		_2WebDesignBooleanElement notificationEmailSupportWebsiteAudit; 
		_2WebDesignBooleanElement notificationEmailSupportYearlyAnalysis; 

		_2WebDesignBooleanElement notificationEmailSubscribe;



		public MainSettingPage () : base (UITableViewStyle.Grouped, null)
		{
			Pushing = true; 


			profileSettingName = new _2WebDesignEntryElement ("Name", getView (), new UIImage ("Cut_Images/Name_Icon.png"));
			profileSettingEmail = new _2WebDesignEntryElement ("Email", getView (), new UIImage ("Cut_Images/Email_Icon.png")); 



			notificationPushWebNewEvent = new _2WebDesignBooleanElement("On new event",getView(),false); 
			notificationPushWebNewsUpdate = new _2WebDesignBooleanElement ("On news update",getView(), false); 

			notificationPushProjectProjectUpdates = new _2WebDesignBooleanElement ("On project updates",getView(), false);
			notificationPushProjectApprovalDocument = new _2WebDesignBooleanElement ("On design approval document",getView(), false);
			notificationPushProjectReleaseDocument = new _2WebDesignBooleanElement ("On website release document",getView(), false); 

			notificationPushSupportSupportUpdates = new _2WebDesignBooleanElement ("For support updates",getView(), false);
			notificationPushSupportWebsiteAudit = new _2WebDesignBooleanElement ("For website audit",getView(), false); 
			notificationPushSupportYearlyAnalysis = new _2WebDesignBooleanElement ("For yearly analysis",getView(), false); 



			notificationEmailWebNewEvent = new _2WebDesignBooleanElement("On new event",getView(),false); 
			notificationEmailWebNewsUpdate = new _2WebDesignBooleanElement ("On news update",getView(), false); 

			notificationEmailProjectProjectUpdates = new _2WebDesignBooleanElement ("On project updates",getView(), false);
			notificationEmailProjectApprovalDocument = new _2WebDesignBooleanElement ("On design approval document",getView(), false);
			notificationEmailProjectReleaseDocument = new _2WebDesignBooleanElement ("On website release document",getView(), false); 

			notificationEmailSupportSupportUpdates = new _2WebDesignBooleanElement ("For support updates",getView(), false);
			notificationEmailSupportWebsiteAudit = new _2WebDesignBooleanElement ("For website audit",getView(), false); 
			notificationEmailSupportYearlyAnalysis = new _2WebDesignBooleanElement ("For yearly analysis",getView(), false); 

			notificationEmailSubscribe = new _2WebDesignBooleanElement ("Subscribe to 2Web Email Blasts",getView(), false);



			var headerFrame = new RectangleF (0, 0, (float) UIScreen.MainScreen.Bounds.Width, 25);

			var headerAccount = new UILabel(headerFrame){
				Font = UIFont.BoldSystemFontOfSize (17),
				TextColor = GlobalAPI.Manager().getTwoWebColor(),
				BackgroundColor = UIColor.Clear,
				Text = "My Account"
			};

			var headerNotifications = new UILabel(headerFrame){
				Font = UIFont.BoldSystemFontOfSize (17),
				TextColor = GlobalAPI.Manager().getTwoWebColor(),
				BackgroundColor = UIColor.Clear,
				Text = "Notifications"
			};

			var headerProjects = new UILabel(headerFrame){
				Font = UIFont.BoldSystemFontOfSize (17),
				TextColor = GlobalAPI.Manager().getTwoWebColor(),
				BackgroundColor = UIColor.Clear,
				Text = "My Projects"
			};

			Root = new RootElement ("Settings") {
				new Section (headerAccount) {
					new _WebDesignRootElement ("Update Profile") {
						new Section () {
							profileSettingName,
							profileSettingEmail
						},
						new Section(){
							new _2WebDesignButtonElement("Reset Password",getView(),"")
						}
					}
				},
				new Section (headerNotifications) {
					new _WebDesignRootElement ("Push Notifications") {
						new Section ("2 Web Notifications") {
							notificationPushWebNewEvent,
							notificationPushWebNewsUpdate
						},
						new Section ("Project Notifications") {
							notificationPushProjectProjectUpdates,
							notificationPushProjectApprovalDocument,
							notificationPushProjectReleaseDocument
						},
						new Section ("Support Notifications") {
							notificationPushSupportSupportUpdates,
							notificationPushSupportWebsiteAudit,
							notificationPushSupportYearlyAnalysis
						}
					},
					new _WebDesignRootElement ("Email Notifications") {
						new Section ("2 Web Notifications") {
							notificationEmailWebNewEvent,
							notificationEmailWebNewsUpdate
						},
						new Section ("Project Notifications") {
							notificationEmailProjectProjectUpdates,
							notificationEmailProjectApprovalDocument,
							notificationEmailProjectReleaseDocument
						},
						new Section ("Support Notifications") {
							notificationEmailSupportSupportUpdates,
							notificationEmailSupportWebsiteAudit,
							notificationEmailSupportYearlyAnalysis
						},
						new Section () {
							notificationEmailSubscribe
						}
					}
				},
				new Section (headerProjects) {
					new _WebDesignRootElement ("Organization Settings") {
						new Section () {
							new _2WebDesignBadgeElement("Sarc",getView(),""),
							new _2WebDesignBadgeElement("Sarcan",getView(),""),
							new _2WebDesignBadgeElement("Employ Link",getView(),""),
							new _2WebDesignBadgeElement("Drop n Go",getView(),""),
							new _2WebDesignBadgeElement("Sarc",getView(),""),
							new _2WebDesignBadgeElement("Sarcan",getView(),""),
						},new Section(){
							new _2WebDesignButtonElement("Join New Organization",getView(),"")
						}
					},
					new _WebDesignRootElement ("Project Settings") {
						new Section () {
							new _2WebDesignBadgeElement("Sarc",getView(),""),
							new _2WebDesignBadgeElement("Sarcan",getView(),""),
							new _2WebDesignBadgeElement("Employ Link",getView(),""),
							new _2WebDesignBadgeElement("Drop n Go",getView(),""),
							new _2WebDesignBadgeElement("Sarc",getView(),""),
							new _2WebDesignBadgeElement("Sarcan",getView(),""),
						},new Section(){
							new _2WebDesignButtonElement("Join New Project",getView(),"")
						}
					}
				},
				new Section(""){
				}
			};



		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			NavigationItem.SetHidesBackButton (false, true); 
			GlobalAPI.Manager ().PageDefault(this, "", true, false);

			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			this.NavigationItem.RightBarButtonItem = new UIBarButtonItem (new UIImage ("Cut_Images/Log_Out_Icon.png"), UIBarButtonItemStyle.Plain, 
				(sender, args) => {
					/*
					if(!GlobalAPI.Manager().Logout())
						new UIAlertView("Alert","Error unable to logout",null, "Ok", null).Show(); 
					GlobalAPI.Manager().PushPage(NavigationController, new LoginFirstPage());
					//NavigationController.PopToRootViewController(true); 
					//NavigationController.ViewControllers = new UIViewController[]{new LoginFirstPage()}; 
					*/
				});
		}

		private UIView getView(){
			return new UIView (new CGRect (0, 0, UIScreen.MainScreen.Bounds.Width, 44)){BackgroundColor = UIColor.Clear}; 
		}




	}
}
