using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utilities.Components
{
    public partial class EditForm : Form
    {
        protected Action<string> OnSaveCallback;

        public EditForm()
        {
            InitializeComponent();
        }
        
        public static void Popup(string content, string title = "Editor", Action<string> saveCallback = null)
        {
            EditForm f = new EditForm();
            f.Text = title;
            f.SetContent(content);
            f.OnSaveCallback = saveCallback;
            f.WindowState = FormWindowState.Maximized;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
        }

        protected void SetContent(string content)
        {
            tEditor.Font = new Font("Arial", 16);
            tEditor.Text = content;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            if (OnSaveCallback != null)
            {
                OnSaveCallback(tEditor.Text);
            }
            Close();
        }
    }
}
