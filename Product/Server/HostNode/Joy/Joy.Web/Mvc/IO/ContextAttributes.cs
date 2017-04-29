using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Joy.Core;
using Joy.Web.Mvc.Security;
using Joy.Core.Encode;

namespace Joy.Web.Mvc.IO
{
    public abstract class ContextAttribute : Attribute
    {
        public abstract object Retrieve(HttpContext context, string key, ContextAttribute att = null);
        public abstract void Save(HttpContext context, string key, object value, ContextAttribute att = null);
        public string Postfix;
        protected internal Type type;
        protected internal object instance;
    }
    public class CacheAttribute : ContextAttribute
    {
        public override object Retrieve(HttpContext context, string key, ContextAttribute att = null)
        {
            var k = MakeCacheKey(key, att);
            if (!string.IsNullOrEmpty(k))
            {
                return SimpleCache.Instance.Get(k); //context.Cache[k];
            }
            return null;
        }

        public override void Save(HttpContext context, string key, object value, ContextAttribute att = null)
        {
            var k = MakeCacheKey(key, att);
            //context.Cache.Add(k, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), System.Web.Caching.CacheItemPriority.Normal, null);
            SimpleCache.Instance[k] = value;
        }

        private string MakeCacheKey(string key, ContextAttribute att)
        {
            if (type == null || string.IsNullOrEmpty(att.Postfix))
            {
                return key;
            }
            else
            {
                var pi = type.GetProperty(this.Postfix);
                string appendix = null;
                if (pi == null)
                {
                    appendix = this.Postfix;
                }
                else
                {
                    appendix = (string)pi.GetValue(instance);
                }
                if (string.IsNullOrEmpty(appendix))
                {
                    return null;
                }
                return key + "_" + appendix;
            }
        }
    }
    public class SessionAttribute : ContextAttribute
    {
        public override object Retrieve(HttpContext context, string key, ContextAttribute att = null)
        {
            if (context.Session == null)
            {
                return null;
            }
            return context.Session[key];
        }
        public override void Save(HttpContext context, string key, object value, ContextAttribute att = null)
        {
            if (context.Session == null)
            {
                return;
            }
            context.Session[key] = value;
        }
    }
    public class CookieAttribute : ContextAttribute
    {
        public bool IsHttpOnly;

        public override object Retrieve(HttpContext context, string key, ContextAttribute att = null)
        {
            var creq = context.Request.Cookies[key];
            HttpCookie c = null;
            if (creq != null && !string.IsNullOrEmpty(creq.Value))
            {
                c = creq;
                c.Expires = DateTime.Now.AddMinutes(20);
                context.Response.Cookies.Set(c);
            }
            if (c == null)
            {
                return null;
            }
            var r = c.Value;
            //var rr = c.Values[key];
            //var rlt = string.IsNullOrEmpty(rr) ? r : rr;
            //return rlt.UrlDecode();
            return r.UrlDecode();
        }

        public override void Save(HttpContext context, string key, object value, ContextAttribute att = null)
        {
            var at = att as CookieAttribute;
            var v = value == null ? string.Empty : value.ToString();
            var cc = new HttpCookie(key)
            {
                Expires = DateTime.Now.AddMinutes(20)
            };
            //cc.Values.Add(key, v);
            cc.Value = v;
            if (at != null)
            {
                cc.HttpOnly = at.IsHttpOnly;
            }
            //context.Request.Cookies.Set(c);
            context.Response.Cookies.Set(cc);
        }
    }
}
