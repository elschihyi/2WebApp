
using System;

using Foundation;
using UIKit;
using System.Drawing;

namespace WebApp_iOS
{
	public partial class Settings : UIViewController
	{

		private UIView[] pages; 

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public Settings ()
			: base (UserInterfaceIdiomIsPhone ? "Settings2_iPhone" : "Settings2_iPad", null)
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
			
			// Perform any additional setup after loading the view, typically from a nib.


			GlobalAPI.Manager ().PageDefault (this, "2 Web Design",false, false);













//			pages = new UIView[]{ Page1, Page2, Page3, Page4 }; 
//
//			int i;
//
//			for (i = 0; i < pages.Length; i++) {
//				//UIView view = new UIView (); 
//				CoreGraphics.CGRect frame = new CoreGraphics.CGRect(); 
//
//				frame.X = (this.ScrollView.Frame.Width * i) + 1;
//				frame.Y = this.ScrollView.Frame.Y; 
//
//				frame.Height = this.ScrollView.Frame.Height;
//				frame.Width = this.ScrollView.Frame.Width; 
//
//				pages [i].Frame = frame;
//				this.ScrollView.AddSubview (pages[i]);
//			}
//
//			// set pages and content size
//			this.PageControl.Pages = i;
//			ScrollView.ContentSize = new CoreGraphics.CGSize (ScrollView.Frame.Width * i, ScrollView.Frame.Height - 70);
//
//			this.ScrollView.Scrolled += ScrollEvent; 
//
//
//
//			btnLogout.TouchUpInside += (object sender, EventArgs e) => {
//				//clear authentication token
//				try {
//					DbStorage.Manager().AuthenticationLogout(); 
//				} catch (Exception ex) {
//					new UIAlertView ("Alert", ex.Message, null, "OK", null).Show (); 
//				}
//
//				NavigationController.PopToRootViewController(false);  
//			};

		}

//		private void ScrollEvent (object sender, EventArgs e)
//		{
//			this.PageControl.CurrentPage = 
//				(int)System.Math.Floor (ScrollView.ContentOffset.X
//			/ this.ScrollView.Frame.Size.Width);
//		}
	}
}

