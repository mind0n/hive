using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.ServiceModel;
using System.Text;
using System.Web;

namespace Joy.Core.Reflect
{
    public class CfgProxy<T> : JoyProxy<T> 
    {
        protected Type type;

        public CfgProxy()
        {
            type = typeof(T);
        }

        protected override object MethodInvoked(MemberInfo info, object[] args, bool isprop = false, bool isget = true)
        {
            string url = "net.pipe://localhost/CfgPipe";
            return url.InvokeService(new Func<T, object>(c => info.Call(c, args)), new NetNamedPipeBinding());
        }
    }
}
