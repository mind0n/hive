namespace ServiceClient
{
	partial class Mainform
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
			this.bCall = new System.Windows.Forms.Button();
			this.box = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// bCall
			// 
			this.bCall.Location = new System.Drawing.Point(12, 12);
			this.bCall.Name = "bCall";
			this.bCall.Size = new System.Drawing.Size(75, 23);
			this.bCall.TabIndex = 0;
			this.bCall.Text = "Call";
			this.bCall.UseVisualStyleBackColor = true;
			this.bCall.Click += new System.EventHandler(this.bCall_Click);
			// 
			// box
			// 
			this.box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.box.Location = new System.Drawing.Point(93, 12);
			this.box.Multiline = true;
			this.box.Name = "box";
			this.box.Size = new System.Drawing.Size(227, 224);
			this.box.TabIndex = 1;
			// 
			// Mainform
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(332, 248);
			this.Controls.Add(this.box);
			this.Controls.Add(this.bCall);
			this.Name = "Mainform";
			this.Text = "Client";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bCall;
		private System.Windows.Forms.TextBox box;
	}
}

