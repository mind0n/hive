using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Joy.Storage;

namespace Portal.Winform
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
            FileAdapter fa = new FileAdapter();
            StorageManager.Instance.Initialize(fa);
            StorageManager.Instance.Clear();

            Load += Mainform_Load;
            FormClosing += Mainform_FormClosing;
        }

        void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            StorageManager.Instance.Save(); 
        }

        void Mainform_Load(object sender, EventArgs e)
        {
        }
    }
}
