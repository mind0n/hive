using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Joy.Core;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using Joy.Web.Configs;
using Joy.Web.Mvc.IO;

namespace Joy.Web.Handlers
{
    public class ResHandler : ServiceHandler
    {
        static FileCfg<ExtConfig> config;
        static ResHandler()
        {
            config = new FileCfg<ExtConfig>("/configs/exts.json".MapPath());
        }

        [Action(OutputType = ActionOutputType.Json)]
        public string Test(int id)
        {
            return string.Format("Success! {0}", id);
        }

        [Action(OutputType = ActionOutputType.Raw, ContentType = "text/javascript")]
        public string Js(string path)
        {
            return Load(path);
        }

        [Action(OutputType = ActionOutputType.Raw, ContentType = "text/css")]
        public string Css(string path)
        {
            return Load(path);
        }

        public string Read(string path)
        {
            var ext = Groups[WebConstants.const_action];
            if (ext != null && !string.IsNullOrEmpty(ext.Value) && config.Instance.Exts.ContainsKey(ext.Value))
            {
                var i = config.Instance.Exts[ext.Value];
                return Load(path);
            }
            Response.StatusCode = 404;
            return null;
        }
        protected string Load(string path)
        {
            if (path.IndexOf('/') > 0)
            {
                var p = path.MapPath();
                if (File.Exists(p))
                {
                    var c = File.ReadAllText(p);
                    return c;
                }
            }
            else
            {
                var l = path.Split(',');
                if (l.Length < 2)
                {
                    return LoadFromAsm(l[0]);
                }
                else
                {
                    return LoadFromAsm(l[1], l[0]);
                }
            }
            return null;
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