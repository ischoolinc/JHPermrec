using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaiChung.StudentRecordReport
{
    /// <summary>
    /// 成績合併欄位使用
    /// </summary>
    public class ScoreNameMergeIdx
    {
        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 子名稱
        /// </summary>
        public List<string> SubNameList { get; set; }

        /// <summary>
        /// 是否列合併
        /// </summary>
        public bool isRowMerge { get; set; }

        /// <summary>
        /// 合併列數
        /// </summary>
        public int MergeRowCount { get; set; }

        /// <summary>
        /// 是否欄合併
        /// </summary>
        public bool isColMerge { get; set; }
           
        /// <summary>
        /// 合併欄數
        /// </summary>
        public int MergeColCount { get; set; }

    }
}
