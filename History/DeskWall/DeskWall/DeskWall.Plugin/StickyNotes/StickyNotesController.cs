using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dw.Module;
using System.IO;
using Fs.IO;

namespace Dw.Plugins
{
	public partial class StickyNotesController : Form
	{
		public StickyNote BelongModule;
		public StickyNotesController()
		{
			InitializeComponent();
			ShowInTaskbar = false;
			Visible = false;
			Opacity = 0;
		}
		public void ShowNote(StickyForm window)
		{
			if (window.InvokeRequired)
			{
				window.Invoke(window.ShowWindow);
			}
			else
			{
				window.ShowWindow();
			}
		}
		protected override void OnLoad(EventArgs e)
		{
			Hide();

			BelongModule.NoteDir = BelongModule.BaseDir + "\\Notes\\";
			if (!Directory.Exists(BelongModule.NoteDir))
			{
				Directory.CreateDirectory(BelongModule.NoteDir);
			}
			else
			{
				DiskHelper.EnumDirectory(BelongModule.NoteDir, delegate(string fullName, DiskHelper.DirectoryItemType type)
				{
					FileInfo info = new FileInfo(fullName);
					string ext = info.Extension;
					if (!string.IsNullOrEmpty(ext))
					{
						ext = ext.ToLower();
						if (ext.Equals(".txt"))
						{
							StickyForm sf = BelongModule.RegistNewWindow();
							sf.Controller = this;
							sf.FileName = fullName;
						}
					}
					return true;
				});
			}


			base.OnLoad(e);
		}
	}
}