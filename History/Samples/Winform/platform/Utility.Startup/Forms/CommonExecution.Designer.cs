namespace Utility.Startup.Forms
{
    partial class CommonExecution
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.bReduceSqlMem = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.bReduceSqlMem);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(300, 300);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // bReduceSqlMem
            // 
            this.bReduceSqlMem.Location = new System.Drawing.Point(3, 3);
            this.bReduceSqlMem.Name = "bReduceSqlMem";
            this.bReduceSqlMem.Size = new System.Drawing.Size(107, 23);
            this.bReduceSqlMem.TabIndex = 0;
            this.bReduceSqlMem.Text = "Reduce SQL Mem";
            this.bReduceSqlMem.UseVisualStyleBackColor = true;
            this.bReduceSqlMem.Click += new System.EventHandler(this.bReduceSqlMem_Click);
            // 
            // CommonExecution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "CommonExecution";
            this.Text = "CommonExecution";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button bReduceSqlMem;

    }
}