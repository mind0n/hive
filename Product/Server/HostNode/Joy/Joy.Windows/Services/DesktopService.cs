using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using Joy.Core;
using Joy.Windows;
using Joy.Windows.Events;
using Joy.Core.Encode;
using System.ServiceModel.Channels;

namespace Joy.Windows.Services
{
    
    public class DesktopService : IDesktopService
    {
        Clients clients = new Clients();
        public delegate void ConnectDgt(RemoteEndpointMessageProperty epp, string action, params string[] args);
        public event ConnectDgt OnConnect;
        public const string Pwd = "EfRGL8mt";
        EvtFactory fac = new EvtFactory();

        public DesktopService()
        {
            
        }

        public ServiceResult C(string n)
        {
            try
            {
                //var cx = OperationContext.Current;
                //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "svc.txt", u);
                if (!VerifyClient("Capture", n))
                {
                    return null;
                }
                var s = Wall.CaptureScreenSec(Pwd);
                return new ServiceResult { Result = s };
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
                return null;
            }
        }


        protected virtual bool VerifyClient(string action, params string[] args)
        {
            try
            {
                if (OnConnect != null)
                {
                    if (args == null)
                    {
                        return false;
                    }
                    var cx = HttpContext.Current;
                    OperationContext context = OperationContext.Current;
                    MessageProperties mp = context.IncomingMessageProperties;
                    RemoteEndpointMessageProperty epp = mp[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                    var hp = mp[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
                    if (hp.Headers["User-Agent"] != null)
                    {
                        return false;
                    }
                    var n = args[0];
                    n = n.Replace("\"", "/");
                    var u = OpenSSL.OpenSSLDecrypt(n, Pwd);
                    if (!clients.Contains(epp.Address))
                    {
                        clients.Add(new Client { Address = epp.Address, Port = epp.Port });
                        OnConnect(epp, action, u);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
                return false;
            }
        }

        public bool T(string json)
        {
            // {"name":0,"btn":0,"x":0,"y":0,"key":"k","cw":0,"ch":0}
            try
            {
                fac.ParseEvent(json);
                return true;
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
                return false;
            }
        }

        // http://localhost:8000/desktopservice/i/d/cmd/n/
        public bool I(string desktop, string cmd, string args)
        {
            try
            {
                if (!cmd.IsAbsolutePath())
                {
                    if (cmd.EndsWith(".exe") || cmd.EndsWith(".bat"))
                    {
                        cmd = cmd.MakeAbsolutePath();
                    }
                }
                //Desktops.DesktopOpenInput();

                if (string.IsNullOrEmpty(desktop) || string.Equals(desktop, "d", StringComparison.OrdinalIgnoreCase))
                {
                    Process.Start(cmd, args);
                }
                else
                {
                    Desktops.ProcessCreate(desktop, cmd, args);
                }
                return true;
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
                return false;
            }
        }


        public bool Tl(Stream data)
        {
            try
            {
                var json = new StreamReader(data).ReadToEnd();
                //var j = HttpUtility.UrlDecode(json);
                json = OpenSSL.OpenSSLDecrypt(json, Pwd);
                fac.ParseEvents(json);
                return true;
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
                return false;
            }
        }


        public Stream G(string path, string type)
        {
            using (var ms = new MemoryStream())
            {
                if (string.IsNullOrEmpty(path))
                {
                    return ms;
                }
                var p = path.Replace("|", "\\");
                if (string.IsNullOrEmpty(type))
                {
                    type = "text/plain";
                }
                if (p.IndexOf(":") < 0)
                {
                    p = AppDomain.CurrentDomain.BaseDirectory + p;
                }
                if (File.Exists(p))
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = type;
                    var s = File.OpenRead(p);
                    return s;
                }
                return ms;
            }
        }
    }

    [ServiceContract]
    public interface IDesktopService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "c/{*n}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ServiceResult C(string n);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "t/{*json}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool T(string json);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "tl", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool Tl(Stream json);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "i/{desktop}/{cmd}/{*args}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool I(string desktop, string cmd, string args);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "d/{path}/{*type}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream G(string path, string type);
    }


}
