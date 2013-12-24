using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using Aspose.Cells;
using JHSchool.Permrec;

namespace JHPermrec.UpdateRecord.Batch
{
    public partial class BatchStudGraduateUpdateRec_Warning : FISCA.Presentation.Controls.BaseForm
    {
        private bool _chkWriteData = false;
        private bool _chkViewData = false;
        private int _DataCount = 0;
        private List<DAL.StudUpdateRecordEntity> _StudUpdateRecordEntity;

        public bool chkWriteData
        { get { return _chkWriteData; } set { _chkWriteData = value; } }

        public bool chkViewData
        { get { return _chkViewData; } set { _chkViewData = value; } }

        public int DataCount
        { get { return _DataCount; } set { _DataCount = value; } }

        public List<DAL.StudUpdateRecordEntity> chkStudUpdateRecords
        { get { return _StudUpdateRecordEntity; } set { _StudUpdateRecordEntity = value; } }

      
        public BatchStudGraduateUpdateRec_Warning()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this._chkWriteData = false;
            this.Visible = false;
        }

        private void btnExportXls_Click(object sender, EventArgs e)
        {
            Workbook wb = new Workbook();
            int Row = 1;
            wb.Worksheets[0].Cells[0, 0].PutValue("學號");
            wb.Worksheets[0].Cells[0, 1].PutValue("班級");
            wb.Worksheets[0].Cells[0, 2].PutValue("座號");
            wb.Worksheets[0].Cells[0, 3].PutValue("姓名");
            wb.Worksheets[0].Cells[0, 4].PutValue("異動日期");
            wb.Worksheets[0].Cells[0, 5].PutValue("入學年月");

            foreach (DAL.StudUpdateRecordEntity sure in this._StudUpdateRecordEntity)
            {
                wb.Worksheets[0].Cells[Row, 0].PutValue(sure.GetStudentNumber());
                wb.Worksheets[0].Cells[Row, 1].PutValue(sure.GetClassName());
                wb.Worksheets[0].Cells[Row, 2].PutValue(sure.GetSeatNo());
                wb.Worksheets[0].Cells[Row, 3].PutValue(sure.GetName());
                wb.Worksheets[0].Cells[Row, 4].PutValue(sure.GetUpdateDate());
                wb.Worksheets[0].Cells[Row, 5].PutValue(sure.GetEnrollmentSchoolYear());
                Row++;
            }
            wb.Worksheets[0].Name = "已有畢業異動";
            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\已有畢業異動.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\已有畢業異動.xls");

            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "已有畢業異動.xls";
                sd1.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd1.FileName, FileFormatType.Excel2003);
                        System.Diagnostics.Process.Start(sd1.FileName);
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void btnWriteData_Click(object sender, EventArgs e)
        {
            this._chkWriteData = true;
            this.Visible = false;
        }

        private void BatchStudGraduateUpdateRec_Warning_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "有" + this._DataCount.ToString() + "名學生已有畢業異動。";
        }
    }
}
