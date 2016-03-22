using System;
using UIKit;
using CoreDataService;
using System.Collections.Generic;
using Foundation;

namespace WebApp_iOS
{
	public class ProjectSettingsSource: UITableViewSource
	{
		List<userproj> theProjectList;
		ProjectSettingsController projectSettingsController;

		public ProjectSettingsSource (ProjectSettingsController projectSettingsController)
		{
			this.projectSettingsController = projectSettingsController;
			this.theProjectList = projectSettingsController.theProjectList;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return theProjectList.Count+1;
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
			if (indexPath.Row < theProjectList.Count) {
				var cell = tableView.DequeueReusableCell (ProjectSettingsCell.Key) as ProjectSettingsCell;
				if (cell == null) {
					cell = new ProjectSettingsCell ();
					cell.RequestBtn.TouchUpInside += (s, e) => {
						projectSettingsController.RequestBtnClick (cell.Row);
					};
				}
				cell.Row = indexPath.Row;
				cell.nameLabel.Text = theProjectList [indexPath.Row].name;
				return cell;
			} else {
				var cell = tableView.DequeueReusableCell (RequestProjectCell.Key) as RequestProjectCell;
				if (cell == null) {
					cell = new RequestProjectCell ();
					cell.RequestBtn.TouchUpInside += (s, e) => {
						projectSettingsController.RequestBtn2Click (cell.Row);
					};
				}
				cell.Row = indexPath.Row;
				return cell;
			}	
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 66f;
		}
	}
}

