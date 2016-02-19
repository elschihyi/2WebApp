
using System;

using Foundation;
using UIKit;

namespace WebApp_iOS
{
	public partial class TabProjects : UIViewController
	{
		//private Boolean firstLoad = true;

		private UITableView table;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public TabProjects ()
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


			table = new UITableView(View.Bounds); // defaults to Plain style
			table.SeparatorStyle = UITableViewCellSeparatorStyle.None; 
			table.BackgroundColor = UIColor.Clear; 
			table.Frame = new CoreGraphics.CGRect (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 60); 

			table.SectionIndexBackgroundColor = UIColor.Clear; 


			//Manual fix to bug , seems to be no longer needed in view did load. 
			//table.Frame = new CoreGraphics.CGRect (0, 63, View.Frame.Width, View.Frame.Height);

			if (GlobalAPI.Manager().ProjectAuthorization("demo")) {

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

			//at the end so that it is over the table view
			GlobalAPI.Manager ().PageDefault (this, "Projects",true, true);

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);



		}

		private void LoadTable(){
			table.Source = new ProjectsTableSource (GlobalAPI.Manager().LoadProjects(), this.NavigationController);

			Add (table);





		}
	}
}

