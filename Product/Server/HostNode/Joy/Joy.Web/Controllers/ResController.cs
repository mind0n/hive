using Joy.Core;
using Joy.Web.Configs;
using Joy.Web.Mvc;
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Joy.Web.Controllers
{
    [AllowAnonymous]
    public class ResController : JoyController
    {
        static FileCfg<ExtConfig> config;
        static Regex reg = new Regex("(?<url>.*[\\.](?<ext>\\w*))$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public ResController() : base(false, "Load")
        {
            config = new FileCfg<ExtConfig>("/configs/exts.json".MapPath());
        }

        public ActionResult Load(string url)
        {
            return config.Instance.ValidateAndGetContent(url, new Func<ExtItem, string, ActionResult>(delegate(ExtItem i, string u)
            {
                var l = u.Split(',');
                string rr = null;
                if (l.Length < 2)
                {
                    rr = LoadFromAsm(l[0]);
                }
                else
                {
                    rr = LoadFromAsm(l[1], l[0]);
                }
                if (string.IsNullOrEmpty(rr))
                {
                    return new HttpStatusCodeResult(403);
                }
                else
                {
#if DEBUG
                    Logger.Log(i.ContentType + "=>" + url);
#endif
                    return new ContentResult { Content = rr, ContentType = i.ContentType };
                }
            }), reg);
        }

        private static string LoadFromAsm(string url, string asmName = null)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            if (!string.IsNullOrEmpty(asmName))
            {
                asm = Assembly.Load(asmName);
            }
            using (var s = asm.GetManifestResourceStream(url))
            {
                if (s != null)
                {
                    using (var sr = new StreamReader(s))
                    {
                        var r = sr.ReadToEnd();
                        return r;
                    }
                }
            }
            return null;
        }

	}
}