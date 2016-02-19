
using System;

using Foundation;
using UIKit;

namespace WebApp_iOS
{
	public partial class SupportDetail : UIViewController
	{
		String projectTitle;

		public SupportDetail (Support support) : base ("SupportDetail", null)
		{
			projectTitle = support.SupportTitle; 
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

			GlobalAPI.Manager ().PageDefault (this, "Support",false, true);

			lblTitle.Text = projectTitle;

			//populate Support Detail scroll page
			SupportScrl.Frame = new CoreGraphics.CGRect(0,0,View.Bounds.Width, View.Bounds.Height - 40);
			SupportScrl.ContentSize = SupportVw.Frame.Size; 
			SupportScrl.AddSubview(SupportVw); 

		}
	}
}

