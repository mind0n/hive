using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Web.UI;
using System.Web;
using Joy.Server.Web;
using System.Web.Compilation;
using Joy.Core;
using System.IO;

namespace Joy.Server
{
    public static class WebExtensions
    {
		public static void Execute(this Page p)
		{
			try
			{
				if (p != null)
				{
					HttpContext.Current.Server.Execute(p, HttpContext.Current.Response.Output, true);
				}
			}
			catch (Exception ex)
			{
				ErrorHandler.Handle(ex);
			}
		}
		public static bool ValidateServiceMethod(this MethodInfo mi, InvokeResult rlt = null)
		{
			if (mi != null)
			{
				MethodAttribute attr = mi.GetCustomAttribute<MethodAttribute>();
				if (attr != null)
				{
					if (rlt != null)
					{
						rlt.IsPageResponse = attr.IsPage;
					}
					return true;
				}
			}
			return false;
		}

	    public static T LoadPage<T>(this string url) where T : class
	    {
		    try
		    {
			    Type type = BuildManager.GetCompiledType(url);
			    if (type != null)
			    {
				    return Activator.CreateInstance(type) as T;
			    }
			    return default(T);
		    }
		    catch (Exception ex)
		    {
			    ErrorHandler.Handle(ex);
			    return default(T);
		    }
	    }

	    public static string JsEncode(this HttpServerUtility Server, object content)
        {
            string text = null;
            if (content != null)
            {
                text = content.ToString();
            }
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            string r = Server.UrlEncode(text);
            if (!string.IsNullOrEmpty(r))
            {
                r = r.Replace('+', ' ');
            }
            return r;
        }
        public static bool Exists(this Cache cache, string id)
        {
            if (cache != null)
            {
                return cache[id] != null;
            }
            return false;
        }
		//public static string ToJson(this object o, JsonSerializer.SerializeCallbackHandler callback = null)
		//{
		//	JsonSerializer s = new JsonSerializer();
		//	if (callback != null)
		//	{
		//		s.OnSerializeMember += callback;
		//	}
		//	return s.Serialize(o);
		//}
        public static string GetString(this DataRow row, string field, string format = null, int? left = null)
        {
            string r = null;
            if (!row.Table.Columns.Contains(field))
            {
                return string.Empty;
            }
            object value = row[field];
            if (value == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(format))
            {
                r = value.ToString();
            }
            else
            {
                IFormattable v = (IFormattable)value;
                r = v.ToString(format, null);
            }
            if (left == null)
            {
                return r;
            }
            if (!string.IsNullOrEmpty(r))
            {
                if (left.Value > r.Length)
                {
                    return r;
                }
                else
                {
                    return string.Concat(r.Substring(0, left.Value), " ...");
                }
            }
            return r;
        }
        public static HtmlTableRow AddRow(this HtmlTable table)
        {
            HtmlTableRow row = new HtmlTableRow();
            table.Rows.Add(row);
            return row;
        }
        public static HtmlTableCell AddCell(this HtmlTableRow row, string content, string className = null, string url = null)
        {
            HtmlTableCell td = new HtmlTableCell();
            if (!string.IsNullOrEmpty(url))
            {
                HtmlAnchor a = new HtmlAnchor();
                td.Controls.Add(a);
                a.HRef = url;
                a.InnerHtml = content;
            }
            else
            {
                td.InnerHtml = content;
            }
            if (!string.IsNullOrEmpty(className))
            {
                td.Attributes.Add("class", className);
            }
            row.Cells.Add(td);
            return td;
        }
        public static Control Find(this Control target, string id)
        {
            foreach (Control i in target.Controls)
            {
                if (string.Equals(i.ID, id, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
                else
                {
                    Control rlt = i.Find(id);
                    if (rlt != null)
                    {
                        return rlt;
                    }
                }
            }
            return null;
        }
        public static Control Copy(this Control target)
        {
            Type type = target.GetType();
            ConstructorInfo info = type.GetConstructor(Type.EmptyTypes);
            Control root = info.Invoke(null) as Control;
            PropertyInfo[] pis = type.GetProperties();
            FieldInfo[] fis = type.GetFields();
            foreach (PropertyInfo pi in pis)
            {
                if (pi.CanRead && pi.CanWrite && pi.Name != "InnerHtml" && pi.Name != "InnerText")
                {
                    object value = pi.GetValue(target, null);
                    pi.SetValue(root, value, null);
                }
            }
            foreach (FieldInfo fi in fis)
            {
                object value = fi.GetValue(target);
                fi.SetValue(root, value);
            }
            if (target.Controls != null && target.Controls.Count > 0)
            {
                foreach (Control c in target.Controls)
                {
                    Control cc = c.Copy();
                    if (cc != null)
                    {
                        cc.Visible = true;
                        root.Controls.Add(cc);
                    }
                }
            }
            HtmlControl hc = target as HtmlControl;
            if (hc != null && hc.Attributes.Count > 0)
            {
                HtmlControl hr = root as HtmlControl;

                foreach (string k in hc.Attributes.Keys)
                {
                    hr.Attributes[k] = hc.Attributes[k];
                }
            }
            return root;
        }
    }
}
