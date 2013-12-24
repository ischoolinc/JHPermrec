using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataRationality;
using FISCA.Presentation.Controls;
using K12.Data;

namespace UserDefineData
{
    public class DoubleUserDefDataRATRec
    {
        public string UID { get; set; }

        public string 學生系統編號 { get; set; }

        public string 學號 { get; set; }

        public string 身分證號 { get; set; }

        public string 班級 { get; set; }

        public string 座號 { get; set; }

        public string 姓名 { get; set; }

        public string 狀態 { get; set; }

        public string 欄位名稱 { get; set; }

        public string 值   { get; set; }
    }

    public class DoubleUserDefDataRAT : ICorrectableDataRationality
    {
        List<UserDefineData.DAL.UserDefData> CorrectableRecs = new List<UserDefineData.DAL.UserDefData>();
        List<DoubleUserDefDataRATRec> RATRecs = new List<DoubleUserDefDataRATRec>();


        #region ICorrectableDataRationality 成員

        public void ExecuteAutoCorrect(IEnumerable<string> EntityIDs)
        {
            
        }

        public void ExecuteAutoCorrect()
        {
            
        }

        #endregion

        #region IDataRationality 成員

        public void AddToTemp()
        {
            AddToTemp(null);
        }

        public void AddToTemp(IEnumerable<string> EntityIDs)
        {
            List<string> PrimaryKeys = new List<string>();

            if (K12.Data.Utility.Utility.IsNullOrEmpty(EntityIDs))
                PrimaryKeys = CorrectableRecs.Select(x => x.RefID).Distinct().ToList();
            else
                PrimaryKeys = CorrectableRecs.Where(x => EntityIDs.Contains(x.UID)).Select(x => x.RefID).Distinct().ToList();

            PrimaryKeys.AddRange(K12.Presentation.NLDPanels.Student.TempSource);

            K12.Presentation.NLDPanels.Student.AddToTemp(PrimaryKeys.Distinct().ToList());

        }

        public string Category
        {
            get { return "學籍"; }
        }

        public string Description
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();

                strBuilder.AppendLine("檢查範圍：所有學生的自訂資料欄位名稱。");
               

                return strBuilder.ToString();
            }
        }

        public DataRationalityMessage Execute()
        {
            DataRationalityMessage retMsg = new DataRationalityMessage();
            
            // 取得所有學生資料
            Dictionary<string,StudentRecord> studIDDict = new Dictionary<string,StudentRecord> ();
            List<StudentRecord> studRecList = Student.SelectAll();
            foreach (StudentRecord stud in studRecList)
                studIDDict.Add(stud.ID, stud);

            // 取得學生自訂欄位資料
            List<UserDefineData.DAL.UserDefData> UserDefDataList = UDTTransfer.GetDataFromUDT(studIDDict.Keys.ToList());

            // 檢查資料並組合
            Dictionary<string, Dictionary<string, List<UserDefineData.DAL.UserDefData>>> dataDict = new Dictionary<string, Dictionary<string, List<DAL.UserDefData>>>();

            foreach (UserDefineData.DAL.UserDefData data in UserDefDataList)
            {
                if (!dataDict.ContainsKey(data.RefID))
                    dataDict.Add(data.RefID, new Dictionary<string,List<DAL.UserDefData>>());

                if (!dataDict[data.RefID].ContainsKey(data.FieldName))
                    dataDict[data.RefID].Add(data.FieldName, new List<DAL.UserDefData>());
                
                dataDict[data.RefID][data.FieldName].Add(data);            
            }
                        
            CorrectableRecs.Clear();
            RATRecs.Clear();            
            try
            {
                foreach (string sid in dataDict.Keys)
                {
                    Dictionary<string, List<UserDefineData.DAL.UserDefData>> dataA = dataDict[sid];
                    foreach (List<UserDefineData.DAL.UserDefData> data in dataA.Values)
                    {                        
                        // 有重複
                        if (data.Count > 1)
                        {
                            foreach (UserDefineData.DAL.UserDefData data1 in data)
                            {
                                DoubleUserDefDataRATRec rec = new DoubleUserDefDataRATRec();
                                rec.UID = data1.UID;
                                rec.學生系統編號 = data1.RefID;
                                rec.欄位名稱 = data1.FieldName;
                                rec.值 = data1.Value;
                                if (studIDDict.ContainsKey(data1.RefID))
                                {
                                    rec.身分證號 = studIDDict[data1.RefID].IDNumber;
                                    rec.姓名 = studIDDict[data1.RefID].Name;
                                    rec.狀態 = studIDDict[data1.RefID].Status.ToString();
                                    rec.學號 = studIDDict[data1.RefID].StudentNumber;
                                    if (studIDDict[data1.RefID].SeatNo.HasValue)
                                        rec.座號 = studIDDict[data1.RefID].SeatNo.Value.ToString();
                                    else
                                        rec.座號 = "";

                                    if (studIDDict[data1.RefID].Class != null)
                                        rec.班級 = studIDDict[data1.RefID].Class.Name;
                                    else
                                        rec.班級 = "";
                                }

                                RATRecs.Add(rec);
                                CorrectableRecs.Add(data1);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                retMsg.Message = ex.Message;

                return retMsg;
            }

            StringBuilder strBuilder = new StringBuilder();

            strBuilder.AppendLine("檢查學生自訂欄位資料筆數：" + UserDefDataList.Count);
            strBuilder.AppendLine("自訂欄位資料有重複筆數：" + RATRecs.Count);

            var SortedRATRecords = from RATRecord in RATRecs orderby RATRecord.狀態, RATRecord.班級, K12.Data.Int.ParseAllowNull(RATRecord.座號), RATRecord.欄位名稱, RATRecord.值 select RATRecord;

            retMsg.Message = strBuilder.ToString();
            retMsg.Data = SortedRATRecords.ToList();

            return retMsg;
        }

        public string Name
        {
            get { return "學生自訂資料欄位名稱重複檢查"; }
        }

        #endregion
    }
}
