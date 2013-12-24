using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Student_JSBT_HsinChu101
{
    public class Global
    {
        /// <summary>
        /// 電話對照使用StudentID,PhoneTyye,Phone
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> _tempPhomeDict = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 使用這選擇電話型態
        /// </summary>
        public static string SelectPhoneType = "";
    }
}
