using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Framework.Legacy;
using JHSchool.Data;

namespace JHPermrec.UpdateRecord.Transfer
{
    public partial class AddTransStudCourse : FISCA.Presentation.Controls.BaseForm
    {
        private JHStudentRecord  studRec;
        private JHClassRecord studClassRec;
        private Dictionary<string, JHClassRecord> classRecIdx;

        private List<DAL.AttendCourseEntity> studAttendCourseEntitys = new List<JHPermrec.UpdateRecord.DAL.AttendCourseEntity>();
        int SchoolYear = GlobalOld.SystemConfig.DefaultSchoolYear;
        int Semester = GlobalOld.SystemConfig.DefaultSemester;

        public AddTransStudCourse(JHSchool.Data.JHStudentRecord studentRec)
        {
            InitializeComponent();
            classRecIdx = new Dictionary<string, JHClassRecord>();

            foreach (JHClassRecord cr in JHSchool.Data.JHClass.SelectAll ())
            {
                if (cr != null)
                {
                    cboSelectClass.Items.Add(cr.Name);
                    if (!classRecIdx.ContainsKey(cr.Name))
                        classRecIdx.Add(cr.Name, cr);
                }
            }

            chkSaveYes.Checked = true;
            studRec = studentRec;
            if (studRec.Class != null)
            {
                lblStudMsg.Text = "班級:" + studRec.Class.Name + ", 座號:" + studRec.SeatNo + ", 姓名:" + studRec.Name + ", 學號:" + studRec.StudentNumber;
                cboSelectClass.Text = studRec.Class.Name;
                studClassRec = studRec.Class;
                
            }
            else
                lblStudMsg.Text = "未設定班級";
            AddTransBackgroundManager.AddTransStudCourseObj = this;
            
        }

        private void AddTransStudCourse_Load(object sender, EventArgs e)
        {
            getCourseToForm();
        }

        private void getCourseToForm()
        {
            getStudAttendCourses();
            int hAtndCot = 0, hNAtnCot = 0, hQAtnCot = 0;
            foreach (DAL.AttendCourseEntity ace in studAttendCourseEntitys)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems[0].Text = ace.CourseID;
                lvi.SubItems.Add(ace.CourseName);
                lvi.SubItems.Add(ace.SubjectName);
                lvi.SubItems.Add(ace.Credit);
                if (ace.CousreAttendType ==DAL.AttendCourseEntity.AttendType.學生修課與班級相同)
                {
                    lvi.SubItems.Add("已修");

                    foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
                        lvs.ForeColor = Color.Black;
                    hAtndCot++;
                }
                if (ace.CousreAttendType == DAL.AttendCourseEntity.AttendType.學生未修)
                {
                    lvi.SubItems.Add("未修");
                    foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
                        lvs.ForeColor = Color.Blue;
                    hNAtnCot++;
                }

                if (ace.CousreAttendType == DAL.AttendCourseEntity.AttendType.學生本身已修)
                {
                    lvi.SubItems.Add("已修非該班課程");
                    foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
                        lvs.ForeColor = Color.Red;
                    hQAtnCot++;
                }

                lstView.Items.Add(lvi);
            }
            lblmsg.Text = "已修課程:" + hAtndCot + ", 未修課程:" + hNAtnCot + ", 已修非該班課程:" + hQAtnCot;
            this.Text = SchoolYear + "學年度 第" + Semester + "學期 學生修課";
        
        }

        private void getStudAttendCourses()
        {            
            studAttendCourseEntitys.Clear();
            if (studRec.Class != null)
                studAttendCourseEntitys = DAL.DALTransfer2.getStudAttendCourseBySchoolYearSemester(SchoolYear, Semester, studRec, studClassRec);
            else
                MessageBox.Show("沒有設定班級,無法取得修課.");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (chkSaveYes.Checked == true)
            {
                List<JHSchool.Data.JHCourseRecord> courseRecs = new List<JHSchool.Data.JHCourseRecord>();
                foreach (DAL.AttendCourseEntity ace in studAttendCourseEntitys)
                    if (ace.CousreAttendType == DAL.AttendCourseEntity.AttendType.學生未修)
                        courseRecs.Add(ace.CourseRec);
                // 待加入儲存            
                if (courseRecs.Count > 0)
                {
                    DAL.DALTransfer2.SetStudentAttendCourse(studRec.ID, courseRecs);

                    string CourseName = "";

                    foreach (JHCourseRecord cor in courseRecs)
                        CourseName += cor.Name + ",";

                    // log
                    JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
                    prlp.SaveLog("學籍.學生轉入異動", "新增學生課程", "新增學生" + studRec.Name + "的課程,課程名稱：" + CourseName);
                }
            }
            
            // 學生課程成績輸入
            AddTransStudCourseScore ATSCS = new AddTransStudCourseScore(studRec);
            this.Visible = false;
            ATSCS.StartPosition = FormStartPosition.CenterParent;
            ATSCS.ShowDialog();

        }

        private void AddStuCourse()
        {


        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            if (chkSaveYes.Checked == true)
            {
                List<JHSchool.Data.JHCourseRecord> courseRecs = new List<JHSchool.Data.JHCourseRecord>();
                foreach (DAL.AttendCourseEntity ace in studAttendCourseEntitys)
                    if (ace.CousreAttendType == DAL.AttendCourseEntity.AttendType.學生未修)
                        courseRecs.Add(ace.CourseRec);
                // 待加入儲存            
                if (courseRecs.Count > 0)
                {
                    DAL.DALTransfer2.SetStudentAttendCourse(studRec.ID, courseRecs);

                    string CourseName = "";

                    foreach (JHCourseRecord cor in courseRecs)
                        CourseName += cor.Name + ",";

                    // log
                    JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
                    prlp.SaveLog("學籍.學生轉入異動", "新增學生課程", "新增學生" + studRec.Name + "的課程,課程名稱：" + CourseName);
                }
            }
            this.Close();

        }

        private void cboSelectClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstView.Items.Clear();
            if (!string.IsNullOrEmpty(cboSelectClass.Text))
            {
                // 更新所選班級時更換相對班級，給載入班課程使用
                if (classRecIdx.ContainsKey(cboSelectClass.Text))
                    studClassRec = classRecIdx[cboSelectClass.Text];

                getCourseToForm();
            }
        }
    }
}
