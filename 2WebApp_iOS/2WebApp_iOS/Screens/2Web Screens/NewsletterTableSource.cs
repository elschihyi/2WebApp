using System;
using UIKit;
using Foundation;

namespace WebApp_iOS
{
	public class NewsletterTableSource : UITableViewSource
	{
		NewsLetter[] tableNewsletters;
		string cellIdentifier = "NewsletterTableCell";
		private UIViewController parent;


		public NewsletterTableSource (NewsLetter[] items, UIViewController Parent)
		{
			tableNewsletters = items;
			parent = Parent;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableNewsletters.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (cellIdentifier) as CustomArticleCell;
			if (cell == null)
				cell = new CustomArticleCell ((Foundation.NSString) cellIdentifier);
			
			try{
//			cell.UpdateCell (tableNewsletters[indexPath.Row].NewsletterTitle
//					, GlobalAPI.Manager ().ImageFromUrl (tableNewsletters[indexPath.Row].ImageUrl).Result );

				cell.UpdateCell (tableNewsletters[indexPath.Row].NewsletterTitle
					, tableNewsletters[indexPath.Row].ImageUrl);
			}catch{

			}

			cell.BackgroundColor = UIColor.Clear;
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			
			//Go to article detail page


			//GlobalAPI.Manager().PushPage(parent.NavigationController,GlobalAPI.Manager().getInternetPage(tableNewsletters [indexPath.Row].Url));
			GlobalAPI.Manager().PushPage(parent.NavigationController,new WebViewController(tableNewsletters [indexPath.Row].Url,""));
			tableView.DeselectRow (indexPath, true); // normal iOS behaviour is to remove the blue highlight
		}
	}
}

