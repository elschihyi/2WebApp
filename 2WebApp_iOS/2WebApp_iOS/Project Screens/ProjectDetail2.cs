
using System;

using Foundation;
using UIKit;

namespace WebApp_iOS
{
	public partial class ProjectDetail2 : UIViewController
	{
		private string projectTitle; 
		private float projectProgress; 
		private string projectTeamLead;
		private string projectPrimaryContact;
		private string projectStage;
		private DateTime projectLastPost ;
		private int projectNumberOfUpdates;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public ProjectDetail2 (Project project)
			: base (UserInterfaceIdiomIsPhone ? "ProjectDetail2_iPhone" : "ProjectDetail2_iPad", null)
		{
			projectTitle = project.Title; 
			projectProgress = project.Progress; 

			projectTeamLead = project.TeamLead; 
			projectPrimaryContact = project.PrimaryContact; 

			projectStage = project.Stage;
			projectLastPost = project.LastPost;
			projectNumberOfUpdates = project.NumberOfUpdates; 
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
			GlobalAPI.Manager ().PageDefault (this, "",false); 


			Title.Text = projectTitle; 
			Progress.Progress = projectProgress; 

			Teamlead.Text = projectTeamLead;
			PrimaryContact.Text = projectPrimaryContact;

			Stage.Text = projectStage;
			LastPost.Text = projectLastPost.ToShortDateString();
			UpdateCount.Text = projectNumberOfUpdates.ToString();

		}
	}
}

