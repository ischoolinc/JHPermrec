﻿namespace JHSchool.Permrec.StudentExtendControls.Reports
{
    partial class StudGraduateCertficateForm
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
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.cbxUserDefine = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbxDefault = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lnkUpload = new System.Windows.Forms.LinkLabel();
            this.lnkUserDefine = new System.Windows.Forms.LinkLabel();
            this.lnkDefault = new System.Windows.Forms.LinkLabel();
            this.cmdPrint = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtCertDoc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtCertNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cbxUserDefine2 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbxDefault2 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lnkUpload2 = new System.Windows.Forms.LinkLabel();
            this.lnkUserDefine2 = new System.Windows.Forms.LinkLabel();
            this.lnkDefault2 = new System.Windows.Forms.LinkLabel();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.checkBoxX2 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbIsPointByUpdateRecord = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnExit.Location = new System.Drawing.Point(285, 138);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(61, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbxUserDefine
            // 
            this.cbxUserDefine.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbxUserDefine.BackgroundStyle.Class = "";
            this.cbxUserDefine.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxUserDefine.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxUserDefine.Location = new System.Drawing.Point(13, 53);
            this.cbxUserDefine.Name = "cbxUserDefine";
            this.cbxUserDefine.Size = new System.Drawing.Size(27, 23);
            this.cbxUserDefine.TabIndex = 4;
            // 
            // cbxDefault
            // 
            this.cbxDefault.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbxDefault.BackgroundStyle.Class = "";
            this.cbxDefault.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxDefault.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxDefault.Location = new System.Drawing.Point(13, 24);
            this.cbxDefault.Name = "cbxDefault";
            this.cbxDefault.Size = new System.Drawing.Size(27, 23);
            this.cbxDefault.TabIndex = 3;
            // 
            // lnkUpload
            // 
            this.lnkUpload.AutoSize = true;
            this.lnkUpload.BackColor = System.Drawing.Color.Transparent;
            this.lnkUpload.Location = new System.Drawing.Point(174, 58);
            this.lnkUpload.Name = "lnkUpload";
            this.lnkUpload.Size = new System.Drawing.Size(31, 16);
            this.lnkUpload.TabIndex = 2;
            this.lnkUpload.TabStop = true;
            this.lnkUpload.Text = "上傳";
            this.lnkUpload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpload_LinkClicked);
            // 
            // lnkUserDefine
            // 
            this.lnkUserDefine.AutoSize = true;
            this.lnkUserDefine.BackColor = System.Drawing.Color.Transparent;
            this.lnkUserDefine.Location = new System.Drawing.Point(38, 58);
            this.lnkUserDefine.Name = "lnkUserDefine";
            this.lnkUserDefine.Size = new System.Drawing.Size(127, 16);
            this.lnkUserDefine.TabIndex = 1;
            this.lnkUserDefine.TabStop = true;
            this.lnkUserDefine.Text = "檢視畢業證書自訂範本";
            this.lnkUserDefine.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUserDefine_LinkClicked);
            // 
            // lnkDefault
            // 
            this.lnkDefault.AutoSize = true;
            this.lnkDefault.BackColor = System.Drawing.Color.Transparent;
            this.lnkDefault.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lnkDefault.Location = new System.Drawing.Point(38, 28);
            this.lnkDefault.Name = "lnkDefault";
            this.lnkDefault.Size = new System.Drawing.Size(127, 16);
            this.lnkDefault.TabIndex = 0;
            this.lnkDefault.TabStop = true;
            this.lnkDefault.Text = "檢視畢業證書預設範本";
            this.lnkDefault.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDefault_LinkClicked);
            // 
            // cmdPrint
            // 
            this.cmdPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdPrint.BackColor = System.Drawing.Color.Transparent;
            this.cmdPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdPrint.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmdPrint.Location = new System.Drawing.Point(218, 138);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(61, 23);
            this.cmdPrint.TabIndex = 4;
            this.cmdPrint.Text = "列印";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(5, 181);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(61, 23);
            this.labelX1.TabIndex = 7;
            this.labelX1.Text = "校內文號";
            this.labelX1.Visible = false;
            // 
            // txtCertDoc
            // 
            // 
            // 
            // 
            this.txtCertDoc.Border.Class = "TextBoxBorder";
            this.txtCertDoc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCertDoc.Location = new System.Drawing.Point(62, 181);
            this.txtCertDoc.Name = "txtCertDoc";
            this.txtCertDoc.Size = new System.Drawing.Size(102, 23);
            this.txtCertDoc.TabIndex = 8;
            this.txtCertDoc.Visible = false;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(170, 181);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(32, 23);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "字第";
            this.labelX2.Visible = false;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(313, 181);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(32, 23);
            this.labelX3.TabIndex = 10;
            this.labelX3.Text = "號";
            this.labelX3.Visible = false;
            // 
            // txtCertNo
            // 
            // 
            // 
            // 
            this.txtCertNo.Border.Class = "TextBoxBorder";
            this.txtCertNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCertNo.Location = new System.Drawing.Point(205, 181);
            this.txtCertNo.Name = "txtCertNo";
            this.txtCertNo.Size = new System.Drawing.Size(102, 23);
            this.txtCertNo.TabIndex = 11;
            this.txtCertNo.Visible = false;
            // 
            // cbxUserDefine2
            // 
            this.cbxUserDefine2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbxUserDefine2.BackgroundStyle.Class = "";
            this.cbxUserDefine2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxUserDefine2.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxUserDefine2.Location = new System.Drawing.Point(13, 112);
            this.cbxUserDefine2.Name = "cbxUserDefine2";
            this.cbxUserDefine2.Size = new System.Drawing.Size(27, 23);
            this.cbxUserDefine2.TabIndex = 16;
            // 
            // cbxDefault2
            // 
            this.cbxDefault2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbxDefault2.BackgroundStyle.Class = "";
            this.cbxDefault2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxDefault2.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxDefault2.Location = new System.Drawing.Point(13, 83);
            this.cbxDefault2.Name = "cbxDefault2";
            this.cbxDefault2.Size = new System.Drawing.Size(27, 23);
            this.cbxDefault2.TabIndex = 15;
            // 
            // lnkUpload2
            // 
            this.lnkUpload2.AutoSize = true;
            this.lnkUpload2.BackColor = System.Drawing.Color.Transparent;
            this.lnkUpload2.Location = new System.Drawing.Point(174, 117);
            this.lnkUpload2.Name = "lnkUpload2";
            this.lnkUpload2.Size = new System.Drawing.Size(31, 16);
            this.lnkUpload2.TabIndex = 14;
            this.lnkUpload2.TabStop = true;
            this.lnkUpload2.Text = "上傳";
            this.lnkUpload2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpload2_LinkClicked);
            // 
            // lnkUserDefine2
            // 
            this.lnkUserDefine2.AutoSize = true;
            this.lnkUserDefine2.BackColor = System.Drawing.Color.Transparent;
            this.lnkUserDefine2.Location = new System.Drawing.Point(38, 117);
            this.lnkUserDefine2.Name = "lnkUserDefine2";
            this.lnkUserDefine2.Size = new System.Drawing.Size(127, 16);
            this.lnkUserDefine2.TabIndex = 13;
            this.lnkUserDefine2.TabStop = true;
            this.lnkUserDefine2.Text = "檢視修業證書自訂範本";
            this.lnkUserDefine2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUserDefine2_LinkClicked);
            // 
            // lnkDefault2
            // 
            this.lnkDefault2.AutoSize = true;
            this.lnkDefault2.BackColor = System.Drawing.Color.Transparent;
            this.lnkDefault2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lnkDefault2.Location = new System.Drawing.Point(38, 87);
            this.lnkDefault2.Name = "lnkDefault2";
            this.lnkDefault2.Size = new System.Drawing.Size(127, 16);
            this.lnkDefault2.TabIndex = 12;
            this.lnkDefault2.TabStop = true;
            this.lnkDefault2.Text = "檢視修業證書預設範本";
            this.lnkDefault2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDefault2_LinkClicked);
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.cbIsPointByUpdateRecord);
            this.groupPanel1.Controls.Add(this.checkBoxX2);
            this.groupPanel1.Location = new System.Drawing.Point(218, 10);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(128, 122);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 17;
            this.groupPanel1.Text = "證書字號來源";
            // 
            // checkBoxX2
            // 
            // 
            // 
            // 
            this.checkBoxX2.BackgroundStyle.Class = "";
            this.checkBoxX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX2.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkBoxX2.Location = new System.Drawing.Point(12, 35);
            this.checkBoxX2.Name = "checkBoxX2";
            this.checkBoxX2.Size = new System.Drawing.Size(100, 52);
            this.checkBoxX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX2.TabIndex = 1;
            this.checkBoxX2.Text = "畢業資訊\r\n(資料項目)";
            // 
            // cbIsPointByUpdateRecord
            // 
            this.cbIsPointByUpdateRecord.AutoSize = true;
            // 
            // 
            // 
            this.cbIsPointByUpdateRecord.BackgroundStyle.Class = "";
            this.cbIsPointByUpdateRecord.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbIsPointByUpdateRecord.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbIsPointByUpdateRecord.Checked = true;
            this.cbIsPointByUpdateRecord.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsPointByUpdateRecord.CheckValue = "Y";
            this.cbIsPointByUpdateRecord.Location = new System.Drawing.Point(12, 10);
            this.cbIsPointByUpdateRecord.Name = "cbIsPointByUpdateRecord";
            this.cbIsPointByUpdateRecord.Size = new System.Drawing.Size(76, 20);
            this.cbIsPointByUpdateRecord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbIsPointByUpdateRecord.TabIndex = 0;
            this.cbIsPointByUpdateRecord.Text = "畢業異動";
            // 
            // StudGraduateCertficateForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(358, 168);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.cbxUserDefine2);
            this.Controls.Add(this.cbxDefault2);
            this.Controls.Add(this.lnkUpload2);
            this.Controls.Add(this.lnkUserDefine2);
            this.Controls.Add(this.lnkDefault2);
            this.Controls.Add(this.cbxUserDefine);
            this.Controls.Add(this.txtCertNo);
            this.Controls.Add(this.cbxDefault);
            this.Controls.Add(this.lnkUpload);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.lnkUserDefine);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.lnkDefault);
            this.Controls.Add(this.txtCertDoc);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cmdPrint);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "StudGraduateCertficateForm";
            this.Text = "畢業證書範本設定與列印";
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxUserDefine;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxDefault;
        private System.Windows.Forms.LinkLabel lnkUpload;
        private System.Windows.Forms.LinkLabel lnkUserDefine;
        private System.Windows.Forms.LinkLabel lnkDefault;
        private DevComponents.DotNetBar.ButtonX cmdPrint;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCertDoc;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCertNo;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxUserDefine2;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxDefault2;
        private System.Windows.Forms.LinkLabel lnkUpload2;
        private System.Windows.Forms.LinkLabel lnkUserDefine2;
        private System.Windows.Forms.LinkLabel lnkDefault2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX2;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbIsPointByUpdateRecord;
    }
}