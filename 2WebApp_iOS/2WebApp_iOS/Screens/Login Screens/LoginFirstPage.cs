
using System;

using Foundation;
using UIKit;

namespace WebApp_iOS
{
	public partial class LoginFirstPage : UIViewController
	{
		WelcomePage welcomePage;
		SignUpUser signUpUser;

		public LoginFirstPage () : base ("LoginFirstPage", null)
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

			GlobalAPI.Manager ().PageDefault (this, "",false, false);

			//hide the back button
			//NavigationItem.SetHidesBackButton (true, false);

			imgPwd.Image = new UIImage("Cut_Images/Password_Icon.png");
			imgEmail.Image = new UIImage("Cut_Images/Email_Icon.png");
			imgLogo.Image = new UIImage("Cut_Images/2Web_Logo.png");

			tglLoginRegister.ValueChanged += (object sender, EventArgs e) => {
				if((sender as UISegmentedControl).SelectedSegment == 0){
					//login page

				}else{
					// register page
					if(signUpUser == null)
						signUpUser = new SignUpUser(); 
					NavigationController.PopViewController(false);
					GlobalAPI.Manager().PushPage((UINavigationController)UIApplication.SharedApplication.KeyWindow.RootViewController, signUpUser); 
				}
			};

			Email.ShouldReturn += (textField) => {
				textField.ResignFirstResponder();
				return true;
			};
			Password.ShouldReturn += (textField) => {
				textField.ResignFirstResponder();
				return true;
			};
			/*
			btnLogin.TouchUpInside += (object sender, EventArgs e) => {
				//log in with 2Web
				try {
					if (GlobalAPI.Manager ().Login (Email.Text, Password.Text,swtRememberMe.Enabled)) {
						//check if should remember 


						if(welcomePage == null)
							welcomePage = new WelcomePage();
						GlobalAPI.Manager ().PushPage (NavigationController, welcomePage); 
					} else {
						new UIAlertView ("Alert", "Please check your credentials, as well as your internet connection.", null, "OK", null).Show (); 
					}
				} catch (Exception ex) {
					new UIAlertView ("Alert", ex.Message, null, "OK", null).Show (); 
				}


				//clear usr & pwd fields
				Email.Text = ""; 
				Password.Text = ""; 
			};
			*/


			btnForgotPassword.TouchUpInside += (object sender, EventArgs e) => {
				if (GlobalAPI.Manager ().ForgotPassword ("demo")) {
					new UIAlertView ("Alert", "An email with instructions will be sent to you shortly.", null, "OK", null).Show ();
				} else {
					new UIAlertView ("Alert", "Please check your supplied information.", null, "OK", null).Show ();
				}
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);


			//if user already authenticated go to welcome page instead of login
			/*
			try {
				if (DbStorage.Manager ().checkAuthenticationToken ()) {
					if(welcomePage == null)
						welcomePage = new WelcomePage();
					GlobalAPI.Manager ().PushPage (NavigationController, welcomePage); 
				} 
			} catch (Exception ex) {
				new UIAlertView ("Alert", ex.Message, null, "OK", null).Show (); 
			}
			*/


		}
	}
}

