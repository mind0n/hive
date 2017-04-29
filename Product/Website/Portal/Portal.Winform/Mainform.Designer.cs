namespace Portal.Winform
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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tbTest1 = new System.Windows.Forms.TabPage();
            this.tbTest2 = new System.Windows.Forms.TabPage();
            this.testRightJoin1 = new Portal.Winform.TestRightJoin();
            this.testLink1 = new Portal.Winform.TestLink();
            this.tabs.SuspendLayout();
            this.tbTest1.SuspendLayout();
            this.tbTest2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tbTest1);
            this.tabs.Controls.Add(this.tbTest2);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(479, 499);
            this.tabs.TabIndex = 0;
            // 
            // tbTest1
            // 
            this.tbTest1.Controls.Add(this.testRightJoin1);
            this.tbTest1.Location = new System.Drawing.Point(4, 22);
            this.tbTest1.Name = "tbTest1";
            this.tbTest1.Padding = new System.Windows.Forms.Padding(3);
            this.tbTest1.Size = new System.Drawing.Size(471, 473);
            this.tbTest1.TabIndex = 0;
            this.tbTest1.Text = "Test Right Join";
            this.tbTest1.UseVisualStyleBackColor = true;
            // 
            // tbTest2
            // 
            this.tbTest2.Controls.Add(this.testLink1);
            this.tbTest2.Location = new System.Drawing.Point(4, 22);
            this.tbTest2.Name = "tbTest2";
            this.tbTest2.Size = new System.Drawing.Size(471, 473);
            this.tbTest2.TabIndex = 1;
            this.tbTest2.Text = "Test Link";
            this.tbTest2.UseVisualStyleBackColor = true;
            // 
            // testRightJoin1
            // 
            this.testRightJoin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testRightJoin1.Location = new System.Drawing.Point(3, 3);
            this.testRightJoin1.Name = "testRightJoin1";
            this.testRightJoin1.Size = new System.Drawing.Size(465, 467);
            this.testRightJoin1.TabIndex = 0;
            // 
            // testLink1
            // 
            this.testLink1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testLink1.Location = new System.Drawing.Point(0, 0);
            this.testLink1.Name = "testLink1";
            this.testLink1.Size = new System.Drawing.Size(471, 473);
            this.testLink1.TabIndex = 0;
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 499);
            this.Controls.Add(this.tabs);
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabs.ResumeLayout(false);
            this.tbTest1.ResumeLayout(false);
            this.tbTest2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tbTest1;
        private TestRightJoin testRightJoin1;
        private System.Windows.Forms.TabPage tbTest2;
        private TestLink testLink1;
    }
}

