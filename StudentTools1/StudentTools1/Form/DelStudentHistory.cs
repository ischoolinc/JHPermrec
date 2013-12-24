using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudentTools1.Form
{
    public partial class DelStudentHistory : FISCA.Presentation.Controls.BaseForm 
    {
        BackgroundWorker _bgWork;
        List<string> _StudentIDList;
        int sc, ss;
        
        public DelStudentHistory()
        {
            InitializeComponent();
            _StudentIDList = new List<string>();
            _bgWork = new BackgroundWorker();
            _bgWork.DoWork += new DoWorkEventHandler(_bgWork_DoWork);
            _bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWork_RunWorkerCompleted);
        }

        void _bgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnDel.Enabled = true;
            FISCA.Presentation.Controls.MsgBox.Show("刪除完成!");
        }

        void _bgWork_DoWork(object sender, DoWorkEventArgs e)
        {
            List<K12.Data.SemesterHistoryRecord> recs = K12.Data.SemesterHistory.SelectByStudentIDs(_StudentIDList);
            List<K12.Data.SemesterHistoryRecord> delRecs = new List<K12.Data.SemesterHistoryRecord>();

            foreach (K12.Data.SemesterHistoryRecord shis in recs)
            {
                K12.Data.SemesterHistoryItem delItm = null;

                foreach (K12.Data.SemesterHistoryItem shi in shis.SemesterHistoryItems)
                {
                    if (shi.SchoolYear == sc && shi.Semester == ss)
                        delItm = shi;
                }

                if (delItm != null)
                {
                    shis.SemesterHistoryItems.Remove(delItm);
                    delRecs.Add(shis);
                }
            }

            if (delRecs.Count > 0)
                K12.Data.SemesterHistory.Update(delRecs);
        }

        private void DelStudentHistory_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.MinimumSize = this.Size;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            btnDel.Enabled = false;
            bool pass = false;
            if (int.TryParse(txtSchoolYear.Text, out sc))
            {
                if (int.TryParse(txtSemester.Text, out ss))
                {
                    pass = true;
                    _StudentIDList = K12.Presentation.NLDPanels.Student.SelectedSource;
                }            
            }

            if (pass)
                _bgWork.RunWorkerAsync();
            else
            {
                btnDel.Enabled = true;
                return;
            }
        }
    }
}
