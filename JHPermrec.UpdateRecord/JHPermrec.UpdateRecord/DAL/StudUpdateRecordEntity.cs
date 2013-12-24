using System;
using System.Collections.Generic;
using System.Text;

namespace JHPermrec.UpdateRecord.DAL
{
    public class StudUpdateRecordEntity
    {
        /// <summary>
        /// 學年度
        /// </summary>
        public int SchoolYear { get; set; }
        /// <summary>
        /// 學期
        /// </summary>
        public int Semester { get; set; }

        /// <summary>
        /// StudentID
        /// </summary>
        public string StudentID { get; set; }

        /// <summary>
        /// UID
        /// </summary>
        public string UID { get; set; }

        private string _UpdateDate = string.Empty;
        private string _EnrollmentSchoolYear = string.Empty;
        private string _Comment = string.Empty;
        private string _GraduateSchool = string.Empty;
        private string _ClassName = string.Empty;
        private string _Name = string.Empty;
        private string _IDNumber = string.Empty;
        private string _StudentNumber = string.Empty;
        private string _Gender = string.Empty;
        private string _Birthday = string.Empty;
        private string _Address = string.Empty;
        private string _ADDate = string.Empty;
        private string _ADNumber = string.Empty;
        private string _GradeYear = string.Empty;
        private string _GraduateSchoolYear = string.Empty;
        private string _Graduate = string.Empty;
        private string _BirthPlace = string.Empty;
        private string _LastADDate = string.Empty;
        private string _LastADNumber = string.Empty;
        private string _GraduateCertificateNumber = string.Empty;
        private string _UpdateDescription = string.Empty;
        private string _ImportExportSchool = string.Empty;
        private string _SeatNo = string.Empty;
        private string _UpdateCode = string.Empty;
        private string _NewIDNumber = string.Empty;
        private string _NewName = string.Empty;
        private string _NewBirthday = string.Empty;
        private string _NewGender = string.Empty;


        /// <summary>
        /// 存異動日期
        /// </summary>
        /// <param name="value"></param>
        public void SetUpdateDate(string value) { _UpdateDate = value; }

        /// <summary>
        /// 存入學年月
        /// </summary>
        /// <param name="value"></param>
        public void SetEnrollmentSchoolYear(string value) { _EnrollmentSchoolYear = value; }

        /// <summary>
        /// 存備註
        /// </summary>
        /// <param name="value"></param>
        public void SetComment(string value) { _Comment = value; }

        /// <summary>
        /// 存畢業國小
        /// </summary>
        /// <param name="value"></param>
        public void SetGraduateSchool(string value) 
        {
            if (string.IsNullOrEmpty(value))
                _GraduateSchool = "";
            else
                _GraduateSchool = value;        
        }

        /// <summary>
        /// 存班級
        /// </summary>
        /// <param name="value"></param>
        public void SetClassName(string value) { _ClassName = value; }

        /// <summary>
        /// 存姓名
        /// </summary>
        /// <param name="value"></param>
        public void SetName(string value) { _Name = value; }

        /// <summary>
        /// 存身分證號
        /// </summary>
        /// <param name="value"></param>
        public void SetIDNumber(string value) { _IDNumber = value; }

        /// <summary>
        /// 存學號
        /// </summary>
        /// <param name="value"></param>
        public void SetStudentNumber(string value) { _StudentNumber = value; }

        /// <summary>
        /// 存性別
        /// </summary>
        /// <param name="value"></param>
        public void SetGender(string value) { _Gender = value; }

        /// <summary>
        /// 存生日
        /// </summary>
        /// <param name="value"></param>
        public void SetBirthday(string value) { _Birthday = value; }

        /// <summary>
        /// 存地址
        /// </summary>
        /// <param name="value"></param>
        public void SetAddress(string value) { _Address = value; }

        /// <summary>
        /// 存核准日期
        /// </summary>
        /// <param name="value"></param>
        public void SetADDate(string value) { _ADDate = value; }

        /// <summary>
        /// 存核准文號
        /// </summary>
        /// <param name="value"></param>
        public void SetADNumber(string value) { _ADNumber = value; }

        /// <summary>
        /// 存異動年級
        /// </summary>
        /// <param name="value"></param>
        public void SetGradeYear(string value) { _GradeYear = value; }

        /// <summary>
        /// 存畢業年月
        /// </summary>
        /// <param name="value"></param>
        public void SetGraduateSchoolYear(string value) { _GraduateSchoolYear = value; }

        /// <summary>
        /// 存畢修業別
        /// </summary>
        /// <param name="value"></param>
        public void SetGraduate(string value) { _Graduate = value; }

        /// <summary>
        /// 存出生地
        /// </summary>
        /// <param name="value"></param>
        public void SetBirthPlace(string value) { _BirthPlace = value; }

        /// <summary>
        /// 存學籍異動日期
        /// </summary>
        /// <param name="value"></param>
        public void SetLastADDate(string value) { _LastADDate = value; }

        /// <summary>
        /// 存學籍異動文號
        /// </summary>
        /// <param name="value"></param>
        public void SetLastADNumber(string value) { _LastADNumber = value; }

        /// <summary>
        /// 存畢業證書字號
        /// </summary>
        /// <param name="value"></param>
        public void SetGraduateCertificateNumber(string value) { _GraduateCertificateNumber = value; }

        /// <summary>
        /// 存異動說明
        /// </summary>
        /// <param name="value"></param>
        public void SetUpdateDescription(string value) { _UpdateDescription = value; }

        /// <summary>
        /// 存轉出入學校
        /// </summary>
        /// <param name="value"></param>
        public void SetImportExportSchool(string value) { _ImportExportSchool = value; }

        /// <summary>
        /// 存座號
        /// </summary>
        /// <param name="value"></param>
        public void SetSeatNo(string value) { _SeatNo = value; }

        /// <summary>
        /// 存異動代碼
        /// </summary>
        /// <param name="value"></param>
        public void SetUpdateCode(string value) { _UpdateCode = value; }

        /// <summary>
        /// 存新身分證號
        /// </summary>
        /// <param name="value"></param>
        public void SetNewIDNumber(string value) { _NewIDNumber = value; }

        /// <summary>
        /// 存新姓名
        /// </summary>
        /// <param name="value"></param>
        public void SetNewName(string value) { _NewName = value; }

        /// <summary>
        /// 存新生日
        /// </summary>
        /// <param name="value"></param>
        public void SetNewBirthday(string value) { _NewBirthday = value; }

        /// <summary>
        /// 存新性別
        /// </summary>
        /// <param name="value"></param>
        public void SetNewGender(string value) { _NewGender = value; }



        /// <summary>
        /// 取異動日期
        /// </summary>
        /// <returns></returns>
        public DateTime? GetUpdateDate()
        {
            DateTime dt;
            if (DateTime.TryParse(_UpdateDate, out dt))
                return dt;
            else
                return DateTime.Now;
        }

        /// <summary>
        /// 取入學年月
        /// </summary>
        /// <returns></returns>
        public string GetEnrollmentSchoolYear() { return _EnrollmentSchoolYear; }

        /// <summary>
        /// 取備註
        /// </summary>
        /// <returns></returns>
        public string GetComment() { return _Comment; }

        /// <summary>
        /// 取畢業國小
        /// </summary>
        /// <returns></returns>
        public string GetGraduateSchool() { return _GraduateSchool; }

        /// <summary>
        /// 取班級名稱
        /// </summary>
        /// <returns></returns>
        public string GetClassName() { return _ClassName; }

        /// <summary>
        /// 取姓名
        /// </summary>
        /// <returns></returns>
        public string GetName() { return _Name; }

        /// <summary>
        /// 取身分證號
        /// </summary>
        /// <returns></returns>
        public string GetIDNumber() { return _IDNumber; }

        /// <summary>
        /// 取學號
        /// </summary>
        /// <returns></returns>
        public string GetStudentNumber() { return _StudentNumber; }

        /// <summary>
        /// 取性別
        /// </summary>
        /// <returns></returns>
        public string GetGender() { return _Gender; }

        /// <summary>
        /// 取生日
        /// </summary>
        /// <returns></returns>
        public DateTime? GetBirthday()
        {
            DateTime dt;
            if (DateTime.TryParse(_Birthday, out dt))
                return dt;
            else
                return null;
        }

        /// <summary>
        /// 取地址
        /// </summary>
        /// <returns></returns>
        public string GetAddress() { return _Address; }

        /// <summary>
        /// 取核准日期
        /// </summary>
        /// <returns></returns>
        public DateTime? GetADDate()
        {
            DateTime dt;
            if (DateTime.TryParse(_ADDate, out dt))
                return dt;
            else
                return null;
        }

        /// <summary>
        /// 取核准字號
        /// </summary>
        /// <returns></returns>
        public string GetADNumber() { return _ADNumber; }

        /// <summary>
        /// 取異動年級
        /// </summary>
        /// <returns></returns>
        public string GetGradeYear() { return _GradeYear; }

        /// <summary>
        /// 取畢業年月
        /// </summary>
        /// <returns></returns>
        public string GetGraduateSchoolYear() { return _GraduateSchoolYear; }

        /// <summary>
        /// 取畢休業別
        /// </summary>
        /// <returns></returns>
        public string GetGraduate() { return _Graduate; }

        /// <summary>
        /// 取出生地
        /// </summary>
        /// <returns></returns>
        public string GetBirthPlace() { return _BirthPlace; }

        /// <summary>
        /// 取學籍核准日期
        /// </summary>
        /// <returns></returns>
        public DateTime? GetLastADDate() 
        {
            DateTime dt;

            if (DateTime.TryParse(_LastADDate, out dt))
                return dt;
            else
                return null;
                
        }


        /// <summary>
        /// 取學籍核准文號
        /// </summary>
        /// <returns></returns>
        public string GetLastADNumber() { return _LastADNumber; }

        /// <summary>
        /// 取畢業證書字號
        /// </summary>
        /// <returns></returns>
        public string GetGraduateCertificateNumber() { return _GraduateCertificateNumber; }

        /// <summary>
        /// 取異動原因
        /// </summary>
        /// <returns></returns>
        public string GetUpdateDescription() { return _UpdateDescription; }

        /// <summary>
        /// 取轉出入學校
        /// </summary>
        /// <returns></returns>
        public string GetImportExportSchool() { return _ImportExportSchool; }

        /// <summary>
        /// 取座號
        /// </summary>
        /// <returns></returns>
        public string GetSeatNo() { return _SeatNo; }

        /// <summary>
        /// 取異動代碼
        /// </summary>
        /// <returns></returns>
        public string GetUpdateCode() { return _UpdateCode; }

        /// <summary>
        /// 取新身分證號
        /// </summary>
        /// <returns></returns>
        public string GetNewIDNumber() { return _NewIDNumber; }

        /// <summary>
        /// 取新姓名
        /// </summary>
        /// <returns></returns>
        public string GetNewName() { return _NewName; }

        /// <summary>
        /// 取新生日
        /// </summary>
        /// <returns></returns>
        public DateTime? GetNewBirthday() 
        { 
            DateTime dt;
            if (DateTime.TryParse(_NewBirthday, out dt))
                return dt;
            else
                return null;
        }

        /// <summary>
        /// 取新性別
        /// </summary>
        /// <returns></returns>
        public string GetNewGender() { return _NewGender; }

        /// <summary>
        /// 檢查是否相同異動代碼
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool checkSameUpdateCode(string code)
        {
            bool returnValue = false;

            if (_UpdateCode.Equals(code))
                returnValue = true;

            return returnValue;
        }

    }
}
