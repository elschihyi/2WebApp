using System;
using UIKit;
using Foundation;

namespace WebApp_iOS
{
	public class ProjectMainScreenScource: UITableViewSource
	{
		ProjectMainController ProjectMainController;
		public ProjectMainScreenScource (ProjectMainController ProjectMainController)
		{
			this.ProjectMainController=ProjectMainController;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return ProjectMainController.projectList.Count;
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return "";
		}

		public override string TitleForFooter (UITableView tableView, nint section)
		{
			return "";
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (ProjectMainScreenCell.Key) as ProjectMainScreenCell;
			if (cell == null)
				cell = new ProjectMainScreenCell ();

			cell.projectNameLabel.Text = ProjectMainController.projectList [indexPath.Row].name;
			//cell.projectNameLabel.Text += " "+ProjectMainController.projectList [indexPath.Row].status;
			switch (ProjectMainController.projectList [indexPath.Row].status) {
			case "Discovery":
				cell.projectStatusImageView.Image = new UIImage ("project_graphics/project_discovery.png");
				break;
			case "Design":
				cell.projectStatusImageView.Image = new UIImage ("project_graphics/design.png");
				break;
			case "Development":
				cell.projectStatusImageView.Image = new UIImage ("project_graphics/developement.png");
				break;
			case "Testing":
				cell.projectStatusImageView.Image = new UIImage ("project_graphics/testing.png");
				break;
			case "Launch":
				cell.projectStatusImageView.Image = new UIImage ("project_graphics/launch.png");
				break;
			default:
				cell.projectStatusImageView.Image = new UIImage ();
				break;
			}
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			
			GlobalAPI.Manager().PushPage(ProjectMainController.NavigationController,
				new ProjectPageController(ProjectMainController.projectList[indexPath.Row]));
			/*
			GlobalAPI.Manager().PushPage(ProjectMainController.NavigationController,
				new ProjectUpdateScreenController(ProjectMainController.projectList[indexPath.Row]));
			*/	
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f; 
		}
	}
}

