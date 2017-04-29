using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OfficePortal.MsExcel12;
using Fs.UI.Controls;
using Fs;
using TestOffice;
using OAWidgets.Widget;
using XL = Microsoft.Office.Interop.Excel;
using OfficePortal.MsOutlook12;
using Fs.Core;


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
				Excel.Book book = Excel.OpenWorkBook(fopenXL.FullPath, false);
				if (book != null)
				{
					FetchSheets(book);
					book.Close();
				}
				else
				{
					Excel.Application.Quit();
					MessageBox.Show("Cannot open file ('" + fopenXL.FullPath + "')", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			});
		}
		void FetchSheets(Excel.Book book)
		{
			cbSheets.Items.Clear();
			cbSheets.DisplayMember = "SheetName";
			cbSheets.ValueMember = "SheetName";
			book.EnumSheets(delegate(Excel.Sheet sheet)
			{
				if (sheet != null)
				{
					cbSheets.Items.Add(sheet.SheetName);
					cbSheets.Update();
					return true;
				}
				return false;
			});
			cbSheets.Update();
			cbSheets.SelectedIndex = 0;
		}
		void Generate()
		{
			Excel.Book book = Excel.OpenWorkBook(fopenXL.FullPath, false);
			if (book != null)
			{
				string sheetName = (string)cbSheets.SelectedItem;
				if (string.IsNullOrEmpty(sheetName))
				{
					MessageBox.Show("Sheet missing error, please select sheet before loading template.", "Error: Sheet not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				Excel.Sheet sheet = book.GetSheetByName(sheetName);
				Excel.Range startPos = sheet.GetRange("A2", "A2");
				while (!startPos.IsEmpty)
				{
					ConfigRangeItem item = new ConfigRangeItem(startPos, typeof(Processor));
					if (item.IsEnabled)
					{
						if (item.Name == "To")
						{
							txtSender.Text = item.GetValue<string>(0, null);
						}
						else if (item.Name == "CC")
						{
							txtCC.Text = item.GetValue<string>(0, null);
						}
						else if (item.Name == "File")
						{
							listAttaches.Items.Add(item.GetValue<string>(0, null));
						}
						else if (item.Name == "Subject")
						{
							txtSubject.Text = item.GetValue<string>(0, null);
						}
						else
						{
							foreach (Excel.Range content in item.Values)
							{
								content.Copy(delegate(object range)
								{
									Excel.Range r = (Excel.Range)range;
									if (!string.IsNullOrEmpty(r.Comment))
									{
										r.Value = item.ProcessValue(r.Value, r.Comment);
									}
								});
								Clipboard.GetData(DataFormats.Text);
								htmlBox.Focus();
								htmlBox.Paste();
							}
						}
					}
					startPos = startPos.Offset(1, 0);
				}
				book.Close();
			}
			else
			{
				MessageBox.Show("Please select template Excel file first.");
			}
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
		private void btnSendMail_Click(object sender, EventArgs e)
		{
			OutlookPortal op = new OutlookPortal();
			foreach (object attach in listAttaches.Items)
			{
				op.AttachFile((string)attach, (string)attach);
			}
			op.SetValue(txtSender.Text, htmlBox.HtmlText, txtCC.Text, txtSubject.Text);
			if (MessageBox.Show("Mail ready, send now?", "Mail Sending", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
			{
				op.Send();
			}
			op = null;
		}
		private void btnLoadTemplate_Click(object sender, EventArgs e)
		{
			Generate();
		}
		private void btnGenerate_Click(object sender, EventArgs e)
		{
			Generate();
		}

		private void btnOpenBook_Click(object sender, EventArgs e)
		{
			fopenXL.Open();
		}
	}
}
