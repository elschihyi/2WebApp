// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WebApp_iOS
{
	[Register ("LoginFirstPage")]
	partial class LoginFirstPage
	{
		[Outlet]
		UIKit.UIButton btnForgotPassword { get; set; }

		[Outlet]
		UIKit.UIButton btnLogin { get; set; }

		[Outlet]
		UIKit.UIButton btnSignUp { get; set; }

		[Outlet]
		UIKit.UITextField Email { get; set; }

		[Outlet]
		UIKit.UIImageView imgEmail { get; set; }

		[Outlet]
		UIKit.UIImageView imgLogo { get; set; }

		[Outlet]
		UIKit.UIImageView imgPwd { get; set; }

		[Outlet]
		UIKit.UITextField Password { get; set; }

		[Outlet]
		UIKit.UISwitch swtRememberMe { get; set; }

		[Outlet]
		UIKit.UISegmentedControl tglLoginRegister { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tglLoginRegister != null) {
				tglLoginRegister.Dispose ();
				tglLoginRegister = null;
			}

			if (imgLogo != null) {
				imgLogo.Dispose ();
				imgLogo = null;
			}

			if (swtRememberMe != null) {
				swtRememberMe.Dispose ();
				swtRememberMe = null;
			}

			if (imgEmail != null) {
				imgEmail.Dispose ();
				imgEmail = null;
			}

			if (imgPwd != null) {
				imgPwd.Dispose ();
				imgPwd = null;
			}

			if (btnForgotPassword != null) {
				btnForgotPassword.Dispose ();
				btnForgotPassword = null;
			}

			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
			}

			if (btnSignUp != null) {
				btnSignUp.Dispose ();
				btnSignUp = null;
			}

			if (Email != null) {
				Email.Dispose ();
				Email = null;
			}

			if (Password != null) {
				Password.Dispose ();
				Password = null;
			}
		}
	}
}
