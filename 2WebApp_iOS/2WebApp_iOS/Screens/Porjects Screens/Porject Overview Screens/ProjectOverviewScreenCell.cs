using System;
using UIKit;
using Foundation;
using System.Drawing;

namespace WebApp_iOS
{
	public class ProjectOverviewScreenCell: UITableViewCell
	{
		public static readonly NSString Key = new NSString ("ProjectOverviewScreenCell");
		public int Row;
		public UILabel TitleLabel;
		public UIView SeperateView;
		public UILabel ValueLabel;

		public ProjectOverviewScreenCell ()
		{
			BackgroundColor = UIColor.Clear;
			SelectionStyle = UITableViewCellSelectionStyle.None;
			this.Frame = new RectangleF (0, 0, 1.0f*(float)UIScreen.MainScreen.Bounds.Width, 88f); 

			TitleLabel = new UILabel () {
				Font = UIFont.BoldSystemFontOfSize ( 16f),
				TextColor = UIColor.FromRGB(0,172,237),
				TextAlignment = UITextAlignment.Center,
				Text = "",
				Frame = new RectangleF (0f * (float)Frame.Width, 0, 1.0f * (float)Frame.Width, 0.5f * (float)Frame.Height),
			};
			Add (TitleLabel);

			SeperateView = new UIView () {
				BackgroundColor = UIColor.FromRGB (0,172,237),
				Frame = new RectangleF (0f * (float)Frame.Width, 0.49f * (float)Frame.Height, 1.0f * (float)Frame.Width, 0.02f * (float)Frame.Height),
			};
			Add (SeperateView);

			ValueLabel = new UILabel () {
				Font = UIFont.BoldSystemFontOfSize ( 16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center,
				Text = "",
				Frame = new RectangleF (0f * (float)Frame.Width, 0.5f * (float)Frame.Height, 1.0f * (float)Frame.Width, 0.5f * (float)Frame.Height),
			};
			Add (ValueLabel);
		}
	}
}

