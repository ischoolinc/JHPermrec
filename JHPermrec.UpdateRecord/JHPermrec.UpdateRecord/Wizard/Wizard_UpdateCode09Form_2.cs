using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JHPermrec.UpdateRecord.Wizard
{
    public partial class Wizard_UpdateCode09Form_2 : FISCA.Presentation.Controls.BaseForm 
    {
        public bool isNameEnable { get; set; }
        public bool isIDNumberEnable { get; set; }
        public bool isBirthdayEnable { get; set; }
        public bool isGenderEnable { get; set; }
        public string _StudentID { get; set; }

        public Wizard_UpdateCode09Form_2()
        {
            InitializeComponent();
        }

        private void Wizard_UpdateCode09_2_Load(object sender, EventArgs e)
        {
            // 載入可修改
            txtNewName.Enabled = isNameEnable;
            txtNewIDNumber.Enabled = isIDNumberEnable;
            dtNewBirthday.Enabled = isBirthdayEnable;
            cboNewGender.Enabled = isGenderEnable;
            cboNewGender.Items.Add("男");
            cboNewGender.Items.Add("女");
            cboNewGender.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // 驗證身份證號
            if(!string.IsNullOrEmpty(txtNewIDNumber.Text ))
            if (StudCheckTool.CheckStudIDNumberSame(txtNewIDNumber.Text, _StudentID))
            {
                FISCA.Presentation.Controls.MsgBox.Show("身分證號重覆請檢查.");
                return;
            }
            // 呼叫異動並帶入相對資料
            DAL.StudUpdateRecordEntity sure = DAL.DALTransfer2.AddStudUpdateRecordEntity(_StudentID, JHPermrec.UpdateRecord.DAL.DALTransfer2.UpdateType.更正學籍, DateTime.Now.ToShortDateString());
            
            if (isNameEnable)
                sure.SetNewName(txtNewName.Text);

            if (isIDNumberEnable)
                sure.SetNewIDNumber(txtNewIDNumber.Text);

            if (isBirthdayEnable)
                sure.SetNewBirthday(dtNewBirthday.Text);

            if (isGenderEnable)
                sure.SetNewGender(cboNewGender.Text);


            if (sure == null)
                return;

            
            UpdateRecordItemForm form = new UpdateRecordItemForm(UpdateRecordItemForm.actMode.修改, sure, _StudentID);            
            form.StartPosition = FormStartPosition.CenterParent;
            this.Hide();
            this.Close();
            this.Location = new Point(-100, -100);
            form.ShowDialog(FISCA.Presentation.MotherForm.Form);
            

            // 當使用這按確定
            if (form.DialogResult == DialogResult.OK)
            {
                // Log
                JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
                prlp.SetBeforeSaveText("姓名", sure.GetName());
                prlp.SetBeforeSaveText("身分證號", sure.GetIDNumber());

                if (sure.GetBirthday().HasValue)
                    prlp.SetBeforeSaveText("生日", sure.GetBirthday().Value.ToShortDateString());

                prlp.SetBeforeSaveText("性別", sure.GetGender());

                JHSchool.Data.JHStudentRecord studRec = JHSchool.Data.JHStudent.SelectByID(_StudentID);

                // 更正資料
                if (isNameEnable && string.IsNullOrEmpty(sure.GetNewName()) == false)
                {
                    studRec.Name = sure.GetNewName();
                }

                if (isIDNumberEnable && string.IsNullOrEmpty(sure.GetNewIDNumber()) == false)
                {
                    studRec.IDNumber = sure.GetNewIDNumber();
                }

                if (isBirthdayEnable && sure.GetNewBirthday().HasValue)
                {
                    studRec.Birthday = sure.GetNewBirthday();
                }

                if (isGenderEnable && string.IsNullOrEmpty(sure.GetNewGender()) == false)
                {
                    studRec.Gender = sure.GetNewGender();
                }

                if (isNameEnable == true || isIDNumberEnable == true || isBirthdayEnable == true || isGenderEnable == true)
                {
                    JHSchool.Data.JHStudent.Update(studRec);
                    JHSchool.Student.Instance.SyncAllBackground();
                }
                prlp.SetAfterSaveText("姓名", studRec.Name);
                prlp.SetAfterSaveText("身分證號", studRec.IDNumber);
                if (studRec.Birthday.HasValue)
                    prlp.SetAfterSaveText("生日", studRec.Birthday.Value.ToShortDateString());

                prlp.SetAfterSaveText("性別", studRec.Gender);

                prlp.SetActionBy("學生學籍", "學生更正學籍功能");
                prlp.SetDescTitle("學生姓名：" + sure.GetName() + ",更改資料：");
                prlp.SaveLog("", "", "student", _StudentID);
                this.Close();
            }
            //else
                //form.Dispose();
            
        }
    }
}
