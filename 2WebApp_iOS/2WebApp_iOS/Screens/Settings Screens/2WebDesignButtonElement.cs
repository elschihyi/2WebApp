using System;
using MonoTouch.Dialog;
using UIKit;
using System.Drawing;
using CoreGraphics;
namespace WebApp_iOS
{
	public class _2WebDesignButtonElement : UIViewElement
	{
		

		public _2WebDesignButtonElement (String title, UIView view, String request) : base("", view, false)
		{
			var btn = new UIButton (UIButtonType.System);
			btn.SetTitle (title, UIControlState.Normal); 
			btn.Frame = new CGRect (40, 5, view.Bounds.Width - 80, view.Bounds.Height - 10); 
			btn.BackgroundColor = GlobalAPI.Manager ().getTwoWebColor (); 
			btn.SetTitleColor (UIColor.White, UIControlState.Normal); 
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



