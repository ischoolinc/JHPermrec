using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.Xml;
using System.IO;
using JHPermrec.UpdateRecord.DAL;

namespace JHPermrec.UpdateRecord.GovernmentalDocument.NameList
{
    // 處理畢業名冊
    public class GraduatingStudentList : ReportBuilder
    {
        protected override void Build(System.Xml.XmlElement source, string location)
        {
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.HsinChu)
                ProcessHsinChu(source, location);

            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.KaoHsiung)
                ProcessKaoHsing(source, location);

            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.TaiChung)
                ProcessTaiChung(source, location);

        }


        // 處理新竹
        private void ProcessHsinChu(System.Xml.XmlElement source, string location)
        {
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
            template.Open(new MemoryStream(GDResources.JGraduateListTemplate_HsinChu), FileFormatType.Excel2003);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JGraduateListTemplate_HsinChu), FileFormatType.Excel2003);

            #region 複製樣式-預設樣式、欄寬

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;
            #endregion

            #region 初始變數

            int rowj = 1; 
            int recCount = 0;
            int totalRec = data.Count;

            #endregion

            wb.Worksheets[0].Cells[0, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + " 學年度畢修業 學生名冊");
            rowj = 2;

                #region 異動紀錄                

                //將xml資料填入至excel
                foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
                {
                    recCount++;

                    wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetClassName ());
                    wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetName());
                    wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetStudentNumber());
                    wb.Worksheets[0].Cells[rowj, 3].PutValue(sburce.GetIDNumber ());
                    //出生年月日
                    wb.Worksheets[0].Cells[rowj, 4].PutValue(DAL.DALTransfer.ConvertDateStr1(sburce.GetBirthday ()));

                    // 性別
                    wb.Worksheets[0].Cells[rowj, 5].PutValue(sburce.GetGender());

                    // 監護人姓名
                    wb.Worksheets[0].Cells[rowj, 6].PutValue(sburce.GetGuardian());

                    // 戶籍地址
                    wb.Worksheets[0].Cells[rowj, 7].PutValue(sburce.GetPermanentAddress());

                    wb.Worksheets[0].Cells[rowj, 8].PutValue(sburce.GetGraduate());

                    wb.Worksheets[0].Cells[rowj, 9].PutValue(sburce.GetGraduateCertificateNumber());

                    wb.Worksheets[0].Cells[rowj, 10].PutValue(sburce.GetComment());
                    rowj ++;

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
                tmpMaxRow = wb.Worksheets[wbIdx1].Cells.MaxDataRow - 1;
                tmpMaxCol = wb.Worksheets[wbIdx1].Cells.MaxDataColumn + 1;
                wb.Worksheets[wbIdx1].Cells.CreateRange(2, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            }
            //儲存
            wb.Save(location, FileFormatType.Excel2003);

        }

        // 處理高雄
        private void ProcessKaoHsing(System.Xml.XmlElement source, string location)
        {
            // 資料轉換
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

            int peoTotalCount = 0;  // 總人數
            int peoBoyCount = 0;    // 男生人數
            int peoGirlCount = 0;   // 女生人數

            Workbook template = new Workbook();
            string strPrintDate = UpdateRecordUtil.ChangeDate1911(DateTime.Now.ToString());
            
            //從Resources把Template讀出來
            template.Open(new MemoryStream(GDResources.JGraduatingStudentListTemplate), FileFormatType.Excel2003);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JGraduatingStudentListTemplate), FileFormatType.Excel2003);

            #region 複製樣式-預設樣式、欄寬

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            #endregion

            #region 初始變數
            int rowi = 0, rowj = 1;            
            int recCount = 0;
            int totalRec = data.Count;

            #endregion

            rowj = 5;
                int i = 0;
                 wb.Worksheets[0].Cells[rowi, 4].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + StudBatchUpdateRecEntity.GetContentSchoolYear () + "學年度 第" + StudBatchUpdateRecEntity.GetContentSemester() + "學期");
                 wb.Worksheets[0].Cells[rowi + 2, 13].PutValue("列印日期：" + strPrintDate);

                #region 異動紀錄

                //將xml資料填入至excel
                foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
                {
                    recCount++;

                    // 合併欄位
                    wb.Worksheets[0].Cells.CreateRange(rowj, 6, 1, 3).Merge ();
                    //wb.Worksheets[0].Cells.CreateRange(rowj, 11, 1, 2).Merge();
                    //wb.Worksheets[0].Cells.CreateRange(rowj, 9, 1, 2).Merge();

                    //將學生資料填入適當的位置內
                    // 學號
                    wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber ());
                    // 班級
                    wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetClassName());
                    // 座號
                    wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetSeatNo());

                    // 身分證號
                    wb.Worksheets[0].Cells[rowj, 3].PutValue(sburce.GetIDNumber());

                    // 性別
                    wb.Worksheets[0].Cells[rowj, 4].PutValue(sburce.GetGender ());
                    // 姓名
                    wb.Worksheets[0].Cells[rowj, 5].PutValue(sburce.GetName ());

                    //wb.Worksheets[0].Cells[rowj, 3].PutValue(sburce.GetBirthPlace ());

                    if (sburce.GetGender () == "男")
                        peoBoyCount++;
                    if (sburce.GetGender() == "女")
                        peoGirlCount++;

                    // 出生日
                    DateTime dt;
                    if (DateTime.TryParse(sburce.GetBirthday(), out dt))
                    {
                        string tmpBirth = (dt.Year-1911) + "年";
                        if (dt.Month < 10)
                            tmpBirth += "0" + dt.Month+"月";
                        else
                            tmpBirth += dt.Month + "月"; 
                        if (dt.Day < 10)
                            tmpBirth += "0" + dt.Day+"日";
                        else
                            tmpBirth += dt.Day+"日";

                        wb.Worksheets[0].Cells[rowj, 6].PutValue(tmpBirth);
                    }

                    // 入學年月
                    if (sburce.GetEnrollmentSchoolYear () != "")
                    {
                        //string EnrollYearMonth = UpdateRecordUtil.getChineseYearStr(sburce.GetEnrollmentSchoolYear ()) + UpdateRecordUtil.getMonthStr(sburce.GetEnrollmentSchoolYear (), true);

                        string EnrolSchoolYear = sburce.GetEnrollmentSchoolYear().Trim();
                        string EnrollYear="";
                        string EnrollMonth = "";

                        if (EnrolSchoolYear.Length == 6)
                        { 
                            int sc;
                            if (int.TryParse(EnrolSchoolYear.Substring(0, 4), out sc))
                            {
                                EnrollYear = (sc - 1911).ToString();
                                EnrollMonth = EnrolSchoolYear.Substring(4, 2);
                            }
                        }

                        wb.Worksheets[0].Cells[rowj, 9].PutValue(EnrollYear);
                        wb.Worksheets[0].Cells[rowj, 10].PutValue(EnrollMonth);
                    }
                    // 畢業年月
                    //if (sburce.GetGraduateSchoolYear()!= "")
                    //{
                    //    string GraduateYearMonth = UpdateRecordUtil.getChineseYearStr(sburce.GetGraduateSchoolYear ()) + UpdateRecordUtil.getMonthStr(sburce.GetGraduateSchoolYear (), true);
                    //    wb.Worksheets[0].Cells[rowj, 9].PutValue(GraduateYearMonth);
                    //}
                    wb.Worksheets[0].Cells[rowj, 11].PutValue(sburce.GetGraduateCertificateNumber());
                    wb.Worksheets[0].Cells[rowj, 12].PutValue(sburce.GetLastADNumber());
                    wb.Worksheets[0].Cells[rowj, 13].PutValue(sburce.GetComment ());
                    #endregion
                    peoTotalCount++;
                    i++;
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
                tmpMaxRow = wb.Worksheets[wbIdx1].Cells.MaxDataRow - 4;
                tmpMaxCol = wb.Worksheets[wbIdx1].Cells.MaxDataColumn + 1;
                wb.Worksheets[wbIdx1].Cells.CreateRange(5, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            }

            // 統計人數
            rowj++;
            wb.Worksheets[0].Cells[rowj, 2].PutValue("男：" + peoBoyCount.ToString());
            wb.Worksheets[0].Cells[rowj, 4].PutValue("女：" + peoGirlCount.ToString());
            wb.Worksheets[0].Cells[rowj, 8].PutValue("總計：" + peoTotalCount.ToString());
//            wb.Worksheets[0].Cells[rowj + 1, 0].PutValue("校長                                                          教務主任                                                          註冊組長                                                          核對員");
            wb.Worksheets[0].Cells[rowj + 1, 0].PutValue("核對員                                                          註冊組長                                                          教務主任                                                          校長");


            //儲存
            wb.Save(location, FileFormatType.Excel2003);
        
        }

        // 處理台中
        private void ProcessTaiChung(System.Xml.XmlElement source, string location)
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
            template.Open(new MemoryStream(GDResources.JGraduateListTemplate_TaiChung), FileFormatType.Excel2003);
            
            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JGraduateListTemplate_TaiChung), FileFormatType.Excel2003);

            #region 複製樣式-預設樣式、欄寬

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;
            #endregion

            #region 初始變數

            int rowj = 1;
            
            int recCount = 0;
            int totalRec = data.Count;

            #endregion

            wb.Worksheets[0].Cells[0, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName ()+ " 畢業異動名冊");
            wb.Worksheets[0].Cells[1, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolYear () + "學年度");
            wb.Worksheets[0].Cells[1, 6].PutValue(tmpRptY + "年" + tmpRptM + "月填報");

            rowj = 4;
                

                //將xml資料填入至excel
                foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
                {
                    recCount++;
                    wb.Worksheets[0].Cells.CreateRange(rowj, 0, 2, 1).Merge();
                    wb.Worksheets[0].Cells.CreateRange(rowj, 1, 2, 1).Merge();
                    wb.Worksheets[0].Cells.CreateRange(rowj, 2, 2, 1).Merge();
                    wb.Worksheets[0].Cells.CreateRange(rowj + 1, 3, 1, 3).Merge();
                    wb.Worksheets[0].Cells.CreateRange(rowj, 6, 2, 1).Merge();
                    wb.Worksheets[0].Cells.CreateRange(rowj, 7, 2, 1).Merge();

                    wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber ());
                    wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetName ());
                    wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGender ());
                    wb.Worksheets[0].Cells[rowj, 3].PutValue(sburce.GetIDNumber ());
                    // 戶籍地址
                    wb.Worksheets[0].Cells[rowj+1, 3].PutValue(sburce.GetPermanentAddress ());
                    wb.Worksheets[0].Cells.SetRowHeight(rowj + 1, 50);

                    //出生年月日
                    wb.Worksheets[0].Cells[rowj, 4].PutValue(DAL.DALTransfer.ConvertDateStr1 (sburce.GetBirthday ()));

                    // 入學年月
                    string tempStr=sburce.GetEnrollmentSchoolYear ().Trim();
                    int enSchoolYearLen = tempStr.Length;
                    
                    string strEnYearMonth="";
                    if (enSchoolYearLen >0)
                    {
                        if (enSchoolYearLen == 4)
                            strEnYearMonth = tempStr.Substring(0, 2) + "." + tempStr.Substring(4, 2);

                        if (enSchoolYearLen == 6)
                        {
                            int year;
                            int.TryParse(tempStr.Substring(0, 4), out year);


                            strEnYearMonth = (year-1911) + "." + tempStr.Substring(4, 2);
                        }
                        wb.Worksheets[0].Cells[rowj, 5].PutValue(strEnYearMonth);
                    }
                    
                    string strDocNo = DAL.DALTransfer.ConvertDateStr1(sburce.GetLastADDate ())+"\n"+ sburce.GetLastADNumber ().Trim();
                    string GrdDocNo = DAL.DALTransfer.ConvertDateStr1(sburce.GetUpdateDate ()) +"\n"+ sburce.GetGraduateCertificateNumber().Trim();

                    wb.Worksheets[0].Cells[rowj, 6].PutValue(strDocNo);
                    wb.Worksheets[0].Cells[rowj, 7].PutValue(GrdDocNo);

                    rowj+=2;

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

            // 合計人數
            wb.Worksheets[0].Cells[rowj, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 0].PutValue("合計");
            wb.Worksheets[0].Cells[rowj, 1].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 1].PutValue("" + data.Values.Count + "名");
            wb.Worksheets[0].Cells[rowj, 3].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 3].PutValue("以下空白");

            //儲存
            wb.Save(location, FileFormatType.Excel2003);
            
        }


        //處理符號"/"
        private string GetBirthdateWithoutSlash(string orig)
        {
            if (string.IsNullOrEmpty(orig)) return orig;
            string[] array = orig.Split('/');
            int chang;
            if (array[0].Length == 4)
            {
                chang = int.Parse(array[0]) - 1911;
            }
            else
            {
                chang = int.Parse(array[0]);
            }
            return chang.ToString() + array[1].PadLeft(2, '0') + array[2].PadLeft(2, '0');
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
            get { return "畢業名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
