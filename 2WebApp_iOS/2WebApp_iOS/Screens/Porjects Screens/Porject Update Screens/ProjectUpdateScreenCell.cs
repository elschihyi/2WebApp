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

		public ProjectUpdateScreenCell (BtnClickDelegate btn1Click): base ()
		{
			BackgroundColor = UIColor.Clear;

			nameLabel = new UILabel () {
				Font = UIFont.BoldSystemFontOfSize ( 16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Left,
				Text = "",
				Frame = new RectangleF (0f * (float)Frame.Width, 0, 0.7f * (float)Frame.Width, 0.5f * (float)Frame.Height),
			};
			Add (nameLabel);

			dateLabel = new UILabel () {
				Font = UIFont.BoldSystemFontOfSize (12f),
				TextColor = UIColor.FromRGB(75,200,215),
				TextAlignment = UITextAlignment.Left,
				Text = "",
				Frame = new RectangleF (0f * (float)Frame.Width,0.5f * (float)Frame.Height, 0.7f * (float)Frame.Width, 0.5f * (float)Frame.Height),
			};
			Add (dateLabel);

			viewBtn = UIButton.FromType (UIButtonType.RoundedRect);
			viewBtn.BackgroundColor = UIColor.White;
			viewBtn.SetTitle ("View",UIControlState.Normal);
			viewBtn.Frame = new RectangleF (0.7f * (float)Frame.Width, 0.25f * (float)Frame.Height, 0.2f * (float)Frame.Width,  0.5f * (float)Frame.Height);
			viewBtn.TouchUpInside += (s, e) => {
				btn1Click(Row);
			};	
			Add (viewBtn);
		}
	}
}

