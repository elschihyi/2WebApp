using System;
using UIKit;
using System.Collections.Generic;
using System.Drawing;
using Foundation;

namespace WebApp_iOS
{
	public class PushNotificationSource: UITableViewSource
	{
		PushNotificationController pushNotificationController;
		List<bool> PushNotificationList;

		public PushNotificationSource (PushNotificationController pushNotificationController)
		{
			this.pushNotificationController = pushNotificationController;
			PushNotificationList = pushNotificationController.PushNotificationList;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 3;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			switch (section) {
			case 0:
				return 2;
			case 1:
				return 3;
			case 2:
				return 3;
			default:
				return 0;
			}
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return "";
		}

		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			UIView HeaderView;
			UILabel HeaderLabel;
			UIView BlueLineView;

			HeaderView = new UIView () {
				BackgroundColor=UIColor.FromRGB(35,40,46),
				Frame=new RectangleF (0f, 0f, (float)UIScreen.MainScreen.Bounds.Width,44f),
			};

			HeaderLabel = new UILabel () {
				Font = UIFont.BoldSystemFontOfSize (16f),
				TextColor = UIColor.FromRGB(0,172,237),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor=UIColor.Clear,
				Frame = new RectangleF (0f, 0f, (float)UIScreen.MainScreen.Bounds.Width,(float)HeaderView.Frame.Height),
			};
			switch (section) {
			case 0:
				HeaderLabel.Text="2 Web Notifications";
				break;
			case 1:
				HeaderLabel.Text="Project Notifications";
				break;
			case 2:
				HeaderLabel.Text="Support Notifications";
				break;
			default:
				HeaderLabel.Text="";
				break;
			}
			HeaderView.Add (HeaderLabel);

			BlueLineView = new UIView () {
				BackgroundColor = UIColor.FromRGB (0, 172, 237),
				Frame = new RectangleF (0f, 42f, (float)UIScreen.MainScreen.Bounds.Width, 2f),
			};
			HeaderView.Add (BlueLineView);
			return HeaderView;
		}

		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			return 44f;
		}

		public override string TitleForFooter (UITableView tableView, nint section)
		{
			return "";
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (PushNotificationCell.Key) as PushNotificationCell;
			if (cell == null) {
				cell = new PushNotificationCell ();
				cell.switchBtn.ValueChanged+= (s, e) => {
					pushNotificationController.SwitchValueChanges(cell.Section,cell.Row);
				};
			}
			cell.Section = indexPath.Section;
			cell.Row = indexPath.Row;
			switch (indexPath.Section) {
			case 0:
				switch (indexPath.Row) {
				case 0:
					cell.nameLabel.Text = "On new event";
					cell.switchBtn.On = PushNotificationList [0];
					break;
				case 1:
					cell.nameLabel.Text = "On new update";
					cell.switchBtn.On = PushNotificationList [1];
					break;
				default:
					cell.nameLabel.Text = "";
					break;
				}
				break;
			case 1:
				switch (indexPath.Row) {
				case 0:
					cell.nameLabel.Text = "On project update";
					cell.switchBtn.On = PushNotificationList [2];
					break;
				case 1:
					cell.nameLabel.Text = "On design approved document";
					cell.switchBtn.On = PushNotificationList [3];
					break;
				case 2:
					cell.nameLabel.Text = "On website release document";
					cell.switchBtn.On = PushNotificationList [4];
					break;
				default:
					cell.nameLabel.Text = "";
					break;
				}
				break;
			case 2:
				switch (indexPath.Row) {
				case 0:
					cell.nameLabel.Text = "For support updates";
					cell.switchBtn.On = PushNotificationList [5];
					break;
				case 1:
					cell.nameLabel.Text = "For website audit";
					cell.switchBtn.On = PushNotificationList [6];
					break;
				case 2:
					cell.nameLabel.Text = "For yearly analysis";
					cell.switchBtn.On = PushNotificationList [7];
					break;
				default:
					cell.nameLabel.Text = "";
					break;
				}
				break;
			default:
				cell.nameLabel.Text="";
				break;
			}
			return cell;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 66f;
		}
	}
}

