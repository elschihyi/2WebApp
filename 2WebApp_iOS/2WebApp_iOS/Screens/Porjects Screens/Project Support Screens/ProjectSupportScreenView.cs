using System;
using UIKit;
using System.Drawing;
using CoreDataService;

namespace WebApp_iOS
{
	public class ProjectSupportScreenView:UIScrollView
	{
		public UILabel titleLabel { get; set; }

		//*******************************************
		public UILabel TypeLabel{ get; set;}
		public UILabel HourRemainLabel1{ get; set;}
		public UILabel HourRemainLabel2{ get; set;}
		public UILabel LatestBackUpLabel{ get; set;}
		public UILabel LastRestoredLabel{ get; set;}
		public UILabel SystemStatusLabel{ get; set;}
		public UILabel WebSiteAuditsLabel{ get; set;}
		public UILabel YearAndAnalysisLabel{ get; set;}

		//*******************************************
		public UILabel comma1{ get; set;}
		public UILabel comma2{ get; set;}
		public UILabel comma3{ get; set;}
		public UILabel comma4{ get; set;}
		public UILabel comma5{ get; set;}
		public UILabel comma6{ get; set;}
		public UILabel comma7{ get; set;}

		//*******************************************
		public UILabel TypeValueLabel{ get; set;}
		public UILabel HourRemainValueLabel{ get; set;}
		public UILabel LatestBackUpValueLabel{ get; set;}
		public UILabel LastRestoredValueLabel{ get; set;}
		public UILabel SystemStatusValueLabel{ get; set;}
		public UIButton WebSiteAuditsBtn{ get; set;}
		public UIButton YearAndAnalysisBtn{ get; set;}

		public UIButton ContactSupportBtn{ get; set;}

		public ProjectSupportScreenView (projectsummary theProject,RectangleF Frame)
		{
			this.Frame = Frame;
			titleLabel= new UILabel () 
			{
				BackgroundColor=UIColor.FromRGB(0,172,237),
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
			};
			Add(titleLabel);

			//*******************************************
			TypeLabel= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Text="Type",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(TypeLabel);

			HourRemainLabel1= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Text="Consultation Hours",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(HourRemainLabel1);

			HourRemainLabel2= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Text="Remaining",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(HourRemainLabel2);

			LatestBackUpLabel= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Text="Latest Back Up",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(LatestBackUpLabel);

			LastRestoredLabel= new UILabel ()
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Text="Latest Restored",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(LastRestoredLabel);
			SystemStatusLabel= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Text="System Status",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(SystemStatusLabel);

			WebSiteAuditsLabel= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Text="Website Audits",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(WebSiteAuditsLabel);

			YearAndAnalysisLabel= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Text="Year and Analysis",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(YearAndAnalysisLabel);
			//*******************************************
			comma1= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
				Text=":",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(comma1);
			comma2= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
				Text=":",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(comma2);
			comma3= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
				Text=":",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(comma3);
			comma4= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
				Text=":",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(comma4);
			comma5= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
				Text=":",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(comma5);

			comma6= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
				Text=":",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(comma6);

			comma7= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
				Text=":",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(comma7);


			//*******************************************
			TypeValueLabel= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(TypeValueLabel);

			HourRemainValueLabel= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(HourRemainValueLabel);

			LatestBackUpValueLabel= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(TypeValueLabel);

			LastRestoredValueLabel= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(LastRestoredValueLabel);

			SystemStatusValueLabel= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(SystemStatusValueLabel);

			WebSiteAuditsBtn=UIButton.FromType(UIButtonType.RoundedRect);
			WebSiteAuditsBtn.SetTitle("View",UIControlState.Normal);
			WebSiteAuditsBtn.BackgroundColor=UIColor.White;
			WebSiteAuditsBtn.SetTitleColor(UIColor.FromRGB(0,172,237),UIControlState.Normal);
			Add(WebSiteAuditsBtn);

			YearAndAnalysisBtn=UIButton.FromType(UIButtonType.RoundedRect);
			YearAndAnalysisBtn.SetTitle("View",UIControlState.Normal);
			YearAndAnalysisBtn.BackgroundColor=UIColor.White;
			YearAndAnalysisBtn.SetTitleColor(UIColor.FromRGB(0,172,237),UIControlState.Normal);
			Add(YearAndAnalysisBtn);

			ContactSupportBtn=UIButton.FromType(UIButtonType.RoundedRect);
			ContactSupportBtn.SetTitle("Contact Support",UIControlState.Normal);
			ContactSupportBtn.BackgroundColor=UIColor.FromRGB(0,172,237);
			ContactSupportBtn.SetTitleColor(UIColor.White,UIControlState.Normal);
			Add(ContactSupportBtn);

			try{
				TypeValueLabel.Text=theProject.support_package[0].name;
				HourRemainValueLabel.Text=theProject.support_package[0].hourused;
				LatestBackUpValueLabel.Text=theProject.support_package[0].lastbackup;
				LastRestoredValueLabel.Text=theProject.support_package[0].lastpost;
				SystemStatusValueLabel.Text=theProject.support_package[0].status;
			}catch{
			}	
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

			//*******************************************
			TypeLabel.Frame = new RectangleF (5f,40f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);
			HourRemainLabel1.Frame = new RectangleF (5f,80f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);
			HourRemainLabel2.Frame = new RectangleF (5f,120f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);
			LatestBackUpLabel.Frame = new RectangleF (5f,160f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);
			LastRestoredLabel.Frame = new RectangleF (5f,200f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);
			SystemStatusLabel.Frame = new RectangleF (5f,240f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);
			WebSiteAuditsLabel.Frame = new RectangleF (5f,280f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);
			YearAndAnalysisLabel.Frame = new RectangleF (5f,320f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);

			//*******************************************
			comma1.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,40f, 4f, 30.0f);
			comma2.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,120f, 4f, 30.0f);
			comma3.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,160f, 4f, 30.0f);
			comma4.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,200f, 4f, 30.0f);
			comma5.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,240f, 4f, 30.0f);
			comma6.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,280f, 4f, 30.0f);
			comma7.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,320f, 4f, 30.0f);

			//*******************************************
			TypeValueLabel.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,40f, (float)UIScreen.MainScreen.Bounds.Width/2-10f, 30.0f);
			HourRemainValueLabel.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,120f, (float)UIScreen.MainScreen.Bounds.Width/2-10f, 30.0f);
			LatestBackUpValueLabel.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,160f, (float)UIScreen.MainScreen.Bounds.Width/2-10f, 30.0f);
			LastRestoredValueLabel.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,200f, (float)UIScreen.MainScreen.Bounds.Width/2-10f, 30.0f);
			SystemStatusValueLabel.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,240f, (float)UIScreen.MainScreen.Bounds.Width/2-10f, 30.0f);
			WebSiteAuditsBtn.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,280f, (float)UIScreen.MainScreen.Bounds.Width/4-10f, 30.0f);
			YearAndAnalysisBtn.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,320f, (float)UIScreen.MainScreen.Bounds.Width/4-10f, 30.0f);

			ContactSupportBtn.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/4f,370f, (float)UIScreen.MainScreen.Bounds.Width/2f, 30.0f);
		}
	}
}

