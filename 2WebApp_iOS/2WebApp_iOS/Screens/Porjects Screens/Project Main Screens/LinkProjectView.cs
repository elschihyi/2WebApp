using System;
using UIKit;
using System.Drawing;

namespace WebApp_iOS
{
	public class LinkProjectView:UIView
	{
		public UILabel Label1{ get; set;}
		public UILabel Label2{ get; set;}
		public UILabel Label3{ get; set;}
		public UIButton Button1{ get; set;}

		public LinkProjectView ()
		{
			Label1= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				Text="Have a project",
				TextAlignment=UITextAlignment.Center,
			};
			Add(Label1);

			Label2= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				Text="with",
				TextAlignment=UITextAlignment.Right,
			};
			Add(Label2);

			Label3= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.FromRGB(0,172,237),
				Text="2web?",
				TextAlignment=UITextAlignment.Left,
			};
			Add(Label3);

			Button1 = UIButton.FromType (UIButtonType.RoundedRect);
			Button1.SetTitle("Link Your Porject",UIControlState.Normal);
			Button1.BackgroundColor=UIColor.FromRGB(0,172,237);
			Button1.SetTitleColor(UIColor.White,UIControlState.Normal);
			Add(Button1);

			Position ();
		}
		public void Hide ()
		{
			UIView.Animate (
				0.5, // duration
				() => { Alpha = 1; },
				() => { RemoveFromSuperview(); }
			);
		}

		public void Position()
		{
			Frame=new RectangleF (0,0, (float)UIScreen.MainScreen.Bounds.Width, 90f);
			//***************************************************************************************************************************************************
			Label1.Frame=new RectangleF (0f,0f, (float)Frame.Width, 30.0f);
			Label2.Frame=new RectangleF (0f,30f, (float)Frame.Width/2.0f, 30.0f);
			Label3.Frame=new RectangleF ((float)Frame.Width/2.0f,30f, (float)Frame.Width/2.0f, 30.0f);
			Button1.Frame=new RectangleF (0.2f*(float)Frame.Width,60f, 0.6f*(float)Frame.Width, 30.0f);
		}
	}
}

