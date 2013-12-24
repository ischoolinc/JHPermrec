namespace Student_JSBT_HsinChu101
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
            this.chkByClassSeatNo = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkByStudentNum = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lnkSetSCT = new System.Windows.Forms.LinkLabel();
            this.btn_Export = new DevComponents.DotNetBar.ButtonX();
            this.btn_Exit = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chkMai = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkPerm = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cbxPhoneType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkByClassSeatNo
            // 
            this.chkByClassSeatNo.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkByClassSeatNo.BackgroundStyle.Class = "";
            this.chkByClassSeatNo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkByClassSeatNo.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkByClassSeatNo.Location = new System.Drawing.Point(9, 32);
            this.chkByClassSeatNo.Name = "chkByClassSeatNo";
            this.chkByClassSeatNo.Size = new System.Drawing.Size(91, 23);
            this.chkByClassSeatNo.TabIndex = 1;
            this.chkByClassSeatNo.Text = "依班級座號";
            // 
            // chkByStudentNum
            // 
            this.chkByStudentNum.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkByStudentNum.BackgroundStyle.Class = "";
            this.chkByStudentNum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkByStudentNum.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkByStudentNum.Location = new System.Drawing.Point(9, 3);
            this.chkByStudentNum.Name = "chkByStudentNum";
            this.chkByStudentNum.Size = new System.Drawing.Size(75, 23);
            this.chkByStudentNum.TabIndex = 2;
            this.chkByStudentNum.Text = "依學號";
            // 
            // lnkSetSCT
            // 
            this.lnkSetSCT.AutoSize = true;
            this.lnkSetSCT.BackColor = System.Drawing.Color.Transparent;
            this.lnkSetSCT.Location = new System.Drawing.Point(9, 117);
            this.lnkSetSCT.Name = "lnkSetSCT";
            this.lnkSetSCT.Size = new System.Drawing.Size(151, 17);
            this.lnkSetSCT.TabIndex = 3;
            this.lnkSetSCT.TabStop = true;
            this.lnkSetSCT.Text = "設定學生類別與欄位對照";
            this.lnkSetSCT.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSetSCT_LinkClicked);
            // 
            // btn_Export
            // 
            this.btn_Export.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Export.BackColor = System.Drawing.Color.Transparent;
            this.btn_Export.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Export.Location = new System.Drawing.Point(198, 117);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(75, 23);
            this.btn_Export.TabIndex = 4;
            this.btn_Export.Text = "產生資料";
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Exit.BackColor = System.Drawing.Color.Transparent;
            this.btn_Exit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Exit.Location = new System.Drawing.Point(287, 117);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(75, 23);
            this.btn_Exit.TabIndex = 0;
            this.btn_Exit.Text = "離開";
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.groupPanel1.Controls.Add(this.chkByStudentNum);
            this.groupPanel1.Controls.Add(this.chkByClassSeatNo);
            this.groupPanel1.Location = new System.Drawing.Point(12, 12);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(111, 85);
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
            this.groupPanel1.TabIndex = 6;
            this.groupPanel1.Text = "排序方式";
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.groupPanel2.Controls.Add(this.chkMai);
            this.groupPanel2.Controls.Add(this.chkPerm);
            this.groupPanel2.Location = new System.Drawing.Point(134, 12);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(104, 85);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.Class = "";
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.Class = "";
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.Class = "";
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 7;
            this.groupPanel2.Text = "地址";
            // 
            // chkMai
            // 
            this.chkMai.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkMai.BackgroundStyle.Class = "";
            this.chkMai.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkMai.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkMai.Location = new System.Drawing.Point(13, 32);
            this.chkMai.Name = "chkMai";
            this.chkMai.Size = new System.Drawing.Size(75, 23);
            this.chkMai.TabIndex = 4;
            this.chkMai.Text = "聯絡";
            // 
            // chkPerm
            // 
            this.chkPerm.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkPerm.BackgroundStyle.Class = "";
            this.chkPerm.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkPerm.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkPerm.Location = new System.Drawing.Point(13, 3);
            this.chkPerm.Name = "chkPerm";
            this.chkPerm.Size = new System.Drawing.Size(75, 23);
            this.chkPerm.TabIndex = 3;
            this.chkPerm.Text = "戶籍";
            // 
            // groupPanel3
            // 
            this.groupPanel3.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.groupPanel3.Controls.Add(this.cbxPhoneType);
            this.groupPanel3.Location = new System.Drawing.Point(246, 12);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(114, 85);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.Class = "";
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.Class = "";
            this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.Class = "";
            this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.TabIndex = 8;
            this.groupPanel3.Text = "緊急連絡電話";
            // 
            // cbxPhoneType
            // 
            this.cbxPhoneType.DisplayMember = "Text";
            this.cbxPhoneType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxPhoneType.FormattingEnabled = true;
            this.cbxPhoneType.ItemHeight = 19;
            this.cbxPhoneType.Location = new System.Drawing.Point(11, 13);
            this.cbxPhoneType.Name = "cbxPhoneType";
            this.cbxPhoneType.Size = new System.Drawing.Size(91, 25);
            this.cbxPhoneType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxPhoneType.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 147);
            this.Controls.Add(this.groupPanel3);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.lnkSetSCT);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "產生竹苗區免試入學報名檔";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.CheckBoxX chkByClassSeatNo;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkByStudentNum;
        private System.Windows.Forms.LinkLabel lnkSetSCT;
        private DevComponents.DotNetBar.ButtonX btn_Export;
        private DevComponents.DotNetBar.ButtonX btn_Exit;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkMai;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkPerm;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxPhoneType;
    }
}