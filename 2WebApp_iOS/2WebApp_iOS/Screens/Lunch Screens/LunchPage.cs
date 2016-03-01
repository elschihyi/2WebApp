
using System;

using Foundation;
using UIKit;
using System.Drawing;
using System.Threading.Tasks;
using CoreDataService;

namespace WebApp_iOS
{
	public partial class LunchPage : UIViewController
	{
		UIImageView animatedImage;
		private int i= 0;
		private string num = "";
		private NSTimer timer;

		private bool SyncFinished=false;
		private bool SyncSuccess=false;
		private string SyncErrMsg="";

		UILabel SyncLabel;


		public LunchPage () : base ("LunchPage", null)
		{
		}

		[Export("AnimationTimeLoop")]
		public void AnimationTimeLoop() {
			if (i <= 97) {
				// Dispose of existing image
				if (animatedImage.Image != null) {
					animatedImage.Image.Dispose ();
					animatedImage.Image = null;
				}

				// load the string file prefix
				if (i < 10)
					num = "0" + i.ToString ();
				else
					num = i.ToString (); 

				//loop next frame of the animation
				animatedImage.Image = UIImage.FromFile ("launch_images/01-Splash_crane_0" + num + ".jpg");

				// Increment
				i = i + 5;
			} else if (!SyncFinished) {
				//do nothing..
				if (SyncLabel == null) {
					SyncLabel = new UILabel ();
					SyncLabel.BackgroundColor = UIColor.Clear;
					SyncLabel.TextColor = UIColor.White;
					SyncLabel.Text="Laoding Data..";
					SyncLabel.Font=UIFont.SystemFontOfSize (16f);
					SyncLabel.TextAlignment = UITextAlignment.Center;
					SyncLabel.Frame=new RectangleF(0f*(float)UIScreen.MainScreen.Bounds.Width,
						0.8f*(float)UIScreen.MainScreen.Bounds.Height,
						1.0f*(float)UIScreen.MainScreen.Bounds.Width,
						0.2f*(float)UIScreen.MainScreen.Bounds.Height);
					View.AddSubview (SyncLabel);
				}	

			} else {// Stop if at end
				if (SyncSuccess) {
					//preload rss feed
					GlobalAPI.Manager ().loadRss ();

					//dispose timer
					timer.Invalidate ();
					timer.Dispose ();
					timer = null;
					animatedImage = null;  
					GlobalAPI.Manager ().PushPage (NavigationController, new WelcomePage ());  
				} else {
					//preload rss feed
					GlobalAPI.Manager ().loadRss ();

					//dispose timer
					timer.Invalidate ();
					timer.Dispose ();
					timer = null;
					animatedImage = null; 

					//alert
					UIAlertController Alert = UIAlertController.Create ("Sync Error",
						SyncErrMsg, UIAlertControllerStyle.Alert);
					Alert.AddAction (UIAlertAction.Create ("OK",
						UIAlertActionStyle.Cancel, action=>{
							GlobalAPI.Manager ().PushPage (NavigationController, new WelcomePage ()); 
						}
					));
					PresentViewController (Alert, true, null);
				}
			}	
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

			//Activate animation timer
			animatedImage = new UIImageView(); 
			animatedImage.Frame = new CoreGraphics.CGRect (0,0,View.Frame.Width,View.Frame.Height - 0);
			View.AddSubview(animatedImage);


			timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromMilliseconds(100), delegate {
				AnimationTimeLoop(); 
			});

			//sync data while animate
			syncData();
		}

		/********************************************************************************
		*Load data from database
		********************************************************************************/
		public void syncData(){
			DataService dataService = GlobalAPI.GetDataService();
			dataService.Sync (SyncRespond);
		}

		/********************************************************************************
		*Load data responds
		********************************************************************************/
		public void SyncRespond(Boolean succeed, string errmsg){
			SyncSuccess=succeed;
			SyncErrMsg=errmsg;
			SyncFinished=true;
		}
	}
}

