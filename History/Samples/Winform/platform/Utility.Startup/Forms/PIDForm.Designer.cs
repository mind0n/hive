namespace Utility.Startup.Forms
{
	partial class PIDForm
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
			this.tpid = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.bSearch = new System.Windows.Forms.Button();
			this.box = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tpid
			// 
			this.tpid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tpid.Location = new System.Drawing.Point(46, 12);
			this.tpid.Name = "tpid";
			this.tpid.Size = new System.Drawing.Size(417, 20);
			this.tpid.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(28, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "PID:";
			// 
			// bSearch
			// 
			this.bSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bSearch.Location = new System.Drawing.Point(469, 10);
			this.bSearch.Name = "bSearch";
			this.bSearch.Size = new System.Drawing.Size(57, 23);
			this.bSearch.TabIndex = 2;
			this.bSearch.Text = "Search";
			this.bSearch.UseVisualStyleBackColor = true;
			this.bSearch.Click += new System.EventHandler(this.bSearch_Click);
			// 
			// box
			// 
			this.box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.box.Location = new System.Drawing.Point(15, 55);
			this.box.Multiline = true;
			this.box.Name = "box";
			this.box.ReadOnly = true;
			this.box.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.box.Size = new System.Drawing.Size(511, 360);
			this.box.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Path:";
			// 
			// PIDForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(538, 427);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.box);
			this.Controls.Add(this.bSearch);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tpid);
			this.Name = "PIDForm";
			this.Text = "PIDForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tpid;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button bSearch;
		private System.Windows.Forms.TextBox box;
		private System.Windows.Forms.Label label2;
	}
}