using System;
using UIKit;
using Foundation;

namespace WebApp_iOS
{
	public class ProjectsTableSource : UITableViewSource
	{
		Project[] tableItems;
		String cellIdentifier = "tablecell";

		private UINavigationController nav; 

		public ProjectsTableSource (Project[] projects, UINavigationController navCon)
		{
			tableItems = projects;
			nav = navCon; 
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableItems.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
//			// in a Storyboard, Dequeue will ALWAYS return a cell, 
//			var cell = tableView.DequeueReusableCell (cellIdentifier);
//			// now set the properties as normal
//			cell.TextLabel.Text = tableItems[indexPath.Row].ProjectTitle;
//
//			return cell;



			var cell = tableView.DequeueReusableCell (cellIdentifier) as ProjectCell;
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new ProjectCell (cellIdentifier);

			cell.UpdateCell ( tableItems[indexPath.Row].Title,  tableItems[indexPath.Row].Status);
			//cell.Accessory = UITableViewCellAccessory.DisclosureIndicator; 

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//new UIAlertView("Row Selected", tableItems[indexPath.Row].ProjectTitle, null, "OK", null).Show();

			tableView.DeselectRow (indexPath, true); // normal iOS behaviour is to remove the blue highlight

			//var page = story.InstantiateViewController("detail") as ProjectDetail; 
			//nav.PushViewController(page,true);

			GlobalAPI.Manager().PushPage(nav,new ProjectDetail3(tableItems[indexPath.Row]));


		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return tableItems.Length + " Projects";
		}

		public Project GetItem(int id) {
			return tableItems[id];
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 80; 
		}
	}
}

