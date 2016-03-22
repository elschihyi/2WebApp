using System;
using UIKit;
using Foundation;
using System.Drawing;

namespace WebApp_iOS
{
	public class SettingMenuScreenSource: UITableViewSource
	{
		SettingMenuScreenController settingMenuScreenController;

		public SettingMenuScreenSource (SettingMenuScreenController settingMenuScreenController)
		{
			this.settingMenuScreenController=settingMenuScreenController;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 3;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			switch (section) {
			case 0:
				return 1;
			case 1:
				return 2;
			case 2:
				return 2;
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
				BackgroundColor=UIColor.Clear,
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
				HeaderLabel.Text="My Account";
				break;
			case 1:
				HeaderLabel.Text="Notifications";
				break;
			case 2:
				HeaderLabel.Text="My Projects";
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
			var cell = tableView.DequeueReusableCell (SettingMenuScreenCell.Key) as SettingMenuScreenCell;
			if (cell == null)
				cell = new SettingMenuScreenCell ();

			switch (indexPath.Section) {
			case 0:
				cell.OperationsLabel.Text = "Update Profile";
				break;
			case 1:
				if (indexPath.Row == 0) {
					cell.OperationsLabel.Text = "Push Notifications";
				} else {
					cell.OperationsLabel.Text = "Email Notifications";
				}	
				break;
			case 2:
				if (indexPath.Row == 0) {
					cell.OperationsLabel.Text = "Organization Settings";
				} else {
					cell.OperationsLabel.Text = "Project Settings";
				}
				break;
			default:
				cell.OperationsLabel.Text = "";
				break;
			}
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			switch (indexPath.Section) {
			case 0:
				GlobalAPI.Manager().PushPage(settingMenuScreenController.NavigationController,new ProfileSettingController(settingMenuScreenController.theaccountsummary));
				break;
			case 1:
				if (indexPath.Row == 0) {
					GlobalAPI.Manager().PushPage(settingMenuScreenController.NavigationController,new PushNotificationController(settingMenuScreenController.theaccountsummary));
				} else {
					GlobalAPI.Manager().PushPage(settingMenuScreenController.NavigationController,new EmailNotificationController(settingMenuScreenController.theaccountsummary));
				}	
				break;
			case 2:
				if (indexPath.Row == 0) {
					GlobalAPI.Manager().PushPage(settingMenuScreenController.NavigationController,new OrganizationSettingController(settingMenuScreenController.theaccountsummary));
				} else {
					GlobalAPI.Manager().PushPage(settingMenuScreenController.NavigationController,new ProjectSettingsController(settingMenuScreenController.theaccountsummary));
				}
				break;
			default:
				break;
			}
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 44f;
		}
	}
}

