using Joy.Server.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.Bookmarks
{
	public class Bookmark : ServiceHandler
	{
		private const string ViewBookmarks = "/Bookmarks/";

		[Method]
		public string Add(string url)
		{
			return url;
		}


		[Method (IsPage = true)]
		public void View()
		{
			LoadView("Bookmarks/ViewBookmark");
		}
	}
}