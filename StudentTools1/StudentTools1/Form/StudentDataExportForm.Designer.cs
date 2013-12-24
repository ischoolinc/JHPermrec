namespace StudentTools1.Form
{
    partial class StudentDataExportForm
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
            this.gpExportData = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.lvSelItems = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.Item = new System.Windows.Forms.ColumnHeader();
            this.gpSorter = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chkSortByClassSeatNo = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkSortByStudNum = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.gpExportData.SuspendLayout();
            this.gpSorter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpExportData
            // 
            this.gpExportData.BackColor = System.Drawing.Color.Transparent;
            this.gpExportData.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpExportData.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpExportData.Controls.Add(this.lvSelItems);
            this.gpExportData.Location = new System.Drawing.Point(11, 13);
            this.gpExportData.Name = "gpExportData";
            this.gpExportData.Size = new System.Drawing.Size(239, 166);
            // 
            // 
            // 
            this.gpExportData.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpExportData.Style.BackColorGradientAngle = 90;
            this.gpExportData.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpExportData.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpExportData.Style.BorderBottomWidth = 1;
            this.gpExportData.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpExportData.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpExportData.Style.BorderLeftWidth = 1;
            this.gpExportData.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpExportData.Style.BorderRightWidth = 1;
            this.gpExportData.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpExportData.Style.BorderTopWidth = 1;
            this.gpExportData.Style.CornerDiameter = 4;
            this.gpExportData.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpExportData.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpExportData.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.gpExportData.TabIndex = 0;
            this.gpExportData.Text = "可產生資料項目";
            // 
            // lvSelItems
            // 
            // 
            // 
            // 
            this.lvSelItems.Border.Class = "ListViewBorder";
            this.lvSelItems.CheckBoxes = true;
            this.lvSelItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Item});
            this.lvSelItems.Location = new System.Drawing.Point(4, 7);
            this.lvSelItems.Name = "lvSelItems";
            this.lvSelItems.Size = new System.Drawing.Size(228, 129);
            this.lvSelItems.TabIndex = 0;
            this.lvSelItems.UseCompatibleStateImageBehavior = false;
            this.lvSelItems.View = System.Windows.Forms.View.Details;
            // 
            // Item
            // 
            this.Item.Text = "資料項目";
            this.Item.Width = 188;
            // 
            // gpSorter
            // 
            this.gpSorter.BackColor = System.Drawing.Color.Transparent;
            this.gpSorter.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpSorter.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpSorter.Controls.Add(this.chkSortByClassSeatNo);
            this.gpSorter.Controls.Add(this.chkSortByStudNum);
            this.gpSorter.Location = new System.Drawing.Point(261, 13);
            this.gpSorter.Name = "gpSorter";
            this.gpSorter.Size = new System.Drawing.Size(125, 84);
            // 
            // 
            // 
            this.gpSorter.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpSorter.Style.BackColorGradientAngle = 90;
            this.gpSorter.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpSorter.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSorter.Style.BorderBottomWidth = 1;
            this.gpSorter.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpSorter.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSorter.Style.BorderLeftWidth = 1;
            this.gpSorter.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSorter.Style.BorderRightWidth = 1;
            this.gpSorter.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSorter.Style.BorderTopWidth = 1;
            this.gpSorter.Style.CornerDiameter = 4;
            this.gpSorter.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpSorter.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpSorter.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.gpSorter.TabIndex = 1;
            this.gpSorter.Text = "排序方式";
            // 
            // chkSortByClassSeatNo
            // 
            this.chkSortByClassSeatNo.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkSortByClassSeatNo.Location = new System.Drawing.Point(2, 7);
            this.chkSortByClassSeatNo.Name = "chkSortByClassSeatNo";
            this.chkSortByClassSeatNo.Size = new System.Drawing.Size(100, 23);
            this.chkSortByClassSeatNo.TabIndex = 1;
            this.chkSortByClassSeatNo.Text = "依班級座號";
            // 
            // chkSortByStudNum
            // 
            this.chkSortByStudNum.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkSortByStudNum.Location = new System.Drawing.Point(2, 30);
            this.chkSortByStudNum.Name = "chkSortByStudNum";
            this.chkSortByStudNum.Size = new System.Drawing.Size(75, 23);
            this.chkSortByStudNum.TabIndex = 0;
            this.chkSortByStudNum.Text = "依學號";
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(261, 152);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(60, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "執行";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(327, 152);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(59, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.ForeColor = System.Drawing.Color.Red;
            this.labelX1.Location = new System.Drawing.Point(266, 119);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(100, 23);
            this.labelX1.TabIndex = 4;
            this.labelX1.Text = "資料產生中..";
            // 
            // StudentDataExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 183);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.gpSorter);
            this.Controls.Add(this.gpExportData);
            this.Name = "StudentDataExportForm";
            this.Text = "產生常用學生基本資料";
            this.Load += new System.EventHandler(this.StudentDataExportForm_Load);
            this.gpExportData.ResumeLayout(false);
            this.gpSorter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gpExportData;
        private DevComponents.DotNetBar.Controls.GroupPanel gpSorter;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSortByStudNum;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSortByClassSeatNo;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.ListViewEx lvSelItems;
        private System.Windows.Forms.ColumnHeader Item;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}