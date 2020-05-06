namespace JHSchool.Permrec.StudentExtendControls.Reports
{
    partial class StudentAtSchoolCertificateForm
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
            this.lnkDefault_ChiEng = new System.Windows.Forms.LinkLabel();
            this.cmdPrint = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtCertDoc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtCertNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cbxDefault_Chi = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lnkDefault_Chi = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnExit.Location = new System.Drawing.Point(272, 146);
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
            this.cbxUserDefine.Location = new System.Drawing.Point(12, 67);
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
            this.cbxDefault.Location = new System.Drawing.Point(12, 38);
            this.cbxDefault.Name = "cbxDefault";
            this.cbxDefault.Size = new System.Drawing.Size(27, 23);
            this.cbxDefault.TabIndex = 3;
            // 
            // lnkUpload
            // 
            this.lnkUpload.AutoSize = true;
            this.lnkUpload.BackColor = System.Drawing.Color.Transparent;
            this.lnkUpload.Location = new System.Drawing.Point(141, 72);
            this.lnkUpload.Name = "lnkUpload";
            this.lnkUpload.Size = new System.Drawing.Size(32, 16);
            this.lnkUpload.TabIndex = 2;
            this.lnkUpload.TabStop = true;
            this.lnkUpload.Text = "上傳";
            this.lnkUpload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpload_LinkClicked);
            // 
            // lnkUserDefine
            // 
            this.lnkUserDefine.AutoSize = true;
            this.lnkUserDefine.BackColor = System.Drawing.Color.Transparent;
            this.lnkUserDefine.Location = new System.Drawing.Point(37, 72);
            this.lnkUserDefine.Name = "lnkUserDefine";
            this.lnkUserDefine.Size = new System.Drawing.Size(80, 16);
            this.lnkUserDefine.TabIndex = 1;
            this.lnkUserDefine.TabStop = true;
            this.lnkUserDefine.Text = "檢視自訂範本";
            this.lnkUserDefine.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUserDefine_LinkClicked);
            // 
            // lnkDefault_ChiEng
            // 
            this.lnkDefault_ChiEng.AutoSize = true;
            this.lnkDefault_ChiEng.BackColor = System.Drawing.Color.Transparent;
            this.lnkDefault_ChiEng.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lnkDefault_ChiEng.Location = new System.Drawing.Point(37, 42);
            this.lnkDefault_ChiEng.Name = "lnkDefault_ChiEng";
            this.lnkDefault_ChiEng.Size = new System.Drawing.Size(124, 16);
            this.lnkDefault_ChiEng.TabIndex = 0;
            this.lnkDefault_ChiEng.TabStop = true;
            this.lnkDefault_ChiEng.Text = "檢視預設範本(中英文)";
            this.lnkDefault_ChiEng.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDefault_ChiEng_LinkClicked);
            // 
            // cmdPrint
            // 
            this.cmdPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdPrint.BackColor = System.Drawing.Color.Transparent;
            this.cmdPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdPrint.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmdPrint.Location = new System.Drawing.Point(199, 146);
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
            this.labelX1.Location = new System.Drawing.Point(12, 107);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(74, 23);
            this.labelX1.TabIndex = 7;
            this.labelX1.Text = "校內文號";
            // 
            // txtCertDoc
            // 
            // 
            // 
            // 
            this.txtCertDoc.Border.Class = "TextBoxBorder";
            this.txtCertDoc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCertDoc.Location = new System.Drawing.Point(81, 107);
            this.txtCertDoc.Name = "txtCertDoc";
            this.txtCertDoc.Size = new System.Drawing.Size(102, 23);
            this.txtCertDoc.TabIndex = 8;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(186, 107);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(37, 23);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "字第";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(337, 107);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(32, 23);
            this.labelX3.TabIndex = 10;
            this.labelX3.Text = "號";
            // 
            // txtCertNo
            // 
            // 
            // 
            // 
            this.txtCertNo.Border.Class = "TextBoxBorder";
            this.txtCertNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCertNo.Location = new System.Drawing.Point(228, 107);
            this.txtCertNo.Name = "txtCertNo";
            this.txtCertNo.Size = new System.Drawing.Size(102, 23);
            this.txtCertNo.TabIndex = 11;
            // 
            // cbxDefault_Chi
            // 
            this.cbxDefault_Chi.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbxDefault_Chi.BackgroundStyle.Class = "";
            this.cbxDefault_Chi.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxDefault_Chi.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxDefault_Chi.Location = new System.Drawing.Point(12, 9);
            this.cbxDefault_Chi.Name = "cbxDefault_Chi";
            this.cbxDefault_Chi.Size = new System.Drawing.Size(27, 23);
            this.cbxDefault_Chi.TabIndex = 13;
            // 
            // lnkDefault_Chi
            // 
            this.lnkDefault_Chi.AutoSize = true;
            this.lnkDefault_Chi.BackColor = System.Drawing.Color.Transparent;
            this.lnkDefault_Chi.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lnkDefault_Chi.Location = new System.Drawing.Point(37, 13);
            this.lnkDefault_Chi.Name = "lnkDefault_Chi";
            this.lnkDefault_Chi.Size = new System.Drawing.Size(112, 16);
            this.lnkDefault_Chi.TabIndex = 12;
            this.lnkDefault_Chi.TabStop = true;
            this.lnkDefault_Chi.Text = "檢視預設範本(中文)";
            this.lnkDefault_Chi.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDefault_Chi_LinkClicked);
            // 
            // StudentAtSchoolCertificateForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(364, 181);
            this.Controls.Add(this.cbxDefault_Chi);
            this.Controls.Add(this.lnkDefault_Chi);
            this.Controls.Add(this.cbxUserDefine);
            this.Controls.Add(this.txtCertNo);
            this.Controls.Add(this.cbxDefault);
            this.Controls.Add(this.lnkUpload);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.lnkUserDefine);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.lnkDefault_ChiEng);
            this.Controls.Add(this.txtCertDoc);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cmdPrint);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MaximumSize = new System.Drawing.Size(380, 220);
            this.MinimumSize = new System.Drawing.Size(380, 220);
            this.Name = "StudentAtSchoolCertificateForm";
            this.Text = "在學證明書(無成績) 範本設定與列印";
            this.Load += new System.EventHandler(this.StudentAtSchoolCertificateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxUserDefine;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxDefault;
        private System.Windows.Forms.LinkLabel lnkUpload;
        private System.Windows.Forms.LinkLabel lnkUserDefine;
        private System.Windows.Forms.LinkLabel lnkDefault_ChiEng;
        private DevComponents.DotNetBar.ButtonX cmdPrint;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCertDoc;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCertNo;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxDefault_Chi;
        private System.Windows.Forms.LinkLabel lnkDefault_Chi;
    }
}