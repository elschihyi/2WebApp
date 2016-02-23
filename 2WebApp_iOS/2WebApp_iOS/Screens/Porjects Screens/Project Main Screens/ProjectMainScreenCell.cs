using System;
using UIKit;
using Foundation;
using System.Drawing;

namespace WebApp_iOS
{
	public class ProjectMainScreenCell:UITableViewCell
	{
		public static readonly NSString Key = new NSString ("ProjectMainScreenCell");

		public UILabel projectNameLabel{ get; set;}
		public UIImageView projectStatusImageView{ get; set;}
		public ProjectMainScreenCell ()
		{
			Frame = new RectangleF (0, 0, (float)UIScreen.MainScreen.Bounds.Width, 80.0f);
			BackgroundColor = UIColor.Clear;

			projectNameLabel = new UILabel () {
				Font = UIFont.BoldSystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Left,
				Frame = new RectangleF (0.1f * (float)Frame.Width, 0, 0.9f * (float)Frame.Width, 0.25f * (float)Frame.Height),
			};
			Add (projectNameLabel);

			projectStatusImageView = new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Frame = new RectangleF (0.1f * (float)Frame.Width, 0.25f * (float)Frame.Height, 0.8f * (float)Frame.Width, 0.75f *(float)Frame.Height),
			};	
			Add (projectStatusImageView);


		}
	}
}

