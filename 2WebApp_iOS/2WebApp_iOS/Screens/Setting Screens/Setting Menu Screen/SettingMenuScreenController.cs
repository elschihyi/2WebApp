using System;
using UIKit;
using System.Drawing;
using CoreDataService;

namespace WebApp_iOS
{
	public class SettingMenuScreenController: UIViewController
	{
		//Views
		UITableView TableView;

		//object
		public accountsummary theaccountsummary;
		bool isLogin = false;

		public SettingMenuScreenController ()
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
			setNavigationItems ();
			initView ();
			GlobalAPI.Manager ().PageDefault (this, "Settings", true, false);


		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			//get user info
			GetAccountSummary();

			if (!isLogin) {
				GlobalAPI.Manager().PushPage(NavigationController,new LoginRegisterController ());
			}
		}
		/********************************************************************************
		*Set navigation Items
		********************************************************************************/
		public void setNavigationItems(){
			bool isLogin = true;
			if (isLogin) {
				NavigationItem.RightBarButtonItem = new UIBarButtonItem (UIImage.FromFile ("Cut_Images/Log_Out_Icon.png"), UIBarButtonItemStyle.Plain, (s,a)=> {
					string errmsg;
					ActionParameters ap = new ActionParameters ();
					ap.IN.type = ActionType.LOGOUT;
//					ap.IN.data = theaccountsummary;
					ap.IN.func = (o,e) => {};
					if (GlobalAPI.GetDataService ().Action (ref ap)) {
						NavigationController.PopViewController (true);
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


				});
			}
		}

		/********************************************************************************
		*Views initializations
		********************************************************************************/
		public void initView(){
			TableView = new UITableView ();
			var statusbar=UIApplication.SharedApplication.StatusBarFrame.Size.Height;
			var navigationbarHeight = NavigationController.NavigationBar.Frame.Size.Height;
			var y = statusbar + navigationbarHeight;
			TableView.Frame = new RectangleF (0f, (float)y + 5f, (float)UIScreen.MainScreen.Bounds.Width, (float)(UIScreen.MainScreen.Bounds.Height - y - 5f-66.0f));
			TableView.BackgroundColor = UIColor.Clear;
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.Source = new SettingMenuScreenSource (this);
			TableView.AllowsSelection = true;
			TableView.ScrollEnabled = false;
			View.Add (TableView);
		}

		/********************************************************************************
		*Load data from database
		********************************************************************************/
		public void GetAccountSummary(){
			string errmsg;
			ActionParameters ap = new ActionParameters ();
			ap.IN.type = ActionType.GETACCTINFO;
			ap.IN.data = new AccountInfo ();
			ap.IN.func = (o,e) => {};
			if (GlobalAPI.GetDataService ().Action (ref ap)) {
				theaccountsummary = (accountsummary)ap.OUT.dataset;
				isLogin = !String.IsNullOrEmpty (theaccountsummary.client_email);
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
	}
}

