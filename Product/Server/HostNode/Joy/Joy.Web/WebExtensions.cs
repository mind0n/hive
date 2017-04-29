using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Joy.Web.Mvc.IO;

namespace Joy.Web
{
    public static class WebExtensions
    {
        public static object InvokeAttributes<T>(this MethodCallMessageWrapper msg, T Instance, HttpContext cx, ContextAttribute att, bool isproperty, bool isget, string name)
	    {
            object rlt = null;
            if (att != null)
            {
                att.type = typeof(T);
                att.instance = Instance;
                if (isproperty)
                {
                    if (isget)
                    {
                        rlt = att.Retrieve(cx, name, att);
                    }
                    else if (msg.ArgCount > 0)
                    {
                        att.Save(cx, name, msg.Args[0], att);
                    }
                }
            }
            return rlt;
	    }   
    }
}