
using ULib.Winform.Controls;
using ScriptEngine.Frontend;
using System.IO;
using System.Windows.Forms;
using System;
namespace ScriptPlatform.Screens
{
	public partial class ScannerScreen : ScreenRegion
	{
		TextTreeScreen tts = new TextTreeScreen();
		public ScannerScreen()
		{
			InitializeComponent();
			Load += new System.EventHandler(ScannerScreen_Load);
		}

		void ScannerScreen_Load(object sender, System.EventArgs e)
		{
			tts.EmbedInto(this);
		}

		private void bScan_Click(object sender, System.EventArgs e)
		{
			string testCommentNString = AppDomain.CurrentDomain.BaseDirectory + "scripts\\TestCommentNString.js";
			ScanScriptFile(testCommentNString);
		}

		private void ScanScriptFile(string testCommentNString)
		{
			tts.TreeView.Nodes.Clear();
			tts.Textbox.Text = File.ReadAllText(testCommentNString);
			bLex_Click(this, null);
		}

		void scanner_OnReadTokenCallback(ScriptScanner sender, System.Collections.Generic.List<ScriptEngine.Core.Token> tokens, ScriptEngine.Core.Token token)
		{
			TreeNode node = new TreeNode();
			node.Text = token.Content + " - " + token.Type;
			//TreeNode type = new TreeNode();
			//type.Text = lex.Type.ToString();
			//node.Nodes.Add(type);
			tts.TreeView.Nodes.Add(node);
			tts.TreeView.ExpandAll();
		}

		private void bContext_Click(object sender, EventArgs e)
		{
			string testFunctionContext = AppDomain.CurrentDomain.BaseDirectory + "scripts\\TestFunctionContext.js";
			ScanScriptFile(testFunctionContext);
		}

		private void bLex_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(tts.Textbox.Text))
			{
				ScriptScanner scanner = new ScriptScanner();
				scanner.OnReadTokenCallback += new ScriptScanner.OnReadTokenCallbackHandler(scanner_OnReadTokenCallback);
				scanner.Load(tts.Textbox.Text);
			}
		}
	}
}
