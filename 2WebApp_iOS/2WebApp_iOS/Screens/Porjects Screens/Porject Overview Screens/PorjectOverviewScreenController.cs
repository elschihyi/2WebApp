using System;
using UIKit;
using CoreDataService;
using System.Drawing;

namespace WebApp_iOS
{
	public class PorjectOverviewScreenController: UIViewController
	{
		ProjectOverviewScreenView projectOverviewScreenView;

		//object
		public projectsummary theProject;

		public PorjectOverviewScreenController (projectsummary theProject)
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
			initTableView ();
		}

		/********************************************************************************
		*Views initializations
		********************************************************************************/
		public void initTableView(){
			
			var statusbar=UIApplication.SharedApplication.StatusBarFrame.Size.Height;
			var navigationbarHeight = NavigationController.NavigationBar.Frame.Size.Height;
			var y = statusbar + navigationbarHeight;
			projectOverviewScreenView = new ProjectOverviewScreenView (new RectangleF(0f,(float)y,(float)UIScreen.MainScreen.Bounds.Width,(float)(UIScreen.MainScreen.Bounds.Height-y)));
			projectOverviewScreenView.titleLabel.Text="Project Overview";
			projectOverviewScreenView.OverViewTableView.Source = new ProjectOverviewScreenSource (this);
			projectOverviewScreenView.ScheduleMeetingBtn.TouchUpInside += (s, e) => {
				ScheduleMeetingClick();
			};
			View.Add (projectOverviewScreenView);
		}

		/********************************************************************************
		*Btn clicks
		********************************************************************************/
		public void ScheduleMeetingClick()
		{
		}
	}
}

