namespace ULib.Forms
{
	partial class WizardForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			StepPanel = new ULib.Controls.Wizard.StepsPanel(this, pnMain);
			this.pnTotal = new System.Windows.Forms.Panel();
			this.pnRight = new System.Windows.Forms.Panel();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnController = new System.Windows.Forms.Panel();
			this.bStart = new System.Windows.Forms.Button();
			this.bBack = new System.Windows.Forms.Button();
			this.bNext = new System.Windows.Forms.Button();
			this.bStop = new System.Windows.Forms.Button();
			this.pnLeft = new System.Windows.Forms.Panel();
			this.pnTotal.SuspendLayout();
			this.pnRight.SuspendLayout();
			this.pnController.SuspendLayout();
			this.SuspendLayout();

			//
			// pnLeft
			//
			pnLeft.Controls.Add(StepPanel);

			// 
			// pnTotal
			// 
			this.pnTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnTotal.Controls.Add(this.pnRight);
			this.pnTotal.Controls.Add(this.pnLeft);
			this.pnTotal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnTotal.Location = new System.Drawing.Point(0, 0);
			this.pnTotal.Name = "pnTotal";
			this.pnTotal.Size = new System.Drawing.Size(620, 514);
			this.pnTotal.TabIndex = 0;
			// 
			// pnRight
			// 
			this.pnRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnRight.Controls.Add(this.pnMain);
			this.pnRight.Controls.Add(this.pnController);
			this.pnRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnRight.Location = new System.Drawing.Point(151, 0);
			this.pnRight.Name = "pnRight";
			this.pnRight.Size = new System.Drawing.Size(465, 510);
			this.pnRight.TabIndex = 1;
			// 
			// pnMain
			// 
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(463, 468);
			this.pnMain.TabIndex = 1;
			// 
			// pnController
			// 
			this.pnController.BackColor = System.Drawing.Color.Gainsboro;
			this.pnController.Controls.Add(this.bStart);
			this.pnController.Controls.Add(this.bBack);
			this.pnController.Controls.Add(this.bNext);
			this.pnController.Controls.Add(this.bStop);
			this.pnController.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnController.Location = new System.Drawing.Point(0, 468);
			this.pnController.Name = "pnController";
			this.pnController.Size = new System.Drawing.Size(463, 40);
			this.pnController.TabIndex = 0;
			// 
			// bStart
			// 
			this.bStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bStart.Location = new System.Drawing.Point(302, 7);
			this.bStart.Name = "bStart";
			this.bStart.Size = new System.Drawing.Size(75, 23);
			this.bStart.TabIndex = 1;
			this.bStart.Text = "Start";
			this.bStart.UseVisualStyleBackColor = true;
			this.bStart.Click += new System.EventHandler(this.bStart_Click);
			// 
			// bBack
			// 
			this.bBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bBack.Location = new System.Drawing.Point(15, 7);
			this.bBack.Name = "bBack";
			this.bBack.Size = new System.Drawing.Size(75, 23);
			this.bBack.TabIndex = 0;
			this.bBack.Text = "Back";
			this.bBack.UseVisualStyleBackColor = true;
			this.bBack.Click += new System.EventHandler(this.bBack_Click);
			// 
			// bNext
			// 
			this.bNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bNext.Location = new System.Drawing.Point(91, 7);
			this.bNext.Name = "bNext";
			this.bNext.Size = new System.Drawing.Size(75, 23);
			this.bNext.TabIndex = 0;
			this.bNext.Text = "Next";
			this.bNext.UseVisualStyleBackColor = true;
			this.bNext.Click += new System.EventHandler(this.bNext_Click);
			// 
			// bStop
			// 
			this.bStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bStop.Location = new System.Drawing.Point(379, 7);
			this.bStop.Name = "bStop";
			this.bStop.Size = new System.Drawing.Size(75, 23);
			this.bStop.TabIndex = 0;
			this.bStop.Text = "Stop";
			this.bStop.UseVisualStyleBackColor = true;
			this.bStop.Click += new System.EventHandler(this.bStop_Click);
			// 
			// pnLeft
			// 
			this.pnLeft.BackColor = System.Drawing.Color.Silver;
			this.pnLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnLeft.Location = new System.Drawing.Point(0, 0);
			this.pnLeft.Name = "pnLeft";
			this.pnLeft.Size = new System.Drawing.Size(151, 510);
			this.pnLeft.TabIndex = 0;
			// 
			// WizardForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(620, 514);
			this.Controls.Add(this.pnTotal);
			this.Name = "WizardForm";
			this.Text = "WizardForm";
			this.pnTotal.ResumeLayout(false);
			this.pnRight.ResumeLayout(false);
			this.pnController.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnTotal;
		private System.Windows.Forms.Panel pnRight;
		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Panel pnController;
		private System.Windows.Forms.Panel pnLeft;
        public Controls.Wizard.StepsPanel StepPanel;
		private System.Windows.Forms.Button bStop;
		private System.Windows.Forms.Button bNext;
		private System.Windows.Forms.Button bBack;
		private System.Windows.Forms.Button bStart;

	}
}