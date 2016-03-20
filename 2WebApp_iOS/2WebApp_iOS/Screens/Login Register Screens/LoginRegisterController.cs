using System;
using UIKit;
using System.Drawing;

namespace WebApp_iOS
{
	public class LoginRegisterController: UIViewController
	{
		//views
		LoginRegisterView loginRegisterView;

		public LoginRegisterController ()
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
			NavigationItem.Title="";
			setNavigationItems ();
			initView ();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		/********************************************************************************
		*Views initializations
		********************************************************************************/
		public void initView(){
			var statusbar=UIApplication.SharedApplication.StatusBarFrame.Size.Height;
			var navigationbarHeight = NavigationController.NavigationBar.Frame.Size.Height;
			var y = statusbar + navigationbarHeight;
			loginRegisterView = new LoginRegisterView  (
				new RectangleF(0f,(float)y,(float)UIScreen.MainScreen.Bounds.Width,(float)(UIScreen.MainScreen.Bounds.Height-y)));
			loginRegisterView.LoginBtn.TouchUpInside += (s, e) => {
				loginScreenClick();
			};
			loginRegisterView.RegisterBtn.TouchUpInside += (s, e) => {
				registerScreenClick();
			};
			loginRegisterView.LoginView.LoginBtn.TouchUpInside += (s, e) => {
				loginClick();
			};
			loginRegisterView.RegisterView.RegisterBtn.TouchUpInside += (s, e) => {
				registerClick();
			};
			View.Add (loginRegisterView);
		}
		/********************************************************************************
		*Set navigation Items
		********************************************************************************/
		public void setNavigationItems(){
			NavigationItem.HidesBackButton=true;
			NavigationItem.LeftBarButtonItem = new UIBarButtonItem ("Cancel", UIBarButtonItemStyle.Plain, (s,a) => {
				NavigationController.PopToViewController(GlobalAPI.welcomePage,true);
			});
		}	
		/********************************************************************************
		*Btn clicks
		********************************************************************************/
		public void loginScreenClick(){
			loginRegisterView.LoginBtn.BackgroundColor=UIColor.FromRGB(0,172,237);
			loginRegisterView.RegisterBtn.BackgroundColor = UIColor.Black;
			loginRegisterView.LoginView.Hidden = false;
			loginRegisterView.RegisterView.Hidden = true;
		}

		public void registerScreenClick(){
			loginRegisterView.LoginBtn.BackgroundColor=UIColor.Black;
			loginRegisterView.RegisterBtn.BackgroundColor = UIColor.FromRGB(0,172,237);
			loginRegisterView.LoginView.Hidden = true;
			loginRegisterView.RegisterView.Hidden = false;
		}

		public void loginClick(){
			string email=loginRegisterView.LoginView.EmailTextField.Text;
			string password = loginRegisterView.LoginView.PasswordTextField.Text;
			bool rememberme = loginRegisterView.LoginView.RememberMeSwitch.On;
			Console.WriteLine ("Login email:" + email);
			Console.WriteLine ("Login password:" +password);
			Console.WriteLine ("Login rememberme:" + rememberme);
			NavigationController.PopViewController(true);
		}

		public void registerClick(){
			string email=loginRegisterView.RegisterView.EmailTextField.Text;
			string password = loginRegisterView.RegisterView.PasswordTextField.Text;
			string firstName=loginRegisterView.RegisterView.FirstNameTextField.Text;
			string lastname=loginRegisterView.RegisterView.LastNameTextField.Text;
			bool rememberme = loginRegisterView.RegisterView.RememberMeSwitch.On;
			Console.WriteLine ("Register email:" + email);
			Console.WriteLine ("Register password:" +password);
			Console.WriteLine ("Register firstname:" +password);
			Console.WriteLine ("Register password:" +password);
			Console.WriteLine ("Register rememberme:" + rememberme);


			GlobalAPI.Manager().PushPage(NavigationController,new RegisterSuccessScreenController ());
		}	
	}
}

