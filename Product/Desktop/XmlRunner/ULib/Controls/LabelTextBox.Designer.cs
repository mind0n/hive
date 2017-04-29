namespace ULib.Controls
{
    partial class LabelTextBox
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
			this.tContent = new System.Windows.Forms.TextBox();
			this.tLabel = new System.Windows.Forms.Label();
			this.Splitter = new System.Windows.Forms.SplitContainer();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.SuspendLayout();
			// 
			// tContent
			// 
			this.tContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tContent.Location = new System.Drawing.Point(0, 0);
			this.tContent.Name = "tContent";
			this.tContent.Size = new System.Drawing.Size(673, 21);
			this.tContent.TabIndex = 0;
			// 
			// tLabel
			// 
			this.tLabel.AutoSize = true;
			this.tLabel.Location = new System.Drawing.Point(1, 3);
			this.tLabel.Name = "tLabel";
			this.tLabel.Size = new System.Drawing.Size(65, 12);
			this.tLabel.TabIndex = 1;
			this.tLabel.Text = "Label Text";
			// 
			// Splitter
			// 
			this.Splitter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.Splitter.Location = new System.Drawing.Point(0, 0);
			this.Splitter.Margin = new System.Windows.Forms.Padding(0);
			this.Splitter.Name = "Splitter";
			// 
			// Splitter.Panel1
			// 
			this.Splitter.Panel1.Controls.Add(this.tLabel);
			// 
			// Splitter.Panel2
			// 
			this.Splitter.Panel2.Controls.Add(this.tContent);
			this.Splitter.Size = new System.Drawing.Size(738, 23);
			this.Splitter.SplitterDistance = 61;
			this.Splitter.TabIndex = 2;
			// 
			// LabelTextBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Splitter);
			this.Name = "LabelTextBox";
			this.Size = new System.Drawing.Size(738, 23);
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel1.PerformLayout();
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.Panel2.PerformLayout();
			this.Splitter.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tContent;
        private System.Windows.Forms.Label tLabel;
        private System.Windows.Forms.SplitContainer Splitter;
    }
}
