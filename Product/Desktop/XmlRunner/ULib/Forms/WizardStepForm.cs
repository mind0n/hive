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
	public partial class WizardStepForm : DockForm
	{
		protected bool isStopping;
		public WizardStepForm()
		{
			InitializeComponent();
		}
		public virtual bool PreviewNext()
		{
			return true;
		}
		public virtual void Stop()
		{
			isStopping = true;
		}
		public WizardForm Wizard;
	}
}
