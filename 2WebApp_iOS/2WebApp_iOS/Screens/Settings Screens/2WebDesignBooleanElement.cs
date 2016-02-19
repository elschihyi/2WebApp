using System;
using MonoTouch.Dialog;
using UIKit;
using System.Drawing;
using CoreGraphics;
namespace WebApp_iOS
{
	public class _2WebDesignBooleanElement : UIViewElement
	{
		public Boolean Value;
		UISwitch sw;


		public _2WebDesignBooleanElement (string caption, UIView view, Boolean state) : base("", view, false)
		{
			

			Value = state;  

			sw = new UISwitch (new CGRect(view.Bounds.Width - 58, 7, 30, 20)); 

			sw.ValueChanged += (object sender, EventArgs e) => {
				if(Value)
					Value = false;
				else
					Value = true; 
			};

			sw.OnTintColor = GlobalAPI.Manager ().getTwoWebColor (); 
			sw.On = Value;

			view.Add(sw); 


			var lbl = new UILabel (new CGRect (10, 0, view.Bounds.Width - 70, view.Bounds.Height));
			lbl.Text = caption;
			lbl.TextColor = UIColor.White;
			lbl.BackgroundColor = UIColor.Clear; 

			view.Add (lbl);

			view.BackgroundColor = UIColor.Clear; 


		}

		public override void Selected (DialogViewController dvc, UITableView tableView, Foundation.NSIndexPath path)
		{
			this.GetActiveCell ().SelectionStyle = UITableViewCellSelectionStyle.None; 
			base.Selected (dvc, tableView, path);
		}

		public override UITableViewCell GetCell (UITableView tv)
		{ 
			var cell = base.GetCell(tv);
			cell.BackgroundColor = UIColor.Clear; 
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			return cell; 
		}








	}
}



