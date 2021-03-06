﻿using System;
using UIKit;
using System.Drawing;

namespace WebApp_iOS
{
	public class ProjectUpdateScreenView:UIScrollView
	{
		public UILabel titleLabel { get; set; }
		public UITableView UpdatesTableView{ get; set; }
		public UILabel NoUpdate{ get; set; }

		public ProjectUpdateScreenView (RectangleF Frame)
		{
			this.Frame = Frame;
			titleLabel= new UILabel () 
			{
				BackgroundColor=UIColor.FromRGB(0,172,237),
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
			};
			Add(titleLabel);

			UpdatesTableView= new UITableView () 
			{
				BackgroundColor=UIColor.Clear,
			};
			Add (UpdatesTableView);


			NoUpdate= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
				Text="No Update",
			};
			Add(NoUpdate);
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
			UpdatesTableView.Frame = new RectangleF (0f,38f, (float)UIScreen.MainScreen.Bounds.Width, (float)Frame.Height-38.0f);
			NoUpdate.Frame = new RectangleF (0,90, (float)UIScreen.MainScreen.Bounds.Width, 60.0f);
			NoUpdate.Font = UIFont.SystemFontOfSize (24.0f);
		}
	}
}

