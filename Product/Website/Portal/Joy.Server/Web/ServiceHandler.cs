using Joy.Core;
using Joy.Server.Errors;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Joy.Server.Web
{
	public class ServiceHandler : IRoutable
	{
		public MethodInfo Intent { get; set; }

		public System.Web.Routing.RequestContext RequestContext { get; set; }

		public RouteInfo RouteInfo { get; set; }

		public string[] Params { get; set; }

		public bool IsReusable
		{
			get { return false; }
		}

		protected HttpResponseBase Response
		{
			get { return RequestContext.HttpContext.Response; }
		}

		public virtual void ProcessRequest(HttpContext context)
		{
			context.Response.Clear();
			if (RouteInfo.IsRestful)
			{
				HandleRestfulRequest(context);
			}
			else
			{
				string json = GetFieldValue(ServerConst.Json);
				if (string.IsNullOrEmpty(json))
				{
					HandleNormalRequest();
				}
				else
				{
					HandleJsRequest(json);
				}
			}
			context.Response.End();
		}

		#region Exception Handlers
		private static void HandleInvokeException(MethodInfo currentMethod, Method m)
		{
			m.Error = string.Concat("请求的方法调用失败: ", currentMethod.Name);
		}
		private static void HandleUnknownException(Method m, Exception err)
		{
			m.Error = ErrorHandler.Handle(err).Message;
		}

		private static void HandleInvalidInputException(ParameterInfo currentParameterInfo, Method m)
		{
			string name = "N/A";
			if (currentParameterInfo != null)
			{
				name = currentParameterInfo.Name;
			}
			m.Error = string.Concat("不正确的输入: ", name);
		}
		#endregion Exception Handlers

		#region Request Handlers
		private void HandleJsRequest(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				return;
			}
			JsRequest r = json.FromJson<JsRequest>();
			if (r == null)
			{
				return;
			}
			ProcessJsRequest(r);
		}

		private void HandleNormalRequest()
		{
			JsRequest r = new JsRequest();
			MethodInfo mi = Intent;
			InvokeResult ir = null;
			if (mi != null)
			{
				ir = InvokeMethod(r, mi);
			}
			else
			{
				r.SetException(new ArgumentException(ServerErrorCode.InvalidNormalRequestMethod));
			}
			if (ir == null || !ir.IsPageResponse)
			{
				OutputResponse(r);
			}
		}


		private void HandleRestfulRequest(HttpContext context)
		{
			string method = context.Request.HttpMethod;
			JsRequest r = new JsRequest();
			Method m = r.AddMethod(method);
			if (method.IndexOf("get", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				m.MethodReturnValue = Get(context);
			}
			else if (method.IndexOf("post", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				m.MethodReturnValue = Post(context);
			}
			else if (method.IndexOf("put", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				m.MethodReturnValue = Put(context);
			}
			else if (method.IndexOf("delete", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				m.MethodReturnValue = Delete(context);
			}
			else
			{
				r.SetException(new ArgumentException(ServerErrorCode.InvalidHttpMethod));
			}
			OutputResponse(r);
		}
		private void ProcessJsRequest(JsRequest r)
		{
			Type type = GetType();
			ParameterInfo currentParameterInfo = null;
			MethodInfo currentMethod = null;
			foreach (Method m in r.Methods)
			{
				if (m != null)
				{
					try
					{
						MethodInfo mi = type.GetMethod(m.Name, BindingFlags.Public | BindingFlags.Instance);
						currentMethod = mi;
						InvokeJsMethod(ref currentParameterInfo, m, mi);
					}
					catch (ArgumentNullException err)
					{
						ErrorHandler.Handle(err);
						HandleInvalidInputException(currentParameterInfo, m);
					}
					catch (InvalidCastException err)
					{
						ErrorHandler.Handle(err);
						HandleInvalidInputException(currentParameterInfo, m);
					}
					catch (FormatException err)
					{
						ErrorHandler.Handle(err);
						HandleInvalidInputException(currentParameterInfo, m);
					}
					catch (TargetInvocationException err)
					{
						Exception innerException = ErrorHandler.Handle(err);
						if (innerException is CustomException)
						{
							HandleUnknownException(m, ErrorHandler.Handle(err));
						}
						else if (!(innerException is IgnorableException))
						{
							HandleInvokeException(currentMethod, m);
						}
					}
					catch (Exception err)
					{
						HandleUnknownException(m, err);
					}
				}
			}
			OutputResponse(r);
		}


		#endregion Request Handlers

		#region Method Invoker

		private void InvokeJsMethod(ref ParameterInfo currentParameterInfo, Method m, MethodInfo mi)
		{
			if (mi != null && mi.IsPublic)
			{
				object[] args = m.ToArray();
				ParameterInfo[] pis = mi.GetParameters();
				if (pis.Length == 1 && args.Length > 1)
				{
					args = new object[] {args};
				}
				for (int ii = 0; ii < pis.Length; ii++)
				{
					ParameterInfo pi = pis[ii];
					currentParameterInfo = pi;
					Type ptype = pi.ParameterType;
					Param p = args[ii] as Param;
					args[ii] = Convert.ChangeType(p == null ? args[ii] : p.Value, ptype);
				}
				if (mi.ReturnType != typeof (void))
				{
					m.MethodReturnValue = mi.Invoke(this, args);
				}
				else
				{
					mi.Invoke(this, args);
				}
			}
		}

		private InvokeResult InvokeMethod(JsRequest r, MethodInfo mi)
		{
			InvokeResult rlt = new InvokeResult();
			try
			{
				if (mi != null)
				{
					mi.ValidateServiceMethod(rlt);
					r.AddMethod(mi.Name);
					if (mi.ReturnType == typeof (void))
					{
						if (Params != null && Params.Length > 0)
						{
							object[] args = Params.ConvertType(0, mi.GetParameters());
							mi.Invoke(this, args);
							r.Methods[0].MethodReturnValue = null;
						}
						else
						{
							mi.Invoke(this, null);
							r.Methods[0].MethodReturnValue = null;
						}
					}
					else
					{
					    object rv = null;
						if (Params != null && Params.Length > 0)
						{
							object[] args = Params.ConvertType(0, mi.GetParameters());
							rv = mi.Invoke(this, args);
						}
						else
						{
                            rv = mi.Invoke(this, null);
						}
                        r.Methods[0].MethodReturnValue = rv;
					    if (rv is ResultBase)
					    {
					        ResultBase result = (ResultBase)rv;
					        if (!result.IsNoException)
					        {
					            r.SetException(result.LastError, mi.Name);
					        }
					    }
					}
				}
				else
				{
					throw new ArgumentNullException(ServerErrorCode.MethodNull);
				}
			}
			catch (Exception ex)
			{
				rlt.LastError = ex;
				if (mi != null)
				{
					r.SetException(ex, mi.Name);
				}
				else
				{
					r.SetException(ex);
				}
			}
			return rlt;
		}

		#endregion Method Invoker

		#region Processors

		public string Method(HttpContext context)
		{
			return "ok";
		}

		protected void Writeln(string msg, params string[] args)
		{
			HttpContext context = HttpContext.Current;
			context.Response.Write(context.Request.Form.Count + "<br />");
		}

		protected virtual string Get(HttpContext context)
		{
			return Method(context);
		}

		protected virtual string Post(HttpContext context)
		{
			return Method(context);
		}

		protected virtual string Put(HttpContext context)
		{
			return Method(context);
		}

		protected virtual string Delete(HttpContext context)
		{
			return Method(context);
		}
		
		#endregion

		protected Page LoadView(string name, bool execute = true)
		{
			const string prefix = "/Views/{0}.aspx";
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (!name.StartsWith(prefix))
			{
				name = string.Format(prefix, name);
			}
			Page p = name.LoadPage<Page>();
			if (execute)
			{
				p.Execute();
			}
			return p;
		}


		private string GetFieldValue(string field)
		{
			HttpRequest Request = HttpContext.Current.Request;
			string xml = Request.Form[field];
			if (string.IsNullOrEmpty(xml))
			{
				xml = Request.QueryString[field];
			}
			return xml;
		}

		private static void OutputResponse(JsRequest r)
		{
			string output = r.ToJson();
			HttpContext.Current.Response.Write(output);
		}  

	}
}