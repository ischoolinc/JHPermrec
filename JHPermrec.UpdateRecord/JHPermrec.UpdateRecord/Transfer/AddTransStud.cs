using System;
using System.Windows.Forms;
using IRewriteAPI_JH;
using FISCA.Presentation;

namespace JHPermrec.UpdateRecord.Transfer
{
    public partial class AddTransStud : FISCA.Presentation.Controls.BaseForm
    {
        IStudentTransStudBaseAPI item = FISCA.InteractionService.DiscoverAPI<IStudentTransStudBaseAPI>();

        public AddTransStud()
        {
            InitializeComponent();
            // 傳一份給背後管理者
            AddTransBackgroundManager.AddTransStudObj = this;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AddTransStudBase.AddTransStudStatus status;

            if (string.IsNullOrEmpty(txtSSN.Text))
            {
                return;
            }


            if (AddTransBackgroundManager.GetHasStudentData() == false || (txtSSN.Text != AddTransBackgroundManager.StudentIDNumber))
                AddTransBackgroundManager.SetStudentBySSN(txtSSN.Text.ToUpper());

            AddTransBackgroundManager.StudentIDNumber = txtSSN.Text.ToUpper ();

            // 已有學生資料
            if (AddTransBackgroundManager.GetHasStudentData())
            {
                    status = AddTransStudBase.AddTransStudStatus.Modify;
            }
            else
            {
                status = AddTransStudBase.AddTransStudStatus.Added;
            }

            setTransClassAndCourse(status, AddTransBackgroundManager.GetStudent());

            //StudentEntity student=null ;

            //if (AddTransBackgroundManager.GetCheckSSN()==false )
            //{
            //student = DAL.TransferDAL.GetStudent(txtSSN.Text);

            //}

            //if (student == null)
            //{
            //    // 修改先不新增學生
            //    // student = DAL.TransferDAL.AddStudent(txtSSN.Text);

            //    status = AddTransStudBase.AddTransStudStatus.Added;
            //}
            //else
            //    status = AddTransStudBase.AddTransStudStatus.Modify;

            //setTransClassAndCourse(status, student);         

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setTransClassAndCourse(AddTransStudBase.AddTransStudStatus status, JHSchool.Data.JHStudentRecord student)
        {
            if (item != null)
            {
                this.Visible = false;
                ITransStudBase tt = item.CreateForm();                
                tt.SetData(student, status);
                tt.Display();
//                item.SetData(student, status);
                
                //ats.setStudent_Status(student, status);
               

                //item.CreateForm().StartPosition = FormStartPosition.CenterParent;
                //item.CreateForm().ShowDialog(FISCA.Presentation.MotherForm.Form);
            }
            else
            {
                AddTransStudBase ats = new AddTransStudBase();
                ats.setStudent_Status(student, status);
                this.Visible = false;
                ats.StartPosition = FormStartPosition.CenterParent;
                ats.ShowDialog(FISCA.Presentation.MotherForm.Form);
            }
        }
    }
}
