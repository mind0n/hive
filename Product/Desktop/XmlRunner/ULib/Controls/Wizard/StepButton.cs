using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ULib.Controls.Wizard
{
	public class StepButton : Button
	{
		private StepStatus status;

		public StepStatus Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;
				if (status == StepStatus.Active)
				{
					this.BackColor = Color.LightSteelBlue;
					this.FlatAppearance.BorderColor = Color.Blue;
					this.FlatAppearance.BorderSize = 1;
				}
				else if (status == StepStatus.Inactive)
				{
					this.BackColor = Color.Transparent;
					this.FlatAppearance.BorderColor = Color.Gray;
					this.FlatAppearance.BorderSize = 1;
				}
				else
				{
					this.BackColor = Color.Transparent;
					this.FlatAppearance.BorderColor = Color.Black;
					this.FlatAppearance.BorderSize = 1;
				}
			}
		}

		public StepButton()
		{
			FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			Status = StepStatus.Inactive;
			TabStop = false;
			GotFocus += new EventHandler(StepButton_GotFocus);
			Enabled = false;
		}

		void StepButton_GotFocus(object sender, EventArgs e)
		{
			InvokeLostFocus(this, null);
		}
	}
    public enum StepStatus
    {
        Inactive,
        Active,
        Passed
    }
}
