using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaiChung.StudentRecordReport
{
    /// <summary>
    /// 畢業成績
    /// </summary>
    public class StudGraduateScore
    {
        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 成績
        /// </summary>
        public decimal? Score { get; set; }

        /// <summary>
        /// 等第
        /// </summary>
        public string Level { get; set; }
    }
}
