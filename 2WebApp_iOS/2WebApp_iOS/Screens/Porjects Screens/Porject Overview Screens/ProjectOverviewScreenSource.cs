using System;
using UIKit;
using CoreDataService;
using Foundation;

namespace WebApp_iOS
{
	public class ProjectOverviewScreenSource: UITableViewSource
	{
		projectsummary theProject;
		PorjectOverviewScreenController PorjectOverviewScreenController;

		public ProjectOverviewScreenSource (PorjectOverviewScreenController PorjectOverviewScreenController)
		{
			this.PorjectOverviewScreenController = PorjectOverviewScreenController;
			this.theProject = PorjectOverviewScreenController.theProject;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return 4;
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
			var cell = tableView.DequeueReusableCell (ProjectOverviewScreenCell.Key) as ProjectOverviewScreenCell;
			if (cell == null)
				cell = new ProjectOverviewScreenCell ();

			switch (indexPath.Row) {
				case 0:
					cell.TitleLabel.Text = "Company";
					cell.ValueLabel.Text = theProject.org_name;
					cell.Row = 0;
					break;
				case 1:
					cell.TitleLabel.Text = "Project Type";
					cell.ValueLabel.Text = theProject.type;
					cell.Row = 1;
					break;
				case 2:
					cell.TitleLabel.Text = "Primary Contact";
					cell.ValueLabel.Text = theProject.client_name;
					cell.Row = 2;
					break;
				case 3:
					cell.TitleLabel.Text = "2 Web Contact";
					cell.ValueLabel.Text = theProject.staff_name;
					cell.Row = 3;
					break;
				default:
					cell.TitleLabel.Text = "";
					cell.ValueLabel.Text = "";
					cell.Row = -1;
					break;	
			}
			return cell;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 88f;
		}
	}
}

