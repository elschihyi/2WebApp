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
			//AutomaticallyAdjustsScrollViewInsets = false;
			initTableView ();
			//GlobalAPI.Manager ().PageDefault (this, "Projects", true, true);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			//AutomaticallyAdjustsScrollViewInsets = false;
		}

		/********************************************************************************
		*Views initializations
		********************************************************************************/
		public void initTableView(){
			var statusbar=UIApplication.SharedApplication.StatusBarFrame.Size.Height;
			var navigationbarHeight = NavigationController.NavigationBar.Frame.Size.Height;
			var y = statusbar + navigationbarHeight;

			projectUpdateScreenView = new ProjectUpdateScreenView (
				new RectangleF(0f,(float)y,(float)UIScreen.MainScreen.Bounds.Width,(float)(UIScreen.MainScreen.Bounds.Height-y-66.0f)));
			projectUpdateScreenView.titleLabel.Text="Recent Update";
			if (theProject.tasks != null && theProject.tasks.Count != 0) {
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
				new WebViewController(theProject.tasks[Row].file_url,theProject.tasks[Row].name));
		}
	}
}

