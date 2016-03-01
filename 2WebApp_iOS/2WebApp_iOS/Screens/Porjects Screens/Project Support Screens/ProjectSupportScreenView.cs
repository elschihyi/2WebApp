using System;
using UIKit;
using System.Drawing;

namespace WebApp_iOS
{
	public class ProjectSupportScreenView:UIScrollView
	{
		public UILabel titleLabel { get; set; }
		public UILabel UnderDevelop{ get; set; }

		public ProjectSupportScreenView ()
		{
			titleLabel= new UILabel () 
			{
				BackgroundColor=UIColor.FromRGB(100,200,255),
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
			};
			Add(titleLabel);

			UnderDevelop= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
			};
			Add(UnderDevelop);

			position ();
		}

		public void Hide ()
		{
			UIView.Animate (
				0.5, // duration
				() => { Alpha = 1; },
				() => { RemoveFromSuperview(); }
			);
		}

		public void position()
		{
			//Frame=new RectangleF (0,0, (float)UIScreen.MainScreen.Bounds.Width, (float)UIScreen.MainScreen.Bounds.Height);
			//***************************************************************************************************************************************************
			titleLabel.Frame = new RectangleF (0,8, (float)UIScreen.MainScreen.Bounds.Width, 30.0f);
			titleLabel.Font = UIFont.SystemFontOfSize (18.0f);
			UnderDevelop.Frame = new RectangleF (0,90, (float)UIScreen.MainScreen.Bounds.Width, 60.0f);
			UnderDevelop.Font = UIFont.SystemFontOfSize (24.0f);
		}
	}
}

