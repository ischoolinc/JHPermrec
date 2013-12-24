using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentTools1.Export
{
    /// <summary>
    /// 欄位名稱實體
    /// </summary>
    class ColumnIndexEntity
    {
        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string ColumnName{get;set;}
        /// <summary>
        /// 欄位位置
        /// </summary>
        public int ColumnIndex{get;set;}

        /// <summary>
        /// 是否顯示
        /// </summary>
        public bool Visible { get; set; }
    }
}
