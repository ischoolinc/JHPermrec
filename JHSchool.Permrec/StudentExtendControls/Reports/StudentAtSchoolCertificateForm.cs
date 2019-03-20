using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;

namespace JHSchool.Permrec.StudentExtendControls.Reports
{
    public partial class StudentAtSchoolCertificateForm : FISCA.Presentation.Controls.BaseForm 
    {
        bool isDefaultTemplate = true;
        bool isDefaultTemplateChinese = false; // [ischoolkingdom] Vicky新增，[11-04][02]在學證明書(英文)
        StudentAtSchoolCertificateManager sascm;
        public StudentAtSchoolCertificateForm()
        {
            InitializeComponent();
            sascm = new StudentAtSchoolCertificateManager();

            if (sascm.GetisDefaultTemplate())
                cbxDefault_Chi.Checked = true;
            else
                cbxUserDefine.Checked = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // [ischoolkingdom] Vicky新增，[11-04][02]在學證明書(英文)，新增預設中文樣板、中英文預設樣板選擇
        private void lnkDefault_Chi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sascm.SaveDefaulTemplate_Chi();
        }

        private void lnkDefault_ChiEng_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sascm.SaveDefaulTemplate_ChiEng();
        }

        private void lnkUserDefine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sascm.SaveUserDefineTemplate();
        }

        private void lnkUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sascm.SetUserDefineTemplateToSystem();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            //// [ischoolkingdom] Vicky新增，[11-04][02]在學證明書(英文)，新增預設選項
            // 檢查選項是否有設定
            if (cbxDefault_Chi.Checked == false && cbxDefault.Checked == false && cbxUserDefine.Checked == false && Student.Instance.SelectedKeys.Count < 1)
                return;

            if (txtCertDoc.Text.Trim() == "" || txtCertNo.Text.Trim() == "")            
                if (MessageBox.Show("校內文號輸入不完整，請問是否繼續列印 ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;

            sascm.SetCertDoc(txtCertDoc.Text);
            sascm.SetCertNo(txtCertNo.Text);

            sascm.SetSemester(School.DefaultSemester,false );

            // [ischoolkingdom] Vicky新增，[11-04][02]在學證明書(英文)，新增cbx選項
            isDefaultTemplateChinese = cbxDefault_Chi.Checked;
            if (cbxDefault.Checked || cbxDefault_Chi.Checked)
                isDefaultTemplate = true;
            else
                isDefaultTemplate = false;

            cmdPrint.Enabled = false;
            sascm.PrintData(Student.Instance.SelectedKeys, isDefaultTemplate, isDefaultTemplateChinese);            
            PermRecLogProcess prlp = new PermRecLogProcess();
            prlp.SaveLog("學生.報表", "列印", "列印" + Student.Instance.SelectedKeys.Count + "筆在學證明書資料。");

            cmdPrint.Enabled = true;

        }
    }
}
