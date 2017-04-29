using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ULib.Exceptions;

namespace ULib.Controls
{
    public class HistoryCombox : System.Windows.Forms.ComboBox
    {
        public string CacheFile { get; set; }
        public HistoryCombox()
        {
        }
        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
        }

        private void SortHistory()
        {
            List<object> list = new List<object>();
            foreach (object i in this.Items)
            {
                if (i != null)
                {
                    list.Add(i);
                }
            }
            list.Sort();
            Items.Clear();
            foreach (object i in list)
            {
                Items.Add(i);
            }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
        }
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            SaveHistory();
            base.OnSelectedIndexChanged(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(this.Text))
                {
                    if (!Items.Contains(this.Text))
                    {
                        Items.Insert(0, this.Text);
                    }
                    SaveHistory(true);
                }
            }
            base.OnKeyDown(e);
        }
        private void SaveHistory(bool sort = false)
        {
            if (sort)
            {
                SortHistory();
            }
            try
            {
                if (!string.IsNullOrEmpty(CacheFile))
                {
                    if (File.Exists(CacheFile))
                    {
                        File.Delete(CacheFile);
                    }
                    if (!string.IsNullOrEmpty(Text))
                    {
                        File.WriteAllText(CacheFile, Text);
                    }
                    foreach (string i in this.Items)
                    {
                        if (!string.IsNullOrEmpty(i))
                        {
                            File.AppendAllText(CacheFile, i);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
        protected override void OnParentChanged(EventArgs e)
        {
            GenerateCacheFilename();
            base.OnParentChanged(e);
        }

        private void GenerateCacheFilename()
        {
            List<string> list = new List<string>();
            Control c = this;
            while (c.Parent != null)
            {
                list.Add(c.Name);
                c = c.Parent;
            }
            CacheFile = string.Concat(AppDomain.CurrentDomain.BaseDirectory, string.Join("-", list.ToArray()), ".dat");
            File.WriteAllText(CacheFile, string.Empty);
        }
    }
}
