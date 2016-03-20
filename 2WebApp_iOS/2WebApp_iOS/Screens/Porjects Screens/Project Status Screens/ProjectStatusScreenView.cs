using System;
using UIKit;
using System.Drawing;
using CoreGraphics;
using CoreDataService;
using System.Collections.Generic;

namespace WebApp_iOS
{

	public class ProjectStatusScreenView:UIScrollView
	{
		//public string phase;
		public int phaseNumber;
		List<string> TaskString = new List<string> ();

		public UILabel titleLabel { get; set; }
		public UIScrollView ScrollView{ get; set;}
		public ProjectStatusScreenSubView StatusView1{ get; set;}
		public UIImageView DotImageView1{ get; set;}
		public ProjectStatusScreenSubView StatusView2{ get; set;}
		public UIImageView DotImageView2{ get; set;}
		public ProjectStatusScreenSubView StatusView3{ get; set;}
		public UIImageView DotImageView3{ get; set;}
		public ProjectStatusScreenSubView StatusView4{ get; set;}
		public UIImageView DotImageView4{ get; set;}
		public ProjectStatusScreenSubView StatusView5{ get; set;}

		public ProjectStatusScreenView (RectangleF Frame,projectsummary theProject)
		{
			this.Frame = Frame;

			phaseNumber=getPhaseNumber (theProject);

			titleLabel= new UILabel () 
			{
				BackgroundColor=UIColor.FromRGB(0,172,237),
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
			};
			Add(titleLabel);

			ScrollView = new UIScrollView () {
				BackgroundColor=UIColor.Clear,
			};	
			Add(ScrollView);


			TaskString.Clear ();
			foreach (var task in theProject.tasks) {
				if(task.display=="1"&&task.status=="1"){
					TaskString.Add (task.name);
				}	
			}				
			StatusView1 = new ProjectStatusScreenSubView ("Discovery",phaseNumber>1?"Completed":"InComplete",TaskString.ToArray());
			ScrollView.Add (StatusView1);

			DotImageView1= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image=new UIImage("Cut_Images/Separator_Dot.png"),
			};
			ScrollView.Add (DotImageView1);

			TaskString.Clear ();
			foreach (var task in theProject.tasks) {
				if(task.display=="1"&&task.status=="2"){
					TaskString.Add (task.name);
				}	
			}
			StatusView2 = new ProjectStatusScreenSubView ("Design", phaseNumber>2?"Completed":"InComplete",TaskString.ToArray());
			ScrollView.Add (StatusView2);

			DotImageView2= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image=new UIImage("Cut_Images/Separator_Dot.png"),
			};
			ScrollView.Add (DotImageView2);

			TaskString.Clear ();
			foreach (var task in theProject.tasks) {
				if(task.display=="1"&&task.status=="3"){
					TaskString.Add (task.name);
				}	
			}
			StatusView3 = new ProjectStatusScreenSubView ("Development", phaseNumber>3?"Completed":"InComplete",TaskString.ToArray());
			ScrollView.Add (StatusView3);

			DotImageView3= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image=new UIImage("Cut_Images/Separator_Dot.png"),
			};
			ScrollView.Add (DotImageView3);

			TaskString.Clear ();
			foreach (var task in theProject.tasks) {
				if(task.display=="1"&&task.status=="4"){
					TaskString.Add (task.name);
				}	
			}
			StatusView4 = new ProjectStatusScreenSubView ("Quality Assurance", phaseNumber>4?"Completed":"InComplete",TaskString.ToArray());
			ScrollView.Add (StatusView4);

			DotImageView4= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image=new UIImage("Cut_Images/Separator_Dot.png"),
			};
			ScrollView.Add (DotImageView4);

			TaskString.Clear ();
			foreach (var task in theProject.tasks) {
				if(task.display=="1"&&task.status=="5"){
					TaskString.Add (task.name);
				}	
			}
			StatusView5 = new ProjectStatusScreenSubView ("Launch",phaseNumber>5?"Completed":"InComplete",TaskString.ToArray());
			ScrollView.Add (StatusView5);

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
			float totalHeight = 0f;
			//***************************************************************************************************************************************************
			titleLabel.Frame = new RectangleF (5f, 8, (float)UIScreen.MainScreen.Bounds.Width-10f, 30.0f);
			titleLabel.Font = UIFont.SystemFontOfSize (18.0f);
			ScrollView.Frame= new RectangleF (0f,45f, (float)UIScreen.MainScreen.Bounds.Width, (float)Frame.Height-45.0f);
			var contentHeight = StatusView1.Frame.Height + StatusView2.Frame.Height + StatusView3.Frame.Height + StatusView4.Frame.Height + StatusView5.Frame.Height;
			ScrollView.ContentSize = new CGSize ((float)UIScreen.MainScreen.Bounds.Width,contentHeight+210f);

			StatusView1.Frame= new RectangleF((float)StatusView1.Frame.X,totalHeight,(float)StatusView1.Frame.Width,(float)StatusView1.Frame.Height);
			totalHeight += (float)StatusView1.Frame.Height;
			DotImageView1.Frame=new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-25.0f,totalHeight , 50.0f, 50.0f);
			totalHeight += (float)DotImageView1.Frame.Height;

			StatusView2.Frame=new RectangleF((float)StatusView2.Frame.X,totalHeight,(float)StatusView2.Frame.Width,(float)StatusView2.Frame.Height);
			totalHeight += (float)StatusView2.Frame.Height;
			DotImageView2.Frame=new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-25.0f,totalHeight , 50.0f, 50.0f);
			totalHeight += (float)DotImageView2.Frame.Height;

			StatusView3.Frame=new RectangleF((float)StatusView3.Frame.X,totalHeight,(float)StatusView3.Frame.Width,(float)StatusView3.Frame.Height);
			totalHeight += (float)StatusView3.Frame.Height;
			DotImageView3.Frame=new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-25.0f,totalHeight , 50.0f, 50.0f);
			totalHeight += (float)DotImageView3.Frame.Height;

			StatusView4.Frame=new RectangleF((float)StatusView4.Frame.X,totalHeight,(float)StatusView4.Frame.Width,(float)StatusView4.Frame.Height);
			totalHeight += (float)StatusView4.Frame.Height;
			DotImageView4.Frame=new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-25.0f,totalHeight , 50.0f, 50.0f);
			totalHeight += (float)DotImageView4.Frame.Height;

			StatusView5.Frame=new RectangleF((float)StatusView5.Frame.X,totalHeight,(float)StatusView5.Frame.Width,(float)StatusView5.Frame.Height);
			totalHeight += (float)StatusView5.Frame.Height;
		}

		private int getPhaseNumber (projectsummary theProject){
			switch (theProject.phase) {
			case "Discovery":
				return 1;
			case "Design":
				return 2;
			case "Development":
				return 3;
			case "Quality Assurance":
				return 4;
			case "Launch":
				return 5;
			default:
				return 1;
			}
		}	
	}

	public class ProjectStatusScreenSubView:UIView
	{
		public UIImageView PhaseImageView{ get; set;}
		public UILabel PhaseNameStatusLabel{ get; set;}
		public UIImageView CheckImageView{ get; set;}
		public UIView [] ResultBorderViews{ get; set;}
		public UILabel [] ResultLabelViews{ get; set;}

		public ProjectStatusScreenSubView(string phase,string status,string [] taskNames){
			BackgroundColor = UIColor.Clear;

			PhaseImageView= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
			};
			Add (PhaseImageView);

			PhaseNameStatusLabel= new UILabel () 
			{
				BackgroundColor=UIColor.Clear,
				TextColor = UIColor.White,
				TextAlignment=UITextAlignment.Center,
			};
			Add(PhaseNameStatusLabel);

			CheckImageView= new UIImageView () {
				ContentMode = UIViewContentMode.ScaleAspectFit,
				Image=new UIImage("Cut_Images/project_check.png"),
			};
			Add (CheckImageView);

			ResultBorderViews=new UIView[taskNames.Length]; 
			ResultLabelViews = new UILabel[taskNames.Length];
			for(int i=0;i<taskNames.Length;i++){
				UIView ResultBorderView;
				UILabel ResultLabelView;

				ResultBorderView = new UIView () {
					BackgroundColor = UIColor.Gray,
				};
				ResultBorderViews [i] = ResultBorderView;
				Add (ResultBorderView);

				ResultLabelView= new UILabel () {
					//BackgroundColor=UIColor.FromRGB(0,172,237),
					BackgroundColor =UIColor.FromRGB(35,40,46),
					TextColor=UIColor.White,
					TextAlignment=UITextAlignment.Center,
				};
				ResultLabelViews [i] = ResultLabelView;
				Add (ResultLabelView);
			}

			if (phase == "Discovery") {
				if (status == "Completed") {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase1_blue.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = false;

				} else {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase1_grey.png");
					PhaseNameStatusLabel.Text=phase;
					CheckImageView.Hidden = true;
				}		
			} else if (phase == "Design") {
				if (status == "Completed") {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase2_blue.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = false;
				} else {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase2_grey.png");
					PhaseNameStatusLabel.Text=phase;
					CheckImageView.Hidden = true;
				}
			}else if (phase == "Development") {
				if (status == "Completed") {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase3_blue.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = false;
				} else {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase3_grey.png");
					PhaseNameStatusLabel.Text=phase;
					CheckImageView.Hidden = true;
				}
			}else if (phase == "Quality Assurance") {
				if (status == "Completed") {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase4_blue.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = false;
				} else {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase4_grey.png");
					PhaseNameStatusLabel.Text=phase;
					CheckImageView.Hidden = true;
				}
			}else if (phase == "Launch") {
				if (status == "Completed") {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase5_blue.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = false;
				} else {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase5_grey.png");
					PhaseNameStatusLabel.Text=phase;
					CheckImageView.Hidden = true;
				}
			}
			for (int i = 0; i < taskNames.Length; i++) {
				ResultLabelViews[i].Text =taskNames[i];
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

		public void position(){
			Frame=new RectangleF(0,0,(float)UIScreen.MainScreen.Bounds.Width,75f+27f*ResultLabelViews.Length);

			float SWidth = (float)UIScreen.MainScreen.Bounds.Width;
			float totalHeight = 0f;
			PhaseImageView.Frame=new RectangleF(0f,totalHeight,SWidth,50f);
			totalHeight += (float)PhaseImageView.Frame.Height;
			PhaseNameStatusLabel.Font=UIFont.SystemFontOfSize (12.0f);
			PhaseNameStatusLabel.Frame=new RectangleF(0.5f*SWidth-(float)PhaseNameStatusLabel.IntrinsicContentSize.Width/2.0f,totalHeight,
				(float)PhaseNameStatusLabel.IntrinsicContentSize.Width,30f);
			CheckImageView.Frame=new RectangleF((float)PhaseNameStatusLabel.Frame.X+(float)PhaseNameStatusLabel.Frame.Width,totalHeight+5f,
				20f,20f);
			totalHeight += (float)PhaseNameStatusLabel.Frame.Height;

			float largestWidth = 0f;
			for (int i = 0; i < ResultLabelViews.Length; i++) {
				ResultLabelViews[i].Font=UIFont.SystemFontOfSize (12.0f);
				if((float)ResultLabelViews[i].IntrinsicContentSize.Width>largestWidth){
					largestWidth=(float)ResultLabelViews[i].IntrinsicContentSize.Width;
				}	
			}	

			for (int i = 0; i < ResultLabelViews.Length; i++) {
				ResultLabelViews[i].Frame=new RectangleF((SWidth-largestWidth)/2.0f,totalHeight,largestWidth,25f);
				ResultBorderViews[i].Frame=new RectangleF((float)ResultLabelViews[i].Frame.X-1f,(float)ResultLabelViews[i].Frame.Y-1f,
					(float)ResultLabelViews[i].Frame.Width+2f,(float)ResultLabelViews[i].Frame.Height+2f);
				totalHeight +=(float) ResultLabelViews[i].Frame.Height+2f;
			}	
		}	
	}	
}

