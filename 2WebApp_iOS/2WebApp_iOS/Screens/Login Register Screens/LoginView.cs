using System;
using UIKit;
using Foundation;
using System.Drawing;
using CoreGraphics;

namespace WebApp_iOS
{
	public class LoginView:UIScrollView
	{
		public UIImageView EmailIconImageView{ get; set;}
		public UITextField EmailTextField{ get; set;}
		public UIView EmailSeperateLineView{ get; set;}

		public UIImageView PasswordIconImageView{ get; set;}
		public UITextField PasswordTextField{ get; set;}
		public UIView PasswordSeperateLineView{ get; set;}

		public UISwitch RememberMeSwitch{ get; set;}
		public UILabel RememberMeLabel { get; set;}

		public UIButton LoginBtn{ get; set;}
		public UIButton ForgetPasswordBtn{ get; set;}

		public LoginView ():base()
		{   
			BackgroundColor = UIColor.Clear;
			//BackgroundColor = UIColor.Yellow;

			EmailIconImageView= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image = UIImage.FromFile ("Cut_Images/Email_Icon.png"),
			};	
			Add (EmailIconImageView);

			EmailTextField=new UITextField( ) 
			{
				KeyboardType=UIKeyboardType.EmailAddress,
				ReturnKeyType=UIReturnKeyType.Done,
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear,
				AttributedPlaceholder=new NSAttributedString("EMAIL",null,UIColor.LightGray),
				BorderStyle = UITextBorderStyle.None,
				Enabled = true,
				SecureTextEntry=false,
			};
			EmailTextField.ShouldReturn += (textField) => 
			{
				textField.ResignFirstResponder();
				return true;
			};
			Add(EmailTextField);

			EmailSeperateLineView = new UIView () {
				BackgroundColor = UIColor.LightGray,
			};
			Add(EmailSeperateLineView);

			PasswordIconImageView= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image = UIImage.FromFile ("Cut_Images/Password_Icon.png"),
			};	
			Add (PasswordIconImageView);

			PasswordTextField=new UITextField( ) 
			{
				KeyboardType=UIKeyboardType.Default,
				ReturnKeyType=UIReturnKeyType.Done,
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear,
				AttributedPlaceholder=new NSAttributedString("PASSWORD",null,UIColor.LightGray),
				BorderStyle = UITextBorderStyle.None,
				Enabled = true,
				SecureTextEntry=true,
			};
			PasswordTextField.ShouldReturn += (textField) => 
			{
				textField.ResignFirstResponder();
				return true;
			};
			Add(PasswordTextField);

			PasswordSeperateLineView = new UIView () {
				BackgroundColor = UIColor.LightGray,
			};
			Add(PasswordSeperateLineView);

			RememberMeSwitch = new UISwitch();
			RememberMeSwitch.BackgroundColor = UIColor.Clear;
			RememberMeSwitch.TintColor= UIColor.FromRGB(0,172,237);
			RememberMeSwitch.OnTintColor=UIColor.FromRGB(0,172,237);
			//RememberMeSwitch.Frame = new RectangleF (0.75f * (float)Frame.Width, 0.25f*(float)Frame.Height, 0.20f * (float)Frame.Width,  0.5f*(float)Frame.Height);
			Add (RememberMeSwitch);

			RememberMeLabel= new UILabel () {
				Font = UIFont.BoldSystemFontOfSize (12f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Left,
				Text="Remember Me",
				//Frame = new RectangleF (0.02f* (float)Frame.Width ,0f, 0.53f * (float)Frame.Width,(float)Frame.Height),
			};
			Add (RememberMeLabel);

			LoginBtn = UIButton.FromType (UIButtonType.RoundedRect);
			LoginBtn.BackgroundColor = UIColor.FromRGB(0,172,237);
			LoginBtn.SetTitleColor(UIColor.White,UIControlState.Normal);
			LoginBtn.SetTitle ("Login",UIControlState.Normal);
			//RequestBtn.Frame = new RectangleF (0.15f * (float)Frame.Width, 0.2f*(float)Frame.Height, 0.7f * (float)Frame.Width,  0.6f*(float)Frame.Height);
			Add (LoginBtn);

			ForgetPasswordBtn = UIButton.FromType (UIButtonType.RoundedRect);
			ForgetPasswordBtn.BackgroundColor = UIColor.Clear;
			ForgetPasswordBtn.SetTitleColor(UIColor.White,UIControlState.Normal);
			ForgetPasswordBtn.SetTitle ("Forget Password?",UIControlState.Normal);
			//RequestBtn.Frame = new RectangleF (0.15f * (float)Frame.Width, 0.2f*(float)Frame.Height, 0.7f * (float)Frame.Width,  0.6f*(float)Frame.Height);
			Add (ForgetPasswordBtn);

			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.DidShowNotification,KeyBoardUpNotification);

			position ();
		}

		private void KeyBoardUpNotification(NSNotification notification)
		{
			UIView activeview=new UIView();

			// get the keyboard size
			CGRect r = UIKeyboard.BoundsFromNotification (notification);

			// Find what opened the keyboard
			foreach (UIView view in this.Subviews) {
				if (view.IsFirstResponder)
					activeview = view;
			}

			if(ContentOffset.Y<activeview.Frame.Y-(Frame.Height-r.Height-activeview.Frame.Height))
			{	
				SetContentOffset (new CGPoint(0.0f,activeview.Frame.Y-(Frame.Height-r.Height-activeview.Frame.Height)), true);
			}
		}


		public void position(){
			ContentSize = new CGSize ((float)Frame.Width,220f);
			EmailIconImageView.Frame = new RectangleF (0f, 0f, 30.0f,  30.0f);
			EmailTextField.Frame= new RectangleF (50.0f, 0, (float)Frame.Width-50.0f, 40.0f);
			EmailSeperateLineView.Frame= new RectangleF (0f, 40.0f, (float)Frame.Width,  2.0f);

			PasswordIconImageView.Frame= new RectangleF (0f, 50f, 30.0f,  30.0f);
			PasswordTextField.Frame= new RectangleF (50.0f, 50f, (float)Frame.Width-50.0f, 40.0f);
			PasswordSeperateLineView.Frame= new RectangleF (0f, 90.0f, (float)Frame.Width,  2.0f);

			RememberMeSwitch.Frame= new RectangleF (0f, 105f, 50.0f, 30.0f);
			RememberMeLabel.Frame = new RectangleF (60.0f, 105f, (float)Frame.Width-60.0f,  30.0f);

			LoginBtn.Frame= new RectangleF (0f, 160.0f, (float)Frame.Width, 40f);
			ForgetPasswordBtn.Frame= new RectangleF (0.2f*(float)Frame.Width, 210f, 0.6f*(float)Frame.Width,10f);
		}	
	}
}

