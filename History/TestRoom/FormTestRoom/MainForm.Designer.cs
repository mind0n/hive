namespace FormTestRoom
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
			this.tMy = new System.Windows.Forms.TextBox();
			this.btnRun = new System.Windows.Forms.Button();
			this.tSys = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// tMy
			// 
			this.tMy.Location = new System.Drawing.Point(12, 12);
			this.tMy.Multiline = true;
			this.tMy.Name = "tMy";
			this.tMy.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tMy.Size = new System.Drawing.Size(325, 451);
			this.tMy.TabIndex = 0;
			// 
			// btnRun
			// 
			this.btnRun.Location = new System.Drawing.Point(12, 469);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(75, 23);
			this.btnRun.TabIndex = 1;
			this.btnRun.Text = "Run";
			this.btnRun.UseVisualStyleBackColor = true;
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// tSys
			// 
			this.tSys.Location = new System.Drawing.Point(343, 12);
			this.tSys.Multiline = true;
			this.tSys.Name = "tSys";
			this.tSys.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tSys.Size = new System.Drawing.Size(316, 451);
			this.tSys.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(671, 504);
			this.Controls.Add(this.tSys);
			this.Controls.Add(this.btnRun);
			this.Controls.Add(this.tMy);
			this.Name = "MainForm";
			this.Text = "Main";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tMy;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.TextBox tSys;
	}
}

