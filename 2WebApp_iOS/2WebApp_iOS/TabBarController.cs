using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace WebApp_iOS
{
	partial class TabBarController : UITabBarController
	{
		private Settings2 settingsPage; 

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public TabBarController (IntPtr handle) : base (handle)
		{
			
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.

//			Settings.Clicked += (object sender, EventArgs e) => {
//				if(settingsPage == null) 
//					settingsPage = new Settings(); 
//				NavigationController.PushViewController(settingsPage , true); 
//			};


			btnSetting.TouchUpInside += (object sender, EventArgs e) => {
				if(settingsPage == null)
					settingsPage = new Settings2(); 
				NavigationController.PushViewController(settingsPage,true);  
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}






		#endregion
	}
}
