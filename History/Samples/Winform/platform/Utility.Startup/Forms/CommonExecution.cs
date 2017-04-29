using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Winform.Controls;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Utility.Startup.Forms
{
    public partial class CommonExecution : ScreenRegion
    {
        public CommonExecution()
        {
            InitializeComponent();
        }

        private void bReduceSqlMem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("server=192.168.220.128;database=master;user=sa;pwd=1w1w!W!W"))
                {
                    using (SqlCommand cm = conn.CreateCommand())
                    {
                        conn.Open();
                        cm.CommandText = "DBCC DROPCLEANBUFFERS";
                        cm.CommandType = CommandType.Text;
                        cm.ExecuteNonQuery();
                    }
                }
                if (MessageBox.Show("Done, close?", "Choice", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
