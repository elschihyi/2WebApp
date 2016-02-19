using System;
using MonoTouch.Dialog;
using UIKit;
using System.Drawing;
using CoreGraphics;
namespace WebApp_iOS
{
	public class _2WebDesignEntryElement : UIViewElement
	{
		public String value;

		public _2WebDesignEntryElement (string placeHolder, UIView view, UIImage image) : base("", view, false)
		{

			var entry = new UITextField(new CGRect(40, 0, view.Bounds.Width - 50, view.Bounds.Height));
			entry.AttributedPlaceholder = new Foundation.NSAttributedString (placeHolder, null, UIColor.LightGray);
			entry.TextColor = UIColor.White;
			entry.BackgroundColor = UIColor.Clear; 
			entry.ValueChanged += (object sender, EventArgs e) => {
				value = entry.Text; 
			};

			view.Add(entry); 

			var img = new UIImageView (image); 
			img.Frame = new CGRect (10, 10, 20, 20); 

			view.Add (img); 

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



