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
		public ProjectOverviewScreenView (RectangleF Frame)
		{
			this.Frame = Frame;
			titleLabel= new UILabel () 
			{
				BackgroundColor=UIColor.FromRGB(0,172,237),
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
			};
			Add(titleLabel);

			OverViewTableView= new UITableView () 
			{
				BackgroundColor=UIColor.Clear,
				ScrollEnabled=false,
			};
			Add (OverViewTableView);


			ScheduleMeetingBtn = UIButton.FromType (UIButtonType.RoundedRect);
			ScheduleMeetingBtn.BackgroundColor = UIColor.FromRGB(0,172,237);
			ScheduleMeetingBtn.SetTitleColor (UIColor.White,UIControlState.Normal);
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
			titleLabel.Frame = new RectangleF (5f,8, (float)UIScreen.MainScreen.Bounds.Width-10f, 30.0f);
			titleLabel.Font = UIFont.SystemFontOfSize (18.0f);
			OverViewTableView.Frame = new RectangleF (0f,38f, (float)UIScreen.MainScreen.Bounds.Width, 352f);
			ScheduleMeetingBtn.Frame = new RectangleF (0.2f * (float)Frame.Width, 410f, 0.6f * (float)Frame.Width,  44f);
		}
	}
}

