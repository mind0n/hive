using System;
using System.IO;
using System.Windows.Forms;
using ScriptEngine.Frontend;
using ScriptEngine.Core;

namespace TestInterpreter
{
	public partial class MainForm : Form
	{
		//Interpreter engine = Interpreter.CreateInstance();

		public MainForm()
		{
			InitializeComponent();
			Load += new EventHandler(MainForm_Load);
		}
        
		private void Output(bool clear = false, params string [] msg)
		{
			if (clear)
			{
				tResult.Text = "";
			}
            tResult.Text += string.Join("\t", msg) + "\r\n";
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			bLoad_Click(sender, e);
			bParse_Click(sender, e);
		}

		private void bParse_Click(object sender, EventArgs e)
		{
			tResult.Text = "";
			ScriptScanner scanner = new ScriptScanner();
			scanner.Load(tScript.Text);
            foreach (Lex i in scanner.Lexes)
            {
                Output(false, i.Content.ToString(), i.Type.ToString());
            }
			//TokenCollection tokens = engine.Read(tScript.Text);
			//if (tokens != null)
			//{		    
			//    foreach (Token token in tokens)
			//    {
			//        Output(token.LineNumber + ":\t" + token.TokenType + "   " + token.Content);
			//    }
			//    engine.Parse(tokens);
			//}
		}
		private void bLoad_Click(object sender, EventArgs e)
		{
			tScript.Text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Scripts\Test.js");
		}
		private void bExec_Click(object sender, EventArgs e)
		{
		}
	}
}
