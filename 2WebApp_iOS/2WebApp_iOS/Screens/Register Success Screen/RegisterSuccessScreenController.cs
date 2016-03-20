using System;
using UIKit;
using System.Drawing;

namespace WebApp_iOS
{
	public class RegisterSuccessScreenController: UIViewController
	{
		//Views
		RegisterSuccessView  registerSuccessView;

		public RegisterSuccessScreenController ()
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
		}

		/********************************************************************************
		*Set navigation Items
		********************************************************************************/
		public void setNavigationItems(){
			NavigationItem.HidesBackButton=true;
		}

		/********************************************************************************
		*Views initializations
		********************************************************************************/
		public void initView(){
			var statusbar=UIApplication.SharedApplication.StatusBarFrame.Size.Height;
			var navigationbarHeight = NavigationController.NavigationBar.Frame.Size.Height;
			var y = statusbar + navigationbarHeight;
			registerSuccessView = new RegisterSuccessView (new RectangleF (0f, (float)y + 5f, (float)UIScreen.MainScreen.Bounds.Width, (float)(UIScreen.MainScreen.Bounds.Height - y - 5f)));
			registerSuccessView.okBtn.TouchUpInside += (s, e) => {
				OKBtnClick();
			};	
			View.Add (registerSuccessView);
		}

		/********************************************************************************
		*Btn clicks
		********************************************************************************/
		public void OKBtnClick(){
			NavigationController.PopToViewController(GlobalAPI.welcomePage,true);
		}
	}
}

