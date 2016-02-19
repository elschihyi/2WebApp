
using System;

using Foundation;
using UIKit;

namespace WebApp_iOS
{
	public partial class MainSupport : UIViewController
	{
		private UITableView table;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public MainSupport ()
			: base (UserInterfaceIdiomIsPhone ? "TabProjects_iPhone" : "TabProjects_iPad", null)
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

			table = new UITableView(new CoreGraphics.CGRect(0,40,View.Bounds.Width,View.Bounds.Height - 40)); // defaults to Plain style
			table.BackgroundColor = UIColor.Clear; 
			table.SectionIndexBackgroundColor = UIColor.Clear; 
			table.SeparatorStyle = UITableViewCellSeparatorStyle.None; 


			//Manual fix to bug 
			//Seems to be no longer needed in viewdidLoad
			//table.Frame = new CoreGraphics.CGRect (0, 63, View.Frame.Width, View.Frame.Height);

			if (GlobalAPI.Manager().SupportAuthorization("demo")) {

				LoadTable();

			} else {

				//Create Alert
				var okCancelAlertController = UIAlertController.Create ("Alert", "You currently have no active projects with 2Web.", UIAlertControllerStyle.Alert);

				//Add Actions
				okCancelAlertController.AddAction (UIAlertAction.Create ("Request Organization Access", UIAlertActionStyle.Default, alert => { GlobalAPI.Manager().ProjectAuthorization("demo"); LoadTable();}));
				okCancelAlertController.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, null));

				//Present Alert
				PresentViewController (okCancelAlertController, true, null);
			}


			//At the end so that it is over the table view
			GlobalAPI.Manager ().PageDefault (this, "Support",true, true);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);




		}

		private void LoadTable(){
			table.Source = new SupportTableSource (GlobalAPI.Manager().LoadSupport(), this.NavigationController);

			View.AddSubview (table);

		}
	}
}

