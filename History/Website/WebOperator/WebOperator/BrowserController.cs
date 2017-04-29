using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using mshtml;

namespace WebOperator
{
	class BrowserController
	{
		public WebBrowser Browser;
		private HtmlWindow iframe;

		public HtmlWindow Iframe
		{
			get {
				RefreshIframe();
				return iframe;
			}
		}

		public object InvokeScript(string name)
		{
			return InvokeScript(null, name, null);
		}
		public object ExecuteScript(HtmlWindow frame, string name, string par, string scripts, params object[] args)
		{
			object rlt = null;
			if (frame == null)
			{
				frame = Iframe;
			}
			if (string.IsNullOrEmpty(name))
			{
				name = UniqueName();
			}
			if (frame != null)
			{
				HtmlElement el = frame.Document.CreateElement("script");
				IHTMLScriptElement script = el.DomElement as IHTMLScriptElement;
				script.text = string.Format("function {0}({1}){{ {2} }}", name, par, scripts);
				frame.Document.Body.AppendChild(el);
				rlt = InvokeScript(frame, name, args);
			}
			return rlt;
		}
		public object InvokeScript(HtmlWindow frame, string name, params object[] args)
		{
			HtmlDocument doc = GetDoc(frame);
			return doc.InvokeScript(name, args);
		}
		
		public void FillForm(FormDataCollection data)
		{
			foreach (FormDataItem item in data)
			{
				if (item.Type == FormDataType.Script)
				{
					ExecuteScript(null, "checkPassengerInputBox", null, item.Script);
				}
				else
				{
					HtmlElement el = GetEl(item.Id);
					if (item.Type == FormDataType.Input && el != null)
					{
						IHTMLInputElement input = el.DomElement as IHTMLInputElement;
						input.value = item.Value;
					}
					else if (item.Type == FormDataType.Action)
					{
						item.Action(el);
					}
				}
			}
		}

		public HtmlElement GetEl(string id)
		{
			return GetEl(id, null);
		}

		public HtmlElement GetEl(string id, HtmlWindow frame)
		{
			HtmlDocument doc = GetDoc(frame);
			if (doc != null)
			{
				return doc.GetElementById(id);
			}
			return null;
		}

		public HtmlDocument GetDoc(HtmlWindow frame)
		{
			HtmlDocument doc = null;
			if (frame != null)
			{
				doc = frame.Document;
			}
			else if (Iframe != null)
			{
				doc = Iframe.Document;
			}
			else if (Browser != null)
			{
				doc = Browser.Document;
			}
			return doc;
		}

		public void RefreshIframe()
		{
			if (Browser != null)
			{
				iframe = Browser.Document.Window.Frames[0];
			}
		}

		public void Navigate(string url)
		{
			if (Browser != null)
			{
				Browser.Navigate(url);
			}
		}
		public string UniqueName()
		{
			string rlt = string.Empty;
			DateTime t = DateTime.Now;
			rlt = string.Concat("func_", t.Hour, t.Minute, t.Second, t.Millisecond);
			return rlt;
		}
	}
}
