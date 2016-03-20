using System;
using UIKit;
using Foundation;
using System.Drawing;
using CoreGraphics;

namespace WebApp_iOS
{
	public class ProfileSettingCell: UITableViewCell
	{
		public static readonly NSString Key = new NSString ("ProfileSettingCell");
		public int Row;
		public int Section;
		public UIImageView IconImageView;
		public UITextField TextField;
		public UIImageView EditImageView;

		public ProfileSettingCell ()
		{
			BackgroundColor = UIColor.Clear;
			Frame = new RectangleF (0, 0, (float)UIScreen.MainScreen.Bounds.Width, 66.0f); 
			SelectionStyle = UITableViewCellSelectionStyle.None;

			IconImageView= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Frame = new RectangleF (0.05f*(float)Frame.Width, 0.35f*(float)Frame.Height, 0.3f*(float)Frame.Height, 0.3f*(float)Frame.Height),
			};	
			Add (IconImageView);

			TextField=new UITextField( ) 
			{
				KeyboardType=UIKeyboardType.Default,
				ReturnKeyType=UIReturnKeyType.Done,
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear,
				AttributedPlaceholder=new NSAttributedString("",null,UIColor.White),
				BorderStyle = UITextBorderStyle.None,
				Enabled = true,
				SecureTextEntry=false,
				Frame = new RectangleF (0.3f*(float)Frame.Height+0.10f*(float)Frame.Width,0f, (float)Frame.Width-0.6f*(float)Frame.Height-0.2f*(float)Frame.Width, (float)Frame.Height),
			};
			TextField.ShouldReturn += (textField) => 
			{
				textField.ResignFirstResponder();
				return true;
			};
			Add(TextField);

			EditImageView= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Frame = new RectangleF ((float)Frame.Width-0.3f*(float)Frame.Height-0.05f*(float)Frame.Width, 0.35f*(float)Frame.Height, 0.3f*(float)Frame.Height, 0.3f*(float)Frame.Height),
				Image=UIImage.FromFile("Cut_Images/Pencil_Icon.png"),
			};	
			Add (EditImageView);
		}
	}
}

