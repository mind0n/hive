namespace XnaEngine.Winform
{
	partial class XnaPanel
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
			this.panelViewport = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// panelViewport
			// 
			this.panelViewport.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelViewport.Location = new System.Drawing.Point(0, 0);
			this.panelViewport.Name = "panelViewport";
			this.panelViewport.Size = new System.Drawing.Size(310, 296);
			this.panelViewport.TabIndex = 0;
			// 
			// XnaPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelViewport);
			this.Name = "XnaPanel";
			this.Size = new System.Drawing.Size(310, 296);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelViewport;
	}
}
