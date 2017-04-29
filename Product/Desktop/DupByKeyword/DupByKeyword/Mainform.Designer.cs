namespace DupByKeyword
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
			this.ta = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tb = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tt = new System.Windows.Forms.TextBox();
			this.tp = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.bs = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.tn = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// ta
			// 
			this.ta.Location = new System.Drawing.Point(89, 12);
			this.ta.Name = "ta";
			this.ta.Size = new System.Drawing.Size(100, 21);
			this.ta.TabIndex = 0;
			this.ta.Text = "[#]";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "关键字[1]：";
			// 
			// tb
			// 
			this.tb.Location = new System.Drawing.Point(281, 12);
			this.tb.Name = "tb";
			this.tb.Size = new System.Drawing.Size(100, 21);
			this.tb.TabIndex = 2;
			this.tb.Text = "[$]";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(204, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "关键字[2]：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 101);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 3;
			this.label3.Text = "模板：";
			// 
			// tt
			// 
			this.tt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tt.Location = new System.Drawing.Point(14, 116);
			this.tt.Multiline = true;
			this.tt.Name = "tt";
			this.tt.Size = new System.Drawing.Size(509, 222);
			this.tt.TabIndex = 4;
			this.tt.Text = "Append000[#] : Replace[$]\r\n";
			// 
			// tp
			// 
			this.tp.Location = new System.Drawing.Point(89, 39);
			this.tp.Name = "tp";
			this.tp.Size = new System.Drawing.Size(292, 21);
			this.tp.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 42);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 12);
			this.label4.TabIndex = 1;
			this.label4.Text = "文件名：";
			// 
			// bs
			// 
			this.bs.Location = new System.Drawing.Point(387, 12);
			this.bs.Name = "bs";
			this.bs.Size = new System.Drawing.Size(136, 48);
			this.bs.TabIndex = 5;
			this.bs.Text = "开始";
			this.bs.UseVisualStyleBackColor = true;
			this.bs.Click += new System.EventHandler(this.bStartClick);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 69);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 3;
			this.label5.Text = "数量：";
			// 
			// tn
			// 
			this.tn.Location = new System.Drawing.Point(89, 66);
			this.tn.Name = "tn";
			this.tn.Size = new System.Drawing.Size(100, 21);
			this.tn.TabIndex = 0;
			this.tn.Text = "10";
			// 
			// Mainform
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(535, 350);
			this.Controls.Add(this.bs);
			this.Controls.Add(this.tt);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tb);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tn);
			this.Controls.Add(this.tp);
			this.Controls.Add(this.ta);
			this.MinimumSize = new System.Drawing.Size(551, 388);
			this.Name = "Mainform";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Duplicator";
			this.Load += new System.EventHandler(this.Mainform_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox ta;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tb;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tt;
		private System.Windows.Forms.TextBox tp;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button bs;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tn;
	}
}

