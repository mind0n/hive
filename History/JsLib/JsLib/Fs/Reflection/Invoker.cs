using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Fs.Entities;
using System.Reflection;
using System.Xml.Linq;

namespace Fs.Reflection
{
	public class InvokeException : CustomException
	{
		public InvokeException(string content) : base(content) { }
		public InvokeException(string content, params object[] format) : base(content, format) { }
	}
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public class InvokableMethodAttribute : Attribute
	{
		//public delegate InvokeResult DlgBeforeInvoke(params object[] parlist);
		public enum ParamTypes : int
		{
			DictParams
			, XElement
			, ObjectArray
		}
		//public Delegate BeforeInvoke;
		public ParamTypes ParamType;
		public InvokableMethodAttribute(ParamTypes paramType)
		{
			ParamType = paramType;
		}
		//public InvokableMethodAttribute(ParamTypes paramType, Delegate beforeInvoke)
		//{
		//    ParamType = paramType;
		//    BeforeInvoke = beforeInvoke;
		//}
	}
	public class InvokeResult
	{
		public bool HasError
		{
			get
			{
				return Errors != null;
			}
		}
		public Exception Errors;
		public string MethodName;
		protected object vresult;
		public InvokeResult(string methodName, object rlt, Exception exp)
		{
			vresult = rlt;
			Errors = exp;
			MethodName = methodName;
		}
		public T ReturnValue<T>()
		{
			return (T)vresult;
		}
	}
	public class Invoker
	{
		public static InvokeResult Invoke(object vtarget, AttributeInfo ai, object[] param)
		{
			object rlt;
			if (ai == null)
			{
				return new InvokeResult(ai.MemberInfo.Name, null, new InvokeException("Incorrect method name ({0})", ai.MemberInfo.Name));
			}
			else
			{
				string method = ai.MemberInfo.Name;
				InvokableMethodAttribute attr = ai.GetAttribute<InvokableMethodAttribute>();
				if (attr != null)
				{
					try
					{
						//if (attr.BeforeInvoke != null)
						//{
						//    InvokeResult ir = attr.BeforeInvoke.DynamicInvoke(param) as InvokeResult;
						//    if (ir.HasError)
						//    {
						//        return ir;
						//    }
						//}
						rlt = (ai.MemberInfo as MethodInfo).Invoke(vtarget, param);
						return new InvokeResult(method, rlt, null);
					}
					catch (Exception err)
					{
						Exceptions.LogOnly(err);
						return new InvokeResult(method, null, err);
					}
				}
				return new InvokeResult(method, null, new InvokeException("Invalid method call to ({0})", method));
			}
		}
		public static InvokeResult Invoke(object vtarget, string method, object[] param, InvokableMethodAttribute.ParamTypes paramType)
		{
			AttributeInfo ai = TypeHelper.GetMethodCustomAttribute(vtarget, method);
			InvokableMethodAttribute attr = ai.GetAttribute<InvokableMethodAttribute>();
			if (ai != null && attr.ParamType == paramType)
			{
				return Invoke(vtarget, ai, param);
			}
			return new InvokeResult(method, null, new InvokeException("Invalid method call to ({0})", method));
			//object rlt;
			//if (!string.IsNullOrEmpty(method))
			//{
			//    MethodInfo mi = vtarget.GetType().GetMethod(method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			//    if (mi == null)
			//    {
			//        return new InvokeResult(method, null, new InvokeException("Incorrect method name ({0})", method));
			//    }
			//    else
			//    {
			//        InvokableMethodAttribute attr = TypeHelper.GetCustomAttribute<InvokableMethodAttribute>(mi);
			//        if (attr != null)
			//        {
			//            try
			//            {
			//                if (attr.ParamType == paramType)
			//                {
			//                    rlt = mi.Invoke(vtarget, param);
			//                    return new InvokeResult(method, rlt, null);
			//                }
			//                return new InvokeResult(method, null, new InvokeException("Parameter type mismatch ({0} needed, not {1})", attr.ParamType.ToString(), paramType.ToString()));
			//            }
			//            catch (Exception err)
			//            {
			//                Exceptions.LogOnly(err);
			//                return new InvokeResult(method, null, err);
			//            }
			//        }
			//    }
			//}
			//return new InvokeResult(method, null, new InvokeException("Method name missing"));
		}
	}

}
