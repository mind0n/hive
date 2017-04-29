using Fs.UI.Controls;
namespace OA
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.pnMiddle = new System.Windows.Forms.SplitContainer();
			this.pnLeftTop = new System.Windows.Forms.Panel();
			this.tvCaption = new System.Windows.Forms.Label();
			this.tvMain = new System.Windows.Forms.TreeView();
			this.tbMain = new Fs.UI.Controls.MClickCloseTabControl();
			this.tbStart = new Fs.UI.Controls.EventTabPage();
			this.cboView = new Fs.UI.Controls.EventCombo();
			this.label1 = new System.Windows.Forms.Label();
			this.lvMain = new Fs.UI.Controls.EventListView();
			this.mnMain = new System.Windows.Forms.MenuStrip();
			this.imgIcons = new System.Windows.Forms.ImageList(this.components);
			this.imgSmallIcons = new System.Windows.Forms.ImageList(this.components);
			this.pnTopContainer = new System.Windows.Forms.Panel();
			this.pnTop = new System.Windows.Forms.Panel();
			this.FormCaption = new System.Windows.Forms.Label();
			this.btnCloseForm = new System.Windows.Forms.Button();
			this.pnBottom = new System.Windows.Forms.Panel();
			this.pnMiddle.Panel1.SuspendLayout();
			this.pnMiddle.Panel2.SuspendLayout();
			this.pnMiddle.SuspendLayout();
			this.pnLeftTop.SuspendLayout();
			this.tbMain.SuspendLayout();
			this.tbStart.SuspendLayout();
			this.pnTopContainer.SuspendLayout();
			this.pnTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnMiddle
			// 
			this.pnMiddle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnMiddle.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.pnMiddle.IsSplitterFixed = true;
			this.pnMiddle.Location = new System.Drawing.Point(12, 60);
			this.pnMiddle.Name = "pnMiddle";
			// 
			// pnMiddle.Panel1
			// 
			this.pnMiddle.Panel1.Controls.Add(this.pnLeftTop);
			this.pnMiddle.Panel1.Controls.Add(this.tvMain);
			// 
			// pnMiddle.Panel2
			// 
			this.pnMiddle.Panel2.Controls.Add(this.tbMain);
			this.pnMiddle.Panel2.Padding = new System.Windows.Forms.Padding(0, 8, 4, 8);
			this.pnMiddle.Size = new System.Drawing.Size(820, 558);
			this.pnMiddle.SplitterDistance = 172;
			this.pnMiddle.SplitterWidth = 12;
			this.pnMiddle.TabIndex = 8;
			// 
			// pnLeftTop
			// 
			this.pnLeftTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnLeftTop.BackColor = System.Drawing.SystemColors.Info;
			this.pnLeftTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnLeftTop.BackgroundImage")));
			this.pnLeftTop.Controls.Add(this.tvCaption);
			this.pnLeftTop.Location = new System.Drawing.Point(3, 6);
			this.pnLeftTop.Name = "pnLeftTop";
			this.pnLeftTop.Size = new System.Drawing.Size(166, 24);
			this.pnLeftTop.TabIndex = 10;
			// 
			// tvCaption
			// 
			this.tvCaption.AutoSize = true;
			this.tvCaption.BackColor = System.Drawing.Color.White;
			this.tvCaption.Location = new System.Drawing.Point(5, 7);
			this.tvCaption.Name = "tvCaption";
			this.tvCaption.Size = new System.Drawing.Size(53, 13);
			this.tvCaption.TabIndex = 0;
			this.tvCaption.Text = "Favorites:";
			// 
			// tvMain
			// 
			this.tvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tvMain.Location = new System.Drawing.Point(3, 30);
			this.tvMain.Name = "tvMain";
			this.tvMain.PathSeparator = "/";
			this.tvMain.Size = new System.Drawing.Size(166, 528);
			this.tvMain.TabIndex = 9;
			// 
			// tbMain
			// 
			this.tbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbMain.Controls.Add(this.tbStart);
			this.tbMain.Location = new System.Drawing.Point(0, 6);
			this.tbMain.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.tbMain.Name = "tbMain";
			this.tbMain.SelectedIndex = 0;
			this.tbMain.Size = new System.Drawing.Size(636, 552);
			this.tbMain.TabIndex = 7;
			// 
			// tbStart
			// 
			this.tbStart.Controls.Add(this.cboView);
			this.tbStart.Controls.Add(this.label1);
			this.tbStart.Controls.Add(this.lvMain);
			this.tbStart.Location = new System.Drawing.Point(4, 22);
			this.tbStart.Name = "tbStart";
			this.tbStart.Size = new System.Drawing.Size(628, 526);
			this.tbStart.TabIndex = 0;
			this.tbStart.Text = "Start Page";
			this.tbStart.UseVisualStyleBackColor = true;
			// 
			// cboView
			// 
			this.cboView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboView.FormattingEnabled = true;
			this.cboView.Items.AddRange(new object[] {
            "Icons",
            "Details"});
			this.cboView.Location = new System.Drawing.Point(520, 5);
			this.cboView.Name = "cboView";
			this.cboView.Size = new System.Drawing.Size(92, 21);
			this.cboView.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Applications:";
			// 
			// lvMain
			// 
			this.lvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvMain.Location = new System.Drawing.Point(15, 32);
			this.lvMain.Name = "lvMain";
			this.lvMain.Size = new System.Drawing.Size(597, 453);
			this.lvMain.TabIndex = 0;
			this.lvMain.UseCompatibleStateImageBehavior = false;
			// 
			// mnMain
			// 
			this.mnMain.Location = new System.Drawing.Point(0, 0);
			this.mnMain.Name = "mnMain";
			this.mnMain.Size = new System.Drawing.Size(844, 24);
			this.mnMain.TabIndex = 9;
			this.mnMain.Text = "menuStrip1";
			this.mnMain.Visible = false;
			// 
			// imgIcons
			// 
			this.imgIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imgIcons.ImageSize = new System.Drawing.Size(48, 48);
			this.imgIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// imgSmallIcons
			// 
			this.imgSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgSmallIcons.ImageStream")));
			this.imgSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imgSmallIcons.Images.SetKeyName(0, "Icons.png");
			// 
			// pnTopContainer
			// 
			this.pnTopContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnTopContainer.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.pnTopContainer.Controls.Add(this.pnTop);
			this.pnTopContainer.Location = new System.Drawing.Point(6, 5);
			this.pnTopContainer.Name = "pnTopContainer";
			this.pnTopContainer.Size = new System.Drawing.Size(832, 49);
			this.pnTopContainer.TabIndex = 10;
			// 
			// pnTop
			// 
			this.pnTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnTop.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.pnTop.Controls.Add(this.FormCaption);
			this.pnTop.Controls.Add(this.btnCloseForm);
			this.pnTop.ForeColor = System.Drawing.Color.Black;
			this.pnTop.Location = new System.Drawing.Point(0, 1);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(832, 33);
			this.pnTop.TabIndex = 0;
			// 
			// FormCaption
			// 
			this.FormCaption.AutoSize = true;
			this.FormCaption.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.FormCaption.ForeColor = System.Drawing.SystemColors.GrayText;
			this.FormCaption.Location = new System.Drawing.Point(24, 11);
			this.FormCaption.Name = "FormCaption";
			this.FormCaption.Size = new System.Drawing.Size(154, 16);
			this.FormCaption.TabIndex = 1;
			this.FormCaption.Text = "Office Assistant 2011";
			// 
			// btnCloseForm
			// 
			this.btnCloseForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCloseForm.Location = new System.Drawing.Point(806, 0);
			this.btnCloseForm.Name = "btnCloseForm";
			this.btnCloseForm.Size = new System.Drawing.Size(26, 23);
			this.btnCloseForm.TabIndex = 0;
			this.btnCloseForm.Text = "X";
			this.btnCloseForm.UseVisualStyleBackColor = true;
			this.btnCloseForm.Click += new System.EventHandler(this.btnCloseForm_Click);
			// 
			// pnBottom
			// 
			this.pnBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnBottom.Location = new System.Drawing.Point(6, 615);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Size = new System.Drawing.Size(832, 31);
			this.pnBottom.TabIndex = 11;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(844, 651);
			this.Controls.Add(this.pnBottom);
			this.Controls.Add(this.pnMiddle);
			this.Controls.Add(this.pnTopContainer);
			this.Controls.Add(this.mnMain);
			this.MainMenuStrip = this.mnMain;
			this.Name = "MainForm";
			this.pnMiddle.Panel1.ResumeLayout(false);
			this.pnMiddle.Panel2.ResumeLayout(false);
			this.pnMiddle.ResumeLayout(false);
			this.pnLeftTop.ResumeLayout(false);
			this.pnLeftTop.PerformLayout();
			this.tbMain.ResumeLayout(false);
			this.tbStart.ResumeLayout(false);
			this.tbStart.PerformLayout();
			this.pnTopContainer.ResumeLayout(false);
			this.pnTop.ResumeLayout(false);
			this.pnTop.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer pnMiddle;
		private System.Windows.Forms.TreeView tvMain;
		private MClickCloseTabControl tbMain;
		private System.Windows.Forms.MenuStrip mnMain;
		private System.Windows.Forms.Panel pnLeftTop;
		private System.Windows.Forms.Label tvCaption;
		private EventTabPage tbStart;
		private System.Windows.Forms.Label label1;
		private EventListView lvMain;
		private System.Windows.Forms.ImageList imgIcons;
		private System.Windows.Forms.ImageList imgSmallIcons;
		private EventCombo cboView;
		private System.Windows.Forms.Panel pnTopContainer;
		private System.Windows.Forms.Panel pnTop;
		private System.Windows.Forms.Button btnCloseForm;
		private System.Windows.Forms.Label FormCaption;
		private System.Windows.Forms.Panel pnBottom;

	}
}

