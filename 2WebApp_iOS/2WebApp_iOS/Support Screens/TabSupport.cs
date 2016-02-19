
using System;

using Foundation;
using UIKit;

namespace WebApp_iOS
{
	public partial class TabSupport : UIViewController
	{
		private UITableView table;

		public TabSupport () : base ("TabSupport", null)
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

			GlobalAPI.Manager ().PageDefault (this, "Support", true);


		}
	}
}

