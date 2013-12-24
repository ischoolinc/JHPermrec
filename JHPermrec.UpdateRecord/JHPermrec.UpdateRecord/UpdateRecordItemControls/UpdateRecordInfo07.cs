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
    // 中輟異動
    public partial class UpdateRecordInfo07 : UserControl,IUpdateRecordInfo 
    {
        private DAL.StudUpdateRecordEntity _StudUpdateRecordEntity;
        private void GetUpdateReocrdInfoFromForm()
        {
            _StudUpdateRecordEntity.SetUpdateDate(dtUpdateDate.Text);
            _StudUpdateRecordEntity.SetComment (txtComment.Text);
            _StudUpdateRecordEntity.SetClassName (txtClass.Text);
            _StudUpdateRecordEntity.SetName (txtName.Text);
            _StudUpdateRecordEntity.SetIDNumber(txtIDNumber.Text);
            _StudUpdateRecordEntity.SetStudentNumber(txtStudentNumber.Text);
            _StudUpdateRecordEntity.SetGender(cboGender.Text);
            _StudUpdateRecordEntity.SetBirthday(dtBirthday.Text);
            _StudUpdateRecordEntity.SetAddress(txtAddress.Text);
            _StudUpdateRecordEntity.SetADDate(dtADDate.Text);
            _StudUpdateRecordEntity.SetADNumber(txtADNumber.Text);
            _StudUpdateRecordEntity.SetUpdateDescription(cboUpdateDescription.Text);
            _StudUpdateRecordEntity.SetImportExportSchool (txtImportSchool.Text);
            _StudUpdateRecordEntity.SetLastADDate(dtLastADDate.Text);
            _StudUpdateRecordEntity.SetLastADNumber (txtLastADNumber.Text);
            _StudUpdateRecordEntity.SetSeatNo (txtSeatNo.Text);
            _StudUpdateRecordEntity.SetUpdateCode("7");
            
            // 記 log --
            UpdateRecordItemForm.prlp.SetAfterSaveText("異動日期", dtUpdateDate.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("備註", txtComment.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("班級", txtClass.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("姓名", txtName.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("身分證號", txtIDNumber.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("學號", txtStudentNumber.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("性別", cboGender.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("生日", dtBirthday.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("地址", txtAddress.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("核准日期", dtADDate.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("核准文號", txtADNumber.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("異動原因", cboUpdateDescription.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("學籍核准日期", dtLastADDate.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("學籍核准文號", txtLastADNumber.Text);
            UpdateRecordItemForm.prlp.SetAfterSaveText("座號", txtSeatNo.Text);        
        }

        private void SetUpdateRecordInfoToForm()
        {

            if (_StudUpdateRecordEntity.GetUpdateDate().HasValue)
                dtUpdateDate.Value = _StudUpdateRecordEntity.GetUpdateDate().Value;
            else
                dtUpdateDate.IsEmpty = true;

            txtComment.Text = _StudUpdateRecordEntity.GetComment();
            txtClass.Text = _StudUpdateRecordEntity.GetClassName();
            txtAddress.Text = _StudUpdateRecordEntity.GetAddress();
            txtSeatNo.Text = _StudUpdateRecordEntity.GetSeatNo();
            txtName.Text = _StudUpdateRecordEntity.GetName();
            txtIDNumber.Text = _StudUpdateRecordEntity.GetIDNumber();
            txtStudentNumber.Text = _StudUpdateRecordEntity.GetStudentNumber();
            cboGender.Text = _StudUpdateRecordEntity.GetGender();
            if (_StudUpdateRecordEntity.GetBirthday().HasValue)
                dtBirthday.Value = _StudUpdateRecordEntity.GetBirthday().Value;
            else
                dtBirthday.IsEmpty = true;

            txtImportSchool.Text = _StudUpdateRecordEntity.GetImportExportSchool();

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
            cboUpdateDescription.Text = _StudUpdateRecordEntity.GetUpdateDescription();

            // 記 log --
            UpdateRecordItemForm.prlp.SetBeforeSaveText("異動日期", dtUpdateDate.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("備註", txtComment.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("班級", txtClass.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("姓名", txtName.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("身分證號", txtIDNumber.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("學號", txtStudentNumber.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("性別", cboGender.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("生日", dtBirthday.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("地址", txtAddress.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("核准日期", dtADDate.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("核准文號", txtADNumber.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("異動原因", cboUpdateDescription.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("學籍核准日期", dtLastADDate.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("學籍核准文號", txtLastADNumber.Text);
            UpdateRecordItemForm.prlp.SetBeforeSaveText("座號", txtSeatNo.Text);
        
        }
        
        // 檢查表單中是否有不正確的資料。
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            Errors.Clear();
            if (string.IsNullOrEmpty(dtUpdateDate.Text))
            {
                Errors.SetError(dtUpdateDate, "異動日期不可以空白。");
            }

            return !Errors.HasError; //沒有(有錯誤)。
        }

        private EnhancedErrorProvider Errors { get; set; }
        ConfigData cd;          //異動原因的組態物件
        private string updateConfigKey = "UpdateReasons";   //紀錄異動原因清單的組態值的Key
        private string ExtendingConfigKey = "ExtendingReason";    //紀錄中輟原因清單的組態值的Key
        //將中輟紀錄清單的key輸入至list cd中


        public UpdateRecordInfo07(DAL.StudUpdateRecordEntity sure)
        {
            InitializeComponent();
            Errors = new EnhancedErrorProvider();
            // 設定男、女值與設定只能選不能修改
            cboGender.Items.Add("男");
            cboGender.Items.Add("女");
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cd = JHSchool.School.Configuration[updateConfigKey];
            if (!cd.Contains(ExtendingConfigKey))
            {
                cd[ExtendingConfigKey] = "出國;行蹤不明;其他";
                cd.Save();

            }
            string[] reasons = cd[ExtendingConfigKey].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> reasonList = new List<string>(reasons);
            foreach (string reason in reasons)
            {
                if (reason == "死亡")
                    continue;

                this.cboUpdateDescription.Items.Add(reason);
            }
            foreach (string defaultreason in new string[] { "出國", "行蹤不明", "其他" })
            {
                if (!reasonList.Contains(defaultreason))
                    this.cboUpdateDescription.Items.Add(defaultreason);

            }
            _StudUpdateRecordEntity = sure;
            SetUpdateRecordInfoToForm();
        }

        private void btnGetSchoolList_Click(object sender, EventArgs e)
        {
            GetJHSchoolNames gjn = new GetJHSchoolNames();
            if (gjn.ShowDialog() == DialogResult.OK)
            {
                txtImportSchool.Text = gjn.County + gjn.SchoolName;
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
