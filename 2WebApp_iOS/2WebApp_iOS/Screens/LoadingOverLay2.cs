﻿using System;
using System.Drawing;
using UIKit;

namespace WebApp_iOS
{
	public class LoadingOverlay2 : UIView {
		// control declarations
		UIActivityIndicatorView activitySpinner;
		UILabel loadingLabel;

		public LoadingOverlay2 (string Text) : base ()
		{

			Frame=new RectangleF (0, 0, (float)UIScreen.MainScreen.Bounds.Width, (float)UIScreen.MainScreen.Bounds.Height);

			// configurable bits
			BackgroundColor = UIColor.Black;
			Alpha = 1.0f;
			AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

			float labelHeight = 22;
			float labelWidth = (float)Frame.Width - 20;

			// derive the center x and y
			float centerX = (float)Frame.Width / 2;
			float centerY = (float)Frame.Height / 2;

			// create the activity spinner, center it horizontall and put it 5 points above center x
			activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			activitySpinner.Frame = new RectangleF (
				(float)(centerX - (activitySpinner.Frame.Width / 2)),
				(float)(centerY - activitySpinner.Frame.Height - 20),
				(float)activitySpinner.Frame.Width ,
				(float)activitySpinner.Frame.Height);
			activitySpinner.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			AddSubview (activitySpinner);
			activitySpinner.StartAnimating ();

			// create and configure the "Loading Data" label
			loadingLabel = new UILabel(new RectangleF (
				centerX - (labelWidth / 2),
				centerY + 20 ,
				labelWidth ,
				labelHeight
			));
			loadingLabel.BackgroundColor = UIColor.Clear;
			loadingLabel.TextColor = UIColor.White;
			//loadingLabel.Text = "Loading Data...";
			loadingLabel.Text=Text;
			loadingLabel.TextAlignment = UITextAlignment.Center;
			loadingLabel.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			AddSubview (loadingLabel);
		}

		/// <summary>
		/// Fades out the control and then removes it from the super view
		/// </summary>
		public void Hide ()
		{
			UIView.Animate (
				0.5, // duration
				() => { Alpha = 1; },
				() => { RemoveFromSuperview(); }
			);
		}
	}
}

