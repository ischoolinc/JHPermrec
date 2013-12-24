using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaiChung.StudentRecordReport
{
    /// <summary>
    /// 學生日常生活具體建議
    /// </summary>
    public class StudTextScore
    {
        /// <summary>
        /// Student ID
        /// </summary>
        public string StudentID { get; set; }

        /// <summary>
        /// 學年度
        /// </summary>
        public int SchoolYear { get; set; }

        /// <summary>
        /// 年級
        /// </summary>
        public int GradeYear { get; set; }

        /// <summary>
        /// 學期
        /// </summary>
        public int Semester { get; set; }

        /// <summary>
        /// 具體建議
        /// </summary>
        public string DailyLifeRecommend { get; set; }
    }
}
