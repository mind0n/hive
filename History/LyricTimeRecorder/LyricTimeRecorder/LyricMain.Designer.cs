namespace LyricTimeRecorder
{
	partial class LyricMain
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
            this.tResult = new System.Windows.Forms.TextBox();
            this.bRecord = new System.Windows.Forms.Button();
            this.lyrics = new System.Windows.Forms.ListBox();
            this.bReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tResult
            // 
            this.tResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tResult.Location = new System.Drawing.Point(421, 11);
            this.tResult.Multiline = true;
            this.tResult.Name = "tResult";
            this.tResult.Size = new System.Drawing.Size(326, 458);
            this.tResult.TabIndex = 0;
            // 
            // bRecord
            // 
            this.bRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bRecord.Location = new System.Drawing.Point(353, 11);
            this.bRecord.Name = "bRecord";
            this.bRecord.Size = new System.Drawing.Size(62, 365);
            this.bRecord.TabIndex = 1;
            this.bRecord.Text = "Record";
            this.bRecord.UseVisualStyleBackColor = true;
            this.bRecord.Click += new System.EventHandler(this.bRecord_Click);
            // 
            // lyrics
            // 
            this.lyrics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lyrics.FormattingEnabled = true;
            this.lyrics.ItemHeight = 12;
            this.lyrics.Location = new System.Drawing.Point(12, 11);
            this.lyrics.Name = "lyrics";
            this.lyrics.Size = new System.Drawing.Size(335, 460);
            this.lyrics.TabIndex = 2;
            this.lyrics.SelectedIndexChanged += new System.EventHandler(this.lyrics_SelectedIndexChanged);
            // 
            // bReset
            // 
            this.bReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bReset.Location = new System.Drawing.Point(353, 382);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(62, 86);
            this.bReset.TabIndex = 3;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // LyricMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 480);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.lyrics);
            this.Controls.Add(this.bRecord);
            this.Controls.Add(this.tResult);
            this.Name = "LyricMain";
            this.Text = "Lyric Recorder";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tResult;
		private System.Windows.Forms.Button bRecord;
		private System.Windows.Forms.ListBox lyrics;
        private System.Windows.Forms.Button bReset;
	}
}

