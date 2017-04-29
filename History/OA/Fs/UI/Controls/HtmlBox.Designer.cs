namespace Fs.UI.Controls
{
	partial class HtmlBox
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
			this.pn = new System.Windows.Forms.Panel();
			this.htmlContent = new System.Windows.Forms.WebBrowser();
			this.pn.SuspendLayout();
			this.SuspendLayout();
			// 
			// pn
			// 
			this.pn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pn.Controls.Add(this.htmlContent);
			this.pn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pn.Location = new System.Drawing.Point(0, 0);
			this.pn.Name = "pn";
			this.pn.Size = new System.Drawing.Size(540, 473);
			this.pn.TabIndex = 0;
			// 
			// htmlContent
			// 
			this.htmlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.htmlContent.Location = new System.Drawing.Point(0, 0);
			this.htmlContent.MinimumSize = new System.Drawing.Size(20, 20);
			this.htmlContent.Name = "htmlContent";
			this.htmlContent.Size = new System.Drawing.Size(538, 471);
			this.htmlContent.TabIndex = 5;
			// 
			// HtmlBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pn);
			this.Name = "HtmlBox";
			this.Size = new System.Drawing.Size(540, 473);
			this.pn.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pn;
		private System.Windows.Forms.WebBrowser htmlContent;

	}
}
