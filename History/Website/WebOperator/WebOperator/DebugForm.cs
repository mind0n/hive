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
	partial class DebugForm : Form
	{
		BrowserController controller;
		public DebugForm(BrowserController controller)
		{
			InitializeComponent();
			this.controller = controller;
			bRun.GotFocus += new EventHandler(bRun_GotFocus);
			controller.ExecuteScript(null, null, null,
				@"var ta = document.createElement('textarea');
document.body.appendChild(ta);
ta.style.position = 'fixed';
ta.style.left = '10px';
ta.style.top = '10px';
ta.style.width = '600px';
ta.style.height = '400px';
ta.style.display = 'none';
ta.ondblclick = function(){
	this.style.display = 'none';
};
document.body.box = ta;
$.Output = function(msg){
	document.body.box.style.display='';  
	document.body.box.value += msg + '\r\n';
};
$.LogObj = function(obj){
	$.Output('=====================');
	for (var p in obj){
		$.Output(p + ':\t' + obj[p])
		$.Output('-----------------');
	}
	$.Output('=====================');
};
				");
		}

		void bRun_GotFocus(object sender, EventArgs e)
		{
			tScripts.Focus();
			RunScript();
		}

		private void RunScript()
		{
			string script = "var tempFunc = function(){{ {0} }}; var rlt=tempFunc(); if (typeof(rlt) != 'object'){{ $.Output(tempFunc()); }}else{{$.LogObj(rlt);}}";
			if (tScripts.SelectionLength > 0)
			{
				script = string.Format(script, tScripts.Text.Substring(tScripts.SelectionStart, tScripts.SelectionLength));
			}
			else
			{
				script = string.Format(script, tScripts.Text);
			}
			object rlt = controller.ExecuteScript(null, null, null, script);

		}

		private void bRun_Click(object sender, EventArgs e)
		{
			RunScript();
		}

	}
}
