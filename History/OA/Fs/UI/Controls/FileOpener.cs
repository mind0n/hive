using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Fs.Xml;
using Fs;
using System.IO;

namespace TestOffice
{
	public partial class FileOpener : UserControl
	{
		public delegate void FileOpenerEventHandler(FileOpener sender);
		public FileOpenerEventHandler FileOnOpen;
		public Control FileOpenButton
		{
			set
			{
				value.Click += new EventHandler(value_Click);
			}
		}
		public bool IsReady
		{
			get
			{
				if (File.Exists(FullPath))
				{
					return true;
				}
				return false;
			}
		}
		public string FullPath
		{
			get
			{
				return txtFilename.Text;
			}
			set
			{
				txtFilename.Text = value;
			}
		}
		public string Filename
		{
			get
			{
				return dlgOpen.SafeFileName;
			}
		}
		public FileOpener()
		{
			InitializeComponent();
		}
		public void Open()
		{
			if (IsReady && FileOnOpen != null)
			{
				XReader xr = Configuration.GetConfigFileReader("FileOpener")["root"][Parent.Name];
				if (!xr.Exist("RecentFiles", FullPath))
				{
					xr.AddValue("RecentFiles", FullPath);
					xr.Save();
				}
				FileOnOpen(this);
			}
		}
		protected override void OnLoad(EventArgs e)
		{
			XReader xr = Configuration.GetConfigFileReader("FileOpener");
			XReader recentFiles = xr["root"][Parent.Name];
			recentFiles.EnumChilds(delegate(object i)
			{
				XReader item = (XReader)i;
				if (!item.IsEmpty)
				{
					txtFilename.Items.Add(item.Value);
					txtFilename.Update();
					txtFilename.SelectedIndex = 0;
					dlgOpen.FileName = (string)txtFilename.SelectedItem;
				}
				return true;
			});
		}
		private void btnOpen_Click(object sender, EventArgs e)
		{
			if (dlgOpen.ShowDialog() == DialogResult.OK)
			{
				FullPath = dlgOpen.FileName;
				txtFilename.Text = FullPath;
				Open();
			}
		}
		private void value_Click(object sender, EventArgs e)
		{
			Open();
		}
	}
}
