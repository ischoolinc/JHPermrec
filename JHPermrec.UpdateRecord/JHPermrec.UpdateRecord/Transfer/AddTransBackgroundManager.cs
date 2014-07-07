using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHPermrec.UpdateRecord.Transfer
{
    // 在背後管理每個轉入異動 Form
    public static class AddTransBackgroundManager
    {
        private static JHSchool.Data.JHStudentRecord _Student=null ;
        private static bool _HasStudentData=false;
        public  static string StudentIDNumber { get; set; }


        public static AddTransStud AddTransStudObj;
        public static AddTransStudBase AddTransStudBaseObj;
        public static AddTransStudBaseData AddTransStudBaseDataObj;
        public static AddTransStudCourse AddTransStudCourseObj;
        public static AddTransStudCourseScore AddTransStudCourseScoreObj;
        public static AddTransStudSemsSubjScore AddTransStudSemsSubjScoreObj;
        public static AddTransStudUpdateRecord AddTransStudUpdateRecordObj;
        public static AddTransManagerForm AddTransManagerFormObj;


        /// <summary>
        /// 透過身分證號設定學生資料
        /// </summary>
        /// <param name="SSN"></param>
        /// <returns></returns>
        public static void SetStudentBySSN(string SSN)
        {
            _Student = DALTransfer1.GetStudentRecBySSN(SSN);
            if (_Student != null)
                _HasStudentData = true;
            else
                _HasStudentData = false;
        }

        /// <summary>
        /// 設定學生資料
        /// </summary>
        /// <param name="Student"></param>
        public static void SetStudent(JHSchool.Data.JHStudentRecord  Student)
        {
            _Student = Student;
        
        }

        /// <summary>
        /// 取得學生資料
        /// </summary>
        /// <returns></returns>
        public static JHSchool.Data.JHStudentRecord  GetStudent()
        {
            return _Student;
        }

        /// <summary>
        /// 是否已有學生資料
        /// </summary>
        /// <returns></returns>
        public static bool GetHasStudentData()
        {
            if (_Student == null)
                _HasStudentData = false;
            else
                _HasStudentData = true;

            return _HasStudentData;
        }
    }
}
