using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FISCA.DSAUtil;
//using SmartSchool.Common;
using Framework;
using JHSchool.Permrec.Feature.Legacy;
using Framework.Legacy;

namespace JHPermrec.UpdateRecord.GovernmentalDocument.NameList
{
    class GraduatingStudentListProvider : INameListProvider
    {
        #region INameListProvider 成員

        //比對大小
        private static int CompareUpdateRecord(XmlElement x, XmlElement y)
        {
            string gradeyear = x.SelectSingleNode("GradeYear").InnerText;
            string dept = x.SelectSingleNode("Department").InnerText;
            int c = gradeyear.CompareTo(y.SelectSingleNode("GradeYear").InnerText);
            if (c == 0)
            {
                return dept.CompareTo(y.SelectSingleNode("Department").InnerText);
            }
            else
            {
                return c;
            }
        }

        //排序畢業證書字號
        private static int GraduateCertificateNumberComparison(XmlElement a, XmlElement b)
        {
            string a1 = new DSXmlHelper(a).GetText("ContextInfo/ContextInfo/GraduateCertificateNumber");
            string b1= new DSXmlHelper(b).GetText("ContextInfo/ContextInfo/GraduateCertificateNumber");
            return a1.CompareTo(b1);
        }

        //排序學號
        private static int StudentNumberComparison(XmlElement a, XmlElement b)
        {
            string sa = new DSXmlHelper(a).GetText("StudentNumber");
            string sb = new DSXmlHelper(b).GetText("StudentNumber");
            int ia, ib;
            if (int.TryParse(sa, out ia) && int.TryParse(sb, out ib))
                return ia.CompareTo(ib);
            else
                return sa.CompareTo(sb);
        }

        //排序科別代碼
        private static int DepartmentCodeComparison(XmlElement a, XmlElement b)
        {
            string sa = new DSXmlHelper(a).GetText("@科別代號");
            string sb = new DSXmlHelper(b).GetText("@科別代號");
            int ia, ib;
            if (int.TryParse(sa, out ia) && int.TryParse(sb, out ib))
                return ia.CompareTo(ib);
            else
                return sa.CompareTo(sb);
        }

        //明國年轉換
        private string CDATE(string p)
        {
            DateTime d = DateTime.Now;
            if (p != "" && DateTime.TryParse(p, out d))
            {
                return "" + (d.Year - 1911) + "/" + d.Month + "/" + d.Day;
            }
            else
                return "";
        }

        // 定義標題
        public string Title
        {
            get { return "畢業名冊"; }
        }

        // 定義異動代號
        //private string[] strCodeList = new string[] { "501" };
        private string[] strCodeList = new string[] { "2" };

        public List<System.Xml.XmlElement> GetExpectantList()
        {
            List<XmlElement> list = new List<XmlElement>();
            foreach (XmlElement var in QueryStudent.GetUpdateRecordByCode(strCodeList).GetContent().GetElements("UpdateRecord"))
            {
                if (var.SelectSingleNode("GradeYear").InnerText != "延修生")
                {
                    list.Add(var);
                }
            }
            return (list);           
            
        }

        public System.Xml.XmlElement CreateNameList(string schoolYear, string semester, List<System.Xml.XmlElement> list)
        {
            XmlDocument xmlDoc = new XmlDocument();
            Dictionary<string, string> deptCode = new Dictionary<string, string>();
            // 建立科別代碼
            /* 這段沒使用到先註
            foreach (XmlElement x in SmartSchool.Feature.Basic.Config.GetDepartment().GetContent().GetElements("Department"))
            {
                deptCode.Add(x.SelectSingleNode("Name").InnerText, x.SelectSingleNode("Code").InnerText);
            }
            */
            // 依年級科別將資料排序
            //list.Sort(CompareUpdateRecord);

            // 取得 StudentID
            List<string> StudentIDList = DAL.DALTransfer.GetUpdatRecordStudentIDList(list);

            //// 取得學生地址
            Dictionary<string, JHSchool.Data.JHAddressRecord> AddressRecDic = DAL.DALTransfer.GetStudentAddressRecordDic(StudentIDList);

            // 取得學生座號
            Dictionary<string, string> StudSeatNoDict = DAL.DALTransfer.GetStudentSeatNo(StudentIDList);

            // 取的學生轉入異動年月
            Dictionary<string, string> GetUrCode03Dict = DAL.DALTransfer.GetUpdateRecCode03SchoolYear(StudentIDList);


            // 取得學生家長
            Dictionary<string, JHSchool.Data.JHParentRecord> ParentRecDic = DAL.DALTransfer.GetParentRecordDic(StudentIDList);

            ////依畢業證書字號排序
            //list.Sort(GraduateCertificateNumberComparison);

            // 依學號排序
            list.Sort(StudentNumberComparison);
            // 產生 XML 資料
            Dictionary <string ,Dictionary <string ,XmlElement >> gradeYear_deptMap = new Dictionary<string,Dictionary<string,XmlElement>>() ;

            xmlDoc.LoadXml("<異動名冊 類別=\"畢業名冊\" 學年度=\"" + schoolYear + "\" 學期=\"" + semester + "\" 學校代號=\"" + GlobalOld.SchoolInformation.Code + "\" 學校名稱=\"" + GlobalOld.SchoolInformation.ChineseName + "\"/>");

            foreach (XmlElement x in list)
            {
                DSXmlHelper helper = new DSXmlHelper(x);
                string gradeYear = helper.GetText("GradeYear");
                string dept = helper.GetText("Department");

                XmlElement deptGradeNode;
                if (!(gradeYear_deptMap.ContainsKey(gradeYear)))
                {
                    gradeYear_deptMap.Add (gradeYear ,new Dictionary <string ,XmlElement >() );
                }

                if (!(gradeYear_deptMap[gradeYear].ContainsKey(dept )))
                {
                    deptGradeNode=xmlDoc.CreateElement ("清單");
                    deptGradeNode.SetAttribute ("科別",dept);
                    deptGradeNode.SetAttribute ("年級",gradeYear );
                    //deptGradeNode.SetAttribute ("科別代號",deptCode.ContainsKey(dept) ? deptCode[dept]:"" );
                    gradeYear_deptMap[gradeYear].Add(dept, deptGradeNode);
                    xmlDoc.DocumentElement.AppendChild (deptGradeNode);

                }
                else
                {
                    deptGradeNode = gradeYear_deptMap[gradeYear][dept];
                }

                // 產生異動細項資料
                XmlElement dataElement = xmlDoc.CreateElement("異動紀錄");

                dataElement.SetAttribute("編號",helper.GetText ("@ID"));
                dataElement.SetAttribute("異動代號", helper.GetText("UpdateCode"));
                dataElement.SetAttribute("異動日期", CDATE (helper.GetText("UpdateDate")));

                // 畢業證書字號
                dataElement.SetAttribute("畢業證書字號", helper.GetText("ContextInfo/ContextInfo/GraduateCertificateNumber"));

                // 最後異動代號
                dataElement.SetAttribute("最後異動代號", helper.GetText("ContextInfo/ContextInfo/LastUpdateCode"));
                dataElement.SetAttribute("出生地", helper.GetText("ContextInfo/ContextInfo/BirthPlace"));
                dataElement.SetAttribute("入學年月", helper.GetText("ContextInfo/ContextInfo/EnrollmentSchoolYear"));
                dataElement.SetAttribute("畢業年月", helper.GetText("ContextInfo/ContextInfo/GraduateSchoolYear"));

                dataElement.SetAttribute("備註", helper.GetText("Comment"));
                dataElement.SetAttribute("學籍核准文號", helper.GetText("LastADNumber"));
                dataElement.SetAttribute("學籍核准日期", CDATE(helper.GetText("LastADDate")));
                dataElement.SetAttribute("出生年月日", CDATE(helper.GetText("Birthdate")));
                dataElement.SetAttribute("生日", helper.GetText("Birthdate"));
                dataElement.SetAttribute("身分證號", helper.GetText("IDNumber"));
                dataElement.SetAttribute("性別", helper.GetText("Gender"));                
                dataElement.SetAttribute("異動日期1", helper.GetText("UpdateDate"));
                dataElement.SetAttribute("學籍核准日期1", helper.GetText("LastADDate"));
                dataElement.SetAttribute("異動年級", helper.GetText("GradeYear"));
                string StudentID = helper.GetAttribute("@RefStudentID");


                if (AddressRecDic.ContainsKey(StudentID))
                {
                    //dataElement.SetAttribute("戶籍縣市", AddressRecDic[StudentID].Permanent.County);
                    //dataElement.SetAttribute("戶籍里", AddressRecDic[StudentID].Permanent.District);
                    //dataElement.SetAttribute("戶籍鄰", AddressRecDic[StudentID].Permanent.Area);
                    //dataElement.SetAttribute("戶籍其他", AddressRecDic[StudentID].Permanent.Detail);
                    dataElement.SetAttribute("戶籍地址", AddressRecDic[StudentID].PermanentAddress);
                }
                 
                dataElement.SetAttribute("學號", helper.GetText("StudentNumber"));
                dataElement.SetAttribute("姓名", helper.GetText("Name"));
                dataElement.SetAttribute("班級", helper.GetText("ContextInfo/ContextInfo/OriginClassName"));
                dataElement.SetAttribute("畢修業別", helper.GetText("ContextInfo/ContextInfo/Graduate"));
                dataElement.SetAttribute("畢業證書字號", helper.GetText("ContextInfo/ContextInfo/GraduateCertificateNumber"));
                if (ParentRecDic.ContainsKey(StudentID))
                    dataElement.SetAttribute("監護人", ParentRecDic[StudentID].Custodian.Name);
                if(StudSeatNoDict.ContainsKey(StudentID ))
                    dataElement.SetAttribute("座號", StudSeatNoDict[StudentID]);

                // 當有轉入異動用轉入異動年月
                if(GetUrCode03Dict.ContainsKey(StudentID ))
                    dataElement.SetAttribute("入學年月", GetUrCode03Dict[StudentID]);

                deptGradeNode.AppendChild(dataElement);
            
            }
            /*
            #region 排序科別代碼

            List<XmlElement> deptList = new List<XmlElement>();
            foreach (XmlElement var in xmlDoc.DocumentElement.SelectNodes("清單"))
                deptList.Add(var);
            deptList.Sort(DepartmentCodeComparison);

            DSXmlHelper docHelper = new DSXmlHelper(xmlDoc.DocumentElement);
            while (docHelper.PathExist("清單")) docHelper.RemoveElement("清單");

            foreach (XmlElement var in deptList)
                docHelper.AddElement(".", var);

            #endregion
            */
            return xmlDoc.DocumentElement;
        }

        #endregion
    }
}
