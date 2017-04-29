using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Joy.Core;
using System.Text.RegularExpressions;

namespace Joy.Service
{
    public class JoyInstance : Instance
    {
        protected FileCfg<JoyInstanceSettings> cfg = new FileCfg<JoyInstanceSettings>();
        public override InstanceSettings Settings
        {
            get
            {
                return cfg.Instance;
            }
        }

        [Action]
        public Stream img(string url)
        {
            var reg = new Regex(".*[.](?<ext>\\w*)");
            if (reg.IsMatch(url))
            {
                var m = reg.Match(url);
                var g = m.Groups["ext"];
                if (!string.IsNullOrEmpty(g.Value))
                {
                    cx.OutgoingResponse.ContentType = "image/" + g.Value;
                    //var file = ParseFilename(url, "." + g.Value);
                    //return LoadFileStream(file);
                    return Settings.Provider.LoadStream(url);
                }
            }
            return Stream.Null;
        }

        //[Action]
        //public Stream fgif(string url)
        //{
        //    cx.OutgoingResponse.ContentType = "image/gif";
        //    var file = ParseFilename(url, ".gif");
        //    return Settings.Provider.LoadStream(file);
        //    //return LoadFileStream(file);
        //}

        //[Action]
        //public string fjs(string url)
        //{
        //    cx.OutgoingResponse.ContentType = "text/javascript";
        //    var file = ParseFilename(url,".js");
        //    //return LoadFileContent(file);
        //    return Settings.Provider.LoadContent(file);
        //}
        //[Action]
        //public string fcs(string url)
        //{
        //    cx.OutgoingResponse.ContentType = "text/css";
        //    var file = ParseFilename(url, ".css");
        //    return Settings.Provider.LoadContent(file);
        //    //return LoadFileContent(file);
        //}

        [Action]
        public Stream Js(string name)
        {
            cx.OutgoingResponse.ContentType = "text/javascript";
            //var file = MakeFilename(name); //ParseFilename(name, "Joy.Service.Js.{0}", ".js");
            return Settings.Provider.LoadStream(name);
            //return LoadResStream(file);
        }

        private string MakeFilename(string name)
        {
            return Settings.Provider.Joiner + name;
        }

        private static string ParseFilename(string name, string ext)
        {
            return string.Empty;
        }
        private static string ParseFilename(string name, string tmp, string ext)
        {
            var file = tmp.Fmt(name);
            if (!name.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
            {
                file += ext;
            }
            return file;
        }

        [Action]
        public Stream Css(string name)
        {
            cx.OutgoingResponse.ContentType = "text/css";
            //var file = ParseFilename(name, "Joy.Service.Js.{0}", ".css");
            return Settings.Provider.LoadStream(name);
            //return LoadResStream(file);
        }
    }
    public class JoyInstanceSettings : InstanceSettings
    {

    }
}
