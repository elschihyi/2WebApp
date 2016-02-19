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
	[Register ("ProjectDetail2")]
	partial class ProjectDetail2
	{
		[Outlet]
		UIKit.UILabel LastPost { get; set; }

		[Outlet]
		UIKit.UILabel PrimaryContact { get; set; }

		[Outlet]
		UIKit.UIProgressView Progress { get; set; }

		[Outlet]
		UIKit.UILabel Stage { get; set; }

		[Outlet]
		UIKit.UILabel Teamlead { get; set; }

		[Outlet]
		UIKit.UILabel Title { get; set; }

		[Outlet]
		UIKit.UILabel UpdateCount { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LastPost != null) {
				LastPost.Dispose ();
				LastPost = null;
			}

			if (PrimaryContact != null) {
				PrimaryContact.Dispose ();
				PrimaryContact = null;
			}

			if (Progress != null) {
				Progress.Dispose ();
				Progress = null;
			}

			if (Stage != null) {
				Stage.Dispose ();
				Stage = null;
			}

			if (Teamlead != null) {
				Teamlead.Dispose ();
				Teamlead = null;
			}

			if (Title != null) {
				Title.Dispose ();
				Title = null;
			}

			if (UpdateCount != null) {
				UpdateCount.Dispose ();
				UpdateCount = null;
			}
		}
	}
}
