using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OfficePortal.MsExcel12;
using XL = Microsoft.Office.Interop.Excel;
using Fs.UI.Controls;
using Fs;
using TestOffice;
using OAWidgets.Widget;


namespace OAWidgets
{
	public partial class MailGenerator : BasicWidget
	{
		public override string Id
		{
			get
			{
				return "2f303e17-34fd-496c-a2bd-719c0eede59c";
			}
		}
		public MailGenerator(string baseDir)
		{
			InitializeComponent();
			fopenXL.FileOnOpen += new TestOffice.FileOpener.FileOpenerEventHandler(delegate(FileOpener s)
			{
				Generate(s);
			});
		}
		void Generate(FileOpener s)
		{
			Excel.Book book = null;
			book = Excel.OpenWorkBook(fopenXL.FullPath, false);
			if (book != null)
			{
				Excel.Sheet sheet = book.GetSheetByName("$_ConfigMail");
				sheet.GetRange("A1", "D7").Copy();
				htmlBox.Focus();
				htmlBox.Paste();
				book.Close();
			}
			else
			{
				Excel.Application.Quit();
				MessageBox.Show("Cannot open file ('" + fopenXL.FullPath + "')", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			Generate(fopenXL);
		}
		public override void Launch(PlatformAgent agent)
		{
			EventTabPage page = agent.CreateNewTab("Outlook Mail Generator");
			EmbedInto(page, DockStyle.Fill);
			page.TabOnClose += new EventTabPage.PageEvtHandler(OnClosing);
		}
		public bool OnClosing()
		{
			return true;
		}
	}
}
