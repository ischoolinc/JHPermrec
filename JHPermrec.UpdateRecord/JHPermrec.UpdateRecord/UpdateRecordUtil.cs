using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework;

namespace JHPermrec.UpdateRecord
{
    class UpdateRecordUtil
    {
        /// <summary>
        /// 將初始值填入 Control
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        public static void LoadYearAndMonth(TextBox Year, ComboBox Month)
        {
            // 目前電腦西元年
            Year.Text = DateTime.Now.Year.ToString();

            // 加入月
            for (byte mon = 1; mon <= 12; mon++)
                Month.Items.Add(mon.ToString());

            Month.Text = DateTime.Now.Month.ToString();
            Month.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// 檢查批次異動輸入年月
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="Errors"></param>
        /// <returns></returns> 
        public static string checkYearAndMonthInput(TextBox Year, ComboBox Month, EnhancedErrorProvider Errors)
        {
            string strEnrrolSchoolYear =string.Empty,cMonth=string.Empty;
            string cYear = "";
           
            cYear = strIntCheckA (Year.Text);
            if (string.IsNullOrEmpty(cYear) || cYear.Length != 4)
            {
                Errors.SetError(Year, "請檢查學年度是否是西元年");
            }
            else
            {
                strEnrrolSchoolYear = cYear;
            }

            cMonth = strIntCheckA(Month.Text);
            if (string.IsNullOrEmpty(cMonth ))
            {
                Errors.SetError(Year, "請選擇月份");
            }
            else
                strEnrrolSchoolYear += cMonth;


            // 200901
            if (strEnrrolSchoolYear.Length != 6)
                strEnrrolSchoolYear = "";

            return strEnrrolSchoolYear;
        }

        private static string strIntCheckA(string str)
        {
            string strValue = string.Empty;

            int i;

            if (int.TryParse(str, out i))
            {
                if(i>0 && i<10)
                    strValue = "0"+ str;
                else
                    strValue = str;
            }
            return strValue;
        }
    }
}
