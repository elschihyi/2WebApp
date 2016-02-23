using System;
using UIKit;
using Foundation;

namespace WebApp_iOS
{
	public class EventsTableSource : UITableViewSource
	{
		Event[] tableEvents;
		string cellIdentifier = "EventsTableCell";
		private UIViewController parent;
		ArticlePage articlePage; 

		public EventsTableSource (Event[] items, UIViewController Parent)
		{
			tableEvents = items;
			parent = Parent; 
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableEvents.Length;
		}
		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{

			var cell = tableView.DequeueReusableCell (cellIdentifier) as CustomEventCell;
			if (cell == null)
				cell = new CustomEventCell ((Foundation.NSString) cellIdentifier);

			try{
				String[] split = tableEvents [indexPath.Row].EventTitle.Split ('|'); 
				cell.UpdateCell (split[0], split[1]);
			}catch(Exception e){
				cell.UpdateCell ("NA",tableEvents [indexPath.Row].EventTitle); 
			}

			cell.BackgroundColor = UIColor.Clear;
			//cell.Layer.BorderColor = UIColor.FromRGB(0,213,255).CGColor; 
			//cell.Layer.BorderWidth = 0.3f; 
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			
			//GlobalAPI.Manager().PushPage(parent.NavigationController,GlobalAPI.Manager().getInternetPage(tableEvents[indexPath.Row].EventLink));
			GlobalAPI.Manager().PushPage(parent.NavigationController,new WebViewController(tableEvents[indexPath.Row].EventLink,""));
			tableView.DeselectRow (indexPath, true); // normal iOS behaviour is to remove the blue highlight
		}


	}
}

