using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using Dw.Window;

namespace Dw.Plugins
{
	public partial class PowerMgrForm : PluginForm
	{
		protected delegate void TimeElapseHandler(int secondRemain);
		
		public MenuItem NotifierMenuItem;

		protected string _menuItemString;
		protected TimeElapseHandler OnTimeElapse;
		protected int totalSeconds;
		protected int secondsRemain;
		protected System.Timers.Timer tmr;
		
		public PowerMgrForm()
		{
			InitializeComponent();
			OnTimeElapse += TimeEventCallback;
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			e.Cancel = true;
			Hide();
			base.OnClosing(e);
		}
		private void TimeEventCallback(int sm)
		{
			NotifierMenuItem.Text = _menuItemString + "(" + sm.ToString() + ")";
			if (btnCount.InvokeRequired)
			{
				btnCount.Invoke(new TimeElapseHandler(TimeEventCallback), sm);
			}
			else
			{
				btnCount.Text = "Stop Count\n(" + sm.ToString() + ")";
			}

		}
		private void btnCount_Click(object sender, EventArgs e)
		{
			if (txtSeconds.Enabled)
			{
				tmr = new System.Timers.Timer();
				txtSeconds.Enabled = false;
				_menuItemString = NotifierMenuItem.Text;
				totalSeconds = Convert.ToInt32(txtSeconds.Text);
				secondsRemain = totalSeconds;
				txtSeconds.Enabled = false;
				tmr.Interval = 1000;
				tmr.Elapsed += delegate(object sd, ElapsedEventArgs se)
				{
					tmr.Stop();
					secondsRemain -= 1;
					TimeEventCallback(secondsRemain);
					if (secondsRemain <= 0)
					{
						System.Windows.Forms.Application.SetSuspendState(PowerState.Suspend, true, false);
						return;
					}
					tmr.Start();
				};
				tmr.Enabled = true;
				tmr.Start();
			}
			else
			{
				tmr.Stop();
				tmr.Close();
				NotifierMenuItem.Text = _menuItemString;
				txtSeconds.Enabled = true;
				txtSeconds.Focus();
				tmr.Stop();
				btnCount.Text = "Count";
			}
		}
		private void txtSeconds_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnCount_Click(sender, e);
			}
		}
	}
}
