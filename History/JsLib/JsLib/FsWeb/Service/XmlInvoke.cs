using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fs.Entities;
using Fs;
using System.Reflection;
using Fs.Core;
using System.Xml.Linq;
using System.Collections;
using System.Web;
using Fs.Reflection;

namespace FsWeb.Service
{

	public class LinqXmlInvoker
	{
		public static ArrayList Invoke(object vtarget, string xml)
		{
			ArrayList rlt = new ArrayList();
			XDocument xd;
			try
			{
				xd = XDocument.Parse(xml, LoadOptions.None);
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
				return rlt;
			}
			int len = xd.Root.Elements().Count<XElement>();
			if (len > 0)
			{
				try
				{
					foreach (XElement method in xd.Root.Elements())
					{
						AttributeInfo ai = TypeHelper.GetMethodCustomAttribute(vtarget, method.Attribute("Name").Value);
						if (ai == null || ai.Attribute == null)
						{
							rlt.Add(new InvokeResult(method.Attribute("Name").Value, null, new InvokeException("Invalid method name({0})", method.Attribute("Name").Value)));
						}
						else
						{
							InvokableMethodAttribute attr = ai.GetAttribute<InvokableMethodAttribute>();
							if (attr.ParamType == InvokableMethodAttribute.ParamTypes.XElement)
							{
								rlt.Add(Invoker.Invoke(vtarget, ai, new object[] { method.Elements() }));
							}
							else
							{
								DictParams dp = new DictParams();
								int i = 0;
								foreach (XElement param in method.Elements())
								{

									if (param.Attribute("Name") != null)
									{
										dp[param.Attribute("Name").Value] = param.Value;
									}
									else
									{
										dp[i.ToString()] = param.Value;
										i++;
									}
								}
								if (attr.ParamType == InvokableMethodAttribute.ParamTypes.ObjectArray)
								{
									rlt.Add(Invoker.Invoke(vtarget, ai, dp.ToObjectArray()));
								}
								else
								{
									rlt.Add(Invoker.Invoke(vtarget, ai, new object[] { dp }));
								}
							}
						}
					}
					return rlt;
				}
				catch (InvokeException err)
				{
					Exceptions.LogOnly(err, Exceptions.ExceptionType.UserDefined);
				}
			}
			return rlt;
		}
	}

	public class RequestXmlInvoker
	{
		public ArrayList Results;
		public void Invoke(object vtarget, HttpRequest request, HttpResponse response)
		{
			string req = HttpUtility.HtmlDecode(request.Form["xml"]);
			Results = LinqXmlInvoker.Invoke(vtarget, req);
			response.Write("<result successful=\"true\">");
			for (int i = 0; i < Results.Count; i++)
			{
				InvokeResult rlt = (InvokeResult)Results[i];
				if (rlt != null)
				{
					response.Write("\n<method name=\"" + rlt.MethodName + "\" successful=\"" + !rlt.HasError + "\"><return>" + rlt.ReturnValue<string>() + "</return>");
					if (rlt.HasError)
					{
						response.Write("<error><![CDATA[" + Exceptions.GetExceptionMessage(rlt.Errors) + "]]></error>");
					}
					response.Write("</method>");
				}
			}
			response.Write("</result>");
			//response.Write(rlt.Count);
		}

	}
}
