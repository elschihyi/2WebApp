using System;
using MonoTouch.Dialog;
using UIKit;

namespace WebApp_iOS
{
	public class _WebDesignElement : StringElement
	{
		public _WebDesignElement (string caption) : base(caption)
		{
			
		}

		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = base.GetCell (tv);

			cell.BackgroundColor = UIColor.Clear;


			return cell; 
		}
	}
}

