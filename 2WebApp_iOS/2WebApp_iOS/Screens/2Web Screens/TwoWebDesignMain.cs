
using System;

using Foundation;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;
using MessageUI;

namespace WebApp_iOS
{
	public partial class TwoWebDesignMain : UIViewController
	{
//		//put this as global var. 
//		protected LoadingOverlay _loadPop = null;
//
//		// Determine the correct size to start the overlay (depending on device orientation)
//		var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
//		if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
//			bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
//		}
//
//		// show the loading overlay on the UI thread using the correct orientation sizing
//		this._loadPop = new LoadingOverlay (bounds);
//		this.View.AddSubview ( this._loadPop );
//
//		Task.Factory.StartNew (
//
//			tt => {
//
//			}, TaskScheduler.FromCurrentSynchronizationContext()
//		).ContinueWith (
//			t => {
//				this._loadPop.Hide ();
//			}, TaskScheduler.FromCurrentSynchronizationContext()
//		);

		MFMailComposeViewController mailController; 


		public TwoWebDesignMain () : base ("TwoWebDesignMain", null)
		{
			
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);



		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		
			GlobalAPI.Manager ().PageDefault (this, "2 Web", true, true);


			//Populate Blogs Page
			tbvBlogs.Source = new NewsletterTableSource(GlobalAPI.Manager().LoadNewsletters(),this); 
			tbvBlogs.BackgroundColor = UIColor.Clear;
			//tbvBlogs.RowHeight = 165; 
			tbvBlogs.RowHeight = 145; 
			tbvBlogs.SeparatorStyle = UITableViewCellSeparatorStyle.None; 

			//Populate Workshop Page
			tbvWorkshops.Source = new EventsTableSource(GlobalAPI.Manager().LoadEvents(), this); 
			tbvWorkshops.BackgroundColor = UIColor.Clear; 
			tbvWorkshops.SeparatorStyle = UITableViewCellSeparatorStyle.None; 


			//populate contact scroll page
			ContactScrl.Frame = new CoreGraphics.CGRect(0,0,View.Bounds.Width, View.Bounds.Height - 40); 
			ContactScrl.ContentSize = ContactVw.Frame.Size;
			ContactScrl.AddSubview (ContactVw); 


			//load the contact page social buttons
			btnGo.TouchUpInside += (object sender, EventArgs e) => {
				//GlobalAPI.Manager().PushPage(NavigationController,GlobalAPI.Manager().getInternetPage("https://plus.google.com/u/0/+2webdesign/posts")); 
				GlobalAPI.Manager().PushPage(NavigationController,new WebViewController("https://plus.google.com/u/0/+2webdesign/posts",""));
			};

			btnTw.TouchUpInside += (object sender, EventArgs e) => {
				//GlobalAPI.Manager().PushPage(NavigationController,GlobalAPI.Manager().getInternetPage("https://twitter.com/2webdesign")); 
				GlobalAPI.Manager().PushPage(NavigationController,new WebViewController("https://twitter.com/2webdesign",""));
			};

			btnFb.TouchUpInside += (object sender, EventArgs e) => {
				//GlobalAPI.Manager().PushPage(NavigationController,GlobalAPI.Manager().getInternetPage("https://www.facebook.com/2webdesign")); 
				GlobalAPI.Manager().PushPage(NavigationController,new WebViewController("https://www.facebook.com/2webdesign",""));
			};

			btnLi.TouchUpInside += (object sender, EventArgs e) => {
				//GlobalAPI.Manager().PushPage(NavigationController,GlobalAPI.Manager().getInternetPage("http://www.linkedin.com/company/2webdesign-com")); 
				GlobalAPI.Manager().PushPage(NavigationController,new WebViewController("https://www.linkedin.com/company/2webdesign-com",""));
			};

			btnPin.TouchUpInside += (object sender, EventArgs e) => {
				//GlobalAPI.Manager().PushPage(NavigationController,GlobalAPI.Manager().getInternetPage("https://www.pinterest.com/2webdesign")); 
				GlobalAPI.Manager().PushPage(NavigationController,new WebViewController("https://www.pinterest.com/2webdesign",""));
			};
				
			if (MFMailComposeViewController.CanSendMail) {
				btnEmail.TouchUpInside += (object sender, EventArgs e) => {
					mailController = new MFMailComposeViewController (); 

					mailController.SetToRecipients (new string[]{ "info@webdesign.com" }); 
					mailController.SetSubject (""); 
					mailController.SetMessageBody ("", false); 

					mailController.Finished += (object s, MFComposeResultEventArgs args) => {
						args.Controller.DismissViewController (true, null);
					};
					//PresentViewController (mailController, true, null);
					UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(mailController, true, null);
				};
			} else {
				//device can't make emails
				btnEmail.Hidden = true;
			}

			if (GlobalAPI.Manager ().phoneAvailability ()) {
				btnCallUs.TouchUpInside += (object sender, EventArgs e) => {
					UIApplication.SharedApplication.OpenUrl(new NSUrl("tel:3066642932"));
				};
			} else {
				//device can't make calls
				btnCallUs.Hidden = true; 
			}

			btnVisitUs.TouchUpInside += (object sender, EventArgs e) => {
				GlobalAPI.Manager().PushPage(NavigationController, new ArticlePage("http://www.2webdesign.com")); 
			};
				

			//Courasal Pages
			var pages = new UIView[]{ BlogView, WorkshopsView, ContactView }; 

			int i;

			ScrollView.Frame = new CGRect (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 70); 
			PageControl.Frame = new CGRect (0, UIScreen.MainScreen.Bounds.Height - 55, UIScreen.MainScreen.Bounds.Width, 40); 
		
			for (i = 0; i < pages.Length; i++) {
				//UIView view = new UIView (); 
				CoreGraphics.CGRect frame = new CoreGraphics.CGRect (); 

				frame.X = (this.ScrollView.Frame.Width * i);
				frame.Y = this.ScrollView.Frame.Y; 

				frame.Height = this.ScrollView.Frame.Height;
				frame.Width = this.ScrollView.Frame.Width; 

				pages [i].Frame = frame;
				this.ScrollView.AddSubview (pages [i]);
			}

			// set pages and content size
			PageControl.Pages = i;
			ScrollView.ContentSize = new CoreGraphics.CGSize (ScrollView.Frame.Width * i, ScrollView.Frame.Height - 70);
			 
			tbvBlogs.Frame = new CGRect (0, 40, ScrollView.Frame.Width, ScrollView.Frame.Height - 80); 
			tbvWorkshops.Frame = new CGRect (0, 40, ScrollView.Frame.Width, ScrollView.Frame.Height - 80); 


			ScrollView.Scrolled += ScrollEvent; 

		}
		
		//For the courasel pages
		private void ScrollEvent (object sender, EventArgs e)
		{
			PageControl.CurrentPage = 
			(int)System.Math.Floor (ScrollView.ContentOffset.X
			/ this.ScrollView.Frame.Size.Width);
		}

	}
}

