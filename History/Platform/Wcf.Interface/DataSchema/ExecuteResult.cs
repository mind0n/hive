using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Platform.Core;
using System;
using System.Collections;

namespace Wcf.Interface.DataSchema
{
	[XmlInclude(typeof(ResultBase))]
	[Serializable]
	public class ExecuteResult : ResultBase
	{
		public string ActionName;
	}
	public class ExecuteResultSet : ArrayList
	{
	}
	public static class WcfExtensions
	{
		public static string Object2Xml(object instance, Type type = null)
		{
			if (instance == null)
			{
				return null;
			}
			if (type == null)
			{
				type = instance.GetType();
			}
			if (instance != null)
			{
				StringBuilder b = new StringBuilder();
				StringWriter sw = new StringWriter(b);
				XmlSerializer xs = new XmlSerializer(type);
				xs.Serialize(sw, instance);
				sw.Close();
				return b.ToString();
			}
			return null;
		}
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
	}
}
