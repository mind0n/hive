using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ULib.Forms
{
    public partial class ModuleForm : FormBase
    {
        public ModuleForm()
        {
            InitializeComponent();
        }
        protected virtual void AddModule(Type moduleType) { throw new NotImplementedException(); }
        protected virtual void ClearModules() { throw new NotImplementedException(); }
    }
}
