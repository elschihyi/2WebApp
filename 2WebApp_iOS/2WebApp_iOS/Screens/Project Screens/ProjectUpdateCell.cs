using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace WebApp_iOS
{
	public class ProjectUpdateCell : UITableViewCell
	{
		UILabel headingLabel;
		UIImageView arrow;
		UILabel dateLabel;

		public ProjectUpdateCell (String cellId) : base (UITableViewCellStyle.Default, cellId)
		{

			BackgroundColor = UIColor.Clear; 


			headingLabel = new UILabel () {
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear,
				TextAlignment = UITextAlignment.Left,
				Font = UIFont.SystemFontOfSize(15)
			};

			arrow = new UIImageView (new UIImage("Cut_Images/Arrow_Icon_Blue.png"));

			dateLabel = new UILabel () {
				TextColor = GlobalAPI.Manager().getTwoWebColor(),
				BackgroundColor = UIColor.Clear,
				TextAlignment = UITextAlignment.Left,
				Font = UIFont.SystemFontOfSize(13)
			}; 

			ContentView.AddSubviews(new UIView[] {headingLabel, arrow, dateLabel});

		}

		public void UpdateCell (string title, string date)
		{
			headingLabel.Text = title;
			dateLabel.Text = date; 
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			headingLabel.Frame = new CGRect (15, 7, (ContentView.Bounds.Width * 3) / 2, 25);
			dateLabel.Frame = new CGRect (15, 35, (ContentView.Bounds.Width * 3) / 2 , 25); 
			arrow.Frame = new CGRect (ContentView.Bounds.Width - 30, 19, 15, 20);
		}
	}
}



