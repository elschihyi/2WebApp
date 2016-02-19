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
	[Register ("SignUpUser")]
	partial class SignUpUser
	{
		[Outlet]
		UIKit.UIButton BtnSignUp { get; set; }

		[Outlet]
		UIKit.UITextField FirstName { get; set; }

		[Outlet]
		UIKit.UIImageView imgEmail { get; set; }

		[Outlet]
		UIKit.UIImageView imgFirstName { get; set; }

		[Outlet]
		UIKit.UIImageView imgLastName { get; set; }

		[Outlet]
		UIKit.UIImageView imgLogo { get; set; }

		[Outlet]
		UIKit.UIImageView imgPwd { get; set; }

		[Outlet]
		UIKit.UITextField LastName { get; set; }

		[Outlet]
		UIKit.UITextField PassWord { get; set; }

		[Outlet]
		UIKit.UISegmentedControl sgmLoginRegister { get; set; }

		[Outlet]
		UIKit.UITextField UserName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgLastName != null) {
				imgLastName.Dispose ();
				imgLastName = null;
			}

			if (imgFirstName != null) {
				imgFirstName.Dispose ();
				imgFirstName = null;
			}

			if (imgPwd != null) {
				imgPwd.Dispose ();
				imgPwd = null;
			}

			if (imgEmail != null) {
				imgEmail.Dispose ();
				imgEmail = null;
			}

			if (sgmLoginRegister != null) {
				sgmLoginRegister.Dispose ();
				sgmLoginRegister = null;
			}

			if (imgLogo != null) {
				imgLogo.Dispose ();
				imgLogo = null;
			}

			if (BtnSignUp != null) {
				BtnSignUp.Dispose ();
				BtnSignUp = null;
			}

			if (FirstName != null) {
				FirstName.Dispose ();
				FirstName = null;
			}

			if (LastName != null) {
				LastName.Dispose ();
				LastName = null;
			}

			if (PassWord != null) {
				PassWord.Dispose ();
				PassWord = null;
			}

			if (UserName != null) {
				UserName.Dispose ();
				UserName = null;
			}
		}
	}
}
