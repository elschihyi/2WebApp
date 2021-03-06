﻿using System;
using UIKit;
using CoreDataService;
using System.Drawing;

namespace WebApp_iOS
{
	public class ProjectStatusScreenController: UIViewController
	{
		ProjectStatusScreenView projectStatusScreenView;

		//object
		projectsummary theProject;

		public ProjectStatusScreenController (projectsummary theProject)
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
			//GlobalAPI.Manager ().PageDefault (this, "Projects", true, true);
		}

		/********************************************************************************
		*Views initializations
		********************************************************************************/
		public void initTableView(){
			var statusbar=UIApplication.SharedApplication.StatusBarFrame.Size.Height;
			var navigationbarHeight = NavigationController.NavigationBar.Frame.Size.Height;
			var y = statusbar + navigationbarHeight;

			projectStatusScreenView = new ProjectStatusScreenView (new RectangleF(0f,(float)y,
				(float)UIScreen.MainScreen.Bounds.Width,(float)(UIScreen.MainScreen.Bounds.Height-y-66.0f)),theProject);		
			projectStatusScreenView.titleLabel.Text="Project Status";
			View.Add (projectStatusScreenView);
		}
	}
}

