using Joy.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace Joy.Service
{
    public class TestInstance : Instance
    {
        protected FileCfg<TestInstanceSettings> cfg = new FileCfg<TestInstanceSettings>();
        public override InstanceSettings Settings
        {
            get
            {
                return cfg.Instance;
            }
        }
        public string Email()
        {
            return SendEmail();
        }

        protected static string SendEmail()
        {
            try
            {
                var s = new GmailSettings { Uname = "weerspecial@gmail.com" };
                s.Send();
                return "Email sent successful";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public Stream Img(string url, string type)
        {
            cx.OutgoingResponse.ContentType = "image/{0}".Fmt(type);
            //return LoadFileStream(url);
            return Settings.Provider.LoadStream(url);
        }
        public string Read(string url, string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                type = "html";
            }
            cx.OutgoingResponse.ContentType = "text/{0}".Fmt(type);
            //return LoadFileContent(url);
            return Settings.Provider.LoadContent(url);
        }
        public string Css()
        {
            cx.OutgoingResponse.ContentType = "text/css";
            //return LoadFileContent("/testhome.css");
            return Settings.Provider.LoadContent("/testhome.css");
        }
        public string Test(string arg1 = null, string arg2 = null)
        {
            //return LoadFileContent("/testhome.html");
            return Settings.Provider.LoadContent("/testhome.html");
        }
        public string TestResult(string arg)
        {
            Thread.Sleep(1500);
            var r = new ServiceResult { Result = arg };
            return r.ToJson();
        }
    }
    public class TestInstanceSettings : InstanceSettings
    {

    }
}
