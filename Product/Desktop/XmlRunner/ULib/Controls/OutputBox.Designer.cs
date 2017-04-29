namespace ULib.Controls
{
	partial class OutputBox
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pnCmd = new System.Windows.Forms.Panel();
            this.web = new System.Windows.Forms.WebBrowser();
            this.pn = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.bClear = new System.Windows.Forms.Button();
            this.pnCmd.SuspendLayout();
            this.pn.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnCmd
            // 
            this.pnCmd.Controls.Add(this.flowLayoutPanel1);
            this.pnCmd.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnCmd.Location = new System.Drawing.Point(0, 0);
            this.pnCmd.Name = "pnCmd";
            this.pnCmd.Size = new System.Drawing.Size(378, 28);
            this.pnCmd.TabIndex = 1;
            // 
            // web
            // 
            this.web.Dock = System.Windows.Forms.DockStyle.Fill;
            this.web.Location = new System.Drawing.Point(0, 0);
            this.web.MinimumSize = new System.Drawing.Size(20, 22);
            this.web.Name = "web";
            this.web.Size = new System.Drawing.Size(378, 412);
            this.web.TabIndex = 0;
            // 
            // pn
            // 
            this.pn.Controls.Add(this.web);
            this.pn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn.Location = new System.Drawing.Point(0, 28);
            this.pn.Name = "pn";
            this.pn.Size = new System.Drawing.Size(378, 412);
            this.pn.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.bClear);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(378, 28);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(3, 3);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(75, 23);
            this.bClear.TabIndex = 0;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // OutputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pn);
            this.Controls.Add(this.pnCmd);
            this.Name = "OutputBox";
            this.Size = new System.Drawing.Size(378, 440);
            this.pnCmd.ResumeLayout(false);
            this.pn.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel pnCmd;
        private System.Windows.Forms.WebBrowser web;
        private System.Windows.Forms.Panel pn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button bClear;

    }
}
