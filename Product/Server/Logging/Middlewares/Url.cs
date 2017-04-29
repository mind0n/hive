using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Logger;
using CoreJson;

namespace Middlewares
{
    public class Url
    {
        public string Primary
        {
            get
            {
                if (list.Length > 0)
                {
                    return list[0];
                }
                return string.Empty;
            }
        }
        public string Secondary
        {
            get
            {
                if (list.Length > 1)
                {
					var rlt = list[1];
	                if (rlt.IndexOf('?') >= 0)
	                {
		                rlt = rlt.Split('?')[0];
	                }
	                return rlt;
                }
                return string.Empty;
            }
        }
        public Hashtable Args { get; protected set; }
        public bool HasUrlArgs
        {
            get
            {
                return list.Length > 2;
            }
        }
        public List<string> UrlArgs
        {
            get
            {
                List<string> rlt = new List<string>();
                for (int i = 2; i < list.Length; i++)
                {
                    var t = list[i];
                    rlt.Add(t);
                }
                return rlt;
            }
        }
        public bool IsAction
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Primary) && !string.IsNullOrWhiteSpace(Secondary);
            }
        }
        public bool IsRoot
        {
            get
            {
                return string.IsNullOrWhiteSpace(Primary);
            }
        }
        public bool HasSecondary
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Secondary);
            }
        }
        protected string[] list;
        protected string raw;

        public Url(string url)
        {
            raw = url;
            Args = new Hashtable();
            Parse(url);
        }
        public string ContentType(dynamic mime)
        {
            var ext = raw.PathExt('/');
            var ct = "text/html";
            Dobj.Enum(mime, new Func<string, dynamic, bool>((i, o) =>
            {
                if (string.Equals(i, ext, StringComparison.OrdinalIgnoreCase))
                {
                    ct = o;
                    return true;
                }
                else if (string.Equals(i, "*"))
                {
                    ct = o;
                    return true;
                }
                return false;
            }));
            return ct;
        }
        public Stream MapResource(string asmname = null)
        {
            Assembly asm = null;
            if (string.IsNullOrWhiteSpace(asmname))
            {
                asm = Assembly.GetCallingAssembly();
            }
            else
            {
                asm = Assembly.Load(asmname);
            }
            if (asm != null)
            {
                var sub = new string[list.Length - 1];
                for (int i = 1; i < list.Length; i++)
                {
                    sub[i - 1] = list[i];
                }
                var s = asmname + "." + string.Join(".", sub);
                var names = asm.GetManifestResourceNames();
                foreach (var i in names)
                {
                    if (string.Equals(i,s, StringComparison.OrdinalIgnoreCase))
                    {
                        return asm.GetManifestResourceStream(i);
                    }
                }
            }
            return null;
        }
        public Stream MapPath(string basedir)
        {
            var s =  raw.PathMap(basedir);
            if (File.Exists(s))
            {
                return File.Open(s, FileMode.Open);
            }
            return null;
        }

        protected void Parse(string url)
        {
            list = new string[] { string.Empty };
            if (url.StartsWith("/"))
            {
                url = url.Substring(1);
            }
            if (!string.IsNullOrWhiteSpace(url))
            {
                list = url.Split('/');
            }
            Args = url.UrlArgs();
        }
    }
}
