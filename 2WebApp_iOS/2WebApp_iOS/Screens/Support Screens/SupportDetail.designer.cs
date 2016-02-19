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
	[Register ("SupportDetail")]
	partial class SupportDetail
	{
		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		[Outlet]
		UIKit.UIScrollView SupportScrl { get; set; }

		[Outlet]
		UIKit.UIView SupportVw { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SupportVw != null) {
				SupportVw.Dispose ();
				SupportVw = null;
			}

			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}

			if (SupportScrl != null) {
				SupportScrl.Dispose ();
				SupportScrl = null;
			}
		}
	}
}
