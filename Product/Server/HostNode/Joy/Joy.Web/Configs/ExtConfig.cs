using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Joy.Web.Configs
{
    [Serializable]
    class ExtConfig
    {
        public Dictionary<string, ExtItem> Exts = new Dictionary<string, ExtItem>();
        public ActionResult ValidateAndGetContent(string url, Func<ExtItem, string, ActionResult> callback, Regex reg = null)
        {
            string ext = url;
            if (reg != null)
            {
                var m = reg.Match(url);
                if (m.Success)
                {
                    ext = GetMatchValue(m, "ext");
                    url = GetMatchValue(m, "url");
                }
            }
            if (Exts.ContainsKey(ext) && callback != null)
            {
                return callback(Exts[ext], url);
            }
            return new HttpStatusCodeResult(403);
        }

        private static string GetMatchValue(Match m, string n)
        {
            var g = m.Groups[n];
            if (g != null)
            {
                return g.Value;
            }
            return string.Empty;
        }
    }

    [Serializable]
    class ExtItem
    {
        public string ContentType;
    }
}