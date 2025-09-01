using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.Xml;
using System.IO;
using JHPermrec.UpdateRecord.DAL;


namespace JHPermrec.UpdateRecord.GovernmentalDocument.NameList
{
    // 處理新生名冊
    public class EnrollmentList : ReportBuilder
    {
        protected override void Build(System.Xml.XmlElement source, string location)
        {
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.HsinChu)
                ProcessHsinChu(source, location);

            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.KaoHsiung)
                ProcessKaoHsiung(source, location);

            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.TaiChung)
                ProcessTaiChung(source, location);
        }

        private string GetBirthdateWithoutSlash(string orig)
        {
            if (string.IsNullOrEmpty(orig)) return orig;
            string[] array = orig.Split('/');
            return array[0] + array[1].PadLeft(2, '0') + array[2].PadLeft(2, '0');
        }

        public override string Copyright
        {
            get { return "IntelliSchool"; }
        }

        
        // 處理新竹市樣版
        private void ProcessHsinChu(System.Xml.XmlElement source, string location)
        {
            // 資料轉換
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

            #region 建立 Excel

            //從 Resources 將新生名冊template讀出來
            Workbook template = new Workbook();
            template.Open(new MemoryStream(GDResources.JEnrollmentListTemplate_HsinChu), FileFormatType.Excel2003);

            //產生 excel
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JEnrollmentListTemplate_HsinChu), FileFormatType.Excel2003);
            #endregion

            //#region 複製樣式-預設樣式、欄寬

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            int recCount = 0;
            int totalRec = data.Count;

            Worksheet wst = wb.Worksheets[0];
            wst.Name = "新生名冊";

            string SchoolInfoAndSchoolYear = StudBatchUpdateRecEntity.GetContentSchoolName() + "  " + StudBatchUpdateRecEntity.GetContentSchoolYear () + wst.Cells[0, 0].StringValue;
            wst.Cells[0, 0].PutValue(SchoolInfoAndSchoolYear);
            int row = 2;
             #region 異動紀錄
                
                //將xml資料填入至excel
                foreach (StudBatchUpdateRecContentEntity sburce in data.Values )
                {
                    wst.Cells[row, 0].PutValue(sburce.GetClassName ());
                    wst.Cells[row, 1].PutValue(sburce.GetName ());
                    wst.Cells[row, 2].PutValue(sburce.GetStudentNumber());
                    wst.Cells[row, 3].PutValue(sburce.GetIDNumber ());
                    
                    DateTime dt;
                    string strDate = "";
                    if (DateTime.TryParse(sburce.GetBirthday(), out dt))
                    {
                        strDate = "民國" + (dt.Year - 1911) + "年" + dt.Month + "月" + dt.Day + "日";
                    }
                    wst.Cells[row, 4].PutValue(strDate);
                    wst.Cells[row, 5].PutValue(sburce.GetGender());
                    wst.Cells[row, 6].PutValue(sburce.GetGuardian());
                    wst.Cells[row, 7].PutValue(sburce.GetAddress ());
                    wst.Cells[row, 8].PutValue(sburce.GetPrimarySchoolName());
                    wst.Cells[row, 9].PutValue(sburce.GetComment());

                    row++;
                    recCount++;
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
                wb.Worksheets[wbIdx1].Cells.CreateRange(1 , 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            }

            //儲存 Excel
            wb.Save(location, FileFormatType.Excel2003);
        
        
        }


        // 處理高雄市樣版
        private void ProcessKaoHsiung(System.Xml.XmlElement source, string location)
        {
            // 資料轉換
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

            #region 建立 Excel
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

            string strPrintDate = UpdateRecordUtil.ChangeDate1911(DateTime.Now.ToString());


            //從 Resources 將新生名冊template讀出來
            Workbook template = new Workbook();
            //template.Worksheets[0].PageSetup.
            template.Open(new MemoryStream(GDResources.JEnrollmentListTemplate), FileFormatType.Excel2003);

            //產生 excel
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JEnrollmentListTemplate), FileFormatType.Excel2003);
            #endregion


            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;


            int rowi = 0, rowj = 1;            
            int recCount = 0;
            int totalRec = data.Count;

            rowj = 5;
                wb.Worksheets[0].Cells[rowi, 4].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + "  " + StudBatchUpdateRecEntity.GetContentSchoolYear() + "學年度第" + StudBatchUpdateRecEntity.GetContentSemester() + "學期");            
                wb.Worksheets[0].Cells[rowi, 9].PutValue("列印日期：" + strPrintDate);
                wb.Worksheets[0].Cells[rowi + 1, 9].PutValue("列印時間：" + DateTime.Now.ToLongTimeString());
                
                //將xml資料填入至excel
                foreach (StudBatchUpdateRecContentEntity sburce in data.Values )
                {

                    recCount++;
                    //將學生資料填入適當的位置內
                    wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber ());
                    wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetClassName ());
                    wb.Worksheets[0].Cells[rowj, 3].PutValue(sburce.GetName ());

                    wb.Worksheets[0].Cells[rowj, 4].PutValue(sburce.GetIDNumber ());

                    wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGender ());

                    if (sburce.GetGender() == "男")
                        peoBoyCount++;
                    if (sburce.GetGender() == "女")
                        peoGirlCount++;



                    if (sburce.GetBirthday () != "")
                    {

                        wb.Worksheets[0].Cells[rowj, 5].PutValue(UpdateRecordUtil.ChangeDate1911(sburce.GetBirthday ()));
                    }

                    if (sburce.GetEnrollmentSchoolYear ().Trim() != "")
                    {
                        wb.Worksheets[0].Cells[rowj, 6].PutValue(UpdateRecordUtil.getChineseYearStr(sburce.GetEnrollmentSchoolYear ()));
                        wb.Worksheets[0].Cells[rowj, 7].PutValue(UpdateRecordUtil.getMonthStr(sburce.GetEnrollmentSchoolYear(), false));
                    }
                    wb.Worksheets[0].Cells[rowj, 8].PutValue(sburce.GetPrimarySchoolName ());
                //wb.Worksheets[0].Cells[rowj, 9].PutValue(sburce.GetPermanentAddress());

                wb.Worksheets[0].Cells[rowj, 9].PutValue(sburce.GetAddress());

                wb.Worksheets[0].Cells[rowj, 10].PutValue(sburce.GetComment ());

                    peoTotalCount++;
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
                tmpMaxRow = wb.Worksheets[wbIdx1].Cells.MaxDataRow-4;
                tmpMaxCol = wb.Worksheets[wbIdx1].Cells.MaxDataColumn + 1;
                wb.Worksheets[wbIdx1].Cells.CreateRange(5, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            }

            // 統計人數
            rowj++;
            wb.Worksheets[0].Cells[rowj , 2].PutValue("男：" + peoBoyCount.ToString());
            wb.Worksheets[0].Cells[rowj, 4].PutValue("女：" + peoGirlCount.ToString());
            wb.Worksheets[0].Cells[rowj, 6].PutValue("總計：" + peoTotalCount.ToString());
//            wb.Worksheets[0].Cells[rowj + 1, 0].PutValue("校長                                                          教務主任                                                          註冊組長                                                          核對員");
            wb.Worksheets[0].Cells[rowj + 1, 0].PutValue("核對員                                                          註冊組長                                                          教務主任                                                          校長");


            // 顯示頁
            PageSetup pg = wb.Worksheets[0].PageSetup;
            string tmp = "&12 " + tmpRptY + "年" + tmpRptM + "月 填報" + "共&N頁";
            pg.SetHeader(2, tmp);

            //儲存 Excel
            wb.Save(location, FileFormatType.Excel2003);
        
        }

       
        // 處理台中縣樣版
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


            //從 Resources 將新生名冊template讀出來
            Workbook template = new Workbook();
            //template.Worksheets[0].PageSetup.
            template.Open(new MemoryStream(GDResources.JEnrollmentListTemplate_TaiChung), FileFormatType.Excel2003);

            //產生 excel
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JEnrollmentListTemplate_TaiChung), FileFormatType.Excel2003);
            #endregion

            #region 複製樣式-預設樣式、欄寬

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            #endregion


            int rowj = 1;
            
            int recCount = 0;
            int totalRec = data.Count;

            

            wb.Worksheets[0].Cells[0, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + "  國民中學入學學生名冊");
            wb.Worksheets[0].Cells[1, 10].PutValue(tmpRptY + "年" + tmpM + "月填製");

            rowj = 4;

                wb.Worksheets[0].Cells[1, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolYear () + "學年度第" + StudBatchUpdateRecEntity.GetContentSemester() + "學期 1年級");

               
                //將xml資料填入至excel
                foreach (StudBatchUpdateRecContentEntity sburce in data.Values )
                {

                    recCount++;
                    //將學生資料填入適當的位置內
                    wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber());
                    wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetName());
                    wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGender());
                    wb.Worksheets[0].Cells[rowj, 3].PutValue(sburce.GetIDNumber());

                        DateTime dt;
                        if(DateTime.TryParse (sburce.GetBirthday (),out dt))
                        {
                            wb.Worksheets[0].Cells[rowj, 4].PutValue("" +(dt.Year - 1911));
                            wb.Worksheets[0].Cells[rowj, 5].PutValue("" +dt.Month );
                            wb.Worksheets[0].Cells[rowj, 6].PutValue("" + dt.Day);
                        }


                    if (sburce.GetEnrollmentSchoolYear() != "")
                    {
                        wb.Worksheets[0].Cells[rowj, 7].PutValue(UpdateRecordUtil.getChineseYearStr(sburce.GetEnrollmentSchoolYear()));
                        wb.Worksheets[0].Cells[rowj, 8].PutValue(UpdateRecordUtil.getMonthStr(sburce.GetEnrollmentSchoolYear (), false));
                    }
                    wb.Worksheets[0].Cells[rowj, 9].PutValue(sburce.GetPrimarySchoolName ());
                    wb.Worksheets[0].Cells[rowj, 10].PutValue(sburce.GetAddress ());
                    

//                    peoTotalCount++;
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

            //合計人數
            wb.Worksheets[0].Cells[rowj, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 0].PutValue("合計");
            wb.Worksheets[0].Cells[rowj, 1].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 1].PutValue("" + data.Count + " 名");
            wb.Worksheets[0].Cells[rowj, 3].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 3].PutValue("以下空白");

            //儲存 Excel
            wb.Save(location, FileFormatType.Excel2003);

        }

        public override string Description
        {
            get { return "中部辦公室95年11月編印管理手冊規範格式"; }
        }

        public override string ReportName
        {
            get { return "新生名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
