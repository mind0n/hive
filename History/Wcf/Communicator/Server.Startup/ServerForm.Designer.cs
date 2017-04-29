namespace Server.Startup
{
	partial class ServerForm
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
			this.thistory = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// thistory
			// 
			this.thistory.Location = new System.Drawing.Point(12, 12);
			this.thistory.Multiline = true;
			this.thistory.Name = "thistory";
			this.thistory.Size = new System.Drawing.Size(411, 413);
			this.thistory.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(435, 437);
			this.Controls.Add(this.thistory);
			this.Name = "Form1";
			this.Text = "Server";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox thistory;
	}
}

