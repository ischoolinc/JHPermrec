namespace TaiChung.StudentRecordReport.ConfigForm
{
    partial class PrintConfigForm
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
            this.gpDomainSubject = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbSubject = new System.Windows.Forms.RadioButton();
            this.rbDomain = new System.Windows.Forms.RadioButton();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.gpDomainSubject.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpDomainSubject
            // 
            this.gpDomainSubject.BackColor = System.Drawing.Color.Transparent;
            this.gpDomainSubject.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpDomainSubject.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpDomainSubject.Controls.Add(this.panel1);
            this.gpDomainSubject.DrawTitleBox = false;
            this.gpDomainSubject.Location = new System.Drawing.Point(7, 8);
            this.gpDomainSubject.Name = "gpDomainSubject";
            this.gpDomainSubject.Size = new System.Drawing.Size(322, 139);
            // 
            // 
            // 
            this.gpDomainSubject.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpDomainSubject.Style.BackColorGradientAngle = 90;
            this.gpDomainSubject.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpDomainSubject.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpDomainSubject.Style.BorderBottomWidth = 1;
            this.gpDomainSubject.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpDomainSubject.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpDomainSubject.Style.BorderLeftWidth = 1;
            this.gpDomainSubject.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpDomainSubject.Style.BorderRightWidth = 1;
            this.gpDomainSubject.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpDomainSubject.Style.BorderTopWidth = 1;
            this.gpDomainSubject.Style.Class = "";
            this.gpDomainSubject.Style.CornerDiameter = 4;
            this.gpDomainSubject.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpDomainSubject.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpDomainSubject.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpDomainSubject.StyleMouseDown.Class = "";
            this.gpDomainSubject.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpDomainSubject.StyleMouseOver.Class = "";
            this.gpDomainSubject.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpDomainSubject.TabIndex = 11;
            this.gpDomainSubject.Text = "領域科目";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.rbSubject);
            this.panel1.Controls.Add(this.rbDomain);
            this.panel1.Controls.Add(this.labelX2);
            this.panel1.Controls.Add(this.labelX1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(316, 112);
            this.panel1.TabIndex = 12;
            // 
            // rbSubject
            // 
            this.rbSubject.AutoSize = true;
            this.rbSubject.Location = new System.Drawing.Point(13, 58);
            this.rbSubject.Name = "rbSubject";
            this.rbSubject.Size = new System.Drawing.Size(91, 21);
            this.rbSubject.TabIndex = 0;
            this.rbSubject.TabStop = true;
            this.rbSubject.Text = "只列印科目";
            this.rbSubject.UseVisualStyleBackColor = true;
            // 
            // rbDomain
            // 
            this.rbDomain.AutoSize = true;
            this.rbDomain.Checked = true;
            this.rbDomain.Location = new System.Drawing.Point(13, 4);
            this.rbDomain.Name = "rbDomain";
            this.rbDomain.Size = new System.Drawing.Size(91, 21);
            this.rbDomain.TabIndex = 0;
            this.rbDomain.TabStop = true;
            this.rbDomain.Text = "只列印領域";
            this.rbDomain.UseVisualStyleBackColor = true;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(29, 77);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(189, 25);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "列印全部領域的所有科目。";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(29, 23);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(286, 25);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "包含「語文」及「彈性課程」領域的所有科目。";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(183, 159);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "儲存設定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Location = new System.Drawing.Point(264, 159);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "離開";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // PrintConfigForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(336, 186);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gpDomainSubject);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PrintConfigForm";
            this.Text = "列印設定";
            this.gpDomainSubject.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gpDomainSubject;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.RadioButton rbSubject;
        private System.Windows.Forms.RadioButton rbDomain;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}