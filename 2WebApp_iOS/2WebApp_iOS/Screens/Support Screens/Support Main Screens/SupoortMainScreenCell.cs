using System;
using UIKit;
using Foundation;
using System.Drawing;

namespace WebApp_iOS
{
	public class SupportMainScreenCell:UITableViewCell
	{
		public static readonly NSString Key = new NSString ("SupportMainScreenCell");
		public int Row;
		public UILabel projectNameLabel{ get; set;}
		public UIButton ViewBtn{ get; set;}

		public SupportMainScreenCell ()
		{
			Frame = new RectangleF (0, 0, (float)UIScreen.MainScreen.Bounds.Width, 80.0f);
			BackgroundColor = UIColor.Clear;
			SelectionStyle = UITableViewCellSelectionStyle.None;

			projectNameLabel = new UILabel () {
				Font = UIFont.BoldSystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Left,
				Frame = new RectangleF (0.05f * (float)Frame.Width, 0, 0.7f * (float)Frame.Width, 1f * (float)Frame.Height),
			};
			Add (projectNameLabel);

			ViewBtn = UIButton.FromType (UIButtonType.RoundedRect);
			ViewBtn.BackgroundColor = UIColor.White;
			ViewBtn.SetTitle ("View",UIControlState.Normal);
			ViewBtn.Frame = new RectangleF (0.75f * (float)Frame.Width,  0.3f * (float)Frame.Height,
				0.225f * (float)Frame.Width,  0.4f * (float)Frame.Height);
			Add (ViewBtn);
		}
	}
}

