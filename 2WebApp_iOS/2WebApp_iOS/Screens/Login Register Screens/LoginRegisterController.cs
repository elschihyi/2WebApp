using System;
using UIKit;
using System.Drawing;
using CoreDataService;
using System.Text.RegularExpressions;

namespace WebApp_iOS
{
	public class LoginRegisterController: UIViewController
	{
		//views
		LoginRegisterView loginRegisterView;
		LoadingOverlay2 loadingScreen;

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
		public void initLoadingScreen(string Text){
			loadingScreen = new LoadingOverlay2 (Text);
			Add (loadingScreen);
		}	

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
				NavigationController.PopToViewController(GlobalAPI.originPage,true);
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
			string rememberme = loginRegisterView.LoginView.RememberMeSwitch.On?"1":"0";
			if(string.Equals(email,"0")){
				email = "test@test.com";
				password = "test";
			}
			if(string.Equals(email,"1")){
				email = "alicia.hanwell@gmail.com";
				password = "2WebDesign";
			}
			string errmsg;
			if (!InputChecking(email=email,password=password,out errmsg=errmsg)) {
				UIAlertController Alert = UIAlertController.Create ("Format",
					errmsg, UIAlertControllerStyle.Alert);
				Alert.AddAction (UIAlertAction.Create ("OK",
					UIAlertActionStyle.Cancel, action => {
						NavigationController.PopToViewController (GlobalAPI.originPage, true);
					}		
				));
				PresentViewController (Alert, true, null);
				return;
			}	
			initLoadingScreen("Authenticating");
			LoginWebCall (email, password, rememberme);
		}

		public void registerClick(){
			string email=loginRegisterView.RegisterView.EmailTextField.Text;
			string password = loginRegisterView.RegisterView.PasswordTextField.Text;
			string firstName=loginRegisterView.RegisterView.FirstNameTextField.Text;
			string lastname=loginRegisterView.RegisterView.LastNameTextField.Text;
			string rememberme = loginRegisterView.RegisterView.RememberMeSwitch.On?"1":"0";

			string errmsg;
			if (!InputChecking(email=email,password=password,firstName=firstName,lastname=lastname,out errmsg=errmsg)) {
				UIAlertController Alert = UIAlertController.Create ("Format",
					errmsg, UIAlertControllerStyle.Alert);
				Alert.AddAction (UIAlertAction.Create ("OK",
					UIAlertActionStyle.Cancel, action => {
						NavigationController.PopToViewController (GlobalAPI.originPage, true);
					}		
				));
				PresentViewController (Alert, true, null);
				return;
			}	

			RegisterWebCall (email, password,firstName,lastname, rememberme);
		}

		/********************************************************************************
		*Web calls
		********************************************************************************/
		public void LoginWebCall(string email,string password,string rememberme){
			ActionParameters ap = new ActionParameters ();
			ap.IN.type = ActionType.LOGIN;
			ap.IN.data = new accountsummary();
			ap.IN.data.client_email = email;
			ap.IN.data.client_password = password;
			ap.IN.data.settings = new usersettings ();
			ap.IN.data.settings.remember_password=rememberme;
			ap.IN.func = LoginWebCallRespond;
			GlobalAPI.GetDataService ().Action (ref ap);
		}	

		public void RegisterWebCall(string email,string password,string firstName,string lastname,string rememberme){
			ActionParameters ap = new ActionParameters ();
			ap.IN.type = ActionType.CREATEACCOUNT;
			ap.IN.data = new accountsummary();
			ap.IN.data.client_email = email;
			ap.IN.data.client_password = password;
			ap.IN.data.client_firstname = firstName;
			ap.IN.data.client_lastname = lastname;
			ap.IN.data.settings = new usersettings ();
			ap.IN.data.settings.remember_password=rememberme;
			ap.IN.func = (o, e) => {};
			if (GlobalAPI.GetDataService ().Action (ref ap)) {
				GlobalAPI.Manager ().PushPage (NavigationController, new RegisterSuccessScreenController ());
			} else {
				string errmsg = ap.OUT.errmsg;
				UIAlertController Alert = UIAlertController.Create ("Error",
					errmsg, UIAlertControllerStyle.Alert);
				Alert.AddAction (UIAlertAction.Create ("OK",
					UIAlertActionStyle.Cancel, action => {
						NavigationController.PopToViewController (GlobalAPI.originPage, true);
					}		
				));
				PresentViewController (Alert, true, null);
			}		
		}

		/********************************************************************************
		*Web calls Response
		********************************************************************************/
		public void LoginWebCallRespond(Boolean succeed, string errmsg){
			if (succeed) {
				InvokeOnMainThread (() => {
					loadingScreen.Hide();
					NavigationController.PopViewController (true);
				});
			} else {
				InvokeOnMainThread (() => {
					loadingScreen.Hide ();
					UIAlertController Alert = UIAlertController.Create ("Error",
						                         errmsg, UIAlertControllerStyle.Alert);
					Alert.AddAction (UIAlertAction.Create ("OK",
						UIAlertActionStyle.Cancel, action => {
						NavigationController.PopToViewController (GlobalAPI.originPage, true);
					}		
					));
					PresentViewController (Alert, true, null);
				});
			}
		}

		/********************************************************************************
		*input checking
		********************************************************************************/
		public bool InputChecking(string email,string password,out string errmsg,string firstName=null,string lastName=null){
			Regex emailformat = new Regex (@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

			if(!emailformat.Match (email).Success){
				errmsg="Email format is incorrect";
				return false;
			}	

			if (!firstName != null && firstName.Length == 0) {
				errmsg="FIRST NAME can not be empty";
				return false;
			}	

			if (lastName != null && lastName.Length == 0) {
				errmsg="LAST NAME can not be empty";
				return false;
			}

			return true;
		}
	}
}

