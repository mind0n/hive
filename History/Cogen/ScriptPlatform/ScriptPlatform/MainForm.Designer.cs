namespace Platform
{
	partial class MainForm
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
			this.pnMain = new System.Windows.Forms.SplitContainer();
			this.treeMain = new Switcher();
			this.pnMain.Panel1.SuspendLayout();
			this.pnMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.pnMain.Location = new System.Drawing.Point(12, 12);
			this.pnMain.Name = "pnMain";
			// 
			// pnMain.Panel1
			// 
			this.pnMain.Panel1.Controls.Add(this.treeMain);
			this.pnMain.Size = new System.Drawing.Size(780, 491);
			this.pnMain.SplitterDistance = 129;
			this.pnMain.TabIndex = 0;
			// 
			// treeMain
			// 
			this.treeMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeMain.Location = new System.Drawing.Point(3, 3);
			this.treeMain.Name = "treeMain";
			this.treeMain.Size = new System.Drawing.Size(123, 485);
			this.treeMain.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(804, 515);
			this.Controls.Add(this.pnMain);
			this.Name = "MainForm";
			this.Text = "Platform";
			this.pnMain.Panel1.ResumeLayout(false);
			this.pnMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer pnMain;
		private Switcher treeMain;
	}
}

