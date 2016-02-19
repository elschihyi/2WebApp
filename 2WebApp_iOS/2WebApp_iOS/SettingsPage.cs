
using System;

using Foundation;
using UIKit;

namespace WebApp_iOS
{
	public partial class SettingsPage : UIViewController
	{
		public SettingsPage () : base ("SettingsPage", null)
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

			//SettingsTable.Source = new ExpandableTableSource<> ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

