using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHPermrec.UpdateRecord
{
    public static class StudCheckTool
    {
        /// <summary>
        /// 檢查身分證號是否重複
        /// </summary>
        /// <param name="strIDNumber"></param>
        /// <returns></returns>
        public static bool CheckStudIDNumberSame(string strIDNumber,string StudentID)
        {
            JHSchool.Data.JHStudent.RemoveAll();
            JHSchool.Data.JHStudent.SelectAll();
            bool retValue = false;
            byte count = 0;

            foreach (JHSchool.Data.JHStudentRecord stud in JHSchool.Data.JHStudent.SelectAll())
                if (stud.IDNumber.ToUpper() == strIDNumber.ToUpper() && stud.Status != K12.Data.StudentRecord.StudentStatus.刪除)
                {
                    if (string.IsNullOrEmpty(StudentID))
                    {
                        count++;
                        if (count == 2)
                        {
                            retValue = true;
                            break;
                        }
                        
                    }
                    else
                    {
                        if (stud.ID == StudentID)
                            continue;
                        else
                        {
                            retValue = true;
                            break;
                        }
                    }
                }
            return retValue;
        }
    }
}
