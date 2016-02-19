using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace WebApp_iOS
{
	partial class Login : UIViewController
	{
		public Login (IntPtr handle) : base (handle)
		{
			
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			LoginBtn.TouchUpInside += (object sender, EventArgs e) => {
				
				NavigationController.PushViewController(new TabController(),true); 
			};


		}
	}
}
