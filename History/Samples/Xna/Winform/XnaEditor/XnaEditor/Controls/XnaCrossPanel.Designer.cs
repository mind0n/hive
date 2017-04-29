namespace XnaEditor.Controls
{
	partial class XnaCrossPanel
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
			this.spMain = new System.Windows.Forms.SplitContainer();
			this.spLeft = new System.Windows.Forms.SplitContainer();
			this.spRight = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.spMain)).BeginInit();
			this.spMain.Panel1.SuspendLayout();
			this.spMain.Panel2.SuspendLayout();
			this.spMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spLeft)).BeginInit();
			this.spLeft.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spRight)).BeginInit();
			this.spRight.SuspendLayout();
			this.SuspendLayout();
			// 
			// spMain
			// 
			this.spMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spMain.IsSplitterFixed = true;
			this.spMain.Location = new System.Drawing.Point(0, 0);
			this.spMain.Name = "spMain";
			// 
			// spMain.Panel1
			// 
			this.spMain.Panel1.Controls.Add(this.spLeft);
			this.spMain.Panel1MinSize = 50;
			// 
			// spMain.Panel2
			// 
			this.spMain.Panel2.Controls.Add(this.spRight);
			this.spMain.Panel2MinSize = 50;
			this.spMain.Size = new System.Drawing.Size(528, 490);
			this.spMain.SplitterDistance = 264;
			this.spMain.SplitterWidth = 1;
			this.spMain.TabIndex = 1;
			// 
			// spLeft
			// 
			this.spLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spLeft.IsSplitterFixed = true;
			this.spLeft.Location = new System.Drawing.Point(0, 0);
			this.spLeft.Name = "spLeft";
			this.spLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.spLeft.Size = new System.Drawing.Size(264, 490);
			this.spLeft.SplitterDistance = 244;
			this.spLeft.SplitterWidth = 1;
			this.spLeft.TabIndex = 0;
			// 
			// spRight
			// 
			this.spRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spRight.IsSplitterFixed = true;
			this.spRight.Location = new System.Drawing.Point(0, 0);
			this.spRight.Name = "spRight";
			this.spRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.spRight.Size = new System.Drawing.Size(263, 490);
			this.spRight.SplitterDistance = 244;
			this.spRight.SplitterWidth = 1;
			this.spRight.TabIndex = 1;
			// 
			// XnaCrossPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.spMain);
			this.Name = "XnaCrossPanel";
			this.Size = new System.Drawing.Size(528, 490);
			this.spMain.Panel1.ResumeLayout(false);
			this.spMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spMain)).EndInit();
			this.spMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spLeft)).EndInit();
			this.spLeft.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spRight)).EndInit();
			this.spRight.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer spMain;
		private System.Windows.Forms.SplitContainer spLeft;
		private System.Windows.Forms.SplitContainer spRight;
	}
}
