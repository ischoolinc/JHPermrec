using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Aspose.Cells;
using JHPermrec.UpdateRecord.DAL;

namespace JHPermrec.UpdateRecord.GovernmentalDocument.NameList
{
    // 休學名冊
    class SuspensionStudentUpdateRecordList:ReportBuilder 
    {
        protected override void Build(XmlElement source, string location)
        {
            // 目前休學名冊只有高雄使用
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.KaoHsiung)
                ProcessKaoHsiung(source, location);
        }

        public override string Copyright
        {
            get { return "IntelliSchool"; }
        }

        // 處理高雄
        private void ProcessKaoHsiung(XmlElement source, string location)
        {
            // 資料轉換
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

            int peoTotalCount = 0;  // 總人數
            int peoBoyCount = 0;    // 男生人數
            int peoGirlCount = 0;   // 女生人數

            Workbook template = new Workbook();

            //從Resources把Template讀出來
            template.Open(new MemoryStream(GDResources.JSuspensionStudentUpdateRecordListTemplate), FileFormatType.Excel2003);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JSuspensionStudentUpdateRecordListTemplate), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            #region 初始變數
            int rowi = 0, rowj = 1;
            int recCount = 0;
            int totalRec = data.Count;

            #endregion
            rowj = 4;
            wb.Worksheets[0].Cells[rowi, 3].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + "  " + StudBatchUpdateRecEntity.GetContentSchoolYear() + "學年度第" + StudBatchUpdateRecEntity.GetContentSemester() + "學期");

                //將xml資料填入至excel
                foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
                {
                    recCount++;

                    #region 填入學生資料

                    wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber());
                    wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetClassName());
                    wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGender ());
                    wb.Worksheets[0].Cells[rowj, 3].PutValue(sburce.GetName ());
                    wb.Worksheets[0].Cells[rowj, 4].PutValue(sburce.GetIDNumber ());
                    if (sburce.GetGender() == "男")
                        peoBoyCount++;
                    if (sburce.GetGender() == "女")
                        peoGirlCount++;

                    peoTotalCount++;

                    if (!string.IsNullOrEmpty(sburce.GetBirthday ()))
                        wb.Worksheets[0].Cells[rowj, 5].PutValue(UpdateRecordUtil.ChangeDate1911(sburce.GetBirthday()));

                    if (!string.IsNullOrEmpty(sburce.GetUpdateDate()))
                        wb.Worksheets[0].Cells[rowj, 6].PutValue(UpdateRecordUtil.ChangeDate1911(sburce.GetUpdateDate ()));
                    
                    wb.Worksheets[0].Cells[rowj, 7].PutValue(sburce.GetAddress ());
                    wb.Worksheets[0].Cells[rowj, 8].PutValue(sburce.GetLastADNumber());
                    wb.Worksheets[0].Cells[rowj, 9].PutValue(sburce.GetUpdateDescription());

                    #endregion
                    
                    rowj++;

                    //回報進度
                    ReportProgress((int)(((double)recCount * 100.0) / ((double)totalRec)));
                }

            // 畫表
            Style st2 = wb.Styles[wb.Styles.Add()];
            StyleFlag sf2 = new StyleFlag();
            sf2.Borders = true;

            st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            int tmpMaxRow = 0, tmpMaxCol = 0;
            for (int wbIdx1 = 0; wbIdx1 < wb.Worksheets.Count; wbIdx1++)
            {
                tmpMaxRow = wb.Worksheets[wbIdx1].Cells.MaxDataRow - 3;
                tmpMaxCol = wb.Worksheets[wbIdx1].Cells.MaxDataColumn + 1;
                wb.Worksheets[wbIdx1].Cells.CreateRange(4, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            }


            // 統計人數
            rowj++;
            wb.Worksheets[0].Cells.CreateRange(rowj, 2, 1, 2).Merge();
            wb.Worksheets[0].Cells[rowj, 2].PutValue("男：" + peoBoyCount.ToString());
            wb.Worksheets[0].Cells[rowj, 4].PutValue("女：" + peoGirlCount.ToString());
            wb.Worksheets[0].Cells[rowj, 8].PutValue("總計：" + peoTotalCount.ToString());
            wb.Worksheets[0].Cells.CreateRange(rowj + 1, 0, 1, 10).Merge();
//            wb.Worksheets[0].Cells[rowj + 1, 0].PutValue("校長                                                          教務主任                                                          註冊組長                                                          核對員");
            wb.Worksheets[0].Cells[rowj + 1, 0].PutValue("核對員                                                          註冊組長                                                          教務主任                                                          校長");


            //儲存
            wb.Save(location, FileFormatType.Excel2003);        
        }

        public override string Description
        {
            get { return "中部辦公室95年11月編印管理手冊規範格式"; }
        }

        public override string ReportName
        {
            get { return "休學學生名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }

    }
}
