using System;
using System.Windows.Forms;
using JHPermrec.UpdateRecord.GovernmentalDocument.NameList;


namespace JHPermrec.UpdateRecord.GovernmentalDocument.Process
{
    public partial class NameList : UserControl
    {
        //FeatureAccessControl nameListCtrl;

        public NameList()
        {
            InitializeComponent();
        }

        private void btnItemNameList_Click(object sender, EventArgs e)
        {

            RegisterNames();

            new ListForm().ShowDialog();
        }

        internal static void RegisterNames()
        {
            // 要記得相對要改 Build Wizard 因為選項在內
            // 使用高雄模組會用
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.KaoHsiung)
            {
                if (ReportBuilderManager.Items["新生名冊"].Count == 0)
                    ReportBuilderManager.Items["新生名冊"].Add(new EnrollmentList());

                if (ReportBuilderManager.Items["畢業名冊"].Count == 0)
                    ReportBuilderManager.Items["畢業名冊"].Add(new GraduatingStudentList());

                if (ReportBuilderManager.Items["轉入學生名冊"].Count == 0)
                    ReportBuilderManager.Items["轉入學生名冊"].Add(new TransferringStudentUpdateRecordList());


                if (ReportBuilderManager.Items["轉出學生名冊"].Count == 0)
                    ReportBuilderManager.Items["轉出學生名冊"].Add(new Transferring01StudentUpdateRecordList());


                if (ReportBuilderManager.Items["中輟學生名冊"].Count == 0)
                    ReportBuilderManager.Items["中輟學生名冊"].Add(new ExtendingStudentList());


                if (ReportBuilderManager.Items["休學學生名冊"].Count == 0)
                    ReportBuilderManager.Items["休學學生名冊"].Add(new SuspensionStudentUpdateRecordList());

                if (ReportBuilderManager.Items["復學學生名冊"].Count == 0)
                    ReportBuilderManager.Items["復學學生名冊"].Add(new ResumeStudentUpdateRecordList());

                if (ReportBuilderManager.Items["續讀學生名冊"].Count == 0)
                    ReportBuilderManager.Items["續讀學生名冊"].Add(new ContinueStudentUpdateRecordList());

                //if (ReportBuilderManager.Items["更正學籍學生名冊"].Count == 0)
                //    ReportBuilderManager.Items["更正學籍學生名冊"].Add(new UpdateStudentPermrecList());


                if (ReportBuilderManager.Items["死亡學生名冊"].Count == 0)
                    ReportBuilderManager.Items["死亡學生名冊"].Add(new DeadStudentList());
            }


            // 使用新竹模組
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.HsinChu)
            {
                if (ReportBuilderManager.Items["新生名冊"].Count == 0)
                    ReportBuilderManager.Items["新生名冊"].Add(new EnrollmentList());

                if (ReportBuilderManager.Items["畢業名冊"].Count == 0)
                    ReportBuilderManager.Items["畢業名冊"].Add(new GraduatingStudentList());

                // 轉入+復學
                if (ReportBuilderManager.Items["轉入學生名冊"].Count == 0)
                    ReportBuilderManager.Items["轉入學生名冊"].Add(new TransferringStudentUpdateRecordList());

                // 其它使用轉出
                if (ReportBuilderManager.Items["轉出學生名冊"].Count == 0)
                    ReportBuilderManager.Items["轉出學生名冊"].Add(new Transferring01StudentUpdateRecordList());


                if (ReportBuilderManager.Items["更正學籍學生名冊"].Count == 0)
                    ReportBuilderManager.Items["更正學籍學生名冊"].Add(new UpdateStudentPermrecList());

            }

            // 使用台中縣模組
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.TaiChung)
            {

                if (ReportBuilderManager.Items["新生名冊"].Count == 0)
                    ReportBuilderManager.Items["新生名冊"].Add(new EnrollmentList());

                if (ReportBuilderManager.Items["畢業名冊"].Count == 0)
                    ReportBuilderManager.Items["畢業名冊"].Add(new GraduatingStudentList());

                if (ReportBuilderManager.Items["轉入學生名冊"].Count == 0)
                    ReportBuilderManager.Items["轉入學生名冊"].Add(new TransferringStudentUpdateRecordList());

                if (ReportBuilderManager.Items["轉出學生名冊"].Count == 0)
                    ReportBuilderManager.Items["轉出學生名冊"].Add(new Transferring01StudentUpdateRecordList());

                if (ReportBuilderManager.Items["復學學生名冊"].Count == 0)
                    ReportBuilderManager.Items["復學學生名冊"].Add(new ResumeStudentUpdateRecordList());

                if (ReportBuilderManager.Items["中輟學生名冊"].Count == 0)
                    ReportBuilderManager.Items["中輟學生名冊"].Add(new ExtendingStudentList());

                if (ReportBuilderManager.Items["更正學籍學生名冊"].Count == 0)
                    ReportBuilderManager.Items["更正學籍學生名冊"].Add(new UpdateStudentPermrecList());
            }
        }
    }
}
