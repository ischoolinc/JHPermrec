using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework;

namespace JHPermrec.UpdateRecord.UpdateRecordItemControls
{
    // 新生異動
    public partial class UpdateRecordInfo01 : UserControl,IUpdateRecordInfo 
    {
        private DAL.StudUpdateRecordEntity _StudUpdateRecordEntity;
               
        private void GetUpdateReocrdInfoFromForm()
        {
            _StudUpdateRecordEntity.SetUpdateDate (dtUpdateDate.Text);
            _StudUpdateRecordEntity.SetComment(txtComment.Text);
            _StudUpdateRecordEntity.SetGraduateSchool(txtGraduateSchool.Text);
            _StudUpdateRecordEntity.SetClassName (txtClass.Text);    
            _StudUpdateRecordEntity.SetName (txtName.Text);    
            _StudUpdateRecordEntity.SetIDNumber(txtIDNumber.Text);    
            _StudUpdateRecordEntity.SetStudentNumber(txtStudentNumber.Text);
            _StudUpdateRecordEntity.SetGender(cboGender.Text);
            _StudUpdateRecordEntity.SetBirthday(dtBirthday.Text);    
            _StudUpdateRecordEntity.SetAddress (txtAddress.Text );    
            _StudUpdateRecordEntity.SetADDate (dtADDate.Text);    
            _StudUpdateRecordEntity.SetADNumber (txtADNumber.Text);               
            _StudUpdateRecordEntity.SetUpdateCode ("1");
            _StudUpdateRecordEntity.SetEnrollmentSchoolYear (txtEnrollmentSchoolYear.Text);
                        // 記 log --
            UpdateRecordItemForm.prlp.SetAfterSaveText("異動日期", dtUpdateDate.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("入學年月", txtEnrollmentSchoolYear.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("備註", txtComment.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("畢業國小", txtGraduateSchool.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("班級", txtClass.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("姓名", txtName.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("身分證號", txtIDNumber.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("學號", txtStudentNumber.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("性別", cboGender.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("生日", dtBirthday.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("地址", txtAddress.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("核准日期", dtADDate.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("核准文號", txtADNumber.Text);

        }
            
        private void SetUpdateRecordInfoToForm()
        {

            if (_StudUpdateRecordEntity.GetUpdateDate().HasValue)
                dtUpdateDate.Value = _StudUpdateRecordEntity.GetUpdateDate().Value;
            else
                dtUpdateDate.IsEmpty = true;
            txtComment.Text = _StudUpdateRecordEntity.GetComment();
            txtGraduateSchool.Text = _StudUpdateRecordEntity.GetGraduateSchool();
            txtClass.Text = _StudUpdateRecordEntity.GetClassName();
            txtAddress.Text = _StudUpdateRecordEntity.GetAddress();
            txtName.Text = _StudUpdateRecordEntity.GetName();
            txtIDNumber.Text = _StudUpdateRecordEntity.GetIDNumber();
            txtStudentNumber.Text = _StudUpdateRecordEntity.GetStudentNumber();
            cboGender.Text = _StudUpdateRecordEntity.GetGender();
            if (_StudUpdateRecordEntity.GetBirthday().HasValue)
                dtBirthday.Value = _StudUpdateRecordEntity.GetBirthday().Value;
            else
                dtBirthday.IsEmpty = true;
            if (_StudUpdateRecordEntity.GetADDate().HasValue)
                dtADDate.Value = _StudUpdateRecordEntity.GetADDate().Value;
            else
                dtADDate.IsEmpty = true; 
            
            txtADNumber.Text = _StudUpdateRecordEntity.GetADNumber();
            txtEnrollmentSchoolYear.Text = _StudUpdateRecordEntity.GetEnrollmentSchoolYear();

            // 記 log --
            UpdateRecordItemForm.prlp.SetBeforeSaveText("異動日期", dtUpdateDate.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("入學年月", txtEnrollmentSchoolYear.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("備註", txtComment.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("畢業國小", txtGraduateSchool.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("班級", txtClass.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("姓名", txtName.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("身分證號", txtIDNumber.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("學號", txtStudentNumber.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("性別", cboGender.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("生日", dtBirthday.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("地址", txtAddress.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("核准日期", dtADDate.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("核准文號", txtADNumber.Text);       
        
        }

        public UpdateRecordInfo01(DAL.StudUpdateRecordEntity sure)
        {
            InitializeComponent();
            // 設定男、女值與設定只能選不能修改
            cboGender.Items.Add("男");
            cboGender.Items.Add("女");
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            _StudUpdateRecordEntity = sure;
            SetUpdateRecordInfoToForm();
        }


        private void btnGetSchoolList_Click(object sender, EventArgs e)
        {
            GetJHSchoolNames gjn = new GetJHSchoolNames();
            if (gjn.ShowDialog() == DialogResult.OK)
            {
                txtGraduateSchool.Text = gjn.County + gjn.SchoolName;
                gjn.Close();
            }
        }

        #region IUpdateRecordInfo 成員

        public JHPermrec.UpdateRecord.DAL.StudUpdateRecordEntity GetStudUpdateRecordData()
        {
            GetUpdateReocrdInfoFromForm();
            return _StudUpdateRecordEntity;
        }

        #endregion
    }
}
