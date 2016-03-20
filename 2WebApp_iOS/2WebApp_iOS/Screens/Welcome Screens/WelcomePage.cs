
using System;

using Foundation;
using UIKit;
using SatelliteMenu;
using CoreGraphics;
using System.Drawing;
using CoreDataService;

namespace WebApp_iOS
{
	public partial class WelcomePage : UIViewController
	{
		

		public WelcomePage () : base ("WelcomePage", null)
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

			if (!GlobalAPI.Manager ().internetConnection ())
				new UIAlertView ("Alert", "No internet connection detected. All data being viewed is cached data.", null, "Ok", null).Show (); 

			GlobalAPI.Manager ().PageDefault (this, "",true, true);

			//load the welcome scroll page
			WelcomeScrl.Frame = new CoreGraphics.CGRect(0,0,View.Bounds.Width, View.Bounds.Height - 40); 
			WelcomeScrl.ContentSize = WelcomeVw.Frame.Size;
			WelcomeScrl.AddSubview (WelcomeVw); 

			//hide the back button
			NavigationItem.SetHidesBackButton (true, false); 

			//put logo
			welcomeLogo.Image = new UIImage("Cut_Images/2Web_Logo.png"); 


			square1.SetImage (UIImage.FromFile ("Cut_Images/2web_Normal.png"), UIControlState.Normal);
			square1.SetImage (UIImage.FromFile ("Cut_Images/2Web_Hover.png"), UIControlState.Highlighted); 

			square2.SetImage (UIImage.FromFile ("Cut_Images/Library_Normal.png"), UIControlState.Normal);
			square2.SetImage (UIImage.FromFile ("Cut_Images/Library_Hover.png"), UIControlState.Highlighted); 

			square3.SetImage (UIImage.FromFile ("Cut_Images/project_Normal.png"), UIControlState.Normal);
			square3.SetImage (UIImage.FromFile ("Cut_Images/Project_Hover.png"), UIControlState.Highlighted); 

			square4.SetImage (UIImage.FromFile ("Cut_Images/Support_Normal.png"), UIControlState.Normal);
			square4.SetImage (UIImage.FromFile ("Cut_Images/Support_Hover.png"), UIControlState.Highlighted); 


			square1.TouchUpInside += (object sender, EventArgs e) => {
				GlobalAPI.Manager().PushPage(NavigationController,GlobalAPI.Manager().getTwoWebDesignMain());
			};
			square2.TouchUpInside += (object sender, EventArgs e) => {
				GlobalAPI.Manager().PushPage(NavigationController,GlobalAPI.Manager().getTwoWebDesignLibrary());
			};
			square3.TouchUpInside += (object sender, EventArgs e) => {
				//GlobalAPI.Manager().PushPage(NavigationController,GlobalAPI.Manager().getTabProjects());
				GlobalAPI.Manager().PushPage(NavigationController,new ProjectMainController());
			};
			square4.TouchUpInside += (object sender, EventArgs e) => {
				GlobalAPI.Manager().PushPage(NavigationController,new SupportMainScreenController());
				//GlobalAPI.Manager().PushPage(NavigationController,GlobalAPI.Manager().getMainSupport());
			};

		    //for Hidden screen
			int hiddenBtnClick=0;
			UIButton hiddenBtn=UIButton.FromType(UIButtonType.RoundedRect);
			hiddenBtn.BackgroundColor = UIColor.Clear;
			hiddenBtn.Frame = new RectangleF (0f, (float)UIScreen.MainScreen.Bounds.Width/2-15.0f, 30.0f, 30.0f);
			hiddenBtn.TouchUpInside += (s, e) => {
				if(hiddenBtnClick<3){
					hiddenBtnClick++;
				}else{
					hiddenBtnClick=0;
					GlobalAPI.Manager().PushPage(NavigationController,new CoreDataServiceTestUI ());
				}		
			};
			View.Add (hiddenBtn);

			GlobalAPI.welcomePage = this;
		}
	}
}

