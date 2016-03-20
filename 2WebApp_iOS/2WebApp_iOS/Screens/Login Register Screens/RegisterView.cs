using System;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Drawing;

namespace WebApp_iOS
{
	public class RegisterView:UIScrollView
	{
		public UIImageView EmailIconImageView{ get; set;}
		public UITextField EmailTextField{ get; set;}
		public UIView EmailSeperateLineView{ get; set;}

		public UIImageView PasswordIconImageView{ get; set;}
		public UITextField PasswordTextField{ get; set;}
		public UIView PasswordSeperateLineView{ get; set;}

		public UIImageView FirstNameIconImageView{ get; set;}
		public UITextField FirstNameTextField{ get; set;}
		public UIView FirstNameSeperateLineView{ get; set;}

		public UIImageView LastNameIconImageView{ get; set;}
		public UITextField LastNameTextField{ get; set;}
		public UIView LastNameSeperateLineView{ get; set;}

		public UISwitch RememberMeSwitch{ get; set;}
		public UILabel RememberMeLabel { get; set;}

		public UIButton RegisterBtn{ get; set;}

		public RegisterView ()
		{
			BackgroundColor = UIColor.Clear;
			ShowsVerticalScrollIndicator = false;
			//BackgroundColor = UIColor.Yellow;

			EmailIconImageView= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image = UIImage.FromFile ("Cut_Images/Email_Icon.png"),
			};	
			Add (EmailIconImageView);

			EmailTextField=new UITextField( ) 
			{
				KeyboardType=UIKeyboardType.Default,
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

			FirstNameIconImageView= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image = UIImage.FromFile ("Cut_Images/Name_Icon.png"),
			};	
			Add (FirstNameIconImageView);

			FirstNameTextField=new UITextField( ) 
			{
				KeyboardType=UIKeyboardType.Default,
				ReturnKeyType=UIReturnKeyType.Done,
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear,
				AttributedPlaceholder=new NSAttributedString("FIRST NAME",null,UIColor.LightGray),
				BorderStyle = UITextBorderStyle.None,
				Enabled = true,
				SecureTextEntry=false,
			};
			FirstNameTextField.ShouldReturn += (textField) => 
			{
				textField.ResignFirstResponder();
				return true;
			};
			Add(FirstNameTextField);

			FirstNameSeperateLineView = new UIView () {
				BackgroundColor = UIColor.LightGray,
			};
			Add(FirstNameSeperateLineView);

			LastNameIconImageView= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image = UIImage.FromFile ("Cut_Images/Name_Icon.png"),
			};	
			Add (LastNameIconImageView);

			LastNameTextField=new UITextField( ) 
			{
				KeyboardType=UIKeyboardType.Default,
				ReturnKeyType=UIReturnKeyType.Done,
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear,
				AttributedPlaceholder=new NSAttributedString("LAST NAME",null,UIColor.LightGray),
				BorderStyle = UITextBorderStyle.None,
				Enabled = true,
				SecureTextEntry=false,
			};
			LastNameTextField.ShouldReturn += (textField) => 
			{
				textField.ResignFirstResponder();
				return true;
			};
			Add(LastNameTextField);

			LastNameSeperateLineView = new UIView () {
				BackgroundColor = UIColor.LightGray,
			};
			Add(LastNameSeperateLineView);

			RememberMeSwitch = new UISwitch();
			RememberMeSwitch.BackgroundColor = UIColor.Clear;
			RememberMeSwitch.TintColor= UIColor.FromRGB(0,172,237);
			RememberMeSwitch.OnTintColor=UIColor.FromRGB(0,172,237);
			Add (RememberMeSwitch);

			RememberMeLabel= new UILabel () {
				Font = UIFont.BoldSystemFontOfSize (12f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Left,
				Text="Remember Me",
			};
			Add (RememberMeLabel);

			RegisterBtn = UIButton.FromType (UIButtonType.RoundedRect);
			RegisterBtn.BackgroundColor = UIColor.FromRGB(0,172,237);
			RegisterBtn.SetTitleColor(UIColor.White,UIControlState.Normal);
			RegisterBtn.SetTitle ("Register",UIControlState.Normal);
			Add (RegisterBtn);

			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.DidShowNotification,KeyBoardUpNotification);
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

		public void position (){
			ContentSize = new CGSize ((float)Frame.Width,320f);
			EmailIconImageView.Frame = new RectangleF (0f, 0f, 30.0f,  30.0f);
			EmailTextField.Frame= new RectangleF (50.0f, 0, (float)Frame.Width-50.0f, 40.0f);
			EmailSeperateLineView.Frame= new RectangleF (0f, 40.0f, (float)Frame.Width,  2.0f);

			PasswordIconImageView.Frame= new RectangleF (0f, 50f, 30.0f,  30.0f);
			PasswordTextField.Frame= new RectangleF (50.0f, 50f, (float)Frame.Width-50.0f, 40.0f);
			PasswordSeperateLineView.Frame= new RectangleF (0f, 90.0f, (float)Frame.Width,  2.0f);

			FirstNameIconImageView.Frame= new RectangleF (0f, 100f, 30.0f,  30.0f);
			FirstNameTextField.Frame= new RectangleF (50.0f, 100f, (float)Frame.Width-50.0f, 40.0f);
			FirstNameSeperateLineView.Frame= new RectangleF (0f, 140.0f, (float)Frame.Width,  2.0f);

			LastNameIconImageView.Frame= new RectangleF (0f, 150f, 30.0f,  30.0f);
			LastNameTextField.Frame= new RectangleF (50.0f, 150f, (float)Frame.Width-50.0f, 40.0f);
			LastNameSeperateLineView.Frame= new RectangleF (0f, 190.0f, (float)Frame.Width,  2.0f);


			RememberMeSwitch.Frame= new RectangleF (0f, 205f, 50.0f, 30.0f);
			RememberMeLabel.Frame = new RectangleF (60.0f, 205f, (float)Frame.Width-60.0f,  30.0f);

			RegisterBtn.Frame= new RectangleF (0f, 260.0f, (float)Frame.Width, 40f);
		}	
	}
}

