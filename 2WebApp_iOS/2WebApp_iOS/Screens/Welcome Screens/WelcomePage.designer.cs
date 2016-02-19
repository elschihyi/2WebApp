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
	[Register ("WelcomePage")]
	partial class WelcomePage
	{
		[Outlet]
		UIKit.UICollectionView CollectionView { get; set; }

		[Outlet]
		UIKit.UIButton square1 { get; set; }

		[Outlet]
		UIKit.UIButton square2 { get; set; }

		[Outlet]
		UIKit.UIButton square3 { get; set; }

		[Outlet]
		UIKit.UIButton square4 { get; set; }

		[Outlet]
		UIKit.UIImageView welcomeLogo { get; set; }

		[Outlet]
		UIKit.UIScrollView WelcomeScrl { get; set; }

		[Outlet]
		UIKit.UIView WelcomeVw { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (WelcomeVw != null) {
				WelcomeVw.Dispose ();
				WelcomeVw = null;
			}

			if (WelcomeScrl != null) {
				WelcomeScrl.Dispose ();
				WelcomeScrl = null;
			}

			if (CollectionView != null) {
				CollectionView.Dispose ();
				CollectionView = null;
			}

			if (square1 != null) {
				square1.Dispose ();
				square1 = null;
			}

			if (square2 != null) {
				square2.Dispose ();
				square2 = null;
			}

			if (square3 != null) {
				square3.Dispose ();
				square3 = null;
			}

			if (square4 != null) {
				square4.Dispose ();
				square4 = null;
			}

			if (welcomeLogo != null) {
				welcomeLogo.Dispose ();
				welcomeLogo = null;
			}
		}
	}
}
