using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DAL.DataEntity;
using Joy.Storage;

namespace Portal.Winform.Controls
{
    public partial class UpDownGrid : UserControl
    {
        public delegate void OnLoadHandler();
        public event OnLoadHandler OnGridLoad;

        public DataGridView Gv
        {
            get
            {
                return gv;
            }
        }

        public DataGridView GvDetails
        {
            get
            {
                return gvDetails;
            }
        }

        public UpDownGrid()
        {
            InitializeComponent();
            this.Load += UpDownLoad;
        }


        void UpDownLoad(object sender, EventArgs e)
        {
            if (OnGridLoad != null)
            {
                OnGridLoad();
            }
        }

    }
}
