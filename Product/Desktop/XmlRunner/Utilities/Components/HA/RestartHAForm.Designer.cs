namespace Utilities.Components.HA
{
    partial class RestartHAForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestartHAForm));
            this.tabs = new System.Windows.Forms.TabControl();
            this.tbPlan = new System.Windows.Forms.TabPage();
            this.pnPlan = new System.Windows.Forms.Panel();
            this.spTool = new System.Windows.Forms.SplitContainer();
            this.toolPlan = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.spPlan = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bRunCurt = new System.Windows.Forms.Button();
            this.bRemoveAll = new System.Windows.Forms.Button();
            this.bRunAll = new System.Windows.Forms.Button();
            this.bRemove = new System.Windows.Forms.Button();
            this.tvPlan = new ULib.Controls.ActionTreeView();
            this.lbCommand = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.flowPlan = new System.Windows.Forms.FlowLayoutPanel();
            this.bChangeServer = new System.Windows.Forms.Button();
            this.bExecuteCommand = new System.Windows.Forms.Button();
            this.bWaitStart = new System.Windows.Forms.Button();
            this.bWaitTime = new System.Windows.Forms.Button();
            this.bRepeatCommand = new System.Windows.Forms.Button();
            this.bServiceStatus = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TabPage();
            this.bStopExec = new System.Windows.Forms.Button();
            this.bClearOutput = new System.Windows.Forms.Button();
            this.obox = new ULib.Controls.OutputBox();
            this.openPlanDlg = new System.Windows.Forms.OpenFileDialog();
            this.savePlanDlg = new System.Windows.Forms.SaveFileDialog();
            this.bNLB = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.tbPlan.SuspendLayout();
            this.pnPlan.SuspendLayout();
            this.spTool.Panel1.SuspendLayout();
            this.spTool.Panel2.SuspendLayout();
            this.spTool.SuspendLayout();
            this.toolPlan.SuspendLayout();
            this.spPlan.Panel1.SuspendLayout();
            this.spPlan.Panel2.SuspendLayout();
            this.spPlan.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowPlan.SuspendLayout();
            this.tbOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tbPlan);
            this.tabs.Controls.Add(this.tbOutput);
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(713, 521);
            this.tabs.TabIndex = 5;
            // 
            // tbPlan
            // 
            this.tbPlan.Controls.Add(this.pnPlan);
            this.tbPlan.Location = new System.Drawing.Point(4, 22);
            this.tbPlan.Name = "tbPlan";
            this.tbPlan.Padding = new System.Windows.Forms.Padding(3);
            this.tbPlan.Size = new System.Drawing.Size(705, 495);
            this.tbPlan.TabIndex = 1;
            this.tbPlan.Text = "Plan";
            this.tbPlan.UseVisualStyleBackColor = true;
            // 
            // pnPlan
            // 
            this.pnPlan.Controls.Add(this.spTool);
            this.pnPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnPlan.Location = new System.Drawing.Point(3, 3);
            this.pnPlan.Name = "pnPlan";
            this.pnPlan.Size = new System.Drawing.Size(699, 489);
            this.pnPlan.TabIndex = 1;
            // 
            // spTool
            // 
            this.spTool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spTool.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spTool.IsSplitterFixed = true;
            this.spTool.Location = new System.Drawing.Point(0, 0);
            this.spTool.Name = "spTool";
            this.spTool.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spTool.Panel1
            // 
            this.spTool.Panel1.Controls.Add(this.toolPlan);
            // 
            // spTool.Panel2
            // 
            this.spTool.Panel2.Controls.Add(this.spPlan);
            this.spTool.Size = new System.Drawing.Size(699, 489);
            this.spTool.SplitterDistance = 25;
            this.spTool.TabIndex = 1;
            // 
            // toolPlan
            // 
            this.toolPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolPlan.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator1});
            this.toolPlan.Location = new System.Drawing.Point(0, 0);
            this.toolPlan.Name = "toolPlan";
            this.toolPlan.Size = new System.Drawing.Size(699, 25);
            this.toolPlan.TabIndex = 1;
            this.toolPlan.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.cutToolStripButton.Text = "C&ut";
            this.cutToolStripButton.Click += new System.EventHandler(this.cutToolStripButton_Click);
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton.Text = "&Copy";
            this.copyToolStripButton.Click += new System.EventHandler(this.copyToolStripButton_Click);
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
            this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.pasteToolStripButton.Text = "&Paste";
            this.pasteToolStripButton.Click += new System.EventHandler(this.pasteToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // spPlan
            // 
            this.spPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spPlan.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spPlan.Location = new System.Drawing.Point(0, 0);
            this.spPlan.Name = "spPlan";
            // 
            // spPlan.Panel1
            // 
            this.spPlan.Panel1.Controls.Add(this.panel1);
            this.spPlan.Panel1.Controls.Add(this.tvPlan);
            this.spPlan.Panel1.Controls.Add(this.lbCommand);
            // 
            // spPlan.Panel2
            // 
            this.spPlan.Panel2.Controls.Add(this.label3);
            this.spPlan.Panel2.Controls.Add(this.flowPlan);
            this.spPlan.Size = new System.Drawing.Size(699, 460);
            this.spPlan.SplitterDistance = 574;
            this.spPlan.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.bRunCurt);
            this.panel1.Controls.Add(this.bRemoveAll);
            this.panel1.Controls.Add(this.bRunAll);
            this.panel1.Controls.Add(this.bRemove);
            this.panel1.Location = new System.Drawing.Point(3, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(72, 438);
            this.panel1.TabIndex = 2;
            // 
            // bRunCurt
            // 
            this.bRunCurt.Dock = System.Windows.Forms.DockStyle.Top;
            this.bRunCurt.Location = new System.Drawing.Point(0, 20);
            this.bRunCurt.Name = "bRunCurt";
            this.bRunCurt.Size = new System.Drawing.Size(72, 20);
            this.bRunCurt.TabIndex = 1;
            this.bRunCurt.Text = "Run Current";
            this.bRunCurt.UseVisualStyleBackColor = true;
            this.bRunCurt.Click += new System.EventHandler(this.bRunCurt_Click);
            // 
            // bRemoveAll
            // 
            this.bRemoveAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bRemoveAll.Location = new System.Drawing.Point(0, 398);
            this.bRemoveAll.Name = "bRemoveAll";
            this.bRemoveAll.Size = new System.Drawing.Size(72, 20);
            this.bRemoveAll.TabIndex = 1;
            this.bRemoveAll.Text = "Remove All";
            this.bRemoveAll.UseVisualStyleBackColor = true;
            this.bRemoveAll.Click += new System.EventHandler(this.bRemoveAll_Click);
            // 
            // bRunAll
            // 
            this.bRunAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.bRunAll.Location = new System.Drawing.Point(0, 0);
            this.bRunAll.Name = "bRunAll";
            this.bRunAll.Size = new System.Drawing.Size(72, 20);
            this.bRunAll.TabIndex = 1;
            this.bRunAll.Text = "Run All";
            this.bRunAll.UseVisualStyleBackColor = true;
            this.bRunAll.Click += new System.EventHandler(this.bRunAll_Click);
            // 
            // bRemove
            // 
            this.bRemove.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bRemove.Location = new System.Drawing.Point(0, 418);
            this.bRemove.Name = "bRemove";
            this.bRemove.Size = new System.Drawing.Size(72, 20);
            this.bRemove.TabIndex = 1;
            this.bRemove.Text = "Remove Curt";
            this.bRemove.UseVisualStyleBackColor = true;
            this.bRemove.Click += new System.EventHandler(this.bRemove_Click);
            // 
            // tvPlan
            // 
            this.tvPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvPlan.Location = new System.Drawing.Point(84, 19);
            this.tvPlan.Name = "tvPlan";
            this.tvPlan.Size = new System.Drawing.Size(487, 438);
            this.tvPlan.TabIndex = 1;
            this.tvPlan.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPlan_AfterSelect);
            // 
            // lbCommand
            // 
            this.lbCommand.AutoSize = true;
            this.lbCommand.Location = new System.Drawing.Point(81, 3);
            this.lbCommand.Name = "lbCommand";
            this.lbCommand.Size = new System.Drawing.Size(64, 13);
            this.lbCommand.TabIndex = 0;
            this.lbCommand.Text = "Active Plan:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Command box";
            // 
            // flowPlan
            // 
            this.flowPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowPlan.BackColor = System.Drawing.SystemColors.ControlDark;
            this.flowPlan.Controls.Add(this.bChangeServer);
            this.flowPlan.Controls.Add(this.bExecuteCommand);
            this.flowPlan.Controls.Add(this.bWaitStart);
            this.flowPlan.Controls.Add(this.bWaitTime);
            this.flowPlan.Controls.Add(this.bRepeatCommand);
            this.flowPlan.Controls.Add(this.bServiceStatus);
            this.flowPlan.Controls.Add(this.bNLB);
            this.flowPlan.Location = new System.Drawing.Point(0, 19);
            this.flowPlan.Name = "flowPlan";
            this.flowPlan.Size = new System.Drawing.Size(121, 438);
            this.flowPlan.TabIndex = 0;
            // 
            // bChangeServer
            // 
            this.bChangeServer.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bChangeServer.Location = new System.Drawing.Point(3, 3);
            this.bChangeServer.Name = "bChangeServer";
            this.bChangeServer.Size = new System.Drawing.Size(54, 35);
            this.bChangeServer.TabIndex = 0;
            this.bChangeServer.Text = "Change Server";
            this.bChangeServer.UseVisualStyleBackColor = false;
            this.bChangeServer.Click += new System.EventHandler(this.bChangeServer_Click);
            // 
            // bExecuteCommand
            // 
            this.bExecuteCommand.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bExecuteCommand.Location = new System.Drawing.Point(63, 3);
            this.bExecuteCommand.Name = "bExecuteCommand";
            this.bExecuteCommand.Size = new System.Drawing.Size(54, 35);
            this.bExecuteCommand.TabIndex = 0;
            this.bExecuteCommand.Text = "Execute Cmd";
            this.bExecuteCommand.UseVisualStyleBackColor = false;
            this.bExecuteCommand.Click += new System.EventHandler(this.bExecuteCommand_Click);
            // 
            // bWaitStart
            // 
            this.bWaitStart.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bWaitStart.Location = new System.Drawing.Point(3, 44);
            this.bWaitStart.Name = "bWaitStart";
            this.bWaitStart.Size = new System.Drawing.Size(54, 35);
            this.bWaitStart.TabIndex = 0;
            this.bWaitStart.Text = "Port Status";
            this.bWaitStart.UseVisualStyleBackColor = false;
            this.bWaitStart.Click += new System.EventHandler(this.bWaitStart_Click);
            // 
            // bWaitTime
            // 
            this.bWaitTime.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bWaitTime.Location = new System.Drawing.Point(63, 44);
            this.bWaitTime.Name = "bWaitTime";
            this.bWaitTime.Size = new System.Drawing.Size(54, 35);
            this.bWaitTime.TabIndex = 0;
            this.bWaitTime.Text = "Wait Time";
            this.bWaitTime.UseVisualStyleBackColor = false;
            this.bWaitTime.Click += new System.EventHandler(this.bWaitTime_Click);
            // 
            // bRepeatCommand
            // 
            this.bRepeatCommand.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bRepeatCommand.Location = new System.Drawing.Point(3, 85);
            this.bRepeatCommand.Name = "bRepeatCommand";
            this.bRepeatCommand.Size = new System.Drawing.Size(54, 35);
            this.bRepeatCommand.TabIndex = 0;
            this.bRepeatCommand.Text = "Repeat";
            this.bRepeatCommand.UseVisualStyleBackColor = false;
            this.bRepeatCommand.Click += new System.EventHandler(this.bRepeatCommand_Click);
            // 
            // bServiceStatus
            // 
            this.bServiceStatus.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bServiceStatus.Location = new System.Drawing.Point(63, 85);
            this.bServiceStatus.Name = "bServiceStatus";
            this.bServiceStatus.Size = new System.Drawing.Size(54, 35);
            this.bServiceStatus.TabIndex = 0;
            this.bServiceStatus.Text = "Service Status";
            this.bServiceStatus.UseVisualStyleBackColor = false;
            this.bServiceStatus.Click += new System.EventHandler(this.bServiceStatus_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.BackColor = System.Drawing.Color.Gainsboro;
            this.tbOutput.Controls.Add(this.bStopExec);
            this.tbOutput.Controls.Add(this.bClearOutput);
            this.tbOutput.Controls.Add(this.obox);
            this.tbOutput.Location = new System.Drawing.Point(4, 22);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(705, 495);
            this.tbOutput.TabIndex = 2;
            this.tbOutput.Text = "Output";
            // 
            // bStopExec
            // 
            this.bStopExec.Location = new System.Drawing.Point(77, 1);
            this.bStopExec.Name = "bStopExec";
            this.bStopExec.Size = new System.Drawing.Size(75, 23);
            this.bStopExec.TabIndex = 2;
            this.bStopExec.Text = "Stop";
            this.bStopExec.UseVisualStyleBackColor = true;
            this.bStopExec.Click += new System.EventHandler(this.bStopExec_Click);
            // 
            // bClearOutput
            // 
            this.bClearOutput.Location = new System.Drawing.Point(1, 1);
            this.bClearOutput.Name = "bClearOutput";
            this.bClearOutput.Size = new System.Drawing.Size(75, 23);
            this.bClearOutput.TabIndex = 1;
            this.bClearOutput.Text = "Clear";
            this.bClearOutput.UseVisualStyleBackColor = true;
            this.bClearOutput.Click += new System.EventHandler(this.bClearOutput_Click);
            // 
            // obox
            // 
            this.obox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.obox.HideCmdBar = true;
            this.obox.Location = new System.Drawing.Point(0, 25);
            this.obox.Name = "obox";
            this.obox.Size = new System.Drawing.Size(705, 470);
            this.obox.TabIndex = 0;
            // 
            // openPlanDlg
            // 
            this.openPlanDlg.DefaultExt = "plan";
            this.openPlanDlg.Filter = "Plan file(*.plan)|*.plan|All files(*.*)|*.*";
            // 
            // savePlanDlg
            // 
            this.savePlanDlg.DefaultExt = "plan";
            this.savePlanDlg.Filter = "Plan file(*.plan)|*.plan|All files(*.*)|*.*";
            // 
            // bNLB
            // 
            this.bNLB.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bNLB.Location = new System.Drawing.Point(3, 126);
            this.bNLB.Name = "bNLB";
            this.bNLB.Size = new System.Drawing.Size(54, 35);
            this.bNLB.TabIndex = 0;
            this.bNLB.Text = "NLB Status";
            this.bNLB.UseVisualStyleBackColor = false;
            this.bNLB.Click += new System.EventHandler(this.bNLB_Click);
            // 
            // RestartHAForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(737, 545);
            this.Controls.Add(this.tabs);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "RestartHAForm";
            this.Text = "Rolling Restart";
            this.tabs.ResumeLayout(false);
            this.tbPlan.ResumeLayout(false);
            this.pnPlan.ResumeLayout(false);
            this.spTool.Panel1.ResumeLayout(false);
            this.spTool.Panel1.PerformLayout();
            this.spTool.Panel2.ResumeLayout(false);
            this.spTool.ResumeLayout(false);
            this.toolPlan.ResumeLayout(false);
            this.toolPlan.PerformLayout();
            this.spPlan.Panel1.ResumeLayout(false);
            this.spPlan.Panel1.PerformLayout();
            this.spPlan.Panel2.ResumeLayout(false);
            this.spPlan.Panel2.PerformLayout();
            this.spPlan.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.flowPlan.ResumeLayout(false);
            this.tbOutput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tbPlan;
        private System.Windows.Forms.Panel pnPlan;
        private System.Windows.Forms.SplitContainer spPlan;
        private System.Windows.Forms.Label lbCommand;
        private System.Windows.Forms.FlowLayoutPanel flowPlan;
        private System.Windows.Forms.Button bExecuteCommand;
        private System.Windows.Forms.Button bWaitStart;
		private System.Windows.Forms.Button bChangeServer;
        private ULib.Controls.ActionTreeView tvPlan;
        private System.Windows.Forms.Button bRunAll;
        private System.Windows.Forms.Button bRunCurt;
        private System.Windows.Forms.Button bRemoveAll;
        private System.Windows.Forms.Button bRemove;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolPlan;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer spTool;
		private System.Windows.Forms.TabPage tbOutput;
		private ULib.Controls.OutputBox obox;
		private System.Windows.Forms.Button bClearOutput;
		private System.Windows.Forms.Button bWaitTime;
		private System.Windows.Forms.OpenFileDialog openPlanDlg;
		private System.Windows.Forms.SaveFileDialog savePlanDlg;
		private System.Windows.Forms.Button bRepeatCommand;
		private System.Windows.Forms.Button bStopExec;
		private System.Windows.Forms.Button bServiceStatus;
        private System.Windows.Forms.Button bNLB;
    }
}