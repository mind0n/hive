namespace Dw.Plugins
{
	partial class StickyForm
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
			this.components = new System.ComponentModel.Container();
			this.txtContent = new System.Windows.Forms.RichTextBox();
			this.TitleBar = new System.Windows.Forms.Panel();
			this.btnHideAll = new System.Windows.Forms.Button();
			this.btnDel = new System.Windows.Forms.Button();
			this.btnNew = new System.Windows.Forms.Button();
			this.cxText = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.reminderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TitleBar.SuspendLayout();
			this.cxText.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtContent
			// 
			this.txtContent.AcceptsTab = true;
			this.txtContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtContent.Location = new System.Drawing.Point(0, 23);
			this.txtContent.Name = "txtContent";
			this.txtContent.Size = new System.Drawing.Size(192, 63);
			this.txtContent.TabIndex = 0;
			this.txtContent.Text = "";
			this.txtContent.WordWrap = false;
			this.txtContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContent_KeyDown);
			this.txtContent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtContent_MouseDown);
			this.txtContent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtContent_KeyUp);
			this.txtContent.TextChanged += new System.EventHandler(this.txtContent_TextChanged);
			// 
			// TitleBar
			// 
			this.TitleBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.TitleBar.Controls.Add(this.btnHideAll);
			this.TitleBar.Controls.Add(this.btnDel);
			this.TitleBar.Controls.Add(this.btnNew);
			this.TitleBar.Location = new System.Drawing.Point(0, 1);
			this.TitleBar.Name = "TitleBar";
			this.TitleBar.Size = new System.Drawing.Size(192, 22);
			this.TitleBar.TabIndex = 1;
			this.TitleBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDoubleClick);
			// 
			// btnHideAll
			// 
			this.btnHideAll.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnHideAll.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnHideAll.Location = new System.Drawing.Point(170, 0);
			this.btnHideAll.Name = "btnHideAll";
			this.btnHideAll.Size = new System.Drawing.Size(22, 22);
			this.btnHideAll.TabIndex = 4;
			this.btnHideAll.Text = "_";
			this.btnHideAll.UseVisualStyleBackColor = true;
			this.btnHideAll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnHideAll_MouseDown);
			// 
			// btnDel
			// 
			this.btnDel.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnDel.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnDel.Location = new System.Drawing.Point(22, 0);
			this.btnDel.Name = "btnDel";
			this.btnDel.Size = new System.Drawing.Size(22, 22);
			this.btnDel.TabIndex = 3;
			this.btnDel.Text = "X";
			this.btnDel.UseVisualStyleBackColor = true;
			this.btnDel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDel_MouseDown);
			// 
			// btnNew
			// 
			this.btnNew.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnNew.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnNew.Location = new System.Drawing.Point(0, 0);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(22, 22);
			this.btnNew.TabIndex = 0;
			this.btnNew.Text = "+";
			this.btnNew.UseVisualStyleBackColor = true;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// cxText
			// 
			this.cxText.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.reminderToolStripMenuItem});
			this.cxText.Name = "cxText";
			this.cxText.Size = new System.Drawing.Size(120, 98);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.cutToolStripMenuItem.Text = "Cut";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(116, 6);
			// 
			// reminderToolStripMenuItem
			// 
			this.reminderToolStripMenuItem.Name = "reminderToolStripMenuItem";
			this.reminderToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.reminderToolStripMenuItem.Text = "Reminder";
			this.reminderToolStripMenuItem.Click += new System.EventHandler(this.reminderToolStripMenuItem_Click);
			// 
			// StickyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(192, 86);
			this.Controls.Add(this.TitleBar);
			this.Controls.Add(this.txtContent);
			this.Name = "StickyForm";
			this.Text = "StickyForm";
			this.TitleBar.ResumeLayout(false);
			this.cxText.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox txtContent;
		private System.Windows.Forms.Panel TitleBar;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnDel;
		private System.Windows.Forms.Button btnHideAll;
		private System.Windows.Forms.ContextMenuStrip cxText;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem reminderToolStripMenuItem;
	}
}