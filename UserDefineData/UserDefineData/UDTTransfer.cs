using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FISCA.UDT;

namespace UserDefineData
{
    public class UDTTransfer
    {
        /// <summary>
        /// 取得單筆學生UDT資料
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public static List<DAL.UserDefData> GetDataFromUDT(string StudentID)
        {       
            AccessHelper accHelper = new AccessHelper();
            string query = "RefID='" + StudentID+"'";
            return accHelper.Select<DAL.UserDefData>(query);        
        }

        ///// <summary>
        ///// 取得單筆學生UDT資料，並轉換成 dic
        ///// </summary>
        ///// <param name="StudentID"></param>
        ///// <returns></returns>
        //public static Dictionary<string, DAL.UserDefData> GetDataFromUDTDict(string StudentID)
        //{
        //    // 回傳資料
        //    Dictionary<string, DAL.UserDefData> retValue = new Dictionary<string, UserDefineData.DAL.UserDefData>();

        //    // 刪除可能多餘資料
        //    List<DAL.UserDefData> DeleteList = new List<UserDefineData.DAL.UserDefData>();

        //    // 取得 UDT 內
        //    foreach (DAL.UserDefData ud in GetDataFromUDT(StudentID))
        //        if (!retValue.ContainsKey(ud.FieldName))
        //        {
        //            ud.isNull = false;
        //            retValue.Add(ud.FieldName, ud);
        //        }
        //        else
        //        {
        //            ud.Deleted = true;
        //            DeleteList.Add(ud);                    
        //        }

        //    if (DeleteList.Count > 0)
        //        DeleteDataToUDT(DeleteList);

        //    // 取得自訂欄位設定，沒有使用空白
        //    foreach (string FName in Global.GetUserConfigData().Keys)
        //        if (!retValue.ContainsKey(FName))
        //        {
        //            DAL.UserDefData ud = new UserDefineData.DAL.UserDefData();
        //            ud.RefID = StudentID;
        //            ud.FieldName = FName;
        //            ud.Value = "";                    
        //            retValue.Add(FName, ud);
        //        }
        //    return retValue;
        //}

        /// <summary>
        /// 取得多筆學生UDT資料
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static List<DAL.UserDefData> GetDataFromUDT(List<string> StudentIDList)
        {
            AccessHelper accHelper = new AccessHelper();
            string query = "RefID in ('" + String.Join("','", StudentIDList.ToArray()) + "')";
            return accHelper.Select<DAL.UserDefData>(query);            
        }

        /// <summary>
        /// 新增資料到 UDT
        /// </summary>
        /// <param name="?"></param>
        public static void InsertDataToUDT(List<DAL.UserDefData> data)
        {
            AccessHelper accHelper = new AccessHelper();
            accHelper.InsertValues(data.ToArray());        
        }

        /// <summary>
        /// 刪除 UDT 內資料
        /// </summary>
        /// <param name="data"></param>
        public static void DeleteDataToUDT(List<DAL.UserDefData> data)
        {
            AccessHelper accHelper = new AccessHelper();
            accHelper.DeletedValues(data.ToArray());        
        }

        /// <summary>
        /// 取得單筆學生UDT資料，包括"自定資料管理" (add by 小郭, 2013/12/16)
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public static List<DAL.UserDefData> GetDataFromUDTIncludeUserConfig(string StudentID)
        {
            // 回傳資料
            List<DAL.UserDefData> result = new List<DAL.UserDefData>();
            // 暫存資料, Key: 欄位名稱; Value: DAL.UserDefData
            Dictionary<string, DAL.UserDefData> temp = new Dictionary<string, UserDefineData.DAL.UserDefData>();
            // 刪除名單, 用來刪除可能重複的欄位名稱
            List<DAL.UserDefData> DeleteList = new List<UserDefineData.DAL.UserDefData>();

            // 取得UDT的資料
            List<DAL.UserDefData> userDefDataList = GetDataFromUDT(StudentID);
            // 依照uid反向排序
            userDefDataList.Sort(delegate(DAL.UserDefData obj1, DAL.UserDefData obj2)
            {
                string compare1 = (obj1.UID).PadLeft(20, '0');
                string compare2 = (obj2.UID).PadLeft(20, '0');
                return compare2.CompareTo(compare1);
            });

            // 找出重複的，並保留新的資料(uid比較大的為新的資料)
            foreach (DAL.UserDefData rec in userDefDataList)
            {
                if (!temp.ContainsKey(rec.FieldName))
                {
                    rec.isNull = false;
                    temp.Add(rec.FieldName, rec);
                }
                else
                {
                    // 假如找到重複的欄位名稱, 加入刪除名單, 因為排序過, 所以會刪除比較舊的資料
                    rec.Deleted = true;
                    DeleteList.Add(rec);
                }
            }

            if (DeleteList.Count > 0)
                DeleteDataToUDT(DeleteList);

            // 處理完需要刪除的名單後, 正向排序
            userDefDataList = temp.Values.ToList();
            userDefDataList.Reverse();

            // 取得自訂欄位設定，沒有使用空白
            int index = 0;
            foreach (string FName in Global.GetUserConfigData().Keys)
            {
                DAL.UserDefData ud;
                if (!temp.ContainsKey(FName))
                {
                    ud = new UserDefineData.DAL.UserDefData();
                    ud.RefID = StudentID;
                    ud.FieldName = FName;
                    ud.Value = "";
                }
                else
                {
                    ud = temp[FName];
                    // 先移除既有的資料
                    userDefDataList.Remove(ud);
                }

                // 新增到前面
                userDefDataList.Insert(index++, ud);
            }

            // 回傳List
            result = userDefDataList;

            return result;
        }
    }
}
