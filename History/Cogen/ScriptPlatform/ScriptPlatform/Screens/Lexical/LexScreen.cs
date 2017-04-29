
using System.Windows.Forms;
using System;
using System.IO;

namespace Platform.Screens
{
	public partial class LexScreen : Screen
	{
		protected SplitTestScreen spScreen = new SplitTestScreen();
		TextBox sb { get { return spScreen.ScriptBox; } }
		TextBox rb { get { return spScreen.ResultBox; } }
		string ScriptFile = AppDomain.CurrentDomain.BaseDirectory + @"\Scripts\Lexical.js";
		public LexScreen()
		{
			InitializeComponent();
			Load += new System.EventHandler(LexScreen_Load);
		}

		void LexScreen_Load(object sender, System.EventArgs e)
		{
			spScreen.EmbedInto(this);
			string scripts = File.ReadAllText(ScriptFile);
			sb.Text = scripts;
		}

		private void bRun_Click(object sender, System.EventArgs e)
		{
			//StringLexReader reader = new StringLexReader();
			//reader.Load(sb.Text);
			//while (true)
			//{
			//    Word word = reader.ReadNext<Word>();
			//    if (word == null)
			//    {
			//        break;
			//    }
			//}
		}
	}
}
