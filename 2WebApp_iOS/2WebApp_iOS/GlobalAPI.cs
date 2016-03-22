using System;
using System.Collections.Generic;
using UIKit;
using SatelliteMenu;
using Foundation;
using System.Threading.Tasks;

//using Haneke;
using System.Drawing;
using CoreTelephony;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json.Linq;
using CoreDataService;
using System.IO;

namespace WebApp_iOS
{
	public class GlobalAPI
	{
		private static GlobalAPI networkAPI;
		TwoWebDesignMain twoWebDesignMain;
		TwoWebDesignLibrary twoWebDesignLibrary;
		ArticlePage articlePage;

		public static DataService dataService;

		public GlobalAPI ()
		{
			
		}

		public static UIViewController originPage{ get; set;}
		public static WelcomePage welcomePage{ get; set;}

		public static DataService GetDataService(){
			if (dataService == null) {
				//db set up
				string sqliteFilename = "2Web_DB.db3";
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string dbPath = Path.Combine(documentsPath, sqliteFilename);
				dataService = new DataService (dbPath);
			}
			return dataService;
		}	

		public static GlobalAPI Manager ()
		{
			if (networkAPI == null)
				networkAPI = new GlobalAPI (); 

			return networkAPI; 
		}

		public UIColor getTwoWebColor ()
		{
			//return new UIColor (0, 255/172, 255/237, 1); 
			return new UIColor (0, 172 / 255.0f, 237 / 255.0f, 1);
			//return new UIColor( 

		}

		public void PushPage (UINavigationController con, UIViewController toPush)
		{
			try {
				con.PushViewController (toPush, false);
				UIView.BeginAnimations (null);
				UIView.SetAnimationDuration (0.6);
				UIView.SetAnimationTransition (UIViewAnimationTransition.FlipFromRight, con.View, true);
				UIView.CommitAnimations (); 
			} catch (Exception e) {
				var tmp = e.Message; 
				con.PushViewController (toPush, true); 
			}
		}

		public Boolean internetConnection ()
		{
			return Reachability.Reachability.IsHostReachable ("https://google.com"); 

			//NetworkStatus internetStatus = Reachability.InternetConnectionStatus();
		}

		public Boolean phoneAvailability ()
		{
			try {
				if (UIApplication.SharedApplication.CanOpenUrl (new NSUrl (@"tel://"))) {
					//A dialer is installed, now let's check if we can actually make a call.
					var networkInfo = new CTTelephonyNetworkInfo ();
					var carrier = networkInfo.SubscriberCellularProvider;
					var networkCode = carrier.MobileNetworkCode;
					if (String.IsNullOrEmpty (networkCode) || networkCode == "65535") {
						//Device can not place a call
						//SIM is probably inactive or not installed.
						return false;
					} else {
						//Device most likely can make calls.
						return true;
					}
				}

				return false;
			} catch  {
				return false; 
			}

		}

		public void PageDefault (UIViewController page, string title, Boolean includeRadialMenu, Boolean includeSettingsGear)
		{

			page.Title = title; 
			page.NavigationController.NavigationBar.Translucent = true;
			page.View.BackgroundColor = UIColor.Clear;

			if (includeSettingsGear) {
				//put settings icon
				page.NavigationItem.RightBarButtonItem = new UIBarButtonItem (new UIImage ("Cut_Images/Setting_Icon.png"), UIBarButtonItemStyle.Plain, 
					(sender, args) => {
						originPage=page;
						PushPage (page.NavigationController, new SettingMenuScreenController ());
					});
			}

			if (includeRadialMenu) {
				//Add Radial menu button  

				var image = UIImage.FromFile ("Cut_Images/radial_2web_icon.png");
				nfloat yPos;

				yPos= UIScreen.MainScreen.Bounds.Height - image.Size.Height - 10;
				var btnFrame = new CoreGraphics.CGRect (10, yPos, image.Size.Width, image.Size.Height); 

				var shadow = new UIImageView (UIImage.FromFile ("Cut_Images/Radial_menu_shadow.png")); 
				var ring = new UIImageView (UIImage.FromFile ("Cut_Images/Radial_menu_tranparent_bg.png")); 
				var outView = new UIView (new CoreGraphics.CGRect (10, yPos, image.Size.Width * 2, image.Size.Height * 2)); 
				outView.Add (shadow); 
				outView.Add (ring); 

				var items = new [] { 
					new SatelliteMenuButtonItem (UIImage.FromFile ("Cut_Images/radial_menu_item_2web.png"), 1, "2Web"),
					new SatelliteMenuButtonItem (UIImage.FromFile ("Cut_Images/radial_menu_item_library.png"), 2, "2WebLibrary"),
					new SatelliteMenuButtonItem (UIImage.FromFile ("Cut_Images/radial_menu_item_project.png"), 3, "Projects"),
					new SatelliteMenuButtonItem (UIImage.FromFile ("Cut_Images/radial_menu_item_support.png"), 4, "Support")
				};

				var button = new SatelliteMenuButton (page.View, image, items, btnFrame);

				button.MenuItemClick += (_, args) => {
					if (args.MenuItem.Tag == 1) {
						PushPage (page.NavigationController, new TwoWebDesignMain ()); 
					} else if (args.MenuItem.Tag == 2) {
						PushPage (page.NavigationController, new TwoWebDesignLibrary ()); 
					} else if (args.MenuItem.Tag == 3) {
						PushPage(page.NavigationController,new ProjectMainController());
					} else {
						PushPage(page.NavigationController,new SupportMainScreenController ());
					}
					; 
				};

				page.View.AddSubview (button); 
			}

		}

		public ArticlePage getInternetPage (string url)
		{
			if (articlePage == null) {
				articlePage = new ArticlePage (url);
			} else {
				articlePage.Url (url); 
			}

			return articlePage;
		}

		public TwoWebDesignMain getTwoWebDesignMain ()
		{
			if (twoWebDesignMain == null)
				twoWebDesignMain = new TwoWebDesignMain ();

			return twoWebDesignMain; 
		}

		public TwoWebDesignLibrary getTwoWebDesignLibrary ()
		{
			if (twoWebDesignLibrary == null)
				twoWebDesignLibrary = new TwoWebDesignLibrary (); 

			return twoWebDesignLibrary;
		}

		//load images regularly, not using anymore in favor of Hak compinant - asnc with cache
		public async Task<UIImage> ImageFromUrl (string uri)
		{
			try {
				var url = new NSUrl (uri);
				var data = NSData.FromUrl (url);
				return UIImage.LoadFromData (data);
			} catch {
				return null; 
			}
		}

		public Boolean SupportAuthorization (string usr)
		{

			//send request to 2web 
			return true;
		}

		public Boolean ProjectAuthorization (string usr)
		{
			//send request to 2web 

			return true;
			//return true if successfull, false otherwise
		}

		public Boolean SignUp (string fName, string lName, string usr, string pwd)
		{
			//send to 2web 
			return true;
		}

		public Boolean ForgotPassword (String usr)
		{
			//send to 2web
			return true;
		}

		public Blast[] LoadEmailBlasts ()
		{

			//grab email blasts  
			var blasts = new List<Blast> (); 

			try {

				var posts = RssFeedReader.Manager ().ReadBlasts (); 

				foreach (var post in posts) {
					blasts.Add (new Blast {
						Title = post.Title,
						thumbnailUrl = post.thumbnailUrl,
						Link = post.Link
					}); 
				}
			} catch {

			}

			return blasts.ToArray ();

		}


		public MarketResource[] LoadMarketResources ()
		{
			//grab markets
			var markets = new List<MarketResource> (); 

			try {
				var posts = RssFeedReader.Manager ().ReadMarketResources (); 
				foreach (var post in posts) {
					markets.Add (new MarketResource {
						MarketResourceTitle = post.Title,
						ImageUrl = post.thumbnailUrl,
						Url = post.Link
					}); 
				}
			} catch {

			}

			return markets.ToArray (); 
		}

		public NewsLetter[] LoadNewsletters ()
		{
			//grab newsletters 
			var news = new List<NewsLetter> (); 

			try {
				var posts = RssFeedReader.Manager ().ReadBlogs (); 
				foreach (var post in posts) {
					news.Add (new NewsLetter {
						NewsletterTitle = post.Title,
						ImageUrl = post.thumbnailUrl,
						Url = post.Link
					}); 
				}
			} catch {

			}

			return news.ToArray (); 
		}
			
		public Event[] LoadEvents ()
		{
			//grab events 
			var events = new List<Event> (); 

			try {

				var posts = RssFeedReader.Manager ().ReadEvents (); 

				foreach (var post in posts) {
					events.Add (new Event {
						EventTitle = post.Title,
						EventLink = post.Link
					}); 
				}

				events.RemoveAt (0);
			} catch {

			}
				 
			return events.ToArray (); 
		}

		public Boolean loadRss ()
		{
			try {
				RssFeedReader.Manager ().loadRssFeeds ();
			} catch {
				return false;
			}
			return true; 
		}

	}
}

