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
    // 更正學籍
    public partial class UpdateRecordInfo09 : UserControl,IUpdateRecordInfo 
    {
        private DAL.StudUpdateRecordEntity _StudUpdateRecordEntity;
        private void GetUpdateReocrdInfoFromForm()
        {
            _StudUpdateRecordEntity.SetUpdateDate (dtUpdateDate.Text);
            _StudUpdateRecordEntity.SetComment (txtComment.Text);
            _StudUpdateRecordEntity.SetClassName(txtClass.Text);
            _StudUpdateRecordEntity.SetName(txtName.Text);
            _StudUpdateRecordEntity.SetIDNumber(txtIDNumber.Text);
            _StudUpdateRecordEntity.SetStudentNumber(txtStudentNumber.Text);
            _StudUpdateRecordEntity.SetGender(cboGender.Text);
            _StudUpdateRecordEntity.SetBirthday(dtBirthday.Text);
            _StudUpdateRecordEntity.SetNewIDNumber(txtNewIDNumber.Text);
            _StudUpdateRecordEntity.SetNewName(txtNewName.Text);
            _StudUpdateRecordEntity.SetNewBirthday(dtNewBirthday.Text);
            _StudUpdateRecordEntity.SetNewGender(cboNewGender.Text);
            _StudUpdateRecordEntity.SetADDate(dtADDate.Text);
            _StudUpdateRecordEntity.SetADNumber(txtADNumber.Text);
            _StudUpdateRecordEntity.SetLastADDate(dtLastADDate.Text);
            _StudUpdateRecordEntity.SetLastADNumber(txtLastADNumber.Text);
            _StudUpdateRecordEntity.SetSeatNo(txtSeatNo.Text);
            _StudUpdateRecordEntity.SetUpdateCode("9");            

            // 記 log --
            UpdateRecordItemForm.prlp.SetAfterSaveText("異動日期", dtUpdateDate.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("備註", txtComment.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("班級", txtClass.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("姓名", txtName.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("身分證號", txtIDNumber.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("學號", txtStudentNumber.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("性別", cboGender.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("生日", dtBirthday.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("核准日期", dtADDate.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("核准文號", txtADNumber.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("學籍核准日期", dtLastADDate.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("學籍核准文號", txtLastADNumber.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("座號", txtSeatNo.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("新生日", dtNewBirthday.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("新性別", cboNewGender.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("新身分證號", txtNewIDNumber.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("新姓名", txtNewName.Text);

        
        }

        private void SetUpdateRecordInfoToForm()
        {
            if (_StudUpdateRecordEntity.GetUpdateDate().HasValue)
                dtUpdateDate.Value = _StudUpdateRecordEntity.GetUpdateDate().Value;
            else
                dtUpdateDate.IsEmpty = true;

            txtComment.Text = _StudUpdateRecordEntity.GetComment();
            txtClass.Text = _StudUpdateRecordEntity.GetClassName();
            txtSeatNo.Text = _StudUpdateRecordEntity.GetSeatNo();
            txtStudentNumber.Text = _StudUpdateRecordEntity.GetStudentNumber();

            if (_StudUpdateRecordEntity.GetLastADDate().HasValue)
                dtLastADDate.Value = _StudUpdateRecordEntity.GetLastADDate().Value;
            else
                dtLastADDate.IsEmpty = true;

            txtLastADNumber.Text = _StudUpdateRecordEntity.GetLastADNumber();
            if (_StudUpdateRecordEntity.GetADDate().HasValue)
                dtADDate.Value = _StudUpdateRecordEntity.GetADDate().Value;
            else
                dtADDate.IsEmpty = true;
            txtADNumber.Text = _StudUpdateRecordEntity.GetADNumber();
            txtName.Text = _StudUpdateRecordEntity.GetName();
            txtIDNumber.Text = _StudUpdateRecordEntity.GetIDNumber();
            cboGender.Text = _StudUpdateRecordEntity.GetGender();

            if (_StudUpdateRecordEntity.GetBirthday().HasValue)
                dtBirthday.Value = _StudUpdateRecordEntity.GetBirthday().Value;
            else
                dtBirthday.IsEmpty = true;

            txtNewIDNumber.Text = _StudUpdateRecordEntity.GetNewIDNumber();
            txtNewName.Text = _StudUpdateRecordEntity.GetNewName();

            if (_StudUpdateRecordEntity.GetNewBirthday().HasValue)
                dtNewBirthday.Value = _StudUpdateRecordEntity.GetNewBirthday().Value;
            else
                dtNewBirthday.IsEmpty = true;
            
            cboNewGender.Text = _StudUpdateRecordEntity.GetNewGender();

            // 記 log --
            UpdateRecordItemForm.prlp.SetBeforeSaveText("異動日期", dtUpdateDate.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("備註", txtComment.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("班級", txtClass.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("姓名", txtName.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("身分證號", txtIDNumber.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("學號", txtStudentNumber.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("性別", cboGender.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("生日", dtBirthday.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("核准日期", dtADDate.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("核准文號", txtADNumber.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("學籍核准日期", dtLastADDate.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("學籍核准文號", txtLastADNumber.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("座號", txtSeatNo.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("新生日", dtNewBirthday.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("新性別", cboNewGender.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("新身分證號", txtNewIDNumber.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("新姓名", txtNewName.Text);
        
        }

        public UpdateRecordInfo09(DAL.StudUpdateRecordEntity sure)
        {
            InitializeComponent();

            // 設定男、女值與設定只能選不能修改
            cboGender.Items.Add("男");
            cboGender.Items.Add("女");
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;

            cboNewGender.Items.Add("");
            cboNewGender.Items.Add("男");
            cboNewGender.Items.Add("女");
            cboNewGender.DropDownStyle = ComboBoxStyle.DropDownList;
            
            _StudUpdateRecordEntity = sure;
            SetUpdateRecordInfoToForm();
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
