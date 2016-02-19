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
	[Register ("LunchPage")]
	partial class LunchPage
	{
		[Outlet]
		UIKit.UIImageView Images { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Images != null) {
				Images.Dispose ();
				Images = null;
			}
		}
	}
}
