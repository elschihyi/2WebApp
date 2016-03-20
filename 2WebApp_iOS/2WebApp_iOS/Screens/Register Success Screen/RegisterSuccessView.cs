using System;
using UIKit;
using CoreGraphics;
using System.Drawing;

namespace WebApp_iOS
{
	public class RegisterSuccessView:UIView
	{
		public UIImageView LogoImageView { get; set;}
		public UILabel CompanyLabel { get; set;}
		public UILabel Label1{ get; set;}
		public UILabel Label2{ get; set;}
		public UILabel Label3{ get; set;}
		public UILabel Label4{ get; set;}
		public UILabel Label5{ get; set;}
		public UILabel Label6{ get; set;}
		public UILabel Label7{ get; set;}
		public UILabel Label8{ get; set;}
		public UILabel Label9{ get; set;}
		public UIImageView ImageView1{ get; set;}
		public UILabel Label10{ get; set;}
		public UILabel Label11{ get; set;}
		public UIButton okBtn{ get; set;}

		public RegisterSuccessView (CGRect Frame)
		{
			this.Frame = Frame;
			BackgroundColor = UIColor.Clear;

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

			Label1= new UILabel () {
				Font = UIFont.SystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center,
				Text="Thank you for registering with",
			};
			Add (Label1);

			Label2= new UILabel () {
				Font = UIFont.SystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center,
				Text="the",
			};
			Add (Label2);

			Label3= new UILabel () {
				Font = UIFont.SystemFontOfSize (16f),
				TextColor = UIColor.FromRGB(0,172,237),
				TextAlignment = UITextAlignment.Center,
				Text="2web app!",
			};
			Add (Label3);

			Label4= new UILabel () {
				Font = UIFont.SystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center,
				Text="You can review and customize your",
			};
			Add (Label4);

			Label5= new UILabel () {
				Font = UIFont.SystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center,
				Text="Settings by using the",
			};
			Add (Label5);

			Label6= new UILabel () {
				Font = UIFont.SystemFontOfSize (16f),
				TextColor = UIColor.FromRGB(0,172,237),
				TextAlignment = UITextAlignment.Center,
				Text="\"Profile\"",
			};
			Add (Label6);

			Label7= new UILabel () {
				Font = UIFont.SystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center,
				Text="and",
			};
			Add (Label7);

			Label8= new UILabel () {
				Font = UIFont.SystemFontOfSize (16f),
				TextColor = UIColor.FromRGB(0,172,237),
				TextAlignment = UITextAlignment.Center,
				Text="\"Notification\"",
			};
			Add (Label8);

			Label9= new UILabel () {
				Font = UIFont.SystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center,
				Text="settings(",
			};
			Add (Label9);

			ImageView1= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image=UIImage.FromFile("Cut_Images/Setting_Icon_Blue.png"),
			};	
			Add (ImageView1);

			Label10= new UILabel () {
				Font = UIFont.SystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center,
				Text=")",
			};
			Add (Label10);

			Label11= new UILabel () {
				Font = UIFont.SystemFontOfSize (16f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center,
				Text="in the title bar.",
			};
			Add (Label11);

			okBtn = UIButton.FromType (UIButtonType.RoundedRect);
			okBtn.BackgroundColor = UIColor.FromRGB(0,172,237);
			okBtn.SetTitleColor(UIColor.White,UIControlState.Normal);
			okBtn.SetTitle ("OK",UIControlState.Normal);
			Add (okBtn);

			position ();
		}

		public void position(){
			LogoImageView.Frame = new RectangleF (0.5f * (float)Frame.Width-20f, 15f, 40f, 40f );
			CompanyLabel.Frame = new RectangleF (0f, 40.0f+15f, 1.0f*(float)Frame.Width, 0.15f*(float)Frame.Height);

			Label1.Frame = new RectangleF (0f,55f+0.15f*(float)Frame.Height,(float)Frame.Width,30f);

			var line2Width = Label2.IntrinsicContentSize.Width + Label3.IntrinsicContentSize.Width;
			Label2.Frame = new RectangleF (((float)Frame.Width-(float)line2Width)/2.0f,85f+0.15f*(float)Frame.Height,(float)Label2.IntrinsicContentSize.Width,30f);
			Label3.Frame = new RectangleF ((float)(Label2.Frame.X+Label2.Frame.Width),85f+0.15f*(float)Frame.Height,(float)Label3.IntrinsicContentSize.Width,30f);

			Label4.Frame = new RectangleF (0f,145f+0.15f*(float)Frame.Height,(float)Frame.Width,30f);

			var line4Width = Label5.IntrinsicContentSize.Width + Label6.IntrinsicContentSize.Width;
			Label5.Frame = new RectangleF (((float)Frame.Width-(float)line4Width)/2.0f,175f+0.15f*(float)Frame.Height,(float)Label5.IntrinsicContentSize.Width ,30f);
			Label6.Frame = new RectangleF ((float)(Label5.Frame.X+Label5.Frame.Width),175f+0.15f*(float)Frame.Height,(float)Label6.IntrinsicContentSize.Width,30f);

			var line5Width = Label7.IntrinsicContentSize.Width + Label8.IntrinsicContentSize.Width
				+Label9.IntrinsicContentSize.Width + Label10.IntrinsicContentSize.Width+30f;
			Label7.Frame = new RectangleF (((float)Frame.Width-(float)line5Width)/2.0f,205f+0.15f*(float)Frame.Height,(float)Label7.IntrinsicContentSize.Width,30f);
			Label8.Frame = new RectangleF ((float)(Label7.Frame.X+Label7.Frame.Width),205f+0.15f*(float)Frame.Height,(float)Label8.IntrinsicContentSize.Width,30f);
			Label9.Frame = new RectangleF ((float)(Label8.Frame.X+Label8.Frame.Width),205f+0.15f*(float)Frame.Height,(float)Label9.IntrinsicContentSize.Width,30f);
			ImageView1.Frame = new RectangleF ((float)(Label9.Frame.X+Label9.Frame.Width),205f+0.15f*(float)Frame.Height,30f,30f);
			Label10.Frame = new RectangleF ((float)(ImageView1.Frame.X+ImageView1.Frame.Width),205f+0.15f*(float)Frame.Height,(float)Label10.IntrinsicContentSize.Width,30f);

			Label11.Frame = new RectangleF (0f,235f+0.15f*(float)Frame.Height,(float)Frame.Width,30f);

			okBtn.Frame = new RectangleF (0.4f*(float)Frame.Width,280f+0.15f*(float)Frame.Height,0.2f*(float)Frame.Width,40f);
		}	
	}
}

