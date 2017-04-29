using System;
using System.Collections.Generic;
using System.Text;
using Fs.Xml;
using System.Reflection;

namespace Fs
{
	public interface IConfigurable
	{
		object RereadConfigFile();
	}
	public class Configuration
	{
		public static string BaseDir
		{
			get
			{
				string baseDir = AppDomain.CurrentDomain.BaseDirectory;
				if (baseDir[baseDir.Length - 1] != '\\')
				{
					baseDir += '\\';
				}
				return baseDir;
			}
		}
		public static string UiBaseUrl
		{
			get
			{
				XReader xr = UIConfig;
				if (xr != null)
				{
					return xr.Reset()["configuration"]["$BaseUrl"].Value;
				}
				else
				{
					return null;
				}
			}
		}
		public static XReader DbConfig
		{
			get
			{
				return GetConfigFileReader("DB");
			}
		}
		public static XReader UIConfig
		{
			get
			{
				return GetConfigFileReader("UI");
			}
		}
		public static XReader GetConfigFileReader(string name)
		{
			string fullConfigFilename = BaseDir + name + ".Config";
			XReader xr = new XReader(fullConfigFilename);
			return xr;
		}
	}
	public class FsConfig : Configuration { }
	public class AttributeInfo
	{
		public Attribute Attribute
		{
			get
			{
				return vattr;
			}
		}
		public MemberInfo MemberInfo
		{
			get
			{
				return vmi;
			}
		}
		protected Attribute vattr;
		protected MemberInfo vmi;
		public AttributeInfo(Attribute attr, MemberInfo memberInfo)
		{
			vmi = memberInfo;
			vattr = attr;
		}
		public T GetAttribute<T>() where T:Attribute
		{
			return (T)vattr;
		}
	}
	public class TypeHelper
	{
		public static AttributeInfo GetMethodCustomAttribute(object vtarget, string method)
		{
			MethodInfo mi = vtarget.GetType().GetMethod(method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (mi != null)
			{
				return new AttributeInfo(GetCustomAttribute<Attribute>(mi), mi);
			}
			return null;
		}
		public static T GetCustomAttribute<T>(MemberInfo mif) where T : class
		{
			object[] attrs = mif.GetCustomAttributes(typeof(T), true);
			if (attrs != null && attrs.Length > 0)
			{
				return attrs[0] as T;
			}
			return default(T);
		}
		public static bool IsAttributeExist<T>(object target) where T : class
		{
			if (GetCustomAttribute<T>(target.GetType()) != null)
			{
				return true;
			}
			return false;
		}
	}
}
