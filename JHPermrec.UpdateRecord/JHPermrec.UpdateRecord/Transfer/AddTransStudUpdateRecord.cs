using System;
using System.Windows.Forms;
using JHSchool.Data;
using System.Collections.Generic;

namespace JHPermrec.UpdateRecord.Transfer
{
    // 新增一筆轉入異動，取得學生基本資料後加入一筆轉入異動。

    public partial class AddTransStudUpdateRecord : FISCA.Presentation.Controls.BaseForm 
    {
        DAL.StudUpdateRecordEntity _StudUpdateRecordEntity;

        public AddTransStudUpdateRecord(JHSchool.Data.JHStudentRecord studEntity)
        {
            InitializeComponent();
            // 取得原理：當有同一天轉入異動，帶入同一筆，如果沒有新增一筆。
            bool checkNoTodayUrData=true;

            // 取得 StudUpdateRecordEntityList 
            List<DAL.StudUpdateRecordEntity> sureList = DAL.DALTransfer.GetStudUpdateRecordEntityListByUpdateType(studEntity.ID, JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.轉入);

            foreach (DAL.StudUpdateRecordEntity sure in sureList)
            { 
                if(sure.GetUpdateDate().HasValue )
                    if (sure.GetUpdateDate().Value.ToShortDateString() == DateTime.Now.ToShortDateString())
                    {
                        _StudUpdateRecordEntity = sure;
                        checkNoTodayUrData = false;
                    }           
            }

            if (checkNoTodayUrData)
            {
                _StudUpdateRecordEntity = DAL.DALTransfer.AddStudUpdateRecordEntity(studEntity.ID, JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.轉入, DateTime.Now.ToShortDateString());

                txtClass.Text = _StudUpdateRecordEntity.GetClassName();
                txtName.Text = _StudUpdateRecordEntity.GetName();
                txtStudentNumber.Text = _StudUpdateRecordEntity.GetStudentNumber();
                txtSeatNo.Text = _StudUpdateRecordEntity.GetSeatNo();
                txtIDNumber.Text = _StudUpdateRecordEntity.GetIDNumber();
                cboGender.Text = _StudUpdateRecordEntity.GetGender();
                if (_StudUpdateRecordEntity.GetBirthday().HasValue)
                    dtBirthday.Value = _StudUpdateRecordEntity.GetBirthday().Value;
                else
                    dtBirthday.IsEmpty = true; ;
                dtUpdateDate.Text = DateTime.Now.ToString();
                txtAddress.Text = _StudUpdateRecordEntity.GetAddress();
               // cboUpdateDescription.Items.AddRange(new string[] { "遷居", "安置", "其他" });
            }
            else
            {
                if (_StudUpdateRecordEntity.GetUpdateDate().HasValue)
                    dtUpdateDate.Value = _StudUpdateRecordEntity.GetUpdateDate().Value;
                else
                    dtUpdateDate.IsEmpty = true;

                if (_StudUpdateRecordEntity.GetADDate().HasValue)
                    dtADDate.Value = _StudUpdateRecordEntity.GetADDate().Value;
                else
                    dtADDate.IsEmpty = true;

                txtComment.Text = _StudUpdateRecordEntity.GetComment();
                txtClass.Text = _StudUpdateRecordEntity.GetClassName();
                txtAddress.Text = _StudUpdateRecordEntity.GetAddress();
                txtSeatNo.Text = _StudUpdateRecordEntity.GetSeatNo();
                txtExportSchool.Text = _StudUpdateRecordEntity.GetImportExportSchool();
                if (_StudUpdateRecordEntity.GetLastADDate().HasValue)
                    dtLastADDate.Value = _StudUpdateRecordEntity.GetLastADDate().Value;
                else
                    dtLastADDate.IsEmpty = true;
                txtLastADNumber.Text = _StudUpdateRecordEntity.GetLastADNumber();
                txtName.Text = _StudUpdateRecordEntity.GetName();
                txtIDNumber.Text = _StudUpdateRecordEntity.GetIDNumber();
                txtStudentNumber.Text = _StudUpdateRecordEntity.GetStudentNumber();
                cboGender.Text = _StudUpdateRecordEntity.GetGender();
                if (_StudUpdateRecordEntity.GetBirthday().HasValue)
                    dtBirthday.Value = _StudUpdateRecordEntity.GetBirthday().Value;
                else
                    dtBirthday.IsEmpty = true;

                txtADNumber.Text = _StudUpdateRecordEntity.GetADNumber();
                cboUpdateDescription.Text = _StudUpdateRecordEntity.GetUpdateDescription();
            
            }
            cboUpdateDescription.Items.AddRange(new string[] { "遷居", "安置", "其他" });
            chkNextYes.Checked = true;
            AddTransBackgroundManager.AddTransStudUpdateRecordObj = this;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _StudUpdateRecordEntity.SetUpdateDate(dtUpdateDate.Text);
            _StudUpdateRecordEntity.SetComment (txtComment.Text);
            _StudUpdateRecordEntity.SetClassName (txtClass.Text);
            _StudUpdateRecordEntity.SetName (txtName.Text);
            _StudUpdateRecordEntity.SetIDNumber (txtIDNumber.Text);
            _StudUpdateRecordEntity.SetStudentNumber (txtStudentNumber.Text);
            _StudUpdateRecordEntity.SetGender(cboGender.Text);
            _StudUpdateRecordEntity.SetBirthday ( dtBirthday.Text);
            _StudUpdateRecordEntity.SetAddress(txtAddress.Text);
            _StudUpdateRecordEntity.SetImportExportSchool(txtExportSchool.Text);
            _StudUpdateRecordEntity.SetLastADDate(dtLastADDate.Text);
            _StudUpdateRecordEntity.SetLastADNumber(txtLastADNumber.Text);
            _StudUpdateRecordEntity.SetADDate(dtADDate.Text);
            _StudUpdateRecordEntity.SetADNumber(txtADNumber.Text);
            _StudUpdateRecordEntity.SetUpdateDescription(cboUpdateDescription.Text);
            int intSchoolYear,intSemester;
            int.TryParse(JHSchool.School.DefaultSchoolYear,out intSchoolYear);
            int.TryParse(JHSchool.School.DefaultSemester ,out intSemester );
            _StudUpdateRecordEntity.SchoolYear = intSchoolYear;
            _StudUpdateRecordEntity.Semester = intSemester;
            _StudUpdateRecordEntity.SetUpdateCode("3");

            // 檢查相同學年度學期異動日期
            List<JHUpdateRecordRecord> StudUpRecList = JHUpdateRecord.SelectByStudentID(_StudUpdateRecordEntity.StudentID);
            foreach (JHUpdateRecordRecord urr in StudUpRecList)
            {
                // 轉入
                if (urr.UpdateCode == "3" && urr.SchoolYear == intSchoolYear && urr.Semester == intSemester)
                {
                    DateTime dtA, dtB;
                    DateTime.TryParse(urr.UpdateDate, out dtA);
                    DateTime.TryParse(dtUpdateDate.Text, out dtB);
                    // 相同異動日期
                    if (dtA == dtB)
                    {
                        if (FISCA.Presentation.Controls.MsgBox.Show("已有一筆相同學年度、學期、異動日期的轉入異動，請問是否覆蓋?",MessageBoxButtons.YesNo) == DialogResult.Yes )
                        {
                            JHUpdateRecord.Delete(urr);
                            _StudUpdateRecordEntity.UID = string.Empty;
                        }
                        else                        
                            return;                        
                    }
                }            
            }

            // Save
            DAL.DALTransfer.SetStudUpdateRecordEntity(_StudUpdateRecordEntity);
            
            // log
            JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
            prlp.SaveLog("學生.轉入異動", "新增轉入異動", "新增轉入異動資料，姓名:" + _StudUpdateRecordEntity.GetName() + ",學號:" + _StudUpdateRecordEntity.GetStudentNumber());
            
            this.Close();
        }

        private void btnGetSchoolList_Click(object sender, EventArgs e)
        {
            JHPermrec.UpdateRecord.UpdateRecordItemControls.GetJHSchoolNames gjn = new JHPermrec.UpdateRecord.UpdateRecordItemControls.GetJHSchoolNames();
            if (gjn.ShowDialog() == DialogResult.OK)
            {
                txtExportSchool.Text = gjn.County + gjn.SchoolName;
                gjn.Close();
            }

        }
    }
}
