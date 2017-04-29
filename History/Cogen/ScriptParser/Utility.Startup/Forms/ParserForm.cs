using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptParser.Common;
using ULib.Winform.Controls;
using System.IO;
using ScriptParser;

namespace Utility.Startup.Forms
{
    public partial class ParserForm : ScreenRegion
    {
        public ParserForm()
        {
            InitializeComponent();
			Load += ParserForm_Load;
        }

		void ParserForm_Load(object sender, EventArgs e)
		{
			string jsfile = AppDomain.CurrentDomain.BaseDirectory + "test.js";
			string content = File.ReadAllText(jsfile);
			txtGrid.ScriptBox.Text = content;
		}

        private void bParse_Click(object sender, EventArgs e)
        {
			string script = txtGrid.ScriptBox.Text;
			Tokens tokens = new Tokens();
			for (int i = 0; i < script.Length; i++)
			{
				tokens.Add(script[i]);
			}
			tokens.Flush();
			txtGrid.GridBox.DataSource = tokens.Content;
			tokens.PrintCache();
        }
    }
}
