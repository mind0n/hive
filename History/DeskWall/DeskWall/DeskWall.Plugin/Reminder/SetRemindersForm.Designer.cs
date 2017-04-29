namespace Dw.Plugins
{
	partial class SetRemindersForm
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
			this.mcStart = new System.Windows.Forms.MonthCalendar();
			this.grpRepeat = new System.Windows.Forms.GroupBox();
			this.rbMinute = new System.Windows.Forms.RadioButton();
			this.txtCustomize = new System.Windows.Forms.TextBox();
			this.rdWorkdays = new System.Windows.Forms.RadioButton();
			this.rdTwoWeeks = new System.Windows.Forms.RadioButton();
			this.rdManually = new System.Windows.Forms.RadioButton();
			this.rdMonthly = new System.Windows.Forms.RadioButton();
			this.rdWeekly = new System.Windows.Forms.RadioButton();
			this.rdDaily = new System.Windows.Forms.RadioButton();
			this.grpPickDate = new System.Windows.Forms.GroupBox();
			this.chNoRepeat = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cbTime = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.grpSummary = new System.Windows.Forms.GroupBox();
			this.lbSumRepeat = new System.Windows.Forms.Label();
			this.lbSumDate = new System.Windows.Forms.Label();
			this.lbSumTime = new System.Windows.Forms.Label();
			this.hdRepeat = new System.Windows.Forms.TextBox();
			this.hdRepeats = new System.Windows.Forms.TextBox();
			this.btnRmvRmd = new System.Windows.Forms.Button();
			this.grpRepeat.SuspendLayout();
			this.grpPickDate.SuspendLayout();
			this.grpSummary.SuspendLayout();
			this.SuspendLayout();
			// 
			// mcStart
			// 
			this.mcStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mcStart.Location = new System.Drawing.Point(16, 75);
			this.mcStart.Name = "mcStart";
			this.mcStart.ShowToday = false;
			this.mcStart.TabIndex = 0;
			this.mcStart.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mcStart_DateChanged);
			// 
			// grpRepeat
			// 
			this.grpRepeat.Controls.Add(this.rbMinute);
			this.grpRepeat.Controls.Add(this.txtCustomize);
			this.grpRepeat.Controls.Add(this.rdWorkdays);
			this.grpRepeat.Controls.Add(this.rdTwoWeeks);
			this.grpRepeat.Controls.Add(this.rdManually);
			this.grpRepeat.Controls.Add(this.rdMonthly);
			this.grpRepeat.Controls.Add(this.rdWeekly);
			this.grpRepeat.Controls.Add(this.rdDaily);
			this.grpRepeat.Location = new System.Drawing.Point(230, 12);
			this.grpRepeat.Name = "grpRepeat";
			this.grpRepeat.Size = new System.Drawing.Size(237, 150);
			this.grpRepeat.TabIndex = 8;
			this.grpRepeat.TabStop = false;
			this.grpRepeat.Text = "Repeat";
			// 
			// rbMinute
			// 
			this.rbMinute.AutoSize = true;
			this.rbMinute.Location = new System.Drawing.Point(109, 65);
			this.rbMinute.Name = "rbMinute";
			this.rbMinute.Size = new System.Drawing.Size(87, 17);
			this.rbMinute.TabIndex = 8;
			this.rbMinute.TabStop = true;
			this.rbMinute.Tag = ">i60000;";
			this.rbMinute.Text = "Every Minute";
			this.rbMinute.UseVisualStyleBackColor = true;
			this.rbMinute.CheckedChanged += new System.EventHandler(this.rdGrpRepeat_CheckedChanged);
			// 
			// txtCustomize
			// 
			this.txtCustomize.Location = new System.Drawing.Point(16, 111);
			this.txtCustomize.Name = "txtCustomize";
			this.txtCustomize.Size = new System.Drawing.Size(180, 20);
			this.txtCustomize.TabIndex = 6;
			this.txtCustomize.TextChanged += new System.EventHandler(this.txtCustomize_TextChanged);
			// 
			// rdWorkdays
			// 
			this.rdWorkdays.AutoSize = true;
			this.rdWorkdays.Location = new System.Drawing.Point(109, 19);
			this.rdWorkdays.Name = "rdWorkdays";
			this.rdWorkdays.Size = new System.Drawing.Size(88, 17);
			this.rdWorkdays.TabIndex = 5;
			this.rdWorkdays.TabStop = true;
			this.rdWorkdays.Tag = ">d1;wSunday|wSaturday";
			this.rdWorkdays.Text = "on Workdays";
			this.rdWorkdays.UseVisualStyleBackColor = true;
			this.rdWorkdays.CheckedChanged += new System.EventHandler(this.rdGrpRepeat_CheckedChanged);
			// 
			// rdTwoWeeks
			// 
			this.rdTwoWeeks.AutoSize = true;
			this.rdTwoWeeks.Location = new System.Drawing.Point(109, 42);
			this.rdTwoWeeks.Name = "rdTwoWeeks";
			this.rdTwoWeeks.Size = new System.Drawing.Size(112, 17);
			this.rdTwoWeeks.TabIndex = 4;
			this.rdTwoWeeks.TabStop = true;
			this.rdTwoWeeks.Tag = ">d14;";
			this.rdTwoWeeks.Text = "every Two Weeks";
			this.rdTwoWeeks.UseVisualStyleBackColor = true;
			this.rdTwoWeeks.CheckedChanged += new System.EventHandler(this.rdGrpRepeat_CheckedChanged);
			// 
			// rdManually
			// 
			this.rdManually.AutoSize = true;
			this.rdManually.Location = new System.Drawing.Point(16, 88);
			this.rdManually.Name = "rdManually";
			this.rdManually.Size = new System.Drawing.Size(67, 17);
			this.rdManually.TabIndex = 3;
			this.rdManually.TabStop = true;
			this.rdManually.Tag = ";";
			this.rdManually.Text = "Manually";
			this.rdManually.UseVisualStyleBackColor = true;
			this.rdManually.CheckedChanged += new System.EventHandler(this.rdGrpRepeat_CheckedChanged);
			// 
			// rdMonthly
			// 
			this.rdMonthly.AutoSize = true;
			this.rdMonthly.Location = new System.Drawing.Point(16, 65);
			this.rdMonthly.Name = "rdMonthly";
			this.rdMonthly.Size = new System.Drawing.Size(62, 17);
			this.rdMonthly.TabIndex = 2;
			this.rdMonthly.TabStop = true;
			this.rdMonthly.Tag = "m;";
			this.rdMonthly.Text = "Monthly";
			this.rdMonthly.UseVisualStyleBackColor = true;
			this.rdMonthly.CheckedChanged += new System.EventHandler(this.rdGrpRepeat_CheckedChanged);
			// 
			// rdWeekly
			// 
			this.rdWeekly.AutoSize = true;
			this.rdWeekly.Location = new System.Drawing.Point(16, 42);
			this.rdWeekly.Name = "rdWeekly";
			this.rdWeekly.Size = new System.Drawing.Size(61, 17);
			this.rdWeekly.TabIndex = 1;
			this.rdWeekly.TabStop = true;
			this.rdWeekly.Tag = ">d7;";
			this.rdWeekly.Text = "Weekly";
			this.rdWeekly.UseVisualStyleBackColor = true;
			this.rdWeekly.CheckedChanged += new System.EventHandler(this.rdGrpRepeat_CheckedChanged);
			// 
			// rdDaily
			// 
			this.rdDaily.AutoSize = true;
			this.rdDaily.Checked = true;
			this.rdDaily.Location = new System.Drawing.Point(16, 19);
			this.rdDaily.Name = "rdDaily";
			this.rdDaily.Size = new System.Drawing.Size(48, 17);
			this.rdDaily.TabIndex = 0;
			this.rdDaily.TabStop = true;
			this.rdDaily.Tag = ">d1;";
			this.rdDaily.Text = "Daily";
			this.rdDaily.UseVisualStyleBackColor = true;
			this.rdDaily.CheckedChanged += new System.EventHandler(this.rdGrpRepeat_CheckedChanged);
			// 
			// grpPickDate
			// 
			this.grpPickDate.Controls.Add(this.label2);
			this.grpPickDate.Controls.Add(this.cbTime);
			this.grpPickDate.Controls.Add(this.label1);
			this.grpPickDate.Controls.Add(this.mcStart);
			this.grpPickDate.Location = new System.Drawing.Point(12, 12);
			this.grpPickDate.Name = "grpPickDate";
			this.grpPickDate.Size = new System.Drawing.Size(212, 249);
			this.grpPickDate.TabIndex = 10;
			this.grpPickDate.TabStop = false;
			this.grpPickDate.Text = "Remind Date";
			// 
			// chNoRepeat
			// 
			this.chNoRepeat.AutoSize = true;
			this.chNoRepeat.Location = new System.Drawing.Point(373, 154);
			this.chNoRepeat.Name = "chNoRepeat";
			this.chNoRepeat.Size = new System.Drawing.Size(78, 17);
			this.chNoRepeat.TabIndex = 11;
			this.chNoRepeat.Text = "No Repeat";
			this.chNoRepeat.UseVisualStyleBackColor = true;
			this.chNoRepeat.CheckedChanged += new System.EventHandler(this.chNoRepeat_CheckedChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Start Date:";
			// 
			// cbTime
			// 
			this.cbTime.BackColor = System.Drawing.SystemColors.Info;
			this.cbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cbTime.FormattingEnabled = true;
			this.cbTime.Location = new System.Drawing.Point(77, 25);
			this.cbTime.Name = "cbTime";
			this.cbTime.Size = new System.Drawing.Size(117, 21);
			this.cbTime.TabIndex = 3;
			this.cbTime.SelectedIndexChanged += new System.EventHandler(this.cbTime_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Start Time:";
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(233, 238);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 11;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(314, 238);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// grpSummary
			// 
			this.grpSummary.Controls.Add(this.lbSumRepeat);
			this.grpSummary.Controls.Add(this.lbSumDate);
			this.grpSummary.Controls.Add(this.lbSumTime);
			this.grpSummary.Location = new System.Drawing.Point(161, 307);
			this.grpSummary.Name = "grpSummary";
			this.grpSummary.Size = new System.Drawing.Size(211, 93);
			this.grpSummary.TabIndex = 13;
			this.grpSummary.TabStop = false;
			this.grpSummary.Text = "Summary";
			this.grpSummary.Visible = false;
			// 
			// lbSumRepeat
			// 
			this.lbSumRepeat.AutoSize = true;
			this.lbSumRepeat.Location = new System.Drawing.Point(13, 60);
			this.lbSumRepeat.Name = "lbSumRepeat";
			this.lbSumRepeat.Size = new System.Drawing.Size(82, 13);
			this.lbSumRepeat.TabIndex = 2;
			this.lbSumRepeat.Text = "and {REPEAT}.";
			// 
			// lbSumDate
			// 
			this.lbSumDate.AutoSize = true;
			this.lbSumDate.Location = new System.Drawing.Point(13, 40);
			this.lbSumDate.Name = "lbSumDate";
			this.lbSumDate.Size = new System.Drawing.Size(104, 13);
			this.lbSumDate.TabIndex = 1;
			this.lbSumDate.Text = "on {START_DATE} ";
			// 
			// lbSumTime
			// 
			this.lbSumTime.AutoSize = true;
			this.lbSumTime.Location = new System.Drawing.Point(13, 21);
			this.lbSumTime.Name = "lbSumTime";
			this.lbSumTime.Size = new System.Drawing.Size(183, 13);
			this.lbSumTime.TabIndex = 0;
			this.lbSumTime.Text = "Reminder will start at {START_TIME}";
			// 
			// hdRepeat
			// 
			this.hdRepeat.Enabled = false;
			this.hdRepeat.Location = new System.Drawing.Point(283, 281);
			this.hdRepeat.Name = "hdRepeat";
			this.hdRepeat.Size = new System.Drawing.Size(42, 20);
			this.hdRepeat.TabIndex = 14;
			this.hdRepeat.Visible = false;
			// 
			// hdRepeats
			// 
			this.hdRepeats.Location = new System.Drawing.Point(161, 281);
			this.hdRepeats.Name = "hdRepeats";
			this.hdRepeats.Size = new System.Drawing.Size(120, 20);
			this.hdRepeats.TabIndex = 15;
			this.hdRepeats.Visible = false;
			// 
			// btnRmvRmd
			// 
			this.btnRmvRmd.Location = new System.Drawing.Point(395, 238);
			this.btnRmvRmd.Name = "btnRmvRmd";
			this.btnRmvRmd.Size = new System.Drawing.Size(75, 23);
			this.btnRmvRmd.TabIndex = 16;
			this.btnRmvRmd.Text = "Remove";
			this.btnRmvRmd.UseVisualStyleBackColor = true;
			this.btnRmvRmd.Click += new System.EventHandler(this.btnRmvRmd_Click);
			// 
			// SetRemindersForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(481, 270);
			this.Controls.Add(this.chNoRepeat);
			this.Controls.Add(this.btnRmvRmd);
			this.Controls.Add(this.hdRepeats);
			this.Controls.Add(this.hdRepeat);
			this.Controls.Add(this.grpSummary);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.grpPickDate);
			this.Controls.Add(this.grpRepeat);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SetRemindersForm";
			this.Text = "Set Reminder";
			this.Load += new System.EventHandler(this.SetRemindersForm_Load);
			this.grpRepeat.ResumeLayout(false);
			this.grpRepeat.PerformLayout();
			this.grpPickDate.ResumeLayout(false);
			this.grpPickDate.PerformLayout();
			this.grpSummary.ResumeLayout(false);
			this.grpSummary.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MonthCalendar mcStart;
		private System.Windows.Forms.GroupBox grpRepeat;
		private System.Windows.Forms.RadioButton rdDaily;
		private System.Windows.Forms.RadioButton rdMonthly;
		private System.Windows.Forms.RadioButton rdWeekly;
		private System.Windows.Forms.RadioButton rdManually;
		private System.Windows.Forms.GroupBox grpPickDate;
		private System.Windows.Forms.ComboBox cbTime;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtCustomize;
		private System.Windows.Forms.RadioButton rdWorkdays;
		private System.Windows.Forms.RadioButton rdTwoWeeks;
		private System.Windows.Forms.CheckBox chNoRepeat;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox grpSummary;
		private System.Windows.Forms.Label lbSumTime;
		private System.Windows.Forms.Label lbSumDate;
		private System.Windows.Forms.Label lbSumRepeat;
		private System.Windows.Forms.TextBox hdRepeat;
		private System.Windows.Forms.TextBox hdRepeats;
		private System.Windows.Forms.RadioButton rbMinute;
		private System.Windows.Forms.Button btnRmvRmd;
	}
}