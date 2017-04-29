namespace Portal.Winform
{
    partial class TestLink
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
            this.udg = new Portal.Winform.UpDownGrid();
            this.SuspendLayout();
            // 
            // udg
            // 
            this.udg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udg.Location = new System.Drawing.Point(0, 0);
            this.udg.Name = "udg";
            this.udg.Size = new System.Drawing.Size(467, 532);
            this.udg.TabIndex = 0;
            // 
            // TestLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.udg);
            this.Name = "TestLink";
            this.Size = new System.Drawing.Size(467, 532);
            this.ResumeLayout(false);

        }

        #endregion

        private UpDownGrid udg;
    }
}
