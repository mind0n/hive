namespace Utility.Startup.Forms
{
    partial class ParserForm
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
            this.sp = new System.Windows.Forms.SplitContainer();
            this.bParse = new System.Windows.Forms.Button();
            this.txtGrid = new ULib.Winform.Controls.TextGridRegion();
            this.sp.Panel1.SuspendLayout();
            this.sp.Panel2.SuspendLayout();
            this.sp.SuspendLayout();
            this.SuspendLayout();
            // 
            // sp
            // 
            this.sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sp.IsSplitterFixed = true;
            this.sp.Location = new System.Drawing.Point(0, 0);
            this.sp.Name = "sp";
            this.sp.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sp.Panel1
            // 
            this.sp.Panel1.Controls.Add(this.bParse);
            // 
            // sp.Panel2
            // 
            this.sp.Panel2.Controls.Add(this.txtGrid);
            this.sp.Size = new System.Drawing.Size(300, 300);
            this.sp.SplitterDistance = 30;
            this.sp.TabIndex = 0;
            // 
            // bParse
            // 
            this.bParse.Location = new System.Drawing.Point(3, 3);
            this.bParse.Name = "bParse";
            this.bParse.Size = new System.Drawing.Size(75, 23);
            this.bParse.TabIndex = 0;
            this.bParse.Text = "Parse";
            this.bParse.UseVisualStyleBackColor = true;
            this.bParse.Click += new System.EventHandler(this.bParse_Click);
            // 
            // txtGrid
            // 
            this.txtGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGrid.Location = new System.Drawing.Point(0, 0);
            this.txtGrid.Name = "txtGrid";
            this.txtGrid.Size = new System.Drawing.Size(300, 266);
            this.txtGrid.TabIndex = 0;
            // 
            // ParserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.sp);
            this.Name = "ParserForm";
            this.Text = "ParserForm";
            this.sp.Panel1.ResumeLayout(false);
            this.sp.Panel2.ResumeLayout(false);
            this.sp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer sp;
        private System.Windows.Forms.Button bParse;
        private ULib.Winform.Controls.TextGridRegion txtGrid;

    }
}