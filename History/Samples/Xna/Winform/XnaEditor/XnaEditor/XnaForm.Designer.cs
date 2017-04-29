namespace XnaEditor
{
	partial class XnaForm
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
			this.spTotal = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.spTotal)).BeginInit();
			this.spTotal.SuspendLayout();
			this.SuspendLayout();
			// 
			// spTotal
			// 
			this.spTotal.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.spTotal.Location = new System.Drawing.Point(12, 12);
			this.spTotal.Name = "spTotal";
			this.spTotal.Size = new System.Drawing.Size(562, 447);
			this.spTotal.SplitterDistance = 104;
			this.spTotal.TabIndex = 1;
			// 
			// XnaForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(586, 471);
			this.Controls.Add(this.spTotal);
			this.Name = "XnaForm";
			this.Text = "Xna Form";
			((System.ComponentModel.ISupportInitialize)(this.spTotal)).EndInit();
			this.spTotal.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer spTotal;
	}
}

