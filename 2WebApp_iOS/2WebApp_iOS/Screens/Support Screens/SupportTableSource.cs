using System;
using UIKit;
using Foundation;

namespace WebApp_iOS
{
	public class SupportTableSource : UITableViewSource
	{
		Support[] tableItems;
		String cellIdentifier = "tablecell";

		private UINavigationController nav; 

		public SupportTableSource (Support[] projects, UINavigationController navCon)
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
			//			cell.TextLabel.Text = tableItems[indexPath.Row].SupportTitle;
			//
			//			return cell;



			var cell = tableView.DequeueReusableCell (cellIdentifier) as SupportCell;
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new SupportCell (cellIdentifier);

			cell.UpdateCell ( tableItems[indexPath.Row].SupportTitle);
			//cell.Accessory = UITableViewCellAccessory.DisclosureIndicator; 

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//new UIAlertView("Row Selected", tableItems[indexPath.Row].SupportTitle, null, "OK", null).Show();

			tableView.DeselectRow (indexPath, true); // normal iOS behaviour is to remove the blue highlight

			//var page = story.InstantiateViewController("detail") as SupportDetail; 
			//nav.PushViewController(page,true);

			 
			GlobalAPI.Manager().PushPage(nav,new SupportDetail(tableItems[indexPath.Row]));

		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return tableItems.Length + " Supports";
		}

		public Support GetItem(int id) {
			return tableItems[id];
		}
	}
}

