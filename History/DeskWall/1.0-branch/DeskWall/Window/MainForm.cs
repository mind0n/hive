using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Dw.Window;
using Native.Monitor;
using Native.Desktop;
using Fs.IO;
using Fs;
using System.IO;
using Fs.Reflection;

namespace Dw
{
	public partial class MainForm : PluginForm
	{
		public delegate void OnMainFormLoadedDelegate();
		public OnMainFormLoadedDelegate OnMainFormLoaded;
		public bool AllowClose = false;
		internal string BgImgDir
		{
			get
			{
				return _bgImgDir;
			}
			set
			{
				string baseDir = AppDomain.CurrentDomain.BaseDirectory;
				if (baseDir[baseDir.Length - 1] != '\\')
				{
					baseDir += '\\';
				}
				if (string.IsNullOrEmpty(value))
				{
					_bgImgDir = baseDir;
					return;
				}
				if (value.IndexOf(':') >= 1)
				{
					_bgImgDir = value;
				}
				else
				{
					_bgImgDir = baseDir + value;
				}
				if (!Directory.Exists(_bgImgDir))
				{
					_bgImgDir = baseDir + "BgImg";
					Bitmap bi = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
					Graphics g = Graphics.FromImage(bi);
					g.FillRectangle(Brushes.Black, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
					g.Dispose();
					bi.Save(_bgImgDir + "\\_default.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
					bi.Dispose();
				}
				ClassHelper.CreateThread(delegate(object par)
				{
					bool rlt = DiskHelper.EnumDirectory(_bgImgDir,
					delegate(string path,
								DiskHelper.DirectoryItemType type)
					{
						if (type == DiskHelper.DirectoryItemType.File)
						{
							try
							{
								FileInfo fi = new FileInfo(path);
								if (fi.Extension != "ini" && fi.Extension != "db")
								{
									Bitmap bi = new Bitmap(path);
									bgs.Add(path);
								}
							}
							catch (Exception err)
							{
								Exceptions.LogOnly(err);
							}
						}
						return true;
					});
				});
			}
		}protected string _bgImgDir;

		protected List<string> bgs = new List<string>();
		protected Random rnd = new Random();

		protected System.Timers.Timer tmr;
		public MainForm() : base()
		{
			InitializeComponent();
			Dispose += Dispose_Delegate;
			int scrHeight, scrWidth;
			Left = 0;
			Top = 0;
			FormBorderStyle = FormBorderStyle.None;
			WindowState = FormWindowState.Normal;
			scrHeight = Screen.PrimaryScreen.Bounds.Height; //System.Windows.SystemParameters.PrimaryScreenHeight;
			scrWidth = Screen.PrimaryScreen.Bounds.Width; //System.Windows.SystemParameters.PrimaryScreenWidth;
			Width = scrWidth;
			Height = scrHeight;
			tmr = new System.Timers.Timer();
			tmr.Interval = 399;
			tmr.Enabled = true;
			tmr.Elapsed += new System.Timers.ElapsedEventHandler(tmr_Elapsed);
			tmr.Start();
			Opacity = 0;
			ShowInTaskbar = false;
			MsgFilter.OnMsgProcess += new MsgFilter.MsgProcessDelegate(delegate(ref Message m)
			{
				switch (m.Msg)
				{
					//WM_QUERYENDSESSION
					case (0x0011):
						m.Result = IntPtr.Zero;
						return true;
					//WM_SYSCOMAND SC_SCREENSAVE SC_MONITORPOWER
					case (0x0112):
						if (m.LParam == (IntPtr)0xF140 || m.LParam == (IntPtr)0XF170)
						{
							m.Result = IntPtr.Zero;
							return true;
						}
						else
						{
							return false;
						}
					default:
						return false;
				}
			});
			MsgFilter.Start();
		}
		public void HideScreen()
		{
			TopMost = false;
			Opacity = 0;
			Hide();
		}
		public void UnlockScreen()
		{
			if (TopMost)
			{
				TopMost = false;
				SendKeys.SendWait("%{Tab}");
				Taskbar.AdjustSizeAgainstTaskBar(this);
			}
		}
		public void LockScreen()
		{
			if (Opacity <= 0)
			{
				if (bgs.Count >= 1)
				{
					SwitchImage();
				}
				Opacity = 1;
			}
			//BgPic.SizeMode = PictureBoxSizeMode.StretchImage;
			Width = Screen.PrimaryScreen.Bounds.Width;
			Height = Screen.PrimaryScreen.Bounds.Height;
			ShowInTaskbar = false;
			TopMost = true;
			Show();
		}
		public void SwitchImage()
		{
			Bitmap bi = null;
			if (BgPic != null && BgPic.Image != null)
			{
				bi = new Bitmap(BgPic.Image);
			}
			BgPic.Image = GetRandomImg();
			if (bi != null)
			{
				bi.Dispose();
			}
		}
		Bitmap GetRandomImg()
		{
			Bitmap rlt = null;
			int index;
			if (bgs.Count > 0)
			{
				for (int i = 1; i <= 10; i++)
				{
					index = rnd.Next(1, bgs.Count + 1) - 1;
					try
					{
						rlt = new Bitmap(bgs[index]);
						break;
					}
					catch (Exception err)
					{
						Exceptions.LogOnly(err);
					}
				}
			}
			return rlt;
		}

		void tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			Screensaver.KillScreenSaver(false);
			if (TopMost)
			{
				Invoke((MethodInvoker)delegate()
				{
					Activate();
				});
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			if (bgs.Count > 0)
			{
				BgPic.Image = GetRandomImg();
			}
			if (OnMainFormLoaded != null)
			{
				OnMainFormLoaded();
			}
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			if (AllowClose)
			{
				return;
			}
			else
			{
				e.Cancel = true;
			}
		}
	}
}