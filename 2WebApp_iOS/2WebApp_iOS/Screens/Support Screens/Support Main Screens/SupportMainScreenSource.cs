using System;
using UIKit;
using Foundation;
using CoreDataService;

namespace WebApp_iOS
{
	public class SupportMainScreenSource: UITableViewSource
	{

		SupportMainScreenController supportMainScreenController;

		public SupportMainScreenSource (SupportMainScreenController supportMainScreenController)
		{
			this.supportMainScreenController=supportMainScreenController;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return supportMainScreenController.projectList.Count;
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
			var cell = tableView.DequeueReusableCell (SupportMainScreenCell.Key) as SupportMainScreenCell;
			if (cell == null) {
				cell = new SupportMainScreenCell ();
				cell.ViewBtn.TouchUpInside += (s, e) => {
					supportMainScreenController.CellViewClick(cell.Row);
				};	
			}
			projectsummary project = supportMainScreenController.projectList [indexPath.Row];
			cell.Row = indexPath.Row;
			cell.projectNameLabel.Text = project.name;
			return cell;
		}

		/*
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{

			GlobalAPI.Manager().PushPage(ProjectMainController.NavigationController,
				new ProjectPageController(ProjectMainController.projectList[indexPath.Row]));
		}
		*/

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f; 
		}
	}
}

