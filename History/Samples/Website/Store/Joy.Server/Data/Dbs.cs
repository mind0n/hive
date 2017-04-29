using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.UI;
using System.Xml;
using Joy.Server.Reflection;
using Joy.Server.Text;
using Joy.Server.Xml;
using System.Web;
using Joy.Core;

namespace Joy.Server.Data
{
	public class Dbs : IConfigurable
	{
		public static int Count
		{
			get
			{
				return list.Count;
			}
		}
		protected static Dictionary<string, Db> list;
		static Dbs()
		{
			ReadConfigFile();
		}
		public static string Decode(string encoded)
		{
			try
			{
				encoded = encoded.Replace("\r", "");
				encoded = encoded.Replace("\n", "");
				encoded = encoded.Replace("\t", "");
				encoded = encoded.Replace(" ", "");
				return Base64.Decode(encoded);
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
				return null;
			}
		}
		protected static void ReadConfigFile()
		{
			//FileStream fs;
			string path = string.Empty, file = "DB.Config";
			Page p = new Page();
			list = new Dictionary<string, Db>();
			XmlDocument x = new XmlDocument();
			try
			{
				//path = p.Server.MapPath(file);
				path = AppDomain.CurrentDomain.BaseDirectory + file;
			}
			catch (Exception e)
			{
				Exceptions.LogOnly(e);
				path = Directory.GetCurrentDirectory() + "\\" + file;
			}
			if (!System.IO.File.Exists(path))
			{
				throw Exceptions.Log("Config file not found: " + path);
			}
			x.Load(path);
			XReader n = new XReader(x);

			XReader xr = n.Reset()["configuration"]["connections"];

			foreach (XReader item in xr)
			{
				try
				{

					string decoder = item.Restore()["$decoder"].Value;
					string decodeMethod = item.Restore()["$method"].Value;
					string url = item.Restore()["$url"].Value;
					Type typ = Type.GetType(item.Restore()["$type"].Value);
					Db db = (Db)Activator.CreateInstance(typ); //new Db(item.Value, item["$type"].Value, item["$url"].Value);
					if (string.IsNullOrEmpty(decoder))
					{
						db.ConnStr = item.Restore().Value;
						if (!string.IsNullOrEmpty(url))
						{
							db.ConnStr = db.ConnStr.Replace("[dbpath]",HttpContext.Current.Server.MapPath(url));
						}
					}
					else
					{
						string decoded;
						if (string.IsNullOrEmpty(decodeMethod))
						{
							decodeMethod = "Decode";
						}

						decoded = (string)ClassHelper.Invoke(Type.GetType(decoder), decodeMethod, BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, new object[] { item.Restore().Value });
						db.ConnStr = decoded;
					}
					list[item.Name] = db;
				}
				catch (Exception err)
				{
					Exceptions.LogOnly(err);
					throw err;
				}
			}
		}
		public static T Use<T>(string DbName) where T : Db
		{
			if (list.Count > 0)
			{
				return list[DbName] as T;
			}
			else
			{
				throw new Exception("Error: Database connection configuration conntent not found.");
			}
		}

		#region IConfigurable Members

		public object RereadConfigFile()
		{
			ReadConfigFile();
			return null;
		}

		#endregion

	}
}
