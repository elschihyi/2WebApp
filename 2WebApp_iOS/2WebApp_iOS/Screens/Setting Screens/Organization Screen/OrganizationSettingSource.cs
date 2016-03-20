using System;
using System.Collections.Generic;
using UIKit;
using Foundation;

namespace WebApp_iOS
{
	public class OrganizationSettingSource: UITableViewSource
	{
		List<string> theOrganizationList;
		OrganizationSettingController organizationSettingsController;

		public OrganizationSettingSource (OrganizationSettingController organizationSettingsController)
		{
			this.organizationSettingsController = organizationSettingsController;
			theOrganizationList = organizationSettingsController.theOrganizationList;
		}
		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return theOrganizationList.Count+1;
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
			if (indexPath.Row < theOrganizationList.Count) {
				var cell = tableView.DequeueReusableCell (OrganizationSettingCell.Key) as OrganizationSettingCell;
				if (cell == null) {
					cell = new OrganizationSettingCell ();
					cell.RequestBtn.TouchUpInside += (s, e) => {
						organizationSettingsController.RequestBtnClick (cell.Row);
					};
				}
				cell.Row = indexPath.Row;
				cell.nameLabel.Text = theOrganizationList [indexPath.Row];
				return cell;
			} else {
				var cell = tableView.DequeueReusableCell (RequestOrganizationCell.Key) as RequestOrganizationCell;
				if (cell == null) {
					cell = new RequestOrganizationCell ();
					cell.RequestBtn.TouchUpInside += (s, e) => {
						organizationSettingsController.RequestBtn2Click (cell.Row);
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

