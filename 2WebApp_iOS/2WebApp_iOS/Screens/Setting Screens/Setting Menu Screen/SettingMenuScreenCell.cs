using System;
using UIKit;
using Foundation;
using System.Drawing;

namespace WebApp_iOS
{
	public class SettingMenuScreenCell:UITableViewCell
	{
		public static readonly NSString Key = new NSString ("SettingMenuScreenCell");

		public UILabel OperationsLabel{ get; set;}
		public UIImageView arrowImageView{get;set;}

		public SettingMenuScreenCell ()
		{
			Frame = new RectangleF (0f, 0f, (float)UIScreen.MainScreen.Bounds.Width, 44f);
			BackgroundColor = UIColor.Clear;
			SelectionStyle = UITableViewCellSelectionStyle.None;

			OperationsLabel = new UILabel () {
				Font = UIFont.BoldSystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Left,
				Frame = new RectangleF (0.05f * (float)Frame.Width, 0, 0.8f * (float)Frame.Width,(float)Frame.Height),
			};
			Add (OperationsLabel);

			arrowImageView = new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image=new UIImage ("Cut_Images/Arrow_Icon_Blue.png"),
				Frame = new RectangleF (0.85f * (float)Frame.Width, 0.25f *(float)Frame.Height, 0.15f * (float)Frame.Width,  0.5f *(float)Frame.Height),
			};	
			Add (arrowImageView);
		}
	}
}

