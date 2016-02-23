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
			cell.projectNameLabel.Text += " "+ProjectMainController.projectList [indexPath.Row].status;
			switch (ProjectMainController.projectList [indexPath.Row].status) {
				default:
					cell.projectStatusImageView.Image = new UIImage ("project_graphics/design.png");
					break;
			}
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f; 
		}
	}
}

