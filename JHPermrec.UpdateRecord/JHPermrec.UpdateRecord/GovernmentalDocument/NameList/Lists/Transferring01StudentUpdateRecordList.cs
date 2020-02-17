using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Aspose.Cells;
using Framework.Legacy;
using JHPermrec.UpdateRecord.DAL;

namespace JHPermrec.UpdateRecord.GovernmentalDocument.NameList
{
    // 轉出名冊
    class Transferring01StudentUpdateRecordList : ReportBuilder
    {
        protected override void Build(XmlElement source, string location)
        {
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.HsinChu)
                ProcessHsinChu(source, location);

            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.KaoHsiung)
                ProcessKaoHsiung(source, location);

            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.TaiChung)
                ProcessTaiChung(source, location);
        }


        // 處理新竹相關
        private void ProcessHsinChu(XmlElement source, string location)
        {
            // 資料轉換
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

            int tmpY, tmpM;
            tmpY = DateTime.Now.Year;
            tmpM = DateTime.Now.Month;
            string tmpRptY, tmpRptM;
            tmpRptY = (tmpY - 1911).ToString();
            if (tmpM < 10)
                tmpRptM = "0" + tmpM.ToString();
            else
                tmpRptM = tmpM.ToString();

            Workbook template = new Workbook();

            //從Resources把Template讀出來
            template.Open(new MemoryStream(GDResources.JTransfer01ListTemplate_HsinChu), FileFormatType.Excel2003);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JTransfer01ListTemplate_HsinChu), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];


            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            #region 初始變數
            int rowj = 1;
            int recCount = 0;
            int totalRec = data.Count;

            #endregion

            rowj = 4;

            wb.Worksheets[0].Cells[0, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + "　　　學　生　異　動　名　冊");

            wb.Worksheets[0].Cells[1, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolYear() + "學年度第" + StudBatchUpdateRecEntity.GetContentSemester() + "學期");
            wb.Worksheets[0].Cells[1, 8].PutValue(tmpRptY + "年" + tmpRptM + "月填報");

            //將xml資料填入至excel
            foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
            {
                recCount++;

                wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber());
                wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetName());
                //wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetClassYear ());// 穎驊更新，檢查 # 6202 客服，發現年級欄位抓錯，不應該找取班級年級，而該用異動年級
                wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGradeYear());
                wb.Worksheets[0].Cells[rowj, 3].PutValue(StudBatchUpdateRecEntity.GetContentSemester());
                DateTime dt;
                // 用西元轉換
                if (DateTime.TryParse(sburce.GetUpdateDate(), out dt))
                {
                    wb.Worksheets[0].Cells[rowj, 4].PutValue((dt.Year - 1911) + "");
                    wb.Worksheets[0].Cells[rowj, 5].PutValue((dt.Month + ""));
                }
                wb.Worksheets[0].Cells[rowj, 6].PutValue(sburce.GetUpdateCodeType());
                wb.Worksheets[0].Cells[rowj, 7].PutValue(sburce.GetUpdateDescription());
                wb.Worksheets[0].Cells[rowj, 8].PutValue(sburce.GetImportExportSchool());
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
            //儲存
            wb.Save(location, FileFormatType.Excel2003);


        }

        // 處理高雄相關
        private void ProcessKaoHsiung(XmlElement source, string location)
        {
            // 資料轉換
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

            int peoTotalCount = 0;  // 總人數
            int peoBoyCount = 0;    // 男生人數
            int peoGirlCount = 0;   // 女生人數

            int tmpY, tmpM;
            tmpY = DateTime.Now.Year;
            tmpM = DateTime.Now.Month;
            string tmpRptY, tmpRptM;
            tmpRptY = (tmpY - 1911).ToString();
            if (tmpM < 10)
                tmpRptM = "0" + tmpM.ToString();
            else
                tmpRptM = tmpM.ToString();

            Workbook template = new Workbook();

            //從Resources把Template讀出來
            template.Open(new MemoryStream(GDResources.JTransferring01StudentUpdateRecordListTemplate), FileFormatType.Excel2003);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JTransferring01StudentUpdateRecordListTemplate), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];

            #region 複製樣式-預設樣式、欄寬

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            #endregion

            #region 初始變數

            int rowi = 0, rowj = 1, numcount = 1, j = 0;
            int recCount = 0;
            int totalRec = data.Count;

            #endregion
            rowj = 4;

            wb.Worksheets[0].Cells[rowi, 4].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + "  " + StudBatchUpdateRecEntity.GetContentSchoolYear() + "學年第 " + StudBatchUpdateRecEntity.GetContentSemester() + "學期");
            wb.Worksheets[0].Cells[rowi, 8].PutValue("列印日期：" + UpdateRecordUtil.ChangeDate1911(DateTime.Now.ToString()));
            wb.Worksheets[0].Cells[rowi + 1, 8].PutValue("列印時間：" + DateTime.Now.ToLongTimeString());

            #region 異動紀錄

            //將xml資料填入至excel
            foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
            {
                recCount++;

                #region 填入學生資料

                wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber());
                wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetClassName());
                wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGender());
                wb.Worksheets[0].Cells[rowj, 3].PutValue(sburce.GetName());
                wb.Worksheets[0].Cells[rowj, 4].PutValue(sburce.GetIDNumber());

                if (sburce.GetGender() == "男")
                    peoBoyCount++;
                if (sburce.GetGender() == "女")
                    peoGirlCount++;

                peoTotalCount++;

                if (!string.IsNullOrEmpty(sburce.GetBirthday()))
                    wb.Worksheets[0].Cells[rowj, 5].PutValue(UpdateRecordUtil.ChangeDate1911(sburce.GetBirthday()));

                if (!string.IsNullOrEmpty(sburce.GetUpdateDate()))
                    wb.Worksheets[0].Cells[rowj, 6].PutValue(UpdateRecordUtil.ChangeDate1911(sburce.GetUpdateDate()));


                wb.Worksheets[0].Cells[rowj, 7].PutValue(sburce.GetImportExportSchool());
                wb.Worksheets[0].Cells[rowj, 8].PutValue(sburce.GetAddress());
                wb.Worksheets[0].Cells[rowj, 9].PutValue(sburce.GetLastADNumber());

                #endregion
                rowj++;
                //回報進度
                ReportProgress((int)(((double)recCount * 100.0) / ((double)totalRec)));
            }
            #endregion

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
            wb.Worksheets[0].Cells[rowj, 2].PutValue("男：" + peoBoyCount.ToString());
            wb.Worksheets[0].Cells[rowj, 4].PutValue("女：" + peoGirlCount.ToString());
            wb.Worksheets[0].Cells[rowj, 8].PutValue("總計：" + peoTotalCount.ToString());
            wb.Worksheets[0].Cells.CreateRange(rowj + 1, 0, 1, 10).Merge();
            //            wb.Worksheets[0].Cells[rowj + 1, 0].PutValue("校長                                                          教務主任                                                          註冊組長                                                          核對員");
            wb.Worksheets[0].Cells[rowj + 1, 0].PutValue("核對員                                                          註冊組長                                                          教務主任                                                          校長");


            // 顯示頁
            PageSetup pg = wb.Worksheets[0].PageSetup;
            string tmp = "&12 " + tmpRptY + "年" + tmpRptM + "月 填報" + "共&N頁";
            pg.SetHeader(2, tmp);


            wb.Save(location, FileFormatType.Excel2003);
        }

        // 處理台中相關
        private void ProcessTaiChung(XmlElement source, string location)
        {
            // 資料轉換
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

            int tmpY, tmpM;
            tmpY = DateTime.Now.Year;
            tmpM = DateTime.Now.Month;
            string tmpRptY, tmpRptM;
            tmpRptY = (tmpY - 1911).ToString();
            if (tmpM < 10)
                tmpRptM = "0" + tmpM.ToString();
            else
                tmpRptM = tmpM.ToString();

            Workbook template = new Workbook();

            //從Resources把Template讀出來
            template.Open(new MemoryStream(GDResources.JTransfer01ListTemplate_TaiChung), FileFormatType.Excel2003);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JTransfer01ListTemplate_TaiChung), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];

            #region 複製樣式-預設樣式、欄寬

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            #endregion

            #region 初始變數

            int rowj = 1;
            int recCount = 0;
            int totalRec = data.Count;

            #endregion
            rowj = 4;
            wb.Worksheets[0].Cells[0, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + " 學生異動名冊");
            string strSemester = StudBatchUpdateRecEntity.GetContentSemester();
            wb.Worksheets[0].Cells[1, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolYear() + "學年度第" + strSemester + "學期");
            wb.Worksheets[0].Cells[1, 7].PutValue(tmpRptY + "年" + tmpRptM + "月填報");

            #region 異動紀錄

            //將xml資料填入至excel
            foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
            {
                recCount++;

                wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber());
                wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetName());
                wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGradeYear());
                wb.Worksheets[0].Cells[rowj, 3].PutValue(strSemester);

                DateTime dt;
                // 用西元轉換
                if (DateTime.TryParse(sburce.GetUpdateDate(), out dt))
                {
                    wb.Worksheets[0].Cells[rowj, 4].PutValue("" + (dt.Year - 1911));
                    wb.Worksheets[0].Cells[rowj, 5].PutValue("" + dt.Month);
                }

                wb.Worksheets[0].Cells[rowj, 6].PutValue(sburce.GetUpdateDescription());
                wb.Worksheets[0].Cells[rowj, 7].PutValue(sburce.GetImportExportSchool());
                wb.Worksheets[0].Cells[rowj, 8].PutValue(sburce.GetComment());

                rowj++;

                //回報進度
                ReportProgress((int)(((double)recCount * 100.0) / ((double)totalRec)));
            }

            #endregion

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

            //合計人數
            wb.Worksheets[0].Cells[rowj, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 0].PutValue("合計");
            wb.Worksheets[0].Cells[rowj, 1].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 1].PutValue(data.Count + " 名");
            wb.Worksheets[0].Cells[rowj, 6].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 6].PutValue("以下空白");

            //儲存
            wb.Save(location, FileFormatType.Excel2003);

        }

        public override string Copyright
        {
            get { return "IntelliSchool"; }
        }

        public override string Description
        {
            get { return "中部辦公室95年11月編印管理手冊規範格式"; }
        }

        public override string ReportName
        {
            get { return "轉出學生名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }

    }
}
