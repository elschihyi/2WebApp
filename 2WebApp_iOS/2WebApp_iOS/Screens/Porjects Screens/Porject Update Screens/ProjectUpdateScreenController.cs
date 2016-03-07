using System;
using UIKit;
using CoreDataService;
using System.Drawing;

namespace WebApp_iOS
{
	public class ProjectUpdateScreenController: UIViewController
	{
		ProjectUpdateScreenView projectUpdateScreenView;

		//object
		public projectsummary theProject;

		public ProjectUpdateScreenController (projectsummary theProject)
		{
			this.theProject = theProject;
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
			//AutomaticallyAdjustsScrollViewInsets = true;
			initTableView ();
			GlobalAPI.Manager ().PageDefault (this, "Projects", true, true);
		}

		/********************************************************************************
		*Views initializations
//		********************************************************************************/
		public void initTableView(){
			projectUpdateScreenView = new ProjectUpdateScreenView ();
			var statusbar=UIApplication.SharedApplication.StatusBarFrame.Size.Height;
			var navigationbarHeight = NavigationController.NavigationBar.Frame.Size.Height;
			var y = statusbar + navigationbarHeight;
			projectUpdateScreenView.Frame = new RectangleF(0f,(float)y,(float)UIScreen.MainScreen.Bounds.Width,(float)(UIScreen.MainScreen.Bounds.Height-y));
			projectUpdateScreenView.titleLabel.Text="Recent Update";
			if (theProject.update != null && theProject.update.Count != 0) {
				projectUpdateScreenView.NoUpdate.Hidden = true;
				projectUpdateScreenView.UpdatesTableView.Hidden = false;
				projectUpdateScreenView.UpdatesTableView.Source = new ProjectUpdateScreenSource (this);
			} else {
				projectUpdateScreenView.NoUpdate.Hidden = false;
				projectUpdateScreenView.UpdatesTableView.Hidden = true;
			}	
			View.Add (projectUpdateScreenView);
		}

		/********************************************************************************
		*Btn clicks
		********************************************************************************/
		public void CellViewClick(int Row)
		{
			GlobalAPI.Manager().PushPage(NavigationController,
				new WebViewController(theProject.update[Row].file_url,theProject.update[Row].name));
		}
	}
}

