using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.Xml;
using System.Windows.Forms;
using K12.Data;
using K12.Data.Configuration;
//using Framework;


namespace Student_JSBT
{
    class StudSBTManager
    {
        /// <summary>
        /// 依班級座號排序
        /// </summary>
        /// <param name="StudInfoEntityList"></param>
        /// <returns></returns>
        public List<StudInfoEntity> SortDataByClassSeatNo(List<StudInfoEntity> StudInfoEntityList)
        {
            StudInfoEntityList.Sort(new Comparison<StudInfoEntity>(SorterByClassSeatNo));
            return StudInfoEntityList;
        }


        /// <summary>
        /// 依學號排序
        /// </summary>
        /// <param name="StudInfoEntityList"></param>
        /// <returns></returns>
        public List<StudInfoEntity> SortDataByStudentNum(List<StudInfoEntity> StudInfoEntityList)
        {
            StudInfoEntityList.Sort(new Comparison<StudInfoEntity>(SorterByStudentNum));
            return StudInfoEntityList;
        }        
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static XmlDocument GetDataFormSystem()
        {
            School.Configuration.Sync("國中學生基本學力資料對照");
            ConfigData cd = School.Configuration["國中學生基本學力資料對照"];
            XmlDocument doc = new XmlDocument();

            if (string.IsNullOrEmpty(cd["XMLData"]))
                doc.LoadXml(Properties.Resources.StudentJSBT_conf);
            else
                doc.LoadXml(cd["XMLData"]);
            return doc;
        }

        public static void SaveDataToSystem(XmlDocument XmlDoc)
        {
            
            ConfigData cd = School.Configuration["國中學生基本學力資料對照"];

            if (XmlDoc == null)
                cd["XMLData"] = "";
            else
                cd["XMLData"] = XmlDoc.OuterXml;
            cd.Save();
        }
        
        public void ExportDataToExcel(List<StudInfoEntity> StudInfoEntityList)
        {
            Workbook wb = new Workbook();
            Worksheet wst = wb.Worksheets[0];
            wst.Name = "學生基本學力資料";
            Dictionary<string, int> ColumnIdx = new Dictionary<string, int>();

            ColumnIdx.Add("考區／考場代碼", 0);
            ColumnIdx.Add("學校代碼", 1);
            ColumnIdx.Add("報名序號", 2);
            ColumnIdx.Add("學號", 3);
            ColumnIdx.Add("班級", 4);
            ColumnIdx.Add("座號", 5);
            ColumnIdx.Add("學生姓名", 6);
            ColumnIdx.Add("身分證號", 7);
            ColumnIdx.Add("性別", 8);
            ColumnIdx.Add("出生年", 9);
            ColumnIdx.Add("出生月", 10);
            ColumnIdx.Add("出生日", 11);
            ColumnIdx.Add("畢業學校代碼", 12);
            ColumnIdx.Add("畢業年度", 13);
            ColumnIdx.Add("畢肄業", 14);
            ColumnIdx.Add("學生身分", 15);
            ColumnIdx.Add("身心障礙", 16);
            ColumnIdx.Add("分發區", 17);            
            ColumnIdx.Add("低收入戶", 18);
            ColumnIdx.Add("失業勞工子女", 19);
            //ColumnIdx.Add("中低收入戶", 20);
            ColumnIdx.Add("資料授權", 20);
            ColumnIdx.Add("家長姓名", 21);
            ColumnIdx.Add("緊急連絡電話", 22);
            ColumnIdx.Add("郵遞區號", 23);
            ColumnIdx.Add("地址", 24);
            ColumnIdx.Add("手機", 25);

            // 標頭
            foreach (KeyValuePair<string, int> val in ColumnIdx)
                wst.Cells[0, val.Value].PutValue(val.Key);
            int RowIdx =1;

            // 設欄寬
            if (StudInfoEntityList.Count > 0)
            {
                // default
                wst.Cells.SetColumnWidth(0, 2);
                wst.Cells.SetColumnWidth(2, 5);
                wst.Cells.SetColumnWidth(17, 2);
                wst.Cells.SetColumnWidth(20, 1);

                foreach (KeyValuePair<string, int> val in ColumnIdx)
                {
                    if (StudInfoEntityList[0].GetDataCellEntity(val.Key).FieldLenght > 0)
                        wst.Cells.SetColumnWidth(val.Value, StudInfoEntityList[0].GetDataCellEntity(val.Key).FieldLenght);
                }
            }

            foreach (StudInfoEntity sie in StudInfoEntityList)
            {
                foreach (KeyValuePair<string, int> ColIdx in ColumnIdx)
                {
                    
                    if((ColIdx.Value >=2 && ColIdx.Value <=5) || ColIdx.Value >=9 && ColIdx.Value <=11)
                        wst.Cells[RowIdx, ColIdx.Value].Style.HorizontalAlignment = TextAlignmentType.Right;
                    else
                        wst.Cells[RowIdx, ColIdx.Value].Style.HorizontalAlignment = TextAlignmentType.Left;
                    
                    wst.Cells[RowIdx, ColIdx.Value].PutValue(sie.GetDataCellEntity(ColIdx.Key).Value);
                }
                RowIdx ++;
            }

            

            try
            {

                wb.Save(Application.StartupPath + "\\Reports\\學生基本學力資料.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\學生基本學力資料.xls");                
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "學生基本學力資料.xls";
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
        //SaveSTCDataToSystem
        //GetSTCDataFromSystem

        public List<StudInfoEntity> GetStudInfoData(List<string> StudIDList)
        {
            return DALTransfer.GetStudentEntityList(StudIDList);
        }

        // 學號
        private int SorterByStudentNum(StudInfoEntity x, StudInfoEntity y)
        {
            int intX, intY;

            int.TryParse(x.StudentNumber, out intX);
            int.TryParse(y.StudentNumber, out intY);

            if (intX == 0 || x.StudentNumber == "")
                intX = int.MaxValue;

            if (intY == 0 || y.StudentNumber == "")
                intY = int.MaxValue;

            return intX.CompareTo(intY);
        }

        // 班座
        private int SorterByClassSeatNo(StudInfoEntity x, StudInfoEntity y)
        {
            string strX, strY;
            strX = strY = string.Empty;
            
            if (string.IsNullOrEmpty(x.SeatNo))
                strX = x.ClassName + "999";
            else
                strX = x.ClassName + string.Format("{0:000}",x.SeatNo);

            if (string.IsNullOrEmpty(y.SeatNo))
                strY = y.ClassName + "999";
            else
                strY = y.ClassName + string.Format("{0:000}", y.SeatNo);
            return strX.CompareTo(strY);
        }

    }
}
