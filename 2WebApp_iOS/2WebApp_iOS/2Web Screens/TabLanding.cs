
using System;

using Foundation;
using UIKit;
using SatelliteMenu;

namespace WebApp_iOS
{
	public partial class TabLanding : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public TabLanding ()
			: base (UserInterfaceIdiomIsPhone ? "TabLanding_iPhone" : "TabLanding_iPad", null)
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

			EventsTable.Source = new EventsTableSource(NetworkAPI.Manager().LoadEvents()); 
			NewsletterTable.Source = new NewsletterTableSource (NetworkAPI.Manager().LoadNewsletters()); 





		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

		 
		}


	}
}

