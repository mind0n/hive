using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Joy.Core.Reflect
{
    public class JoyProxy<T> : RealProxy 
    {
        protected Type type;
        protected T instance;
        public JoyProxy()
            : base(typeof (T))
        {
            this.type = typeof(T);
            this.instance = (T)GetTransparentProxy();
        }
        public T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)GetTransparentProxy();
                }
                return instance;
            }
        }

        public override System.Runtime.Remoting.Messaging.IMessage Invoke(System.Runtime.Remoting.Messaging.IMessage msg)
        {
            var mc = (IMethodCallMessage)msg;
            var m = new MethodCallMessageWrapper(mc);
            var rm = new ReturnMessage(MethodInvoked(m), null, 0, m.LogicalCallContext, mc);
            var r = new MethodReturnMessageWrapper(rm);
            var n = r.TypeName;

            return rm;
        }

        protected virtual object MethodInvoked(MethodCallMessageWrapper msg)
        {
            var mi = msg.MethodBase;
            var name = msg.MethodName;
            bool isget = false;
            bool isproperty = false;

            if (mi != null)
            {
                MemberInfo info = null;
                if (name.IndexOf("get_") == 0 || name.IndexOf("set_") == 0)
                {
                    name = name.Substring(4);
                    info = type.GetProperty(name);
                    isproperty = true;
                    isget = msg.MethodName.IndexOf("get_") == 0;
                }
                else
                {
                    info = mi;
                }
                return MethodInvoked(info, msg.Args, isproperty, isget);

                //JoyAttribute<T>[] cas = null;
                //if (info.HasAttr(out cas))
                //{
                //    object rlt = null;
                //    foreach (var i in cas)
                //    {
                //        rlt = msg.InvokeAttributes(Instance, i, isproperty, isget, name);
                //    }
                //    return rlt;
                //}

            }
            return null;
        }

        protected virtual object MethodInvoked(MemberInfo info, object[] args, bool isprop = false, bool isget = true)
        {
            return null;
        }
    }
}
