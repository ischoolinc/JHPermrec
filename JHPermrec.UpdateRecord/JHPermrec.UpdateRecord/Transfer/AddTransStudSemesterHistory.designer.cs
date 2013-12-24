namespace JHPermrec.UpdateRecord.Transfer
{
    partial class AddTransStudSemesterHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgSemestrHistory = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.學年度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.學期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.年級 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.班級 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.座號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.班導師 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.上課天數 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgSemestrHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgSemestrHistory
            // 
            this.dgSemestrHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgSemestrHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgSemestrHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.學年度,
            this.學期,
            this.年級,
            this.班級,
            this.座號,
            this.班導師,
            this.上課天數});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSemestrHistory.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgSemestrHistory.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgSemestrHistory.HighlightSelectedColumnHeaders = false;
            this.dgSemestrHistory.Location = new System.Drawing.Point(8, 12);
            this.dgSemestrHistory.Name = "dgSemestrHistory";
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dgSemestrHistory.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgSemestrHistory.RowTemplate.Height = 24;
            this.dgSemestrHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSemestrHistory.Size = new System.Drawing.Size(572, 167);
            this.dgSemestrHistory.TabIndex = 1;
            // 
            // 學年度
            // 
            this.學年度.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.學年度.HeaderText = "學年度";
            this.學年度.Name = "學年度";
            // 
            // 學期
            // 
            this.學期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.學期.HeaderText = "學期";
            this.學期.Name = "學期";
            // 
            // 年級
            // 
            this.年級.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.年級.HeaderText = "年級";
            this.年級.Name = "年級";
            // 
            // 班級
            // 
            this.班級.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.班級.HeaderText = "班級";
            this.班級.Name = "班級";
            // 
            // 座號
            // 
            this.座號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.座號.HeaderText = "座號";
            this.座號.Name = "座號";
            // 
            // 班導師
            // 
            this.班導師.HeaderText = "班導師";
            this.班導師.Name = "班導師";
            // 
            // 上課天數
            // 
            this.上課天數.HeaderText = "上課天數";
            this.上課天數.Name = "上課天數";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(509, 189);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AddTransStudSemesterHistory
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(592, 218);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgSemestrHistory);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MaximumSize = new System.Drawing.Size(600, 252);
            this.MinimumSize = new System.Drawing.Size(600, 252);
            this.Name = "AddTransStudSemesterHistory";
            this.Text = "學期歷程";
            ((System.ComponentModel.ISupportInitialize)(this.dgSemestrHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgSemestrHistory;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn 學年度;
        private System.Windows.Forms.DataGridViewTextBoxColumn 學期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 年級;
        private System.Windows.Forms.DataGridViewTextBoxColumn 班級;
        private System.Windows.Forms.DataGridViewTextBoxColumn 座號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 班導師;
        private System.Windows.Forms.DataGridViewTextBoxColumn 上課天數;
    }
}