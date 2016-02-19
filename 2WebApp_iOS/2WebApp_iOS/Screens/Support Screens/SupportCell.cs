using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace WebApp_iOS
{
	public class SupportCell : UITableViewCell
	{
		UILabel headingLabel;
		UIImageView arrow; 

		public SupportCell (String cellId) : base (UITableViewCellStyle.Default, cellId)
		{

			BackgroundColor = UIColor.Clear; 


			headingLabel = new UILabel () {
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear
			};

			arrow = new UIImageView(new UIImage("Cut_Images/Arrow_Icon_Blue.png")); 

			ContentView.AddSubviews(new UIView[] {headingLabel, arrow});

		}

		public void UpdateCell (string title)
		{
			headingLabel.Text = title;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			headingLabel.Frame = new CGRect (15, 7, ContentView.Bounds.Width - 100, 25);
			arrow.Frame = new CGRect (ContentView.Bounds.Width - 30, 10, 12, 17); 
		}
	}
}


