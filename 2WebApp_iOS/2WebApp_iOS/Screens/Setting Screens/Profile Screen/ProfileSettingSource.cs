using System;
using UIKit;
using System.Drawing;
using Foundation;

namespace WebApp_iOS
{
	public class ProfileSettingSource: UITableViewSource
	{
		 
		ProfileSettingController profileSettingController;
		UserProfile userProfile;

		public ProfileSettingSource (ProfileSettingController profileSettingController)
		{
			this.profileSettingController = profileSettingController;
			userProfile = profileSettingController.userProfile;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 2;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			switch (section) {
			case 0:
				return 3;
			case 1:
				return 4;
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
			if (section == 0) {
				return null;
			} else {	
				UIView HeaderView;
				UILabel HeaderLabel;
				UIView BlueLineView;

				HeaderView = new UIView () {
					BackgroundColor = UIColor.FromRGB (35, 40, 46),
					Frame = new RectangleF (0f, 0f, (float)UIScreen.MainScreen.Bounds.Width, 44f),
				};

				HeaderLabel = new UILabel () {
					Font = UIFont.BoldSystemFontOfSize (16f),
					TextColor = UIColor.FromRGB (0, 172, 237),
					TextAlignment = UITextAlignment.Left,
					BackgroundColor = UIColor.Clear,
					Frame = new RectangleF (0f, 0f, (float)UIScreen.MainScreen.Bounds.Width, (float)HeaderView.Frame.Height),
				};
				HeaderLabel.Text = "Change Password";
				HeaderView.Add (HeaderLabel);

				BlueLineView = new UIView () {
					BackgroundColor = UIColor.FromRGB (0, 172, 237),
					Frame = new RectangleF (0f, 42f, (float)UIScreen.MainScreen.Bounds.Width, 2f),
				};
				HeaderView.Add (BlueLineView);
				return HeaderView;
			}
		}

		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			if (section == 0) {
				return 0f;
			} else {	
				return 44f;
			};
		}

		public override string TitleForFooter (UITableView tableView, nint section)
		{
			return "";
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			if (!(indexPath.Section == 1 && indexPath.Row == 3)) {
				var cell = tableView.DequeueReusableCell (ProfileSettingCell.Key) as ProfileSettingCell;
				if (cell == null) {
					cell = new ProfileSettingCell ();
					cell.TextField.EditingDidEnd += (s, e) => {
						EditEnd(cell.Section,cell.Row,cell.TextField.Text);
					};	
				}
				cell.Section = indexPath.Section;
				cell.Row = indexPath.Row;
				switch (indexPath.Section) {
				case 0:
					switch (indexPath.Row) {
					case 0:
						cell.IconImageView.Image = UIImage.FromFile ("Cut_Images/Name_Icon.png");
						cell.TextField.AttributedPlaceholder = new NSAttributedString ("FIRST NAME",null,UIColor.LightGray);
						cell.TextField.Enabled = true;
						cell.TextField.SecureTextEntry = false;
						cell.TextField.Text = userProfile.firstName;
						break;
					case 1:
						cell.IconImageView.Image = UIImage.FromFile ("Cut_Images/Name_Icon.png");
						cell.TextField.AttributedPlaceholder = new NSAttributedString ("LAST NAME",null,UIColor.LightGray);
						//cell.TextField.Placeholder = "LAST NAME";
						cell.TextField.Enabled = true;
						cell.TextField.SecureTextEntry = false;
						cell.TextField.Text = userProfile.lastName;
						break;
					case 2:
						cell.IconImageView.Image = UIImage.FromFile ("Cut_Images/Email_Icon.png");
						cell.TextField.AttributedPlaceholder = new NSAttributedString ("EMAIL",null,UIColor.LightGray);
						//cell.TextField.Placeholder = "EMAIL";
						cell.TextField.Enabled = false;
						cell.TextField.SecureTextEntry = false;
						cell.TextField.Text = userProfile.email;
						break;
					default:
						cell.IconImageView.Image = null;
						cell.TextField.Placeholder = "";
						cell.TextField.Enabled = false;
						cell.TextField.SecureTextEntry = false;
						cell.TextField.Text = "";
						break;
					}
					break;
				case 1:
					switch (indexPath.Row) {
					case 0:
						cell.IconImageView.Image = UIImage.FromFile ("Cut_Images/Password_Icon.png");
						cell.TextField.AttributedPlaceholder = new NSAttributedString ("OLD PASSWORD",null,UIColor.LightGray);
						//cell.TextField.Placeholder = "OLD PASSWORD";
						cell.TextField.Enabled = true;
						cell.TextField.SecureTextEntry = true;
						cell.TextField.Text = profileSettingController.oldpassword;
						break;
					case 1:
						cell.IconImageView.Image = UIImage.FromFile ("Cut_Images/Password_Icon.png");
						cell.TextField.AttributedPlaceholder = new NSAttributedString ("NEW PASSWORD",null,UIColor.LightGray);
						//cell.TextField.Placeholder = "NEW PASSWORD";
						cell.TextField.Enabled = true;
						cell.TextField.SecureTextEntry = true;
						cell.TextField.Text = profileSettingController.newpassword;
						break;
					case 2:
						cell.IconImageView.Image = UIImage.FromFile ("Cut_Images/Confirm_Password_Icon.png");
						cell.TextField.AttributedPlaceholder = new NSAttributedString ("CONFIREM PASSWORD",null,UIColor.LightGray);
						//cell.TextField.Placeholder = "CONFIREM PASSWORD";
						cell.TextField.Enabled = true;
						cell.TextField.SecureTextEntry = true;
						cell.TextField.Text = profileSettingController.confirmpassword;
						break;
					default:
						cell.IconImageView.Image = null;
						cell.TextField.Placeholder = "";
						cell.TextField.Enabled = true;
						cell.TextField.SecureTextEntry = true;
						cell.TextField.Text = "";
						break;
					}
					break;
				default:
					cell.IconImageView.Image = null;
					cell.TextField.Placeholder = "";
					cell.TextField.Enabled = false;
					cell.TextField.SecureTextEntry = false;
					cell.TextField.Text = "";
					break;
				}
				return cell;
			} else {
				var cell = tableView.DequeueReusableCell (ProfileSettingUpdateCell.Key) as ProfileSettingUpdateCell;
				if (cell == null) {
					cell = new ProfileSettingUpdateCell ();
					cell.RequestBtn.TouchUpInside += (s, e) => {
						profileSettingController.UpdateClick();
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

		public void EditEnd(int Section,int Row,string Text){
			switch (Section) {
			case 0:
				switch (Row) {
				case 0:
					userProfile.firstName=Text;
					break;
				case 1:
					userProfile.lastName=Text;
					break;
				case 2:
					userProfile.email=Text;
					break;
				default:
					break;
				}
				break;
			case 1:
				switch (Row) {
				case 0:
					profileSettingController.oldpassword=Text;
					break;
				case 1:
					profileSettingController.newpassword=Text;
					break;
				case 2:
					profileSettingController.confirmpassword=Text;
					break;
				default:
					break;
				}
				break;
			default:
				break;
			}
		}	
	}
}

