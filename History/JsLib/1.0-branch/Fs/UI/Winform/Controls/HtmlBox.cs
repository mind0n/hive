using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using mshtml;
using System.Windows.Forms;

namespace Fs.UI.Winform.Controls
{
	public partial class HtmlBox : UserControl
	{
		public string Html
		{
			get
			{
				return htmlContent.DocumentText;
			}
			set
			{
				htmlContent.DocumentText = value;
			}
		}
		public string BodyInnerHtml
		{
			get
			{
				return htmlContent.Document.Body.InnerHtml;
			}
			set
			{
				htmlContent.Document.Body.InnerHtml = value;
			}
		}
		public string UrlString
		{
			get
			{
				if (htmlContent.Url != null)
				{
					return htmlContent.Url.ToString();
				}
				return null;
			}
			set
			{
				string val;
				if (string.IsNullOrEmpty(value))
				{
					val = "about:blank";
				}
				else
				{
					val = value;
				}
				htmlContent.Url = new Uri(val);
				htmlContent.Update();
				emptyContent = htmlContent.DocumentText;
			}
		}
		protected string emptyContent;
		public HtmlBox()
		{
			InitializeComponent();
		}
		public void Paste()
		{
			if (string.IsNullOrEmpty(UrlString))
			{
				UrlString = "";
			}
			htmlContent.Document.ExecCommand("Paste", false, null);
		}
		public new void Focus()
		{
			htmlContent.Focus();
		}
		public void Clear()
		{
			UrlString = "about:blank";
		}
		protected override void OnLoad(EventArgs e)
		{
			UrlString = "";
			IHTMLDocument2 doc = htmlContent.Document.DomDocument as IHTMLDocument2;
			doc.designMode = "On";
		}
	}
}
