using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
//using JHSchool.Evaluation.StudentExtendControls.SemesterScoreItemControls;
using JHSchool.SF.Evaluation;
using JHSchool.Data;


namespace JHPermrec.UpdateRecord.Transfer
{
    public partial class AddTransStudSemsSubjScore : FISCA.Presentation.Controls.BaseForm
    {
        private BackgroundWorker _worker;
        private List<JHSchool.Data.JHSemesterScoreRecord> _recordList;
        private JHSchool.Data.JHStudentRecord studRec;
        private string _RunningID;
        private List<string> _domainList;

        public AddTransStudSemsSubjScore(JHSchool.Data.JHStudentRecord student)
        {
            
            InitializeComponent();
            chkNextYes.Checked = true;
            studRec = student;
            _domainList = new List<string>();
            InitializeColumnHeader();

            string msg = "班級:";
            if (student.Class != null)
                msg += student.Class.Name;
            if (student.SeatNo.HasValue)
                msg += ", 座號:" + student.SeatNo.Value;
            msg += ", 姓名:" + student.Name + ", 學號:" + student.StudentNumber;

            if (studRec.Class != null)
                lblStudMsg.Text = msg;
            else
                lblStudMsg.Text = "未設定班級";

            _worker = new BackgroundWorker();
            _worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {

                e.Result = JHSchool.Data.JHSemesterScore.SelectByStudentID("" + e.Argument);
            };
            _worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
            {
                if (_RunningID != student.ID)
                {
                    _RunningID = student.ID;
                    _worker.RunWorkerAsync(_RunningID);
                    return;
                }

                _recordList = e.Result as List<JHSchool.Data.JHSemesterScoreRecord>;
                FillListView();
            };
            

            LoadSemesterScores();
            AddTransBackgroundManager.AddTransStudSemsSubjScoreObj = this;
        }

        private void InitializeColumnHeader()
        {
            //一般領域
            
            foreach (string each in JHSchool.Evaluation.Subject.Domains)
            {
                ColumnHeader ch = new ColumnHeader();
                ch.Name = "ch" + each;
                ch.Text = each;
                ch.Width = GetColumnHeaderWidth(each);
                listView.Columns.Add(ch);
                _domainList.Add(each);
            }

            //學習領域總成績
            ColumnHeader chScore = new ColumnHeader();
            chScore.Name = "ch" + "學習領域總成績";
            chScore.Text = "學習領域總成績";
            chScore.Width = GetColumnHeaderWidth("學習領域總成績");
            listView.Columns.Add(chScore);
        }

        private int GetColumnHeaderWidth(string text)
        {
            return (text.Length - 1) * 13 + 31; //神奇的欄位寬度計算…
        }

        private void FillListView()
        {


            listView.Items.Clear();
            if (_recordList == null) return;

            picLoading.Visible = false;

            _recordList.Sort(delegate(JHSemesterScoreRecord a, JHSemesterScoreRecord b)
            {
                if (a.SchoolYear == b.SchoolYear)
                    return a.Semester.CompareTo(b.Semester);
                return a.SchoolYear.CompareTo(b.SchoolYear);
            });

            //記錄哪些領域沒有成績
            Dictionary<string, bool> no_score_domains = new Dictionary<string, bool>();
            foreach (string domain in _domainList)
                no_score_domains.Add(domain, false);

            foreach (var record in _recordList)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = record;

                listView.Items.Add(item);

                item.Text = "" + record.SchoolYear;
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "" + record.Semester));

                foreach (string domain in _domainList)
                {
                    ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(item, string.Empty);
                    if (record.Domains.ContainsKey(domain) && !string.IsNullOrEmpty("" + record.Domains[domain].Score))
                    {
                        subItem.Text = "" + record.Domains[domain].Score;
                        no_score_domains[domain] = true;
                    }
                    item.SubItems.Add(subItem);
                }

                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "" + record.LearnDomainScore));
            }

            //將沒有成績的領域從畫面中拿掉
            foreach (string domain in no_score_domains.Keys)
            {
                string ch = "ch" + domain;
                int width = 0;

                if (no_score_domains[domain]) width = GetColumnHeaderWidth(domain);

                if (listView.Columns.ContainsKey("ch" + domain))
                    listView.Columns["ch" + domain].Width = width;
            }


//            listView.Items.Clear();
//            if (_recordList == null) return;

//            picLoading.Visible = false;

//            _recordList.Sort(delegate(JHSchool.Data.JHSemesterScoreRecord a, JHSchool.Data.JHSemesterScoreRecord b)
//            {
//                if (a.SchoolYear == b.SchoolYear)
//                    return a.Semester.CompareTo(b.Semester);
//                return a.SchoolYear.CompareTo(b.SchoolYear);
//            });

//            foreach (var record in _recordList)
//            {
//                ListViewItem item = new ListViewItem(new string[] {
//                    "" + record.SchoolYear,
//                    "" + record.Semester,
////                    "" + record.GradeYear,
//                    (record.Domains.ContainsKey("語文"))?"" + record.Domains["語文"].Score:"",
//                    (record.Domains.ContainsKey("數學"))?"" + record.Domains["數學"].Score:"",
//                    (record.Domains.ContainsKey("社會"))?"" + record.Domains["社會"].Score:"",
//                    (record.Domains.ContainsKey("自然與生活科技"))?"" + record.Domains["自然與生活科技"].Score:"",
//                    (record.Domains.ContainsKey("藝術與人文"))?"" + record.Domains["藝術與人文"].Score:"",
//                    (record.Domains.ContainsKey("健康與體育"))?"" + record.Domains["健康與體育"].Score:"",
//                    (record.Domains.ContainsKey("綜合活動"))?"" + record.Domains["綜合活動"].Score:"",
//                    (record.Domains.ContainsKey("彈性課程"))?"" + record.Domains["彈性課程"].Score:"",
//                    "" + record.LearnDomainScore
//                });
//                item.Tag = record;
//                listView.Items.Add(item);
//            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {

            if (listView.SelectedItems.Count <= 0) return;

            JHSchool.Data.JHSemesterScoreRecord rec = listView.SelectedItems[0].Tag as JHSchool.Data.JHSemesterScoreRecord;
                   
            if (SemesterScoreEditor.ShowDialog(studRec.ID,rec.SchoolYear,rec.Semester ) == DialogResult.OK)
            {
                LoadSemesterScores();
            }

            listView.Focus();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            btnModify_Click(sender, e);
        }



        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnModify.Enabled = btnDelete.Enabled = (listView.SelectedItems.Count > 0);
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            //JHSchool.SF.Evaluation.QuickInputSemesterScoreForm.ShowDialog();
            //QuickInputSemesterScoreForm form = new QuickInputSemesterScoreForm(JHSchool.Student.Instance[studRec.ID ]);            
            if (QuickInputSemesterScoreForm.ShowDialog(studRec.ID ) == DialogResult.OK)
            {
                LoadSemesterScores();
            }

            listView.Focus();
        }

        private void LoadSemesterScores()
        {
            btnModify.Enabled = btnDelete.Enabled = false;

            if (_domainList.Count == 0)
                InitializeColumnHeader();

            listView.Items.Clear();
            if (!_worker.IsBusy)
            {
                _recordList = null;
                _RunningID = studRec.ID;
                _worker.RunWorkerAsync(_RunningID);
                picLoading.Visible = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // log
            JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
            prlp.SaveLog("學生.轉入異動", "新增學期成績與領域資料", "新增學期成績與領域資料,姓名:"+studRec.Name+",學號:"+studRec.StudentNumber);

            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count <= 0) return;
            ListViewItem item = listView.SelectedItems[0];

            if (Framework.MsgBox.Show("您確定要刪除「" + item.SubItems[0].Text + "學年度 第" + item.SubItems[1].Text + "學期」的學期成績嗎？", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                JHSchool.Data.JHSemesterScoreRecord record = item.Tag as JHSchool.Data.JHSemesterScoreRecord;
                JHSchool.Data.JHSemesterScore.Delete(record);
                listView.Items.Remove(listView.SelectedItems[0]);
                listView.Refresh();
            }

            listView.Focus();
        }

    }
}
