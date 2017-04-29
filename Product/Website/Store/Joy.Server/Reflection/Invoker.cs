using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Joy.Server.Entities;
using System.Reflection;
using System.Xml.Linq;
using Joy.Common;
using Joy.Core.Logging;

namespace Joy.Server.Reflection
{
	public class InvokeException : CustomException
	{
		public InvokeException(string content) : base(content) { }
		public InvokeException(string content, params object[] format) : base(content, format) { }
	}
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public class InvokableMethodAttribute : Attribute
	{
		public enum ParamTypes : int
		{
			DictParams
			, XElement
			, ObjectArray
		}
		public ParamTypes ParamType;
		public InvokableMethodAttribute(ParamTypes paramType)
		{
			ParamType = paramType;
		}
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
		}
	}

}
