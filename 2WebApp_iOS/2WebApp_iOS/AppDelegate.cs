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
			//set up pushnotifications
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var pushSettings = UIUserNotificationSettings.GetSettingsForTypes (
					UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
					new NSSet ());

				UIApplication.SharedApplication.RegisterUserNotificationSettings (pushSettings);
				UIApplication.SharedApplication.RegisterForRemoteNotifications ();
			} else {
				UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes (notificationTypes);
			}

			//set up windowns
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

		public override void ReceiveMemoryWarning (UIApplication application)
		{
			NSUrlCache.SharedCache.RemoveAllCachedResponses (); 
			SDWebImageManager.SharedManager.ImageCache.ClearMemory ();
			SDWebImageManager.SharedManager.ImageCache.ClearDisk (); 
		}

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			// Get current device token
			var DeviceToken = deviceToken.Description;
			if (!string.IsNullOrWhiteSpace(DeviceToken)) {
				DeviceToken = DeviceToken.Trim('<').Trim('>');
			}

			// Get previous device token
			var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");

			// Has the token changed?
			if (string.IsNullOrEmpty(oldDeviceToken) || !oldDeviceToken.Equals(DeviceToken))
			{
				//Notice Server here
			}

			// Save new device token 
			NSUserDefaults.StandardUserDefaults.SetString(DeviceToken, "PushDeviceToken");

			UIAlertController Alert = UIAlertController.Create ("Recieve Token",
				DeviceToken, UIAlertControllerStyle.Alert);
			Alert.AddAction (UIAlertAction.Create ("OK",
				UIAlertActionStyle.Cancel,
				null));

			int l=((UINavigationController)window.RootViewController).ViewControllers.Length;
			((UINavigationController)window.RootViewController).ViewControllers [l - 1].PresentViewController (Alert, true, null);

			Console.WriteLine ("Token is:" + DeviceToken);
		}

		public override void FailedToRegisterForRemoteNotifications (UIApplication application , NSError error)
		{
			Console.WriteLine ("Fail to get Token:" + error.LocalizedDescription);
		}

		public override void DidReceiveRemoteNotification (UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
		{
			NSObject apsValue=userInfo.ValueForKey(new NSString("aps"));
			NSObject alertValue=apsValue.ValueForKey(new NSString("alert"));
			try{
				string msg=((NSString)alertValue).ToString();
				Console.WriteLine ("Get Message:" + msg);
			}catch {
				Console.WriteLine ("Get Message:" +userInfo.ToString ());
			}
		}
	}
}

