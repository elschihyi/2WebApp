using System;
using UIKit;
using Foundation;
using CoreGraphics;
//using Haneke;
using SDWebImage;
using System.Drawing;

namespace WebApp_iOS
{
	
	public class CustomArticleCell : UITableViewCell  {
		UILabel title;
		UIImageView image;
		//HanekeUIImageView image;



		public CustomArticleCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;
			ContentView.BackgroundColor = UIColor.Clear; 
			//var semiTransparentColor = new UIColor(0, 0, 0, new nfloat(0.5));
			var semiTransparentColor = new UIColor(0, 0, 0, new nfloat(0.8));

			image = new UIImageView ();
			//image = new UIImageView(new CGRect(0,0,320,240));
			//image.ClipsToBounds = true;
			//image.ContentMode = UIViewContentMode.ScaleAspectFill;
			//image.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight; 
			//image = new HanekeUIImageView (); 

			title = new UILabel () {
				TextColor = GlobalAPI.Manager().getTwoWebColor(),
				//Font = UIFont.SystemFontOfSize(12),
				Font = UIFont.SystemFontOfSize(16),
				TextAlignment = UITextAlignment.Center,
				BackgroundColor = semiTransparentColor,
				Lines = 2,
			};
			ContentView.AddSubviews(new UIView[] {image, title});


		}

		public void UpdateCell (string titl, string imgUrl)
		{
			try{
			//image.SetCacheFormat (GlobalAPI.Manager().getHNKFormat()); 
			//image.SizeToFit(); 
				//image.CancelSetImage(); 
				//image.Image = null; 

				image.SetImage(new NSUrl(imgUrl));

				//HanekeUIImageView.SetImage(image,new NSUrl(imgUrl)); 
			}catch(Exception e){
				var tmp = e.Message; 
			}

			title.Text = titl;
		}

//		public void UpdateCell (string titl, UIImage img)
//		{
//			image.Image = img;
//			title.Text = titl;
//		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			image.Frame = new CGRect (0, 10, 320, 120);
			title.Frame = new CGRect (0, 90, 320, 40);
		}
	}
}

