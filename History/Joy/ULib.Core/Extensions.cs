using System;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace ULib.Core
{
	public static class Extensions
	{
		public static string ToXml(this object o)
		{
			if (o != null)
			{
				StringBuilder b = new StringBuilder();
				StringWriter sw = new StringWriter(b);
				XmlSerializer xs = new XmlSerializer(o.GetType());
				xs.Serialize(sw, o);
				sw.Close();
				return b.ToString();
			}
			return null;
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
			else
			{
				return default(T);
			}
		}
	}
}
