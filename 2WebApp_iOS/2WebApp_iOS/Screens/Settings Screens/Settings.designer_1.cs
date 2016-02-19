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
	[Register ("Settings2")]
	partial class Settings
	{
		[Outlet]
		UIKit.UIView Page1 { get; set; }

		[Outlet]
		UIKit.UIView Page2 { get; set; }

		[Outlet]
		UIKit.UIView Page3 { get; set; }

		[Outlet]
		UIKit.UIView Page4 { get; set; }

		[Outlet]
		UIKit.UIPageControl PageControl { get; set; }

		[Outlet]
		UIKit.UIScrollView ScrollView { get; set; }

		[Outlet]
		UIKit.UIButton btnLogout { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Page4 != null) {
				Page4.Dispose ();
				Page4 = null;
			}

			if (Page3 != null) {
				Page3.Dispose ();
				Page3 = null;
			}

			if (Page2 != null) {
				Page2.Dispose ();
				Page2 = null;
			}

			if (Page1 != null) {
				Page1.Dispose ();
				Page1 = null;
			}

			if (PageControl != null) {
				PageControl.Dispose ();
				PageControl = null;
			}

			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}

			if (btnLogout != null) {
				btnLogout.Dispose ();
				btnLogout = null;
			}
		}
	}
}
