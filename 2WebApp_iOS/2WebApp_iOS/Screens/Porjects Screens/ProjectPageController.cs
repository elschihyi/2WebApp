using System;
using UIKit;
using CoreDataService;
using System.Collections.Generic;

namespace WebApp_iOS
{
	public class ProjectPageController:UIPageViewController
	{
		//object
		projectsummary theProject;

		public ProjectPageController (projectsummary theProject):base(UIPageViewControllerTransitionStyle.Scroll,UIPageViewControllerNavigationOrientation.Horizontal)
		{
			this.theProject = theProject;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			AutomaticallyAdjustsScrollViewInsets = true;
			NavigationItem.Title=theProject.name;
			View.Frame = View.Bounds;
			List<UIViewController> pages = new List<UIViewController> ();

			ProjectUpdateScreenController projectUpdateController = new ProjectUpdateScreenController (theProject);
			pages.Add (projectUpdateController);

			ProjectStatusScreenController projectStatusScreenController = new ProjectStatusScreenController (theProject);
			pages.Add (projectStatusScreenController);

			PorjectOverviewScreenController porjectOverviewScreenController = new PorjectOverviewScreenController (theProject);
			pages.Add (porjectOverviewScreenController);

			ProjectSupportScreenController projectSupportScreenController = new ProjectSupportScreenController (theProject);
			pages.Add (projectSupportScreenController);

			DataSource = new CustomScreenPageDataSource<UIViewController> (pages);
			SetViewControllers (new UIViewController[] { pages [0] }, UIPageViewControllerNavigationDirection.Forward, false, null);
		}
	}

	public class CustomScreenPageDataSource<T>: UIPageViewControllerDataSource where T:UIViewController
	{
		List<T> pages; 

		public CustomScreenPageDataSource(List<T> pages)
		{
			this.pages = pages;
		}

		override public UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
		{
			var currentPage = referenceViewController as T;
			var index = pages.IndexOf (currentPage);
			if (index > 0) {
				return pages [index - 1];
			} else {
				return null;
			}	
		}

		override public UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
		{
			var currentPage = referenceViewController as T;
			var index = pages.IndexOf (currentPage);
			if (index < (pages.Count-1)) {
				return pages [index + 1];
			} else {
				return null;
			}
		}
	}
}

