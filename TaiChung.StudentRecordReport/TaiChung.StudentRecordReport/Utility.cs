using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Data;
using System.Data;
using System.Xml.Linq;
using K12.Data.Configuration;
using Aspose.Words;

namespace TaiChung.StudentRecordReport
{
    public class Utility
    {
        /// <summary>
        /// 透過班級ID取得班級內學生狀態一般與輟學的學生ID
        /// </summary>
        /// <param name="ClassIDList"></param>
        /// <returns></returns>
        public static List<string> GetStudentIDList18ByClassID(List<string> ClassIDList)
        {
            List<string> retVal = new List<string>();
            if (ClassIDList.Count > 0)
            {
                QueryHelper qh = new QueryHelper();
                string strSQL = "select student.id from student inner join class on student.ref_class_id=class.id where student.status in(1,8) and class.id in("+string.Join(",",ClassIDList.ToArray())+") order by class.class_name,student.seat_no;";
                DataTable dt = qh.Select(strSQL);
                foreach (DataRow dr in dt.Rows)
                    retVal.Add(dr[0].ToString());
            }
            return retVal;        
        }

        /// <summary>
        /// 班座排序
        /// </summary>
        /// <param name="StudIDList"></param>
        /// <returns></returns>
        public static List<string> GetStudentIDListByStudentID(List<string> StudIDList)
        {
            List<string> retVal = new List<string>();
            if (StudIDList.Count > 0)
            {
                QueryHelper qh = new QueryHelper();
                string strSQL = "select student.id from student left join class on student.ref_class_id=class.id where student.id in(" + string.Join(",", StudIDList.ToArray()) + ") order by class.class_name,student.seat_no;";
                DataTable dt = qh.Select(strSQL);
                foreach (DataRow dr in dt.Rows)
                    retVal.Add(dr[0].ToString());
            }
            return retVal;  
        }

        /// <summary>
        /// 西元轉民國101/1/1
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ConvertDate1(DateTime? dt)
        {
            string retVal = "";
            if (dt.HasValue)            
                retVal = (dt.Value.Year - 1911) + "/" + dt.Value.Month + "/" + dt.Value.Day;

            return retVal;
        
        }

        /// <summary>
        /// 轉換decimal
        /// </summary>
        /// <param name="dd"></param>
        /// <returns></returns>
        public static string ConvertDecimal1(decimal? dd)
        {
            string retVal = "";
            if (dd.HasValue)
                retVal = dd.Value.ToString();
            return retVal;
        }

        /// <summary>
        /// 取得等第對照
        /// </summary>
        /// <returns></returns>
        public static Dictionary<decimal,string> GetScoreLevelMapping()
        {
            Dictionary<decimal, string> retVal = new Dictionary<decimal, string>();
            QueryHelper qh = new QueryHelper();            
            string strSQL = "select content from list where name='等第對照表';" ;
            DataTable dt = qh.Select(strSQL);
            string xmlStr = "";
            foreach (DataRow dr in dt.Rows)
                xmlStr = dr[0].ToString();

            try
            {
                if (xmlStr != "")
                {
                    XElement elmRoot = XElement.Parse(xmlStr);
                    if (elmRoot != null)
                    {
                        string elmstr1 = elmRoot.Element("Configuration").Value;
                        XElement elmXml = XElement.Parse(elmstr1);
                        Dictionary<decimal, string> tmp = new Dictionary<decimal, string>();
                        foreach (XElement elm in elmXml.Elements("ScoreMapping"))
                        {
                            decimal dd;
                            if(elm.Attribute("Score") !=null && elm.Attribute("Name") !=null) 
                            if(decimal.TryParse(elm.Attribute("Score").Value,out dd))
                                tmp.Add(dd,elm.Attribute("Name").Value);                            
                        }
                        List<decimal> tmpsort = new List<decimal>();

                        tmpsort = (from data in tmp orderby data.Key descending select data.Key).ToList();
                        
                        foreach (decimal d in tmpsort)
                        {
                            if (tmp.ContainsKey(d))
                                retVal.Add(d, tmp[d]);
                        }

                        if (!retVal.ContainsKey(0))
                            retVal.Add(0, "丁");
                    }

                }
            }
            catch { }
            return retVal;
        
        }

        /// <summary>
        /// 取得等第對照後結果
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public static string GetScoreLevel(decimal? score)
        {
            string retVal = "";
            if (score.HasValue)
            {                
                foreach (KeyValuePair<decimal, string> data in Global._ScoreLevelMapping)
                {
                    if (score.Value >= data.Key)
                    {
                        retVal = data.Value;
                        break;
                    }
                }
            }
            return retVal;
        }


        public static Dictionary<string, List<StudTextScore>> GetStudentTextScoreDict(List<string> StudentIDList)
        {
            Dictionary<string, List<StudTextScore>> retVal = new Dictionary<string, List<StudTextScore>>();
            if (StudentIDList.Count > 0)
            {
                QueryHelper qh = new QueryHelper();
                string strSQL = "select ref_student_id,school_year,semester,sb_comment,text_score from sems_moral_score where ref_student_id in(" + string.Join(",", StudentIDList.ToArray()) + ")";
                DataTable dt = qh.Select(strSQL);

                List<StudTextScore> dataList = new List<StudTextScore>();
                foreach (DataRow dr in dt.Rows)
                {
                    StudTextScore sts = new StudTextScore();
                    sts.StudentID = dr[0].ToString();
                    sts.SchoolYear = int.Parse(dr[1].ToString());
                    sts.Semester = int.Parse(dr[2].ToString());
                    sts.DailyLifeRecommend = "";
                    // 解析資料
                    if (dr[4] != null && dr[4].ToString() != "")
                    {
                        XElement elm = XElement.Parse(dr[4].ToString());
                        if (elm != null)
                        {
                            if (elm.Element("DailyLifeRecommend") != null)
                            {
                                if (elm.Element("DailyLifeRecommend").Value != "")
                                    sts.DailyLifeRecommend = elm.Element("DailyLifeRecommend").Value;
                                else
                                    if (elm.Element("DailyLifeRecommend").Attribute("Description") != null)
                                        sts.DailyLifeRecommend = elm.Element("DailyLifeRecommend").Attribute("Description").Value;
                            }
                        }
                    }
                    dataList.Add(sts);
                }

                // 填入並依年級學期排序
                foreach (string str in StudentIDList)
                    retVal.Add(str, (from data in dataList where data.StudentID == str orderby data.GradeYear, data.Semester select data).ToList());
            }
            return retVal;
        }

        /// <summary>
        /// 取得學生畢業領域成績(國中)
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, List<StudGraduateScore>> GetStudGraduateDictJH(List<string> StudentIDList)
        {
            Dictionary<string, List<StudGraduateScore>> retVal = new Dictionary<string, List<StudGraduateScore>>();
            if (StudentIDList.Count > 0)
            {
                QueryHelper qh = new QueryHelper();
                string strSQL = "select id,grad_score from student where id in('" + string.Join("','", StudentIDList.ToArray()) + "')";
                DataTable dt = qh.Select(strSQL);

                foreach (DataRow dr in dt.Rows)
                {                    
                    string id = dr[0].ToString();
                    string strXml = dr[1].ToString();
                    List<StudGraduateScore> scoreList = new List<StudGraduateScore>();
                    if (strXml != "")
                    {
                        XElement elms = XElement.Parse(strXml);
                        foreach (XElement elm in elms.Elements("Domain"))
                        {
                            StudGraduateScore sgs = new StudGraduateScore();
                                                        
                            decimal s;
                            sgs.Name = elm.Attribute("Name").Value;
                            if (elm.Attribute("Score") != null)
                                if (decimal.TryParse(elm.Attribute("Score").Value, out s))
                                    sgs.Score = s;
                            sgs.Level = Utility.GetScoreLevel(sgs.Score);
                            scoreList.Add(sgs);
                        }

                        // 處理學習領域
                        if (elms.Element("LearnDomainScore") != null)
                        {
                            StudGraduateScore ss = new StudGraduateScore();
                            decimal dc;
                            if (decimal.TryParse(elms.Element("LearnDomainScore").Value, out dc))
                                ss.Score = dc;
                            ss.Level = Utility.GetScoreLevel(ss.Score);
                            ss.Name = "學習領域";
                            scoreList.Add(ss);
                        }
                        
                    }
                    retVal.Add(id, scoreList);
                }
            }
            return retVal;
        }

        /// <summary>
        /// 取得假別設定
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<string>> GetTypes()
        {
            Dictionary<string, List<string>> retVal = new Dictionary<string, List<string>>();
            ConfigData cd = K12.Data.School.Configuration[Global.ReportName];
            if (cd.Contains("假別設定"))
            {
                XElement config = XElement.Parse(cd["假別設定"]);

                foreach (XElement type in config.Elements("Type"))
                {
                    if (type.Attribute("Text") == null)
                        continue;

                    string typeName = type.Attribute("Text").Value;
                    if (!retVal.ContainsKey(typeName))
                        retVal.Add(typeName, new List<string>());

                    foreach (XElement absence in type.Elements("Absence"))
                    {
                        if (absence.Attribute("Text") == null)
                            continue;

                        string absenceName = absence.Attribute("Text").Value;
                        if (!retVal[typeName].Contains(absenceName))
                            retVal[typeName].Add(absenceName);
                    }
                }
            }
            return retVal;        
        }

        /// <summary>
        /// 取得身分註記
        /// </summary>
        /// <param name="studIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetStudIDTypeList(List<string> studIDList)
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();

            if (studIDList.Count > 0)
            {
                QueryHelper qh = new QueryHelper();
                string strSQL = "select ref_student_id,name from tag_student inner join tag on tag_student.ref_tag_id=tag.id where tag.prefix='身分註記'";
                DataTable dt =qh.Select(strSQL);
                foreach (DataRow dr in dt.Rows)
                {
                    string id = dr[0].ToString();
                    string name=dr[1].ToString();
                    if (retVal.ContainsKey(id))
                        retVal[id] += "," + name;
                    else
                        retVal.Add(id, name);
                }
            }
            return retVal;        
        }

        /// <summary>
        /// 處理 Aspose Word 水平合併(左至右)(單一)
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void HorizontallyMergeCells(Cell left, Cell right)
        {
            left.CellFormat.HorizontalMerge = CellMerge.First;

            foreach (Node child in right.ChildNodes)
                left.AppendChild(child);

            right.CellFormat.HorizontalMerge = CellMerge.Previous;

            // 處理移除合併後有 \r\n 問題
            if (left.HasChildNodes)
                left.RemoveChild(left.LastChild);
        }

        /// <summary>
        /// Aspose Word 垂直合併上至下(單一)
        /// </summary>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        public static void VerticallyMergeCells(Cell top, Cell bottom)
        {
            top.CellFormat.VerticalMerge = CellMerge.First;

            foreach (Node child in bottom.ChildNodes)
                top.AppendChild(child);

            bottom.CellFormat.VerticalMerge = CellMerge.Previous;

            if (top.HasChildNodes)
                top.RemoveChild(top.LastChild);
        }
        
        /// <summary>
        /// 取得目前縣市
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCountyList()
        {
            List<string> retVal = new List<string>();
            retVal.Add("大陸地區");
            retVal.Add("台中市");
            retVal.Add("台北市");
            retVal.Add("台東縣");
            retVal.Add("台南市");
            retVal.Add("宜蘭縣");
            retVal.Add("花蓮縣");
            retVal.Add("金門縣");
            retVal.Add("南投縣");
            retVal.Add("屏東縣");
            retVal.Add("苗栗縣");
            retVal.Add("桃園縣");
            retVal.Add("海外地區");
            retVal.Add("高雄市");
            retVal.Add("基隆市");
            retVal.Add("連江縣");
            retVal.Add("雲林縣");
            retVal.Add("新北市");
            retVal.Add("新竹市");
            retVal.Add("新竹縣");
            retVal.Add("嘉義市");
            retVal.Add("嘉義縣");
            retVal.Add("彰化縣");
            retVal.Add("臺中市");
            retVal.Add("臺北市");
            retVal.Add("臺東縣");
            retVal.Add("臺南市");
            retVal.Add("澎湖縣");
            return retVal;
        }


    }
}
