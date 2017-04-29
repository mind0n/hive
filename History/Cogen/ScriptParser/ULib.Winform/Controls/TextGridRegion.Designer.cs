namespace ULib.Winform.Controls
{
    partial class TextGridRegion
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
            this.sp = new System.Windows.Forms.SplitContainer();
            this.scriptBox = new System.Windows.Forms.TextBox();
            this.gridBox = new System.Windows.Forms.DataGridView();
            this.sp.Panel1.SuspendLayout();
            this.sp.Panel2.SuspendLayout();
            this.sp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBox)).BeginInit();
            this.SuspendLayout();
            // 
            // sp
            // 
            this.sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp.Location = new System.Drawing.Point(0, 0);
            this.sp.Name = "sp";
            // 
            // sp.Panel1
            // 
            this.sp.Panel1.Controls.Add(this.scriptBox);
            // 
            // sp.Panel2
            // 
            this.sp.Panel2.Controls.Add(this.gridBox);
            this.sp.Size = new System.Drawing.Size(429, 364);
            this.sp.SplitterDistance = 214;
            this.sp.TabIndex = 0;
            // 
            // scriptBox
            // 
            this.scriptBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptBox.Location = new System.Drawing.Point(0, 0);
            this.scriptBox.Multiline = true;
            this.scriptBox.Name = "scriptBox";
            this.scriptBox.Size = new System.Drawing.Size(214, 364);
            this.scriptBox.TabIndex = 0;
            // 
            // gridBox
            // 
            this.gridBox.AllowUserToAddRows = false;
            this.gridBox.AllowUserToDeleteRows = false;
            this.gridBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBox.Location = new System.Drawing.Point(0, 0);
            this.gridBox.Name = "gridBox";
            this.gridBox.ReadOnly = true;
            this.gridBox.Size = new System.Drawing.Size(211, 364);
            this.gridBox.TabIndex = 0;
            // 
            // TextGridRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sp);
            this.Name = "TextGridRegion";
            this.Size = new System.Drawing.Size(429, 364);
            this.sp.Panel1.ResumeLayout(false);
            this.sp.Panel1.PerformLayout();
            this.sp.Panel2.ResumeLayout(false);
            this.sp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer sp;
        private System.Windows.Forms.TextBox scriptBox;
        private System.Windows.Forms.DataGridView gridBox;

    }
}
