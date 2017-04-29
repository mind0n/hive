namespace Utilities.Components.DBPartitioning
{
	partial class ExecuteForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tbCmd = new System.Windows.Forms.TabPage();
            this.tVersion = new System.Windows.Forms.TextBox();
            this.tabsStatus = new System.Windows.Forms.TabControl();
            this.tbStatus = new System.Windows.Forms.TabPage();
            this.errbox = new System.Windows.Forms.TextBox();
            this.tbParam = new System.Windows.Forms.TabPage();
            this.pnParam = new System.Windows.Forms.Panel();
            this.gDebug = new System.Windows.Forms.GroupBox();
            this.bRunCurt = new System.Windows.Forms.Button();
            this.bRunFrom = new System.Windows.Forms.Button();
            this.bSkip2Curt = new System.Windows.Forms.Button();
            this.bRun2Curt = new System.Windows.Forms.Button();
            this.lvCmd = new System.Windows.Forms.DataGridView();
            this.colIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbOutput = new System.Windows.Forms.TabPage();
            this.bViewLog = new System.Windows.Forms.Button();
            this.obox = new ULib.Controls.OutputBox();
            this.ckStopOnError = new System.Windows.Forms.CheckBox();
            this.tabs.SuspendLayout();
            this.tbCmd.SuspendLayout();
            this.tabsStatus.SuspendLayout();
            this.tbStatus.SuspendLayout();
            this.tbParam.SuspendLayout();
            this.gDebug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvCmd)).BeginInit();
            this.tbOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tbCmd);
            this.tabs.Controls.Add(this.tbOutput);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(764, 631);
            this.tabs.TabIndex = 0;
            // 
            // tbCmd
            // 
            this.tbCmd.Controls.Add(this.ckStopOnError);
            this.tbCmd.Controls.Add(this.tVersion);
            this.tbCmd.Controls.Add(this.tabsStatus);
            this.tbCmd.Controls.Add(this.gDebug);
            this.tbCmd.Controls.Add(this.lvCmd);
            this.tbCmd.Location = new System.Drawing.Point(4, 22);
            this.tbCmd.Name = "tbCmd";
            this.tbCmd.Padding = new System.Windows.Forms.Padding(3);
            this.tbCmd.Size = new System.Drawing.Size(756, 605);
            this.tbCmd.TabIndex = 0;
            this.tbCmd.Text = "Execute";
            this.tbCmd.UseVisualStyleBackColor = true;
            // 
            // tVersion
            // 
            this.tVersion.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.tVersion.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.tVersion.Location = new System.Drawing.Point(3, 3);
            this.tVersion.Multiline = true;
            this.tVersion.Name = "tVersion";
            this.tVersion.ReadOnly = true;
            this.tVersion.Size = new System.Drawing.Size(750, 21);
            this.tVersion.TabIndex = 5;
            // 
            // tabsStatus
            // 
            this.tabsStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabsStatus.Controls.Add(this.tbStatus);
            this.tabsStatus.Controls.Add(this.tbParam);
            this.tabsStatus.Location = new System.Drawing.Point(154, 478);
            this.tabsStatus.Name = "tabsStatus";
            this.tabsStatus.SelectedIndex = 0;
            this.tabsStatus.Size = new System.Drawing.Size(602, 127);
            this.tabsStatus.TabIndex = 4;
            // 
            // tbStatus
            // 
            this.tbStatus.Controls.Add(this.errbox);
            this.tbStatus.Location = new System.Drawing.Point(4, 22);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tbStatus.Size = new System.Drawing.Size(594, 101);
            this.tbStatus.TabIndex = 0;
            this.tbStatus.Text = "Status";
            this.tbStatus.UseVisualStyleBackColor = true;
            // 
            // errbox
            // 
            this.errbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errbox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errbox.Location = new System.Drawing.Point(3, 6);
            this.errbox.Multiline = true;
            this.errbox.Name = "errbox";
            this.errbox.ReadOnly = true;
            this.errbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.errbox.Size = new System.Drawing.Size(588, 97);
            this.errbox.TabIndex = 1;
            this.errbox.Text = "Command Execution Status";
            // 
            // tbParam
            // 
            this.tbParam.Controls.Add(this.pnParam);
            this.tbParam.Location = new System.Drawing.Point(4, 22);
            this.tbParam.Name = "tbParam";
            this.tbParam.Padding = new System.Windows.Forms.Padding(3);
            this.tbParam.Size = new System.Drawing.Size(594, 101);
            this.tbParam.TabIndex = 1;
            this.tbParam.Text = "Parameters";
            this.tbParam.UseVisualStyleBackColor = true;
            // 
            // pnParam
            // 
            this.pnParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnParam.Location = new System.Drawing.Point(3, 3);
            this.pnParam.Name = "pnParam";
            this.pnParam.Size = new System.Drawing.Size(588, 95);
            this.pnParam.TabIndex = 0;
            // 
            // gDebug
            // 
            this.gDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gDebug.Controls.Add(this.bRunCurt);
            this.gDebug.Controls.Add(this.bRunFrom);
            this.gDebug.Controls.Add(this.bSkip2Curt);
            this.gDebug.Controls.Add(this.bRun2Curt);
            this.gDebug.Location = new System.Drawing.Point(3, 478);
            this.gDebug.Name = "gDebug";
            this.gDebug.Size = new System.Drawing.Size(145, 123);
            this.gDebug.TabIndex = 3;
            this.gDebug.TabStop = false;
            this.gDebug.Text = "Debug";
            // 
            // bRunCurt
            // 
            this.bRunCurt.Location = new System.Drawing.Point(6, 18);
            this.bRunCurt.Name = "bRunCurt";
            this.bRunCurt.Size = new System.Drawing.Size(64, 45);
            this.bRunCurt.TabIndex = 2;
            this.bRunCurt.Text = "Run Current";
            this.bRunCurt.UseVisualStyleBackColor = true;
            this.bRunCurt.Click += new System.EventHandler(this.bRunCurt_Click);
            // 
            // bRunFrom
            // 
            this.bRunFrom.Location = new System.Drawing.Point(76, 71);
            this.bRunFrom.Name = "bRunFrom";
            this.bRunFrom.Size = new System.Drawing.Size(64, 45);
            this.bRunFrom.TabIndex = 2;
            this.bRunFrom.Text = "Run from Current";
            this.bRunFrom.UseVisualStyleBackColor = true;
            this.bRunFrom.Click += new System.EventHandler(this.bRunFrom_Click);
            // 
            // bSkip2Curt
            // 
            this.bSkip2Curt.Location = new System.Drawing.Point(76, 18);
            this.bSkip2Curt.Name = "bSkip2Curt";
            this.bSkip2Curt.Size = new System.Drawing.Size(64, 45);
            this.bSkip2Curt.TabIndex = 2;
            this.bSkip2Curt.Text = "Jump to Current";
            this.bSkip2Curt.UseVisualStyleBackColor = true;
            this.bSkip2Curt.Click += new System.EventHandler(this.bSkip2Curt_Click);
            // 
            // bRun2Curt
            // 
            this.bRun2Curt.Location = new System.Drawing.Point(6, 71);
            this.bRun2Curt.Name = "bRun2Curt";
            this.bRun2Curt.Size = new System.Drawing.Size(64, 45);
            this.bRun2Curt.TabIndex = 2;
            this.bRun2Curt.Text = "Run to Current";
            this.bRun2Curt.UseVisualStyleBackColor = true;
            this.bRun2Curt.Click += new System.EventHandler(this.bRun2Curt_Click);
            // 
            // lvCmd
            // 
            this.lvCmd.AllowUserToAddRows = false;
            this.lvCmd.AllowUserToDeleteRows = false;
            this.lvCmd.AllowUserToResizeRows = false;
            this.lvCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCmd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.lvCmd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvCmd.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lvCmd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.lvCmd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lvCmd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIndex,
            this.colName,
            this.colStatus});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.lvCmd.DefaultCellStyle = dataGridViewCellStyle6;
            this.lvCmd.Location = new System.Drawing.Point(3, 30);
            this.lvCmd.Name = "lvCmd";
            this.lvCmd.RowHeadersVisible = false;
            this.lvCmd.RowTemplate.Height = 23;
            this.lvCmd.Size = new System.Drawing.Size(750, 439);
            this.lvCmd.TabIndex = 0;
            this.lvCmd.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lvCmd_CellDoubleClick);
            this.lvCmd.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.lvCmd_CellPainting);
            this.lvCmd.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.lvCmd_CellValueChanged);
            this.lvCmd.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.lvCmd_CellValueNeeded);
            this.lvCmd.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.lvCmd_CellValuePushed);
            this.lvCmd.SelectionChanged += new System.EventHandler(this.lvCmd_SelectionChanged);
            this.lvCmd.BindingContextChanged += new System.EventHandler(this.lvCmd_BindingContextChanged);
            // 
            // colIndex
            // 
            this.colIndex.HeaderText = "Index";
            this.colIndex.Name = "colIndex";
            this.colIndex.ReadOnly = true;
            this.colIndex.Visible = false;
            // 
            // colName
            // 
            this.colName.HeaderText = "Actions";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // tbOutput
            // 
            this.tbOutput.Controls.Add(this.bViewLog);
            this.tbOutput.Controls.Add(this.obox);
            this.tbOutput.Location = new System.Drawing.Point(4, 22);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tbOutput.Size = new System.Drawing.Size(756, 605);
            this.tbOutput.TabIndex = 1;
            this.tbOutput.Text = "Output";
            this.tbOutput.UseVisualStyleBackColor = true;
            // 
            // bViewLog
            // 
            this.bViewLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bViewLog.Location = new System.Drawing.Point(6, 574);
            this.bViewLog.Name = "bViewLog";
            this.bViewLog.Size = new System.Drawing.Size(75, 23);
            this.bViewLog.TabIndex = 1;
            this.bViewLog.Text = "View Log";
            this.bViewLog.UseVisualStyleBackColor = true;
            this.bViewLog.Click += new System.EventHandler(this.bViewLog_Click);
            // 
            // obox
            // 
            this.obox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.obox.HideCmdBar = true;
            this.obox.Location = new System.Drawing.Point(3, 3);
            this.obox.Name = "obox";
            this.obox.Size = new System.Drawing.Size(750, 563);
            this.obox.TabIndex = 0;
            // 
            // ckErrStop
            // 
            this.ckStopOnError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckStopOnError.AutoSize = true;
            this.ckStopOnError.Checked = true;
            this.ckStopOnError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckStopOnError.Location = new System.Drawing.Point(658, 7);
            this.ckStopOnError.Name = "ckStopOnError";
            this.ckStopOnError.Size = new System.Drawing.Size(88, 17);
            this.ckStopOnError.TabIndex = 6;
            this.ckStopOnError.Text = "Stop on Error";
            this.ckStopOnError.UseVisualStyleBackColor = true;
            this.ckStopOnError.CheckedChanged += new System.EventHandler(this.ckErrStop_CheckedChanged);
            // 
            // ExecuteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 631);
            this.Controls.Add(this.tabs);
            this.Name = "ExecuteForm";
            this.Text = "ExecuteForm";
            this.tabs.ResumeLayout(false);
            this.tbCmd.ResumeLayout(false);
            this.tbCmd.PerformLayout();
            this.tabsStatus.ResumeLayout(false);
            this.tbStatus.ResumeLayout(false);
            this.tbStatus.PerformLayout();
            this.tbParam.ResumeLayout(false);
            this.gDebug.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lvCmd)).EndInit();
            this.tbOutput.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tbCmd;
		private System.Windows.Forms.TabPage tbOutput;
		private System.Windows.Forms.DataGridView lvCmd;
		private ULib.Controls.OutputBox obox;
		private System.Windows.Forms.DataGridViewTextBoxColumn colIndex;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
		private System.Windows.Forms.TextBox errbox;
		private System.Windows.Forms.Button bViewLog;
		private System.Windows.Forms.Button bRunCurt;
		private System.Windows.Forms.Button bRun2Curt;
		private System.Windows.Forms.Button bRunFrom;
		private System.Windows.Forms.Button bSkip2Curt;
		private System.Windows.Forms.GroupBox gDebug;
		private System.Windows.Forms.TabControl tabsStatus;
		private System.Windows.Forms.TabPage tbStatus;
		private System.Windows.Forms.TabPage tbParam;
		private System.Windows.Forms.Panel pnParam;
		private System.Windows.Forms.TextBox tVersion;
        private System.Windows.Forms.CheckBox ckStopOnError;
	}
}