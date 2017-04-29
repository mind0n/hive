namespace ScriptPlatform.Screens
{
	partial class TextTreeScreen
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
			this.spLeft = new System.Windows.Forms.SplitContainer();
			this.txt = new System.Windows.Forms.TextBox();
			this.spRight = new System.Windows.Forms.SplitContainer();
			this.tv = new System.Windows.Forms.TreeView();
			this.spLeft.Panel1.SuspendLayout();
			this.spLeft.Panel2.SuspendLayout();
			this.spLeft.SuspendLayout();
			this.spRight.Panel2.SuspendLayout();
			this.spRight.SuspendLayout();
			this.SuspendLayout();
			// 
			// spLeft
			// 
			this.spLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spLeft.Location = new System.Drawing.Point(0, 0);
			this.spLeft.Name = "spLeft";
			// 
			// spLeft.Panel1
			// 
			this.spLeft.Panel1.Controls.Add(this.txt);
			// 
			// spLeft.Panel2
			// 
			this.spLeft.Panel2.Controls.Add(this.spRight);
			this.spLeft.Size = new System.Drawing.Size(758, 530);
			this.spLeft.SplitterDistance = 313;
			this.spLeft.TabIndex = 0;
			// 
			// txt
			// 
			this.txt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txt.Location = new System.Drawing.Point(12, 12);
			this.txt.Multiline = true;
			this.txt.Name = "txt";
			this.txt.Size = new System.Drawing.Size(298, 506);
			this.txt.TabIndex = 0;
			// 
			// spRight
			// 
			this.spRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spRight.Location = new System.Drawing.Point(0, 0);
			this.spRight.Name = "spRight";
			// 
			// spRight.Panel1
			// 
			this.spRight.Panel1.Padding = new System.Windows.Forms.Padding(0, 12, 0, 12);
			// 
			// spRight.Panel2
			// 
			this.spRight.Panel2.Controls.Add(this.tv);
			this.spRight.Size = new System.Drawing.Size(441, 530);
			this.spRight.SplitterDistance = 91;
			this.spRight.TabIndex = 0;
			// 
			// tv
			// 
			this.tv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tv.Location = new System.Drawing.Point(3, 12);
			this.tv.Name = "tv";
			this.tv.Size = new System.Drawing.Size(331, 506);
			this.tv.TabIndex = 0;
			// 
			// TextTreeScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(758, 530);
			this.Controls.Add(this.spLeft);
			this.Name = "TextTreeScreen";
			this.Text = "TextTreeScreen";
			this.spLeft.Panel1.ResumeLayout(false);
			this.spLeft.Panel1.PerformLayout();
			this.spLeft.Panel2.ResumeLayout(false);
			this.spLeft.ResumeLayout(false);
			this.spRight.Panel2.ResumeLayout(false);
			this.spRight.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer spLeft;
		private System.Windows.Forms.SplitContainer spRight;
		private System.Windows.Forms.TextBox txt;
		private System.Windows.Forms.TreeView tv;
	}
}