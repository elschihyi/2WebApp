﻿using System;
using UIKit;
using Foundation;
using System.Drawing;

namespace WebApp_iOS
{
	public class RequestProjectCell: UITableViewCell
	{
		public static readonly NSString Key = new NSString ("RequestProjectCell");
		public int Row;
		public UIButton RequestBtn;
		public RequestProjectCell (): base ()
		{
			BackgroundColor = UIColor.Clear;
			Frame = new RectangleF (0, 0, (float)UIScreen.MainScreen.Bounds.Width, 66.0f); 
			SelectionStyle = UITableViewCellSelectionStyle.None;

			RequestBtn = UIButton.FromType (UIButtonType.RoundedRect);
			RequestBtn.BackgroundColor = UIColor.FromRGB(0,172,237);
			RequestBtn.SetTitleColor(UIColor.White,UIControlState.Normal);
			RequestBtn.SetTitle ("Request New Project",UIControlState.Normal);
			RequestBtn.Frame = new RectangleF (0.15f * (float)Frame.Width, 0.2f*(float)Frame.Height, 0.7f * (float)Frame.Width,  0.6f*(float)Frame.Height);
			Add (RequestBtn);
		}
	}
}

