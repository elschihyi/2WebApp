using System;
using MonoTouch.Dialog;
using UIKit;

namespace WebApp_iOS
{
	public class _WebDesignRootElement : RootElement
	{
		public _WebDesignRootElement (string caption) : base (caption)
		{

		}

		public _WebDesignRootElement(string caption, Func<RootElement, UIViewController> createOnSelected) : base (caption, createOnSelected)
		{
		}

		public _WebDesignRootElement(string caption, int section, int element) : base (caption, section, element)
		{
		}

		public _WebDesignRootElement(string caption, Group group) : base (caption, group)
		{
		}

		public override UITableViewCell GetCell (UITableView tv)
		{
			tv.BackgroundColor = UIColor.Clear;
			tv.BackgroundView = null;
			//return base.GetCell (tv);
			var returnCell=base.GetCell (tv);
			returnCell.BackgroundColor = UIColor.Clear;
			returnCell.TextLabel.TextColor = UIColor.White;
			return returnCell;
		}

		protected override void PrepareDialogViewController(UIViewController dvc)
		{
//			dvc.TableView.BackgroundColor =  UIColor.Clear;
//			dvc.TableView.BackgroundView = null;
			dvc.View.BackgroundColor = UIColor.Clear; 
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			base.PrepareDialogViewController(dvc);
		}

	
	}
}

