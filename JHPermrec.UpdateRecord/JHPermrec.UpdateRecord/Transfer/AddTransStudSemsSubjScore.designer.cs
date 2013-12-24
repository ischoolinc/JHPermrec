namespace JHPermrec.UpdateRecord.Transfer
{
    partial class AddTransStudSemsSubjScore
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
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.listView = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.chSchoolYear = new System.Windows.Forms.ColumnHeader();
            this.chSemester = new System.Windows.Forms.ColumnHeader();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnView = new DevComponents.DotNetBar.ButtonX();
            this.btnNext = new DevComponents.DotNetBar.ButtonX();
            this.chkNextYes = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkNextNo = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lblStudMsg = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.AutoExpandOnClick = true;
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAdd.Location = new System.Drawing.Point(12, 210);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // listView
            // 
            // 
            // 
            // 
            this.listView.Border.Class = "ListViewBorder";
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSchoolYear,
            this.chSemester});
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(12, 34);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(524, 170);
            this.listView.TabIndex = 4;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // chSchoolYear
            // 
            this.chSchoolYear.Text = "學年度";
            this.chSchoolYear.Width = 57;
            // 
            // chSemester
            // 
            this.chSemester.Text = "學期";
            this.chSemester.Width = 44;
            // 
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.Location = new System.Drawing.Point(258, 103);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(32, 32);
            this.picLoading.TabIndex = 8;
            this.picLoading.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelete.Location = new System.Drawing.Point(174, 210);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "刪除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.BackColor = System.Drawing.Color.Transparent;
            this.btnModify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnModify.Location = new System.Drawing.Point(93, 210);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 2;
            this.btnModify.Text = "修改";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnView
            // 
            this.btnView.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnView.BackColor = System.Drawing.Color.Transparent;
            this.btnView.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnView.Location = new System.Drawing.Point(12, 210);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 9;
            this.btnView.Text = "檢視";
            // 
            // btnNext
            // 
            this.btnNext.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNext.Location = new System.Drawing.Point(461, 210);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 10;
            this.btnNext.Text = "儲存";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // chkNextYes
            // 
            this.chkNextYes.BackColor = System.Drawing.Color.Transparent;
            this.chkNextYes.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkNextYes.Location = new System.Drawing.Point(267, 210);
            this.chkNextYes.Name = "chkNextYes";
            this.chkNextYes.Size = new System.Drawing.Size(88, 23);
            this.chkNextYes.TabIndex = 4;
            this.chkNextYes.Text = "加入修課";
            this.chkNextYes.Visible = false;
            // 
            // chkNextNo
            // 
            this.chkNextNo.BackColor = System.Drawing.Color.Transparent;
            this.chkNextNo.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkNextNo.Location = new System.Drawing.Point(361, 210);
            this.chkNextNo.Name = "chkNextNo";
            this.chkNextNo.Size = new System.Drawing.Size(90, 23);
            this.chkNextNo.TabIndex = 5;
            this.chkNextNo.Text = "不加修課";
            this.chkNextNo.Visible = false;
            // 
            // lblStudMsg
            // 
            this.lblStudMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblStudMsg.Location = new System.Drawing.Point(12, 7);
            this.lblStudMsg.Name = "lblStudMsg";
            this.lblStudMsg.Size = new System.Drawing.Size(524, 23);
            this.lblStudMsg.TabIndex = 13;
            this.lblStudMsg.Text = "labelX1";
            // 
            // AddTransStudSemsSubjScore
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(544, 242);
            this.Controls.Add(this.lblStudMsg);
            this.Controls.Add(this.chkNextNo);
            this.Controls.Add(this.chkNextYes);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnView);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MaximumSize = new System.Drawing.Size(552, 276);
            this.MinimumSize = new System.Drawing.Size(552, 276);
            this.Name = "AddTransStudSemsSubjScore";
            this.Text = "學期科目、領域成績";
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.Controls.ListViewEx listView;
        private System.Windows.Forms.ColumnHeader chSchoolYear;
        private System.Windows.Forms.ColumnHeader chSemester;
        private System.Windows.Forms.PictureBox picLoading;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.ButtonX btnView;
        private DevComponents.DotNetBar.ButtonX btnNext;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkNextYes;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkNextNo;
        private DevComponents.DotNetBar.LabelX lblStudMsg;
    }
}