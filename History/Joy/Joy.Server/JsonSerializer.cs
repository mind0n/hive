using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;

namespace Joy.Server
{
	public enum ContentType:int 
	{
		Value,
		Text,
		Object,
		Array,
		Dictionary
	}
	public abstract class Serializer
	{
		public static bool IsNumbericType(Type type)
		{
			if (type != null)
			{
				return NumericTypes.Contains(type);
			}
			return false;
		}
		public static bool IsTextType(Type type)
		{
			if (type != null)
			{
				return IsBasicType(type) && !IsNumbericType(type);
			}
			return false;
		}
		public static bool IsBasicType(Type type)
		{
			if (type != null)
			{
				return type.IsValueType || type == typeof(string);
			}
			return false;
		}
		protected static HashSet<Type> NumericTypes = new HashSet<Type>
		{
			typeof(byte), typeof(sbyte),
			typeof(short), typeof(ushort), 
			typeof(uint), typeof(UInt16),typeof(UInt32),typeof(UInt64),
			typeof(int), typeof(Int16), typeof(Int32), typeof(Int64),
			typeof(decimal), typeof(double), typeof(float)
		};
		public string Serialize(object o, Type type = null)
		{
			if (o == null)
			{
				return null;
			}
			string content;
			if (type == null) { type = o.GetType(); }
			if (type.Name.IndexOf("Collection`") == 0 || type.Name.IndexOf("List") == 0 || type.IsArray)
			{
				content = FormatString(SerializeArray(type, o), ContentType.Array);
			}
			else if (type.Name.IndexOf("Dictionary") == 0)
			{
				content = FormatString(SerializeDict(type, o), ContentType.Dictionary);
			}
			else
			{
				if (IsTextType(type))
				{
					content = FormatString(o.ToString(), ContentType.Text);
				}
				else if (IsNumbericType(type) || type.IsValueType)
				{
					content = FormatString(o.ToString(), ContentType.Value);
				}
				else
				{
					content = FormatString(SerializeObject(type, o), ContentType.Object);
				}
			}
			return content;
		}
		protected abstract string SerializeDict(Type type, object a);
		protected abstract string SerializeArray(Type type, object a);
		protected abstract string SerializeObject(Type type, object o);
		protected abstract string SerializeValue(Type type, object v);
		protected abstract string FormatString(string content, ContentType ctype);
	}
	public class JsonSerializer : Serializer
	{
		protected override string FormatString(string content, ContentType ctype)
		{
			const string ArrayTemplate = "[ {0} ]";
			const string ObjectTemplate = "{{ {0} }}";
			const string StringTemplate = "\"{0}\"";
			if (ctype == ContentType.Array)
			{
				return string.Format(ArrayTemplate, content);
			}
			else if (ctype == ContentType.Dictionary || ctype == ContentType.Object)
			{
				return string.Format(ObjectTemplate, content);
			}
			else if (ctype == ContentType.Text)
			{
				return string.Format(StringTemplate, content);
			}
			else
			{
				return content;
			}
		}
		protected override string SerializeValue(Type type, object v)
		{
			string content = string.Empty;
			if (v != null)
			{
				content = v.ToString();
			}
			if (IsTextType(type))
			{
				return string.Concat("\"", content.Replace("\"", "\\\""), "\"");
			}
			else
			{
				return content;
			}
		}
		protected override string SerializeObject(Type type, object o)
		{
			if (o == null)
			{
				return string.Empty;
			}
			List<string> contents = new List<string>();
			PropertyInfo[] ps = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
			for (int i = 0; i < ps.Length; i++)
			{
				PropertyInfo pi = ps[i];
				object value = pi.GetValue(o, null);
				contents.Add(string.Concat(pi.Name, ":", base.Serialize(value)));
			}
			FieldInfo[] fs = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
			for (int i = 0; i < fs.Length; i++)
			{
				FieldInfo fi = fs[i];
				object value = fi.GetValue(o);
				contents.Add(string.Concat(fi.Name, ":", base.Serialize(value)));
			}
			return string.Join(",", contents.ToArray());
		}
		protected override string SerializeArray(Type type, object a)
		{
			List<string> contents = new List<string>();
			foreach (object o in (a as IEnumerable))
			{
				string content = base.Serialize(o);
				contents.Add(content);
			}
			return string.Join(",", contents.ToArray());
		}
		protected override string SerializeDict(Type type, object a)
		{
			IDictionary dict = a as IDictionary;
			List<string> contents = new List<string>();
			foreach (object key in dict.Keys)
			{
				object o = dict[key];
				string content = base.Serialize(o);
				contents.Add(string.Concat(key, ":", content));
			}
			return string.Join(",", contents.ToArray());
		}
	}
}
