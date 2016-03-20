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
		LoadingOverlay2 loadingOverlayView;
		OrganizationSettingView organizationSettingView;

		//object
		public List<string> theOrganizationList;

		public OrganizationSettingController ()
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
			GetOrganizationList();
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
			Console.WriteLine ("Organiztion Setting Row " + Row + " Clicked");
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
		public void GetOrganizationList(){
			DataService dataService = GlobalAPI.GetDataService();
			string errmsg;
			//if (!dataService.ProjectInfo (out theProjectList, out errmsg)) {
			if(!true){
				if(theOrganizationList==null){
					theOrganizationList = new List<string> ();
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
				if(theOrganizationList==null){
					//theOrganizationList = new List<string> ();
					theOrganizationList=MyOrgList();
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

		public List<string> MyOrgList(){
			List<string> returnList = new List<string> ();
			returnList.Add ("SARC");
			returnList.Add ("Rock Paper Coffee");
			returnList.Add ("BreakOutSask");
			returnList.Add ("SARC222");
			return returnList;
		}	
	}
}

