using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace JHPermrec.UpdateRecord.DAL
{
    // 異動名冊
    public class StudBatchUpdateRecEntity
    {
        private Dictionary<string, StudBatchUpdateRecContentEntity> _ContentData;

        public  enum GovDocType { 新生名冊, 畢業名冊, 轉入學生名冊, 轉出學生名冊, 中輟學生名冊, 休學學生名冊, 復學學生名冊, 續讀學生名冊, 更正學籍學生名冊 }

        private static XmlElement _XmlSouce;

        public StudBatchUpdateRecEntity()
        {
            _ContentData = new Dictionary<string, StudBatchUpdateRecContentEntity>();
        }

        /// <summary>
        /// 異動名冊編號
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 異動名冊名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 異動名冊學年度
        /// </summary>
        public int SchoolYear { get; set; }

        /// <summary>
        /// 異動名冊學期
        /// </summary>
        public int Semester { get; set; }

        /// <summary>
        /// 異動名冊核准日期
        /// </summary>
        public DateTime ADDate { get; set; }    

        /// <summary>
        /// 異動名冊核准文號
        /// </summary>
        public string ADNumber { get; set; }

        /// <summary>
        /// 學校名稱
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 名冊類別
        /// </summary>
        public GovDocType DocType { get; set; }

        /// <summary>
        /// 學校代號
        /// </summary>
        public string SchoolCode { get; set; }


        /// <summary>
        /// 取得轉換後的 Content
        /// </summary>
        /// <param name="Xml"></param>
        /// <returns></returns>
        public Dictionary<string, StudBatchUpdateRecContentEntity> GetContentData()
        {               
            XmlElement ElemRoot = UpdateBatchRec.Content.FirstChild as XmlElement;

            return ConvertGetContentData(ElemRoot);
        }

        /// <summary>
        /// 轉換異動內資料
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Dictionary<string, StudBatchUpdateRecContentEntity> ConvertGetContentData(XmlElement source)
        {
            _XmlSouce = source;
            Dictionary<string, StudBatchUpdateRecContentEntity> retValue = new Dictionary<string, StudBatchUpdateRecContentEntity>();

            if (source != null)
            {
                foreach (XmlNode list in source.SelectNodes("清單"))
                    foreach (XmlNode xn in list.SelectNodes("異動紀錄"))
                    {
                        XmlElement xe = xn as XmlElement;
                        StudBatchUpdateRecContentEntity sburce = new StudBatchUpdateRecContentEntity(xe);
                        if (!retValue.ContainsKey(sburce.GetID()))
                            retValue.Add(sburce.GetID(), sburce);
                    }
            }
            return retValue;
        }

        /// <summary>
        /// 轉回 DAL 用 Content
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public void SetContentData(Dictionary<string, StudBatchUpdateRecContentEntity> data)
        {
            XmlElement retValue = new XmlDocument().CreateElement("Content");
            Dictionary<string, List<StudBatchUpdateRecContentEntity>> ByGradeYear = new Dictionary<string, List<StudBatchUpdateRecContentEntity>>();
            foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
            {
                string strGRYear;
                if (string.IsNullOrEmpty(sburce.GetGradeYear()))
                    strGRYear = " ";
                else
                    strGRYear = sburce.GetGradeYear();

                if (ByGradeYear.ContainsKey(strGRYear))
                    ByGradeYear[strGRYear].Add(sburce);
                else
                {
                    List<StudBatchUpdateRecContentEntity> list = new List<StudBatchUpdateRecContentEntity>();
                    list.Add(sburce);
                    ByGradeYear.Add(strGRYear, list);
                }
            
                // 清除原 XML
                UpdateBatchRec.Content.RemoveAll();

                XmlElement elemRoot = new XmlDocument().CreateElement("異動名冊");
                elemRoot.SetAttribute("學年度", SchoolYear+"");
                elemRoot.SetAttribute("學期", Semester + "");
                elemRoot.SetAttribute("學校名稱", SchoolName);
                elemRoot.SetAttribute("學校代號", SchoolCode);
                elemRoot.SetAttribute("類別", DocType.ToString ());

                foreach (KeyValuePair<string, List<StudBatchUpdateRecContentEntity>> val in ByGradeYear)
                {
                    XmlElement elemGrdYear = new XmlDataDocument().CreateElement("清單");
                    elemGrdYear.SetAttribute("年級", val.Key);
                    elemGrdYear.SetAttribute("科別", "");

                    foreach (StudBatchUpdateRecContentEntity sburce1 in val.Value)
                    {
                        XmlElement xe = elemGrdYear.OwnerDocument.ImportNode(sburce1.GetXmlData(), true)as XmlElement;
                        elemGrdYear.AppendChild(xe);
                    }

                    XmlElement XmlRootChild = elemRoot.OwnerDocument.ImportNode(elemGrdYear, true) as XmlElement;
                    elemRoot.AppendChild(XmlRootChild);
                }

                XmlElement elemRot = UpdateBatchRec.Content.OwnerDocument.ImportNode(elemRoot, true) as XmlElement;
                UpdateBatchRec.Content.AppendChild(elemRot);
            }          
            
        }

        /// <summary>
        /// 異動名冊
        /// </summary>
        public JHSchool.Data.JHUpdateReocrdBatchRecord UpdateBatchRec{get;set;}

        /// <summary>
        /// 取得Content內學校名稱
        /// </summary>
        /// <returns></returns>
        public static string GetContentSchoolName()
        {
            if (_XmlSouce == null)
                return string.Empty;
            else
                return _XmlSouce.GetAttribute("學校名稱");
        }

        /// <summary>
        /// 取得Content內學年度
        /// </summary>
        /// <returns></returns>
        public static string GetContentSchoolYear()
        {
            if (_XmlSouce == null)
                return string.Empty;
            else
                return _XmlSouce.GetAttribute("學年度");
            
        }

        /// <summary>
        /// 取得Content內學期
        /// </summary>
        /// <returns></returns>
        public static string GetContentSemester()
        {
            if (_XmlSouce == null)
                return string.Empty;
            else
                return _XmlSouce.GetAttribute("學期");
        }

        /// <summary>
        /// 學校代號
        /// </summary>
        /// <returns></returns>
        public static string GetContentSchoolCode()
        {
            if (_XmlSouce == null)
                return string.Empty;
            else
                return _XmlSouce.GetAttribute("學校代號");
        }

    }
}

