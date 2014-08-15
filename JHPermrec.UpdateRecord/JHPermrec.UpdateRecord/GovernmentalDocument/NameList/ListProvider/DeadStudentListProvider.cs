using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FISCA.DSAUtil;
using JHSchool.Permrec.Feature.Legacy;
using Framework;
using Framework.Legacy;

namespace JHPermrec.UpdateRecord.GovernmentalDocument.NameList
{
    class DeadStudentListProvider : INameListProvider
    {
        //比對大小
        private static int CompareUpdateRecord(XmlElement x, XmlElement y)
        {
            string gradeyear = x.SelectSingleNode("GradeYear").InnerText;
            string dept = x.SelectSingleNode("Department").InnerText;
            int c = gradeyear.CompareTo(y.SelectSingleNode("GradeYear").InnerText);
            if (c == 0)
            {
                int d = dept.CompareTo(y.SelectSingleNode("Department").InnerText);
                return d == 0 ? 1 : d;
            }
            else
            {
                return c;
            }
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

        //允許代號列表
        // 死亡異動代號:11
        private string[] _CodeList = new string[] { "11" };

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
        #region INameListProvider 成員

        public string Title
        {
            get { return "死亡學生名冊"; }
        }

        public List<XmlElement> GetExpectantList()
        {
            List<XmlElement> list = new List<XmlElement>();
            list.AddRange(QueryStudent.GetUpdateRecordByCode(_CodeList).GetContent().GetElements("UpdateRecord"));
            return (list);
        }

        public System.Xml.XmlElement CreateNameList(string schoolYear, string semester, List<XmlElement> list)
        {
            XmlDocument doc = new XmlDocument();
            Dictionary<string, string> deptCode = new Dictionary<string, string>();

            //排序學號
            list.Sort(StudentNumberComparison);

            #region 產生Xml
            Dictionary<string, Dictionary<string, XmlElement>> gradeyear_dept_map = new Dictionary<string, Dictionary<string, XmlElement>>();
            doc.LoadXml("<異動名冊 類別=\"死亡學生名冊\" 學年度=\"" + schoolYear + "\" 學期=\"" + semester + "\" 學校代號=\"" + Framework.Legacy.GlobalOld.SchoolInformation.Code + "\" 學校名稱=\"" + GlobalOld.SchoolInformation.ChineseName + "\"/>");
            foreach (XmlElement var in list)
            {
                DSXmlHelper helper = new DSXmlHelper(var);
                string gradeyear = helper.GetText("GradeYear");
                string dept = helper.GetText("Department");
                XmlElement deptgradeNode;


                #region 清單
                if (!gradeyear_dept_map.ContainsKey(gradeyear))
                {
                    gradeyear_dept_map.Add(gradeyear, new Dictionary<string, XmlElement>());
                }
                if (!(gradeyear_dept_map[gradeyear].ContainsKey(dept)))
                {
                    deptgradeNode = doc.CreateElement("清單");
                    deptgradeNode.SetAttribute("科別", dept);
                    deptgradeNode.SetAttribute("年級", gradeyear);
                    gradeyear_dept_map[gradeyear].Add(dept, deptgradeNode);
                    doc.DocumentElement.AppendChild(deptgradeNode);
                }
                else
                {
                    deptgradeNode = gradeyear_dept_map[gradeyear][dept];
                }
                #endregion


                #region 異動紀錄
                XmlElement dataElement = doc.CreateElement("異動紀錄");
                dataElement.SetAttribute("編號", helper.GetText("@ID"));
                dataElement.SetAttribute("異動代號", helper.GetText("UpdateCode"));
                dataElement.SetAttribute("異動日期", CDATE(helper.GetText("UpdateDate")));
                dataElement.SetAttribute("學號", helper.GetText("StudentNumber"));
                dataElement.SetAttribute("姓名", helper.GetText("Name"));
                dataElement.SetAttribute("身分證號", helper.GetText("IDNumber"));
                dataElement.SetAttribute("性別", helper.GetText("Gender"));
                dataElement.SetAttribute("性別代號", (helper.GetText("Gender") == "男" ? "1" : (helper.GetText("Gender") == "女" ? "2" : "")));
                dataElement.SetAttribute("出生年月日", CDATE(helper.GetText("Birthdate")));
                dataElement.SetAttribute("入學資格代號", helper.GetText("UpdateCode"));

                // new 
                dataElement.SetAttribute("班別", helper.GetText("ContextInfo/ContextInfo/OriginClassName"));
                dataElement.SetAttribute("座號", helper.GetText("ContextInfo/ContextInfo/SeatNo"));
                dataElement.SetAttribute("地址", helper.GetText("ContextInfo/ContextInfo/OriginAddress"));
                dataElement.SetAttribute("入學年月", helper.GetText("ContextInfo/ContextInfo/EnrollmentSchoolYear"));
                dataElement.SetAttribute("學籍核准文號", helper.GetText("LastADNumber"));
                dataElement.SetAttribute("異動年級", helper.GetText("GradeYear"));


                string schoolName = helper.GetText("ContextInfo/ContextInfo/GraduateSchool");

                dataElement.SetAttribute("畢業國小", schoolName);

                dataElement.SetAttribute("畢業國小所在縣市代號", helper.GetText("ContextInfo/ContextInfo/GraduateSchoolLocationCode"));
                dataElement.SetAttribute("備註", helper.GetText("Comment"));
                #endregion
                deptgradeNode.AppendChild(dataElement);

            }
            #endregion

            return doc.DocumentElement;
        }


        #endregion
    }
}
