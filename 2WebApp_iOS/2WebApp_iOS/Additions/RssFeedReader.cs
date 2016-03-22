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
			rssMarketFeeds = XDocument.Load ("https://www.2webdesign.com/blog/category/marketing-resources/feed/"); 
			rssEvents = XDocument.Load ("http://www.2webdesign.com/blog/category/event/feed/"); 

		}

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