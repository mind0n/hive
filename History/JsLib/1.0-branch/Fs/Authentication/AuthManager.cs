using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.IO;

namespace Fs.Authentication
{
	public class AuthManager
	{
		public delegate bool AuthenticateDelegate(string uname, string upwd);
		public static AuthenticateDelegate OnAuthenticate
		{
			set
			{
				Session["OnAuthenticate"] = value;
			}
			get
			{
				AuthenticateDelegate dg = Session["OnAuthenticate"] as AuthenticateDelegate;
				//if (dg == null)
				//{
				//    return new AuthenticateDelegate(delegate(string a, string b)
				//    {
				//        return false;
				//    });
				//}
				//else
				//{
					return dg;
				//}
			}
		}
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
		public static HttpResponse Response
		{
			get
			{
				return HttpContext.Current.Response;
			}
		}
		public static HttpCookieCollection ResponseCookies
		{
			get
			{
				return HttpContext.Current.Response.Cookies;
			}
		}
		public static HttpCookieCollection RequestCookies
		{
			get
			{
				return HttpContext.Current.Request.Cookies;
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
		public static string LastHash
		{
			get
			{
				return GetSession("_Ticket");
			}
			set
			{
				Session["_Ticket"] = value;
				Response.SetCookie(new HttpCookie("_Ticket", value));
			}
		}
		public static string AuthCode
		{
			get
			{
				return GetSession("_AuthCode");
			}
			set
			{
				Session["_AuthCode"] = value;
			}
		}
		public static string Url
		{
			get
			{
				return GetSession("_Url");
			}
		}
		public static string RememberHash
		{
			get
			{
				return GetSession("_RememberHash");
			}
			set
			{
				Session["_RememberHash"] = value;
			}
		}
		public static string ActiveMember
		{
			get
			{
				//object rlt = Session["_MemberName"];
				//if (rlt != null)
				//{
				//    return rlt.ToString();
				//}
				//else
				//{
				//    return null;
				//}
				return GetSession("_MemberName");
			}
			protected set
			{
				Session["_MemberName"] = "";
			}
		}
		public static bool ShouldRemember
		{
			get
			{
				return GetSession<bool>("_Remember");
			}
			set
			{
				Session["_Remember"] = value;
			}
		}
		public static bool IsLogin
		{
			get
			{
				//object rlt = Session["_IsLogin"];
				//if (rlt != null)
				//{
				//    return rlt.Equals(true);
				//}
				//else
				//{
				//    return false;
				//}
				return GetSession<bool>("_IsLogin");
			}
			protected set
			{
				Session["_IsLogin"] = value;
			}
		}
		public static void Login(string MemberName)
		{
			Session["_IsLogin"] = true;
			Session["_MemberName"] = MemberName;
			HttpCookie hc = new HttpCookie("_MemberName", MemberName);
			Response.SetCookie(hc);
		}
		public static void Logout()
		{
			IsLogin = false;
			ActiveMember = "";
			ShouldRemember = false;
			RememberHash = "";
		}
		public static void RememberUrl()
		{
			Session["_Url"] = Request.Url.ToString();
		}
		public static string MakeAuthCode()
		{
			string rlt = Guid.NewGuid().ToString().Substring(0, 4);
			Session["_AuthCode"] = rlt;
			return rlt;
		}
		public static string MakeTicket()
		{
			string guid = Guid.NewGuid().ToString();
			HttpCookie Cookie = new HttpCookie("_Ticket", guid);
			Cookie.Expires = DateTime.Now.AddDays(1);
			Response.Cookies.Remove("_Ticket");
			Response.SetCookie(Cookie);
			Session["_Ticket"] = guid;
			HttpCookieCollection cookies = Request.Cookies;
			File.AppendAllText("d:\\cookies.txt", guid + "\r\n");
			return guid;
		}
		protected static T GetSession<T>(string name)
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
		protected static string GetSession(string name)
		{
			return GetSession<string>(name);
		}
	}
}
