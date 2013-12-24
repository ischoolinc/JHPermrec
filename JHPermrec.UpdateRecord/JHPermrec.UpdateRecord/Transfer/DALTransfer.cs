using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Data;

namespace JHPermrec.UpdateRecord.Transfer
{
    // 轉入異動使用
    class DALTransfer
    {
        /// <summary>
        /// 透過身分證取得學生紀錄
        /// </summary>
        /// <param name="SSN"></param>
        /// <returns></returns>
        public static JHSchool.Data.JHStudentRecord GetStudentRecBySSN(string SSN)
        {
            foreach (JHSchool.Data.JHStudentRecord studRec in JHSchool.Data.JHStudent.SelectAll())
                if (studRec.IDNumber.ToUpper() == SSN.ToUpper())
                    return studRec;
            return null;            
        }

        /// <summary>
        /// 取得國籍
        /// </summary>
        /// <returns></returns>
        public static List<string> GetNationalities()
        {
            List<string> list = new List<string>();

            list.Add("中華民國");
            list.Add("中華人民共合國");
            list.Add("孟加拉");
            list.Add("緬甸");
            list.Add("印尼");
            list.Add("日本");
            list.Add("韓國");
            list.Add("馬來西亞");
            list.Add("菲律賓");
            list.Add("新加坡");
            list.Add("泰國");
            list.Add("越南");
            list.Add("汶萊");
            list.Add("澳大利亞");
            list.Add("紐西蘭");
            list.Add("埃及");
            list.Add("南非");
            list.Add("法國");
            list.Add("義大利");
            list.Add("瑞典");
            list.Add("英國");
            list.Add("德國");
            list.Add("加拿大");
            list.Add("哥斯大黎加");
            list.Add("瓜地馬拉");
            list.Add("美國");
            list.Add("阿根廷");
            list.Add("巴西");
            list.Add("哥倫比亞");
            list.Add("巴拉圭");
            list.Add("烏拉圭");
            list.Add("其他");

            return list;
        }

        /// <summary>
        /// 取得班級編號與名稱
        /// </summary>
        /// <returns>編號與名稱</returns>
        public static Dictionary<string, string> GetClassNameList()
        {
            Dictionary<string, string> classes = new Dictionary<string, string>();
            List<JHSchool.Data.JHClassRecord > _AllClassRec = JHSchool.Data.JHClass.SelectAll ();
            _AllClassRec.Sort(new Comparison<JHSchool.Data.JHClassRecord>(JHClassNameSorter));

            foreach (JHSchool.Data.JHClassRecord classRecord in _AllClassRec )
                classes.Add(classRecord.ID, classRecord.Name);            

            return classes;
        }

        private static int JHClassNameSorter(JHSchool.Data.JHClassRecord x, JHSchool.Data.JHClassRecord y)
        {
            return x.Name.CompareTo(y.Name);
        }    
    }
}
