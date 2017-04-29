using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fs.Xml;
using System.Xml;

namespace Fs.Web.Client
{
	public class HeaderControl : Control, IConfigurable
	{
		protected override void Render(HtmlTextWriter writer)
		{
			string rlt = (string)RereadConfigFile();
			writer.Write(rlt);
			base.Render(writer);
		}
		#region IConfigurable Members

		public object RereadConfigFile()
		{
			string rlt = "", script = "";
			XReader xr = new XReader();
			XReader xrClient;
			xr.Load(AppDomain.CurrentDomain.BaseDirectory + "ClientLib.Config");
			xrClient = xr.Reset()["configuration"]["client"];
			string defType = xrClient.Reset()["$DefaultScriptType"].Value;
			string clientBasePath = xrClient.Reset()["$BaseUrl"].Value;
			if (!string.IsNullOrEmpty(clientBasePath) && clientBasePath[clientBasePath.Length - 1] != '/')
			{
				clientBasePath += '/';
			}
			foreach (XReader cx in xrClient)
			{
				if (string.IsNullOrEmpty(defType))
				{
					defType = "text/javascript";
				}
				if (cx.Name == "Script")
				{
					rlt += "<script src='" + clientBasePath + cx.Reset()["$url"].Value + "' type='" + defType + "'></script>\n";
				}
			}
			script += "<script type='text/javascript'>";
			script += "var FC$ = {BaseUrl:'" + clientBasePath + "'};";
			script += "</script>";
			rlt = script + rlt;
			return rlt;
		}

		#endregion
	}
}
