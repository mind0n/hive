using Joy.Core;
using Joy.Core.Reflect;
using System;
using System.Reflection;
using System.Web;

namespace Joy.Web.Mvc.IO
{
    public class HttpContextProxy<T> : JoyProxy<T> where T:class
    {
        protected Type type;

        protected HttpContext cx
        {
            get
            {
                return System.Web.HttpContext.Current;
            }
        }

        public HttpContextProxy()
        {
            type = typeof(T);
        }

        protected override object MethodInvoked(System.Runtime.Remoting.Messaging.MethodCallMessageWrapper msg)
        {
            var mi = msg.MethodBase;
            var name = msg.MethodName;
            bool isget = false;
            bool isproperty = false;
            
            if (mi != null)
            {
                object[] atts = null;
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
                ContextAttribute[] cas = null;
                if (info.HasAttr(out cas))
                {
                    object rlt = null;
                    foreach (var i in cas)
                    {
                        rlt = msg.InvokeAttributes(Instance, cx, i, isproperty, isget, name);
                    }
                    return rlt;
                }

            }
            return null;
        }
    }
}
