using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Forms;
using System.Threading;

namespace ULib.Controls.Wizard
{
	public partial class StepsPanel : UserControl
	{
		private Steps steps = new Steps();
		private int index = -1;
		public Control FormRegion;
		public int Count
		{
			get
			{
				return steps.Count;
			}
		}
		public int CurtIndex
		{
			get
			{
				return index;
			}
		}
		public Step CurtStep
		{
			get
			{
				if (index >= 0 && index < steps.Count)
				{
					return steps[index];
				}
				return null;
			}
		}
        public int StepHeight { get; set; }
        public int StepMargin { get; set; }
		public WizardForm Wizard;
        //public List<StepButton> Steps = new List<StepButton>();
		public StepsPanel(WizardForm wizard = null, Control formPanel = null)
		{
			InitializeComponent();
            StepHeight = 24;
			FormRegion = formPanel;
			Wizard = wizard;
		}
		public void Next()
		{
			if (index + 1 >= steps.Count)
			{
				return;
			}
			index++;
			UpdateStatus();
		}
		public void Back(bool isToFirst = false)
		{
			if (index <= 0)
			{
				return;
			}
			if (isToFirst)
			{
				index = 0;
			}
			else
			{
				index--;
			}
			UpdateStatus();
		}
		public void First()
		{
			if (steps.Count < 1)
			{
				return;
			}
			index = 0;
			UpdateStatus();
		}
        public void AddStep(string name, WizardStepForm form)
        {
            StepButton step = new StepButton();
            step.Height = StepHeight;
            step.Text = name;
			form.Wizard = Wizard;
			steps.Add(new Step { Button = step, Form = form, Name = name });
        }
        public void UpdateStatus(bool resetbuttons = false)
        {
			if (steps.Count > 0 && index < 0)
			{
				index = 0;
			}
            int top = StepMargin;
			if (resetbuttons)
			{
				pn.Controls.Clear();
			}
			for (int step = 0; step < steps.Count; step++)
			{
				Step s = steps[step];
				StepButton i = s.Button;
				if (resetbuttons)
				{
					pn.Controls.Add(i);
				}
				i.Top = top;
				i.Left = 0;
				i.Width = pn.Width;
				i.Height = StepHeight;
				if (index == step)
				{
					i.Status = StepStatus.Active;
				}
				else
				{
					i.Status = StepStatus.Inactive;
				}
				top += (StepHeight + StepMargin);
			}
			CurtStep.Button.Status = StepStatus.Active;
			if (FormRegion != null)
			{
				CurtStep.Form.Wizard = Wizard;
				CurtStep.Form.EmbedInto(FormRegion);
			}
        }
	}
	public class Steps : List<Step>
	{
	}
	public class Step
	{
		public StepButton Button;
		public string Name;
		public WizardStepForm Form;
	}
}
