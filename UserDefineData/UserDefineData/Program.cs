using System;
using System.Collections.Generic;
using System.Text;
using FISCA;
using DataRationality;

namespace UserDefineData
{        
    /// <summary>
    /// 自訂資料欄位
    /// </summary>
    public class Program
    {
        [MainMethod("UserDefineData")]
        public static void Main()
        {
            // 加入合理性檢查
            // 自訂欄位資料欄位重複
            try
            {
                DataRationalityManager.Checks.Add(new DoubleUserDefDataRAT());
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show("自訂資料欄位合理性檢查" + ex.Message);
            }

            LoadManager.Start();
        }
    }
}
