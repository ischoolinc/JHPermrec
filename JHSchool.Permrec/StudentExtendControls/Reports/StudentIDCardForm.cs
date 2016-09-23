using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JHSchool.Permrec.StudentExtendControls.Reports
{
    public partial class StudentIDCardForm : FISCA.Presentation.Controls.BaseForm 
    {
        StudentIDCardManager sidm;
        bool isDefalutTemplate = true;
        bool isUseSystemPhoto = false;

        int Photo_inch = 0;

        public enum UseModuleType { 學生, 班級 };
        private UseModuleType _UseModule;

        public StudentIDCardForm(UseModuleType UseModule)
        {
            InitializeComponent();
            sidm = new StudentIDCardManager();
            if (sidm.GetisDefaultTemplate())
                cbxDefault.Checked = true;
            else
                cbxUserDefine.Checked = true;
            _UseModule = UseModule;


                
        }

        private void lnkDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sidm.SaveDefaulTemplate();
        }

        private void lnkUserDefine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sidm.SaveUserDefineTemplate();
        }

        private void lnkUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sidm.SetUserDefineTemplateToSystem();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            if (cbxDefault.Checked == false && cbxUserDefine.Checked == false && Student.Instance.SelectedKeys.Count < 1)
                return;

            if (cbxDefault.Checked)
                isDefalutTemplate = true;
            else
                isDefalutTemplate = false;

            if (chkUseSystemPhotoAuto.Checked)
            {
                isUseSystemPhoto = true;
                DAL.DALTransfer.UseStudPhotp = true;
                Photo_inch = 0;
            }
            else
            {
                isUseSystemPhoto = false;
                DAL.DALTransfer.UseStudPhotp = false;
            }

            //2016/9/22 穎驊新增，新增高雄國中學生證列印可以調整一吋、二吋大小

            if (chkUseSystemPhoto_1_Inch.Checked) 
            {
                isUseSystemPhoto = true;
                DAL.DALTransfer.UseStudPhotp = true;
                Photo_inch = 1;
            
            
            }
            if (chkUseSystemPhoto_2_Inch.Checked)
            {
                isUseSystemPhoto = true;
                DAL.DALTransfer.UseStudPhotp = true;
                Photo_inch = 2;


            }

            cmdPrint.Enabled = false;
            bool isPrintFinish = false;

            int LogCot = 0;

            if (_UseModule == UseModuleType.學生)
            {
                isPrintFinish = sidm.PrintData(Student.Instance.SelectedKeys, isDefalutTemplate, isUseSystemPhoto, Student.Instance.SelectedKeys.Count,Photo_inch);
                LogCot = Student.Instance.SelectedKeys.Count;
            }

            if (_UseModule == UseModuleType.班級)
            {
                List<string> StudentIDList = new List<string>();
                foreach (ClassRecord cr in Class.Instance.SelectedList)
                {
                    foreach (StudentRecord studRec in cr.Students.GetStatusStudents("一般"))
                        StudentIDList.Add(studRec.ID);

                    foreach (StudentRecord studRec in cr.Students.GetStatusStudents("輟學"))
                        StudentIDList.Add(studRec.ID);                
                }
                isPrintFinish = sidm.PrintData(StudentIDList, isDefalutTemplate, isUseSystemPhoto,StudentIDList.Count,Photo_inch);
                LogCot = StudentIDList.Count;
            }

            
            if (isPrintFinish)
            {
                PermRecLogProcess prlp = new PermRecLogProcess();
                prlp.SaveLog("學生.報表", "列印", "列印"+LogCot+"筆學生證資料。");
                cmdPrint.Enabled = true;
            }
        }


        //使使用者只能在自動大小、一吋照片、二吋照片中選一個選項
        private void chkUseSystemPhotoAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseSystemPhotoAuto.Checked == true) 
            {
                chkUseSystemPhoto_1_Inch.Checked = false;
                chkUseSystemPhoto_2_Inch.Checked = false;            
            }
        }

        private void chkUseSystemPhoto_1_Inch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseSystemPhoto_1_Inch.Checked == true)
            {
                chkUseSystemPhotoAuto.Checked = false;
                chkUseSystemPhoto_2_Inch.Checked = false;                
            }
        }

        private void chkUseSystemPhoto_2_Inch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseSystemPhoto_2_Inch.Checked == true)
            {
                chkUseSystemPhoto_1_Inch.Checked = false;
                chkUseSystemPhotoAuto.Checked = false;                
            }
        }
    }
}
