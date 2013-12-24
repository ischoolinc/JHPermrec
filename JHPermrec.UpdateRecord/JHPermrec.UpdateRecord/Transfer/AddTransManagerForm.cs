using System;


namespace JHPermrec.UpdateRecord.Transfer
{
    public partial class AddTransManagerForm : FISCA.Presentation.Controls.BaseForm 
    {
        public AddTransManagerForm()
        {
            InitializeComponent();
            AddTransBackgroundManager.AddTransManagerFormObj = this;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string sid = AddTransBackgroundManager.GetStudent().ID;
            JHSchool.Data.JHStudentRecord StudRec = JHSchool.Data.JHStudent.SelectByID(sid);

            if (chkStudBase.Checked)
            {
                AddTransStudBaseData atsbd = new AddTransStudBaseData(AddTransBackgroundManager.GetStudent());
                atsbd.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                atsbd.ShowDialog(FISCA.Presentation.MotherForm.Form);
            }

            if (chkStudCourse.Checked)
            {
                AddTransStudCourse atsc = new AddTransStudCourse(StudRec);
                atsc.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                atsc.ShowDialog(FISCA.Presentation.MotherForm.Form);
            }

            if (chkStudCourseScore.Checked)
            {
                AddTransStudCourseScore atscs = new AddTransStudCourseScore(StudRec);
                atscs.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                atscs.ShowDialog(FISCA.Presentation.MotherForm.Form);
            }

            if (chkSemesterHistory.Checked)
            {
                AddTransStudSemesterHistory atssh = new AddTransStudSemesterHistory(StudRec);
                atssh.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                atssh.ShowDialog(FISCA.Presentation.MotherForm.Form);
            }

            if (chkStudSemsSubjScore.Checked)
            {
                AddTransStudSemsSubjScore atssss = new AddTransStudSemsSubjScore(AddTransBackgroundManager.GetStudent());
                atssss.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                atssss.ShowDialog(FISCA.Presentation.MotherForm.Form);
            }

            if (chkStudUpdateRec.Checked)
            {
                AddTransStudUpdateRecord atsur = new AddTransStudUpdateRecord(AddTransBackgroundManager.GetStudent());
                atsur.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                atsur.ShowDialog(FISCA.Presentation.MotherForm.Form);
            }
        }
    }
}
