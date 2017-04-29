using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Portal.Winform.Controls
{
    public partial class DropDownSelector : UserControl
    {
        public ComboBox Cb
        {
            get
            {
                return cb;
            }
        }

        public Button Btn
        {
            get
            {
                return bAdd;
            }
        }
        public DropDownSelector()
        {
            InitializeComponent();
        }
    }
}
