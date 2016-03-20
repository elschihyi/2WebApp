using System;
using UIKit;
using System.Drawing;
using CoreGraphics;

namespace WebApp_iOS
{
	public class LoginRegisterView:UIView
	{
		public UIButton LoginBtn{ get; set;}
		public UIButton RegisterBtn{ get; set;}
		public UIImageView LogoImageView { get; set;}
		public UILabel CompanyLabel { get; set;}
		public LoginView LoginView{ get; set;}
		public RegisterView RegisterView { get; set;}


		public LoginRegisterView (CGRect Frame)
		{
			this.Frame = Frame;
			BackgroundColor = UIColor.Clear;

			LoginBtn = UIButton.FromType (UIButtonType.RoundedRect);
			LoginBtn.BackgroundColor = UIColor.FromRGB(0,172,237);
			LoginBtn.SetTitleColor(UIColor.White,UIControlState.Normal);
			LoginBtn.SetTitle ("Login",UIControlState.Normal);
			Add (LoginBtn);

			RegisterBtn = UIButton.FromType (UIButtonType.RoundedRect);
			RegisterBtn.BackgroundColor = UIColor.Black;
			RegisterBtn.SetTitleColor(UIColor.White,UIControlState.Normal);
			RegisterBtn.SetTitle ("Register",UIControlState.Normal);
			Add (RegisterBtn);

			LogoImageView= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image=UIImage.FromFile("2web.png"),
			};	
			Add (LogoImageView);

			CompanyLabel= new UILabel () {
				Font = UIFont.BoldSystemFontOfSize (32f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center,
				Text="webdesign",
			};
			Add (CompanyLabel);

			LoginView = new LoginView ();
			Add (LoginView);

			RegisterView = new RegisterView ();
			RegisterView.Hidden = true;
			Add (RegisterView);

			position ();
		}

		public void position(){
			LoginBtn.Frame = new RectangleF (0.10f * (float)Frame.Width, 15.0f, 0.40f * (float)Frame.Width,  0.06f*(float)Frame.Height);
			RegisterBtn.Frame = new RectangleF (0.5f * (float)Frame.Width, 15.0f, 0.40f * (float)Frame.Width,  0.06f*(float)Frame.Height);
			LogoImageView.Frame = new RectangleF (0.5f * (float)Frame.Width-20f, 0.15f*(float)Frame.Height, 40f, 40f );
			CompanyLabel.Frame = new RectangleF (0f, 40.0f+0.15f*(float)Frame.Height, 1.0f*(float)Frame.Width, 0.15f*(float)Frame.Height);
			LoginView.Frame=new RectangleF (0.10f * (float)Frame.Width, 40.0f+0.3f*(float)Frame.Height, 0.8f * (float)Frame.Width, 0.7f*(float)Frame.Height-50f);
			RegisterView.Frame=new RectangleF (0.10f * (float)Frame.Width, 40.0f+0.3f*(float)Frame.Height, 0.8f * (float)Frame.Width, 0.7f*(float)Frame.Height-50f);
			LoginView.position ();
			RegisterView.position ();
		}	
	}
}

