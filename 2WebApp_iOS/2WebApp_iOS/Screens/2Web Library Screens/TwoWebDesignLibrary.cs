
using System;

using Foundation;
using UIKit;
using CoreGraphics;

namespace WebApp_iOS
{
	public partial class TwoWebDesignLibrary : UIViewController
	{
		

		public TwoWebDesignLibrary () : base ("TwoWebDesignLibrary", null)
		{
			
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.

			GlobalAPI.Manager ().PageDefault (this, "2 Web Library", true, true);

			//Populate Email Blasts Page 
			tbvEmailBlast.Source = new EmailBlastTableSource(GlobalAPI.Manager().LoadEmailBlasts(), this); 
			tbvEmailBlast.BackgroundColor = UIColor.Clear; 
			tbvEmailBlast.RowHeight = 165; 
			tbvEmailBlast.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			//Populate Marketing Resources Page
			tbvMarketResource.Source = new MarketingResourcesTableSource(GlobalAPI.Manager().LoadMarketResources(), this); 
			tbvMarketResource.BackgroundColor = UIColor.Clear; 
			tbvMarketResource.RowHeight = 165; 
			tbvMarketResource.SeparatorStyle = UITableViewCellSeparatorStyle.None; 

			//Courasal Pages
			var pages = new UIView[]{ MarketingView, EmailBlastView }; 

			ScrollView.Frame = new CGRect (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 70); 
			PageControl.Frame = new CGRect (0, UIScreen.MainScreen.Bounds.Height - 55, UIScreen.MainScreen.Bounds.Width, 40);

			int i;

			for (i = 0; i < pages.Length; i++) {
				//UIView view = new UIView (); 
				CoreGraphics.CGRect frame = new CoreGraphics.CGRect (); 

				frame.X = (this.ScrollView.Frame.Width * i);
				frame.Y = this.ScrollView.Frame.Y; 

				frame.Height = this.ScrollView.Frame.Height;
				frame.Width = this.ScrollView.Frame.Width; 

				pages [i].Frame = frame;
				this.ScrollView.AddSubview (pages [i]);
			}

			tbvEmailBlast.Frame = new CGRect (0, 40, ScrollView.Frame.Width, ScrollView.Frame.Height - 80); 
			tbvMarketResource.Frame = new CGRect (0, 40, ScrollView.Frame.Width, ScrollView.Frame.Height - 80); 

			// set pages and content size
			PageControl.Pages = i;
			ScrollView.ContentSize = new CoreGraphics.CGSize (ScrollView.Frame.Width * i, ScrollView.Frame.Height - 70);

			ScrollView.Scrolled += ScrollEvent; 




		}

		//For the courasel pages
		private void ScrollEvent (object sender, EventArgs e)
		{
			PageControl.CurrentPage = 
				(int)System.Math.Floor (ScrollView.ContentOffset.X
					/ this.ScrollView.Frame.Size.Width);
		}
	}
}

