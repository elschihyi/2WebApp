using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace WebApp_iOS
{
	public class ProjectStatusCell : UITableViewCell
	{
		UILabel headingLabel;
		UILabel descLabel;
		UIImageView image;
		UIImageView checkMark;

		public ProjectStatusCell (String cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			
			BackgroundColor = UIColor.Clear; 

			headingLabel = new UILabel () {
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear,
				TextAlignment = UITextAlignment.Center,
				Font = UIFont.SystemFontOfSize(15),
			};

			descLabel = new UILabel () {
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear,
				TextAlignment = UITextAlignment.Center,
				Font = UIFont.SystemFontOfSize(7),
			};

			image = new UIImageView ();

			checkMark = new UIImageView (); 

			ContentView.AddSubviews(new UIView[] {headingLabel, descLabel, image, checkMark});

		}

		public void UpdateCell (string title, string status, bool check)
		{
			headingLabel.Text = title;
			var tmp = "";

			if (status == "Design") {
				if (check)
					image.Image = new UIImage ("Cut_Images/Design_Big_blue_Icon.png");
				else
					image.Image = new UIImage ("Cut_Images/Design_Big_Grey_Icon.png"); 

				tmp = "Design";
			} else if (status == "Development") {
				if (check)
					image.Image = new UIImage ("Cut_Images/Development_Big_Blue_Icon.png");
				else
					image.Image = new UIImage ("Cut_Images/Development_Big_Grey_Icon.png"); 
				
				tmp = "Development";
			} else if (status == "Launch") {
				if (check)
					image.Image = new UIImage ("Cut_Images/Launch_Big_Blue_Icon.png");
				else
					image.Image = new UIImage ("Cut_Images/Launch_Big_Grey_Icon.png"); 
				
			} else if (status == "Discovery") {
				if (check)
					image.Image = new UIImage ("Cut_Images/Project_Discovery_Big_Blue_Icon.png");
				else
					image.Image = new UIImage ("Cut_Images/Project_Discovery_Big_Grey_Icon.png");
				
				tmp = "Project Discovery";
			} else {
				if (check)
					image.Image = new UIImage ("Cut_Images/Testing_Big_Blue_Icon.png");
				else
					image.Image = new UIImage ("Cut_Images/Testing_Big_Grey_Icon.png");
				
				tmp = "Testing and Training";
			}

			if (check) {
				checkMark.Image = new UIImage ("Cut_Images/Tick_Icon.png");
				tmp += " - Completed";
			} else
				checkMark.Image = null; 

			descLabel.Text = tmp; 
			
		} 

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			headingLabel.Frame = new CGRect (0, 95, (ContentView.Bounds.Width), 60);
			descLabel.Frame = new CGRect (0, 85, (ContentView.Bounds.Width), 40); 
			image.Frame = new CGRect (ContentView.Bounds.Width / 2 - 45, 5, 90, 90); 
			checkMark.Frame = new CGRect (ContentView.Bounds.Width * 0.70, 100, 10, 10); 
		}
	}
}



