using System;
using UIKit;
using System.Drawing;
using CoreGraphics;

namespace WebApp_iOS
{

	public class ProjectStatusScreenView:UIScrollView
	{
		public string phase;

		public UILabel titleLabel { get; set; }
		public UIScrollView ScrollView{ get; set;}
		public ProjectStatusScreenSubView StatusView1{ get; set;}
		public UIView linkImageView1{ get; set;}
		public ProjectStatusScreenSubView StatusView2{ get; set;}
		public UIView linkImageView2{ get; set;}
		public ProjectStatusScreenSubView StatusView3{ get; set;}
		public UIView linkImageView3{ get; set;}
		public ProjectStatusScreenSubView StatusView4{ get; set;}
		public UIView linkImageView4{ get; set;}
		public ProjectStatusScreenSubView StatusView5{ get; set;}

		public ProjectStatusScreenView (RectangleF Frame,string phase,string status)
		{
			this.Frame = Frame;

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

			StatusView1 = new ProjectStatusScreenSubView ("Discovery", "Completed");
			ScrollView.Add (StatusView1);

			linkImageView1 = new UIView () {
				BackgroundColor = UIColor.Gray,
			};
			ScrollView.Add(linkImageView1);	

			StatusView2 = new ProjectStatusScreenSubView ("Design", "Incompleted");
			ScrollView.Add (StatusView2);

			linkImageView2 = new UIView () {
				BackgroundColor = UIColor.Gray,
			};
			ScrollView.Add(linkImageView2);

			StatusView3 = new ProjectStatusScreenSubView ("Developement", "Incompleted");
			ScrollView.Add (StatusView3);

			linkImageView3 = new UIView () {
				BackgroundColor = UIColor.Gray,
			};
			ScrollView.Add(linkImageView3);

			StatusView4 = new ProjectStatusScreenSubView ("Testing", "Incompleted");
			ScrollView.Add (StatusView4);

			linkImageView4 = new UIView () {
				BackgroundColor = UIColor.Gray,
			};
			ScrollView.Add(linkImageView4);

			StatusView5 = new ProjectStatusScreenSubView ("Launch", "Incompleted");
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
			//Frame=new RectangleF (0,0, (float)UIScreen.MainScreen.Bounds.Width, (float)UIScreen.MainScreen.Bounds.Height);
			//***************************************************************************************************************************************************
			titleLabel.Frame = new RectangleF (5f, 8, (float)UIScreen.MainScreen.Bounds.Width-10f, 30.0f);
			titleLabel.Font = UIFont.SystemFontOfSize (18.0f);
			ScrollView.Frame= new RectangleF (0f,38f, (float)UIScreen.MainScreen.Bounds.Width, (float)Frame.Height-38.0f);
			ScrollView.ContentSize = new CGSize ((float)UIScreen.MainScreen.Bounds.Width,720.0f);
			StatusView1.Frame=new RectangleF (0, 0, (float)UIScreen.MainScreen.Bounds.Width, 100.0f);
			StatusView1.position ();
			linkImageView1.Frame=new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2.0f,100.0f , 4.0f, 50.0f);
			StatusView2.Frame=new RectangleF (0, 150.0f, (float)UIScreen.MainScreen.Bounds.Width, 100.0f);
			StatusView2.position ();
			linkImageView2.Frame=new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2.0f,250f , 4.0f, 50.0f);
			StatusView3.Frame=new RectangleF (0, 300.0f, (float)UIScreen.MainScreen.Bounds.Width, 100.0f);
			StatusView3.position ();
			linkImageView3.Frame=new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2.0f,400f , 4.0f, 50.0f);
			StatusView4.Frame=new RectangleF (0, 450f, (float)UIScreen.MainScreen.Bounds.Width, 100.0f);
			StatusView4.position ();
			linkImageView4.Frame=new RectangleF ((float)UIScreen.MainScreen.Bounds.Width/2-2.0f,550f , 4.0f, 50.0f);
			StatusView5.Frame=new RectangleF (0, 600, (float)UIScreen.MainScreen.Bounds.Width, 100.0f);
			StatusView5.position ();
		}
	}

	public class ProjectStatusScreenSubView:UIView
	{
		public UIImageView PhaseImageView{ get; set;}
		public UILabel PhaseNameStatusLabel{ get; set;}
		public UIImageView CheckImageView{ get; set;}
		public UIView ResultBorderView{ get; set;}
		public UILabel ResultLabelView{ get; set;}

		public ProjectStatusScreenSubView(string phase,string status){
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
				Image=new UIImage("Cut_Images/Facebook_Icon.png"),
			};
			Add (CheckImageView);

			ResultBorderView = new UIView () {
				BackgroundColor = UIColor.Gray,
			};
			Add (ResultBorderView);

			ResultLabelView= new UILabel () {
				BackgroundColor =UIColor.Black,
				TextColor=UIColor.White,
				TextAlignment=UITextAlignment.Center,
			};
			Add (ResultLabelView);

			if (phase == "Discovery") {
				if (status == "Completed") {
					PhaseImageView.Image = new UIImage ("Cut_Images/project_check.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = false;
					ResultLabelView.Text = "Project Scope";
				} else {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase1_grey.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = true;
					ResultLabelView.Text = "Project Scope";
				}		
			} else if (phase == "Design") {
				if (status == "Completed") {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase2_blue.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = false;
					ResultLabelView.Text = "Design Approval";
				} else {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase2_grey.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = true;
					ResultLabelView.Text = "Design Approval";
				}
			}else if (phase == "Developement") {
				if (status == "Completed") {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase3_blue.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = false;
					ResultLabelView.Text = "View TestLink";
				} else {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase3_grey.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = true;
					ResultLabelView.Text = "View TestLink";
				}
			}else if (phase == "Testing") {
				if (status == "Completed") {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase4_blue.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = false;
					ResultLabelView.Text = "Text for Testing";
				} else {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase4_grey.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = true;
					ResultLabelView.Text = "Text for Testing";
				}
			}else if (phase == "Launch") {
				if (status == "Completed") {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase5_blue.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = false;
					ResultLabelView.Text = "Text for Completed";
				} else {
					PhaseImageView.Image = new UIImage ("Cut_Images/phase5_grey.png");
					PhaseNameStatusLabel.Text=phase+"."+status;
					CheckImageView.Hidden = true;
					ResultLabelView.Text = "Text for Completed";
				}
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
			PhaseImageView.Frame=new RectangleF(0f*(float)Frame.Width,0f*(float)Frame.Height,1.0f*(float)Frame.Width,0.5f*(float)Frame.Height);
			PhaseNameStatusLabel.Frame=new RectangleF(0.5f*(float)Frame.Width-(float)PhaseNameStatusLabel.IntrinsicContentSize.Width/2.0f,0.5f*(float)Frame.Height,
				(float)PhaseNameStatusLabel.IntrinsicContentSize.Width,0.25f*(float)Frame.Height);
			PhaseNameStatusLabel.Font=UIFont.SystemFontOfSize (8.0f);
			CheckImageView.Frame=new RectangleF((float)PhaseNameStatusLabel.Frame.X+(float)PhaseNameStatusLabel.Frame.Width,0.575f*(float)Frame.Height,
				0.10f*(float)Frame.Height,0.10f*(float)Frame.Height);
			ResultLabelView.Frame=new RectangleF(0.5f*(float)Frame.Width-(float)ResultLabelView.IntrinsicContentSize.Width/2.0f-5f,0.75f*(float)Frame.Height,
				(float)ResultLabelView.IntrinsicContentSize.Width+10f,0.25f*(float)Frame.Height);
			ResultLabelView.Font=UIFont.SystemFontOfSize (12.0f);
			ResultBorderView.Frame=new RectangleF((float)ResultLabelView.Frame.X-1f,(float)ResultLabelView.Frame.Y-1f,
				(float)ResultLabelView.Frame.Width+2f,(float)ResultLabelView.Frame.Height+2f);
		}	
	}	
}

