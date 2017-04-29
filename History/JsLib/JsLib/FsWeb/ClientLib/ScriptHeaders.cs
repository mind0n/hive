using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Fs.Xml;
using Fs;
using System.Xml;
using Fs.IO;
using System.IO;

namespace FsWeb.ClientLib
{
	public class ScriptHeaders : WebControl
	{
		protected class ScriptItem
		{
			public string Filename;
			public string FullPath;
			public ScriptItem(string filename, string fullpath)
			{
				Filename = filename;
				FullPath = fullpath;
			}
		}
		protected string rootDirName;
		protected string rootDir;
		protected string rootUrl;
		protected string ConfigFile;
		protected static string Content = "";
		protected List<ScriptItem> scriptFiles = new List<ScriptItem>();
		protected XmlDocument xd;
		protected void ReadFromConfig()
		{
			using (XReader xr = new XReader(ConfigFile))
			{
				rootUrl = xr.Reset()["root"]["$BaseDir"].Value;
				if (xr != null)
				{
					WriteLine("");
					foreach (object oChild in xr.Reset()["root"])
					{
						XReader child = oChild as XReader;
						if ("Script".Equals(child.Name) || "ExternScript".Equals(child.Name) || "CoreScript".Equals(child.Name))
						{
							string content = "<script src='" + rootUrl + child["$Src"].Value + "' type='text/javascript'></script>";
							WriteLine(content);
						}
					}
				}
				else
				{
					WriteLine("\n\n<!-- Incorrect config file format -->\n\n");
				}
			}
		}
		private static int Comparer(ScriptItem x, ScriptItem y)
		{
			for (int i = 0; i < x.Filename.Length && i < y.Filename.Length;)
			{
				if (x.Filename[i] > y.Filename[i])
				{
					return 1;
				}
				else if (x.Filename[i] == y.Filename[i])
				{
					i++;
				}
				else
				{
					return -1;
				}
			}
			if (x.Filename.Length < y.Filename.Length)
			{
				return -1;
			}
			else if (x.Filename.Length == y.Filename.Length)
			{
				return 0;
			}
			else
			{
				return 1;
			}
		}

		public void GenerateConfigFile()
		{
			using (XReader xr = new XReader(ConfigFile))
			{
				//rootDir = xr.Reset()["root"]["$BaseDir"].Value;
				xr.Reset()["root"].RemoveChild("Script");
				xd = xr.Reset().NodeContent<XmlDocument>();
				scriptFiles.Clear();
				DiskHelper.EnumFile(rootDir, new DiskHelper.BoolDlgString(ScriptFileEnumHandler));
				scriptFiles.Sort(Comparer);
				foreach (ScriptItem file in scriptFiles)
				{
					XReader item = xr.Reset()["root"].AddValue("Script", null) as XReader;
					string url = DiskHelper.MapUrl(file.FullPath, rootDir);
					item.SetValue("$Src", file.FullPath);
				}
				xr.Save();
			}
		}
		protected bool ScriptFileEnumHandler(FileInfo info)
		{
			if (info != null && !string.IsNullOrEmpty(info.Extension))
			{
				if (info.Extension.ToLower() == ".js")
				{
					if (rootDirName.Equals(info.Directory.Name.ToLower()))
					{
						scriptFiles.Add(new ScriptItem('0' + info.Name, DiskHelper.MapUrl(info.FullName, rootDir).Substring(1)));
					}
					else if ("core".Equals(info.Directory.Name.ToLower()))
					{
						scriptFiles.Add(new ScriptItem('1' + info.Name, DiskHelper.MapUrl(info.FullName, rootDir).Substring(1)));
					}
					else
					{
						int pose = info.DirectoryName.ToLower().LastIndexOf("\\extern");
						int posl = info.DirectoryName.ToLower().LastIndexOf(rootDir.Replace("/", "").ToLower());
						if (pose >= 0 && posl < pose)
						{
							//do nothing
						}
						else
						{
							scriptFiles.Add(new ScriptItem(info.Name, DiskHelper.MapUrl(info.FullName, rootDir).Substring(1)));
						}
					}
				}
			}
			return true;
		}
		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			ConfigFile = FsConfig.BaseDir + "ClientScript.Config";
			if (string.IsNullOrEmpty(Content))
			{
				using (XReader xr = new XReader(ConfigFile))
				{
					rootUrl = xr.Reset()["root"]["$BaseDir"].Value;
					if (!string.IsNullOrEmpty(rootUrl))
					{
						rootUrl = rootUrl.ToLower();
						rootDir = DiskHelper.MapPath(rootUrl);
						PathInfo pi = new PathInfo(rootDir);
						rootDirName = pi.CurrentDirName;
					}
					else
					{
						rootDir = DiskHelper.MapPath("/FsWeb/ClientLib");
						rootUrl = "/FsWeb/ClientLib/";
						DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "FsWeb\\ClientLib\\");
						rootDirName = di.Name.ToLower();
					}
					GenerateConfigFile();
				}
			}
			Content = "";
			ReadFromConfig();
			writer.WriteLine(Content);
		}
		protected void Write(string content)
		{
			Content += content;
		}
		protected void WriteLine(string content)
		{
			Write(content + "\n");
		}
	}
}
