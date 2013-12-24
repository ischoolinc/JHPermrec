using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaiChung.StudentRecordReport
{
    /// <summary>
    /// 學生學期歷程
    /// </summary>
    public class StudSemsHistory
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
        /// 年級
        /// </summary>
        public int GradeYear { get; set; }

        /// <summary>
        /// 班導師
        /// </summary>
        public string ClassTeacher { get; set; }

        /// <summary>
        /// 上課天數
        /// </summary>
        public int? SchoolDayCount { get; set; }

        /// <summary>
        /// 資料存放開始索引
        /// </summary>
        public int dataStartIdx { get; set; }
    }
}
