using System;
using UIKit;
using System.Drawing;

namespace WebApp_iOS
{
	public class SettingMenuScreenController: UIViewController
	{
		//Views
		UITableView TableView;

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
			//NavigationItem.Title="Settings";
			setNavigationItems ();
			initView ();
			GlobalAPI.Manager ().PageDefault (this, "Settings", true, false);

			bool isLogin = false;
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
					NavigationController.PopViewController (true);
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
	}
}

