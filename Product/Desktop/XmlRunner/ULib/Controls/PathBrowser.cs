using System;
using System.Windows.Forms;
using ULib.Exceptions;
using System.Collections.Generic;

namespace ULib.Controls
{
    public partial class PathBrowser : UserControl
    {
		public System.Windows.Forms.OpenFileDialog FileDialog
		{
			get
			{
				return openFileDlg;
			}
		}
        public ComboBox ComboBox
        {
            get
            {
                return tPath;
            }
        }
		public event Action<PathBrowser> OnTextChanged;
        public event Action<int> OnSelectIndexChanged;
        public bool isOpenFile;
        public string StartupPath
        {
            get
            {
                if (string.IsNullOrEmpty(tPath.Text))
                {
                    return tPath.SelectedItem as string;
                }
                else
                {
                    return tPath.Text;
                }
            }
            set
            {
                tPath.Text = value;
                if (!string.IsNullOrEmpty(value))
                {
                    AddPath(value);
                }
            }
        }
        public System.Windows.Forms.ComboBox.ObjectCollection Items
        {
            get
            {
                return tPath.Items;
            }
        }
        public string SelectedPath
        {
            get
            {
                if (!string.IsNullOrEmpty(tPath.Text))
                {
                    if (!tPath.Items.ContainsPath(tPath.Text))
                    {
                        tPath.Items.Insert(0, tPath.Text);
                        tPath.SelectedIndex = 0;
                    }
                    return tPath.Text;
                }
                return null;
            }
        }
        public PathBrowser()
        {
            InitializeComponent();
            Load += new EventHandler(PathBrowser_Load);
            GotFocus += new EventHandler(PathBrowser_GotFocus);
            bBrowse.GotFocus += new EventHandler(bBrowse_GotFocus);
            Paint += new PaintEventHandler(PathBrowser_Paint);
			DragDrop += new DragEventHandler(PathBrowser_DragDrop);
			tPath.DragDrop += new DragEventHandler(tPath_DragDrop);
			tPath.DragEnter += new DragEventHandler(tPath_DragEnter);
        }

		void tPath_DragDrop(object sender, DragEventArgs e)
		{
			PathBrowser_DragDrop(sender, e);
		}

		void tPath_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		void PathBrowser_DragDrop(object sender, DragEventArgs e)
		{
			object d = e.Data.GetData(DataFormats.FileDrop, true);
			if (d == null)
			{
				d = e.Data.GetData(DataFormats.Text, true);
			}
			if (d != null)
			{
				if (d is string[])
				{
					d = ((string[])d)[0];
				}
				string path = d.ToString();
				StartupPath = path;
			}
		}

        void PathBrowser_Paint(object sender, PaintEventArgs e)
        {
            tPath.SelectionStart = 0;
            tPath.SelectionLength = 0;
        }

        void PathBrowser_Load(object sender, EventArgs e)
        {
            
        }

        void bBrowse_GotFocus(object sender, EventArgs e)
        {
            //tPath.Focus();
        }

        void PathBrowser_GotFocus(object sender, EventArgs e)
        {
            tPath.Select(0, 0);
            //tPath.Focus();
        }
		public bool IsAutoSelect = false;
		public void LoadPaths(params string[] paths)
		{
			tPath.Items.Clear();
			foreach (string path in paths)
			{
				//tPath.Items.Add(path);
				AddPath(path);
			}
			if (IsAutoSelect && tPath.Items.Count > 0)
			{
				tPath.SelectedIndex = 0;
			}
		}

		public void ClearPaths(bool clearText = true)
		{
			tPath.Items.Clear();
			if (clearText)
			{
				tPath.Text = string.Empty;
			}
		}

        public void AddPath(string path = null)
        {
            if (string.IsNullOrEmpty(path) && string.IsNullOrEmpty(tPath.Text))
            {
                return;
            }
            else if (string.IsNullOrEmpty(path))
            {
                path = tPath.Text;
            }
            if (!tPath.Items.ContainsPath(path))
            {
                tPath.Items.Insert(0, path);
            }
        }

		public string[] GetPaths()
		{
			List<string> rlt = new List<string>();
			rlt.Add(StartupPath);
			foreach (object i in tPath.Items)
			{
				if (i != null)
				{
					string s = i.ToString();
					if (!rlt.Contains(s) && !string.IsNullOrEmpty(s.Trim()))
					{
						rlt.Add(s);
					}
				}
			}
			return rlt.ToArray();
		}

        private void bBrowse_Click(object sender, EventArgs e)
        {
            if (isOpenFile)
            {
                openFileDlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
				openFileDlg.Multiselect = false;
				openFileDlg.SupportMultiDottedExtensions = true;
				openFileDlg.CheckFileExists = true;
				if (openFileDlg.ShowDialog() == DialogResult.OK)
				{
					AddPath(openFileDlg.FileName);
					tPath.Text = openFileDlg.FileName;
				}
            }
            else
            {
                folderDlg.ShowNewFolderButton = true;
                folderDlg.SelectedPath = StartupPath;
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    AddPath(folderDlg.SelectedPath);
                    tPath.Text = folderDlg.SelectedPath;
                }
            }
        }

        private void tPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnSelectIndexChanged != null)
            {
                OnSelectIndexChanged(tPath.SelectedIndex);
            }
			if (OnTextChanged != null)
			{
				OnTextChanged(this);
			}
        }

        private void tPath_TextChanged(object sender, EventArgs e)
        {
			if (OnTextChanged != null)
			{
				OnTextChanged(this);
			}
        }

    }
}
