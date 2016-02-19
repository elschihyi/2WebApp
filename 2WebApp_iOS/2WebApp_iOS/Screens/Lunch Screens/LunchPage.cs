
using System;

using Foundation;
using UIKit;
using System.Drawing;
using System.Threading.Tasks;

namespace WebApp_iOS
{
	public partial class LunchPage : UIViewController
	{
		UIImageView animatedImage;
		private int i= 0;
		private string num = "";
		private NSTimer timer;

		public LunchPage () : base ("LunchPage", null)
		{
 
			//scroll not overlap heading, also on projects page. 

			//settings logo position, iphone 4 
			//"are you sure" 

			//focus on 5, and up. 
		}

		[Export("AnimationTimeLoop")]
		public void AnimationTimeLoop() {

			// Dispose of existing image
			if (animatedImage.Image != null) {
				animatedImage.Image.Dispose();
				animatedImage.Image = null;
			}

			// load the string file prefix
			if (i < 10)
				num = "0" + i.ToString ();
			else
				num = i.ToString (); 

			//loop next frame of the animation
			animatedImage.Image = UIImage.FromFile("launch_images/01-Splash_crane_0"+num+".jpg");

			// Increment
			i=i+5;

			// Stop if at end
			if (i > 97) {
				//preload rss feed
				GlobalAPI.Manager().loadRss();

				//dispose timer
				timer.Invalidate();
				timer.Dispose();
				timer = null;
				animatedImage = null; 

				//GlobalAPI.Manager ().PushPage (NavigationController, new LoginFirstPage ()); 
				GlobalAPI.Manager ().PushPage (NavigationController, new WelcomePage ()); 
				//NavigationController.PushViewController (new WelcomePage (), false); 

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
			
			// Perform any additional setup after loading the view, typically from a nib.

			//AnimationScreen ();


			//Activate animation timer
			animatedImage = new UIImageView(); 
			animatedImage.Frame = new CoreGraphics.CGRect (0,0,View.Frame.Width,View.Frame.Height - 0);


			View.AddSubview(animatedImage);


			timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromMilliseconds(100), delegate {
				AnimationTimeLoop(); 
			});
			//timer.Invoke (AnimationTimeLoop, TimeSpan.FromMilliseconds (100)); 

		}
			
	}
}

