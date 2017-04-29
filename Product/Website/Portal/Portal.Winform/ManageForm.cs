using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Awesomium.Core;

namespace Portal.Winform
{
    public partial class ManageForm : Form
    {
        public ManageForm()
        {
            WebConfig cfg = new WebConfig
            {
                AdditionalOptions = new string[]
                {
                    "--use-gl=desktop", "ignore-gpu-blacklist"
                }
            };
            WebCore.Initialize(cfg);
            InitializeComponent();
            Load += ManageForm_Load;
        }

        void ManageForm_Load(object sender, EventArgs e)
        {
        }
    }
}
