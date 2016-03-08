using System;
using UIKit;
using Foundation;
using System.Drawing;

namespace WebApp_iOS
{
	public delegate void BtnClickDelegate (int row);

	public class ProjectUpdateScreenCell: UITableViewCell
	{
		public static readonly NSString Key = new NSString ("ProjectUpdateScreenCell");
		public int Row;
		public UILabel nameLabel;
		public UILabel dateLabel;
		public UIButton viewBtn;

		public ProjectUpdateScreenCell (): base ()
		{
			BackgroundColor = UIColor.Clear;
			Frame = new RectangleF (0, 0, 1.0f*(float)UIScreen.MainScreen.Bounds.Width, 88.0f); 
			SelectionStyle = UITableViewCellSelectionStyle.None;

			nameLabel = new UILabel () {
				Font = UIFont.BoldSystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Left,
				Text = "",
				Frame = new RectangleF (0.05f* (float)Frame.Width, 0.1f*(float)Frame.Height, 0.95f * (float)Frame.Width, 0.4f * (float)Frame.Height),
			};
			Add (nameLabel);

			dateLabel = new UILabel () {
				Font = UIFont.BoldSystemFontOfSize (16f),
				TextColor = UIColor.FromRGB(0,172,237),
				TextAlignment = UITextAlignment.Left,
				Text = "",
				Frame = new RectangleF (0.05f* (float)Frame.Width ,0.5f * (float)Frame.Height, 0.70f * (float)Frame.Width, 0.4f * (float)Frame.Height),
			};
			Add (dateLabel);

			viewBtn = UIButton.FromType (UIButtonType.RoundedRect);
			viewBtn.BackgroundColor = UIColor.White;
			viewBtn.SetTitle ("View",UIControlState.Normal);
			viewBtn.Frame = new RectangleF (0.75f * (float)Frame.Width, 0.5f * (float)Frame.Height, 0.225f * (float)Frame.Width,  0.3f * (float)Frame.Height);
			Add (viewBtn);
		}
	}
}

