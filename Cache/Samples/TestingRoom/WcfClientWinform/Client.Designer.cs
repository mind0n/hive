namespace WcfClientWinform
{
	partial class Client
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
            this.bTest = new System.Windows.Forms.Button();
            this.bError = new System.Windows.Forms.Button();
            this.bTp = new System.Windows.Forms.Button();
            this.box = new System.Windows.Forms.TextBox();
            this.bBasicHttp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bTest
            // 
            this.bTest.Location = new System.Drawing.Point(12, 13);
            this.bTest.Name = "bTest";
            this.bTest.Size = new System.Drawing.Size(75, 25);
            this.bTest.TabIndex = 0;
            this.bTest.Text = "Test";
            this.bTest.UseVisualStyleBackColor = true;
            this.bTest.Click += new System.EventHandler(this.bTest_Click);
            // 
            // bError
            // 
            this.bError.Location = new System.Drawing.Point(12, 44);
            this.bError.Name = "bError";
            this.bError.Size = new System.Drawing.Size(75, 25);
            this.bError.TabIndex = 1;
            this.bError.Text = "Fault";
            this.bError.UseVisualStyleBackColor = true;
            this.bError.Click += new System.EventHandler(this.bError_Click);
            // 
            // bTp
            // 
            this.bTp.Location = new System.Drawing.Point(12, 76);
            this.bTp.Name = "bTp";
            this.bTp.Size = new System.Drawing.Size(165, 25);
            this.bTp.TabIndex = 2;
            this.bTp.Text = "Transparent Proxy";
            this.bTp.UseVisualStyleBackColor = true;
            this.bTp.Click += new System.EventHandler(this.bTp_Click);
            // 
            // box
            // 
            this.box.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.box.Location = new System.Drawing.Point(0, 174);
            this.box.Multiline = true;
            this.box.Name = "box";
            this.box.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.box.Size = new System.Drawing.Size(596, 270);
            this.box.TabIndex = 3;
            this.box.WordWrap = false;
            // 
            // bBasicHttp
            // 
            this.bBasicHttp.Location = new System.Drawing.Point(12, 107);
            this.bBasicHttp.Name = "bBasicHttp";
            this.bBasicHttp.Size = new System.Drawing.Size(75, 23);
            this.bBasicHttp.TabIndex = 4;
            this.bBasicHttp.Text = "Basic Http";
            this.bBasicHttp.UseVisualStyleBackColor = true;
            this.bBasicHttp.Click += new System.EventHandler(this.bBasicHttp_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 444);
            this.Controls.Add(this.bBasicHttp);
            this.Controls.Add(this.box);
            this.Controls.Add(this.bTp);
            this.Controls.Add(this.bError);
            this.Controls.Add(this.bTest);
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wcf Client";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bTest;
		private System.Windows.Forms.Button bError;
		private System.Windows.Forms.Button bTp;
        private System.Windows.Forms.TextBox box;
        private System.Windows.Forms.Button bBasicHttp;
	}
}

