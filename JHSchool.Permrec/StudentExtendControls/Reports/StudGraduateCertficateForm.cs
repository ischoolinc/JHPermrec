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
    public partial class StudGraduateCertficateForm : FISCA.Presentation.Controls.BaseForm
    {
        StudGraduateCertficateManager sgcm;
        string isDefalutTemplate = "true";

        public StudGraduateCertficateForm()
        {
            InitializeComponent();
            sgcm = new StudGraduateCertficateManager();
            if (sgcm.GetisDefaultTemplate() == "true") //畢業證書預設樣板
                cbxDefault.Checked = true;
            else if (sgcm.GetisDefaultTemplate() == "false")
                cbxUserDefine.Checked = true;
            else if (sgcm.GetisDefaultTemplate() == "true2")//修業證書預設樣板
                cbxDefault2.Checked = true;
            else if (sgcm.GetisDefaultTemplate() == "false2")
                cbxUserDefine2.Checked = true;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            if (cbxDefault.Checked == false && cbxUserDefine.Checked == false
                && cbxDefault2.Checked == false && cbxUserDefine2.Checked == false
                && Student.Instance.SelectedKeys.Count < 1)
                return;

            //設定列印來源
            bool IsPointByUpdateRecord = true;
            if (cbIsPointByUpdateRecord.Checked)
                IsPointByUpdateRecord = true;
            else
                IsPointByUpdateRecord = false;

            //if (txtCertDoc.Text.Trim() == "" || txtCertNo.Text.Trim() == "")
            //    if (MessageBox.Show("校內文號輸入不完整，請問是否繼續列印 ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            //        return;

            if (cbxDefault.Checked)
                isDefalutTemplate = "true";
            else if (cbxUserDefine.Checked)
                isDefalutTemplate = "false";
            else if (cbxDefault2.Checked)
                isDefalutTemplate = "true2";
            else if (cbxUserDefine2.Checked)
                isDefalutTemplate = "false2";

            cmdPrint.Enabled = false;
            sgcm.SetCertDoc(txtCertDoc.Text);
            sgcm.SetCertNo(txtCertNo.Text);
            sgcm.SetSemester(School.DefaultSemester, true);

            sgcm.PrintData(Student.Instance.SelectedKeys, isDefalutTemplate, IsPointByUpdateRecord);
            PermRecLogProcess prlp = new PermRecLogProcess();
            prlp.SaveLog("學生.報表", "列印", "列印" + Student.Instance.SelectedKeys.Count + "筆證書。");
            cmdPrint.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sgcm.SaveDefaulTemplate();
        }

        private void lnkUserDefine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sgcm.SaveUserDefineTemplate();
        }

        private void lnkUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sgcm.SetUserDefineTemplateToSystem();
        }

        private void lnkUpload2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sgcm.SetUserDefineSecondTemplateToSystem();
        }

        private void lnkUserDefine2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sgcm.SaveUserDefineSecondTemplate();
        }

        private void lnkDefault2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sgcm.SaveDefaulSecondTemplate();
        }
    }
}
