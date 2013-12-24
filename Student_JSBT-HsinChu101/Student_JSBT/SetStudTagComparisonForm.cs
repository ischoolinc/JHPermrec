using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Aspose.Cells;
using System.Xml;

namespace Student_JSBT_HsinChu101
{
    public partial class SetStudTagComparisonForm : FISCA.Presentation.Controls.BaseForm
    {        

        public SetStudTagComparisonForm()
        {
            InitializeComponent();
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog od = new OpenFileDialog();
                od.Filter = "Excel檔案(*.xls)|*.xls";
                if (od.ShowDialog() != DialogResult.OK) return;
                Dictionary<string, int> cellColIdx = new Dictionary<string, int>();

                Workbook wb = new Workbook();
                wb.Open(od.FileName);
                Worksheet wst = wb.Worksheets[0];

                for (byte i = 0; i <= wst.Cells.MaxDataColumn; i++)
                {
                    if (!string.IsNullOrEmpty(wst.Cells[0, i].StringValue))
                    {
                        if (!cellColIdx.ContainsKey(wst.Cells[0, i].StringValue))
                            cellColIdx.Add(wst.Cells[0, i].StringValue, i);
                    }
                }

                dgData.Rows.Clear();

                // 寫入 DatGridView
                for (int row = 1; row <= wst.Cells.MaxDataRow; row++)
                {
                    int cellCol = 0;
                    foreach (DataGridViewColumn dvc in dgData.Columns)
                    {
                        dgData.Rows.Add();

                        if (cellColIdx.ContainsKey(dvc.HeaderText))
                        {

                            dgData.Rows[row - 1].Cells[cellCol].Value = wst.Cells[row, cellColIdx[dvc.HeaderText]].StringValue;
                            cellCol++;
                        }
                    }
                }
                FISCA.Presentation.Controls.MsgBox.Show("匯入完成");
            }
            catch
            {
                MessageBox.Show("檔案讀取失敗!");
                return;
            }
        }
      
 

        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (dgData.Rows.Count < 1)
                return;

            Workbook wb = new Workbook();
            Worksheet wst = wb.Worksheets[0];

            int col=0;
            // HeadText
            foreach (DataGridViewColumn dvc in dgData.Columns )
            {
                wst.Cells[0, col].PutValue(dvc.HeaderText);
                col++;
            }

            int rowIdx = 1;
            
            foreach (DataGridViewRow dgv in dgData.Rows)
            {
                if (dgv.IsNewRow)
                    continue;
                for (int coli = 0; coli < dgData.Columns.Count; coli++)
                {
                    if (dgv.Cells[coli].Value != null)
                        wst.Cells[rowIdx, coli].PutValue(dgv.Cells[coli].Value + "");
                }
                rowIdx++;
            }

            try
            {

                wb.Save(Application.StartupPath + "\\Reports\\學生基本學力資料對照檔.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\學生基本學力資料對照檔.xls");
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "學生基本學力資料對照檔.xls";
                sd1.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (dgData.Rows.Count < 1)
                return;


            XmlElement XmlElems = new XmlDocument().CreateElement("Data");

            foreach (DataGridViewRow gvr in dgData.Rows)
            {
                if (gvr.IsNewRow)
                    continue;

                XmlElement xlm = XmlElems.OwnerDocument.CreateElement("Data");
                if (gvr.Cells[0] != null)
                    xlm.SetAttribute("FieldName", gvr.Cells[0].Value + "");

                if (gvr.Cells[1] != null)
                    xlm.SetAttribute("StudTag", gvr.Cells[1].Value + "");

                if (gvr.Cells[2] != null)
                    xlm.SetAttribute("ItemName", gvr.Cells[2].Value + "");

                if (gvr.Cells[3] != null)
                    xlm.SetAttribute("ItemValue", gvr.Cells[3].Value + "");
                XmlElems.AppendChild(xlm);
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XmlElems.OuterXml);            
            StudSBTManager.SaveDataToSystem(doc);
            //doc.Save("c:\\StudentJSBT_conf.xml");
            FISCA.Presentation.Controls.MsgBox.Show("儲存成功.");
            lblMsg.Visible = false;
            this.Close();
        }

        private void SetStudTagComparisonForm_Load(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = StudSBTManager.GetDataFormSystem();


            if (xmlDoc == null)
                return;

                if(xmlDoc.SelectSingleNode("Data") != null )
                foreach (XmlElement xe in xmlDoc.SelectSingleNode("Data"))
                {
                    dgData.Rows.Add(xe.GetAttribute("FieldName"), xe.GetAttribute("StudTag"), xe.GetAttribute("ItemName"), xe.GetAttribute("ItemValue"));
                }
                
                lblMsg.Visible = false;
        }

        private void dgData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            lblMsg.Visible = true;
        }

        private void SetStudTagComparisonForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lblMsg.Visible)
            { 
                if(FISCA.Presentation.Controls.MsgBox.Show("資料尚未儲存，確定要離開?",MessageBoxButtons.YesNo,MessageBoxDefaultButton.Button2)== DialogResult.No )
                {
                    e.Cancel = true;                                    
                }            
            }
        }
    }
}
