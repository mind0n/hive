

using System;
using System.Web;
using System.Web.Caching;
using System.Web.Routing;
namespace Joy.Users.Web
{
    public class WebPPLService : PPLService
    {
        protected RequestContext rc;

        protected PPLCache Caches
        {
            get
            {
                return PPLService.Cache;
            }
        }

        protected HttpSessionStateBase Session
        {
            get
            {
                return rc == null ? null : rc.HttpContext.Session;
            }
        }

        protected HttpRequestBase Request
        {
            get
            {
                return rc == null ? null : rc.HttpContext.Request;
            }
        }

        protected HttpResponseBase Response
        {
            get
            {
                return rc == null ? null : rc.HttpContext.Response;
            }
        }

        protected HttpCookieCollection CookiesQ
        {
            get
            {
                return rc == null ? null : rc.HttpContext.Request.Cookies;
            }
        }

        protected HttpCookieCollection Cookies
        {
            get
            {
                return rc == null ? null : rc.HttpContext.Response.Cookies;
            }
        }

        public WebPPLService(RequestContext reqcxt)
        {
            rc = reqcxt;
        }
        
        public override UserPrincipal GetCurrent()
        {
            UserPrincipal u = null;
            var uid = CookiesQ.GetValue(UserConstants.UID);
            if (string.IsNullOrEmpty(uid))
            {
                uid = UserIdentity.GenerateId(); 
            }
            u = Caches.GetValue(uid); 
            Cookies.SetValue(UserConstants.UID, uid);
            return u;
        }

        public override void Clear()
        {
            var uid = CookiesQ.GetValue(UserConstants.UID);
            
            if (!string.IsNullOrEmpty(uid) && Caches.ContainsKey(uid))
            {
                Caches.Remove(uid);
            }
            base.Clear();
        }

        public override void SetPrincipal(UserPrincipal ppl)
        {
            var uid = CookiesQ.GetValue(UserConstants.UID);
            if (string.IsNullOrEmpty(uid))
            {
                uid = UserIdentity.GenerateId();
                Cookies.SetValue(UserConstants.UID, uid);
            }
            Caches[uid] = ppl;
        }
    }
}
