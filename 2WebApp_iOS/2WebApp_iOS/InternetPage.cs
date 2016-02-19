
using System;
using System.Linq;
using System.Collections.Generic;

using MonoTouch.Dialog;

using Foundation;
using UIKit;

namespace WebApp_iOS
{
	public partial class InternetPage : DialogViewController
	{
		private string URL = "";
		HtmlElement web;

		public InternetPage () : base (UITableViewStyle.Grouped, null)
		{
			web = new HtmlElement ("", URL); 

			Root = new RootElement ("") {
				new Section ("") {
					web
				}
			};
		}

		public void setUrl(string url){
			URL = url; 
			web.Url = URL;

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			GlobalAPI.Manager ().PageDefault (this, "",false, false);
		}
	}
}
