namespace TaiChung.StudentRecordReport
{
    partial class PrintForm
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
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtBusinessContractor = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtRegisteredLeader = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lnkPrint = new System.Windows.Forms.LinkLabel();
            this.lnkHDay = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Location = new System.Drawing.Point(130, 79);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(62, 23);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "列印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(200, 79);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(62, 23);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "業務承辦人";
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
            this.labelX2.Location = new System.Drawing.Point(13, 38);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "註冊組長";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtBusinessContractor
            // 
            // 
            // 
            // 
            this.txtBusinessContractor.Border.Class = "TextBoxBorder";
            this.txtBusinessContractor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBusinessContractor.Location = new System.Drawing.Point(95, 6);
            this.txtBusinessContractor.Name = "txtBusinessContractor";
            this.txtBusinessContractor.Size = new System.Drawing.Size(169, 25);
            this.txtBusinessContractor.TabIndex = 4;
            // 
            // txtRegisteredLeader
            // 
            // 
            // 
            // 
            this.txtRegisteredLeader.Border.Class = "TextBoxBorder";
            this.txtRegisteredLeader.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRegisteredLeader.Location = new System.Drawing.Point(95, 38);
            this.txtRegisteredLeader.Name = "txtRegisteredLeader";
            this.txtRegisteredLeader.Size = new System.Drawing.Size(169, 25);
            this.txtRegisteredLeader.TabIndex = 5;
            // 
            // lnkPrint
            // 
            this.lnkPrint.AutoSize = true;
            this.lnkPrint.BackColor = System.Drawing.Color.Transparent;
            this.lnkPrint.Location = new System.Drawing.Point(27, 139);
            this.lnkPrint.Name = "lnkPrint";
            this.lnkPrint.Size = new System.Drawing.Size(60, 17);
            this.lnkPrint.TabIndex = 6;
            this.lnkPrint.TabStop = true;
            this.lnkPrint.Text = "列印設定";
            this.lnkPrint.Visible = false;
            this.lnkPrint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPrint_LinkClicked);
            // 
            // lnkHDay
            // 
            this.lnkHDay.AutoSize = true;
            this.lnkHDay.BackColor = System.Drawing.Color.Transparent;
            this.lnkHDay.Location = new System.Drawing.Point(28, 79);
            this.lnkHDay.Name = "lnkHDay";
            this.lnkHDay.Size = new System.Drawing.Size(60, 17);
            this.lnkHDay.TabIndex = 7;
            this.lnkHDay.TabStop = true;
            this.lnkHDay.Text = "假別設定";
            this.lnkHDay.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHDay_LinkClicked);
            // 
            // PrintForm
            // 
            this.ClientSize = new System.Drawing.Size(277, 109);
            this.Controls.Add(this.lnkHDay);
            this.Controls.Add(this.lnkPrint);
            this.Controls.Add(this.txtRegisteredLeader);
            this.Controls.Add(this.txtBusinessContractor);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.DoubleBuffered = true;
            this.Name = "PrintForm";
            this.Text = "台中學籍表";
            this.Load += new System.EventHandler(this.PrintForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBusinessContractor;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRegisteredLeader;
        private System.Windows.Forms.LinkLabel lnkPrint;
        private System.Windows.Forms.LinkLabel lnkHDay;
    }
}