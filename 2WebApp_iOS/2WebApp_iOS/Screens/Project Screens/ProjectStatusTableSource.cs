using System;
using UIKit;
using Foundation;

namespace WebApp_iOS
{
	public class ProjectStatusTableSource : UITableViewSource
	{
		ProjectStatus[] tableItems;
		String cellIdentifier = "tablecell";

		private UIViewController nav; 

		public ProjectStatusTableSource (ProjectStatus[] projects, UIViewController parent)
		{
			tableItems = projects;
			nav = parent; 
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



			var cell = tableView.DequeueReusableCell (cellIdentifier) as ProjectStatusCell;
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new ProjectStatusCell (cellIdentifier);

			cell.UpdateCell ( tableItems[indexPath.Row].Title,  tableItems[indexPath.Row].Status, tableItems[indexPath.Row].Completed);
			//cell.Accessory = UITableViewCellAccessory.DisclosureIndicator; 

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//new UIAlertView("Row Selected", tableItems[indexPath.Row].ProjectTitle, null, "OK", null).Show();

			tableView.DeselectRow (indexPath, true); // normal iOS behaviour is to remove the blue highlight

			//var page = story.InstantiateViewController("detail") as ProjectDetail; 
			//nav.PushViewController(page,true);


			//GlobalAPI.Manager().PushPage(nav,new ArticlePage(tableItems[indexPath.Row]));

		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return tableItems.Length + " Projects";
		}

		public ProjectStatus GetItem(int id) {
			return tableItems[id];
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 150; 
		}
	}
}



