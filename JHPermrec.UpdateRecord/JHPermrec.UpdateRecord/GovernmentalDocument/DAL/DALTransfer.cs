using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace JHPermrec.UpdateRecord.GovernmentalDocument.DAL
{
    class DALTransfer
    {
        
        
        /// <summary>
        /// 取得異動內學生編號
        /// </summary>
        /// <param name="UpdateRecordList"></param>
        /// <returns></returns>
        public static List<string> GetUpdatRecordStudentIDList(List<XmlElement> UpdateRecordList)
        {
            List<string> StudentIDList = new List<string>();
            foreach (XmlElement xm in UpdateRecordList)
                if(!StudentIDList.Contains(xm.GetAttribute("RefStudentID")))
                    StudentIDList.Add(xm.GetAttribute("RefStudentID"));
            return StudentIDList ;        
        }

        
        /// <summary>
        /// 取得異動紀錄上班年級
        /// </summary>
        /// <param name="UpdateRecordList"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetUpateRecordStudentGradeYearList(List<XmlElement> UpdateRecordList)
        {
            Dictionary<string, string> Dic = new Dictionary<string, string>();

            Dictionary<string, string> ClassDic = new Dictionary<string, string>();

            foreach (JHSchool.Data.JHClassRecord cr in JHSchool.Data.JHClass.SelectAll())
                if (!ClassDic.ContainsKey(cr.Name))
                    if(cr.GradeYear.HasValue )
                        ClassDic.Add(cr.Name, cr.GradeYear.Value.ToString());

            foreach (XmlElement xm in UpdateRecordList)
            {
                string xmlClassName = "";
                if(xm.SelectSingleNode("ContextInfo/ContextInfo/OriginClassName")!=null)
                    xmlClassName=xm.SelectSingleNode("ContextInfo/ContextInfo/OriginClassName").InnerText.Trim();

                string studID = xm.GetAttribute("RefStudentID");
                if(ClassDic.ContainsKey(xmlClassName ))
                    if(!Dic.ContainsKey(studID ))
                        Dic.Add(studID,ClassDic[xmlClassName]);
            }

            return Dic;
        }



        /// <summary>
        /// 取得學生地址 Record
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, JHSchool.Data.JHAddressRecord> GetStudentAddressRecordDic(List<string> StudentIDList)
        {
            Dictionary<string, JHSchool.Data.JHAddressRecord> AddressRecDic = new Dictionary<string, JHSchool.Data.JHAddressRecord>();
            List<JHSchool.Data.JHAddressRecord> AddressRecList = JHSchool.Data.JHAddress.SelectByStudentIDs(StudentIDList);
            foreach (JHSchool.Data.JHAddressRecord addRec in AddressRecList)
                AddressRecDic.Add(addRec.RefStudentID, addRec);
            return AddressRecDic;
        }

        /// <summary>
        /// 取得父母親資料
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, JHSchool.Data.JHParentRecord> GetParentRecordDic(List<string> StudentIDList)
        {
            Dictionary<string, JHSchool.Data.JHParentRecord> ParentRecDic = new Dictionary<string, JHSchool.Data.JHParentRecord>();
            List<JHSchool.Data.JHParentRecord> ParentRecList = JHSchool.Data.JHParent.SelectByStudentIDs(StudentIDList);
            foreach (JHSchool.Data.JHParentRecord ParentRec in ParentRecList)
                ParentRecDic.Add(ParentRec.RefStudentID, ParentRec);
            return ParentRecDic;        
        }

        /// <summary>
        /// 取得學生現在座號
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetStudentSeatNo(List<string> StudentIDList)
        {
            Dictionary<string, string> retValue = new Dictionary<string, string>();
            foreach (JHSchool.Data.JHStudentRecord rec in JHSchool.Data.JHStudent.SelectByIDs(StudentIDList))
            {
                if (rec.SeatNo.HasValue)
                    retValue.Add(rec.ID, rec.SeatNo.Value.ToString());
                else
                    retValue.Add(rec.ID, "");
            }
            return retValue;        
        }

        /// <summary>
        /// 取得民國日期例如:民國98年1月1日
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetChiBirthday1(List<string> StudentIDList)
        {
            Dictionary<string, string> BirthDic = new Dictionary<string, string>();
            foreach (JHSchool.Data.JHStudentRecord studRec in JHSchool.Data.JHStudent.SelectByIDs(StudentIDList))
            {
                if (studRec.Birthday.HasValue)
                {
                    string birth = "民國"+(studRec.Birthday.Value.Year - 1911)+"年"+studRec.Birthday.Value.Month +"月"+studRec.Birthday.Value.Day +"日";
                    BirthDic.Add(studRec.ID, birth);                
                }            
            }                

            return BirthDic;
        }

        /// <summary>
        /// 轉換西元格式成 98.01.01
        /// </summary>
        /// <param name="dtDate"></param>
        /// <returns></returns>
        public static string ConvertDateStr1(string dtDate)
        {
            string returnStr = "";
            DateTime dt;
            if (DateTime.TryParse(dtDate.Trim(), out dt))
            {
                string y, m, d;
                y = "" + (dt.Year - 1911);
                if (dt.Month < 10)
                    m = "0" + dt.Month;
                else
                    m = "" + dt.Month;

                if (dt.Day < 10)
                    d = "0" + dt.Day;
                else
                    d = "" + dt.Day;

                returnStr=y + "." + m + "." + d;
            }
            return returnStr;        
        }

        /// <summary>
        /// 取得轉入異動年月
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetUpdateRecCode03SchoolYear(List<string> StudentIDList)
        {
            Dictionary<string, string> retVal= new Dictionary<string,string> ();
            Dictionary<string, List<string>> UrDict = new Dictionary<string, List<string>>();

            foreach (JHSchool.Data.JHUpdateRecordRecord rec in JHSchool.Data.JHUpdateRecord.SelectByStudentIDs(StudentIDList))
            {
                
                if (rec.UpdateCode == "3")
                { 
                    DateTime dt;
                    if(DateTime.TryParse(rec.UpdateDate ,out dt))
                    {
                        string str = String.Format("{0:0000}", dt.Year) + String.Format("{0:00}", dt.Month);
                        if (UrDict.ContainsKey(rec.StudentID))
                        {
                            UrDict[rec.StudentID].Add(str);
                        }
                        else
                        {
                            List<string> strL = new List<string>();
                            strL.Add(str);
                            UrDict.Add(rec.StudentID, strL);
                        }
                    }
                }
            
            }


            foreach (KeyValuePair<string, List<string>> data in UrDict)
            {
                if (data.Value.Count  > 1)
                    data.Value.Sort();

                retVal.Add(data.Key, data.Value[data.Value.Count - 1]);           
            }


            return retVal;
        }

    }
}
