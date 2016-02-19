// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace WebApp_iOS
{
	[Register ("TabBarController")]
	partial class TabBarController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSetting { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnSetting != null) {
				btnSetting.Dispose ();
				btnSetting = null;
			}
		}
	}
}
