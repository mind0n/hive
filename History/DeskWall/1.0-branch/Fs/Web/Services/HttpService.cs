using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;
using System.Web.UI;
using Fs.Data;
using Fs.Entities;
using Fs.Authentication;

namespace Fs.Web.Services
{
	public class IntegratedParams : Dictionary<string, object>
	{
		public IntegratedParams SelectByPrefix(string prefix)
		{
			string newkey;
			int l;
			IntegratedParams rlt;
			if (string.IsNullOrEmpty(prefix))
			{
				return this;
			}
			rlt = new IntegratedParams();
			l = prefix.Length;
			foreach (string key in Keys)
			{
				if (key.IndexOf(prefix) == 0)
				{
					newkey = key.Substring(l, key.Length - l);
					if (string.IsNullOrEmpty(newkey))
					{
						newkey = Guid.NewGuid().ToString();
					}
					rlt[newkey] = this[key];
				}
			}
			return rlt;
		}
	}
	public class HttpMethodInvoker
	{
		//public string Result;
		//public HttpRequest Request;
		//public object ObjInstance;
		//public List<string> MethodsFilter;
		public static string Invoke(HttpRequest request, object obj)
		{
			string methodName, Result = "";
			NameValueCollection Form = request.Form;
			methodName = Form["methodname"];
			if (string.IsNullOrEmpty(methodName))
			{
				Result = "Error: Method name missing.";
			}
			else
			{
				Result = (string)Call(obj, methodName, request);
			}
			return Result;
		}
		protected static object[] ParseParams(NameValueCollection Form)
		{
			object[] rlt;
			List<object> pars = new List<object>();
			//NameValueCollection Form = request.Form;
			RequestHelper.EnumKeys(Form, "par_", delegate(string key, string val)
			{
				pars.Add(val);
			});
			rlt = new object[pars.Count];
			for (int i = 0; i < pars.Count; i++)
			{
				rlt[i] = pars[i];
			}
			return rlt;
		}
		protected static object[] ParseNParams(NameValueCollection Form)
		{
			object[] rlt;
			List<object> pars = new List<object>();
			//NameValueCollection Form = request.Form;
			RequestHelper.EnumKeys(Form, "par_", delegate(string key, string val)
			{
				pars.Add(key + ":" + val);
			});
			rlt = new object[pars.Count];
			for (int i = 0; i < pars.Count; i++)
			{
				rlt[i] = pars[i];
			}
			return rlt;
		}
		protected static IntegratedParams ParseNamedParams(NameValueCollection Form)
		{
			IntegratedParams rlt = new IntegratedParams();
			//List<object> pars = new List<object>();
			//NameValueCollection Form = request.Form;
			RequestHelper.EnumKeys(Form, "par_", delegate(string key, string val)
			{
				rlt.Add(key, val);
			});
			//rlt = new object[pars.Count];
			//for (int i = 0; i < pars.Count; i++)
			//{
			//    rlt[i] = pars[i];
			//}
			return rlt;
		}
		protected static object Call(object Obj, string MethodName, HttpRequest request)
		{
			object rlt;
			object[] Params;
			IntegratedParams InPars;
			try
			{
				MethodInfo dynMethod = Obj.GetType().GetMethod(MethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				object[] attrs = dynMethod.GetCustomAttributes(typeof(ScriptMethodAttribute), true);
				ScriptMethodAttribute sma = attrs[0] as ScriptMethodAttribute;
				if (attrs.Length > 0)
				{
					HttpCookie hc = request.Cookies["Remember"];
					if (hc != null)
					{
						AuthManager.ShouldRemember = !string.IsNullOrEmpty(hc.Value);
					}
					else
					{
						AuthManager.ShouldRemember = false;
					}
					if (sma.ParamType == ScriptMethodParamType.Explicit)
					{
						Params = ParseParams(request.Form);
					}
					else if (sma.ParamType == ScriptMethodParamType.Integrated)
					{
						InPars = ParseNamedParams(request.Form);
						//Params = ParseNParams(request.Form);
						Params = new object[] { InPars };
					}
					else if (sma.ParamType == ScriptMethodParamType.Raw)
					{
						Params = new object[] { request };
					}
					else
					{
						Params = ParseParams(request.Form);
					}
					rlt = dynMethod.Invoke(Obj, Params);
				}
				else
				{
					rlt = "Error: Method not available (" + MethodName + ")";
				}
			}
			catch (Exception err)
			{
				rlt = "Error: Method not exist or parameter error." + err.ToString();
				Exceptions.LogOnly(rlt.ToString());
			}
			return rlt;
		}
	}

}
