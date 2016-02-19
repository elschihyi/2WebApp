using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace WebApp_iOS
{
	public class CustomEventCell : UITableViewCell  {
		UILabel dateLabel, titleLabel;
		UIImageView block; 

		public CustomEventCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.None;
			ContentView.BackgroundColor = UIColor.Clear; 

			titleLabel = new UILabel () {
				TextColor = UIColor.White,
				Font = UIFont.SystemFontOfSize(9),
				TextAlignment = UITextAlignment.Center,
				BackgroundColor = UIColor.Clear,
				Lines = 2
			};

			dateLabel = new UILabel () {
				TextColor = UIColor.White,
				Font = UIFont.SystemFontOfSize(8),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear,
			};

			block = new UIImageView ();
			block.Image = new UIImage ("project_graphics/events_listing_border.png"); 

			ContentView.AddSubviews(new UIView[] {titleLabel, dateLabel, block});
		}
		public void UpdateCell (string date, string title)
		{
			titleLabel.Text = title;
			dateLabel.Text = date;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			titleLabel.Frame = new CGRect (ContentView.Bounds.Width / 4 + 5, 0, ContentView.Bounds.Width /4 * 3 - 15, 45);
			dateLabel.Frame = new CGRect (15, 0, ContentView.Bounds.Width / 4, 45); 
			block.Frame = new CGRect (2, 2, ContentView.Bounds.Width - 4, ContentView.Bounds.Height - 4); 

		}
	}
}

