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
	[Register ("TabLanding")]
	partial class 2WebMainPage
	{
		[Outlet]
		UIKit.UITableView EventsTable { get; set; }

		[Outlet]
		UIKit.UITableView NewsletterTable { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NewsletterTable != null) {
				NewsletterTable.Dispose ();
				NewsletterTable = null;
			}

			if (EventsTable != null) {
				EventsTable.Dispose ();
				EventsTable = null;
			}
		}
	}
}
