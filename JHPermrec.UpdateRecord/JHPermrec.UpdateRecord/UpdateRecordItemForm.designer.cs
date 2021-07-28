﻿namespace JHPermrec.UpdateRecord
{
    partial class UpdateRecordItemForm
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
            this.cbxSel = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.UpdateRecordEditorPanle = new DevComponents.DotNetBar.PanelEx();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.intSchoolYear = new DevComponents.Editors.IntegerInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.intSemester = new DevComponents.Editors.IntegerInput();
            this.integerInput2 = new DevComponents.Editors.IntegerInput();
            this.lablex11 = new DevComponents.DotNetBar.LabelX();
            this.intGradeYear = new DevComponents.Editors.IntegerInput();
            this.btnUpload = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.intSchoolYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intSemester)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.integerInput2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intGradeYear)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxSel
            // 
            this.cbxSel.DisplayMember = "Text";
            this.cbxSel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxSel.FormattingEnabled = true;
            this.cbxSel.ItemHeight = 17;
            this.cbxSel.Location = new System.Drawing.Point(11, 11);
            this.cbxSel.Name = "cbxSel";
            this.cbxSel.Size = new System.Drawing.Size(121, 23);
            this.cbxSel.TabIndex = 0;
            this.cbxSel.SelectedIndexChanged += new System.EventHandler(this.cbxSel_SelectedIndexChanged);
            // 
            // UpdateRecordEditorPanle
            // 
            this.UpdateRecordEditorPanle.CanvasColor = System.Drawing.SystemColors.Control;
            this.UpdateRecordEditorPanle.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.UpdateRecordEditorPanle.Location = new System.Drawing.Point(9, 45);
            this.UpdateRecordEditorPanle.Name = "UpdateRecordEditorPanle";
            this.UpdateRecordEditorPanle.Size = new System.Drawing.Size(460, 481);
            this.UpdateRecordEditorPanle.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.UpdateRecordEditorPanle.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.UpdateRecordEditorPanle.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.UpdateRecordEditorPanle.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.UpdateRecordEditorPanle.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.UpdateRecordEditorPanle.Style.GradientAngle = 90;
            this.UpdateRecordEditorPanle.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(402, 537);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Location = new System.Drawing.Point(332, 537);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(64, 23);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "確定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // intSchoolYear
            // 
            this.intSchoolYear.AutoOverwrite = true;
            this.intSchoolYear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.intSchoolYear.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSchoolYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intSchoolYear.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSchoolYear.Location = new System.Drawing.Point(199, 12);
            this.intSchoolYear.MaxValue = 9999;
            this.intSchoolYear.MinValue = 1;
            this.intSchoolYear.Name = "intSchoolYear";
            this.intSchoolYear.Size = new System.Drawing.Size(57, 23);
            this.intSchoolYear.TabIndex = 5;
            this.intSchoolYear.Value = 1;
            this.intSchoolYear.ValueChanged += new System.EventHandler(this.intSchoolYear_ValueChanged);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(144, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(52, 23);
            this.labelX1.TabIndex = 6;
            this.labelX1.Text = "學年度";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(266, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(37, 23);
            this.labelX2.TabIndex = 8;
            this.labelX2.Text = "學期";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // intSemester
            // 
            this.intSemester.AutoOverwrite = true;
            this.intSemester.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.intSemester.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSemester.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intSemester.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSemester.Location = new System.Drawing.Point(309, 12);
            this.intSemester.MaxValue = 2;
            this.intSemester.MinValue = 1;
            this.intSemester.Name = "intSemester";
            this.intSemester.Size = new System.Drawing.Size(49, 23);
            this.intSemester.TabIndex = 7;
            this.intSemester.Value = 1;
            // 
            // integerInput2
            // 
            this.integerInput2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.integerInput2.BackgroundStyle.Class = "DateTimeInputBackground";
            this.integerInput2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.integerInput2.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.integerInput2.Location = new System.Drawing.Point(364, 11);
            this.integerInput2.Name = "integerInput2";
            this.integerInput2.ShowUpDown = true;
            this.integerInput2.Size = new System.Drawing.Size(67, 22);
            this.integerInput2.TabIndex = 7;
            // 
            // lablex11
            // 
            this.lablex11.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lablex11.BackgroundStyle.Class = "";
            this.lablex11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lablex11.Location = new System.Drawing.Point(369, 12);
            this.lablex11.Name = "lablex11";
            this.lablex11.Size = new System.Drawing.Size(37, 23);
            this.lablex11.TabIndex = 10;
            this.lablex11.Text = "年級";
            this.lablex11.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // intGradeYear
            // 
            this.intGradeYear.AutoOverwrite = true;
            this.intGradeYear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.intGradeYear.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intGradeYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intGradeYear.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intGradeYear.Location = new System.Drawing.Point(410, 12);
            this.intGradeYear.MaxValue = 12;
            this.intGradeYear.MinValue = 1;
            this.intGradeYear.Name = "intGradeYear";
            this.intGradeYear.Size = new System.Drawing.Size(49, 23);
            this.intGradeYear.TabIndex = 9;
            this.intGradeYear.Value = 1;
            // 
            // btnUpload
            // 
            this.btnUpload.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpload.AutoSize = true;
            this.btnUpload.BackColor = System.Drawing.Color.Transparent;
            this.btnUpload.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUpload.Location = new System.Drawing.Point(13, 537);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 24);
            this.btnUpload.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUpload.TabIndex = 11;
            this.btnUpload.Text = "上傳檔案";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // UpdateRecordItemForm
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(480, 569);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.lablex11);
            this.Controls.Add(this.intGradeYear);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.intSemester);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.intSchoolYear);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.UpdateRecordEditorPanle);
            this.Controls.Add(this.cbxSel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "UpdateRecordItemForm";
            this.Text = "管理學生異動資料";
            this.Load += new System.EventHandler(this.UpdateRecordItemForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.intSchoolYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intSemester)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.integerInput2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intGradeYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxSel;
        private DevComponents.DotNetBar.PanelEx UpdateRecordEditorPanle;
        private JHPermrec.UpdateRecord.UpdateRecordItemControls.UpdateRecordInfo01 updateRecordInfo011;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.Editors.IntegerInput intSchoolYear;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.IntegerInput intSemester;
        private DevComponents.Editors.IntegerInput integerInput2;
        private DevComponents.DotNetBar.LabelX lablex11;
        private DevComponents.Editors.IntegerInput intGradeYear;
        private DevComponents.DotNetBar.ButtonX btnUpload;
    }
}