using System;
using UIKit;
using Foundation;
using System.Drawing;

namespace WebApp_iOS
{
	public class WebViewController:UIViewController
	{
		public UIWebView WebView;
		public string url;
		public string title;

		public WebViewController (string url,string Title)
		{
			this.url = url;
			this.title = Title;
		}

		/********************************************************************************
		 * override funcrions
		 ********************************************************************************/
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NavigationItem.Title = title;
			this.NavigationController.NavigationBar.BackgroundColor = UIColor.Black;

			initWebView();
			View.Add (WebView);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			WebView.Frame = new RectangleF (0f , 0f , (float)UIScreen.MainScreen.Bounds.Width, (float)UIScreen.MainScreen.Bounds.Height);
			WebView.Reload ();
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);

			WebView.Frame = new RectangleF (0f , 0f , (float)UIScreen.MainScreen.Bounds.Width, (float)UIScreen.MainScreen.Bounds.Height);
			WebView.Reload ();
		}


		/********************************************************************************
		 *customus functions
		 ********************************************************************************/

		/********************************************************************************
		*Views initializations
		********************************************************************************/
		public void initWebView(){
			WebView = new UIWebView ();
			WebView.Frame = new RectangleF (0f , 0f , (float)UIScreen.MainScreen.Bounds.Width, (float)UIScreen.MainScreen.Bounds.Height);
			WebView.ScalesPageToFit =true;
			if (!url.ToLower ().Contains ("https://")) {
				NavigationController.PopViewController (true);
			} else {
				var UrlRequest = new NSUrlRequest (new NSUrl (url));
				WebView.LoadRequest(UrlRequest);
			}	
		}


	}
}

