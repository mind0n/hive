using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mshtml;

namespace WebOperator
{
	public partial class MasterForm : Form
	{
		string homeUrl = "https://www.12306.cn/otsweb/main.jsp";
		BrowserController controller;
		public MasterForm()
		{
			InitializeComponent();
			Shown += new EventHandler(MasterForm_Shown);
		}

		void MasterForm_Shown(object sender, EventArgs e)
		{
			
			controller = new BrowserController { Browser = this.browser };
			controller.Browser.Navigating += new WebBrowserNavigatingEventHandler(Browser_Navigating);
			controller.Browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(Browser_DocumentCompleted);
			controller.Navigate(homeUrl);
			Log(homeUrl, tAddress);
			
		}

		void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			Log("Completed:\t" + e.Url.ToString());
			if (e.Url.ToString().IndexOf("https://www.12306.cn/otsweb/main.jsp") >= 0)
			{
				AttachLoginRandCodeEvent();
			}
			controller.Browser.Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
		}

		void Window_Error(object sender, HtmlElementErrorEventArgs e)
		{
			MessageBox.Show(e.LineNumber + "," + e.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			Log("Loading:\t" + e.Url.ToString());
		}

		private void bGo_Click(object sender, EventArgs e)
		{
			controller.Navigate(tAddress.Text);
		}
		private void Log(string content)
		{
			Log(content, null);
		}
		private void Log(string content, ComboBox target)
		{
			if (target == null)
			{
				target = status;
			}
			if (target == tAddress)
			{
				target.Items.Insert(0, content);
			}
			else
			{
				target.Items.Add(content);
			}
			target.Text = content;
		}
		private void Navigate(string url)
		{
			controller.Navigate(tAddress.Text);
			Log(url, tAddress);
		}

		private void bFillQuery_Click(object sender, EventArgs e)
		{
			controller.FillForm(FormData.QueryForm);
			controller.ExecuteScript(null, "submitQueryForm", null, "$('#submitQuery').click(sendQueryFunc);");
			HtmlElement el = controller.GetEl("submitQuery");
			if (el != null)
			{
				//el.RaiseEvent("click");
				el.RaiseEvent("onclick");
				
			//    el.SetAttribute("onmouseover", "alert('ok');");
			//    //$("#submitQuery").click(sendQueryFunc);
				
			}

			//controller.InvokeScript("subForm");
			//string url = "https://www.12306.cn/otsweb/passCodeAction.do?rand=lrand";
			//Recognizer r = new Recognizer(url, new System.Net.CookieCollection());
			//MessageBox.Show(r.proc());			
		}

		private void bBookForm_Click(object sender, EventArgs e)
		{
			//AttachLoginRandCodeEvent();
			//controller.ExecuteScript(null, "execfunc", string.Empty, "alert ('ok');");
			//controller.InvokeScript(null, "getSelected", "D286#05:29#07:45#5l0000D28600#AOH#SQF#13:14#上海虹桥#商丘#M034800002O022300000");
			controller.FillForm(FormData.BookForm);
		}

		private void AttachLoginRandCodeEvent()
		{
			//Completed: https://www.12306.cn/otsweb/main.jsp
			HtmlElement el = controller.GetEl("randCode");
			el.AttachEventHandler("onblur", new EventHandler(delegate(object ss, EventArgs ee)
			{
				IHTMLInputElement input = el.DomElement as IHTMLInputElement;
				if (input != null && input.value.Length >= 4)
				{
					controller.InvokeScript("subForm");
				}
			}));
		}

		private void bHome_Click(object sender, EventArgs e)
		{
			Navigate(homeUrl);
		}

		private void bFillMark_Click(object sender, EventArgs e)
		{
			controller.FillForm(FormData.LoginForm);
		}

		private void bQuery_Click(object sender, EventArgs e)
		{
			if (controller.Iframe != null)
			{
				controller.Iframe.Open("https://www.12306.cn/otsweb/order/querySingleAction.do?method=init", "_self", null, false);
			}
		}

		private void bBook_Click(object sender, EventArgs e)
		{
			Navigate("https://www.12306.cn/otsweb/order/confirmPassengerAction.do?method=init");
		}

		private void lDebug_Click(object sender, EventArgs e)
		{
			DebugForm f = new DebugForm(controller);
			f.Show();
		}
	}
}
