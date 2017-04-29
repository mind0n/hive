using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace Joy.Core
{
	public static class CoreExtensions
	{
		public static string FileExt(this string target, bool withDot = false)
		{
			string rlt = null;
			string filename = target.PathFilename();
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

		public static string PathFilename(this string target, bool includeExt = true, params char [] splitter)
		{
			if (!string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(target.Trim()))
			{
				if (splitter == null)
				{
					splitter = new [] {'\\'};
				}
				string[] list = target.Split(splitter);
				string result = list[list.Length - 1];
				if (!includeExt)
				{
					result = result.Split('.')[0];
				}
				return result;
			}
			return string.Empty;
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
		public static T[] ToArray<T>(this IList list, string fieldName)	where T:class
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
	}
}
