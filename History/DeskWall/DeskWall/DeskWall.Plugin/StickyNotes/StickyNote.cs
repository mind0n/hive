using System;
using System.Collections.Generic;
using System.Text;
using Dw.Module;
using System.Windows.Forms;
using System.IO;
using Fs.IO;
using Fs.Xml;
using Native.Monitor;
using Fs;

namespace Dw.Plugins
{
	public class StickyNote : FunctionModule
	{
		public delegate bool WindowEnumHandler(StickyForm window);
		public string NoteDir;
		public string BackupDir
		{
			set
			{
				_backupDir = value;
			}
			get
			{
				if (Directory.Exists(_backupDir))
				{
					return _backupDir;
				}
				else
				{
					_backupDir = BaseDir + "\\Backup";
					return _backupDir;
				}
			}
		}protected string _backupDir;
		protected StickyNotesController Controller;
		protected List<StickyForm> Windows;
		protected MenuItem IconMenuItem
		{
			set
			{
				IconMenuItemText = value.Text;
				_iconMenuItem = value;
				MenuItem mShowAll = new MenuItem("Show All Notes", delegate(object sender, EventArgs e)
				{
					ShowAllWindows();
				});
				MenuItem mHideAll = new MenuItem("Hide All Notes", delegate(object sender, EventArgs e)
				{
					HideAllWindows();
				});
				MenuItem mCloseAll = new MenuItem("Delete All Notes", delegate(object sender, EventArgs e)
				{
					if (MessageBox.Show("Are you sure to delete *ALL* sticky notes?", "Remove All Sticky Notes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
					{
						ClearWindows();
					}
				});
				MenuItem mNewNote = new MenuItem("New Sticky Note", delegate(object sender, EventArgs e)
				{
					NewNote();
				});
				MenuItem mBackup = new MenuItem("Backup All Notes", delegate(object sender, EventArgs e)
				{
					string backupDir = BackupDir;
					if (!Directory.Exists(backupDir))
					{
						Directory.CreateDirectory(backupDir);
					}
					FolderBrowserDialog fbd = new FolderBrowserDialog();
					fbd.ShowNewFolderButton = true;
					fbd.SelectedPath = backupDir;
					fbd.Description = "Select backup directory.  All notes will be copied there.";
					if (fbd.ShowDialog() == DialogResult.OK)
					{
						string [] files = Directory.GetFiles(NoteDir);
						backupDir = fbd.SelectedPath;
						foreach (string fullfilename in files)
						{
							string filename;
							int n = fullfilename.LastIndexOf('\\');
							filename = fullfilename.Substring(n + 1);
							File.Copy(NoteDir + '\\' + filename, backupDir + '\\' + filename, true);
						}
						XReader xr = new XReader(ConfigFilePath);
						xr["Plugin"]["$BackupPath"].Value = backupDir;
						xr.Save(ConfigFilePath);
						MessageBox.Show("Backup Complete", "Sticky Notes");
					}

				});
				MenuItem mRestore = new MenuItem("Restore All Notes", delegate(object sender, EventArgs e)
				{
					string backupDir = BackupDir;
					if (!Directory.Exists(backupDir))
					{
						backupDir = AppDomain.CurrentDomain.BaseDirectory;
					}
					FolderBrowserDialog fbd = new FolderBrowserDialog();
					fbd.ShowNewFolderButton = true;
					fbd.SelectedPath = backupDir;
					fbd.Description = "Select directory where all backuped notes exists.";
					if (MessageBox.Show("All notes existed might be overwriten, are you sure?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK && fbd.ShowDialog() == DialogResult.OK)
					{
						string[] files = Directory.GetFiles(NoteDir);
						backupDir = fbd.SelectedPath;
						foreach (string fullfilename in files)
						{
							string filename;
							int n = fullfilename.LastIndexOf('\\');
							filename = fullfilename.Substring(n + 1);
							File.Copy(backupDir + '\\' + filename, NoteDir + '\\' + filename, true);
						}
						MessageBox.Show("Restore Complete", "Sticky Notes");
					}
				});
				MenuItem mView = new MenuItem("Display Last Backuped Notes", delegate(object sender, EventArgs e)
				{
					System.Diagnostics.Process.Start(BackupDir);
				});
				value.MenuItems.Add(mNewNote);
				value.MenuItems.Add("-");
				value.MenuItems.Add(mShowAll);
				value.MenuItems.Add(mHideAll);
				value.MenuItems.Add(mCloseAll);
				value.MenuItems.Add("-");
				value.MenuItems.Add(mBackup);
				value.MenuItems.Add(mRestore);
				value.MenuItems.Add(mView);
				value.MenuItems.Add("-");
				DeskWall mm = DeskWall.GetInstance();
				mm.OnKeyDown += new KeyEventHandler(delegate(object sender, KeyEventArgs e)
				{
					if (e.KeyCode == Keys.N && KeyboardStatus.Shift && KeyboardStatus.Alt)
					{
						NewNote();
					}
				});
				mm.OnKeyUp += new KeyEventHandler(delegate(object sender, KeyEventArgs e)
				{

				});
			}
			get
			{
				return _iconMenuItem;
			}
		}

		protected MenuItem _iconMenuItem;
		protected string IconMenuItemText;
		public void NewNote()
		{
			try
			{
				StickyForm sf = RegistNewWindow();
				sf.Controller = Controller;
				Controller.ShowNote(sf);
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
			}
		}
		public StickyForm RegistNewWindow()
		{
			StickyForm window = new StickyForm();
			MenuItem mi = new MenuItem();
			mi.Click += delegate(object sender, EventArgs e)
			{
				if (window.InvokeRequired)
				{
					window.Invoke(window.ShowWindow);
				}
				else
				{
					window.ShowWindow();
				}
			};
			IconMenuItem.MenuItems.Add(mi);
			window.BelongModule = this;
			window.NotifierMenuItem = mi;
			Windows.Add(window);
			IconMenuItem.Text = IconMenuItemText + " - " + Windows.Count;
			return window;
		}
		public void UnregistWindow(StickyForm window)
		{
			IconMenuItem.MenuItems.Remove(window.NotifierMenuItem);
			Windows.Remove(window);
			IconMenuItem.Text = IconMenuItemText + " - " + Windows.Count;
		}
		public void ClearWindows()
		{
			EnumWindows(delegate(StickyForm wi)
			{
				wi.DeleteNote();
				UnregistWindow(wi);
				wi.CloseWindow();
				return true;
			});
			Windows.RemoveRange(0, Windows.Count);
		}
		public void ShowAllWindows()
		{
			EnumWindows(delegate(StickyForm window)
			{
				window.ShowWindow();
				return true;
			});
		}
		public void HideWindow(StickyForm window)
		{
			window.HideWindow();
		}
		public void HideAllWindows()
		{
			EnumWindows(delegate(StickyForm wi)
			{
				wi.HideWindow();
				return true;
			});
		}
		public bool EnumWindows(WindowEnumHandler callback)
		{
			for (int i = Windows.Count - 1; i >= 0; i--)
			{
				if (!callback(Windows[i]))
				{
					return false;
				}
			}
			return true;
		}
		protected override void Init()
		{
			XReader xr = new XReader(ConfigFilePath);
			BackupDir = xr["Plugin"]["$BackupPath"].Value;
			if (!Directory.Exists(BackupDir) || BackupDir.IndexOf(':') < 0)
			{
				BackupDir = BaseDir + '\\' + BackupDir;
			}
			IconMenuItem = Notifier.AdjustMenuItem(System.Windows.Forms.MenuMerge.Add, "Sticky Note", delegate(object sender, EventArgs e)
			{
				if (Windows.Count <= 0)
				{
					StickyForm sf = RegistNewWindow();
					sf.ShowWindow();
				}
				else
				{
					EnumWindows(delegate(StickyForm wi)
					{
						//wi.Invoke(wi.ShowWindow);
						wi.ShowWindow();
						return true;
					});
				}
			});
		}
		protected void OnLoad(DeskWall sender, PluginItem pl)
		{
			Init();
			Controller = new StickyNotesController();
			Controller.BelongModule = this;
			Application.Run(Controller);
		}
		public StickyNote()
		{
			Name = "StickyNote";
			IsSingleton = true;
			DeskWall mi = DeskWall.GetInstance();
			mi.OnPluginsLoadComplete += OnLoad;
			Windows = new List<StickyForm>();
		}
	}
}
