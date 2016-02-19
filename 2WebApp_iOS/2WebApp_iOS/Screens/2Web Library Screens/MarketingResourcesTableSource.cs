using System;
using UIKit;
using Foundation;

namespace WebApp_iOS
{
	
	public class MarketingResourcesTableSource : UITableViewSource
	{
		MarketResource[] tableMarketResources;
		private UIViewController parent;
		string cellIdentifier = "MarketResourceCell";


		public MarketingResourcesTableSource (MarketResource[] items, UIViewController Parent)
		{
			tableMarketResources = items;
			parent = Parent; 
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableMarketResources.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (cellIdentifier) as CustomArticleCell;
			if (cell == null)
				cell = new CustomArticleCell ((Foundation.NSString) cellIdentifier);

			try{
//			cell.UpdateCell (tableMarketResources[indexPath.Row].MarketResourceTitle
//					, GlobalAPI.Manager ().ImageFromUrl (tableMarketResources[indexPath.Row].ImageUrl).Result);

				cell.UpdateCell (tableMarketResources[indexPath.Row].MarketResourceTitle
					, tableMarketResources[indexPath.Row].ImageUrl);
			}catch(Exception e){

			}

			cell.BackgroundColor = UIColor.Clear;
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//Go to article detail page 
			 
			GlobalAPI.Manager().PushPage(parent.NavigationController,GlobalAPI.Manager().getInternetPage(tableMarketResources [indexPath.Row].Url));

			tableView.DeselectRow (indexPath, true); // normal iOS behaviour is to remove the blue highlight

		}


	}

}

