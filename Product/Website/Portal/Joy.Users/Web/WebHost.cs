using System.Web;
using System.Web.Caching;
using System.Web.Routing;

namespace Joy.Users.Web
{
    public interface IWebHost
    {
        Cache Caches { get; }
        HttpSessionStateBase Session { get; }
        HttpRequestBase Request { get; }
        HttpResponseBase Response { get; }
        HttpCookieCollection CookiesQ { get; }
        HttpCookieCollection Cookies { get; }
    }

    public class WebHost : IWebHost
    {
        protected RequestContext rc;
        public WebHost(RequestContext context)
        {
            rc = context;
        }

        public Cache Caches
        {
            get
            {
                return rc == null ? null : rc.HttpContext.Cache;
            }
        }

        public HttpSessionStateBase Session
        {
            get
            {
                return rc == null ? null : rc.HttpContext.Session;
            }
        }

        public HttpRequestBase Request
        {
            get
            {
                return rc == null ? null : rc.HttpContext.Request;
            }
        }

        public HttpResponseBase Response
        {
            get
            {
                return rc == null ? null : rc.HttpContext.Response;
            }
        }

        public HttpCookieCollection CookiesQ
        {
            get
            {
                return rc == null ? null : rc.HttpContext.Request.Cookies;
            }
        }

        public HttpCookieCollection Cookies
        {
            get
            {
                return rc == null ? null : rc.HttpContext.Response.Cookies;
            }
        }

    }
}
