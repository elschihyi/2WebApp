using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace WebApp_iOS
{
	public class ProjectCell : UITableViewCell
	{
		UILabel headingLabel;
		UIImageView image;

		public ProjectCell (String cellId) : base (UITableViewCellStyle.Default, cellId)
		{

			BackgroundColor = UIColor.Clear; 


			headingLabel = new UILabel () {
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear,
				TextAlignment = UITextAlignment.Center
			};

			image = new UIImageView (); 
	
			ContentView.AddSubviews(new UIView[] {headingLabel, image});

		}

		public void UpdateCell (string title, string status)
		{
			headingLabel.Text = title;

			if (status == "Design")
				image.Image = new UIImage ("project_graphics/design.png"); 
			else if(status == "Development")
				image.Image = new UIImage ("project_graphics/developement.png");
			else if(status == "Launch")
				image.Image = new UIImage ("project_graphics/launch.png");
			else if(status == "Discovery")
				image.Image = new UIImage ("project_graphics/project_discovery.png");
			else 
				image.Image = new UIImage ("project_graphics/testing.png");
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			headingLabel.Frame = new CGRect (15, 7, ContentView.Bounds.Width, 25);
			image.Frame = new CGRect (15, 35, ContentView.Bounds.Width - 30, 50); 
		}
	}
}

