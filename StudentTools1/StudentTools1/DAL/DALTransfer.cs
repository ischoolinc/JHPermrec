using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JHSchool.Data;

namespace StudentTools1.DAL
{
    class DALTransfer
    {
        /// <summary>
        /// 排序方式
        /// </summary>
        public enum OrderBy {學號,班座 }

        /// <summary>
        /// 取得學生資料
        /// </summary>
        /// <returns></returns>
        public static List<StudDataEntity> GetStudentDataList(List<string> StudentIDList)
        {
            List<StudDataEntity> retVal = new List<StudDataEntity>();

            // 取得地址資料
            Dictionary<string, JHAddressRecord> StudAddressDict = JHAddress.SelectByStudentIDs(StudentIDList).ToDictionary(x => x.RefStudentID);

            // 取得父母監護人
            Dictionary<string, JHParentRecord> StudParentDict = JHParent.SelectByStudentIDs(StudentIDList).ToDictionary(x => x.RefStudentID);

            // 取得電話
            Dictionary<string, JHPhoneRecord> StudPhoneDict = JHPhone.SelectByStudentIDs(StudentIDList).ToDictionary(x => x.RefStudentID);

            foreach (JHStudentRecord stud in JHStudent.SelectByIDs(StudentIDList))
            {
                StudDataEntity sde = new StudDataEntity();
                // 學生系統編號
                sde.StudentID = stud.ID;
                // 學號
                sde.StudentNumber = stud.StudentNumber;
                sde.Add("學號", sde.StudentNumber);
                // 姓名
                sde.Name = stud.Name;
                sde.Add("姓名", sde.Name);
                // 座號
                if (stud.SeatNo.HasValue)
                {
                    sde.SeatNo = stud.SeatNo.Value;
                    sde.Add("座號", string.Format("{0:00}",sde.SeatNo.Value));
                }
                // 班級
                if (stud.Class != null)
                {
                    sde.ClassName = stud.Class.Name;
                    sde.Add("班級", sde.ClassName);
                }
                // 完整戶籍地址
                if (StudAddressDict.ContainsKey(stud.ID))
                    sde.Add("完整戶籍地址", StudAddressDict[stud.ID].PermanentAddress);

                // 完整聯絡地址
                if (StudAddressDict.ContainsKey(stud.ID))
                    sde.Add("完整聯絡地址", StudAddressDict[stud.ID].MailingAddress);

                // 身分證號
                sde.Add("身分證號", stud.IDNumber);

                // 性別
                sde.Add("性別", stud.Gender);

                // 監護人姓名
                if (StudParentDict.ContainsKey(stud.ID))
                    sde.Add("監護人姓名", StudParentDict[stud.ID].CustodianName);

                // 學生戶籍電話
                if (StudPhoneDict.ContainsKey(stud.ID))
                    sde.Add("戶籍電話", StudPhoneDict[stud.ID].Permanent);

                // 學生聯絡電話
                if (StudPhoneDict.ContainsKey(stud.ID))
                    sde.Add("聯絡電話", StudPhoneDict[stud.ID].Contact);

                // 生日
                if (stud.Birthday.HasValue)
                {
                    sde.Add("西元生日(年/月/日)", stud.Birthday.Value.ToShortDateString());

                    string strb1 = (stud.Birthday.Value.Year - 1911) + "/" + string.Format("{0:00}", stud.Birthday.Value.Month) + "/" + string.Format("{0:00}", stud.Birthday.Value.Day);
                    string strb2 = (stud.Birthday.Value.Year - 1911) + string.Format("{0:00}", stud.Birthday.Value.Month) + string.Format("{0:00}", stud.Birthday.Value.Day);

                    sde.Add("民國生日(0年/0月/0日)", strb1);
                    sde.Add("民國生日(0年0月0日)", strb2);
                }

                retVal.Add(sde);
            
            }
            return retVal;
        }

        
        /// <summary>
        /// 依班級座號或學號排序
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static List<StudDataEntity> SortBy(List<StudDataEntity> Data,OrderBy OrderByType)
        {
            List<StudDataEntity> retVal;

            if (OrderByType == OrderBy.班座)
            {
                var x = from stud in Data orderby stud.ClassName, stud.SeatNo select stud;
                retVal = x.ToList();
            }
            else
            {
                var x = from stud in Data orderby stud.StudentNumber select stud;
                retVal = x.ToList();
            }

            return retVal;
        }

    }
}
