using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DupByKeyword
{
	public partial class Mainform : Form
	{
		public Mainform()
		{
			InitializeComponent();
		}
		private void Mainform_Load(object sender, EventArgs e)
		{
			tp.Text = AppDomain.CurrentDomain.BaseDirectory + "output.txt";
		}

		private void bStartClick(object sender, EventArgs e)
		{
			try
			{
				int num = Convert.ToInt32(tn.Text);
				string ka = ta.Text;
				string kb = tb.Text;
				string file = tp.Text;
				string kt = tt.Text;
				if (File.Exists(file))
				{
					File.Delete(file);
				}
				for (int i = 1; i <= num; i++)
				{
					if (i > 0)
					{
						string c = kt.Replace(ka, i.ToString());
						int len = (int) Math.Floor(Math.Log10(i));
						len = 2 - len;
						string d = i.ToString();
						for (int j = 0; j < len; j++)
						{
							d = "0" + d;
						}
						c = c.Replace(kb, d);
						File.AppendAllText(file, c);
					}
				}
				Process.Start(file);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
