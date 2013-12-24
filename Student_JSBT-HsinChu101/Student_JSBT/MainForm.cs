using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Student_JSBT_HsinChu101
{
    public partial class MainForm : FISCA.Presentation.Controls.BaseForm
    {
        StudSBTManager SSBTM = new StudSBTManager();
        BackgroundWorker _bwWork;
        List<StudInfoEntity> _StudInfoEntityList = new List<StudInfoEntity>();

        public MainForm()
        {
            InitializeComponent();
            chkPerm.Checked = true;
            this.MaximumSize = this.MinimumSize = this.Size;
            _bwWork = new BackgroundWorker();
            _bwWork.DoWork += new DoWorkEventHandler(_bwWork_DoWork);
            _bwWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bwWork_RunWorkerCompleted);
            
        }

        void _bwWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // 排序資料
            if (chkByStudentNum.Checked)
                _StudInfoEntityList = SSBTM.SortDataByStudentNum(_StudInfoEntityList);

            if (chkByClassSeatNo.Checked)
                _StudInfoEntityList = SSBTM.SortDataByClassSeatNo(_StudInfoEntityList);

            // 產生到 Excel
            SSBTM.ExportDataToExcel(_StudInfoEntityList);
            btn_Export.Enabled = true;
        }

        void _bwWork_DoWork(object sender, DoWorkEventArgs e)
        {
            _StudInfoEntityList = SSBTM.GetStudInfoData(K12.Presentation.NLDPanels.Student.SelectedSource);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            btn_Export.Enabled = false;
            Global.SelectPhoneType = cbxPhoneType.Text;

            // 使用聯絡地址，一般使用戶籍地址
            if (chkMai.Checked)
                DALTransfer._UseMailAddress = true;
            
            if(chkPerm.Checked)
                DALTransfer._UseMailAddress = false;

            _bwWork.RunWorkerAsync();
        }

        private void lnkSetSCT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetStudTagComparisonForm sstcf = new SetStudTagComparisonForm();
            sstcf.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            chkByStudentNum.Checked = true;
            // 電話型態初始化
            cbxPhoneType.Items.Add("戶籍電話");
            cbxPhoneType.Items.Add("聯絡電話");
            cbxPhoneType.Items.Add("父親電話");
            cbxPhoneType.Items.Add("母親電話");
            cbxPhoneType.Items.Add("監護人電話");
            cbxPhoneType.Text = "戶籍電話";
        }
    }
}
