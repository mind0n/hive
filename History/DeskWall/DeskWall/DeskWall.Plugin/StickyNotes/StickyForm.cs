using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dw.Window;
using Dw.Module;
using System.IO;
using Fs;
using System.IO.Compression;
using Fs.IO.Compression;
using Native.Desktop;
using Fs.Reflection;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace Dw.Plugins
{

	public partial class StickyForm : PluginForm
	{
		public delegate void FormLoadedExecutionDelegate();
		public delegate void ControllerShowNoteDelegate(StickyForm win);
		public FormLoadedExecutionDelegate FormLoaded;
		public StickyNotesController Controller;
		public StickyForm()
		{
			InitializeComponent();
			ControlBox = false;
			Text = null;
			Windows = new List<WindowItem>();
			ShowInTaskbar = false;

			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			TitleBar.MouseDown += OnMouseDown;
			TitleBar.MouseUp += OnMouseUp;
			TitleBar.MouseMove += OnMouseMove;
			txtContent.LinkClicked += new LinkClickedEventHandler(txtContent_LinkClicked);
			txtContent.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
			TitleBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			tmr = new System.Timers.Timer();
			tmr.Interval = 1027;
			tmr.Elapsed += new System.Timers.ElapsedEventHandler(tmr_Elapsed);
			tmr.Enabled = false;
			Width = 200;
			Height = 120;

			StartPosition = FormStartPosition.CenterScreen;
		}

		void txtContent_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			Process.Start(e.LinkText);
		}

		void tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			tmr.Stop();
			UpdateNotifierItemText();
			if (!IsTyping)
			{
				try
				{
					SaveNoteToFile();
				}
				catch (Exception err)
				{
					Exceptions.Log(err);
				}
			}
			else
			{
				tmr.Start();
			}
		}

		protected delegate void VoidDlgString(string str);
		protected delegate void VoidDlgVoid();

		protected SavableReminder rmd;

		public MenuItem NotifierMenuItem
		{
			set
			{
				_notifierMenuItem = value;
				UpdateNotifierItemText();
			}
			get
			{
				return _notifierMenuItem;
			}
		}protected MenuItem _notifierMenuItem;
		public StickyForm ParentWindow = null;
		public new StickyNote BelongModule;
		public string FileName // = string.Empty;
		{
			get
			{
				return _filename;
			}
			set
			{
				_filename = value;
				NoteContent = LoadNoteFromFile(_filename);
				string fname = _filename + ".rmd";
				if (File.Exists(fname))
				{
					rmd = SavableReminder.Load(fname);
					if (rmd != null)
					{
						rmd.OnElapsed += OnRemind;
						rmd.OnElapsing += OnElapsing;
						rmd.Start();
					}
				}
				UpdateNotifierItemText();
				//Hide();
			}
		}protected string _filename;
		protected List<WindowItem> Windows;
		protected System.Timers.Timer tmr;
		protected bool IsTyping = false;
		protected string NoteContent
		{
			get
			{
				RichTextBox rb = new RichTextBox();
				rb.Rtf = _noteContent;
				return rb.Text;
			}
			set
			{
				_noteContent = value;
			}
		}protected string _noteContent;

		public void DeleteNote()
		{
			tmr.Stop();
			if (rmd != null)
			{
				rmd.Stop();
			}
			try
			{
				File.Delete(FileName);
				if (File.Exists(FileName + ".rmd"))
				{
					File.Delete(FileName + ".rmd");
				}
			}
			catch (Exception err)
			{
				Exceptions.Log(err);
			}
		}
		public void SaveNoteToFile()
		{
			Invoke((MethodInvoker)delegate
			{
				byte[] cmp = GZip.Zip(txtContent.Rtf);
				GZip.SaveToFile(FileName, cmp);
			});
		}
		public string LoadNoteFromFile(string path)
		{
			string rlt;
			_filename = path;

			byte[] cmp = GZip.ReadFromFile(FileName);
			rlt = GZip.UnZip(cmp);
			return rlt;
		}
		public void UpdateNotifierItemText()
		{
			int clen = 32;
			string caption = string.Empty;
			if (!string.IsNullOrEmpty(NoteContent))
			{
				int n = NoteContent.IndexOf('\n');
				if (n >= 0)
				{
					caption = NoteContent.Substring(0, n);
					if (caption.Length > clen)
					{
						caption = caption.Substring(0, clen - 3) + "...";
					}
				}
				else
				{
					if (NoteContent.Length > clen)
					{
						caption = NoteContent.Substring(0, clen - 3) + "...";
					}
					else
					{
						caption = NoteContent;
					}
				}
			}
			if (string.IsNullOrEmpty(caption) || caption.Equals(" "))
			{
				caption = DateTime.Now.ToString();
			}
			if (NotifierMenuItem.Tag != null)
			{
				NotifierMenuItem.Tag = caption;
			}
			else
			{
				NotifierMenuItem.Text = caption;
			}
		}
		protected void LoadNote()
		{
			string note;
			if (string.IsNullOrEmpty(FileName))
			{
				_filename = BelongModule.NoteDir + Guid.NewGuid() + ".txt";
				note = "";
			}
			else
			{
				note = LoadNoteFromFile(FileName);
			}
			StreamWriter sw = File.CreateText(FileName);
			sw.Close();
			txtContent.Rtf = note;
		}
		protected override void OnActivated(EventArgs e)
		{
			UpdateNotifierItemText();
			TopMost = true;
			base.OnActivated(e);
		}
		private bool OnRemind(Reminder rmd)
		{
			if (Controller.InvokeRequired)
			{
				Controller.Invoke(new ControllerShowNoteDelegate(Controller.ShowNote), new object[] { this });
			}
			else
			{
				Controller.ShowNote(this);
			}
			return true;
		}
		protected override void OnLoad(EventArgs e)
		{
			IsFormLoaded = true;
			Left = 0;
			Top = Top * 2;
			LoadNote();
			if (FormLoaded != null)
			{
				FormLoaded();
				FormLoaded = null;
			}
			base.OnLoad(e);
		}
		protected void OnMouseDown(object sender, MouseEventArgs e)
		{
			this.IsDragging = true;
			this.ClickedPoint = new Point(e.X, e.Y);
			base.OnMouseDown(e);
		}
		protected void OnMouseUp(object sender, MouseEventArgs e)
		{
			this.IsDragging = false;
			base.OnMouseUp(e);
		}
		protected void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (this.IsDragging)
			{
				Point NewPoint;
				NewPoint = this.PointToScreen(new Point(e.X, e.Y));
				//The new point is relative to the original point
				NewPoint.Offset((this.ClickedPoint.X + BorderWidth) * -1,
							(this.ClickedPoint.Y + BorderWidth) * -1);
				//Finally, assign the form's location to the              
				//determined new point
				this.Location = NewPoint;
			}
			base.OnMouseMove(e);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Hide();
		}
		private void btnNew_Click(object sender, EventArgs e)
		{
			StickyForm sf = BelongModule.RegistNewWindow();
			sf.Show();
		}
		
		private void btnDel_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (MessageBox.Show("Are you sure you want to remove this note permanently?", "Remove Note", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					DeleteNote();
					BelongModule.UnregistWindow(this);
					Close();
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (MessageBox.Show("Are you sure you want to remove *ALL* notes permanently?", "Remove Note", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					BelongModule.EnumWindows(delegate(StickyForm window)
					{
						window.DeleteNote();
						return true;
					});
					BelongModule.ClearWindows();
				}
			}
		}
		private void btnHideAll_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Hide();
			}
			else if (e.Button == MouseButtons.Right)
			{
				BelongModule.HideAllWindows();
			}
		}

		private void txtContent_TextChanged(object sender, EventArgs e)
		{
			tmr.Stop();
			tmr.Enabled = false;
			NoteContent = txtContent.Rtf;
			tmr.Enabled = true;
			tmr.Start();
		}

		private void txtContent_KeyUp(object sender, KeyEventArgs e)
		{
			IsTyping = false;
		}

		private void txtContent_KeyDown(object sender, KeyEventArgs e)
		{
			IsTyping = true;
		}

		private void TitleBar_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			txtContent.Cut();
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			txtContent.Copy();
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			txtContent.Paste();
		}

		private void reminderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SavableReminder newRmd = SetRemindersForm.Show(rmd);
			if (newRmd != null)
			{
				newRmd.OnElapsed += OnRemind;
				newRmd.OnElapsing += OnElapsing;
				newRmd.OnElapsedComplete += delegate(Reminder r, bool excluded)
				{
					SavableReminder rr = (SavableReminder)r;
					rr.Save(this.FileName + ".rmd");
					return true;
				};
				newRmd.Save(FileName + ".rmd");
				newRmd.Start();
				rmd = newRmd;
			}
			else
			{
				if (NotifierMenuItem.Tag != null)
				{
					NotifierMenuItem.Text = (string)NotifierMenuItem.Tag;
				}
			}

		}
		private void OnElapsing(Reminder rmd, TimeSpan remains)
		{
			if (NotifierMenuItem.Tag == null)
			{
				NotifierMenuItem.Tag = NotifierMenuItem.Text;
			}
			string strRemain = string.Format("{0:D3},{1:D2}:{2:D2}:{3:D2}",
						remains.Days,
						remains.Hours,
						remains.Minutes,
						remains.Seconds);

			NotifierMenuItem.Text = NotifierMenuItem.Tag + " ( " + strRemain + " ) " + rmd.RemindPoint.ToString();
		}
		private void txtContent_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				cxText.Show(e.X + Left, e.Y + Top + TitleBar.Height);
			}
		}
	}
}