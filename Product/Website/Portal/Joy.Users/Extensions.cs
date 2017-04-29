using Joy.Core;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace Joy.Users
{
    public static class Extensions
    {
        public static void AddRoles(this List<URole> roles, URole[] rs)
        {
            foreach (var i in rs)
            {
                roles.AddUniqueRole(i);
            }
        }
        public static void AddUniqueRole(this List<URole> roles, URole role)
        {
            roles.RemoveRole(role);
            roles.Add(role);
        }

        public static void RemoveRole(this List<URole> roles, URole role)
        {
            for (int i = 0, l = roles.Count; i < l; i++)
            {
                var r = roles[i];
                if (string.Equals(r.Name, role.Name, StringComparison.OrdinalIgnoreCase))
                {
                    roles.Remove(r);
                    l--;
                }
            }
        }

        public static string SecClean(this string secret)
        {
            string s = HttpContext.Current.Server.UrlDecode(secret);

            return s == null ? string.Empty : s.Replace(' ', '+');
        }

        public static bool IsQualified(this URole r, params string[] roles)
        {
            foreach (string role in roles)
            {
                var ur = URoles.Instance.GetRole(role);
                if (ur == null)
                {
                    if (URoles.Instance.EnableAutoAddRole)
                    {
                        URoles.Instance.RegisterRole(r);
                    }
                    else
                    {
                        continue;
                    }
                }
                if (string.Equals(r.Name, role, StringComparison.OrdinalIgnoreCase) ||
                    (ur != null && r.Level >= ur.Level))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsQualified(this string name, URole role)
        {
            if (role == null)
            {
                role = URoles.AnonymousRole;
            }
            var r = URoles.Instance.GetRole(name);
            if (r == null)
            {
                return false;
            }
            var rlt = r.Level >= role.Level || string.Equals(name, role.Name, StringComparison.OrdinalIgnoreCase);
            return rlt;
        }

        public static void SetValue(this HttpCookieCollection cookies, string name, string value = null, bool isEncode = false)
        {
            var c = new HttpCookie(name);
            if (value == null)
            {
                c.Expires = DateTime.Now - TimeSpan.FromDays(7);
            }
            else
            {
                c.Value = isEncode ? value.UrlEncode() : value;
                c.Expires = DateTime.Now + TimeSpan.FromDays(14);
            }
            c.HttpOnly = false;
            cookies.Set(c);
        }
        public static string GetValue(this HttpCookieCollection cookies, string name)
        {
            if (cookies != null && cookies[name] != null)
            {
                string r = cookies[name].Value;
                if (!string.IsNullOrEmpty(r))
                {
                    return r.UrlDecode();
                }
            }
            return null;
        }

        public static T GetValue<T>(this HttpSessionStateBase sessions, string name)
        {
            if (sessions != null && sessions[name] != null)
            {
                object o = sessions[name];
                if (o != null)
                {
                    return (T) o;
                }
            }
            return default (T);
        }

        public static T GetValue<T>(this Cache caches, string name)
        {
            if (caches != null)
            {
                object o = caches[name];
                if (o != null)
                {
                    return o.ChangeType<T>();
                }
            }
            return default (T);
        }
    }
}
