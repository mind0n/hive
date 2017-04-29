using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.Exceptions;
using System.IO;
using System.Reflection;
using System.Collections;

namespace ULib.Encoders
{
	public static class ObjectEncoder
	{
		public static string Obj2Xml(this object target, string filename = null)
		{
			string result = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + EncodeObject(target);
			if (!string.IsNullOrEmpty(filename))
			{
				try
				{
					File.WriteAllText(filename, result);
				}
				catch (Exception err)
				{
					ExceptionHandler.Handle(err);
				}
			}
			return result;
		}

		const string template = "\r\n<{0} {2}>{1}</{0}>";

		private static string EncodeObject(object target, string name = null, Type type = null, string assembly = null)
		{
			if (target == null)
			{
				return string.Empty;
			}
			if (type == null)
			{
				type = target.GetType();
			}
			string nameStr = MakeAttribute("name", name);
			string tempAsmStr = assembly ?? type.AssemblyQualifiedName;
			string[] temp = tempAsmStr == null ? null : tempAsmStr.Split(',');
			string asmStr = type.AssemblyQualifiedName;

			if (temp != null && temp.Length > 1)
			{
				asmStr = MakeAttribute("type", string.Join(",", new string[] { temp[0], temp[1] }));
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "root";
			}
			if (type.IsValueType || target is string)
			{
				return string.Format(template, name, target.ToString(), string.Join(" ", new string[] { asmStr }));
			}
			else if (target is IList)
			{
				string rlt = string.Format(template, name, EncodeCollection(target, type, name, asmStr), string.Join(" ", new string[] { asmStr }));
				return rlt;
			}
			else if (type.IsClass)
			{
				List<string> fields = new List<string>();
				FieldInfo[] plist = type.GetFields();
				foreach (FieldInfo i in plist)
				{
					object o = i.GetValue(target);

					string child = EncodeObject(o, i.Name, i.FieldType);
					if (!string.IsNullOrEmpty(child))
					{
						fields.Add(child);
					}
				}
				if (fields.Count > 0)
				{
					return "\r\n" + string.Format(template, name, string.Join("\r\n", fields.ToArray()), string.Join(" ", new string[] { asmStr }));
				}
			}
			return string.Empty;
		}

		private static string EncodeCollection(object target, Type type, string name, string asmStr)
		{
			if (type.IsSubclassOf(typeof(IDictionary)))
			{
				return EncodeDict(target);
			}
			else if (target is IList)
			{
				return EncodeList(target);
			}
			return string.Empty;
		}

		private static string EncodeList(object target)
		{
			List<string> rlt = new List<string>();
			IList o = (IList)target;
			foreach (object i in o)
			{
				object v = i;
				string content = null;
				if (v != null)
				{
					content = EncodeObject(v, "Item");
				}
				rlt.Add(string.Format(template, "Add", content, string.Empty));
			}
			return string.Join("\r\n", rlt.ToArray());
		}

		private static string EncodeDict(object target)
		{
			List<string> rlt = new List<string>();
			IDictionary o = (IDictionary)target;
			foreach (object k in o.Keys)
			{
				if (k is string || k.GetType().IsValueType)
				{
					object v = o[k];
					string content = null;
					if (v != null)
					{
						content = EncodeObject(v, "Item");
					}
					rlt.Add(string.Format(template, "Add", content, string.Join(" ", new string[] { MakeAttribute("key", k.ToString()) })));
				}
			}
			return string.Join("\r\n", rlt.ToArray());
		}

		private static string MakeAttribute(string attrName, string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return string.Format("{1}=\"{0}\"", name, attrName);
			}
			else
			{
				return string.Empty;
			}
		}
	}
}
