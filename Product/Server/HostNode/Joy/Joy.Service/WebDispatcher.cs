using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Joy.Core;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Security;

namespace Joy.Service
{
    /// <summary>
    /// netsh http add urlacl url=http://+:9001/ user=dell2012dev\mind0n listen=yes
    /// </summary>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public class WebDispatcher : IDispatcher
    {
        private int p;
        public int Port
        {
            get { return p; }
        }
        FileCfg<WebDispatcherSettings> cfg = new FileCfg<WebDispatcherSettings>();
        InstanceCache cache = new InstanceCache();

        public WebDispatcher(int port = 9000, Func<string> callback = null)
        {
            p = port;
            if (cfg.Instance.IsAddUrlAcl && callback != null)
            {
                string pwd = null;
                pwd = callback();
                if (string.IsNullOrEmpty(pwd))
                {
                    return;
                }
                else
                {
                    var ss = pwd.Secure();
                    string urlAcl = "http add urlacl url=http://+:{0}/ user={1}\\{2} listen=yes".Fmt(port, cfg.Instance.Domain, Environment.UserName);
                    var ps = new ProcessStartInfo("netsh", urlAcl);
                    ps.Domain = cfg.Instance.Domain;
                    ps.UserName = cfg.Instance.Uname;
                    ps.Password = ss;
                    ps.UseShellExecute = false;
                    ps.RedirectStandardError = true;
                    ps.RedirectStandardOutput = true;
                    var pp = Process.Start(ps);
                    var err = pp.StandardError.ReadToEnd();
                    var ot = pp.StandardOutput.ReadToEnd();
                    if (!string.IsNullOrEmpty(ot))
                    {
                        Logger.Log(ot);
                    }
                    if (!string.IsNullOrEmpty(err))
                    {
                        Logger.Log(err);
                    }
                    //Debugger.Break();
                    //Process.Start("netsh", urlAcl, cfg.Instance.Uname, ss, cfg.Instance.Domain);
                    cfg.Instance.IsAddUrlAcl = false;
                    cfg.Save();
                }
            }
        }
        [OperationBehavior(AutoDisposeParameters = true)]
        public Stream Dispatch(string cname, string aname, string pars)
        {
            var cx = WebOperationContext.Current;
            try
            {
                cx.OutgoingResponse.ContentType = "text/html";
                if (string.IsNullOrEmpty(cname) || string.IsNullOrEmpty(aname))
                {
                    return NotFound(cx);
                }
                if (!cfg.Instance.Mapping.ContainsKey(cname))
                {
                    return NotFound(cx);
                }
                var n = cfg.Instance.Mapping[cname];
                if (string.IsNullOrEmpty(n))
                {
                    return NotFound(cx);
                }
                
                Instance ins = GetInstance(cname, cx);

                if (ins == null)
                {
                    return NotFound(cx);
                }
                else
                {
                    var o = aname.Call(ins, pars.Split('/'));
                    return RetrieveStreamResult(cx, o);
                }
            }
            catch (Exception ex)
            {
                cx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                Error.Handle(ex);
                return string.Empty.Stream();
            }
        }

        private static Stream RetrieveStreamResult(WebOperationContext cx, object o)
        {
            if (o == null)
            {
                return NotFound(cx);
            }
            if (o is ServiceResult)
            {
                ServiceResult r = (ServiceResult)o;
                return r.StringResult.Stream();
            }
            else if (o is Stream)
            {
                return (Stream)o;
            }
            else
            {
                var s = o.ToString();
                return s.Stream();
            }
        }

        private static string RetrieveStringResult(WebOperationContext cx, object o)
        {
            if (o == null)
            {
                cx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return string.Empty;
            }
            if (o is ServiceResult)
            {
                ServiceResult r = (ServiceResult)o;
                return r.StringResult;
            }
            else if (o is Stream)
            {
                var s = new StreamReader((Stream)o).ReadToEnd();
                return s;
            }
            else
            {
                var s = o.ToString();
                return s;
            }
        }

        private Instance GetInstance(string cname, WebOperationContext cx)
        {
            Instance ins = null;
            if (ins == null)
            {
                var t = Type.GetType(cfg.Instance.Mapping[cname]);
                if (t == null)
                {
                    return null;
                }
                ins = Activator.CreateInstance(t) as Instance;
                ins.cx = cx;
            }
            return ins;
        }

        private static Stream NotFound(WebOperationContext cx)
        {
            cx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
            using (var ms = new MemoryStream())
            {
                return ms;
            }
        }


        public Stream Invoke(Stream formdata)
        {
            //{"actions":[{"ins":"TestInstance","act":"TestResult"}]}
            const string kw = "reqjson=";
            var cx = WebOperationContext.Current;
            try
            {
                var req = ParseFormStream(formdata);
                if (req == null)
                {
                    return NotFound(cx);
                }
                foreach (var i in req.actions)
                {
                    var ins = GetInstance(i.ins, cx);
                    if (ins == null)
                    {
                        return NotFound(cx);
                    }
                    else
                    {
                        var o = i.act.Call(ins, string.IsNullOrEmpty(i.arg) ? null : i.arg.Split('/'));
                        i.rlt = RetrieveStringResult(cx, o);
                    }
                }
                var rs = req.ToJson();
                return rs.Stream();
            }
            catch (Exception ex)
            {
                cx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                Error.Handle(ex);
                return string.Empty.Stream();
            }
        }

        private static QueueRequest ParseFormStream(Stream formdata)
        {
            var s = new StreamReader(formdata).ReadToEnd();
            var reg = new Regex("json=(?<rq>[a-zA-Z0-9%]*)");
            var m = reg.Match(s);
            var g = m.Groups["rq"];
            var ss = g.Value;

            var req = string.IsNullOrEmpty(ss) ? null : ss.UrlDecode().FromJson<QueueRequest>();
            return req;
        }
    }



    public class WebDispatcherSettings
    {
        public Dictionary<string, string> Mapping { get; set; }

        public bool IsAddUrlAcl { get; set; }

        public string Uname { get; set; }

        public string Domain { get; set; }
    }

    public class InstanceCache : Dictionary<string, Instance>
    {
    }
}
