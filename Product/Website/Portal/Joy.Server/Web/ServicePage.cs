﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml.Serialization;
using Joy.Core;
using System.Reflection;
using Joy.Server.Authentication;
using System.Web;
using System.Web.Script.Serialization;

namespace Joy.Server.Web
{
	/*
	 *  var JsRequest =
		{
			"xmlns:xsi":"http://www.w3.org/2001/XMLSchema-instance",
			"xmlns:xsd":"http://www.w3.org/2001/XMLSchema",
			Methods:
			[
				{ Name: 'GetData', Params:
					[
						{ Name: 'Arg1', Value: 'val1' },
						{ Name: 'Arg2', Value: 'val2' }
					]
				}
			]
		};
	 */
	public class ServicePage : Page
	{
		protected override void OnLoad(EventArgs e)
		{
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Clear();
			base.OnLoad(e);
			string action = Request.QueryString["a"];
			if (!string.IsNullOrEmpty(action))
			{
				ProcessUtilRequest(action);
				ProcessNormalRequest(action);
			}
			bool processed = ProcessJsonRequest() || ProcessXmlRequest();
			if (!processed && string.IsNullOrEmpty(action))
			{
				JsRequest r = new JsRequest();
				r.Methods.Add(new Method { Name = "Content" });
				r.Methods[0].Params.Add(new Param { Name = "Arg1", Value = "a" });
				r.Methods[0].Params.Add(new Param { Name = "Arg2", Value = "b" });
				Response.Write(r.ToXml());
			}
			AuthCodePage.Update();
			Response.End();
		}

		private bool ProcessJsonRequest()
		{
			string json = GetFieldValue("json");
			if (!string.IsNullOrEmpty(json))
			{
				JsRequest r = json.FromJson<JsRequest>();
				HandleJsRequest(r);
				return true;
			}
			return false;
		}
		private bool ProcessXmlRequest()
		{
			string xml = GetFieldValue("xml");
			if (!string.IsNullOrEmpty(xml))
			{
				JsRequest r = xml.FromXml<JsRequest>();
				HandleJsRequest(r);
				return true;
			}
			return false;
		}

		private void ProcessNormalRequest(string action)
		{
			JsRequest r = new JsRequest();
			string[] actions = action.Split(',');
			foreach (string a in actions)
			{
				Method m = r.AddMethod(a);
				if (!string.IsNullOrEmpty(a))
				{
					string param = Request.QueryString[a];
					if (!string.IsNullOrEmpty(param))
					{
						string[] args = param.Split(',');
						foreach (string i in args)
						{
							m.AddParam(i);
						}
					}
				}
			}
			HandleJsRequest(r);
		}

		private void ProcessUtilRequest(string action)
		{
			if (string.Equals(action, "util", StringComparison.OrdinalIgnoreCase))
			{
				string type = Request.QueryString["t"];
				if (!string.IsNullOrEmpty(type))
				{
					if ("Escape".Equals(type))
					{
						Response.Write("<textarea id='box' style='width:800px; height:500px;'></textarea><br /><script>var b = document.getElementById('box');</script><input type='button' onmousedown='box.value = unescape(box.value);' value='UnEscape' /><input type='button' onmousedown='box.value = escape(box.value);' value='Escape' />");
						Response.End();
					}
				}
			}
		}

		private void HandleJsRequest(JsRequest r)
		{
			if (r == null)
			{
				return;
			}
			Type type = this.GetType();
			ParameterInfo currentParameterInfo = null;
			MethodInfo currentMethod = null;
			for (int i = 0; i < r.Methods.Count; i++)
			{
				Method m = r.Methods[i];
				if (m != null)
				{
					try
					{
						MethodInfo mi = type.GetMethod(m.Name, BindingFlags.Public | BindingFlags.Instance);
						currentMethod = mi;
						if (mi != null && mi.IsPublic)
						{
							object[] args = m.ToArray();

							ParameterInfo[] pis = mi.GetParameters();
							if (pis != null)
							{
								if (pis.Length == 1 && args.Length > 1)
								{
									args = new object[] { args };
								}
								for (int ii = 0; ii < pis.Length; ii++)
								{
									ParameterInfo pi = pis[ii];
									currentParameterInfo = pi;
									Type ptype = pi.ParameterType;
									args[ii] = Convert.ChangeType(args[ii], ptype);
								}
							}
							if (mi.ReturnType != typeof(void))
							{
								m.MethodReturnValue = mi.Invoke(this, args);
							}
							else
							{
								mi.Invoke(this, args);
							}
						}
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
			string output = r.ToJson();
			Response.Write(output);
		}

		private static void HandleInvokeException(MethodInfo currentMethod, Method m)
		{
			string name = "N/A";
			if (currentMethod != null)
			{
				name = currentMethod.Name;
			}
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

		private string GetFieldValue(string field)
		{
			string xml = Request.Form[field];
			if (string.IsNullOrEmpty(xml))
			{
				xml = Request.QueryString[field];
			}
			return xml;
		}

	}

}
