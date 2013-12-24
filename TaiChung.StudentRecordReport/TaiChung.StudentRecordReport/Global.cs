using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaiChung.StudentRecordReport
{
    public class Global
    {
        /// <summary>
        /// 學生基本資料表格
        /// </summary>
        public static int _T_StudBaseInfoIdx = 0;

        /// <summary>
        /// 學生學期領域成績表格
        /// </summary>
        public static int _T_StudScoreInfoIdx = 1;

        /// <summary>
        /// 學生學期成績表格
        /// </summary>
        public static int _T_StudScoreExInfoIdx =2;
        /// <summary>
        /// 學生出缺席表格
        /// </summary>
        public static int _T_StudAbsentInfoIdx = 3;

        /// <summary>
        /// 學生日常生活表現紀錄表格
        /// </summary>
        public static int _T_StudPerformanceInfoIdx = 4;

        /// <summary>
        /// 學生承辦人表格
        /// </summary>
        public static int _T_StudContractorsIdx = 5;

   
        /// <summary>
        /// 業務承辦
        /// </summary>
        public static string _BusinessContractor { get; set; }

        /// <summary>
        /// 註冊組長
        /// </summary>
        public static string _RegisteredLeader { get; set; }

   
        /// <summary>
        /// 列印模式
        /// </summary>
        public enum PrintMode {科目,領域}

        /// <summary>
        /// 成績列印模式
        /// </summary>
        public static PrintMode _PrintMode;

        /// <summary>
        /// 等第對照
        /// </summary>
        public static Dictionary<decimal, string> _ScoreLevelMapping = new Dictionary<decimal, string>();

        public static string ReportName = "台中學籍表";

        /// <summary>
        /// 列印科目或領域 Domain,Subject
        /// </summary>
        public static string _PrintModeString = "";

        /// <summary>
        /// 假別設定
        /// </summary>
        public static Dictionary<string, List<string>> _types = new Dictionary<string, List<string>>();
    }
}
