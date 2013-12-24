namespace JHPermrec.UpdateRecord.Wizard
{
    partial class Wizard_UpdateCode09Form_1
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnNext = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.chkName = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkIDNumber = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkBirthday = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkGender = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "請勾選需要更正項目";
            // 
            // btnNext
            // 
            this.btnNext.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNext.Location = new System.Drawing.Point(19, 136);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(70, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "下ㄧ步";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(105, 136);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // chkName
            // 
            this.chkName.BackColor = System.Drawing.Color.Transparent;
            this.chkName.Location = new System.Drawing.Point(19, 32);
            this.chkName.Name = "chkName";
            this.chkName.Size = new System.Drawing.Size(125, 23);
            this.chkName.TabIndex = 3;
            this.chkName.Text = "姓名";
            // 
            // chkIDNumber
            // 
            this.chkIDNumber.BackColor = System.Drawing.Color.Transparent;
            this.chkIDNumber.Location = new System.Drawing.Point(19, 57);
            this.chkIDNumber.Name = "chkIDNumber";
            this.chkIDNumber.Size = new System.Drawing.Size(125, 23);
            this.chkIDNumber.TabIndex = 4;
            this.chkIDNumber.Text = "身分證號";
            // 
            // chkBirthday
            // 
            this.chkBirthday.BackColor = System.Drawing.Color.Transparent;
            this.chkBirthday.Location = new System.Drawing.Point(19, 82);
            this.chkBirthday.Name = "chkBirthday";
            this.chkBirthday.Size = new System.Drawing.Size(125, 23);
            this.chkBirthday.TabIndex = 5;
            this.chkBirthday.Text = "生日";
            // 
            // chkGender
            // 
            this.chkGender.BackColor = System.Drawing.Color.Transparent;
            this.chkGender.Location = new System.Drawing.Point(19, 107);
            this.chkGender.Name = "chkGender";
            this.chkGender.Size = new System.Drawing.Size(125, 23);
            this.chkGender.TabIndex = 6;
            this.chkGender.Text = "性別";
            // 
            // Wizard_UpdateCode09Form_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(185, 170);
            this.Controls.Add(this.chkGender);
            this.Controls.Add(this.chkBirthday);
            this.Controls.Add(this.chkIDNumber);
            this.Controls.Add(this.chkName);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(193, 204);
            this.MinimumSize = new System.Drawing.Size(193, 204);
            this.Name = "Wizard_UpdateCode09Form_1";
            this.Text = "學生更正學籍異動";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX btnNext;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkName;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkIDNumber;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkBirthday;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkGender;
    }
}