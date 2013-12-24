namespace JHPermrec.UpdateRecord.Batch
{
    partial class BatchStudGraduateUpdateRec_Warning
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
            this.lblMsg = new DevComponents.DotNetBar.LabelX();
            this.btnExportXls = new DevComponents.DotNetBar.ButtonX();
            this.btnWriteData = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMsg.Location = new System.Drawing.Point(22, 6);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(211, 29);
            this.lblMsg.TabIndex = 0;
            this.lblMsg.Text = "labelX1";
            // 
            // btnExportXls
            // 
            this.btnExportXls.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExportXls.BackColor = System.Drawing.Color.Transparent;
            this.btnExportXls.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExportXls.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnExportXls.Location = new System.Drawing.Point(22, 72);
            this.btnExportXls.Name = "btnExportXls";
            this.btnExportXls.Size = new System.Drawing.Size(159, 27);
            this.btnExportXls.TabIndex = 1;
            this.btnExportXls.Text = "檢視已有學生異動清單";
            this.btnExportXls.Click += new System.EventHandler(this.btnExportXls_Click);
            // 
            // btnWriteData
            // 
            this.btnWriteData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnWriteData.BackColor = System.Drawing.Color.Transparent;
            this.btnWriteData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnWriteData.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnWriteData.Location = new System.Drawing.Point(22, 106);
            this.btnWriteData.Name = "btnWriteData";
            this.btnWriteData.Size = new System.Drawing.Size(75, 23);
            this.btnWriteData.TabIndex = 2;
            this.btnWriteData.Text = "覆蓋";
            this.btnWriteData.Click += new System.EventHandler(this.btnWriteData_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCancel.Location = new System.Drawing.Point(106, 106);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX2.Location = new System.Drawing.Point(22, 39);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(211, 23);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "是否要覆蓋?";
            // 
            // BatchStudGraduateUpdateRec_Warning
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(200, 140);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnWriteData);
            this.Controls.Add(this.btnExportXls);
            this.Controls.Add(this.lblMsg);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MaximumSize = new System.Drawing.Size(262, 190);
            this.Name = "BatchStudGraduateUpdateRec_Warning";
            this.Text = "提示訊息";
            this.Load += new System.EventHandler(this.BatchStudGraduateUpdateRec_Warning_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblMsg;
        private DevComponents.DotNetBar.ButtonX btnExportXls;
        private DevComponents.DotNetBar.ButtonX btnWriteData;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}