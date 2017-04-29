using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Dw.Window;
using System.Threading;

namespace Dw.Plugins
{
	public partial class CoderForm : PluginForm
	{
		public CoderForm() : base()
		{
			InitializeComponent();
			txtFileName.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			btnOpen.Anchor = AnchorStyles.Right | AnchorStyles.Top;
			WindowState = FormWindowState.Normal;
			StartPosition = FormStartPosition.CenterScreen;
			txtEncoded.MouseDown += new MouseEventHandler(txtEncoded_MouseDown);
		}
		internal bool AllowClose = false;

		
		private void WriteTxtFile(string content, string filename)
		{
			FileStream wfs = new FileStream(filename, FileMode.Create);
			StreamWriter sw = new StreamWriter(wfs);
			sw.Write(content);
			sw.Close();
			wfs.Close();
		}
		private void WriteBinaryFile(string filename, byte[] content)
		{
			FileStream bfs = new FileStream(filename, FileMode.Create, FileAccess.Write);
			bfs.Write(content, 0, content.Length);
			bfs.Close();
		}
		private byte[] ReadTxtFile(string filename)
		{
			byte[] buffer;
			FileStream rfs = null;
			try
			{
				rfs = new FileStream(filename, FileMode.Open, FileAccess.Read);
				int length = (int)rfs.Length;	// get file length
				buffer = new byte[length];		// create buffer
				int count;						// actual number of bytes read
				int sum = 0;					// total number of bytes read

				// read until Read method returns 0 (end of the stream has been reached)
				while ((count = rfs.Read(buffer, sum, length - sum)) > 0)
					sum += count;  // sum is a buffer offset for next reading
			}
			finally
			{
				rfs.Close();
			}
			return buffer;

		}
		private void EncodeFile()
		{
			byte[] bs;
			string encoded;
			if (string.IsNullOrEmpty(txtEncoded.Text))
			{
				FileStream fs = new FileStream(txtFileName.Text, FileMode.Open, FileAccess.Read);
				if (fs.Length > int.MaxValue)
				{
					MessageBox.Show("File too large");
					return;
				}
				bs = new byte[fs.Length];
				fs.Read(bs, 0, (int)fs.Length);
				fs.Close();
			}
			else
			{
				//bs = new byte[txtEncoded.Text.Length];
				UTF8Encoding en = new UTF8Encoding();
				bs = en.GetBytes(txtEncoded.Text);
			}
			encoded = Convert.ToBase64String(bs, Base64FormattingOptions.None);
			if (encoded.Length <= 200000)
			{
				txtEncoded.Text = encoded;
			}
			else
			{
				if (MessageBox.Show("Save to file?", "DeskWall", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
				{
					dlgSf.FilterIndex = 1;
					dlgSf.FileName = "output.txt";
					if (dlgSf.ShowDialog() == DialogResult.OK)
					{
						WriteTxtFile(encoded, dlgSf.FileName);
					}
				}
				else
				{
					txtEncoded.Text = encoded;
				}
			}
		}
		private void DecodeString()
		{
			string base64str;
			if (!string.IsNullOrEmpty(txtFileName.Text))
			{
				byte[] bytes = ReadTxtFile(txtFileName.Text);
				base64str = Encoding.ASCII.GetString(bytes);
			}
			else
			{
				base64str = txtEncoded.Text;
			}
			byte[] decoded = Convert.FromBase64String(base64str);
			if (decoded.Length < 1000)
			{
				if (MessageBox.Show("Save to file?", "DeskWall", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
				{
					UTF8Encoding en = new UTF8Encoding();
					txtEncoded.Text = en.GetString(decoded);
					return;
				}
			}
			dlgSf.FilterIndex = 2;
			dlgSf.FileName = "output.rar";
			if (dlgSf.ShowDialog() == DialogResult.OK)
			{
				WriteBinaryFile(dlgSf.FileName, decoded);
			}
		}
		private void btnOpen_Click(object sender, EventArgs e)
		{
			dlgOf.Multiselect = false;
			if (dlgOf.ShowDialog() == DialogResult.OK)
			{
				txtFileName.Text = dlgOf.FileName;
			}
		}

		private void txtEncoded_MouseDown(object sender, MouseEventArgs e)
		{
				if (e.Button == MouseButtons.Right)
				{
					if (!string.IsNullOrEmpty(txtEncoded.Text))
					{
						txtEncoded.SelectAll();
						txtEncoded.Copy();
					}
					else
					{
						txtEncoded.Paste();
					}
				}
				else if (e.Button == MouseButtons.Middle)
				{
					txtEncoded.Clear();
				}
			
		}
		private void btnRun_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				EncodeFile();
			}
			else if (e.Button == MouseButtons.Middle)
			{
				DecodeString();
			}
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			if (!AllowClose)
			{
				e.Cancel = true;
				Hide();
			}
		}
		protected override void OnShown(EventArgs e)
		{
			Activate();
			base.OnShown(e);
		}
		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void txtEncoded_TextChanged(object sender, EventArgs e)
		{
			txtFileName.Text = "";
		}

		private void CoderForm_DragEnter(object sender, DragEventArgs e)
		{
//			if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
			{
				e.Effect = DragDropEffects.All;
			}
		}

		private void CoderForm_DragDrop(object sender, DragEventArgs e)
		{
			string fullFilename = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
			txtFileName.Text = fullFilename;
			EncodeFile();
			Activate();
		}
	}
}