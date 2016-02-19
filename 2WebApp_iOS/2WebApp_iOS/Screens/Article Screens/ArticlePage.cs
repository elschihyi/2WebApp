
using System;

using Foundation;
using UIKit;
using System.Drawing;

namespace WebApp_iOS
{
	public partial class ArticlePage : UIViewController
	{
		private NSUrlRequest urlRequest; 
		protected LoadingOverlay _loadPop = null;

		public ArticlePage (string url) : base ("ArticlePage", null)
		{
			urlRequest = new NSUrlRequest(new NSUrl(url));
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}
			
		public void Url(string url){
			urlRequest = new NSUrlRequest(new NSUrl(url));  
			try{
			if (webView != null) {
				//webView.ShouldStartLoad += HandleShouldStartLoad;
				webView.LoadRequest (urlRequest);
				webView.ScalesPageToFit = true;
				//webView.LoadStarted += ProgressbarLoad;
			}
			}catch(Exception e){
				var tmp = e.Message;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.


			GlobalAPI.Manager ().PageDefault (this, "",false, false);

			try{
			if (webView != null) {
				//webView.ShouldStartLoad += HandleShouldStartLoad;
				webView.LoadRequest (urlRequest);
				webView.ScalesPageToFit = true;
				//webView.LoadStarted += ProgressbarLoad;
			}
			}catch(Exception e){
				var tmp = e.Message; 
			}

		}

		void ProgressbarLoad (object sender, EventArgs e)
		{


			// Determine the correct size to start the overlay (depending on device orientation)
			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
				bounds.Size = new CoreGraphics.CGSize(bounds.Size.Height, bounds.Size.Width);
			}
			// show the loading overlay on the UI thread using the correct orientation sizing
			this._loadPop = new LoadingOverlay (bounds);
			this.View.Add ( this._loadPop );

		}
	}
}

