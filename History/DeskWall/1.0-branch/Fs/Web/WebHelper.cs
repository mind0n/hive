using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;

namespace Fs.Web
{
	public class RequestHelper
	{
		public delegate void ItemEnumHandler(string key, string val);
		public static void EnumKeys(NameValueCollection Form, string prefix, ItemEnumHandler callback)
		{
			string key;
			foreach (string item in Form.Keys)
			{
				if (item.IndexOf(prefix) == 0)
				{
					if (item.Length > prefix.Length)
					{
						key = item.Substring(prefix.Length, item.Length - prefix.Length);
					}
					else
					{
						key = "";
					}
					callback(key, Form[item]);
				}
			}

		}
	}
	public class ServerHelper
	{
		public static HttpCookieCollection CookiesRequest
		{
			get
			{
				return HttpContext.Current.Request.Cookies;
			}
		}
		public static HttpCookieCollection CookiesResponse
		{
			get
			{
				return HttpContext.Current.Response.Cookies;
			}
		}
		public static HttpCookieCollection Cookies
		{
			get
			{
				return HttpContext.Current.Response.Cookies;
			}
		}
		public static HttpResponse Response
		{
			get
			{
				return HttpContext.Current.Response;
			}
		}
		public static HttpRequest Request
		{
			get
			{
				return HttpContext.Current.Request;
			}
		}
		public static HttpSessionState Session
		{
			get
			{
				return HttpContext.Current.Session;
			}
		}
		public static T GetSession<T>(string name)
		{
			object rlt = Session[name];
			if (rlt != null)
			{
				return (T)rlt;
			}
			else
			{
				return default(T);
			}
		}
		public static string GetSession(string name)
		{
			return GetSession<string>(name);
		}
		public static string GetCookieValue(string name)
		{
			HttpCookie hc = Request.Cookies.Get(name);
			if (hc != null)
			{
				return hc.Value;
			}
			return null;
		}
		public static void SetSession(string key, object value)
		{
			Session[key] = value;
		}
		public static void SetCookie(string key, string value)
		{
			SetCookie(key, value, null);
		}
		public static void SetCookie(string key, string value, string expireDate)
		{
			HttpCookie hc = new HttpCookie(key, value);
			if (expireDate != null)
			{
				hc.Expires = DateTime.Parse(expireDate);
			}
			Response.SetCookie(hc);
		}

	}
}
