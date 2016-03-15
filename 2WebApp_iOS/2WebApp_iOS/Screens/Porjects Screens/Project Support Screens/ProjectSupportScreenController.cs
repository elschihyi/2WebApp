using System;
using UIKit;
using CoreDataService;
using System.Drawing;
using MessageUI;

namespace WebApp_iOS
{
	public enum FromScreenToSupport{Project,Support};

	public class ProjectSupportScreenController: UIViewController
	{
		ProjectSupportScreenView projectSupportScreenView;

		FromScreenToSupport fromScreen;
		//object
		projectsummary theProject;

		public ProjectSupportScreenController (projectsummary theProject,FromScreenToSupport fromScreen)
		{
			this.theProject = theProject;
			this.fromScreen = fromScreen;
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
			initView ();
			if (fromScreen == FromScreenToSupport.Support) {
				GlobalAPI.Manager ().PageDefault (this, theProject.name, true, true);
			}	
		}

		/********************************************************************************
		*Views initializations
		********************************************************************************/
		public void initView(){
			var statusbar=UIApplication.SharedApplication.StatusBarFrame.Size.Height;
			var navigationbarHeight = NavigationController.NavigationBar.Frame.Size.Height;
			var y = statusbar + navigationbarHeight;
			if (fromScreen == FromScreenToSupport.Support) {
				y = 0f;
			}	
			projectSupportScreenView = 
				new ProjectSupportScreenView (theProject, new RectangleF (0f, (float)y,
					(float)UIScreen.MainScreen.Bounds.Width, (float)(UIScreen.MainScreen.Bounds.Height - y)));	
			projectSupportScreenView.titleLabel.Text="Project Support";
			projectSupportScreenView.ContactSupportBtn.TouchUpInside += (s, e) => {
				ContractSupportClick();
			};	
			projectSupportScreenView.WebSiteAuditsBtn.TouchUpInside += (s, e) => {
				WebSiteAuditsClick();
			};
			projectSupportScreenView.YearAndAnalysisBtn.TouchUpInside += (s, e) => {
				YearAndAnalysisClick();
			};
			View.Add (projectSupportScreenView);
		}

		/********************************************************************************
		*Btn clicks
		********************************************************************************/
		public void ContractSupportClick()
		{
			if (!MFMailComposeViewController.CanSendMail) {
				return;
			}	
			MFMailComposeViewController mailController = new MFMailComposeViewController();
			if (!MFMailComposeViewController.CanSendMail)
			{
				return;
			}
			contact contactInfo;
			string errmsg;
			if (!GlobalAPI.GetDataService ().ContactInfo (out contactInfo, out errmsg)) {
				return;
			}	
			mailController.SetToRecipients(new string[]{contactInfo.support_email});
			mailController.SetSubject("");
			mailController.SetMessageBody("", false);
			mailController.Finished += ( s, args) =>
			{
				args.Controller.DismissViewController(true,null);
			};
			this.PresentViewController(mailController, true, null);
		}

		public void WebSiteAuditsClick()
		{
		}


		public void YearAndAnalysisClick()
		{
		}


	}
}

