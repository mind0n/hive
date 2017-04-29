using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptPad.Components;

namespace ScriptPad
{
    public partial class Mainform : Form
    {
        private ScriptExecutor executor;
        public Mainform()
        {
            InitializeComponent();
            Load += Mainform_Load;
        }

        void Mainform_Load(object sender, EventArgs e)
        {
            executor = new ScriptExecutor(txt);
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var r = executor.Run();
            MessageBox.Show(r.Result.ToString());
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
