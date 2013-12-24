using System.Windows.Forms;

namespace JHPermrec.UpdateRecord.UpdateRecordItemControls
{
    // 畢業異動
    public partial class UpdateRecordInfo02 : UserControl,IUpdateRecordInfo 
    {
        private DAL.StudUpdateRecordEntity _StudUpdateRecordEntity;

        private void GetUpdateReocrdInfoFromForm()
        {
            _StudUpdateRecordEntity.SetUpdateDate(dtUpdateDate.Text);
            _StudUpdateRecordEntity.SetComment(txtComment.Text);
            _StudUpdateRecordEntity.SetGraduateCertificateNumber(txtGraduateCertificateNumber.Text);
            _StudUpdateRecordEntity.SetClassName(txtClass.Text);
            _StudUpdateRecordEntity.SetName(txtName.Text);
            _StudUpdateRecordEntity.SetIDNumber(txtIDNumber.Text);
            _StudUpdateRecordEntity.SetStudentNumber(txtStudentNumber.Text);
            _StudUpdateRecordEntity.SetGender(cboGender.Text);
            _StudUpdateRecordEntity.SetBirthday(dtBirthday.Text);
            _StudUpdateRecordEntity.SetADDate(dtADDate.Text);
            _StudUpdateRecordEntity.SetADNumber(txtADNumber.Text);
            _StudUpdateRecordEntity.SetUpdateCode("2");
            _StudUpdateRecordEntity.SetEnrollmentSchoolYear(txtEnrollmentSchoolYear.Text);
            _StudUpdateRecordEntity.SetGraduateSchoolYear(txtGraduateSchoolYear.Text);
            _StudUpdateRecordEntity.SetBirthPlace(txtBirthPlace.Text );
            _StudUpdateRecordEntity.SetGraduate(cboGraduate.Text);
            _StudUpdateRecordEntity.SetLastADDate(dtLastADDate.Text);
            _StudUpdateRecordEntity.SetLastADNumber(txtLastADNumber.Text);

                UpdateRecordItemForm.prlp.SetAfterSaveText("異動日期", dtUpdateDate.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("入學年月", txtEnrollmentSchoolYear.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("備註", txtComment.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("班級", txtClass.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("姓名", txtName.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("身分證號", txtIDNumber.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("學號", txtStudentNumber.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("性別", cboGender.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("生日", dtBirthday.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("核准日期", dtADDate.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("核准文號", txtADNumber.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("畢業年月", txtGraduateSchoolYear.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("畢修業別", cboGraduate.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("出生地", txtBirthPlace.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("學籍核准日期", dtLastADDate.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("學籍核准文號", txtLastADNumber.Text);
                UpdateRecordItemForm.prlp.SetAfterSaveText("畢業證書字號", txtGraduateCertificateNumber.Text);

        
        }

        private void SetUpdateRecordInfoToForm()
        {

            if (_StudUpdateRecordEntity.GetUpdateDate().HasValue)
                dtUpdateDate.Value = _StudUpdateRecordEntity.GetUpdateDate().Value;
            else
                dtUpdateDate.IsEmpty = true; txtComment.Text = _StudUpdateRecordEntity.GetComment();
            txtGraduateCertificateNumber.Text = _StudUpdateRecordEntity.GetGraduateCertificateNumber();
            txtClass.Text = _StudUpdateRecordEntity.GetClassName();
            txtName.Text = _StudUpdateRecordEntity.GetName();
            txtIDNumber.Text = _StudUpdateRecordEntity.GetIDNumber();
            txtStudentNumber.Text = _StudUpdateRecordEntity.GetStudentNumber();
            cboGender.Text = _StudUpdateRecordEntity.GetGender();

            if (_StudUpdateRecordEntity.GetBirthday().HasValue)
                dtBirthday.Value = _StudUpdateRecordEntity.GetBirthday().Value;
            else
                dtBirthday.IsEmpty = true;

            if (_StudUpdateRecordEntity.GetLastADDate().HasValue)
                dtLastADDate.Value = _StudUpdateRecordEntity.GetLastADDate().Value;
            else
                dtLastADDate.IsEmpty = true;

            txtADNumber.Text = _StudUpdateRecordEntity.GetADNumber();
            txtEnrollmentSchoolYear.Text = _StudUpdateRecordEntity.GetEnrollmentSchoolYear();
            txtGraduateSchoolYear.Text = _StudUpdateRecordEntity.GetGraduateSchoolYear();
            txtBirthPlace.Text = _StudUpdateRecordEntity.GetBirthPlace();
            cboGraduate.Text = _StudUpdateRecordEntity.GetGraduate();

            if (_StudUpdateRecordEntity.GetADDate().HasValue)
                dtADDate.Value = _StudUpdateRecordEntity.GetADDate().Value;
            else
                dtADDate.IsEmpty = true; 

            txtLastADNumber.Text = _StudUpdateRecordEntity.GetLastADNumber();

                // 記 log --
                UpdateRecordItemForm.prlp.SetBeforeSaveText("異動日期", dtUpdateDate.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("入學年月", txtEnrollmentSchoolYear.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("備註", txtComment.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("班級", txtClass.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("姓名", txtName.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("身分證號", txtIDNumber.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("學號", txtStudentNumber.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("性別", cboGender.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("生日", dtBirthday.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("核准日期", dtADDate.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("核准文號", txtADNumber.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("畢業年月",txtGraduateSchoolYear.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("畢修業別", cboGraduate.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("出生地", txtBirthPlace.Text);
                UpdateRecordItemForm.prlp.SetBeforeSaveText("學籍核准日期", dtLastADDate.Text );
                UpdateRecordItemForm.prlp.SetBeforeSaveText("學籍核准文號", txtLastADNumber.Text );
                UpdateRecordItemForm.prlp.SetBeforeSaveText("畢業證書字號", txtGraduateCertificateNumber.Text);           
        
        }

        public UpdateRecordInfo02(DAL.StudUpdateRecordEntity sure)
        {
            InitializeComponent();
            // 設定男、女值與設定只能選不能修改
            cboGender.Items.Add("男");
            cboGender.Items.Add("女");
            cboGraduate.Items.Add("畢業");
            cboGraduate.Items.Add("修業");
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
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
