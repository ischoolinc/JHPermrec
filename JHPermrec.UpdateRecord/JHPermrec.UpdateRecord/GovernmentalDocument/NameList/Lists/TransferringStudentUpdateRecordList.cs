using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.Xml;
using System.IO;
using JHPermrec.UpdateRecord.DAL;

namespace JHPermrec.UpdateRecord.GovernmentalDocument.NameList
{
    // 轉入名冊
    public class TransferringStudentUpdateRecordList : ReportBuilder
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



        public override string Copyright
        {
            get { return "IntelliSchool"; }
        }

        public override string Description
        {
            get { return "中部辦公室95年11月編印管理手冊規範格式"; }
        }

        // 處理新竹樣版
        private void ProcessHsinChu(System.Xml.XmlElement source, string location)
        {
            // 資料轉換
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

            Workbook template = new Workbook();

            //從Resources把Template讀出來
            template.Open(new MemoryStream(GDResources.JTransferStudentUpdateRecordTemplate_HsinChu), FileFormatType.Excel2003);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JTransferStudentUpdateRecordTemplate_HsinChu), FileFormatType.Excel2003);


            Worksheet wst = wb.Worksheets[0];
            wst.Name = "轉入異動名冊";


            #region 複製樣式-預設樣式、欄寬

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            string SchoolInfoAndSchoolYear = StudBatchUpdateRecEntity.GetContentSchoolName() + "  " + source.SelectSingleNode("@學年度").InnerText + wst.Cells[0, 0].StringValue;

            wst.Cells[0, 0].PutValue(SchoolInfoAndSchoolYear);

            #endregion

            #region 初始變數
            int recCount = 0;
            int totalRec = data.Count;

            #endregion

            int row = 2;
            //將xml資料填入至excel
            foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
            {
                wst.Cells[row, 0].PutValue(sburce.GetClassName());
                wst.Cells[row, 1].PutValue(sburce.GetName());
                wst.Cells[row, 2].PutValue(sburce.GetStudentNumber());
                wst.Cells[row, 3].PutValue(sburce.GetIDNumber());


                DateTime dt;
                string strDate = "";
                if (DateTime.TryParse(sburce.GetBirthday(), out dt))
                {
                    strDate = "民國" + (dt.Year - 1911) + "年" + dt.Month + "月" + dt.Day + "日";
                }
                wst.Cells[row, 4].PutValue(strDate);
                wst.Cells[row, 5].PutValue(sburce.GetGender());
                wst.Cells[row, 6].PutValue(sburce.GetGuardian());
                wst.Cells[row, 7].PutValue(sburce.GetAddress());
                wst.Cells[row, 8].PutValue(sburce.GetUpdateCodeType());
                wst.Cells[row, 9].PutValue(sburce.GetUpdateDescription());
                wst.Cells[row, 10].PutValue(sburce.GetImportExportSchool());
                wst.Cells[row, 11].PutValue(sburce.GetComment());

                recCount++;
                row++;
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
                tmpMaxRow = wb.Worksheets[wbIdx1].Cells.MaxDataRow;
                tmpMaxCol = wb.Worksheets[wbIdx1].Cells.MaxDataColumn + 1;
                wb.Worksheets[wbIdx1].Cells.CreateRange(1, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            }

            //儲存
            wb.Save(location, FileFormatType.Excel2003);

        }

        // 處理高雄樣版
        private void ProcessKaoHsiung(System.Xml.XmlElement source, string location)
        {
            // 資料轉換
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);
            int peoTotalCount = 0;  // 總人數
            int peoBoyCount = 0;    // 男生人數
            int peoGirlCount = 0;   // 女生人數

            int tmpY, tmpM, tmpD;
            tmpY = DateTime.Now.Year;
            tmpM = DateTime.Now.Month;
            tmpD = DateTime.Now.Day;
            string tmpRptY, tmpRptM;
            string strPrintDate = (tmpY - 1911).ToString() + "/";
            tmpRptY = (tmpY - 1911).ToString();
            if (tmpM < 10)
            {
                strPrintDate += "0" + tmpM.ToString() + "/";
                tmpRptM = "0" + tmpM.ToString();
            }
            else
            {
                strPrintDate += tmpM.ToString() + "/";
                tmpRptM = tmpM.ToString();
            }
            if (tmpD < 10)
                strPrintDate += "0" + tmpD.ToString();
            else
                strPrintDate += tmpD.ToString();

            Workbook template = new Workbook();

            //從Resources把Template讀出來
            template.Open(new MemoryStream(GDResources.JTransferringStudentUpdateRecordListTemplate), FileFormatType.Excel2003);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JTransferringStudentUpdateRecordListTemplate), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];

            #region 複製樣式-預設樣式、欄寬

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            #endregion

            #region 初始變數
            int rowi = 0, rowj = 1;
            int recCount = 0;
            int totalRec = data.Count;

            #endregion
            rowj = 4;

            wb.Worksheets[0].Cells[rowi, 4].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + "  " + StudBatchUpdateRecEntity.GetContentSchoolYear() + "學年度第" + StudBatchUpdateRecEntity.GetContentSemester() + "學期");
            wb.Worksheets[0].Cells[rowi, 8].PutValue("列印日期：" + strPrintDate);
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
                {
                    wb.Worksheets[0].Cells[rowj, 5].PutValue(UpdateRecordUtil.ChangeDate1911(sburce.GetBirthday()));
                }
                if (!string.IsNullOrEmpty(sburce.GetUpdateDate()))
                {
                    wb.Worksheets[0].Cells[rowj, 6].PutValue(UpdateRecordUtil.ChangeDate1911(sburce.GetUpdateDate()));
                }
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

            //儲存
            wb.Save(location, FileFormatType.Excel2003);

        }

        // 處理台中樣版
        private void ProcessTaiChung(System.Xml.XmlElement source, string location)
        {
            // 資料轉換
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

            #region 建立 Excel
            int tmpY, tmpM;
            tmpY = DateTime.Now.Year;
            tmpM = DateTime.Now.Month;
            string tmpRptY, tmpRptM;
            tmpRptY = (tmpY - 1911).ToString();
            if (tmpM < 10)
                tmpRptM = "0" + tmpM.ToString();
            else
                tmpRptM = tmpM.ToString();

            string strPrintDate = UpdateRecordUtil.ChangeDate1911(DateTime.Now.ToString());

            // 台中轉入與新生樣式相同
            //從 Resources 將新生名冊template讀出來
            Workbook template = new Workbook();
            //template.Worksheets[0].PageSetup.
            //template.Open(new MemoryStream(GDResources.JEnrollmentListTemplate_TaiChung), FileFormatType.Excel2003);
            template.Open(new MemoryStream(GDResources.JTransferListTemplate_TaiChung), FileFormatType.Excel2003);
            //產生 excel
            Workbook wb = new Aspose.Cells.Workbook();
            //wb.Open(new MemoryStream(GDResources.JEnrollmentListTemplate_TaiChung), FileFormatType.Excel2003);
            wb.Open(new MemoryStream(GDResources.JTransferListTemplate_TaiChung), FileFormatType.Excel2003);
            #endregion

            #region 複製樣式-預設樣式、欄寬

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            #endregion

            #region 初始變數
            int rowj = 1;
            int recCount = 0;
            int totalRec = data.Count;

            #endregion
            wb.Worksheets[0].Cells[0, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + "  轉入學生名冊");
            wb.Worksheets[0].Cells[1, 6].PutValue(tmpRptY + "年" + tmpRptM + "月填報");
            //wb.Worksheets[0].Cells[0, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName () + "  國民中學入學學生名冊");
            //wb.Worksheets[0].Cells[1, 10].PutValue(tmpRptY + "年" + tmpM + "月填製");

            Range templateRow = template.Worksheets[0].Cells.CreateRange(4, 7, false);

            //string strGradeYear="";
            rowj = 4;
            //將xml資料填入至excel
            foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
            {
                //填入前先複製格式
                wb.Worksheets[0].Cells.CreateRange(rowj, 7, false).Copy(templateRow);
                //if (rowj == 4)
                //strGradeYear = sburce.GetClassYear ();

                recCount++;
                //將學生資料填入適當的位置內
                wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber());
                wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetName());
                wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGradeYear());
                wb.Worksheets[0].Cells[rowj, 3].PutValue(StudBatchUpdateRecEntity.GetContentSemester());

                //異動年月
                DateTime dt;
                if (DateTime.TryParse(sburce.GetUpdateDate(), out dt))
                {
                    wb.Worksheets[0].Cells[rowj, 4].PutValue("" + (dt.Year - 1911));
                    wb.Worksheets[0].Cells[rowj, 5].PutValue("" + dt.Month);
                }

                //異動情形
                wb.Worksheets[0].Cells[rowj, 6].PutValue(sburce.GetImportExportSchool());

                //原因
                wb.Worksheets[0].Cells[rowj, 7].PutValue(sburce.GetUpdateDescription());

                rowj++;

                //回報進度
                ReportProgress((int)(((double)recCount * 100.0) / ((double)totalRec)));
            }


            // Title
            //wb.Worksheets[0].Cells[1, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolYear() + "學年度第" + StudBatchUpdateRecEntity.GetContentSemester () + "學期 "+strGradeYear +"年級");
            wb.Worksheets[0].Cells[1, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolYear() + "學年度第" + StudBatchUpdateRecEntity.GetContentSemester() + "學期 異動：轉入");

            //合計人數
            wb.Worksheets[0].Cells[rowj, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 0].PutValue("合計");
            wb.Worksheets[0].Cells[rowj, 1].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 1].PutValue("" + data.Count + " 名");
            wb.Worksheets[0].Cells[rowj, 3].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 3].PutValue("以下空白");

            // 畫表
            //Style st2 = wb.Styles[wb.Styles.Add()];
            //StyleFlag sf2 = new StyleFlag();
            //sf2.Borders = true;

            //st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            //st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            //st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            //st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            //int tmpMaxRow = 0, tmpMaxCol = 0;
            //for (int wbIdx1 = 0; wbIdx1 < wb.Worksheets.Count; wbIdx1++)
            //{
            //    tmpMaxRow = wb.Worksheets[wbIdx1].Cells.MaxDataRow - 3;
            //    tmpMaxCol = wb.Worksheets[wbIdx1].Cells.MaxDataColumn + 1;
            //    wb.Worksheets[wbIdx1].Cells.CreateRange(4, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            //}

            //儲存 Excel
            wb.Save(location, FileFormatType.Excel2003);

        }

        public override string ReportName
        {
            get { return "轉入學生名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
