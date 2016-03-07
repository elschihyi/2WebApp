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
			this.Frame = new RectangleF (0, 0, 1.0f*(float)UIScreen.MainScreen.Bounds.Width, (1.0f*(float)UIScreen.MainScreen.Bounds.Height-38f)/5.0f); 

			TitleLabel = new UILabel () {
				Font = UIFont.BoldSystemFontOfSize ( 16f),
				TextColor = UIColor.FromRGB(75,200,215),
				TextAlignment = UITextAlignment.Center,
				Text = "",
				Frame = new RectangleF (0f * (float)Frame.Width, 0, 1.0f * (float)Frame.Width, 0.5f * (float)Frame.Height),
			};
			Add (TitleLabel);

			SeperateView = new UIView () {
				BackgroundColor = UIColor.FromRGB (75, 200, 215),
				Frame = new RectangleF (0f * (float)Frame.Width, 0.48f * (float)Frame.Height, 1.0f * (float)Frame.Width, 0.04f * (float)Frame.Height),
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

