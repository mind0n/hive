using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dw.Window;
using Native.Desktop;

namespace Dw.Plugins
{
	public partial class SetRemindersForm : PluginForm
	{
		protected static string SummaryTime = "Reminder will start at {START_TIME}";
		protected static string SummaryDate = "on {START_DATE}";
		protected static string SummaryRepeat = "and {REPEAT}.";
		protected string Settings;
		protected SavableReminder Rmd = new SavableReminder();

		public static SavableReminder Show(SavableReminder rmd)
		{
			SavableReminder rlt;
			SetRemindersForm instance = new SetRemindersForm();
			instance.SetConfigs(rmd);
			if (instance.ShowDialog() == DialogResult.OK)
			{
				if (rmd != null)
				{
					rmd.Reset();
				}
				rlt = instance.Rmd;
			}
			else
			{
				rlt = null;
			}
			return rlt;
		}
		protected void SetConfigs(SavableReminder rmd)
		{
			if (rmd != null)
			{
				cbTime.SelectedIndex = cbTime.FindString(rmd.GetRemindTimeString());
				if (cbTime.SelectedIndex < 0)
				{
					cbTime.SelectedIndex = cbTime.Items.Count / 2;
				}
				mcStart.SetDate(rmd.GetRemindTime());
				if (!rmd.InfiniteLoop)
				{
					chNoRepeat.Checked = true;
				}
				//rdManually.Checked = true;
				if (rmd.ExtendedProperty.ContainsKey("rdname"))
				{
					object rdname = rmd.ExtendedProperty["rdname"];
					RadioButton rb = (RadioButton)Controls.Find(rdname.ToString(), true)[0];
					if (rb != null)
					{
						rb.Checked = true;
					}
				}
				Rmd.FullFilename = rmd.FullFilename;
				Settings = rmd.Settings;
			}
		}
		protected void UpdateSettingString()
		{
			Settings = cbTime.Text + ';' + mcStart.SelectionStart.ToShortDateString() + ';' + hdRepeats.Text;
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			//string rlt = cbTime.Text + ';' + mcStart.SelectionStart.Date.ToShortDateString() + ';' + hdRepeats.Text;
			base.OnClosing(e);
		}
		private void SetRemindersForm_Load(object sender, EventArgs e)
		{
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cbTime_SelectedIndexChanged(object sender, EventArgs e)
		{
			lbSumTime.Text = SummaryTime.Replace("{START_TIME}", cbTime.Text);
			UpdateSettingString();
		}

		private void mcStart_DateChanged(object sender, DateRangeEventArgs e)
		{
			lbSumDate.Text = SummaryDate.Replace("{START_DATE}", e.Start.ToShortDateString());
			UpdateSettingString();
		}
	
		private void chNoRepeat_CheckedChanged(object sender, EventArgs e)
		{
			if (chNoRepeat.Checked)
			{
				grpRepeat.Enabled = false;
				lbSumRepeat.Text = SummaryRepeat.Replace("{REPEAT}", "will not repeat");
				chNoRepeat.Tag = hdRepeats.Text;
				hdRepeats.Text = ";";
			}
			else
			{
				grpRepeat.Enabled = true;
				lbSumRepeat.Text = SummaryRepeat.Replace("{REPEAT}", hdRepeat.Text);
				hdRepeats.Text = chNoRepeat.Tag.ToString();
			}
			UpdateSettingString();
		}
		private void rdGrpRepeat_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rb = (RadioButton)sender;
			if (rb.Checked)
			{
				Rmd.ExtendedProperty["rdname"] = rb.Name;
				hdRepeat.Text = rb.Text;
				hdRepeats.Text = rb.Tag.ToString();
				lbSumRepeat.Text = SummaryRepeat.Replace("{REPEAT}", "repeat " + hdRepeat.Text.ToLower());
				UpdateSettingString();
			}
		}
		private SetRemindersForm()
		{
			InitializeComponent();
			string hour, minute;
			Rmd = new SavableReminder();
			for (int h = 0; h < 24; h++)
			{
				for (int m = 0; m < 60; m += 2)
				{
					if (h < 10)
					{
						hour = "0" + h;
					}
					else
					{
						hour = h.ToString();
					}
					if (m < 10)
					{
						minute = "0" + m;
					}
					else
					{
						minute = m.ToString();
					}
					cbTime.Items.Add(hour + ":" + minute);
				}
			}
			cbTime.Text = cbTime.Items[0].ToString();
			cbTime.DropDownHeight = 300;
			hdRepeats.Text = rdDaily.Tag.ToString();
			mcStart.SetDate(DateTime.Now);
			TopMost = true;
		}
		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Rmd.SetReminder(Settings);
			Close();
		}
		private void txtCustomize_TextChanged(object sender, EventArgs e)
		{
			rdManually.Tag = txtCustomize.Text;
			hdRepeats.Text = txtCustomize.Text;
			UpdateSettingString();
		}

		private void btnRmvRmd_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("This reminder will be removed permenently.\nAre you sure?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
			{
				DialogResult = DialogResult.OK;
				Rmd.Delete();
				Rmd = null;
				Close();
			}
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			Rmd.Reset();
			SetConfigs(Rmd);
		}
	}
}
