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
			GetProjectList();
		}
		/********************************************************************************
		*Views initializations
		********************************************************************************/
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
			string errmsg;
			ActionParameters ap = new ActionParameters ();
			ap.IN.type = ActionType.GETPROJINFO;
			ap.IN.data = new accountsummary ();
			ap.IN.func = (o,e) => {};
			if (GlobalAPI.GetDataService ().Action (ref ap)) {
				projectList = (List<projectsummary>)ap.OUT.dataset;
				errmsg = ap.OUT.errmsg;
				if(projectList==null){
					projectList = new List<projectsummary> ();
				}
				try{
					initView (projectList[0].status=="DEMO");
				}
				catch{
					initView(true);
				}	
				//put menu and setting
				GlobalAPI.Manager ().PageDefault (this, "Projects", true, true);


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
		*Btn clicks
		********************************************************************************/
		public void LinkProjectBtnClick(){
			contact contactInfo;
			ActionParameters ap = new ActionParameters ();
			ap.IN.type = ActionType.GETCONTINFO;
			ap.IN.data = new accountsummary ();
			ap.IN.func = (o,e) => {};
			if (GlobalAPI.GetDataService ().Action (ref ap)) {
				contactInfo = (contact)ap.OUT.dataset;
				if (MFMailComposeViewController.CanSendMail && !String.IsNullOrEmpty (contactInfo.email)) {
					;
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
}

