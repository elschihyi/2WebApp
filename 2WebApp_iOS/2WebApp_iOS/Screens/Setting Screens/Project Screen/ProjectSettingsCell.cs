using System;
using Foundation;
using UIKit;
using System.Drawing;

namespace WebApp_iOS
{
	public class ProjectSettingsCell: UITableViewCell
	{
		public static readonly NSString Key = new NSString ("ProjectSettingsCell");

		public int Row;
		public UILabel nameLabel;
		public UIButton RequestBtn;

		public ProjectSettingsCell (): base ()
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

			RequestBtn = UIButton.FromType (UIButtonType.RoundedRect);
			RequestBtn.BackgroundColor = UIColor.FromRGB(35,40,46);
			RequestBtn.SetTitleColor(UIColor.FromRGB (0,172,237),UIControlState.Normal);
			RequestBtn.SetTitle ("REQUEST STATUS",UIControlState.Normal);
			RequestBtn.Frame = new RectangleF (0.55f * (float)Frame.Width, 0.25f*(float)Frame.Height, 0.43f * (float)Frame.Width,  0.5f*(float)Frame.Height);
			Add (RequestBtn);
		}
	}
}

