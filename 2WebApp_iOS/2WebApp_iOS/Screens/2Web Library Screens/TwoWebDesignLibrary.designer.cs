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
	[Register ("TwoWebDesignLibrary")]
	partial class TwoWebDesignLibrary
	{
		[Outlet]
		UIKit.UIView EmailBlastView { get; set; }

		[Outlet]
		UIKit.UIView MarketingView { get; set; }

		[Outlet]
		UIKit.UIPageControl PageControl { get; set; }

		[Outlet]
		UIKit.UIScrollView ScrollView { get; set; }

		[Outlet]
		UIKit.UISegmentedControl smcMarketingBlast { get; set; }

		[Outlet]
		UIKit.UIView smcView { get; set; }

		[Outlet]
		UIKit.UITableView tbvEmailBlast { get; set; }

		[Outlet]
		UIKit.UITableView tbvMarketResource { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tbvMarketResource != null) {
				tbvMarketResource.Dispose ();
				tbvMarketResource = null;
			}

			if (tbvEmailBlast != null) {
				tbvEmailBlast.Dispose ();
				tbvEmailBlast = null;
			}

			if (EmailBlastView != null) {
				EmailBlastView.Dispose ();
				EmailBlastView = null;
			}

			if (MarketingView != null) {
				MarketingView.Dispose ();
				MarketingView = null;
			}

			if (PageControl != null) {
				PageControl.Dispose ();
				PageControl = null;
			}

			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}

			if (smcMarketingBlast != null) {
				smcMarketingBlast.Dispose ();
				smcMarketingBlast = null;
			}

			if (smcView != null) {
				smcView.Dispose ();
				smcView = null;
			}
		}
	}
}
