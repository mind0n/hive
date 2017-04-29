using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ULib.Winform.Controls
{
    public partial class TextGridRegion : UserControl
    {
        public TextBox ScriptBox
        {
            get
            {
                return scriptBox;
            }
        }
        public DataGridView GridBox
        {
            get
            {
                return gridBox;
            }
        }
        public TextGridRegion()
        {
            InitializeComponent();
        }
    }
}
