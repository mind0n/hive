namespace Eye.Startup
{
    partial class Eyeform
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
            this.pn = new System.Windows.Forms.Panel();
            this.bCapture = new System.Windows.Forms.Button();
            this.pnpreview = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pn
            // 
            this.pn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pn.Location = new System.Drawing.Point(106, 12);
            this.pn.Name = "pn";
            this.pn.Size = new System.Drawing.Size(637, 540);
            this.pn.TabIndex = 0;
            // 
            // bCapture
            // 
            this.bCapture.Location = new System.Drawing.Point(12, 12);
            this.bCapture.Name = "bCapture";
            this.bCapture.Size = new System.Drawing.Size(88, 24);
            this.bCapture.TabIndex = 1;
            this.bCapture.Text = "Capture";
            this.bCapture.UseVisualStyleBackColor = true;
            this.bCapture.Click += new System.EventHandler(this.bCapture_Click);
            // 
            // pnpreview
            // 
            this.pnpreview.Location = new System.Drawing.Point(12, 42);
            this.pnpreview.Name = "pnpreview";
            this.pnpreview.Size = new System.Drawing.Size(88, 61);
            this.pnpreview.TabIndex = 2;
            // 
            // Eyeform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 564);
            this.Controls.Add(this.pnpreview);
            this.Controls.Add(this.bCapture);
            this.Controls.Add(this.pn);
            this.Name = "Eyeform";
            this.Text = "Eye Recognision";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Eyeform_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Eyeform_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pn;
        private System.Windows.Forms.Button bCapture;
        private System.Windows.Forms.Panel pnpreview;
    }
}

