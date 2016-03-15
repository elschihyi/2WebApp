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
		//private HNKCacheFormat format;
		TwoWebDesignMain twoWebDesignMain;
		TwoWebDesignLibrary twoWebDesignLibrary;
		//TabProjects tabProjects;
		//MainSupport mainSupport;
		ArticlePage articlePage;

		public static DataService dataService;

		public GlobalAPI ()
		{
			
		}

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
			} catch (Exception e) {
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
						if (internetConnection ()) {
							PushPage (page.NavigationController, new MainSettingPage ());
						} else {
							new UIAlertView ("Alert", "An internet connection is required", null, "Ok", null).Show (); 
						}
					});
			}

			if (includeRadialMenu) {
				//Add Radial menu button  

				var image = UIImage.FromFile ("Cut_Images/radial_2web_icon.png");
				nfloat yPos;

				if (includeSettingsGear) {
					yPos = UIScreen.MainScreen.Bounds.Height - image.Size.Height - 10;
				} else {
					//On the setting page itself move it up due to monotouch dialog
					yPos = UIScreen.MainScreen.Bounds.Height - image.Size.Height * 3;
				}

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
						//PushPage(page.NavigationController,getTwoWebDesignMain());
						//page.NavigationController.PushViewController (new TwoWebDesignMain (), true); 
					} else if (args.MenuItem.Tag == 2) {
						PushPage (page.NavigationController, new TwoWebDesignLibrary ()); 
						//PushPage(page.NavigationController,getTwoWebDesignLibrary());
						//page.NavigationController.PushViewController (new TwoWebDesignLibrary (), true);
					} else if (args.MenuItem.Tag == 3) {
						//PushPage (page.NavigationController, new TabProjects ()); 
						//PushPage(page.NavigationController,getTabProjects());
						//page.NavigationController.PushViewController (new TabProjects (), true);
						PushPage(page.NavigationController,new ProjectMainController());
					} else {
						//PushPage (page.NavigationController, new MainSupport ()); 
						//PushPage(page.NavigationController,getMainSupport());
						page.NavigationController.PushViewController (new SupportMainScreenController (), true);
					}
					; 
				};

				page.View.AddSubview (button);
				//page.View.AddSubview (outView); 
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
		/*
		public TabProjects getTabProjects ()
		{
			if (tabProjects == null)
				tabProjects = new TabProjects ();

			return tabProjects;
		}
		*/
		/*
		public MainSupport getMainSupport ()
		{
			if (mainSupport == null)
				mainSupport = new MainSupport ();

			return mainSupport; 
		}
		*/
		//		public HNKCacheFormat getHNKFormat(){
		//			//HNKCacheFormat format = (HNKCacheFormat)HNKCache.SharedCache().Formats["thumbnail"];
		//
		//			if (format == null)
		//			{
		//				format = new HNKCacheFormat("thumbnail")
		//				{
		//					Size = new SizeF(320, 240),
		//					ScaleMode = HNKScaleMode.AspectFill,
		//					CompressionQuality = 0.5f,
		//					DiskCapacity = 1 * 1024 * 1024,
		//					PreloadPolicy = HNKPreloadPolicy.LastSession
		//				};
		//			}
		//
		//
		//			return format;
		//		}

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
		/*
		public Boolean loadUserData (string accountID)
		{
			try {
				using (var client = new WebClient ()) {
					//clear previous data
					DbStorage.Manager ().deleteProjects (); 
					DbStorage.Manager ().deleteOrganizations (); 


					//load landing data
					string dataString = "{\"data\":{\"client_accountid\":\"" + accountID + "\"}}";
					byte[] dataBytes = Encoding.UTF8.GetBytes (dataString); 
					var response = client.UploadData ("http://www.2web.cc.php53-4.ord1-1.websitetestlink.com/webservice/common/landingdata", dataBytes); 
					var responseString = Encoding.Default.GetString (response);
					var a = JObject.Parse (responseString); 

					var projectCount = a ["data"].ToObject<List<DbProject>> ().Count;
					var organizationCount = a ["organization"].ToObject<List<DbOrganization>> ().Count;

					for (int i = 0; i < projectCount; i++) {
						//load project details
						var projectID = a ["data"] [i] ["project_id"].ToString ();
						byte[] detailDataBytes = Encoding.UTF8.GetBytes ("{\"data\": {\"client_accountid\":\"" + accountID + "\",\"project_id\":\"" + projectID + "\"}}"); 
						var detailResponse = client.UploadData ("http://www.2web.cc.php53-4.ord1-1.websitetestlink.com/webservice/common/project_details", detailDataBytes); 
						var detailResponseString = Encoding.Default.GetString (detailResponse);
						var d = JObject.Parse (detailResponseString); 

						DbStorage.Manager ().addProject (
							int.Parse (d ["data"] [0] ["project_id"].ToString ()),
							d ["data"] [0] ["project_name"].ToString (),
							int.Parse (d ["data"] [0] ["org_id"].ToString ()),
							int.Parse (d ["data"] [0] ["client_accountid"].ToString ()),
							int.Parse (d ["data"] [0] ["project_type_id"].ToString ()),
							int.Parse (d ["data"] [0] ["proj_status_id"].ToString ()),
							int.Parse (d ["data"] [0] ["proj_phase_id"].ToString ()),
							int.Parse (d ["data"] [0] ["staff_id"].ToString ()),
							d ["data"] [0] ["row_adddate"].ToString (),
							d ["data"] [0] ["row_added_by"].ToString (),
							d ["data"] [0] ["row_update_date"].ToString (),
							d ["data"] [0] ["row_updated_by"].ToString (),
							d ["data"] [0] ["project_type_name"].ToString (),
							d ["data"] [0] ["proj_status_name"].ToString (),
							d ["data"] [0] ["proj_phase_name"].ToString (),
							d ["data"] [0] ["staff_firstname"].ToString (),
							d ["data"] [0] ["staff_lastname"].ToString ()
						);
					}

					for (int i = 0; i < organizationCount; i++) {
						DbStorage.Manager ().addOrganization (
							int.Parse (a ["organization"] [i] ["org_id"].ToString ()),
							a ["organization"] [i] ["org_name"].ToString (),
							a ["organization"] [i] ["org_status"].ToString (),
							a ["organization"] [i] ["row_adddate"].ToString (),
							int.Parse (a ["organization"] [i] ["row_added_by"].ToString ()),
							a ["organization"] [i] ["row_update_date"].ToString (),
							int.Parse (a ["organization"] [i] ["row_updated_by"].ToString ())
						);
					}
				}
				return true;
			} catch (Exception e) {
				return false;
			}
		}
		*/
		/*
		public Boolean Login (string em, string pwd, bool placeToken)
		{
			//load user data
			try {
				using (var client = new WebClient ()) {
					string dataString = "{\"data\": {\"client_email\":\"" + em + "\", \"password\":\"" + pwd + "\"}}";
					byte[] dataBytes = Encoding.UTF8.GetBytes (dataString); 
					var response = client.UploadData ("http://www.2web.cc.php53-4.ord1-1.websitetestlink.com/webservice/common/login", dataBytes); 
					var responseString = Encoding.Default.GetString (response);
					var l = JObject.Parse (responseString); 

					if (int.Parse (l ["errNode"] ["errCode"].ToString ()) > 0) {
						//error detected 
						var msg = l ["errNode"] ["errMsg"]; 
						return false;
					} else {

						loadUserData (l ["data"] [0] ["client_accountid"].ToString ()); 

						//do anyhting with user data?
						//l["data"][0]["client_accountid"].ToString()
						//l["data"][0]["client_firstname"].ToString()
						//l["data"][0]["client_lastname"].ToString()
						//l["data"][0]["client_email"].ToString()
						//l["data"][0]["client_password"].ToString()
						//l["data"][0]["client_status"].ToString()

						if (placeToken) {
							//put db token 
							DbStorage.Manager ().placeAuthenticationToken (em); 
					
						}

						return true; 
					}
				}
			} catch (Exception e) {
				return false; 
			}

		}
		*/
		/*
		public Boolean Logout ()
		{
			try {
				DbStorage.Manager ().AuthenticationLogout ();

				//clear user data
				DbStorage.Manager ().deleteProjects ();
				DbStorage.Manager ().deleteOrganizations (); 
			} catch (Exception e) {
				return false;
			}

			return true;
		}
		*/
		public Boolean SupportAuthorization (string usr)
		{

			//send request to 2web 
			if (true) {
				return true;
			} else {
				return false; 
			}

			//return true if successfull, false otherwise
		}

		public Boolean ProjectAuthorization (string usr)
		{
			//send request to 2web 
			if (true) {
				return true;
			} else {
				return false; 
			}

			//return true if successfull, false otherwise
		}

		public Boolean SignUp (string fName, string lName, string usr, string pwd)
		{
			//send to 2web 

			if (true) {
				return true;
			} else {
				return false;
			}
		}

		public Boolean ForgotPassword (String usr)
		{
			//send to 2web

			if (true) {
				return true;
			} else {
				return false;
			}
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
			} catch (Exception e) {

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
			} catch (Exception e) {

			}

			return markets.ToArray (); 
			/*
			return new MarketResource[] { 
				new MarketResource { MarketResourceTitle = "First Market Resource",
					ImageUrl = "http://www.2webdesign.com/blog/wp-content/uploads/2015/05/attract4web.jpg",
					Url = "http://www.2webdesign.com/blog/6469/icymi-check-out-these-2-awesome-presentations-coming-soon-near-you/"
				},
				new MarketResource { MarketResourceTitle = "Second Market Resource",
					ImageUrl = "http://www.2webdesign.com/blog/wp-content/uploads/2015/06/discoveremailmarketing1.jpg",
					Url = "http://www.2webdesign.com/blog/6625/icymi-discover-email-marketing-free-presentations/"
				},
				new MarketResource { MarketResourceTitle = "Third Market Resource",
					ImageUrl = "http://www.2webdesign.com/blog/wp-content/uploads/2015/06/youre-invited-we-Copy-1.png",
					Url = "http://www.2webdesign.com/blog/6569/limited-seats-remaining-in-develop-a-social-media-strategy-workshop/"
				}
			}; 
			*/
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
			} catch (Exception e) {

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
			} catch (Exception e) {

			}
				 
			return events.ToArray (); 
		}
		/*
		public Support[] LoadSupport ()
		{
			return new Support[] {
				new Support {
					SupportTitle = "Sarc"
				},
				new Support {
					SupportTitle = "Sarcan"
				},
				new Support {
					SupportTitle = "Employ Link"
				}
			}; 
		}
		*/

		//grab project updates depending on project
		/*
		public ProjectUpdate[] LoadProjectUpdates (int prjId)
		{
			return new ProjectUpdate[] {
				new ProjectUpdate {
					Title = "Bi-Weekly Update",
					Date = "June 4, 2015",
					Link = "https://www.google.ca/",
				},
				new ProjectUpdate {
					Title = "Bi-Weekly Update",
					Date = "June 18, 2015",
					Link = "https://www.google.ca/",
				},
				new ProjectUpdate {
					Title = "Bi-Weekly Update",
					Date = "July 2, 2015",
					Link = "https://www.google.ca/",
				}
			};
		}


		//grab project status depending on project
		public ProjectStatus[] LoadProjectStatus (string Status)
		{
			Boolean[] completed = new Boolean[5];

			var i = 0;

			switch (Status) {
			case "Discovery":
				completed [0] = true;
				break;
			case "Design":
				completed [0] = true;
				completed [1] = true;
				break;
			case "Development":
				completed [0] = true;
				completed [1] = true;
				completed [2] = true;
				break;
			case "Testing":
				completed [0] = true;
				completed [1] = true;
				completed [2] = true;
				completed [3] = true;
				break;
			case "Launch":
				completed [0] = true;
				completed [1] = true;
				completed [2] = true;
				completed [3] = true;
				completed [4] = true; 
				break;
			default:
				completed [0] = true;
				break;
			}

			 

			return new ProjectStatus[] {
				new ProjectStatus {
					Title = "Project Scope",
					Status = "Discovery",
					Completed = completed[0]
				},
				new ProjectStatus {
					Title = "Design Approval",
					Status = "Design",
					Completed = completed[1]
				},
				new ProjectStatus {
					Title = "View Testlink",
					Status = "Development",
					Completed = completed[2]
				},
				new ProjectStatus {
					Title = "View Testlink",
					Status = "Testing",
					Completed = completed[3]
				},
				new ProjectStatus {
					Title = "Launch",
					Status = "Launch",
					Completed = completed[4]
				}
			};
		}



		public Project[] LoadProjects ()
		{
			//grab projects
			var projects = DbStorage.Manager ().getProjects (); 
			Project[] returnProjects = new Project[projects.Count]; 

			int i = 0;
			foreach (var prj in projects) {
				//var prjDetail = DbStorage.Manager ().getProjectDetail (prj.id);

				returnProjects [i] = new Project {
					Id = prj.id,
					Title = prj.projectName,
					Status = prj.projectStatusName,
					PrimaryContact = "Khaled Haggag",
					LastPost = new DateTime (2015, 04, 27),
					NumberOfUpdates = 1,
					Company = "Sarcan",
					Type = prj.projectTypeName,
					StaffContact = prj.staffFirstName + " " + prj.staffLastName,
					SupportType = "360",
					SupportHours = "4/12",
					SupportLatestBackUp = "June 1, 2015",
					SupportLastRestored = "February 3, 2013",
					SupportStatus = "Up-to-date",
				};
				i++; 
			}

			return returnProjects; 

		}
		*/
		public Boolean loadRss ()
		{
			try {
				RssFeedReader.Manager ().loadRssFeeds ();
			} catch (Exception e) {
				return false;
			}
			return true; 
		}

	}
}

