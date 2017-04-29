namespace Dw.Plugins
{
	partial class PowerMgrForm
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
			this.txtSeconds = new System.Windows.Forms.TextBox();
			this.btnCount = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtSeconds
			// 
			this.txtSeconds.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtSeconds.Location = new System.Drawing.Point(87, 6);
			this.txtSeconds.Name = "txtSeconds";
			this.txtSeconds.Size = new System.Drawing.Size(44, 20);
			this.txtSeconds.TabIndex = 0;
			this.txtSeconds.Text = "300";
			this.txtSeconds.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSeconds_KeyDown);
			// 
			// btnCount
			// 
			this.btnCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.btnCount.Location = new System.Drawing.Point(11, 39);
			this.btnCount.Name = "btnCount";
			this.btnCount.Size = new System.Drawing.Size(171, 50);
			this.btnCount.TabIndex = 1;
			this.btnCount.Text = "Count";
			this.btnCount.UseVisualStyleBackColor = true;
			this.btnCount.Click += new System.EventHandler(this.btnCount_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Suspend after";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(137, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "seconds";
			// 
			// PowerMgrForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(194, 100);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCount);
			this.Controls.Add(this.txtSeconds);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "PowerMgrForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Power Suspend";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtSeconds;
		private System.Windows.Forms.Button btnCount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}