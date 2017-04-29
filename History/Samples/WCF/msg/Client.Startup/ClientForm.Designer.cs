namespace Client.Startup
{
	partial class ClientForm
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
			this.tchat = new System.Windows.Forms.TextBox();
			this.bSend = new System.Windows.Forms.Button();
			this.thistory = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// tchat
			// 
			this.tchat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tchat.Location = new System.Drawing.Point(12, 380);
			this.tchat.Multiline = true;
			this.tchat.Name = "tchat";
			this.tchat.Size = new System.Drawing.Size(350, 46);
			this.tchat.TabIndex = 0;
			this.tchat.Text = "Client Message";
			// 
			// bSend
			// 
			this.bSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bSend.Location = new System.Drawing.Point(378, 380);
			this.bSend.Name = "bSend";
			this.bSend.Size = new System.Drawing.Size(75, 46);
			this.bSend.TabIndex = 1;
			this.bSend.Text = "Send";
			this.bSend.UseVisualStyleBackColor = true;
			this.bSend.Click += new System.EventHandler(this.bSend_Click);
			// 
			// thistory
			// 
			this.thistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.thistory.Location = new System.Drawing.Point(12, 12);
			this.thistory.Multiline = true;
			this.thistory.Name = "thistory";
			this.thistory.Size = new System.Drawing.Size(441, 362);
			this.thistory.TabIndex = 2;
			// 
			// ClientForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(465, 438);
			this.Controls.Add(this.thistory);
			this.Controls.Add(this.bSend);
			this.Controls.Add(this.tchat);
			this.Name = "ClientForm";
			this.Text = "Client";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tchat;
		private System.Windows.Forms.Button bSend;
		private System.Windows.Forms.TextBox thistory;
	}
}

