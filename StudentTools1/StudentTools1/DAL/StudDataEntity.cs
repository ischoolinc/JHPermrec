using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentTools1.DAL
{
    /// <summary>
    /// 學生資料
    /// </summary>
    class StudDataEntity
    {
        private Dictionary<string, string> _DataDic = new Dictionary<string, string>();
        /// <summary>
        /// 學生系統編號
        /// </summary>
        public string StudentID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// 班級
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 座號
        /// </summary>
        public int? SeatNo { get; set;}

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void Add(string Key, string Value)
        {
            if (_DataDic.ContainsKey(Key))
                _DataDic[Key] = Value;
            else
                _DataDic.Add(Key, Value);
        }

        /// <summary>
        /// 取得資料
        /// </summary>
        /// <returns></returns>
        public Dictionary <string, string> GetData()
        {
            return _DataDic;        
        }
        
    }
}
