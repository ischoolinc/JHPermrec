using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Framework;

namespace JHPermrec.UpdateRecord.Batch
{
    public partial class BatchAddNewStudUpdateRecord : FISCA.Presentation.Controls.BaseForm 
    {
        public enum FormLoadType {學生,教務};
        private EnhancedErrorProvider Errors{ get; set; }
        private BackgroundWorker _ReadDataWork;
        private BackgroundWorker _WriteDataWork;
        private bool _CheckWriteData = false;
        private bool _CheckDelOldData = false;
        private FormLoadType _FLT;
        string _StudentBatchEnrollYearMonth;

        // 取得畫面上所選學生ID
        List<string> StudentIDList;
        
        // 取得學生新生異動資料
        Dictionary<string, List<DAL.StudUpdateRecordEntity>> StudOldUpdateRecsDic;
        List<DAL.StudUpdateRecordEntity> StudOldUpdateRecList;

        // 產生空的新生異動資料
        List<DAL.StudUpdateRecordEntity> StudUpdateRecsList;
        
        Dictionary<string, string> BeforeEnrollmentDic;
        string strEnrrolSchoolYear="";
        public BatchAddNewStudUpdateRecord(FormLoadType FLT )
        {
            InitializeComponent();
            _FLT = FLT;

        }

        /// <summary>
        /// 取消不做任何動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void btnAddData_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dtUpdateDate.Text))
            {
                Errors.SetError(dtUpdateDate, "請輸入異動日期.");
                return;
            }
           

            strEnrrolSchoolYear = UpdateRecordUtil.checkYearAndMonthInput(txtYear, cboMonth, Errors);
            if (strEnrrolSchoolYear == "")
                return;

            
            int intMonth;
            int.TryParse(cboMonth.Text.Trim(), out intMonth);
            if (intMonth < 10)
                _StudentBatchEnrollYearMonth = txtYear.Text.Trim() + "0" + intMonth;
            else
                _StudentBatchEnrollYearMonth = txtYear.Text.Trim() + intMonth;



            

            // 取得學生 ID 方式
            // 取得畫面上所選學生ID
            if (_FLT == FormLoadType.學生)
                StudentIDList =DAL.DALTransfer.GetStudentTypeIDFromIDs( K12.Presentation.NLDPanels.Student.SelectedSource,K12.Data.StudentRecord.StudentStatus.一般);
            else
                StudentIDList = DAL.DALTransfer.GetStudentIDListByGradeYear(cboGradeYear.Text);

            if (StudentIDList.Count == 0)
                return;
            else
                btnAddData.Enabled = false;

            // 讀取資料
            _ReadDataWork = new BackgroundWorker();
            _ReadDataWork.DoWork += new DoWorkEventHandler(_ReadDataWork_DoWork);
            _ReadDataWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_ReadDataWork_RunWorkerCompleted);
            _ReadDataWork.RunWorkerAsync();

        }

        void _ReadDataWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {           

            // 當已有新生異動
            if (StudOldUpdateRecList.Count > 0)
            {
                this.Visible = false;
                BatchAddNewStudUpdateRecord_Warning bansur = new BatchAddNewStudUpdateRecord_Warning();
                bansur.chkStudUpdateRecords = StudOldUpdateRecList;
                bansur.DataCount = StudOldUpdateRecsDic.Keys.Count;
                bansur.StartPosition = FormStartPosition.CenterScreen;
                bansur.ShowDialog();
                _CheckDelOldData = bansur.chkWriteData;
                _CheckWriteData = bansur.chkWriteData;
            }
            else
                _CheckWriteData = true;            

            if (_CheckWriteData)
            {
                _WriteDataWork = new BackgroundWorker();
                _WriteDataWork.DoWork += new DoWorkEventHandler(_WriteDataWork_DoWork);
                _WriteDataWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_WriteDataWork_RunWorkerCompleted);
                _WriteDataWork.RunWorkerAsync();                
            }

            btnAddData.Enabled = true;
            
        }

        void _WriteDataWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
            prlp.SaveLog("學籍.批次新生異動", "批次新增新生異動", "批次新增" + StudUpdateRecsList.Count + "筆資料.");
            MsgBox.Show("新增批次新生異動完成.");
            btnAddData.Enabled = true;
            this.Close();

        }

        void _WriteDataWork_DoWork(object sender, DoWorkEventArgs e)
        {
            // 執行寫入

            // 刪除舊資料
            if(_CheckDelOldData)
                DAL.DALTransfer.DelStudUpdateRecordEntityList(StudOldUpdateRecList);

            int SchoolYear, Semester;
            int.TryParse(JHSchool.School.DefaultSchoolYear, out SchoolYear);
            int.TryParse(JHSchool.School.DefaultSemester, out Semester);
                

            // 寫入資料
            foreach (DAL.StudUpdateRecordEntity sure in StudUpdateRecsList )
            {
                sure.SetEnrollmentSchoolYear(_StudentBatchEnrollYearMonth);
                sure.SchoolYear = SchoolYear;
                sure.Semester = Semester;
            }
            DAL.DALTransfer.SetStudUpdateRecordEntityList(StudUpdateRecsList);
            
        }

        void _ReadDataWork_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得學生新生異動資料
            StudOldUpdateRecsDic = DAL.DALTransfer.GetStudListUpdateRecordEntityListByUpdateType(StudentIDList, JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.新生);

            StudOldUpdateRecList = new List<JHPermrec.UpdateRecord.DAL.StudUpdateRecordEntity>();
            foreach (List<DAL.StudUpdateRecordEntity> sureList in StudOldUpdateRecsDic.Values)
                StudOldUpdateRecList.AddRange(sureList);


            // 產生空的新生異動資料
            StudUpdateRecsList = DAL.DALTransfer.AddStudUpdateRecordEntityList(StudentIDList, JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.新生,dtUpdateDate.Text);

        }


        private void dtUpdateDate_TextChanged(object sender, EventArgs e)
        {
            if (Errors == null)
                Errors = new EnhancedErrorProvider();
            Errors.Clear();
        }

        private void BatchAddNewStudUpdateRecord_Load(object sender, EventArgs e)
        {
            dtUpdateDate.Text = DateTime.Now.ToShortDateString();
            dtUpdateDate.Select();
            if (Errors == null)
                Errors = new EnhancedErrorProvider();

            // 載入預設年度與月份
            UpdateRecordUtil.LoadYearAndMonth(txtYear, cboMonth);
            StudentIDList = new List<string>();
            StudOldUpdateRecsDic = new Dictionary<string, List<JHPermrec.UpdateRecord.DAL.StudUpdateRecordEntity>>();
            StudOldUpdateRecList = new List<JHPermrec.UpdateRecord.DAL.StudUpdateRecordEntity>();
            StudUpdateRecsList = new List<JHPermrec.UpdateRecord.DAL.StudUpdateRecordEntity>();

            // Load 上來樣式
            if (_FLT == FormLoadType.學生)
            {
                labelX2.Location = new Point(12, 41);
                dtUpdateDate.Location = new Point(74, 42);
                labelX3.Location = new Point(11, 73);
                txtYear.Location = new Point(73, 73);
                labelX5.Location = new Point(136, 73);
                cboMonth.Location = new Point(169, 71);
                btnAddData.Location = new Point(55, 106);
                btnExit.Location = new Point(150, 106);
                this.MaximumSize = new System.Drawing.Size(242, 173);
                this.Size = new System.Drawing.Size(242, 173);
                labelX4.Visible = false;
                cboGradeYear.Visible = false;

            }
            else
            {
                labelX4.Visible = true;
                cboGradeYear.Visible = true;
                labelX4.Location = new Point(36, 42);
                cboGradeYear.Location = new Point(75, 42);

                labelX2.Location = new Point(13, 73);
                dtUpdateDate.Location = new Point(75, 73);
                labelX3.Location = new Point(13, 108);
                txtYear.Location = new Point(75, 106);
                labelX5.Location = new Point(138, 108);
                btnAddData.Location = new Point(71, 141);
                btnExit.Location = new Point(150, 141);
                cboMonth.Location = new Point(169, 106);
                this.MaximumSize = new System.Drawing.Size(252, 203);
                this.Size = new System.Drawing.Size(252, 203);

                cboGradeYear.Items.Clear();
                foreach (int gr in DAL.DALTransfer.GetClassGardeYearList())
                    cboGradeYear.Items.Add(gr.ToString());
            }
            this.StartPosition = FormStartPosition.CenterScreen;
        }

    }
}
