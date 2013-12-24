using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using FISCA.DSAUtil;
using Framework;
using JHSchool.Permrec.Feature.Legacy;
using JHSchool.Data;

namespace JHPermrec.UpdateRecord.UpdateRecordViewForm
{
    public partial class UpdateRecordViewForm : FISCA.Presentation.Controls.BaseForm 
    {
        //private DateTime _startDate;
        //private DateTime _endDate;
        private BackgroundWorker _loader;
        private List<CheckBox> _CheckDit;
        private Dictionary<string, string> _dicUpdateCode;
        private List<string> _studentsList;
        private List<string> _setStudent;
        private List<JHUpdateRecordRecord> _StudUpdateRecList;

        //private AccessHelper _accessHelper = new AccessHelper();

        //Cache學生集合
        private Dictionary<string,JHStudentRecord> students;

        public UpdateRecordViewForm()
        {
            InitializeComponent();
            _dicUpdateCode = new Dictionary<string, string>();
            _dicUpdateCode.Add("1", "新生");
            _dicUpdateCode.Add("2", "畢業");
            _dicUpdateCode.Add("3", "轉入");
            _dicUpdateCode.Add("4", "轉出");
            _dicUpdateCode.Add("5", "休學");
            _dicUpdateCode.Add("6", "復學");
            _dicUpdateCode.Add("7", "中輟");
            _dicUpdateCode.Add("8", "續讀");
            _dicUpdateCode.Add("9", "更正學籍");
            _CheckDit = new List<CheckBox>();
            CheckForm();
            students = new Dictionary<string, JHStudentRecord>();
            _StudUpdateRecList = new List<JHUpdateRecordRecord>();
            Initialize();
            btnRefresh_Click(null, null);
            
            
        }

        private void Initialize()
        {
            _loader = new BackgroundWorker();
            _loader.DoWork += new DoWorkEventHandler(_loader_DoWork);
            _loader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_loader_RunWorkerCompleted);

            // 設定日期預設值今天
            dtEnd.Value = dtStart.Value = DateTime.Now;

            foreach (JHStudentRecord var in JHStudent.SelectAll ())
            {
                // 過濾刪除學生
                if (var.Status == K12.Data.StudentRecord.StudentStatus.刪除)
                    continue;

                students.Add(var.ID, var);
            }
        }

        private void _loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

                    FillDataGridView();
        }

        private void _loader_DoWork(object sender, DoWorkEventArgs e)
        {
            _StudUpdateRecList.Clear();
            List<string> _UpdateCodeList = new List<string>();
            foreach (CheckBox code in _CheckDit)
                _UpdateCodeList.Add((string)code.Tag);

            foreach (JHUpdateRecordRecord UpdateRec in JHUpdateRecord.SelectAll())
            {
                DateTime UpdateDate;
                if (DateTime.TryParse(UpdateRec.UpdateDate, out UpdateDate))
                {
                    if (UpdateDate >= dtStart.Value.Date && UpdateDate < dtEnd.Value.AddDays(1).Date)
                        if (_UpdateCodeList.Contains(UpdateRec.UpdateCode))
                            //檢查是否存非刪除
                            if(students.ContainsKey(UpdateRec.StudentID ))
                                _StudUpdateRecList.Add(UpdateRec);
                }
            }
        }

        private void FillDataGridView()
        {
            dataGridViewX1.SuspendLayout();

            foreach (JHUpdateRecordRecord UpdateRec in _StudUpdateRecList)
            {
                JHStudentRecord student = new JHStudentRecord();
                
                if(students.ContainsKey(UpdateRec.StudentID)) 
                    student = students[UpdateRec.StudentID];

                string strUpdateCode = "";
                if (_dicUpdateCode.ContainsKey(UpdateRec.UpdateCode))
                    strUpdateCode = _dicUpdateCode[UpdateRec.UpdateCode];

                string SeatNo = string.Empty, ClassName = string.Empty, Gender = string.Empty, Status = string.Empty; 

                if (student.SeatNo.HasValue)
                    SeatNo = student.SeatNo.Value + "";

                if(student.Class !=null )
                    ClassName =student.Class.Name ;

                if (!string.IsNullOrEmpty(student.Gender))
                    Gender = student.Gender;
                
                Status = student.Status.ToString();

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1,
                    UpdateRec.ID,
                    UpdateRec.UpdateDate ,
                    ClassName ,
                    SeatNo,
                    student.StudentNumber, 
                    student.Name,
                    Gender,
                    strUpdateCode,
                    UpdateRec.UpdateDescription ,
                    "",
                    "",
                    UpdateRec.ADNumber,
                    Status 
                );
                row.Tag = UpdateRec.StudentID;
                dataGridViewX1.Rows.Add(row);
            
            }
            dataGridViewX1.ResumeLayout();
        }

        private bool IsDateTime(string date)
        {
            DateTime try_value;
            if (DateTime.TryParse(date, out try_value))
                return true;
            return false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bool valid = true;
            errorProvider1.Clear();
            CheckForm();
            if (_CheckDit.Count <= 0)
            {
                dataGridViewX1.Rows.Clear();
                return;
            }

            if (valid && !_loader.IsBusy)
            {
                dataGridViewX1.Rows.Clear();
                _loader.RunWorkerAsync();
            }
        }

        private void btnTypeFilter_Click(object sender, EventArgs e)
        {
            //_typeForm.ShowDialog();
        }

        private void UpdateRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            XmlElement pref = new XmlDocument().CreateElement("異動資料檢視_異動代碼");
            foreach (CheckBox code in _CheckDit)
            {
                XmlElement codeElement = pref.OwnerDocument.CreateElement("Code");
                codeElement.InnerText = (string)code.Tag;
                pref.AppendChild(codeElement);
            }

            //SmartSchool.Customization.Data.SystemInformation.Preference["異動資料檢視_異動代碼"] = pref;

            ConfigData cd = User.Configuration["異動資料檢視"];
            cd["異動代碼"] = pref.OuterXml;
            cd.Save();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            DataGridViewExport export = new DataGridViewExport(dataGridViewX1);
            export.Save(saveFileDialog1.FileName);

            if (new CompleteForm().ShowDialog() == DialogResult.Yes)
                System.Diagnostics.Process.Start(saveFileDialog1.FileName);
        }

        private void CheckForm()
        {
            _CheckDit.Clear();

            foreach (Control each in groupPanel1.Controls)
            {
                if (each is CheckBox)
                {
                    CheckBox x = each as CheckBox;
                    if (x.Checked)
                        _CheckDit.Add(x);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (_studentsList == null)
                _studentsList = new List<string>();


            _setStudent = GetSelectedStudentList();
            K12.Presentation.NLDPanels.Student.AddToTemp(_setStudent);            

            //將增加的學生記錄下來
            foreach (string var in _setStudent)
            {
                if (!_studentsList.Contains(var))
                    _studentsList.Add(var);
            }
            MsgBox.Show("新增 " + _setStudent.Count + " 名學生於待處理");
            labelX4.Text = "待處理共 " + K12.Presentation.NLDPanels.Student.TempSource.Count + " 名學生";
            labelX4.Visible = true;
            btnClear.Visible = true;
        }

        private List<string> GetSelectedStudentList()
        {
            List<string> _temporallist = new List<string>();
            foreach (DataGridViewRow var in dataGridViewX1.SelectedRows)
            {
                if (!_temporallist.Contains((string)var.Tag))
                {
                    _temporallist.Add("" + var.Tag);
                }
            }
            return _temporallist;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            K12.Presentation.NLDPanels.Student.RemoveFromTemp(_studentsList);            
            MsgBox.Show("已清除所有加入項目");
            labelX4.Visible = false;
            btnClear.Visible = false;
        }

        private void checkBoxX9_CheckedChanged_1(object sender, EventArgs e)
        {
            btnRefresh.Pulse(5);

            foreach (Control each in groupPanel1.Controls)
            {
                if (each is CheckBox)
                {
                    if (each.Text == "全 選 篩 選 條 件") continue;

                    CheckBox x = each as CheckBox;
                    x.Checked = checkBoxX9.Checked;
                }
            }
        }

        #region 顏色

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            btnRefresh.Pulse(5);
        }

        private void checkBoxX2_CheckedChanged(object sender, EventArgs e)
        {
            btnRefresh.Pulse(5);
        }

        private void checkBoxX3_CheckedChanged(object sender, EventArgs e)
        {
            btnRefresh.Pulse(5);
        }

        private void checkBoxX4_CheckedChanged(object sender, EventArgs e)
        {
            btnRefresh.Pulse(5);
        }

        private void checkBoxX5_CheckedChanged(object sender, EventArgs e)
        {
            btnRefresh.Pulse(5);
        }

        private void checkBoxX6_CheckedChanged(object sender, EventArgs e)
        {
            btnRefresh.Pulse(5);
        }

        private void checkBoxX7_CheckedChanged(object sender, EventArgs e)
        {
            btnRefresh.Pulse(5);
        }

        private void checkBoxX8_CheckedChanged(object sender, EventArgs e)
        {
            btnRefresh.Pulse(5);
        }

        private void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            btnRefresh.Pulse(5);
        }

        private void txtEndDate_TextChanged(object sender, EventArgs e)
        {
            btnRefresh.Pulse(5);
        }
        #endregion

        private void checkBoxX10_CheckedChanged(object sender, EventArgs e)
        {
            btnRefresh.Pulse(5);
        }
    }
}