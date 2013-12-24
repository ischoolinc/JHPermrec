using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaiChung.StudentRecordReport
{
    public class StudAbsenceCount
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
        /// 統計值
        /// </summary>
        public Dictionary<string, Dictionary<string, int>> AbsenceCount = new Dictionary<string, Dictionary<string, int>>();


    }
}
