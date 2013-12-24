using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaiChung.StudentRecordReport
{
    /// <summary>
    /// 學生領域成績
    /// </summary>
    public class StudDomainScore
    {
        /// <summary>
        /// 學年度
        /// </summary>
        public int SchoolYear { get; set; }

        /// <summary>
        /// 學期
        /// </summary>
        public int Semester { get; set; }

        /// <summary>
        /// 領域名稱
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// 科目名稱
        /// </summary>
        public List<StudSubjectScore> StudSubjectScoreList = new List<StudSubjectScore>();

        /// <summary>
        /// 成績
        /// </summary>
        public decimal? Score { get; set; }

        /// <summary>
        /// 節數
        /// </summary>
        public decimal? Period { get; set; }

        /// <summary>
        /// 等第
        /// </summary>
        public string Level { get; set; }
    }
}
