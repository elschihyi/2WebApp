using System;
using MonoTouch.Dialog;
using UIKit;
using System.Drawing;
using CoreGraphics;
namespace WebApp_iOS
{
	public class _2WebDesignBadgeElement : UIViewElement
	{ 

		public _2WebDesignBadgeElement (string caption, UIView view, String request) : base("", view, false)
		{

			var lbl = new UILabel (new CGRect (10, 0, view.Bounds.Width - 140, view.Bounds.Height));
			lbl.Text = caption;
			lbl.TextColor = UIColor.White;
			lbl.BackgroundColor = UIColor.Clear; 

			view.Add (lbl);

			var btn = new UIButton (UIButtonType.Custom); 
			btn.SetImage (new UIImage ("Settings/RequestAsPrimaryBtn.png"), UIControlState.Normal);
			btn.Frame = new CGRect (UIScreen.MainScreen.Bounds.Width - 120, 3, 95, 40);
			btn.TouchUpInside += (object sender, EventArgs e) => {
				new UIAlertView("Alert","Coming Soon",null,"Ok",null).Show(); 
				//request
			};

			view.Add(btn); 

			view.BackgroundColor = UIColor.Clear; 
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



