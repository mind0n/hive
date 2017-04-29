using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Joy.Core
{
    public abstract class JoyAttribute<T>
    {
        public object Instance;
        public abstract object Retrieve(string name);
        public abstract void Save(string name, object value);
    }
    public class MethodAttribute : Attribute
    {
        public virtual object[] OnInvoke(object instance, ParameterInfo[] pars, object[] args)
        {
            var opars = new object[pars.Length];
            if (args != null && args.Length > 0)
            {
                for (int i = 0; i < opars.Length; i++)
                {
                    if (i < args.Length)
                    {
                        opars[i] = args[i];
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return opars;
        }
    }
}
