using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using K12.Data.Configuration;
using System.Xml;
using Campus.Report;
using System.IO;

namespace TaiChung.StudentRecordReport.ConfigForm
{
    public partial class PrintConfigForm : BaseForm
    {
        private ReportConfiguration _config;

        public PrintConfigForm()
        {
            InitializeComponent();
            _config = new ReportConfiguration(Global.ReportName);
            this.MinimumSize = this.MaximumSize = this.Size;

            //SetupDefaultTemplate();
            LoadConfig();
        }     

        private void LoadConfig()
        {
            string print = _config.GetString("領域科目設定", string.Empty);
            if (print == "Domain")
                rbDomain.Checked = true;
            else if (print == "Subject")
                rbSubject.Checked = true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _config.SetString("領域科目設定", (rbDomain.Checked) ? "Domain" : "Subject");
       
            
            _config.Save();

            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
