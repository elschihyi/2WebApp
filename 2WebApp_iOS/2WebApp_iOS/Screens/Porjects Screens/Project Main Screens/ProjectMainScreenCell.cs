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
		public UIImageView arrowImageView{get;set;}

		public ProjectMainScreenCell ()
		{
			Frame = new RectangleF (0, 0, (float)UIScreen.MainScreen.Bounds.Width, 80.0f);
			BackgroundColor = UIColor.Clear;
			SelectionStyle = UITableViewCellSelectionStyle.None;

			projectNameLabel = new UILabel () {
				Font = UIFont.BoldSystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Left,
				Frame = new RectangleF (0.05f * (float)Frame.Width, 0, 0.8f * (float)Frame.Width, 0.25f * (float)Frame.Height),
			};
			Add (projectNameLabel);

			projectStatusImageView = new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Frame = new RectangleF (0f * (float)Frame.Width, 0.25f * (float)Frame.Height, 1.0f * (float)Frame.Width, 0.70f *(float)Frame.Height),
			};	
			Add (projectStatusImageView);

			arrowImageView = new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image=new UIImage ("Cut_Images/Arrow_Icon_Blue.png"),
				Frame = new RectangleF (0.85f * (float)Frame.Width, 0, 0.15f * (float)Frame.Width,  0.25f * (float)Frame.Height),
			};	
			Add (arrowImageView);

		}
	}
}

