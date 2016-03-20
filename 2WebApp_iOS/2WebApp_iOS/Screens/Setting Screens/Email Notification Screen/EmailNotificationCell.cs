using System;
using UIKit;
using Foundation;
using System.Drawing;

namespace WebApp_iOS
{
	public class EmailNotificationCell: UITableViewCell
	{
		public static readonly NSString Key = new NSString ("EmailNotificationCell");
		public int Row;
		public int Section;
		public UILabel nameLabel;
		public UISwitch switchBtn;

		public EmailNotificationCell ()
		{
			BackgroundColor = UIColor.Clear;
			Frame = new RectangleF (0, 0, (float)UIScreen.MainScreen.Bounds.Width, 66.0f); 
			SelectionStyle = UITableViewCellSelectionStyle.None;

			nameLabel = new UILabel () {
				Font = UIFont.BoldSystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Left,
				Frame = new RectangleF (0.02f* (float)Frame.Width ,0f, 0.53f * (float)Frame.Width,(float)Frame.Height),
			};
			Add (nameLabel);

			switchBtn = new UISwitch();
			switchBtn.BackgroundColor = UIColor.Clear;
			switchBtn.TintColor= UIColor.FromRGB(0,172,237);
			switchBtn.OnTintColor=UIColor.FromRGB(0,172,237);
			switchBtn.Frame = new RectangleF (0.75f * (float)Frame.Width, 0.25f*(float)Frame.Height, 0.20f * (float)Frame.Width,  0.5f*(float)Frame.Height);
			Add (switchBtn);
		}
	}
}

