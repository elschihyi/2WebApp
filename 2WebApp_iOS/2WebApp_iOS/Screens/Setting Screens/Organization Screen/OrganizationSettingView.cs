using System;
using UIKit;
using System.Drawing;

namespace WebApp_iOS
{
	public class OrganizationSettingView:UIView
	{
		public UILabel TitleLabel { get; set; }
		public UITableView TableView{ get; set; }

		public OrganizationSettingView (RectangleF Frame)
		{
			this.Frame = Frame;
			TitleLabel= new UILabel () 
			{
				BackgroundColor=UIColor.FromRGB(0,172,237),
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
			};
			Add(TitleLabel);

			TableView= new UITableView () 
			{
				BackgroundColor=UIColor.Clear,
			};
			Add (TableView);

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
			//***************************************************************************************************************************************************
			TitleLabel.Frame = new RectangleF (5f,8, (float)UIScreen.MainScreen.Bounds.Width-10f, 30.0f);
			TitleLabel.Font = UIFont.SystemFontOfSize (18.0f);
			TableView.Frame = new RectangleF (0f,38f, (float)UIScreen.MainScreen.Bounds.Width, (float)Frame.Height-38.0f);
		}
	}
}

