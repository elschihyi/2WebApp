using System;
using UIKit;
using System.Drawing;

namespace WebApp_iOS
{
	public class ProjectOverviewScreenView:UIScrollView
	{
		public UILabel titleLabel { get; set; }
		public UITableView OverViewTableView{ get; set; }
		public UIButton ScheduleMeetingBtn{get;set;}

		//public UILabel UnderDevelop{ get; set; }
		public ProjectOverviewScreenView ()
		{
			titleLabel= new UILabel () 
			{
				BackgroundColor=UIColor.FromRGB(100,200,255),
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
			};
			Add(titleLabel);

			OverViewTableView= new UITableView () 
			{
				BackgroundColor=UIColor.Clear,
			};
			Add (OverViewTableView);


			ScheduleMeetingBtn = UIButton.FromType (UIButtonType.RoundedRect);
			ScheduleMeetingBtn.BackgroundColor = UIColor.FromRGB(100,200,255);
			ScheduleMeetingBtn.SetTitle ("Schedule a Meeting",UIControlState.Normal);
			Add (ScheduleMeetingBtn);

			/*
			UnderDevelop= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
			};
			Add(UnderDevelop);
			*/
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
			OverViewTableView.Frame = new RectangleF (0,38f, (float)UIScreen.MainScreen.Bounds.Width, 4f*((float)UIScreen.MainScreen.Bounds.Height-38f)/5f);
			ScheduleMeetingBtn.Frame = new RectangleF (0.2f * (float)Frame.Width, (float)UIScreen.MainScreen.Bounds.Height-(1.0f*(float)UIScreen.MainScreen.Bounds.Height-38f)/5.0f, 
				0.6f * (float)Frame.Width,  (1.0f*(float)UIScreen.MainScreen.Bounds.Height-38f)/5.0f);
			//UnderDevelop.Frame = new RectangleF (0,90, (float)UIScreen.MainScreen.Bounds.Width, 60.0f);
			//UnderDevelop.Font = UIFont.SystemFontOfSize (24.0f);
		}
	}
}

