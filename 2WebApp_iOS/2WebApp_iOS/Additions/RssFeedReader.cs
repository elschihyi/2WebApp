using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using UIKit;
using Foundation;
using System.IO;
using System.Net; 

namespace WebApp_iOS
{
	public class RssFeedReader
	{
		private static RssFeedReader Rss;

		private XDocument rssBlogs;
		private XDocument rssEvents; 
		private XDocument rssMarketFeeds;
		private XDocument rssEmailBlasts; 

//		private List<UIImage> blogImages = new List<UIImage>(); 
//		private UIImage[] eventImages;
//		private UIImage[] marketFeedImages;
//		private UIImage[] emailBlastImages;

		public RssFeedReader ()
		{
			 
		}

		public static RssFeedReader Manager ()
		{
			if (Rss == null)
				Rss = new RssFeedReader (); 

			return Rss; 
		}

		public void loadRssFeeds(){

			rssBlogs = XDocument.Load ("http://www.2webdesign.com/blog/feed/");
			rssEmailBlasts = XDocument.Load ("http://www.2webdesign.com/blog/category/email-blasts/feed/"); 
			//rssMarketFeeds = XDocument.Load (""); 
			rssEvents = XDocument.Load ("http://www.2webdesign.com/blog/category/event/feed/"); 

		}

//		public void loadImages(){
//			var blogs = ReadBlogs ();
//
//			foreach (var blog in blogs) {
//				try{
//					var index = 0;
//
//
//					var webClient = new WebClient(); 
//					webClient.DownloadDataCompleted += (s, e) => {
//						var bytes = e.Result; // get the downloaded data
//						string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
//
//						string localPath = Path.Combine (documentsPath, "blog"+index.ToString()+".png");
//						File.WriteAllBytes (localPath, bytes); // writes to local storage
//						index++; 
//					};
//					webClient.DownloadDataAsync(new Uri(blog.Link)); 
//
//					//var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments); 
////					var url = new NSUrl (blog.Link);
////					var data = NSData.FromUrl (url);
////
////					//var tmp = UIImage.LoadFromData (data);
////					var tmp = new UIImage("Cut_Images/Arrow_Icon.png"); 
////
////					var documentsDirectory = Environment.GetFolderPath
////						(Environment.SpecialFolder.Personal);
////					string jpgFilename = System.IO.Path.Combine (documentsDirectory, "Photo.jpg"); // hardcoded filename, overwritten each time
////					NSData imgData = UIImage.LoadFromData (data).AsJPEG();
////					NSError err = null;
////					if (imgData.Save(jpgFilename, false, out err)) {
////						Console.WriteLine("saved as " + jpgFilename);
////					} else {
////						Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
////					}
////					var filename = Path.Combine (documents, "blog" + i.ToString());  
////
////					var url = new NSUrl (blog.Link);
////					var data = NSData.FromUrl (url);
////
////					var tmp = UIImage.LoadFromData (data);
////
////					NSError err = new NSError();
////					tmp.AsPNG().Save(filename,true,out err);
//
//					 
//				}catch(Exception e){
//					var tmp = e.Message;
//				}
//			}
//		}

//		public UIImage grabNewsLetterImage(int index){
//			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
//			string file = System.IO.Path.Combine (documentsPath, "blog"+index.ToString()+".png");
//			return UIImage.FromFile(file);
////			return UIImage.FromFile("MyDocuments/"+ "blog"+index.ToString());
//		}

		public IEnumerable<Post> ReadBlogs()
		{
			//var rssFeed = XDocument.Load(url);

			var posts = from item in rssBlogs.Descendants("img")
				select new Post
			{
				//fields
				Title = item.Parent.Parent.Element("title").Value,
				Link = item.Parent.Parent.Element("link").Value,
				thumbnailUrl = item.Attribute("src").Value
			};

			return posts;
		}

		public IEnumerable<Post> ReadMarketResources()
		{
			//var rssFeed = XDocument.Load(url);

			var posts = from item in rssMarketFeeds.Descendants("img")
				select new Post
			{
				//fields
				Title = item.Parent.Parent.Element("title").Value,
				Link = item.Parent.Parent.Element("link").Value,
				thumbnailUrl = item.Attribute("src").Value
			};

			return posts;
		}

		public IEnumerable<Blast> ReadBlasts(){
			//var rssFeed = XDocument.Load (url); 

			var posts = from item in rssEmailBlasts.Descendants ("img")
			            select new Blast {
				Title = item.Parent.Parent.Element ("title").Value,
				Link = item.Parent.Parent.Element ("link").Value,
				thumbnailUrl = item.Attribute ("src").Value
			};

				return posts; 
		}

		public IEnumerable<EventItem> ReadEvents()
		{
			//var rssFeed = XDocument.Load(url);

			var posts = from item in rssEvents.Descendants("title")
				select new EventItem
			{
				//fields
				Title = item.Parent.Element("title").Value,
				Link = item.Parent.Element("link").Value
			};

//			int i = 0;
//			foreach(var item in posts){
//				//do not split the first item
//				if (i > 0) {
//					string[] tmp = item.Title.Split ('|');
//					item.Title = tmp [1];
//					item.Date = tmp [0]; 
//				}
//				i++; 
//			}

			return posts;
		}
	}

	public class Post
	{
		public string Title;
		public string Link; 
		public string thumbnailUrl; 
	}

	public class EventItem
	{
		public string Title;
		public string Link; 
	}

	public class Blast
	{
		public string Title;
		public string Link;
		public string thumbnailUrl; 
	}
}

//class Program
//{
//	static void Main(string[] args)
//	{
//		var posts = new RssFeedReader().ReadFeed(@"http://www.pwop.com/feed.aspx?show=dotnetrocks&filetype=master");
//		Console.WriteLine(posts.ToList().Count);
//		Console.ReadLine();
//	}
//}