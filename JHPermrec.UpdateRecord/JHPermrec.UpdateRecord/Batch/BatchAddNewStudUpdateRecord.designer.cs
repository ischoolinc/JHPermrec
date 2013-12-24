namespace JHPermrec.UpdateRecord.Batch
{
    partial class BatchAddNewStudUpdateRecord
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnAddData = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.dtUpdateDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.cboMonth = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtYear = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cboGradeYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dtUpdateDate)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(222, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "請輸入新生異動日期及入學年月";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(12, 70);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(65, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "異動日期";
            // 
            // btnAddData
            // 
            this.btnAddData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddData.BackColor = System.Drawing.Color.Transparent;
            this.btnAddData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddData.Location = new System.Drawing.Point(55, 135);
            this.btnAddData.Name = "btnAddData";
            this.btnAddData.Size = new System.Drawing.Size(75, 23);
            this.btnAddData.TabIndex = 4;
            this.btnAddData.Text = "開始產生";
            this.btnAddData.Click += new System.EventHandler(this.btnAddData_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(150, 135);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "取消";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dtUpdateDate
            // 
            this.dtUpdateDate.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dtUpdateDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtUpdateDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtUpdateDate.ButtonDropDown.Visible = true;
            this.dtUpdateDate.Location = new System.Drawing.Point(74, 71);
            // 
            // 
            // 
            this.dtUpdateDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtUpdateDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtUpdateDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtUpdateDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtUpdateDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtUpdateDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtUpdateDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtUpdateDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtUpdateDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtUpdateDate.MonthCalendar.DayNames = new string[] {
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"};
            this.dtUpdateDate.MonthCalendar.DisplayMonth = new System.DateTime(2009, 4, 1, 0, 0, 0, 0);
            this.dtUpdateDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtUpdateDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtUpdateDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtUpdateDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtUpdateDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtUpdateDate.MonthCalendar.TodayButtonVisible = true;
            this.dtUpdateDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtUpdateDate.Name = "dtUpdateDate";
            this.dtUpdateDate.Size = new System.Drawing.Size(151, 23);
            this.dtUpdateDate.TabIndex = 6;
            this.dtUpdateDate.WatermarkText = "2009/9/1";
            this.dtUpdateDate.TextChanged += new System.EventHandler(this.dtUpdateDate_TextChanged);
            // 
            // cboMonth
            // 
            this.cboMonth.DisplayMember = "Text";
            this.cboMonth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.ItemHeight = 17;
            this.cboMonth.Location = new System.Drawing.Point(169, 102);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(56, 23);
            this.cboMonth.TabIndex = 12;
            // 
            // txtYear
            // 
            // 
            // 
            // 
            this.txtYear.Border.Class = "TextBoxBorder";
            this.txtYear.Location = new System.Drawing.Point(73, 102);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(57, 23);
            this.txtYear.TabIndex = 11;
            this.txtYear.WatermarkText = "2009";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            this.labelX5.Location = new System.Drawing.Point(136, 102);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(36, 23);
            this.labelX5.TabIndex = 13;
            this.labelX5.Text = "月份";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            this.labelX3.Location = new System.Drawing.Point(11, 102);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(65, 23);
            this.labelX3.TabIndex = 10;
            this.labelX3.Text = "入學年度";
            // 
            // cboGradeYear
            // 
            this.cboGradeYear.DisplayMember = "Text";
            this.cboGradeYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGradeYear.FormattingEnabled = true;
            this.cboGradeYear.ItemHeight = 17;
            this.cboGradeYear.Location = new System.Drawing.Point(73, 41);
            this.cboGradeYear.Name = "cboGradeYear";
            this.cboGradeYear.Size = new System.Drawing.Size(149, 23);
            this.cboGradeYear.TabIndex = 14;
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            this.labelX4.Location = new System.Drawing.Point(37, 41);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(39, 23);
            this.labelX4.TabIndex = 15;
            this.labelX4.Text = "年級";
            // 
            // BatchAddNewStudUpdateRecord
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(234, 169);
            this.Controls.Add(this.cboGradeYear);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.cboMonth);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.dtUpdateDate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAddData);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MinimumSize = new System.Drawing.Size(242, 173);
            this.Name = "BatchAddNewStudUpdateRecord";
            this.Text = "產生新生異動";
            this.Load += new System.EventHandler(this.BatchAddNewStudUpdateRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtUpdateDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnAddData;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtUpdateDate;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboMonth;
        private DevComponents.DotNetBar.Controls.TextBoxX txtYear;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboGradeYear;
        private DevComponents.DotNetBar.LabelX labelX4;

    }
}