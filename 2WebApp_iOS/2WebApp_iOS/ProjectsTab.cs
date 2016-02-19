using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace WebApp_iOS
{
	public partial class ProjectsTab : UITableViewController
	{
		private Boolean firstLoad = true;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public ProjectsTab (IntPtr handle) : base (handle)
		{
			this.Title = NSBundle.MainBundle.LocalizedString ("Projects", "Projects");
			this.TabBarItem.Image = UIImage.FromBundle ("second");

			 
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}





		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.

			 

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			//Manual fix to bug in storyboard 
			if (firstLoad) {
				this.TableView.Frame = new CoreGraphics.CGRect (0, 63, TableView.Frame.Width, TableView.Frame.Height);
				firstLoad = false; 
			}
			else
				this.TableView.Frame = new CoreGraphics.CGRect (0, 0, TableView.Frame.Width, TableView.Frame.Height);

			TableView.Source = new ProjectsTableSource (new Project[] {
				new Project {
					ProjectTitle = "Sarcan",
					Progress = 0.5f,
					TeamLead = "Alicia",
					PrimaryContact = "Khaled Haggag",
					Stage = "Prelaunch",
					LastPost = new DateTime (2015, 04, 27),
					NumberOfUpdates = 1
				},
				new Project {
					ProjectTitle = "Sarc",
					Progress = 1.0f,
					TeamLead = "Alicia",
					PrimaryContact = "Khaled Haggag",
					Stage = "Prelaunch",
					LastPost = new DateTime (2015, 04, 27),
					NumberOfUpdates = 1
				},
				new Project {
					ProjectTitle = "Caring Careers",
					Progress = 0.2f,
					TeamLead = "Alicia",
					PrimaryContact = "Khaled Haggag",
					Stage = "Prelaunch",
					LastPost = new DateTime (2015, 04, 27),
					NumberOfUpdates = 1
				},
				new Project {
					ProjectTitle = "Drop n Go",
					Progress = 0.8f,
					TeamLead = "Alicia",
					PrimaryContact = "Khaled Haggag",
					Stage = "Prelaunch",
					LastPost = new DateTime (2015, 04, 27),
					NumberOfUpdates = 1
				},
				new Project {
					ProjectTitle = "EmployLink",
					Progress = 0.1f,
					TeamLead = "Alicia",
					PrimaryContact = "Khaled Haggag",
					Stage = "Prelaunch",
					LastPost = new DateTime (2015, 04, 27),
					NumberOfUpdates = 1
				},
			}, this.NavigationController);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}


		#endregion
	}
}

