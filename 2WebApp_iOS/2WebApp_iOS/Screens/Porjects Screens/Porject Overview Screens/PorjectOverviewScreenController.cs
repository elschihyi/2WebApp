﻿using System;
using UIKit;
using CoreDataService;
using System.Drawing;

namespace WebApp_iOS
{
	public class PorjectOverviewScreenController: UIViewController
	{
		ProjectOverviewScreenView projectOverviewScreenView;

		//object
		projectsummary theProject;

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
			//AutomaticallyAdjustsScrollViewInsets = true;
			initTableView ();
			GlobalAPI.Manager ().PageDefault (this, "Projects", true, true);
		}

		/********************************************************************************
		*Views initializations
		********************************************************************************/
		public void initTableView(){
			projectOverviewScreenView = new ProjectOverviewScreenView ();
			var statusbar=UIApplication.SharedApplication.StatusBarFrame.Size.Height;
			var navigationbarHeight = NavigationController.NavigationBar.Frame.Size.Height;
			var y = statusbar + navigationbarHeight;
			projectOverviewScreenView.Frame = new RectangleF(0f,(float)y,(float)UIScreen.MainScreen.Bounds.Width,(float)(UIScreen.MainScreen.Bounds.Height-y));
			projectOverviewScreenView.titleLabel.Text="Project Overview";
			projectOverviewScreenView.UnderDevelop.Text="Under Development";
			View.Add (projectOverviewScreenView);
		}
	}
}
