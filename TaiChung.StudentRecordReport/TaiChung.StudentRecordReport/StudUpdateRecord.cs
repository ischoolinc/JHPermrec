using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaiChung.StudentRecordReport
{
    /// <summary>
    /// 異動資料
    /// </summary>
    public class StudUpdateRecord
    {
        /// <summary>
        /// 異動日期
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 異動類別
        /// </summary>
        public string UpdateType { get; set; }

        /// <summary>
        /// 核准機關
        /// </summary>
        public string ADGov { get; set; }

        /// <summary>
        /// 核准日期
        /// </summary>
        public DateTime? ADDate { get; set; }

        /// <summary>
        /// 核准文號
        /// </summary>
        public string ADDocNo { get; set; }

        /// <summary>
        /// 縣市
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// 學校名稱
        /// </summary>
        public string SchoolName { get; set; }
    }
}
