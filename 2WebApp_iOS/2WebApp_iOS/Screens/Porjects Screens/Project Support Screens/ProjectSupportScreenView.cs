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
		public UILabel HourRemainLabel{ get; set;}
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
		public UITextView TypeValueTextView{ get; set;}
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

			HourRemainLabel= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Text="Consultation Hours",
				Font = UIFont.SystemFontOfSize (16.0f)
			};
			Add(HourRemainLabel);

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
			TypeValueTextView= new UITextView () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Left,
				Font = UIFont.SystemFontOfSize (16.0f),
				Editable=false,
				ScrollEnabled=false,
			};
			Add(TypeValueTextView);

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
			Add(LatestBackUpValueLabel);

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
				TypeValueTextView.Text=theProject.support_package[0].name;
				HourRemainValueLabel.Text=theProject.support_package[0].hourused+"/"+theProject.support_package[0].totalhour;
				DateTime theDate1 = DateTime.ParseExact (theProject.support_package[0].lastbackup, "yyyy-MM-dd HH:mm:ss", null);	
				LatestBackUpValueLabel.Text=theDate1.ToString("MMMM dd, yyyy");
				DateTime theDate2 = DateTime.ParseExact (theProject.support_package[0].lastpost, "yyyy-MM-dd HH:mm:ss", null);	
				LastRestoredValueLabel.Text=theDate2.ToString("MMMM dd, yyyy");
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
			HourRemainLabel.Frame = new RectangleF (5f,90f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);
			LatestBackUpLabel.Frame = new RectangleF (5f,140f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);
			LastRestoredLabel.Frame = new RectangleF (5f,190f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);
			SystemStatusLabel.Frame = new RectangleF (5f,240f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);
			WebSiteAuditsLabel.Frame = new RectangleF (5f,290f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);
			YearAndAnalysisLabel.Frame = new RectangleF (5f,340f, (float)UIScreen.MainScreen.Bounds.Width/2-7f, 30.0f);

			//*******************************************
			comma1.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,40f, 4f, 30.0f);
			comma2.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,90f, 4f, 30.0f);
			comma3.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,140f, 4f, 30.0f);
			comma4.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,190f, 4f, 30.0f);
			comma5.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,240f, 4f, 30.0f);
			comma6.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,290f, 4f, 30.0f);
			comma7.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2f,340f, 4f, 30.0f);

			//*******************************************
			TypeValueTextView.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,40f, (float)UIScreen.MainScreen.Bounds.Width/2-10f, 50.0f);
			HourRemainValueLabel.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,90f, (float)UIScreen.MainScreen.Bounds.Width/2-10f, 30.0f);
			LatestBackUpValueLabel.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,140f, (float)UIScreen.MainScreen.Bounds.Width/2-10f, 30.0f);
			LastRestoredValueLabel.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,190f, (float)UIScreen.MainScreen.Bounds.Width/2-10f, 30.0f);
			SystemStatusValueLabel.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,240f, (float)UIScreen.MainScreen.Bounds.Width/2-10f, 30.0f);
			WebSiteAuditsBtn.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,290f, (float)UIScreen.MainScreen.Bounds.Width/4, 30.0f);
			YearAndAnalysisBtn.Frame = new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2+10f,340f, (float)UIScreen.MainScreen.Bounds.Width/4, 30.0f);

			ContactSupportBtn.Frame = new RectangleF (0.2f*(float)UIScreen.MainScreen.Bounds.Width,410f, 0.6f*(float)UIScreen.MainScreen.Bounds.Width, 44.0f);
		}
	}
}

