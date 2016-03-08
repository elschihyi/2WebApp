using System;
using UIKit;
using Foundation;
using CoreDataService;

namespace WebApp_iOS
{
	public class ProjectUpdateScreenSource: UITableViewSource
	{
		projectsummary theProject;
		ProjectUpdateScreenController ProjectUpdateScreenController;

		public ProjectUpdateScreenSource (ProjectUpdateScreenController ProjectUpdateScreenController)
		{
			this.ProjectUpdateScreenController = ProjectUpdateScreenController;
			this.theProject = ProjectUpdateScreenController.theProject;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			if (theProject.tasks != null) {
				return theProject.tasks.Count;
			} else {
				return 0;
			}	
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
			var cell = tableView.DequeueReusableCell (ProjectUpdateScreenCell.Key) as ProjectUpdateScreenCell;
			if (cell == null) {
				cell = new ProjectUpdateScreenCell ();
				cell.viewBtn.TouchUpInside += (s, e) => {
					ProjectUpdateScreenController.CellViewClick(indexPath.Row);
				};
			}
			cell.nameLabel.Text = theProject.tasks [indexPath.Row].name;
			if (String.IsNullOrEmpty (theProject.tasks [indexPath.Row].date)) {
				cell.dateLabel.Text = "";
				//cell.dateLabel.Text = DateTime.Now.ToString ("MMMM dd,yyyy")+"(fake)";
			} else {
				DateTime theDate = DateTime.ParseExact (theProject.tasks [indexPath.Row].date, "yyyy-MM-dd HH:mm:ss", null);	
				cell.dateLabel.Text = theDate.ToString("MMMM dd, yyyy");
			}
			cell.Row = indexPath.Row;
			return cell;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 88.0f;
		}
	}
}

