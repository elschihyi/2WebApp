
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

			//new Contact View
			ContactView newContactView =new ContactView();
			newContactView.Frame = new CoreGraphics.CGRect(0,0,View.Bounds.Width, View.Bounds.Height - 40); 
			newContactView.ContentSize = ContactVw.Frame.Size;
			newContactView.position ();
			newContactView.titleLabel.Text="Contact 2 Web Design Inc.";
			ContactInfo contactInfo = myHardCodeInfo ();
			if (!String.IsNullOrEmpty (contactInfo.address1)) {
				newContactView.address1Label.Hidden = false;
				newContactView.address1Label.Text = contactInfo.address1;
			} else {
				newContactView.address1Label.Hidden = true;
			}
			if (!String.IsNullOrEmpty (contactInfo.address2)) {
				newContactView.address2Label.Hidden = false;
				newContactView.address2Label.Text = contactInfo.address2;
			} else {
				newContactView.address2Label.Hidden = true;
			}
			if (MFMailComposeViewController.CanSendMail&&!String.IsNullOrEmpty (contactInfo.email)) {
				newContactView.emailLabel.Hidden = false;
				newContactView.emailLabel.Text = contactInfo.email;
				newContactView.emailButton.Hidden = false;
				newContactView.emailButton.TouchUpInside += (s, e) => {
					mailController = new MFMailComposeViewController (); 
					mailController.SetToRecipients (new string[]{contactInfo.email}); 
					mailController.SetSubject (""); 
					mailController.SetMessageBody ("", false); 
					mailController.Finished += (object s1, MFComposeResultEventArgs args) => {
						args.Controller.DismissViewController (true, null);
					};
					//PresentViewController (mailController, true, null);
					UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(mailController, true, null);
				};	
			} else {
				newContactView.emailLabel.Hidden = true;
				newContactView.emailButton.Hidden = true;
			}
			if (!String.IsNullOrEmpty (contactInfo.phone)) {
				newContactView.phoneLabel.Hidden = false;
				newContactView.phoneLabel.Text = contactInfo.phone;
				newContactView.phoneButton.Hidden = false;
				newContactView.phoneButton.TouchUpInside += (s, e) => {
					//make phone call here
				};
			} else {
				newContactView.phoneLabel.Hidden = true;
				newContactView.phoneButton.Hidden = true;
			}
			if (!String.IsNullOrEmpty (contactInfo.visit)) {
				newContactView.visitLabel.Hidden = false;
				newContactView.visitLabel.Text = contactInfo.visit;
				newContactView.visitButton.Hidden = false;
				newContactView.visitButton.TouchUpInside += (s, e) => {
					GlobalAPI.Manager().PushPage(NavigationController,new WebViewController(contactInfo.visit,""));
				};
			} else {
				newContactView.visitLabel.Hidden = true;
				newContactView.visitButton.Hidden = true;
			}

			//***************************************************************************************************************************************************
			if (!String.IsNullOrEmpty (contactInfo.Facebook)) {
				newContactView.FacebookBtn.Hidden = false;
				newContactView.FacebookBtn.TouchUpInside += (s, e) => {
					GlobalAPI.Manager().PushPage(NavigationController,new WebViewController(contactInfo.Facebook,""));
				};
			} else {
				newContactView.FacebookBtn.Hidden = true;
			}
			if (!String.IsNullOrEmpty (contactInfo.Twitter)) {
				newContactView.TwitterBtn.Hidden = false;
				newContactView.TwitterBtn.TouchUpInside += (s, e) => {
					GlobalAPI.Manager().PushPage(NavigationController,new WebViewController(contactInfo.Twitter,""));
				};
			} else {
				newContactView.TwitterBtn.Hidden = true;
			}
			if (!String.IsNullOrEmpty (contactInfo.Google)) {
				newContactView.GoogleBtn.Hidden = false;
				newContactView.GoogleBtn.TouchUpInside += (s, e) => {
					GlobalAPI.Manager().PushPage(NavigationController,new WebViewController(contactInfo.Google,""));
				};
			} else {
				newContactView.GoogleBtn.Hidden = true;
			}
			if (!String.IsNullOrEmpty (contactInfo.LinkIn)) {
				newContactView.LinkInBtn.Hidden = false;
				newContactView.LinkInBtn.TouchUpInside += (s, e) => {
					GlobalAPI.Manager().PushPage(NavigationController,new WebViewController(contactInfo.LinkIn,""));
				};
			} else {
				newContactView.LinkInBtn.Hidden = true;
			}

			if (!String.IsNullOrEmpty (contactInfo.YouTube)) {
				newContactView.PinterestBtn.Hidden = false;
				newContactView.PinterestBtn.TouchUpInside += (s, e) => {
					GlobalAPI.Manager().PushPage(NavigationController,new WebViewController(contactInfo.YouTube,""));
				};
			} else {
				newContactView.PinterestBtn.Hidden = true;
			}

			//load the contact page social buttons
			/*
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
			*/	

			//Courasal Pages
			var pages = new UIView[]{ BlogView, WorkshopsView, newContactView}; 

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
				https://twitter.com/2webdesign
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


		//
		public ContactInfo myHardCodeInfo(){
			ContactInfo hardcodeInfo = new ContactInfo ();
			hardcodeInfo.address1="116-116 Research Drive, Saskatoon,";
			hardcodeInfo.address2="SK S7N3R3";
			hardcodeInfo.email="info@2webdesign";
			hardcodeInfo.phone="306.664.2932";
			hardcodeInfo.visit="www.2webdesign.com";
			hardcodeInfo.Facebook="https://www.facebook.com/2webdesign";
			hardcodeInfo.Twitter="https://twitter.com/2webdesign";
			hardcodeInfo.Google="https://plus.google.com/u/0/+2webdesign/posts";
			hardcodeInfo.LinkIn="https://www.linkedin.com/company/2webdesign-com";
			hardcodeInfo.YouTube="https://www.pinterest.com/2webdesign";
			return hardcodeInfo;
		}	
	}

	public class ContactInfo{
		public string address1="";
		public string address2="";
		public string email="";
		public string phone="";
		public string visit="";
		public string Facebook="";
		public string Twitter="";
		public string Google="";
		public string LinkIn="";
		public string YouTube="";
	}	
}

