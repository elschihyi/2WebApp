using System;
using UIKit;
using System.Drawing;
using CoreGraphics;

namespace WebApp_iOS
{
	public class ContactView:UIScrollView
	{
		//GUI component***********************************************
		public UILabel titleLabel { get; set; }
		public UILabel address1Label{ get; set; }
		public UILabel address2Label{ get; set; }
		public UILabel emailLabel { get; set; }
		public UIButton emailButton{ get; set; }
		public UILabel phoneLabel { get ; set; }
		public UIButton phoneButton{ get; set; }
		public UILabel visitLabel { get; set; }
		public UIButton visitButton { get; set; }

		//***************************************************************************************************************************************************
		public UIButton FacebookBtn { get; set; }
		public UIButton TwitterBtn { get; set; }
		public UIButton GoogleBtn { get; set; }
		public UIButton LinkInBtn { get; set; }
		public UIButton PinterestBtn { get; set; }

		public ContactView ()
		{
			ScrollEnabled = false;

			titleLabel= new UILabel () 
			{
				BackgroundColor=UIColor.FromRGB(100,200,255),
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
			};
			Add(titleLabel);

			address1Label= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
			};
			Add(address1Label);

			address2Label= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
			};
			Add(address2Label);

			emailLabel = new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
			};
			Add(emailLabel);

			emailButton = UIButton.FromType (UIButtonType.RoundedRect);
			emailButton.SetTitle ("E-MAIL", UIControlState.Normal);
			emailButton.BackgroundColor = UIColor.White;
			emailButton.SetTitleColor (UIColor.FromRGB (100, 200, 255), UIControlState.Normal);
			Add (emailButton);

			phoneLabel = new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
			};
			Add(phoneLabel);

			phoneButton = UIButton.FromType (UIButtonType.RoundedRect);
			phoneButton.SetTitle ("CALL US", UIControlState.Normal);
			phoneButton.BackgroundColor = UIColor.White;
			phoneButton.SetTitleColor (UIColor.FromRGB (100, 200, 255), UIControlState.Normal);
			Add (phoneButton);

			visitLabel = new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
			};
			Add(visitLabel);

			visitButton = UIButton.FromType (UIButtonType.RoundedRect);
			visitButton.SetTitle ("VISIT US", UIControlState.Normal);
			visitButton.BackgroundColor = UIColor.White;
			visitButton.SetTitleColor (UIColor.FromRGB (100, 200, 255), UIControlState.Normal);
			Add(visitButton);
			//***************************************************************************************************************************************************
			FacebookBtn = UIButton.FromType (UIButtonType.RoundedRect);
			FacebookBtn.SetBackgroundImage (new UIImage ("Cut_Images/Facebook_Icon.png"), UIControlState.Normal);
			FacebookBtn.BackgroundColor = UIColor.Clear;
			Add(FacebookBtn);

			TwitterBtn = UIButton.FromType (UIButtonType.RoundedRect);
			TwitterBtn.SetBackgroundImage (new UIImage ("Cut_Images/Twitter_Icon.png"), UIControlState.Normal);
			TwitterBtn.BackgroundColor = UIColor.Clear;
			Add(TwitterBtn);

			GoogleBtn = UIButton.FromType (UIButtonType.RoundedRect);
			GoogleBtn.SetBackgroundImage (new UIImage ("Cut_Images/Google+_Icon.png"), UIControlState.Normal);
			GoogleBtn.BackgroundColor = UIColor.Clear;
			Add(GoogleBtn);

			LinkInBtn = UIButton.FromType (UIButtonType.RoundedRect);
			LinkInBtn.SetBackgroundImage (new UIImage ("Cut_Images/Linkedin_Icon.png"), UIControlState.Normal);
			LinkInBtn.BackgroundColor = UIColor.Clear;
			Add(LinkInBtn);

			PinterestBtn = UIButton.FromType (UIButtonType.RoundedRect);
			PinterestBtn.SetBackgroundImage (new UIImage ("Cut_Images/Pinterest_Icon.png"), UIControlState.Normal);
			PinterestBtn.BackgroundColor = UIColor.Clear;
			Add(PinterestBtn);
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
			titleLabel.Frame = new RectangleF (0,8, (float)UIScreen.MainScreen.Bounds.Width, 20.0f);
			titleLabel.Font = UIFont.SystemFontOfSize (18.0f);
			address1Label.Frame = new RectangleF (0,60, (float)UIScreen.MainScreen.Bounds.Width, 30.0f);
			address1Label.Font = UIFont.SystemFontOfSize (18.0f);
			address2Label.Frame = new RectangleF (0,90, (float)UIScreen.MainScreen.Bounds.Width, 30.0f);
			address2Label.Font = UIFont.SystemFontOfSize (18.0f);
			emailLabel.Frame = new RectangleF (0,160, (float)UIScreen.MainScreen.Bounds.Width*2/3, 30.0f);
			emailLabel.Font = UIFont.SystemFontOfSize (18.0f);
			emailButton.Frame = new RectangleF ((float)(UIScreen.MainScreen.Bounds.Width*2/3),(float)162.5, (float)(UIScreen.MainScreen.Bounds.Width/3), 25.0f);
			phoneLabel.Frame = new RectangleF (0,200, (float)(UIScreen.MainScreen.Bounds.Width*2/3), 30.0f);
			phoneLabel.Font = UIFont.SystemFontOfSize (18.0f);
			phoneButton.Frame = new RectangleF ((float)(UIScreen.MainScreen.Bounds.Width*2/3),(float)202.5, (float)(UIScreen.MainScreen.Bounds.Width/3), 25.0f);
			visitLabel.Frame = new RectangleF (0,240, (float)(UIScreen.MainScreen.Bounds.Width*2/3), 30.0f);
			visitLabel.Font = UIFont.SystemFontOfSize (18.0f);
			visitButton.Frame = new RectangleF ((float)(UIScreen.MainScreen.Bounds.Width*2/3),(float)242.5, (float)(UIScreen.MainScreen.Bounds.Width/3), 25.0f);

			FacebookBtn.Frame = new RectangleF ((float)(UIScreen.MainScreen.Bounds.Width*1/10)-25.0f,310, 50.0f, 50.0f);
			TwitterBtn.Frame = new RectangleF ((float)(UIScreen.MainScreen.Bounds.Width*3/10)-25.0f,310, 50.0f, 50.0f);
			GoogleBtn.Frame = new RectangleF ((float)(UIScreen.MainScreen.Bounds.Width*5/10)-25.0f,310, 50.0f, 50.0f);
			LinkInBtn.Frame = new RectangleF ((float)(UIScreen.MainScreen.Bounds.Width*7/10)-25.0f,310, 50.0f, 50.0f);
			PinterestBtn.Frame = new RectangleF ((float)(UIScreen.MainScreen.Bounds.Width*9/10)-25.0f,310, 50.0f, 50.0f);
		}
	}
}

