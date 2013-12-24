using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.ComponentModel;
using System.Windows.Forms;

namespace StudentTools1.Export
{
    class StudDataExporter
    {
        /// <summary>
        /// Excel 欄位
        /// </summary>
        List<ColumnIndexEntity> _ColumnInfo;
        /// <summary>
        /// 學生資料
        /// </summary>
        List<DAL.StudDataEntity> _StudDataList;

        BackgroundWorker _bkWork;

        /// <summary>
        /// 
        /// </summary>
        public StudDataExporter(List<ColumnIndexEntity> ColumnInfo, List<DAL.StudDataEntity> Data)
        {
            _ColumnInfo = ColumnInfo;
            _StudDataList = Data;
            if (_StudDataList.Count == 0 || _ColumnInfo.Count == 0)
                FISCA.Presentation.Controls.MsgBox.Show("沒有資料.");
            // 執行
            Export();
        }

        /// <summary>
        /// 匯出報表
        /// </summary>
        private void Export()
        {
            _bkWork = new BackgroundWorker();
            _bkWork.DoWork += new DoWorkEventHandler(_bkWork_DoWork);
            _bkWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkWork_RunWorkerCompleted);
            _bkWork.RunWorkerAsync();
        
        }

        void _bkWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Workbook wb = (Workbook)e.Result;
            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\學生基本資料.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\學生基本資料.xls");
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "學生基本資料.xls";
                sd1.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd1.FileName, FileFormatType.Excel2003);
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            
        }

        void _bkWork_DoWork(object sender, DoWorkEventArgs e)
        {
            Workbook wb = new Workbook();

            // 建 Idx
            Dictionary<string, ColumnIndexEntity> ColumnIdxDict = new Dictionary<string, ColumnIndexEntity>();

            // 填入欄位
            foreach (ColumnIndexEntity cie in _ColumnInfo)
            {
                wb.Worksheets[0].Cells[0, cie.ColumnIndex].PutValue(cie.ColumnName);
                if (!ColumnIdxDict.ContainsKey(cie.ColumnName))
                    ColumnIdxDict.Add(cie.ColumnName, cie);
            }
            

            // 填入資料
            int rowIdx = 1;

            foreach (DAL.StudDataEntity sde in _StudDataList)
            {
                // 印表
                foreach (KeyValuePair<string,string> data in sde.GetData())
                {
                    if (ColumnIdxDict.ContainsKey(data.Key))
                    {
                        if(ColumnIdxDict[data.Key].Visible )
                            wb.Worksheets[0].Cells[rowIdx, ColumnIdxDict[data.Key].ColumnIndex].PutValue(data.Value);                    
                    }
                }
                rowIdx++;
            }
            wb.Worksheets[0].AutoFitColumns();
            e.Result = wb;
        }
    }
}
