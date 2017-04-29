using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using Joy.Core;

namespace Joy.Server
{
	public class WebPage : Page
	{
		const string STR_Time = "time";
		const string KeyClientCache = "ccx";
		const string KeyClientCacheJson = "ccj";
		const string STR_Dashs = "-";
		const string KeyUserInstanceId = "userid";
		const string HTTP_FORWARDED_FOR = "HTTP_X_FORWARDED_FOR";
		const string UnderLine = "_";
		protected ClientCredential clientCredential;
		protected string PageId
		{
			get
			{
				return this.GetType().Name;
			}
		}
		protected Storage storage;
		protected override void OnInit(EventArgs e)
		{
			Session[STR_Time] = DateTime.UtcNow;
			storage = Storage.GetInstance<Storage>("");
			base.OnInit(e);
		}
		public string NewId(string uid = null)
		{
			if (uid == null)
			{
				uid = Guid.NewGuid().ToString();
			}
			return string.Concat(uid.Replace(STR_Dashs, string.Empty), UnderLine, PageId);
		}
		protected string Cookie(string key, string value = null)
		{
			if (value == null)
			{
				HttpCookie c = Request.Cookies[key];
				if (c != null)
				{
					return c.Value;
				}
			}
			else
			{
				HttpCookie rlt = new HttpCookie(key, value);
				Response.SetCookie(rlt);
			}
			return null;
		}
	}
	public class Storage : Dictionary<string, object>
	{
		protected static TimeSpan DefaultDuration = TimeSpan.FromMinutes(20);
		protected Cache cache;
		public Storage()
		{
			cache = HttpContext.Current.Cache;
		}
		public static T GetInstance<T>(string id) where T : Storage, new()
		{
			Cache c = HttpContext.Current.Cache;
			if (!c.Exists(id))
			{
				T rlt = new T();
				c.Insert(id, rlt, null, DateTime.MaxValue, DefaultDuration);
				return rlt;
			}
			else
			{
				return c[id] as T;
			}
		}
	}
}
