using System;
using UIKit;
using Foundation;

namespace WebApp_iOS
{
	public class EmailBlastTableSource : UITableViewSource
	{
		Blast[] tableEmailBlasts;
		string cellIdentifier = "EmailBlastTableCell";
		private UIViewController parent;


		public EmailBlastTableSource (Blast[] items, UIViewController Parent)
		{
			tableEmailBlasts = items;
			parent = Parent; 
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableEmailBlasts.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (cellIdentifier) as CustomArticleCell;
			if (cell == null)
				cell = new CustomArticleCell ((Foundation.NSString) cellIdentifier);

			try{
//			cell.UpdateCell (tableEmailBlasts[indexPath.Row].Title
//					, GlobalAPI.Manager ().ImageFromUrl (tableEmailBlasts[indexPath.Row].thumbnailUrl).Result);

				cell.UpdateCell (tableEmailBlasts[indexPath.Row].Title
					, tableEmailBlasts[indexPath.Row].thumbnailUrl);
			}catch(Exception e){

			}

			cell.BackgroundColor = UIColor.Clear;
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			
			//Go to article detail page 
			 
			GlobalAPI.Manager().PushPage(parent.NavigationController,GlobalAPI.Manager().getInternetPage(tableEmailBlasts [indexPath.Row].Link));

			tableView.DeselectRow (indexPath, true); // normal iOS behaviour is to remove the blue highlight
		}


	}
}

