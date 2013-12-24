using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using JHPermrec.UpdateRecord.UpdateRecordItemControls;
using FCode = Framework.Security.FeatureCodeAttribute;

namespace JHPermrec.UpdateRecord
{
    [FCode("JHSchool.Student.Detail0081", "異動資料")]
    public partial class UpdateRecordItemForm : FISCA.Presentation.Controls.BaseForm
    {        
        private DAL.StudUpdateRecordEntity _StudUpdateRecordEntity;
        public enum actMode { 新增, 修改 };

        string RunningID;

        // Log
        public static JHSchool.PermRecLogProcess prlp;

        public void setCbxSelIndex(int Idx)
        {
            cbxSel.SelectedIndex = Idx;
        }
        public UpdateRecordItemForm(actMode mode, DAL.StudUpdateRecordEntity StudUdRecEnty, string _RunningID)
        {
            InitializeComponent();

            if (Framework.User.Acl[GetType()].Editable)
                btnConfirm.Enabled = true;
            else
                btnConfirm.Enabled = false;

            _StudUpdateRecordEntity = StudUdRecEnty;

            prlp = new JHSchool.PermRecLogProcess();

            RunningID = _RunningID;
            // 先將畫面學年度、學期、年級設空
            intSchoolYear.IsEmpty = true;
            intSemester.IsEmpty = true;
            intGradeYear.IsEmpty = true;

            if (mode == actMode.新增)
            {
                cbxSel.Enabled = true;

                foreach (DAL.DALTransfer.UpdateType ut in DAL.DALTransfer.CheckCanInputUpdateType)
                    cbxSel.Items.Add(ut.ToString());

                cbxSel.SelectedIndex = 0;
                SetDefaultSchoolYearSemester();

                // 加入 log
                prlp.SetAction("新增");
            }

            if (mode == actMode.修改)
            {
                cbxSel.Enabled = false;
                UpdateRecordEditorPanle.Controls.Clear();
                UpdateRecordEditorPanle.Controls.Add(CreateByUpdateCode());
                
                // 加入 log
                prlp.SetAction("修改");
            }

            // 加入 log
            prlp.SetBeforeSaveText("學年度", intSchoolYear.Text);
            prlp.SetBeforeSaveText("學期", intSemester.Text);

        }

        private void UpdateRecordItemForm_Load(object sender, EventArgs e)
        {

        }

        // 設定畫面上學年度學期
        private void SetLoadUpdateSchoolYearSemester(string SchoolYear, string Semester, string GradeYear)
        {
            if (string.IsNullOrEmpty(SchoolYear) || string.IsNullOrEmpty(Semester))
                SetDefaultSchoolYearSemester();
            else
            {
                int sy, sm, gr;

                if (int.TryParse(SchoolYear, out sy))
                    intSchoolYear.Value = sy;
                else
                    intSchoolYear.IsEmpty = true;

                if (int.TryParse(Semester, out sm))
                    intSemester.Value = sm;
                else
                    intSemester.IsEmpty = true;

                if (int.TryParse(GradeYear, out gr))
                    intGradeYear.Value = gr;
                else
                    intGradeYear.IsEmpty = true;

            }

        }

        public UserControl CreateByUpdateType(string UpdateType)
        {            
            if (UpdateType == "新生")
                return new UpdateRecordInfo01(_StudUpdateRecordEntity);
            else if (UpdateType == "畢業")
                return new UpdateRecordInfo02(_StudUpdateRecordEntity );
            else if (UpdateType == "轉入")
                return new UpdateRecordInfo03(_StudUpdateRecordEntity );
            else if (UpdateType == "轉出")
                return new UpdateRecordInfo04(_StudUpdateRecordEntity );
            else if (UpdateType == "休學")
                return new UpdateRecordInfo05(_StudUpdateRecordEntity );
            else if (UpdateType == "復學")
                return new UpdateRecordInfo06(_StudUpdateRecordEntity );
            else if (UpdateType == "中輟")
                return new UpdateRecordInfo07(_StudUpdateRecordEntity );
            else if (UpdateType == "續讀")
                return new UpdateRecordInfo08(_StudUpdateRecordEntity );
            else if (UpdateType == "更正學籍")
                return new UpdateRecordInfo09(_StudUpdateRecordEntity );
            else
                return null;
        }

        public UserControl CreateByUpdateCode()
        {
            SetLoadUpdateSchoolYearSemester(_StudUpdateRecordEntity.SchoolYear + "", _StudUpdateRecordEntity.Semester + "", _StudUpdateRecordEntity.GetGradeYear());

            if (_StudUpdateRecordEntity.GetUpdateCode() == "1")
            {
                cbxSel.Text = "新生";
                return new UpdateRecordInfo01(_StudUpdateRecordEntity);
            }
            else if (_StudUpdateRecordEntity.GetUpdateCode() == "2")
            {
                cbxSel.Text = "畢業";
                return new UpdateRecordInfo02(_StudUpdateRecordEntity);
            }
            else if (_StudUpdateRecordEntity.GetUpdateCode() == "3")
            {
                cbxSel.Text = "轉入";
                return new UpdateRecordInfo03(_StudUpdateRecordEntity);
            }
            else if (_StudUpdateRecordEntity.GetUpdateCode() == "4")
            {
                cbxSel.Text = "轉出";
                return new UpdateRecordInfo04(_StudUpdateRecordEntity);
            }
            else if (_StudUpdateRecordEntity.GetUpdateCode() == "5")
            {
                cbxSel.Text = "休學";
                return new UpdateRecordInfo05(_StudUpdateRecordEntity);
            }
            else if (_StudUpdateRecordEntity.GetUpdateCode() == "6")
            {
                cbxSel.Text = "復學";
                return new UpdateRecordInfo06(_StudUpdateRecordEntity);
            }
            else if (_StudUpdateRecordEntity.GetUpdateCode() == "7")
            {
                cbxSel.Text = "中輟";
                return new UpdateRecordInfo07(_StudUpdateRecordEntity);
            }
            else if (_StudUpdateRecordEntity.GetUpdateCode() == "8")
            {
                cbxSel.Text = "續讀";
                return new UpdateRecordInfo08(_StudUpdateRecordEntity);
            }
            else if (_StudUpdateRecordEntity.GetUpdateCode() == "9")
            {
                cbxSel.Text = "更正學籍";
                return new UpdateRecordInfo09(_StudUpdateRecordEntity);
            }
            else
                return null;
        }
        private void cbxSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRecordEditorPanle.Controls.Clear();
            UpdateRecordEditorPanle.Controls.Add(CreateByUpdateType(cbxSel.Text));
            
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            IUpdateRecordInfo IU;
            if (UpdateRecordEditorPanle.Controls.Count > 0)
            {
                IU = UpdateRecordEditorPanle.Controls[0] as IUpdateRecordInfo;
                _StudUpdateRecordEntity = IU.GetStudUpdateRecordData();
            }


            // 當新增時才處理
            if (cbxSel.Enabled)
            {
                if (cbxSel.Text == "新生")
                {

                    bool checkSameUpdateCode1 = DAL.DALTransfer.checkStudentSameUpdateCode(RunningID, _StudUpdateRecordEntity, "1");

                    if (checkSameUpdateCode1)
                    {
                        MsgBox.Show("已有1筆新生異動，請刪除後再新增");
                        return;
                    }
                }

                if (cbxSel.Text == "畢業")
                {
                    bool checkSameUpdateCode2 = DAL.DALTransfer.checkStudentSameUpdateCode(RunningID, _StudUpdateRecordEntity, "2");

                    if (checkSameUpdateCode2)
                    {
                        MsgBox.Show("已有1筆畢業異動，請刪除後再新增");
                        return;
                    }
                }
                
                // 檢查同一天是否有相同異動
                bool checkSameUpdateDateAndCode = false;
                checkSameUpdateDateAndCode = DAL.DALTransfer.checkStudentSameUpdateCode(RunningID, _StudUpdateRecordEntity);

                if (checkSameUpdateDateAndCode)
                {
                    if (MsgBox.Show("此異動日期已有相同異動，請問是否新增?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;
                }


            }


            // 儲存學年度學期 年級
            _StudUpdateRecordEntity.SchoolYear = intSchoolYear.Value;
            _StudUpdateRecordEntity.Semester = intSemester.Value;
            _StudUpdateRecordEntity.SetGradeYear(intGradeYear.Text);

            // 儲存異動資料
            DAL.DALTransfer.SetStudUpdateRecordEntity(_StudUpdateRecordEntity);


            // Log                
            string strItemName = "學生姓名:" + JHSchool.Data.JHStudent.SelectByID(RunningID).Name + "," + prlp.GetAction() + ":";
            if (_StudUpdateRecordEntity.checkSameUpdateCode("1"))
                strItemName += "新生異動";
            if (_StudUpdateRecordEntity.checkSameUpdateCode("2"))
                strItemName += "畢業異動";
            if (_StudUpdateRecordEntity.checkSameUpdateCode("3"))
                strItemName += "轉入異動";
            if (_StudUpdateRecordEntity.checkSameUpdateCode("4"))
                strItemName += "轉出異動";
            if (_StudUpdateRecordEntity.checkSameUpdateCode("5"))
                strItemName += "休學異動";
            if (_StudUpdateRecordEntity.checkSameUpdateCode("6"))
                strItemName += "復學異動";
            if (_StudUpdateRecordEntity.checkSameUpdateCode("7"))
                strItemName += "中輟異動";
            if (_StudUpdateRecordEntity.checkSameUpdateCode("8"))
                strItemName += "續讀異動";
            if (_StudUpdateRecordEntity.checkSameUpdateCode("9"))
                strItemName += "更正學籍異動";

            prlp.SetActionBy("學生", strItemName);
            prlp.SaveLog("", ":", "student", RunningID);

            JHSchool.Data.JHStudentRecord studRec = JHSchool.Data.JHStudent.SelectByID(RunningID);
            bool checkUpdateStudStatus = false;

            List<string> tmpList0 = new List<string>();
            List<string> tmpList1 = new List<string>();
            List<string> tmpList2 = new List<string>();
            List<string> tmpList3 = new List<string>();

            foreach (JHSchool.Data.JHStudentRecord stud in JHSchool.Data.JHStudent.SelectAll())
            {
                if (stud.Status == K12.Data.StudentRecord.StudentStatus.一般)
                    tmpList0.Add(stud.IDNumber);

                if (stud.Status == K12.Data.StudentRecord.StudentStatus.畢業或離校)
                    tmpList1.Add(stud.IDNumber);
                if (stud.Status == K12.Data.StudentRecord.StudentStatus.休學)
                    tmpList2.Add(stud.IDNumber);
                if (stud.Status == K12.Data.StudentRecord.StudentStatus.輟學)
                    tmpList3.Add(stud.IDNumber);                
            }

            // 轉出
            if (_StudUpdateRecordEntity.checkSameUpdateCode("4"))
            {
                if (MessageBox.Show("請問是否更改學生狀態成 離校？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (tmpList1.Contains(studRec.IDNumber))
                    {
                        MsgBox.Show("學生狀態 離校 有重複身分證號,請檢查後變更.");
                        return;
                    }
                    studRec.Status = JHSchool.Data.JHStudentRecord.StudentStatus.畢業或離校;
                    checkUpdateStudStatus = true;
                    strItemName += ",更改學生狀態成 離校";
                }
            }

            // 休學
            if (_StudUpdateRecordEntity.checkSameUpdateCode("5"))
            {
                if (MessageBox.Show("請問是否更改學生狀態成 休學？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (tmpList2.Contains(studRec.IDNumber))
                    {
                        MsgBox.Show("學生狀態 休學 有重複身分證號,請檢查後變更.");
                        return;
                    }

                    studRec.Status = JHSchool.Data.JHStudentRecord.StudentStatus.休學;
                    checkUpdateStudStatus = true;
                    strItemName += "更改學生狀態成 休學";
                }
            }

            // 中輟

            if (_StudUpdateRecordEntity.checkSameUpdateCode("7"))
            {
                if (MessageBox.Show("請問是否更改學生狀態成 輟學？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (tmpList3.Contains(studRec.IDNumber))
                    {
                        MsgBox.Show("學生狀態 輟學 有重複身分證號,請檢查後變更.");
                        return;
                    }

                    studRec.Status = JHSchool.Data.JHStudentRecord.StudentStatus.輟學;
                    checkUpdateStudStatus = true;
                    strItemName += "更改學生狀態成 輟學";
                }
            }


            // 處理復學轉成一般
            if (_StudUpdateRecordEntity.checkSameUpdateCode("6"))
            {
                if (MessageBox.Show("請問是否更改學生狀態成 一般？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (tmpList0.Contains(studRec.IDNumber))
                    {
                        MsgBox.Show("學生狀態 一般 有重複身分證號,請檢查後變更.");
                        return;
                    }

                    studRec.Status = JHSchool.Data.JHStudentRecord.StudentStatus.一般;
                    checkUpdateStudStatus = true;
                    strItemName += "更改學生狀態成 一般";
                }
            }



            //  prlp.SaveLog("學生.異動資料", prlp.GetAction(),"學生",RunningID , strItemName);

            if (checkUpdateStudStatus)
            {
                JHSchool.Data.JHStudent.Update(studRec);
                JHSchool.Student.Instance.SyncDataBackground(RunningID);

            }
            JHSchool.Permrec.UpdateRecord.Instance.SyncAllBackground();
            JHSchool.Student.Instance.SyncDataBackground(RunningID);

            prlp = null;

        }

        // 載入系統預設學年度學期
        private void SetDefaultSchoolYearSemester()
        {

            int sy, sm;

            if (int.TryParse(JHSchool.School.DefaultSchoolYear, out sy))
                intSchoolYear.Value = sy;
            else
                intSchoolYear.IsEmpty = true;

            if (int.TryParse(JHSchool.School.DefaultSemester, out sm))
                intSemester.Value = sm;
            else
                intSemester.IsEmpty = true;


            // 取得年級

            JHSchool.Data.JHStudentRecord studRec = JHSchool.Data.JHStudent.SelectByID(RunningID);
            if (studRec.Class != null)
                if (studRec.Class.GradeYear.HasValue)
                    if(intGradeYear.IsEmpty)
                        intGradeYear.Value = studRec.Class.GradeYear.Value;
        }

        private void intSchoolYear_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
