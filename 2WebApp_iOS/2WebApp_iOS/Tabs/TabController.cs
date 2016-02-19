using System;
using UIKit;
using SatelliteMenu;
using System.Drawing;

namespace WebApp_iOS
{
	public class TabController : UITabBarController
	{
		UIViewController tab1, tab2, tab3, tab4;
		private Settings setting; 

		public TabController ()
		{
			tab1 = new 2WebMainPage();
			tab1.Title = "2Web";
			tab1.TabBarItem.Image = new UIImage ("2web.png"); 

			tab2 = new TabProjects (); 
			tab2.Title = "Projects"; 
			tab2.TabBarItem.Image = new UIImage ("hardhat.png"); 

//			tab3 = new UIViewController();
//			tab3.Title = "Support";
//			tab3.TabBarItem.Image = new UIImage ("hardhat.png"); 

			tab4 = new Settings (); 
			tab4.Title = "Settings";
			tab4.TabBarItem.Image = new UIImage ("gear.png"); 

			var tabs = new UIViewController[] {
				tab1, tab2, tab4
			};

			ViewControllers = tabs;

		}	

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
 
			//Hide the back button from the main landing page, so to not take them back to the login page. 
			NavigationItem.SetHidesBackButton (true, false); 







		}




	}
}

