using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.Xml;
using System.IO;
using JHPermrec.UpdateRecord.DAL;


namespace JHPermrec.UpdateRecord.GovernmentalDocument.NameList
{
    // 更正學籍名冊
    class UpdateStudentPermrecList : ReportBuilder
    {
        protected override void Build(XmlElement source, string location)
        {
            // 新竹
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.HsinChu)
                ProcessHsinChu(source, location);

            // 高雄
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.KaoHsiung)
                ProcessKaoHsiung(source, location);

            // 台中
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.TaiChung)
                ProcessTaichung(source, location);
        }


        /// <summary>
        /// 處理新竹
        /// </summary>
        /// <param name="source"></param>
        /// <param name="location"></param>
        private void ProcessHsinChu(XmlElement source, string location)
        {
            // 資料轉換
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

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
            template.Open(new MemoryStream(GDResources.JUpdateStudentPermrecTemplate), FileFormatType.Excel2003);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JUpdateStudentPermrecTemplate), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            int rowj = 5;
            int recCount = 0;
            int totalRec = data.Count;

            string SchoolNameTitle = StudBatchUpdateRecEntity.GetContentSchoolName() + "更　正　學　籍　名　冊";
            string SchoolYearSemesterTitle = StudBatchUpdateRecEntity.GetContentSchoolYear() + "學年度 第" + StudBatchUpdateRecEntity.GetContentSemester() + "學期";
            wb.Worksheets[0].Cells[0, 0].PutValue(SchoolNameTitle);
            wb.Worksheets[0].Cells[1, 0].PutValue(SchoolYearSemesterTitle);

            #region 異動紀錄

            //將xml資料填入至excel
            foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
            {
                recCount++;
                #region 填入學生資料

                //將學生資料填入適當的位置內
                wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber());
                wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetName());
                wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGender());

                DateTime orinialBirtday, LastUpdateDate, Birthday;
                if (DateTime.TryParse(sburce.GetBirthday(), out orinialBirtday))
                {
                    wb.Worksheets[0].Cells[rowj, 3].PutValue((orinialBirtday.Year - 1911).ToString());
                    wb.Worksheets[0].Cells[rowj, 4].PutValue(orinialBirtday.Month.ToString());
                    wb.Worksheets[0].Cells[rowj, 5].PutValue(orinialBirtday.Day.ToString());
                }

                string LastUpdateInfo = "";
                if (DateTime.TryParse(sburce.GetLastADDate(), out LastUpdateDate) == true)
                {
                    LastUpdateInfo = (LastUpdateDate.Year - 1911) + "/" + LastUpdateDate.Month + "/" + LastUpdateDate.Day;
                }
                LastUpdateInfo += sburce.GetLastADNumber();
                wb.Worksheets[0].Cells[rowj, 6].PutValue(LastUpdateInfo);


                if (!string.IsNullOrEmpty(sburce.GetNewBirthday()))
                {
                    if (DateTime.TryParse(sburce.GetNewBirthday(), out Birthday))
                    {
                        wb.Worksheets[0].Cells[rowj, 11].PutValue((Birthday.Year - 1911).ToString());
                        wb.Worksheets[0].Cells[rowj, 12].PutValue(Birthday.Month.ToString());
                        wb.Worksheets[0].Cells[rowj, 13].PutValue(Birthday.Day.ToString());
                    }
                }


                if (!string.IsNullOrEmpty(sburce.GetNewName()))
                    wb.Worksheets[0].Cells[rowj, 9].PutValue(sburce.GetNewName());

                if (!string.IsNullOrEmpty(sburce.GetNewGender()))
                    wb.Worksheets[0].Cells[rowj, 10].PutValue(sburce.GetNewGender());

                if (!string.IsNullOrEmpty(sburce.GetNewIDNumber()))
                    wb.Worksheets[0].Cells[rowj, 14].PutValue(sburce.GetNewIDNumber());

                wb.Worksheets[0].Cells[rowj, 15].PutValue(sburce.GetComment());

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
                tmpMaxRow = wb.Worksheets[wbIdx1].Cells.MaxDataRow;
                tmpMaxCol = wb.Worksheets[wbIdx1].Cells.MaxDataColumn + 1;
                wb.Worksheets[wbIdx1].Cells.CreateRange(1, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            }

            //儲存
            wb.Save(location, FileFormatType.Excel2003);
        }


        /// <summary>
        /// 處理高雄
        /// </summary>
        /// <param name="source"></param>
        /// <param name="location"></param>
        private void ProcessKaoHsiung(XmlElement source, string location)
        {
            // 資料轉換
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

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
            template.Open(new MemoryStream(GDResources.JUpdateStudentPermrecTemplate_KH), FileFormatType.Excel2003);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JUpdateStudentPermrecTemplate_KH), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            int rowj = 5;
            int recCount = 0;
            int totalRec = data.Count;

            string SchoolNameTitle = StudBatchUpdateRecEntity.GetContentSchoolName() + "更　正　學　籍　名　冊";
            string SchoolYearSemesterTitle = StudBatchUpdateRecEntity.GetContentSchoolYear() + "學年度 第" + StudBatchUpdateRecEntity.GetContentSemester() + "學期";
            wb.Worksheets[0].Cells[0, 0].PutValue(SchoolNameTitle);
            wb.Worksheets[0].Cells[1, 0].PutValue(SchoolYearSemesterTitle);

            #region 異動紀錄

            //將xml資料填入至excel
            foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
            {
                recCount++;
                #region 填入學生資料

                //將學生資料填入適當的位置內
                wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber());
                wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetName());
                wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGender());

                DateTime orinialBirtday, LastUpdateDate, Birthday;
                if (DateTime.TryParse(sburce.GetBirthday(), out orinialBirtday))
                {
                    wb.Worksheets[0].Cells[rowj, 3].PutValue((orinialBirtday.Year - 1911).ToString());
                    wb.Worksheets[0].Cells[rowj, 4].PutValue(orinialBirtday.Month.ToString());
                    wb.Worksheets[0].Cells[rowj, 5].PutValue(orinialBirtday.Day.ToString());
                }

                string LastUpdateInfo = "";
                if (DateTime.TryParse(sburce.GetLastADDate(), out LastUpdateDate) == true)
                {
                    LastUpdateInfo = (LastUpdateDate.Year - 1911) + "/" + LastUpdateDate.Month + "/" + LastUpdateDate.Day;
                }
                LastUpdateInfo += sburce.GetLastADNumber();
                wb.Worksheets[0].Cells[rowj, 6].PutValue(LastUpdateInfo);


                if (!string.IsNullOrEmpty(sburce.GetNewBirthday()))
                {
                    if (DateTime.TryParse(sburce.GetNewBirthday(), out Birthday))
                    {
                        wb.Worksheets[0].Cells[rowj, 11].PutValue((Birthday.Year - 1911).ToString());
                        wb.Worksheets[0].Cells[rowj, 12].PutValue(Birthday.Month.ToString());
                        wb.Worksheets[0].Cells[rowj, 13].PutValue(Birthday.Day.ToString());
                    }
                }


                if (!string.IsNullOrEmpty(sburce.GetNewName()))
                    wb.Worksheets[0].Cells[rowj, 9].PutValue(sburce.GetNewName());

                if (!string.IsNullOrEmpty(sburce.GetNewGender()))
                    wb.Worksheets[0].Cells[rowj, 10].PutValue(sburce.GetNewGender());

                if (!string.IsNullOrEmpty(sburce.GetNewIDNumber()))
                    wb.Worksheets[0].Cells[rowj, 14].PutValue(sburce.GetNewIDNumber());

                wb.Worksheets[0].Cells[rowj, 15].PutValue(sburce.GetComment());

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
                tmpMaxRow = wb.Worksheets[wbIdx1].Cells.MaxDataRow;
                tmpMaxCol = wb.Worksheets[wbIdx1].Cells.MaxDataColumn + 1;
                wb.Worksheets[wbIdx1].Cells.CreateRange(1, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            }

            //儲存
            wb.Save(location, FileFormatType.Excel2003);
        }


        /// <summary>
        /// 處理台中
        /// </summary>
        /// <param name="source"></param>
        /// <param name="location"></param>
        private void ProcessTaichung(XmlElement source, string location)
        {
            // 資料轉換
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

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
            template.Open(new MemoryStream(GDResources.JUpdateStudentPermrecListTemplate_TaiChung), FileFormatType.Excel2003);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JUpdateStudentPermrecListTemplate_TaiChung), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            int rowj = 5;
            int recCount = 0;
            int totalRec = data.Count;

            //表頭
            wb.Worksheets[0].Cells[0, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + " 更正學籍名冊");

            ////數字學期轉換成國字學期
            if (StudBatchUpdateRecEntity.GetContentSemester() == "1")
                wb.Worksheets[0].Cells[1, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolYear() + "學年度 第一學期");
            else if (StudBatchUpdateRecEntity.GetContentSemester() == "2")
                wb.Worksheets[0].Cells[1, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolYear() + "學年度 第二學期");
            else
                wb.Worksheets[0].Cells[1, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolYear() + "學年度 第" + StudBatchUpdateRecEntity.GetContentSemester() + "學期");

            wb.Worksheets[0].Cells[1, 14].PutValue(tmpRptY + "年" + tmpRptM + "月填報");

            #region 異動紀錄

            //將xml資料填入至excel
            foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
            {
                recCount++;
                #region 填入學生資料

                //將學生資料填入適當的位置內
                wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber());
                wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetName());
                wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGender());

                DateTime orinialBirtday, LastUpdateDate, Birthday;
                if (DateTime.TryParse(sburce.GetBirthday(), out orinialBirtday))
                {
                    wb.Worksheets[0].Cells[rowj, 3].PutValue((orinialBirtday.Year - 1911).ToString());
                    wb.Worksheets[0].Cells[rowj, 4].PutValue(orinialBirtday.Month.ToString());
                    wb.Worksheets[0].Cells[rowj, 5].PutValue(orinialBirtday.Day.ToString());
                }

                string LastUpdateInfo = "";
                if (DateTime.TryParse(sburce.GetLastADDate(), out LastUpdateDate) == true)
                {
                    LastUpdateInfo = (LastUpdateDate.Year - 1911) + "/" + LastUpdateDate.Month + "/" + LastUpdateDate.Day;
                }
                LastUpdateInfo += sburce.GetLastADNumber();
                wb.Worksheets[0].Cells[rowj, 6].PutValue(LastUpdateInfo);


                if (!string.IsNullOrEmpty(sburce.GetNewBirthday()))
                {
                    if (DateTime.TryParse(sburce.GetNewBirthday(), out Birthday))
                    {
                        wb.Worksheets[0].Cells[rowj, 11].PutValue((Birthday.Year - 1911).ToString());
                        wb.Worksheets[0].Cells[rowj, 12].PutValue(Birthday.Month.ToString());
                        wb.Worksheets[0].Cells[rowj, 13].PutValue(Birthday.Day.ToString());
                    }
                }


                if (!string.IsNullOrEmpty(sburce.GetNewName()))
                    wb.Worksheets[0].Cells[rowj, 9].PutValue(sburce.GetNewName());

                if (!string.IsNullOrEmpty(sburce.GetNewGender()))
                    wb.Worksheets[0].Cells[rowj, 10].PutValue(sburce.GetNewGender());

                if (!string.IsNullOrEmpty(sburce.GetNewIDNumber()))
                    wb.Worksheets[0].Cells[rowj, 14].PutValue(sburce.GetNewIDNumber());

                wb.Worksheets[0].Cells[rowj, 16].PutValue(sburce.GetComment());

                #endregion


                rowj++;

                //回報進度
                ReportProgress((int)(((double)recCount * 100.0) / ((double)totalRec)));
            }

            #endregion

            //合計人數
            wb.Worksheets[0].Cells[rowj, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 0].PutValue("合計");
            wb.Worksheets[0].Cells[rowj, 1].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 1].PutValue(data.Count + " 名");

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
            get { return "更正學籍學生名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }


    }
}
