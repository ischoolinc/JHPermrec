using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework;
using System.Xml.Linq;
using FISCA.DSAClient;

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

        /// 上傳檔案到局端 - 2020/7/30參考班級鎖定功能
        /// </summary>
        public static string UploadFile(string ID, string Data, string FileName)
        {
            string DSNS = FISCA.Authentication.DSAServices.AccessPoint;
            string AccessPoint = @"j.kh.edu.tw";

            if (FISCA.RTContext.IsDiagMode)
            {
                string accPoint = FISCA.RTContext.GetConstant("KH_AccessPoint");
                if (!string.IsNullOrEmpty(accPoint))
                    AccessPoint = accPoint;
            }

            string Contract = "log";
            string ServiceName = "_.Upload";

            string errMsg = "";
            try
            {
                XElement xmlRoot = new XElement("Request");
                xmlRoot.SetElementValue("ID", ID);
                xmlRoot.SetElementValue("Data", Data);
                xmlRoot.SetElementValue("FileName", FileName);
                xmlRoot.SetElementValue("DSNS", DSNS);
                xmlRoot.SetElementValue("Type", "student");
                FISCA.DSAClient.XmlHelper reqXML = new FISCA.DSAClient.XmlHelper(xmlRoot.ToString());
                FISCA.DSAClient.Connection cn = new FISCA.DSAClient.Connection();
                cn.Connect(AccessPoint, Contract, DSNS, DSNS);
                Envelope rsp = cn.SendRequest(ServiceName, new Envelope(reqXML));
                XElement rspXML = XElement.Parse(rsp.XmlString);

            }
            catch (Exception ex)
            {

                errMsg = ex.Message;
            }
            return errMsg;
        }

    }
}
