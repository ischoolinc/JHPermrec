using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JHPermrec.UpdateRecord.GovernmentalDocument.NameList
{
    public class UpdateRecordUtil
    {

        // 捨棄不用
        ////// 79/01/01
        //public static string ChangeDate1900(string str)
        //{
        //    if (string.IsNullOrEmpty(str))
        //        return "";

        //    string returnValue = "";
        //    DateTime dt;
        //    if (DateTime.TryParse(str, out dt))
        //    {
        //        returnValue = (dt.Year - 1900)+"/";
        //        if (dt.Month < 10)
        //            returnValue += "0" + dt.Month;
        //        else
        //            returnValue += "" + dt.Month;

        //        if (dt.Day < 10)
        //            returnValue += "/0" + dt.Day;
        //        else
        //            returnValue += "/" + dt.Day;
        //    }
        //    return returnValue;
        //}

        /// <summary>
        /// 西元轉民國(-1911)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ChangeDate1911(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty; ;

            string returnValue = "";
            DateTime dt;
            if (DateTime.TryParse(str, out dt))
            {
                returnValue = (dt.Year - 1911) + "/";
                if (dt.Month < 10)
                    returnValue += "0" + dt.Month;
                else
                    returnValue += ""+dt.Month;

                if (dt.Day < 10)
                    returnValue += "/0" + dt.Day;
                else
                    returnValue += "/" + dt.Day;
            }
            return returnValue;
        }

        /// <summary>
        /// 回傳報表用民國年
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static string getChineseYearStr(string strDate)
        {
            strDate = strDate.Trim();
            string strYear="";
            int year=0;
            if(strDate.Length ==6)
            {
                int.TryParse (strDate.Substring(0,4) ,out year);
                if(year>0)
                    strYear=(year-1911).ToString ();
            }
            return strYear;
        }

        /// <summary>
        /// 取得報表用月
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="monthAddZero"></param>
        /// <returns></returns>
        public static string getMonthStr(string strDate,bool monthAddZero)
        {
            strDate = strDate.Trim();
            string strMonth = "";
            int month=0;
            if (strDate.Length == 6)
            {
                int.TryParse (strDate.Substring(4, 2),out month );
                if (month > 0)
                {
                    if (monthAddZero == true && month < 10)
                        strMonth = "0" + month;
                    else
                        strMonth = "" + month;
                }
            }
            return strMonth;
        }

    }
}
