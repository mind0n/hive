using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using System.Collections;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace Joy.Core
{
	public static class CoreExtensions
	{
	    public static string UrlDecode(this string s)
	    {
	        return HttpUtility.UrlDecode(s);
	    }

	    public static string UrlEncode(this string s)
	    {
	        return HttpUtility.UrlEncode(s);
	    }

	    public static string HtmlDecode(this string s)
	    {
	        return HttpUtility.HtmlDecode(s);
	    }

	    public static string HtmlEncode(this string s)
	    {
	        return HttpUtility.HtmlEncode(s);
	    }



		public static string AsString(this object o, bool isLowerCase = false, string defaultValue = null)
		{
			if (o != null)
			{
				if (isLowerCase)
				{
					return o.ToString().ToLowerInvariant();
				}
				return o.ToString();
			}
			return null;
		}
		public static string[] SubList(this string[] o, int start, int end = -1)
		{
			if (o != null)
			{
				if (start > -1 && start < o.Length)
				{
					if (end < 0)
					{
						end = o.Length - 1;
					}
					else if (end >= o.Length)
					{
						return null;
					}
					string[] rlt = new string[o.Length - start];
					for (int i = start; i <= end; i++)
					{
						rlt[i - start] = o[i];
					}
					return rlt;
				}
			}
			return null;
		}

	    public static T ChangeType<T>(this object value)
	    {
	        return (T)ChangeType(value, typeof (T));
	    }
        public static object ChangeType(this object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value != null)
                {
                    NullableConverter nullableConverter = new NullableConverter(conversionType);
                    conversionType = nullableConverter.UnderlyingType;
                }
                else
                {
                    return null;
                }
            }

            return Convert.ChangeType(value, conversionType);
        }
		public static object[] ConvertType(this object[] o, int start, ParameterInfo[] types)
		{
			if (o != null && types != null && o.Length == types.Length + start && o.Length > 0)
			{
				if (start > -1 && start < o.Length)
				{
					int end = o.Length - 1;
					object[] rlt = new object[o.Length - start];
					for (int i = start; i <= end; i++)
					{
						rlt[i - start] = Convert.ChangeType(o.GetValue(i), types[i - start].ParameterType);
					}
					return rlt;
				}
			}
			return null;
		}
		public static object CreateInstance(this string name, string assembly = null)
		{
			try
			{
				if (!string.IsNullOrEmpty(name))
				{
					Type type = Type.GetType(name, false);
					if (type != null)
					{
						return Activator.CreateInstance(type);
					}
				}
				return null;
			}
			catch
			{
				return null;
			}
		}
		public static string MapPath(this string url)
		{
			if (!string.IsNullOrEmpty(url))
			{
				if (url.IndexOf('/') < 0)
				{
					return AppDomain.CurrentDomain.BaseDirectory + url;
				}
				if (string.Equals("/", url))
				{
					return AppDomain.CurrentDomain.BaseDirectory;
				}
				if (url.StartsWith("/"))
				{
					url = url.Substring(1);
				}
				string[] ulist = url.Split('/');
				string path = AppDomain.CurrentDomain.BaseDirectory;
				path = path.Substring(0, path.Length - 1);
				string[] plist = path.Split('\\');
				//int n = plist.Length - 1;
				List<string> rlt = new List<string>();
				rlt.AddRange(plist);
				foreach (string i in ulist)
				{
					if ("..".Equals(i))
					{
						if (rlt.Count > 1)
						{
							rlt.RemoveAt(rlt.Count - 1);
						}
						else
						{
							return null;
						}
					}
					else
					{
						rlt.Add(i);
					}
				}
				return string.Join("\\", rlt.ToArray());
			}
			return null;
		}
		public static string FileExt(this string target, bool withDot = false)
		{
			string rlt;
			string filename = target.PathLastName();
			string[] list = filename.Split('.');
			if (list.Length == 1)
			{
				rlt = list[0];
			}
			else
			{
				rlt = list[list.Length - 1];
			}
			if (withDot)
			{
				rlt = '.' + rlt;
			}
			return rlt;
		}

		public static string PathLastName(this string target, bool includeExt = true, params char [] splitter)
		{
			if (!string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(target.Trim()))
			{
				if (splitter == null || splitter.Length < 1)
				{
					splitter = new [] {'\\'};
				}
				string[] list = target.Split(splitter);
				string result = list[list.Length - 1];
				if (!includeExt)
				{
					int pos = result.LastIndexOf('.');
					if (pos <= 0)
					{
						result = string.Empty;
					}
					else if (pos > 0)
					{
						result = result.Substring(0, pos - 1);
					}
					//result = result.Split('.')[0];
				}
				return result;
			}
			return string.Empty;
		}

		public static List<T> Fill<T>(this IDataReader reader) where T : new()
		{
			if (reader == null)
			{
				return null;
			}
			List<T> rlt = new List<T>();
			Type type = typeof(T);
			PropertyInfo[] plist = type.GetProperties();
			FieldInfo[] flist = type.GetFields();
			while (reader.Read())
			{
				T item = new T();
				for (int i = 0; i < reader.FieldCount; i++)
				{
					bool isContinue = false;
					foreach (PropertyInfo pi in plist)
					{
						if (pi.CanWrite && string.Equals(pi.Name, reader.GetName(i), StringComparison.OrdinalIgnoreCase))
						{
							pi.SetValue(item, reader.GetValue(i), null);
							isContinue = true;
							break;
						}
					}
					if (isContinue)
					{
						continue;
					}
					foreach (FieldInfo fi in flist)
					{
						if (string.Equals(fi.Name, reader.GetName(i), StringComparison.OrdinalIgnoreCase))
						{
							fi.SetValue(item, reader.GetValue(i));
							break;
						}
					}
				}
				rlt.Add(item);
			}
			return rlt;
		}
		public static string Left(this string content, int count)
		{
			if (count < 0)
			{
				count = content.Length + count;
			}
			if (count <= 0)
			{
				return string.Empty;
			}
			if (count > 0 && count < content.Length)
			{
				return content.Substring(0, count);
			}
			if (count >= content.Length)
			{
				return content;
			}
			return content;
		}
		public static string ToJson(this object o)
		{
			if (o != null)
			{
				try
				{
					JavaScriptSerializer jss = new JavaScriptSerializer();
					return jss.Serialize(o);
				}
				catch
				{
					return null;
				}
			}
			return null;
		}
		public static T FromJson<T>(this string json)
		{
			if (!string.IsNullOrEmpty(json))
			{
				try
				{
					JavaScriptSerializer jss = new JavaScriptSerializer();
					return (T)jss.Deserialize(json, typeof(T));
				}
				catch
				{
					return default(T);
				}
			}
			return default(T);
		}
		public static T FromXml<T>(this string xml)
		{
			if (!string.IsNullOrEmpty(xml))
			{
				try
				{
					StringReader sr = new StringReader(xml);
					XmlSerializer xs = new XmlSerializer(typeof(T));
					T result = (T)xs.Deserialize(sr);
					sr.Close();
					return result;
				}
				catch
				{
					return default(T);
				}
			}
			return default(T);
		}

        //public static T EnumFieldValue<T>(this Type type, Func<FieldInfo, bool> callback, object target = null,
        //    BindingFlags flags = BindingFlags.Public | BindingFlags.GetField | BindingFlags.Static)
        //{
        //    object o = EnumFieldValue(type, callback, target, flags);
        //    return o == null ? default(T) : o.ChangeType<T>();
        //}
	    public static List<T> EnumFieldValue<T>(this Type type, Func<FieldInfo, bool> callback = null, object target = null, BindingFlags flags = BindingFlags.Public | BindingFlags.GetField | BindingFlags.Static | BindingFlags.FlattenHierarchy)
	    {
	        if (type == null)
	        {
	            return null;
	        }
            List<T> rlt = new List<T>();
	        FieldInfo [] fi = type.GetFields(flags);
	        foreach (FieldInfo i in fi)
	        {
	            if (callback == null || callback(i))
	            {
                    object o = i.GetValue(target);
                    rlt.Add(o.ChangeType<T>());
	            }
	        }
	        return rlt;
	    }

		public static string ToXml(this object o, string file = null)
		{
			if (o != null)
			{
				StringBuilder b = new StringBuilder();
				StringWriter sw = new StringWriter(b);
				XmlSerializer xs = new XmlSerializer(o.GetType());
				xs.Serialize(sw, o);
				sw.Close();
				if (file != null)
				{
					try
					{
						File.WriteAllText(file, b.ToString());
					}
					catch (Exception e)
					{
						ErrorHandler.Handle(e);
					}
				}
				return b.ToString();
			}
			return null;
		}
		public static T[] ToArray<T>(this IList list, string fieldName) where T : class
		{
			if (list == null || list.Count < 1)
			{
				return null;
			}
			List<T> rlt = new List<T>();
			Type type = list[0].GetType();
			PropertyInfo pi = type.GetProperty(fieldName);
			FieldInfo fi = type.GetField(fieldName);
			foreach (object i in list)
			{
				try
				{
					if (pi != null && pi.CanRead)
					{
						object v = pi.GetValue(i, null);
						rlt.Add(Convert.ChangeType(v, typeof(T)) as T);
					}
					else if (fi != null)
					{
						object v = fi.GetValue(i);
						rlt.Add(Convert.ChangeType(v, typeof(T)) as T);
					}
				}
				catch (Exception e)
				{
					ErrorHandler.Handle(e);
				}
			}
			return rlt.ToArray();
		}
		public static T Clone<T>(this object o) where T:class 
		{
			if (o != null)
			{
				BinaryFormatter formatter = new BinaryFormatter();
				using (MemoryStream stream = new MemoryStream())
				{
					formatter.Serialize(stream, o);
					stream.Position = 0;
					object r = formatter.Deserialize(stream);
					return r as T;
				}
			}
			return default(T);
		}
	}
}
