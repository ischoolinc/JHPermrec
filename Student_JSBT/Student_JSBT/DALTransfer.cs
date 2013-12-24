using System;
using System.Collections.Generic;
using System.Text;
using JHSchool.Data;
using System.Xml;

namespace Student_JSBT
{
    class DALTransfer
    {
        /// <summary>
        /// 是否使用聯絡地址
        /// </summary>
        public static bool _UseMailAddress = false; 
        public static List<StudInfoEntity> GetStudentEntityList(List<string> StudentIDList)
        {
            Dictionary<string, string> StudTagDic = new Dictionary<string, string>();
            Dictionary<string, string> StudItemDic = new Dictionary<string, string>();
            
            Global._tempPhomeDict .Clear();

            // 取得對照表
            XmlDocument  doc = StudSBTManager.GetDataFormSystem();
            if (doc != null)
            {
                if (doc.SelectSingleNode("Data") != null)
                    foreach (XmlElement xe in doc.SelectSingleNode("Data"))
                    {
                        string StudTags = xe.GetAttribute("StudTag").Trim();
                        StudTags=StudTags.Replace('，', ',');
                        StudTags = StudTags.Replace('：', ':');

                        string[] StudTagsArr = StudTags.Split(',');
                        

                        if(!string.IsNullOrEmpty (StudTags))
                        {
                            foreach (string str in StudTagsArr)
                            {
                                if (!StudTagDic.ContainsKey(str))
                                    StudTagDic.Add(str, xe.GetAttribute("FieldName") + xe.GetAttribute("ItemName"));
                            }
                        }
                        
                        if(!string.IsNullOrEmpty (xe.GetAttribute("FieldName")) && !string.IsNullOrEmpty(xe.GetAttribute("ItemName")))
                        {
                            string Item=xe.GetAttribute("FieldName")+xe.GetAttribute("ItemName");
                            if(!StudItemDic.ContainsKey(Item))
                                StudItemDic.Add(Item,xe.GetAttribute("ItemValue"));                        
                        }
                    }
            }

            List<StudInfoEntity> StudInfoEntityList = new List<StudInfoEntity>();

            // 建立相關讀取用到資訊

            Dictionary<string, JHLeaveInfoRecord> LeaveInfoRecordDic = new Dictionary<string, JHLeaveInfoRecord>();
            Dictionary<string, List<string>> StudentTagRecordDic = new Dictionary<string, List<string>>();
            Dictionary<string, JHPhoneRecord> PhoneRecordDic = new Dictionary<string, JHPhoneRecord>();
            Dictionary<string, JHParentRecord> ParentRecordDic = new Dictionary<string, JHParentRecord>();
            Dictionary<string, JHAddressRecord> AddressRecordDic = new Dictionary<string, JHAddressRecord>();

            // 畢業資訊
            foreach (JHLeaveInfoRecord lir in JHLeaveIfno.SelectByStudentIDs(StudentIDList))
                if (!LeaveInfoRecordDic.ContainsKey(lir.RefStudentID))
                    LeaveInfoRecordDic.Add(lir.RefStudentID, lir);

            // 學生 Tag
            foreach (JHStudentTagRecord str in JHStudentTag.SelectByStudentIDs(StudentIDList))
            {
                string strS = str.FullName;
                if (StudentTagRecordDic.ContainsKey(str.RefStudentID))
                {
                    StudentTagRecordDic[str.RefStudentID].Add(strS);
                }
                else
                {
                    List<string> strList = new List<string>();
                    strList.Add(strS);
                    StudentTagRecordDic.Add(str.RefStudentID, strList);
                }
            }
            // 電話資訊
            foreach (JHPhoneRecord pr in JHPhone.SelectByStudentIDs(StudentIDList))
            {
                if (!PhoneRecordDic.ContainsKey(pr.RefStudentID))
                    PhoneRecordDic.Add(pr.RefStudentID, pr);
                if(!Global._tempPhomeDict.ContainsKey(pr.RefStudentID))
                    Global._tempPhomeDict.Add(pr.RefStudentID, new Dictionary<string, string>());

                    Global._tempPhomeDict[pr.RefStudentID].Add("戶籍電話",pr.Permanent);
                    Global._tempPhomeDict[pr.RefStudentID].Add("聯絡電話",pr.Contact);                    
                
                
            }
            // 父母及監護人資訊
            foreach (JHParentRecord pr in JHParent.SelectByStudentIDs(StudentIDList))
            {
                if (!ParentRecordDic.ContainsKey(pr.RefStudentID))
                    ParentRecordDic.Add(pr.RefStudentID, pr);

                if(!Global._tempPhomeDict.ContainsKey(pr.RefStudentID))
                    Global._tempPhomeDict.Add(pr.RefStudentID, new Dictionary<string, string>());

                    Global._tempPhomeDict[pr.RefStudentID].Add("父親電話",pr.FatherPhone);
                    Global._tempPhomeDict[pr.RefStudentID].Add("母親電話",pr.MotherPhone);   
                    Global._tempPhomeDict[pr.RefStudentID].Add("監護人電話",pr.CustodianPhone);   
                
            }
            
            // 地址
            foreach (JHAddressRecord ar in JHAddress.SelectByStudentIDs(StudentIDList))
                if (!AddressRecordDic.ContainsKey(ar.RefStudentID))
                    AddressRecordDic.Add(ar.RefStudentID,ar);

            foreach (JHStudentRecord studRec in JHStudent.SelectByIDs(StudentIDList))
            {
                if (studRec.Status == K12.Data.StudentRecord.StudentStatus.一般 || studRec.Status == K12.Data.StudentRecord.StudentStatus.輟學)
                {
                    StudInfoEntity sie = new StudInfoEntity();
                    // 初始
                    sie.ClassName = sie.SeatNo = string.Empty;

                    if (studRec.Class != null)
                        sie.ClassName = string.Format ("{0:00}",studRec.Class.Name);
                    
                    sie.Gender = studRec.Gender;
                    sie.IDNumber = studRec.IDNumber;
                    sie.Name = studRec.Name;
                    if (studRec.SeatNo.HasValue)
                        sie.SeatNo = string.Format("{0:00}",studRec.SeatNo.Value);
                    sie.StudentID = studRec.ID;
                    sie.StudentNumber = string.Format("{0:00000000}", studRec.StudentNumber);

                    // 填入資料
                    if (!string.IsNullOrEmpty(JHSchool.Data.JHSchoolInfo.Code))
                    {
                        string strSchoolCode = JHSchoolInfo.Code;
                       
                        if (strSchoolCode.Length >= 6)
                            strSchoolCode = strSchoolCode.Substring(0, 6);

                        sie.SetDataCellEntity("學校代碼", 6, strSchoolCode);
                        sie.SetDataCellEntity("畢業學校代碼", 6, strSchoolCode);
                    }

                    string strNum;

                    if (sie.StudentNumber.Length >= 8)
                        strNum = sie.StudentNumber.Substring(0, 8);
                    else
                    {
                        strNum = sie.StudentNumber;
                        for (int i = 0; i < (8 - sie.StudentNumber.Length); i++)
                            strNum = "0" + strNum;
                    
                    }

                    sie.SetDataCellEntity("學號", 8, strNum);
                    if (!string.IsNullOrEmpty(sie.ClassName))
                    {
                        if (sie.ClassName.Length >= 3)
                            sie.ClassName = sie.ClassName.Substring(1, 2);

                        if (sie.ClassName.Length >= 2)
                            sie.SetDataCellEntity("班級", 2, sie.ClassName.Substring(0, 2));
                        else
                            sie.SetDataCellEntity("班級", 2, "0" + sie.ClassName);
                    }

                    if (!string.IsNullOrEmpty(sie.SeatNo))
                    {
                        if(sie.SeatNo.Length>0)
                            sie.SetDataCellEntity("座號", 2, sie.SeatNo.Substring(0,2));
                        else
                            sie.SetDataCellEntity("座號", 2, "0"+sie.SeatNo);
                    }

                    string StudName = sie.Name.Trim();
                    if (StudName.Length == 2)
                        StudName = StudName.Substring(0, 1) + "  " + StudName.Substring(1, 1);

                    sie.SetDataCellEntity("學生姓名", 20, StudName);
                    sie.SetDataCellEntity("身分證號", 10, sie.IDNumber);

                    if (studRec.Gender == "男")
                        sie.SetDataCellEntity("性別", 1, "1");
                    if (studRec.Gender == "女")
                        sie.SetDataCellEntity("性別", 1, "2");

                    if (studRec.Birthday.HasValue)
                    {
                        sie.SetDataCellEntity("出生年", 2, string.Format ("{0:00}",studRec.Birthday.Value.Year-1911));
                        sie.SetDataCellEntity("出生月", 2, string.Format("{0:00}",studRec.Birthday.Value.Month));
                        sie.SetDataCellEntity("出生日", 2, string.Format("{0:00}",studRec.Birthday.Value.Day));
                    }

                    if (LeaveInfoRecordDic.ContainsKey(studRec.ID))
                    {
                        if(LeaveInfoRecordDic[studRec.ID].SchoolYear.HasValue )
                            sie.SetDataCellEntity("畢業年度", 2, LeaveInfoRecordDic[studRec.ID].SchoolYear.Value+"");
                        if(LeaveInfoRecordDic[studRec.ID].Reason =="畢業")
                            sie.SetDataCellEntity("畢肄業", 1,"1");
                        if (LeaveInfoRecordDic[studRec.ID].Reason == "修業")
                            sie.SetDataCellEntity("畢肄業", 1, "0");
                    }



                    if (ParentRecordDic.ContainsKey(studRec.ID))
                    {
                        string ParentName = ParentRecordDic[studRec.ID].CustodianName.Trim();

                        if (ParentName.Length == 2)
                            ParentName = ParentName.Substring(0, 1) + "  " + ParentName.Substring(1, 1);
                        sie.SetDataCellEntity("家長姓名", 20,ParentName);


                        //// 戶籍
                        //string strPhone=ParentRecordDic[studRec.ID].CustodianPhone.Replace ("(","");
                        //strPhone = strPhone.Replace(")", "");
                        //strPhone = strPhone.Replace("-", "");
                        string strPhone = "";
                        if (Global._tempPhomeDict.ContainsKey(sie.StudentID))
                            if (Global._tempPhomeDict[sie.StudentID].ContainsKey(Global.SelectPhoneType))
                            {
                                strPhone = Global._tempPhomeDict[sie.StudentID][Global.SelectPhoneType].Replace("(", ""); ;
                                strPhone = strPhone.Replace(")", "");
                                strPhone = strPhone.Replace("-", "");
                            }
                        sie.SetDataCellEntity("緊急連絡電話", 20,strPhone);
                    }

                    if (AddressRecordDic.ContainsKey(studRec.ID))
                    {
                        
                        string strAddress="";
                        if (_UseMailAddress)
                        {
                            sie.SetDataCellEntity("郵遞區號", 3, AddressRecordDic[studRec.ID].MailingZipCode);
                            strAddress = AddressRecordDic[studRec.ID].MailingCounty + AddressRecordDic[studRec.ID].MailingTown + AddressRecordDic[studRec.ID].MailingDistrict + AddressRecordDic[studRec.ID].MailingArea + AddressRecordDic[studRec.ID].MailingDetail;
                        }
                        else
                        {
                            sie.SetDataCellEntity("郵遞區號", 3, AddressRecordDic[studRec.ID].PermanentZipCode);
                            strAddress = AddressRecordDic[studRec.ID].PermanentCounty + AddressRecordDic[studRec.ID].PermanentTown + AddressRecordDic[studRec.ID].PermanentDistrict + AddressRecordDic[studRec.ID].PermanentArea + AddressRecordDic[studRec.ID].PermanentDetail;
                        }

                        sie.SetDataCellEntity("地址", 80, strAddress);
                    }
                    

                    if (PhoneRecordDic.ContainsKey(studRec.ID))
                    {
                        sie.SetDataCellEntity("手機", 10, PhoneRecordDic[studRec.ID].Cell);
                    }

                    // default value
                    sie.SetDataCellEntity("學生身分", 1, "0");
                    sie.SetDataCellEntity("身心障礙", 1, "0");
                    sie.SetDataCellEntity("中低收入戶", 1, "0");
                    sie.SetDataCellEntity("低收入戶", 1, "0");
                    sie.SetDataCellEntity("失業勞工子女", 1, "0");

                    if(StudentTagRecordDic.ContainsKey(studRec.ID ))
                    {
                        foreach (string str in StudentTagRecordDic[studRec.ID])
                        {
                            if (StudTagDic.ContainsKey(str))
                            {
                                if(StudItemDic.ContainsKey(StudTagDic[str]))
                                {

                                    if (StudTagDic[str].IndexOf("學生身分") > -1)
                                        sie.SetDataCellEntity("學生身分", 1, StudItemDic[StudTagDic[str]]);

                                    if (StudTagDic[str].IndexOf("身心障礙") > -1)
                                        sie.SetDataCellEntity("身心障礙", 1, StudItemDic[StudTagDic[str]]);                                        

                                    if (StudTagDic[str].IndexOf("低收入戶") ==0)
                                        sie.SetDataCellEntity("低收入戶", 1, StudItemDic[StudTagDic[str]]);

                                    //if (StudTagDic[str].IndexOf("中低收入戶") > -1)
                                    //    sie.SetDataCellEntity("中低收入戶", 1, StudItemDic[StudTagDic[str]]);                                        

                                    if (StudTagDic[str].IndexOf("失業勞工子女") > -1)
                                        sie.SetDataCellEntity("失業勞工子女", 1, StudItemDic[StudTagDic[str]]);                                        
                                }                           
                            }                        
                        }                    
                    }
                    StudInfoEntityList.Add(sie);
                }            
            }

            return StudInfoEntityList;
        }
    }
}
