using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Threading;
using Joy.Core.Reflect;
using System;
using System.Linq.Expressions;
using System.ServiceModel;

namespace Joy.Core
{
    public class ServiceCfg<T, Ti> : IDisposable where Ti : T
    {
        protected string url;
        protected string name;
        protected Binding b;
        protected T proxy;
        protected ChannelFactory<T> fac;
        protected ServiceHost host;
        protected Ti ins;
        protected bool svcInited;
        protected bool svcTerminated;

        public delegate void ChannelCreated(ChannelFactory<T> ch);

        protected event ChannelCreated OnChannelCreated;

        public Ti Instance
        {
            get
            {
                if (ins == null)
                {
                    throw new ArgumentNullException("Config instance null.  Use Proxy instead of Instance in client side.");
                }
                return ins;
            }
        }
        public T Proxy
        {
            get
            {
                if (EqualityComparer<T>.Default.Equals(proxy, default(T)))
                {
                    fac = new ChannelFactory<T>(b, new EndpointAddress(url));
                    if (OnChannelCreated != null)
                    {
                        OnChannelCreated(fac);
                    }
                    proxy = fac.CreateChannel();
                }
                return proxy;
            }
        }

        public ServiceCfg(string svcUrl, string svcname, Func<Binding> bind)
        {
            url = svcUrl;
            b = bind();
            name = svcname;
        }

        public virtual void Setup(Ti i = default (Ti), bool enableHelp = false, bool autoStart = true)
        {
            Setup(i, enableHelp, autoStart, null);
        }

        protected virtual void Setup(Ti i = default (Ti), bool enableHelp = false, bool startImmediately = true, params IEndpointBehavior [] behaviors)
        {
            svcInited = false;
            var th = new Thread(new ThreadStart(delegate
            {
                try
                {
                    var u = url.Fmt(name);
                    if (i == null)
                    {
                        ins = default(Ti);
                        host = new ServiceHost(typeof(Ti), new Uri[]
                    {
                        new Uri(u)
                    });
                    }
                    else
                    {
                        ins = i;
                        host = new ServiceHost(ins, new Uri[]
                    {
                        new Uri(u)
                    });
                    }
                    Logger.Log(u);

                    var ep = host.AddServiceEndpoint(typeof(T), b, name);
                    if (behaviors != null && behaviors.Length > 0)
                    {
                        foreach (var ii in behaviors)
                        {
                            ep.Behaviors.Add(ii);
                        }
                    }
                    InitServiceBehavior(delegate(ServiceBehaviorAttribute bb)
                    {
                        bb.InstanceContextMode = InstanceContextMode.Single;
                    });
                    InitServiceBehavior(delegate(ServiceDebugBehavior bb)
                    {
                        bb.HttpHelpPageEnabled = enableHelp;
                        bb.HttpsHelpPageEnabled = enableHelp;
                    });

                    if (startImmediately)
                    {
                        host.Open();
                    }
                    svcInited = true;
                }
                catch (Exception ex)
                {
                    svcTerminated = true;
                    Error.Handle(ex);
                }
            }));
            th.IsBackground = true;
            th.Start();
        }

        public ServiceHost WaitSetup(int interval = 100)
        {
            while (!svcInited && !svcTerminated)
            {
                Thread.Sleep(interval);
            }
            return host;
        }

        private void InitServiceBehavior<Tb>(Action<Tb> callback = null) where Tb: IServiceBehavior,new()
        {
            var b = host.Description.Behaviors.Find<Tb>();
            if (b == null)
            {
                b = new Tb();
                host.Description.Behaviors.Add(b);
            }
            if (callback != null)
            {
                callback(b);
            }
        }

        public void Dispose()
        {
            fac.DisposeService();
            host.DisposeService();
        }

    }

    public class NamedPipeCfg<Tinterface, T> : ServiceCfg<Tinterface, T> where T : Tinterface
    {
        public NamedPipeCfg(string name = null)
            : base(
                "net.pipe://localhost/{0}".Fmt(string.IsNullOrEmpty(name) ? typeof (T).Name : name), typeof (T).Name,
                () => new NetNamedPipeBinding {MaxReceivedMessageSize = int.MaxValue, MaxBufferSize = int.MaxValue})
        {

        }
    }

    public class WebHttpCfg<Tinterface, T> : ServiceCfg<Tinterface, T> where T : Tinterface
    {
        // http://localhost:8000/t/%7B%22name%22:0,%22btn%22:0,%22x%22:0,%22y%22:0,%22key%22:%22k%22%7D
        public WebHttpCfg(int port = 8000, string name = null)
            : base("http://localhost:{0}/".Fmt(port), (name == null ? typeof(T).Name : name),
                () =>
                {
                    var r = new WebHttpBinding
                    {
                        MaxReceivedMessageSize = int.MaxValue,
                        MaxBufferSize = int.MaxValue,
                        MaxBufferPoolSize = int.MaxValue,
                        AllowCookies = true
                    };
                    r.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                    return r;
                })
        {
            OnChannelCreated += WebHttpCfg_OnChannelCreated;
        }

        void WebHttpCfg_OnChannelCreated(ChannelFactory<Tinterface> ch)
        {
            ch.Endpoint.Behaviors.Add(new WebHttpBehavior());
        }

        public override void Setup(T i = default(T), bool enableHelp = false, bool autoStart = true)
        {
            base.Setup(i, enableHelp, autoStart,
                new WebHttpBehavior {HelpEnabled = enableHelp, FaultExceptionEnabled = true});
        }
    }

    
}
