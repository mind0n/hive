using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace Fs.Web
{
	public class RequestHelper
	{
		public delegate void ItemEnumHandler(string key, string val);
		public static void EnumKeys(NameValueCollection Form, string prefix, ItemEnumHandler callback)
		{
			string key;
			foreach (string item in Form.Keys)
			{
				if (item.IndexOf(prefix) == 0)
				{
					if (item.Length > prefix.Length)
					{
						key = item.Substring(prefix.Length, item.Length - prefix.Length);
					}
					else
					{
						key = "";
					}
					callback(key, Form[item]);
				}
			}

		}
	}
}
