
using System;

using Foundation;
using UIKit;
using CoreGraphics;
using MessageUI;

namespace WebApp_iOS
{
	public partial class ProjectDetail3 : UIViewController
	{
		Project project;
		MFMailComposeViewController mailController;

		public ProjectDetail3 (Project p) : base ("ProjectDetail3", null)
		{
			project = p; 
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.


			//populate projectUpdates
			tbvUpdates.Source = new ProjectUpdateTableSource(GlobalAPI.Manager().LoadProjectUpdates(1),this);
			tbvUpdates.BackgroundColor = UIColor.Clear;
			tbvUpdates.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			//populate projectStatus
			tbvProjectStatus.Source = new ProjectStatusTableSource(GlobalAPI.Manager().LoadProjectStatus(project.Status),this); 
			tbvProjectStatus.BackgroundColor = UIColor.Clear; 
			tbvProjectStatus.SeparatorStyle = UITableViewCellSeparatorStyle.None; 


			//populate Project Overview scroll page
			OverViewScrl.Frame = new CoreGraphics.CGRect(0,0,View.Bounds.Width, View.Bounds.Height - 40);
			OverViewScrl.ContentSize = OverViewVw.Frame.Size; 
			OverViewScrl.AddSubview(OverViewVw);

			if (MFMailComposeViewController.CanSendMail) {
				btnMakeMeeting.TouchUpInside += (object sender, EventArgs e) => {

					mailController = new MFMailComposeViewController (); 

					mailController.SetToRecipients (new string[]{ "support@2webdesign.com" }); 
					mailController.SetSubject (""); 
					mailController.SetMessageBody ("", false); 

					mailController.Finished += (object s, MFComposeResultEventArgs args) => {
						args.Controller.DismissViewController (true, null);
					};

					PresentViewController (mailController, true, null); 

				};
			} else {
				//device can't make emails
				btnMakeMeeting.Hidden = true;
			}


			//load data
			lblCompany.Text = project.Company;
			lblProjectType.Text = project.Type;
			lblPrimaryContact.Text = project.PrimaryContact;
			lblWebContact.Text = project.StaffContact; 

			if (MFMailComposeViewController.CanSendMail) {
				btnContactSupport.TouchUpInside += (object sender, EventArgs e) => {

					mailController = new MFMailComposeViewController (); 

					mailController.SetToRecipients (new string[]{ "support@2webdesign.com" }); 
					mailController.SetSubject (""); 
					mailController.SetMessageBody ("", false); 

					mailController.Finished += (object s, MFComposeResultEventArgs args) => {
						args.Controller.DismissViewController (true, null);
					};

					PresentViewController (mailController, true, null); 

				};
			} else {
				//device can't make emails
				btnContactSupport.Hidden = true;
			}

			//populate Project Support scroll apge
			SupportScrl.Frame = new CoreGraphics.CGRect(0,0,View.Bounds.Width, View.Bounds.Height - 40); 
			SupportScrl.ContentSize = SupportVw.Frame.Size;
			SupportScrl.AddSubview (SupportVw); 

			//load data
			lblSupportType.Text = project.SupportType; 
			lblSupportHours.Text = project.SupportHours;
			lblSupportBackUp.Text = project.SupportLatestBackUp;
			lblLastRestored.Text = project.SupportLastRestored; 
			lblSystemStatus.Text = project.SupportStatus; 


			GlobalAPI.Manager ().PageDefault (this, "", true, true);

			//Courasal Pages
			var pages = new UIView[]{ Updates, Status, OverView, Support }; 

			ScrollView.Frame = new CGRect (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 70); 
			PageControl.Frame = new CGRect (0, UIScreen.MainScreen.Bounds.Height - 55, UIScreen.MainScreen.Bounds.Width, 40);

			int i;	

			for (i = 0; i < pages.Length; i++) {
				//UIView view = new UIView (); 
				CoreGraphics.CGRect frame = new CoreGraphics.CGRect (); 

				frame.X = (this.ScrollView.Frame.Width * i);
				frame.Y = this.ScrollView.Frame.Y; 

				frame.Height = this.ScrollView.Frame.Height;
				frame.Width = this.ScrollView.Frame.Width; 

				pages [i].Frame = frame;
				this.ScrollView.AddSubview (pages [i]);
			}

			// set pages and content size
			PageControl.Pages = i;
			ScrollView.ContentSize = new CoreGraphics.CGSize (ScrollView.Frame.Width * i, ScrollView.Frame.Height - 70);

			tbvUpdates.Frame = new CGRect (0, 40, ScrollView.Frame.Width, ScrollView.Frame.Height - 80); 
			tbvProjectStatus.Frame = new CGRect (0, 40, ScrollView.Frame.Width, ScrollView.Frame.Height - 100); 

			ScrollView.Scrolled += ScrollEvent; 


		}

		//For the courasel pages
		private void ScrollEvent (object sender, EventArgs e)
		{
			PageControl.CurrentPage = 
				(int)System.Math.Floor (ScrollView.ContentOffset.X
					/ this.ScrollView.Frame.Size.Width);
		}

	}
}


