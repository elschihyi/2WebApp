using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;
using SDWebImage;
using CoreDataService;

namespace WebApp_iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		
		private UIWindow window;

		public override void FinishedLaunching (UIApplication application)
		{
			window = new UIWindow ();

			window.Frame = new CoreGraphics.CGRect (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height); 

			var rootNavigationController = new UINavigationController (); 

			UINavigationBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.BarTintColor = UIColor.Clear;
			UINavigationBar.Appearance.BackgroundColor = UIColor.Clear; 
			UINavigationBar.Appearance.SetBackgroundImage (new UIImage (), UIBarMetrics.Default); 
			UINavigationBar.Appearance.ShadowImage = new UIImage (); 

			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes () {
				TextColor = UIColor.White, TextShadowColor = UIColor.Clear
			}); 



			window.RootViewController = rootNavigationController; 
			window.BackgroundColor = UIColor.FromPatternImage (new UIImage("2WebDesignImages/bg.png")); 
			window.MakeKeyAndVisible ();

			rootNavigationController.PushViewController (new LunchPage (), false); 

		}

		public override void DidEnterBackground (UIApplication application)
		{
			ActionParameters ap = new ActionParameters ();
			ap.IN.type = ActionType.SYNCATSTARTUP;
			ap.IN.data = new accountsummary ();
			ap.IN.func=(o,e)=>{};
			GlobalAPI.GetDataService ().Action (ref ap);
		}
			
		public override void WillEnterForeground (UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
			ActionParameters ap = new ActionParameters ();
			ap.IN.type = ActionType.SYNCATSTARTUP;
			ap.IN.data = new accountsummary ();
			ap.IN.func=(o,e)=>{};
			GlobalAPI.GetDataService ().Action (ref ap);
		}

		public override void ReceiveMemoryWarning (UIApplication application)
		{
			NSUrlCache.SharedCache.RemoveAllCachedResponses (); 
			SDWebImageManager.SharedManager.ImageCache.ClearMemory ();
			SDWebImageManager.SharedManager.ImageCache.ClearDisk (); 
		}
	}
}

