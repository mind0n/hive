using System.Windows.Forms;
using System;

namespace ULib.Forms
{
	public class FormBase:Form
	{
		public FormBase()
		{
			InitializeComponent();
            this.TopLevel = false;
		}
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // FormBase
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FormBase";
            this.ResumeLayout(false);

		}
	}
}
