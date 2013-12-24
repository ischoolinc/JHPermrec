using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JHPermrec.UpdateRecord.Transfer
{
    public partial class AddTransStudSemesterHistory : FISCA.Presentation.Controls.BaseForm 
    {
        private JHSchool.Data.JHStudentRecord studRec;

        private JHSchool.Data.JHSemesterHistoryRecord SemesterHistoryRecord;

        public AddTransStudSemesterHistory(JHSchool.Data.JHStudentRecord studentRec)
        {
            InitializeComponent();
            studRec = studentRec;
            LoadStudSemesterHsitoryEntity();
        }

        private void LoadStudSemesterHsitoryEntity()
        { 
            // 取得學期歷程           
            SemesterHistoryRecord = JHSchool.Data.JHSemesterHistory.SelectByStudentID(studRec.ID);

            // 當沒有學習歷程
            if (SemesterHistoryRecord == null)
            {
                SemesterHistoryRecord = new JHSchool.Data.JHSemesterHistoryRecord();
                SemesterHistoryRecord.RefStudentID = studRec.ID;
            }


            int rowIdx = 0;
            foreach (K12.Data.SemesterHistoryItem shi in SemesterHistoryRecord.SemesterHistoryItems)
            {
                rowIdx = dgSemestrHistory.Rows.Add();

                if(shi.SchoolYear !=null )
                    dgSemestrHistory.Rows[rowIdx].Cells[0].Value = shi.SchoolYear.ToString();
                if (shi.Semester != null) ;
                    dgSemestrHistory.Rows[rowIdx].Cells[1].Value = shi.Semester.ToString();
                if(shi.GradeYear !=null )
                    dgSemestrHistory.Rows[rowIdx].Cells[2].Value = shi.GradeYear.ToString();
                if(shi.ClassName !=null )
                    dgSemestrHistory.Rows[rowIdx].Cells[3].Value = shi.ClassName;
                if(shi.SeatNo.HasValue)
                    dgSemestrHistory.Rows[rowIdx].Cells[4].Value = shi.SeatNo.Value.ToString();
                if(shi.Teacher !=null )
                    dgSemestrHistory.Rows[rowIdx].Cells[5].Value = shi.Teacher;
                if(shi.SchoolDayCount.HasValue )
                    dgSemestrHistory.Rows[rowIdx].Cells[6].Value = shi.SchoolDayCount.Value.ToString();
                rowIdx++;
            }        
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SemesterHistoryRecord.SemesterHistoryItems.Clear ();            

            List<string> checkData = new List<string>();
            foreach (DataGridViewRow dr in dgSemestrHistory.Rows)
            {
                if (dr.IsNewRow)
                    continue;

                if (dr.Cells["學年度"].Value == null || dr.Cells["學期"].Value == null || dr.Cells["年級"].Value == null)
                {
                    MessageBox.Show("資料輸入不完整");
                    return;
                }

                string str = "" + dr.Cells["學年度"].Value + dr.Cells["學期"].Value + dr.Cells["年級"].Value;
                if (checkData.Contains(str))
                {
                    MessageBox.Show("學年度+學期+年級有重複");
                    return;
                }
                else
                    checkData.Add(str);
            
            }


            foreach (DataGridViewRow dr in dgSemestrHistory.Rows )
            {
                if (dr.IsNewRow)
                    continue;

                int tmpInt;
                K12.Data.SemesterHistoryItem sshe = new K12.Data.SemesterHistoryItem();
                tmpInt = 0;
                int.TryParse(dr.Cells["學年度"].Value.ToString (), out tmpInt);
                sshe.SchoolYear = tmpInt;
                
                tmpInt = 0;
                int.TryParse(dr.Cells["學期"].Value.ToString (), out tmpInt);
                sshe.Semester = tmpInt;

                tmpInt = 0;
                int.TryParse(dr.Cells["年級"].Value.ToString () , out tmpInt);
                sshe.GradeYear = tmpInt;
                if(dr.Cells["班級"].Value !=null )
                    sshe.ClassName = dr.Cells["班級"].Value.ToString();
                
                if(dr.Cells["班導師"].Value!=null )
                    sshe.Teacher = dr.Cells["班導師"].Value.ToString ();

                if (dr.Cells["座號"].Value != null)
                {
                    int seatNo;
                    if (int.TryParse(dr.Cells["座號"].Value.ToString(), out seatNo))
                        sshe.SeatNo = seatNo;

                }
                if (dr.Cells["上課天數"].Value != null)
                {
                    int DayCount;
                    if (int.TryParse(dr.Cells["上課天數"].Value.ToString(), out DayCount))
                        sshe.SchoolDayCount = DayCount;
                }

                SemesterHistoryRecord.SemesterHistoryItems.Add(sshe);
                
            }

            // 儲存學期歷程
            JHSchool.Data.JHSemesterHistory.Update(SemesterHistoryRecord);

            // log
            JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
            prlp.SaveLog("學生.轉入異動", "新增", "新增 學生:"+ studRec.Name  +" 學期歷程資料..");

            this.Close();
        }
    }
}
