using System;
using System.Collections.Generic;
using System.Text;
using Dw.Module;
using System.Windows.Forms;

namespace Dw.Plugins
{
	public class PowerManager : FunctionModule
	{
		protected PowerMgrForm powerfm;
		public PowerManager()
		{
			Name = "PowerManager";
			IsSingleton = true;
			MenuItem NotifierMenuItem = Notifier.AdjustMenuItem(System.Windows.Forms.MenuMerge.Add, "Suspend Computer", delegate(object sender, EventArgs e)
			{
				Start();
			});
			powerfm = new PowerMgrForm();
			powerfm.BelongModule = this;
			powerfm.NotifierMenuItem = NotifierMenuItem;
		}
		public override void Start()
		{
			powerfm.ShowWindow();
		}

	}
}
