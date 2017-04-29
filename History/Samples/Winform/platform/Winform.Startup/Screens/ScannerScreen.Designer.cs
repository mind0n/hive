namespace ScriptPlatform.Screens
{
	partial class ScannerScreen
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
			this.bScan = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// bScan
			// 
			this.bScan.Location = new System.Drawing.Point(12, 12);
			this.bScan.Name = "bScan";
			this.bScan.Size = new System.Drawing.Size(75, 23);
			this.bScan.TabIndex = 0;
			this.bScan.Text = "Scan";
			this.bScan.UseVisualStyleBackColor = true;
			this.bScan.Click += new System.EventHandler(this.bScan_Click);
			// 
			// ScannerScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(738, 581);
			this.Controls.Add(this.bScan);
			this.Name = "ScannerScreen";
			this.Text = "ScannerScreen";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button bScan;
	}
}