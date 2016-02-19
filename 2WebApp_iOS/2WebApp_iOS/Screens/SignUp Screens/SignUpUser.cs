
using System;

using Foundation;
using UIKit;

namespace WebApp_iOS
{
	public partial class SignUpUser : UIViewController
	{
		LoginFirstPage loginFirstPage;

		public SignUpUser () : base ("SignUpUser", null)
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
			imgFirstName.Image = new UIImage ("Cut_Images/Name_Icon.png");
			imgLastName.Image = new UIImage ("Cut_Images/Name_Icon.png"); 

			sgmLoginRegister.ValueChanged += (object sender, EventArgs e) => {
				if((sender as UISegmentedControl).SelectedSegment == 0){
					//login page
					if(loginFirstPage == null)
						loginFirstPage = new LoginFirstPage();
					NavigationController.PopViewController(false);
					GlobalAPI.Manager().PushPage((UINavigationController)UIApplication.SharedApplication.KeyWindow.RootViewController, loginFirstPage);
				}else{
					// register page

				}
			};

			FirstName.ShouldReturn += (textField) => {
				textField.ResignFirstResponder();
				return true;
			};
			LastName.ShouldReturn += (textField) => {
				textField.ResignFirstResponder();
				return true;
			};
			UserName.ShouldReturn += (textField) => {
				textField.ResignFirstResponder();
				return true;
			};
			PassWord.ShouldReturn += (textField) => {
				textField.ResignFirstResponder();
				return true;
			};

			BtnSignUp.TouchUpInside += (object sender, EventArgs e) => {
				if(GlobalAPI.Manager().SignUp(FirstName.Text,LastName.Text,UserName.Text,PassWord.Text)){
					new UIAlertView("Alert","Successfully signed up.",null, "OK",null).Show();
					if(loginFirstPage == null)
						loginFirstPage = new LoginFirstPage();
					GlobalAPI.Manager().PushPage(NavigationController, loginFirstPage);
					//NavigationController.PopViewController(true); 
				}else{
					new UIAlertView("Alert","Unable to sign up.",null, "OK",null).Show(); 
				}
			};
		}
	}
}

