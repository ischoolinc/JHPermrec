using System;
using System.Collections.Generic;
using System.Xml;
using Framework;
using JHSchool.Data;
using FISCA.Data;
using System.Data;
using System.Linq;

namespace JHPermrec.UpdateRecord.DAL
{
    public class DALTransfer2
    {        
        /// <summary>
        /// 異動狀態
        /// </summary>
        public enum UpdateType { 新生, 畢業, 轉入, 轉出, 休學, 復學, 中輟, 續讀, 更正學籍, 延長修業年限,死亡, Empty };
        /// <summary>
        /// 可輸入異動類別
        /// </summary>
        public static List<UpdateType> CheckCanInputUpdateType;

        /// <summary>
        /// 異動名冊修改模式
        /// </summary>
        public enum UpdateBatchEditDALMode {新增,更新}

        /// <summary>
        /// 學生批次用入學年月
        /// </summary>
        //public static string _StudentBatchEnrollYearMonth = string.Empty;

        ///// <summary>
        ///// 預設學年度
        ///// </summary>
        //public static int _DefaultSchoolYear;

        ///// <summary>
        ///// 預設學期
        ///// </summary>
        //public static int _DefaultSemester;

        /// <summary>
        /// 成績模組
        /// </summary>
        public enum ScoreType
        {
            HsinChu,
            KaoHsiung
        }


        ///// <summary>
        ///// 學生批次用畢業年月
        ///// </summary>
        //public static string _StudentBatchGradeYearMonth = string.Empty;
        
        /// <summary>
        /// 每次新增1位學生一筆異動
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public static StudUpdateRecordEntity AddStudUpdateRecordEntity(string StudentID,UpdateType UT,string UpdateDate)
        {
            if(string.IsNullOrEmpty (StudentID ))
                return null ;
            
            // 取得學生記錄
            JHSchool.Data.JHStudentRecord studRec = JHSchool.Data.JHStudent.SelectByID(StudentID );
            JHSchool.Data.JHAddressRecord AddressRec = JHSchool.Data.JHAddress.SelectByStudentID(StudentID );

            // 異動紀錄 
            StudUpdateRecordEntity sure = new StudUpdateRecordEntity();
            // 學生編號
            sure.StudentID = StudentID;
            // 異動代碼
            sure.SetUpdateCode(GetUpdateCode(UT));
            // 學生生日
            if (studRec.Birthday.HasValue)
                sure.SetBirthday(studRec.Birthday.Value.ToShortDateString());
            // 學生出生地
            sure.SetBirthPlace(studRec.BirthPlace);

            if (studRec.Class != null)
            {
                // 班級名稱
                sure.SetClassName(studRec.Class.Name);
                // 班級年級
                if(studRec.Class.GradeYear.HasValue )
                    sure.SetGradeYear(studRec.Class.GradeYear.Value +"");
            }

            // 異動日期
            sure.SetUpdateDate(UpdateDate);
            // 學年度
            int schoolyear;
            if (int.TryParse(JHSchool.School.DefaultSchoolYear, out schoolyear))
                sure.SchoolYear = schoolyear;
            // 學期
            int semester;
            if (int.TryParse(JHSchool.School.DefaultSemester, out semester))
                sure.Semester = semester;
            // 性別
            sure.SetGender(studRec.Gender);
            // 身分證號
            sure.SetIDNumber(studRec.IDNumber);
            // 學生姓名
            sure.SetName(studRec.Name);
            // 學生座號
            if(studRec.SeatNo.HasValue )
                sure.SetSeatNo(studRec.SeatNo.Value+"");
            // 學號
            sure.SetStudentNumber(studRec.StudentNumber);
            // 戶籍地址
            string strAddress = AddressRec.Permanent.County + AddressRec.Permanent.Town + AddressRec.Permanent.District + AddressRec.Permanent.Area + AddressRec.Permanent.Detail;
            sure.SetAddress(strAddress);
            // 入學前國小
            JHSchool.Data.JHBeforeEnrollmentRecord ber = JHSchool.Data.JHBeforeEnrollment.SelectByStudentID(StudentID);
            sure.SetGraduateSchool(ber.School);

            // 畢修業別
            JHSchool.Data.JHLeaveInfoRecord LeavInfoRec = GetStudentLeavInfoRecord(StudentID);
            sure.SetGraduate(LeavInfoRec.Reason);            

            // 畢業證書字號
            sure.SetGraduateCertificateNumber(LeavInfoRec.DiplomaNumber);

            // 最後核准文號
            if (GetStudLastADDateUpdateRecord(StudentID) != null)
            {
                if (!string.IsNullOrEmpty(GetStudLastADDateUpdateRecord(StudentID).ADNumber))
                    if (string.IsNullOrEmpty(sure.GetLastADNumber()))                    
                        sure.SetLastADNumber(GetStudLastADDateUpdateRecord(StudentID).ADNumber);

                if (!string.IsNullOrEmpty(GetStudLastADDateUpdateRecord(StudentID).ADDate))
                    if (sure.GetLastADDate().HasValue == false)
                        sure.SetLastADDate(GetStudLastADDateUpdateRecord(StudentID).ADDate);
            }
            // 新生異動(入學年月)
            if (string.IsNullOrEmpty(sure.GetEnrollmentSchoolYear()))
            sure.SetEnrollmentSchoolYear(GetUpdateCode01EnrollYearMonth(StudentID));


            // 出生地
            sure.SetBirthPlace(studRec.BirthPlace);

            return sure;
        }


        /// <summary>
        /// 新增多位學生同型態異動
        /// </summary>
        /// <param name="StudIDList"></param>
        /// <param name="UT"></param>
        /// <returns></returns>
        public static List<StudUpdateRecordEntity> AddStudUpdateRecordEntityList(List<string> StudIDList, UpdateType UT,string UpdateDate)
        {
            List<StudUpdateRecordEntity > StudUpdateRecordEntityList = new List<StudUpdateRecordEntity> ();

            // 取得學生記錄
            List<JHSchool.Data.JHStudentRecord> studRecList = JHSchool.Data.JHStudent.SelectByIDs(StudIDList);
            // 取得地址
            Dictionary<string, JHSchool.Data.JHAddressRecord> StudAddressDic = GetStudentAddressRecDic(StudIDList);
            // 取得入學學校
            Dictionary<string, string> BeforeEnrollSchoolDic = GetStudentBeforeEnrollmentSchoolDic(StudIDList);
            // 取得畢業資訊
            Dictionary<string, JHSchool.Data.JHLeaveInfoRecord> LeavInfoDic = GetStudentLeaveInfoRecordDic(StudIDList);
            // 取得異動代碼為1
            Dictionary<string, string> UpdateCode01EnrollYearMonthDic = GetUpdateCode01EnrollYearMonthDic(StudIDList);
            // 取得學籍核准資料
            Dictionary<string, JHSchool.Data.JHUpdateRecordRecord> StudLastADDateUpdateRecordDic = GetStudLastADDateUpdateRecDic(StudIDList);

            foreach (JHSchool.Data.JHStudentRecord studRec in studRecList)
            {
                StudUpdateRecordEntity sure = new StudUpdateRecordEntity();
                // 學生編號
                sure.StudentID = studRec.ID;
                // 異動代碼
                sure.SetUpdateCode(GetUpdateCode(UT));
                // 生日
                if (studRec.Birthday.HasValue)
                    sure.SetBirthday(studRec.Birthday.Value.ToShortDateString());
                // 出生地
                sure.SetBirthPlace(studRec.BirthPlace);

                // 學年度
                int SchoolYear, Semester;
                if (int.TryParse(JHSchool.School.DefaultSchoolYear, out SchoolYear))
                    sure.SchoolYear = SchoolYear;
                // 學期
                if (int.TryParse(JHSchool.School.DefaultSemester, out Semester))
                    sure.Semester = Semester;

                if (studRec.Class != null)
                {
                    // 班級
                    sure.SetClassName(studRec.Class.Name);
                    // 班級年級
                    if (studRec.Class.GradeYear.HasValue)
                        sure.SetGradeYear(studRec.Class.GradeYear.Value + "");                    
                }
                // 性別
                sure.SetGender(studRec.Gender);
                // 身分證號
                sure.SetIDNumber(studRec.IDNumber);
                // 姓名
                sure.SetName(studRec.Name);
                // 座號
                if (studRec.SeatNo.HasValue)
                    sure.SetSeatNo(studRec.SeatNo.Value + "");
                
                // 學號
                sure.SetStudentNumber(studRec.StudentNumber);
                // 戶籍地址
                string strAddress = string.Empty;
                if(StudAddressDic.ContainsKey(studRec.ID ))
                    strAddress = StudAddressDic[studRec.ID].Permanent.County + StudAddressDic[studRec.ID].Permanent.Town  + StudAddressDic[studRec.ID].Permanent.District + StudAddressDic[studRec.ID].Permanent.Area + StudAddressDic[studRec.ID].Permanent.Detail;
                sure.SetAddress(strAddress);
                // 取得入學前國小
                if (BeforeEnrollSchoolDic.ContainsKey(studRec.ID))
                    sure.SetGraduateSchool(BeforeEnrollSchoolDic[studRec.ID]);

                sure.SetUpdateDate(UpdateDate);
                if (LeavInfoDic.ContainsKey(studRec.ID))
                {
                    // 畢修業別
                    sure.SetGraduate(LeavInfoDic[studRec.ID].Reason);

                    // 畢業證書字號
                    sure.SetGraduateCertificateNumber(LeavInfoDic[studRec.ID].DiplomaNumber);
                    
                }

                // 最後核准文號                
                if (StudLastADDateUpdateRecordDic.ContainsKey(studRec.ID))
                {
                    if (StudLastADDateUpdateRecordDic[studRec.ID] != null)
                    {
                        if (!string.IsNullOrEmpty(StudLastADDateUpdateRecordDic[studRec.ID].ADDate))
                            if(sure.GetLastADDate().HasValue == false )
                                sure.SetLastADDate(StudLastADDateUpdateRecordDic[studRec.ID].ADDate);

                        if (!string.IsNullOrEmpty(StudLastADDateUpdateRecordDic[studRec.ID].ADNumber))
                            if(string.IsNullOrEmpty(sure.GetLastADNumber ()))
                                sure.SetLastADNumber(StudLastADDateUpdateRecordDic[studRec.ID].ADNumber);
                    }
                }
                // 新生異動(入學年月)
                if(string.IsNullOrEmpty(sure.GetEnrollmentSchoolYear()))
                if (UpdateCode01EnrollYearMonthDic.ContainsKey(studRec.ID))
                    sure.SetEnrollmentSchoolYear(UpdateCode01EnrollYearMonthDic[studRec.ID]);

                // 出生地
                sure.SetBirthPlace(studRec.BirthPlace);


                StudUpdateRecordEntityList.Add(sure);
            }
            return StudUpdateRecordEntityList;
        }

        /// <summary>
        /// 取得學生異動資料(單位)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static List<StudUpdateRecordEntity> GetStudUpdateRecordEntityList(string studID)
        {
            List<StudUpdateRecordEntity> StudUpdateRecordEntityList = new List<StudUpdateRecordEntity>();

            List<JHSchool.Data.JHUpdateRecordRecord> updateRecList = JHSchool.Data.JHUpdateRecord.SelectByStudentID(studID);

            foreach (JHSchool.Data.JHUpdateRecordRecord urr in updateRecList)
                StudUpdateRecordEntityList.Add(SetStudUpdateRecordEntityTrans(urr));

            return StudUpdateRecordEntityList;
        }

        /// <summary>
        /// 將 JHUpdateRecordRecord 轉成 StudUpdateRecordEntity
        /// </summary>
        /// <param name="urr"></param>
        /// <returns></returns>
        public static StudUpdateRecordEntity SetStudUpdateRecordEntityTrans(JHSchool.Data.JHUpdateRecordRecord urr)
        {
            StudUpdateRecordEntity sure = new StudUpdateRecordEntity();
            if (urr.SchoolYear.HasValue)
                sure.SchoolYear = urr.SchoolYear.Value;
            if (urr.Semester.HasValue)
                sure.Semester = urr.Semester.Value;
            sure.StudentID = urr.StudentID;
            sure.UID = urr.ID;

            sure.SetADDate(urr.ADDate);
            sure.SetAddress(urr.OriginAddress);
            sure.SetADNumber(urr.ADNumber);
            sure.SetBirthday(urr.Birthdate);
            sure.SetBirthPlace(urr.BirthPlace);
            sure.SetClassName(urr.OriginClassName);
            sure.SetComment(urr.Comment);
            sure.SetEnrollmentSchoolYear(urr.EnrollmentSchoolYear);
            sure.SetGender(urr.Gender);
            sure.SetGradeYear(urr.GradeYear);
            sure.SetGraduate(urr.Graduate);
            sure.SetGraduateCertificateNumber(urr.GraduateCertificateNumber);
            sure.SetGraduateSchool(urr.GraduateSchool);
            sure.SetGraduateSchoolYear(urr.GraduateSchoolYear);
            sure.SetIDNumber(urr.IDNumber);
            sure.SetImportExportSchool(urr.ImportExportSchool);
            sure.SetLastADDate(urr.LastADDate);
            sure.SetLastADNumber(urr.LastADNumber);
            sure.SetName(urr.StudentName);
            sure.SetNewBirthday(urr.NewBirthday);
            sure.SetNewGender(urr.NewGender);
            sure.SetNewIDNumber(urr.NewIDNumber);
            sure.SetNewName(urr.NewName);
            sure.SetSeatNo(urr.SeatNo);
            sure.SetStudentNumber(urr.StudentNumber);
            sure.SetUpdateCode(urr.UpdateCode);
            sure.SetUpdateDate(urr.UpdateDate);
            sure.SetUpdateDescription(urr.UpdateDescription);

            return sure;
        }

        // Get StudUpdateRecordEntityList
        private static JHSchool.Data.JHUpdateRecordRecord GetStudUpdateRecordEntityTrans(StudUpdateRecordEntity sure)
        {
            JHSchool.Data.JHUpdateRecordRecord urr = new JHSchool.Data.JHUpdateRecordRecord();

            // 學生編號
            urr.StudentID = sure.StudentID;
            // 學年度
            if (sure.SchoolYear > 0)
                urr.SchoolYear = sure.SchoolYear;
            else
                urr.SchoolYear = null;
            // 學期
            if (sure.Semester > 0)
                urr.Semester = sure.Semester;
            else
                urr.Semester = null;

            // 核准日期
            if (sure.GetADDate().HasValue)
                urr.ADDate = sure.GetADDate().Value.ToShortDateString();
            else
                urr.ADDate = string.Empty;

            // 核准文號
            urr.ADNumber = sure.GetADNumber();

            // 生日
            if (sure.GetBirthday().HasValue)
                urr.Birthdate = sure.GetBirthday().Value.ToShortDateString();
            else
                urr.Birthdate = string.Empty;
            
            // 出生地
            urr.BirthPlace = sure.GetBirthPlace();
            // 備註
            urr.Comment = sure.GetComment();
            // 入學年月
            urr.EnrollmentSchoolYear = sure.GetEnrollmentSchoolYear();
            // 性別
            urr.Gender = sure.GetGender();
            // 年級
            urr.GradeYear = sure.GetGradeYear();
            // 畢修業
            urr.Graduate = sure.GetGraduate();
            // 畢業證書字號
            urr.GraduateCertificateNumber = sure.GetGraduateCertificateNumber();
            // 畢業國小
            urr.GraduateSchool = sure.GetGraduateSchool();
            // 畢業年月
            urr.GraduateSchoolYear = sure.GetGraduateSchoolYear();
            // 身分證號
            urr.IDNumber = sure.GetIDNumber();
            // 轉出入學校
            urr.ImportExportSchool = sure.GetImportExportSchool();
            // 學籍核准日期
            if (sure.GetLastADDate().HasValue)
                urr.LastADDate = sure.GetLastADDate().Value.ToShortDateString();
            else
                urr.LastADDate = string.Empty;
            // 學籍核准文號
            urr.LastADNumber = sure.GetLastADNumber();
            // 姓名
            urr.StudentName = sure.GetName();
            // 學號
            urr.StudentNumber = sure.GetStudentNumber();
            // 座號
            urr.SeatNo = sure.GetSeatNo();
            // 新生日
            if (sure.GetNewBirthday().HasValue)
                urr.NewBirthday = sure.GetNewBirthday().Value.ToShortDateString();
            else
                urr.NewBirthday = string.Empty;

            // 新性別
            urr.NewGender = sure.GetNewGender();
            // 新身分證號
            urr.NewIDNumber = sure.GetNewIDNumber();
            // 新姓名
            urr.NewName = sure.GetNewName();
            // 地址
            urr.OriginAddress = sure.GetAddress();
            // 班級
            urr.OriginClassName = sure.GetClassName();
            // 異動日期
            if (sure.GetUpdateDate().HasValue)
                urr.UpdateDate = sure.GetUpdateDate().Value.ToShortDateString();
            else
                urr.UpdateDate = string.Empty;
            // 異動代碼
            urr.UpdateCode = sure.GetUpdateCode();
            // 異動原因
            urr.UpdateDescription = sure.GetUpdateDescription();
            // 編號
            urr.ID = sure.UID;

            return urr;
        }

        /// <summary>
        /// 取得學生特定異動異動資料(單位)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static List<StudUpdateRecordEntity> GetStudUpdateRecordEntityListByUpdateType(string studID, UpdateType UT)
        {
            string strUpdateCode = GetUpdateCode(UT);

            List<StudUpdateRecordEntity> StudUpdateRecordEntityList = new List<StudUpdateRecordEntity>();
            List<JHSchool.Data.JHUpdateRecordRecord> updateRecList = JHSchool.Data.JHUpdateRecord.SelectByStudentID(studID);

            foreach (JHSchool.Data.JHUpdateRecordRecord urr in updateRecList)
                if (strUpdateCode == urr.UpdateCode)
                    StudUpdateRecordEntityList.Add(SetStudUpdateRecordEntityTrans(urr));
            return StudUpdateRecordEntityList;
        }


        /// <summary>
        /// 取得學生異動資料(多位)
        /// </summary>
        /// <param name="studIDList"></param>
        /// <returns></returns>
        public static List<StudUpdateRecordEntity> GetStudUpdateRecordEntityList(List<string> studIDList)
        {
            List<StudUpdateRecordEntity> StudUpdateRecordEntityList = new List<StudUpdateRecordEntity>();
            List<JHSchool.Data.JHUpdateRecordRecord> updateRecList = JHSchool.Data.JHUpdateRecord.SelectByStudentIDs(studIDList);

            foreach (JHSchool.Data.JHUpdateRecordRecord urr in updateRecList)
                StudUpdateRecordEntityList.Add(SetStudUpdateRecordEntityTrans(urr));
            return StudUpdateRecordEntityList;
        }

        /// <summary>
        /// 儲存單筆異動
        /// </summary>
        /// <param name="StudUpdateRecord"></param>
        public static void SetStudUpdateRecordEntity(StudUpdateRecordEntity StudUpdateRecord)
        {
            if (string.IsNullOrEmpty(StudUpdateRecord.StudentID) || StudUpdateRecord.GetUpdateDate().HasValue==false || string.IsNullOrEmpty(StudUpdateRecord.GetUpdateCode()))
                return;

            JHSchool.Data.JHUpdateRecordRecord urr = GetStudUpdateRecordEntityTrans(StudUpdateRecord);

            if (string.IsNullOrEmpty(urr.ID))
                JHSchool.Data.JHUpdateRecord.Insert(urr);
            else
                JHSchool.Data.JHUpdateRecord.Update(urr);


        }

        /// <summary>
        /// 儲存多筆異動
        /// </summary>
        /// <param name="StudUpdateRecordList"></param>
        public static void SetStudUpdateRecordEntityList(List<StudUpdateRecordEntity> StudUpdateRecordList)
        {
            List<JHSchool.Data.JHUpdateRecordRecord> InsertUrrList = new List<JHSchool.Data.JHUpdateRecordRecord>();
            List<JHSchool.Data.JHUpdateRecordRecord> UpdateUrrList = new List<JHSchool.Data.JHUpdateRecordRecord>();

            foreach (StudUpdateRecordEntity sure in StudUpdateRecordList)
            {
                if (string.IsNullOrEmpty(sure.UID))
                    InsertUrrList.Add(GetStudUpdateRecordEntityTrans(sure));
                else
                    UpdateUrrList.Add(GetStudUpdateRecordEntityTrans(sure));
            }

            if (InsertUrrList.Count > 0)
                JHSchool.Data.JHUpdateRecord.Insert(InsertUrrList);

            if (UpdateUrrList.Count > 0)
                JHSchool.Data.JHUpdateRecord.Update(UpdateUrrList);
        }


        /// <summary>
        /// 刪除多筆記動紀錄
        /// </summary>
        /// <param name="StudUpdateRecordList"></param>
        public static void DelStudUpdateRecordEntityList(List<StudUpdateRecordEntity> StudUpdateRecordList)
        {
            List<JHSchool.Data.JHUpdateRecordRecord> DelUrrList = new List<JHSchool.Data.JHUpdateRecordRecord>();
            foreach (StudUpdateRecordEntity sure in StudUpdateRecordList)
                if(!string.IsNullOrEmpty (sure.UID ))
                    DelUrrList.Add(GetStudUpdateRecordEntityTrans(sure));

            if (DelUrrList.Count > 0)
                JHSchool.Data.JHUpdateRecord.Delete(DelUrrList);
        }
        
        /// <summary>
        /// 檢查學生同一天異動紀錄內是否有相同異動代碼
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public static bool checkStudentSameUpdateCode(string StudentID,StudUpdateRecordEntity sure)
        {
            bool returnValue = false;
            List<JHSchool.Data.JHUpdateRecordRecord> studUpdateRecList = JHSchool.Data.JHUpdateRecord.SelectByStudentID(StudentID);

            foreach (JHSchool.Data.JHUpdateRecordRecord rec in studUpdateRecList)
            {
                DateTime dt;
                if (DateTime.TryParse(rec.UpdateDate, out dt) && sure.GetUpdateDate().HasValue)
                {                    
                    if (sure.GetUpdateDate().Value == dt)
                        if (sure.checkSameUpdateCode(rec.UpdateCode))
                            returnValue = true;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 檢查異動代碼是否重複
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="sure"></param>
        /// <param name="UpdateCode"></param>
        /// <returns></returns>
        public static bool checkStudentSameUpdateCode(string StudentID, StudUpdateRecordEntity sure,string UpdateCode)
        {
            bool returnValue = false;

            List<JHSchool.Data.JHUpdateRecordRecord> studUpdateRecList = JHSchool.Data.JHUpdateRecord.SelectByStudentID(StudentID);

            foreach (JHSchool.Data.JHUpdateRecordRecord rec in studUpdateRecList)
            {
                if (rec.UpdateCode == UpdateCode)
                {
                    returnValue = true;
                    break;
                }
            }
            return returnValue;        
        }


        /// <summary>
        /// 取得學生學籍異動異動資料(多位)(新生、畢業除外)
        /// </summary>
        /// <param name="StudIDList"></param>
        /// <param name="UT"></param>
        /// <returns></returns>
        public static Dictionary<string, List<StudUpdateRecordEntity>> GetStudListUpdateRecordEntityListByUpdate39(List<string> StudIDList)
        {
            Dictionary<string, List<StudUpdateRecordEntity>> StudUpdateRecordEntityDic = new Dictionary<string, List<StudUpdateRecordEntity>>();
            List<JHSchool.Data.JHUpdateRecordRecord> updateRecList = JHSchool.Data.JHUpdateRecord.SelectByStudentIDs(StudIDList);

            // 因為暫時不使用到先註解 20100727
            // 取得戶籍地址
            //Dictionary<string, JHSchool.Data.JHAddressRecord> StudAddresRecDic = GetStudentAddressRecDic(StudIDList);

            foreach (JHSchool.Data.JHUpdateRecordRecord urr in updateRecList)
            {
                // 因為暫時不使用到先註解 20100727
                //if (StudAddresRecDic.ContainsKey(urr.StudentID))
                //{
                //    // 當異動地址是空的時再加入
                //    if (string.IsNullOrEmpty(urr.OriginAddress))
                //        urr.OriginAddress = StudAddresRecDic[urr.StudentID].Permanent.County + StudAddresRecDic[urr.StudentID].Permanent.District + StudAddresRecDic[urr.StudentID].Permanent.Area + StudAddresRecDic[urr.StudentID].Permanent.Detail;
                //}

                // 新生1,畢業2
                if (urr.UpdateCode == "1" || urr.UpdateCode == "2")
                    continue;

                    if (StudUpdateRecordEntityDic.ContainsKey(urr.StudentID))
                        StudUpdateRecordEntityDic[urr.StudentID].Add(SetStudUpdateRecordEntityTrans(urr));
                    else
                    {
                        List<StudUpdateRecordEntity> studRecList = new List<StudUpdateRecordEntity>();
                        studRecList.Add(SetStudUpdateRecordEntityTrans(urr));
                        StudUpdateRecordEntityDic.Add(urr.StudentID, studRecList);
                    }
            }

            return StudUpdateRecordEntityDic;
        
        }


        /// <summary>
        /// 取得學生特定異動異動資料(多位)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Dictionary<string,List<StudUpdateRecordEntity>> GetStudListUpdateRecordEntityListByUpdateType(List<string> StudIDList, UpdateType UT)
        {

            string strUpdateCode = GetUpdateCode(UT);

            Dictionary<string, List<StudUpdateRecordEntity>> StudUpdateRecordEntityDic = new Dictionary<string, List<StudUpdateRecordEntity>>();
            List<JHSchool.Data.JHUpdateRecordRecord> updateRecList = JHSchool.Data.JHUpdateRecord.SelectByStudentIDs(StudIDList);
            
            //// 取得戶籍地址
            // Dictionary<string, JHSchool.Data.JHAddressRecord> StudAddresRecDic= GetStudentAddressRecDic(StudIDList);

            foreach (JHSchool.Data.JHUpdateRecordRecord urr in updateRecList)
            {
                // 這先註解是讓異動地址如果是空值就保持一致
                //if (StudAddresRecDic.ContainsKey(urr.StudentID))
                //{
                //    // 當異動地址是空的時再加入
                //    if (string.IsNullOrEmpty(urr.OriginAddress))
                //        urr.OriginAddress = StudAddresRecDic[urr.StudentID].Permanent.County + StudAddresRecDic[urr.StudentID].Permanent.District + StudAddresRecDic[urr.StudentID].Permanent.Area + StudAddresRecDic[urr.StudentID].Permanent.Detail;
                //}

                if (strUpdateCode == urr.UpdateCode)
                {
                    if (StudUpdateRecordEntityDic.ContainsKey(urr.StudentID))
                        StudUpdateRecordEntityDic[urr.StudentID].Add(SetStudUpdateRecordEntityTrans(urr));
                    else
                    {
                        List<StudUpdateRecordEntity> studRecList = new List<StudUpdateRecordEntity>();
                        studRecList.Add(SetStudUpdateRecordEntityTrans(urr));
                        StudUpdateRecordEntityDic.Add(urr.StudentID, studRecList);
                    }
                }
            }

            return StudUpdateRecordEntityDic;
        }


        /// <summary>
        /// 取得 UpdateType and UpdateCode 對應
        /// </summary>
        /// <param name="UT"></param>
        /// <returns></returns>
        public static string GetUpdateCode(UpdateType UT)
        {
            string strUpdateCode = "";
            switch (UT)
            {
                case UpdateType.新生:
                    strUpdateCode = "1";
                    break;
                case UpdateType.畢業:
                    strUpdateCode = "2";
                    break;
                case UpdateType.轉入:
                    strUpdateCode = "3";
                    break;
                case UpdateType.轉出:
                    strUpdateCode = "4";
                    break;
                case UpdateType.休學:
                    strUpdateCode = "5";
                    break;
                case UpdateType.復學:
                    strUpdateCode = "6";
                    break;
                case UpdateType.中輟:
                    strUpdateCode = "7";
                    break;
                case UpdateType.續讀:
                    strUpdateCode = "8";
                    break;
                case UpdateType.更正學籍:
                    strUpdateCode = "9";
                    break;
                case UpdateType.Empty :
                    strUpdateCode = string.Empty;
                    break;
            }
            return strUpdateCode;
        }


        /// <summary>
        /// 取得學生地址
        /// </summary>
        /// <param name="studIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, JHSchool.Data.JHAddressRecord> GetStudentAddressRecDic(List<string> studIDList)
        {
            List<JHSchool.Data.JHAddressRecord> addressRecs = JHSchool.Data.JHAddress.SelectByStudentIDs(studIDList);
            Dictionary<string, JHSchool.Data.JHAddressRecord> StudAddressRecDic = new Dictionary<string, JHSchool.Data.JHAddressRecord>();

            foreach (JHSchool.Data.JHAddressRecord AddRec in addressRecs)
                if (!StudAddressRecDic.ContainsKey(AddRec.RefStudentID))
                    StudAddressRecDic.Add(AddRec.RefStudentID, AddRec);            
            return StudAddressRecDic;
        }

        /// <summary>
        /// 取得目前年級
        /// </summary>
        /// <returns></returns>
        public static List<int> GetClassGardeYearList()
        {
            List<int> intGradeYear = new List<int>();
            List<JHSchool.Data.JHClassRecord> classRecList = JHSchool.Data.JHClass.SelectAll();            
            foreach(JHSchool.Data.JHClassRecord cr in classRecList )
            {
                if(cr.GradeYear.HasValue )
                    if(!intGradeYear.Contains(cr.GradeYear.Value ))
                        intGradeYear.Add(cr.GradeYear.Value );
            }            
            intGradeYear.Sort();
            return intGradeYear;
        }

        /// <summary>
        /// 依年級取的該年級學生ID
        /// </summary>
        /// <param name="strGardeYear"></param>
        /// <returns></returns>
        public static List<string> GetStudentIDListByGradeYear(string strGardeYear)
        {
            int grYear;

            if (int.TryParse(strGardeYear, out grYear))
            {
                List<string> StudIDList = new List<string>();
                List<JHSchool.Data.JHStudentRecord> allStudRecs = JHSchool.Data.JHStudent.SelectAll();
                foreach (JHSchool.Data.JHStudentRecord studRec in allStudRecs)
                {
                    // 過濾刪除學生
                    if (studRec.Status == K12.Data.StudentRecord.StudentStatus.刪除)
                        continue;

                    if (studRec.Class != null && studRec.Class.GradeYear.HasValue)
                        if (studRec.Class.GradeYear.Value == grYear && studRec.Status == K12.Data.StudentRecord.StudentStatus.一般 )
                            StudIDList.Add(studRec.ID);                           
                }
                return StudIDList;
            }
            else
                return null;
        }

        /// <summary>
        /// 取得特殊狀態 Student ID
        /// </summary>
        /// <param name="StudentIDLsit"></param>
        /// <param name="st"></param>
        /// <returns></returns>
        public static List<string> GetStudentTypeIDFromIDs(List<string> StudentIDLsit, K12.Data.StudentRecord.StudentStatus st)
        {
            List<string> IDs = new List<string>();
            foreach (JHStudentRecord studRec in JHStudent.SelectByIDs(StudentIDLsit))
                if (studRec.Status == st)
                    IDs.Add(studRec.ID);
            return IDs;
        }

        /// <summary>
        ///透過學生ID取得入學前學校名稱
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetStudentBeforeEnrollmentSchoolDic(List<string> StudentIDList)
        {
            Dictionary<string, string> BeforeEnrollmentSchoolNameDic = new Dictionary<string, string>();

            List<JHSchool.Data.JHBeforeEnrollmentRecord> JHBERecList = JHSchool.Data.JHBeforeEnrollment.SelectByStudentIDs(StudentIDList);

            foreach (JHSchool.Data.JHBeforeEnrollmentRecord ber in JHBERecList)
                if (!BeforeEnrollmentSchoolNameDic.ContainsKey(ber.RefStudentID))
                    BeforeEnrollmentSchoolNameDic.Add(ber.RefStudentID, ber.School);

            return BeforeEnrollmentSchoolNameDic;
        }

        
        /// <summary>
        /// 取得多位學生離校資訊
        /// </summary>
        /// <param name="StudIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, JHSchool.Data.JHLeaveInfoRecord> GetStudentLeaveInfoRecordDic(List<string> StudIDList)
        {
            Dictionary<string, JHSchool.Data.JHLeaveInfoRecord> LeavInfoRecDic = new Dictionary<string, JHSchool.Data.JHLeaveInfoRecord>();

            List<JHSchool.Data.JHLeaveInfoRecord> LeavInfoList = JHSchool.Data.JHLeaveIfno.SelectByStudentIDs(StudIDList);
            foreach (JHSchool.Data.JHLeaveInfoRecord leavInfoRec in LeavInfoList)
                if (!LeavInfoRecDic.ContainsKey(leavInfoRec.RefStudentID))
                    LeavInfoRecDic.Add(leavInfoRec.RefStudentID, leavInfoRec);
            
            return LeavInfoRecDic;
        }

        /// <summary>
        /// 取得單筆學生離校資訊
        /// </summary>
        /// <param name="StudID"></param>
        /// <returns></returns>
        public static JHSchool.Data.JHLeaveInfoRecord GetStudentLeavInfoRecord(string StudID)
        {
            if (string.IsNullOrEmpty(StudID))
                return null;
            else
                return JHSchool.Data.JHLeaveIfno.SelectByStudentID(StudID);
        }

        /// <summary>
        /// 取得單筆學生入學年月
        /// </summary>
        /// <param name="StudID"></param>
        /// <returns></returns>
        public static string GetUpdateCode01EnrollYearMonth(string StudID)
        {
            string returnValue=string.Empty ;

            if (string.IsNullOrEmpty(StudID))
                return string.Empty;

            List<JHSchool.Data.JHUpdateRecordRecord> updateRecList = JHSchool.Data.JHUpdateRecord.SelectByStudentID(StudID);

            foreach (JHSchool.Data.JHUpdateRecordRecord rec in updateRecList)
                if (rec.UpdateCode == "1")
                    returnValue = rec.EnrollmentSchoolYear;
            return returnValue;
        }

        /// <summary>
        /// 取得多筆學生入學年月
        /// </summary>
        /// <param name="StudIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetUpdateCode01EnrollYearMonthDic(List<string> StudIDList)
        {
            Dictionary<string,string> retValueDic = new Dictionary<string,string> ();
            List<JHSchool.Data.JHUpdateRecordRecord> updateRecList = JHSchool.Data.JHUpdateRecord.SelectByStudentIDs(StudIDList);

            foreach (JHSchool.Data.JHUpdateRecordRecord rec in updateRecList)
                if (rec.UpdateCode == "1")
                    if (!retValueDic.ContainsKey(rec.StudentID))
                        retValueDic.Add(rec.StudentID, rec.EnrollmentSchoolYear);
            return retValueDic;
        }


        /// <summary>
        /// 取得單筆最後核准
        /// </summary>
        /// <param name="StudID"></param>
        /// <returns></returns>
        public static JHSchool.Data.JHUpdateRecordRecord GetStudLastADDateUpdateRecord(string StudID)
        {
            if (string.IsNullOrEmpty(StudID))
                return null;

            List<JHSchool.Data.JHUpdateRecordRecord> updateRecList = new List<JHUpdateRecordRecord>();
            foreach (JHUpdateRecordRecord urr in JHSchool.Data.JHUpdateRecord.SelectByStudentID(StudID))
            {
                if (!string.IsNullOrEmpty(urr.ADDate))
                    updateRecList.Add(urr);
            }

            updateRecList.Sort(new Comparison<JHSchool.Data.JHUpdateRecordRecord>(UpdateRecordSorter1));
            if (updateRecList.Count > 0)
                return updateRecList[updateRecList.Count - 1];
            else
                return null;

        }

        /// <summary>
        /// 取得多筆學生最後核准
        /// </summary>
        /// <param name="StudIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, JHSchool.Data.JHUpdateRecordRecord> GetStudLastADDateUpdateRecDic(List<string> StudIDList)
        {
            Dictionary<string, JHSchool.Data.JHUpdateRecordRecord> studUpdateRec = new Dictionary<string, JHSchool.Data.JHUpdateRecordRecord>();
            foreach (string sID in StudIDList)
            { 
                studUpdateRec.Add (sID,GetStudLastADDateUpdateRecord(sID));
            }
            return studUpdateRec;
        
        }

        // 最後一筆異動文號排序用
        private static int UpdateRecordSorter1(JHSchool.Data.JHUpdateRecordRecord x,JHSchool.Data.JHUpdateRecordRecord y)
        {
            // TODO: 待確定排序是用哪種日期
            DateTime dtX;
            DateTime dtY;
            DateTime.TryParse(x.ADDate, out dtX);
            DateTime.TryParse(y.ADDate, out dtY);

            return dtX.CompareTo (dtY);
        }

        /// <summary>
        /// 取得學生畢修業相關資料(依年級)
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static List<StudDiplomaInfoJuniorDiplomaNumber> GetStudentDiplomaInfoJuniorDiplomaNumberListByGradeYear(string GradeYear)
        {
            int intGradeYear;
            int.TryParse (GradeYear,out intGradeYear);

            List<StudDiplomaInfoJuniorDiplomaNumber> DiplomaInfoJuniorDiplomaNumberList = new List<StudDiplomaInfoJuniorDiplomaNumber>();
            List<string> StudentIDList = new List<string>();
            List<JHSchool.Data.JHStudentRecord> StudRecList = JHSchool.Data.JHStudent.SelectAll();

            // 放入學生基本資料
            foreach (JHSchool.Data.JHStudentRecord studRec in StudRecList)
            {
                if (studRec.Class == null)
                    continue;

                if (studRec.Class.GradeYear.HasValue)
                {
                    if (intGradeYear == studRec.Class.GradeYear.Value)
                    {
                        StudDiplomaInfoJuniorDiplomaNumber sdijdn = new StudDiplomaInfoJuniorDiplomaNumber();
                        if (studRec.Class != null)
                        {
                            sdijdn.ClassName = studRec.Class.Name;
                            sdijdn.ClassDisplayOrder = studRec.Class.DisplayOrder;
                            if(studRec.Class.GradeYear.HasValue )
                                sdijdn.GradeYear = studRec.Class.GradeYear.Value;
                            sdijdn.ClassID = studRec.Class.ID;
                        }
                        sdijdn.Name = studRec.Name;
                        if (studRec.SeatNo.HasValue)
                            sdijdn.SeatNo = studRec.SeatNo.Value;
                        sdijdn.StudentID = studRec.ID;
                        sdijdn.StudentNumber = studRec.StudentNumber;
                        StudentIDList.Add(studRec.ID);
                        DiplomaInfoJuniorDiplomaNumberList.Add(sdijdn);
                    }
                }
            }

            List<JHSchool.Data.JHLeaveInfoRecord> LeaveInfoRecordList = JHSchool.Data.JHLeaveIfno.SelectByStudentIDs(StudentIDList);
            Dictionary<string, JHSchool.Data.JHLeaveInfoRecord> LIRDic = new Dictionary<string, JHSchool.Data.JHLeaveInfoRecord>();
            foreach (JHSchool.Data.JHLeaveInfoRecord rec in LeaveInfoRecordList)
                LIRDic.Add(rec.RefStudentID, rec);


            // 放入離校資訊
            foreach (StudDiplomaInfoJuniorDiplomaNumber sdijdn in DiplomaInfoJuniorDiplomaNumberList)
            {
                if (LIRDic.ContainsKey(sdijdn.StudentID))
                {
                    sdijdn.GRDocNo = LIRDic[sdijdn.StudentID].DiplomaNumber;
                    sdijdn.GRStatus = LIRDic[sdijdn.StudentID].Reason;
                    sdijdn.LeaveInfoRec = LIRDic[sdijdn.StudentID];
                }
            }

            return DiplomaInfoJuniorDiplomaNumberList;
        }

        /// <summary>
        /// 儲存學生畢修業相關資料
        /// </summary>
        /// <param name="DiplomaInfoJuniorDiplomaNumberList"></param>
        public static void SetStudentDiplomaInfoJuniorDiplomaNumberList(List<StudDiplomaInfoJuniorDiplomaNumber> DiplomaInfoJuniorDiplomaNumberList)
        {
            List<JHSchool.Data.JHLeaveInfoRecord> LIRRec = new List<JHSchool.Data.JHLeaveInfoRecord>();
            foreach (StudDiplomaInfoJuniorDiplomaNumber rec in DiplomaInfoJuniorDiplomaNumberList)
            {
                rec.LeaveInfoRec.DiplomaNumber = rec.GRDocNo;                
                rec.LeaveInfoRec.Reason = rec.GRStatus;
                LIRRec.Add(rec.LeaveInfoRec);            
            }

            if (LIRRec.Count > 0)
                JHSchool.Data.JHLeaveIfno.Update(LIRRec);
        
        }

        /// <summary>
        /// 取得班級空號
        /// </summary>
        /// <param name="className"></param>
        /// <returns>空號</returns>
        public static List<string> GetClassNullNoList(string className)
        {
            List<string> tmpNo = new List<string>();
            List<string> tmpClassSeatNo = new List<string>();
            
            //bool class_name = false;
            //foreach (JHSchool.Data.JHClassRecord cr in JHSchool.Data.JHClass.SelectAll ())
            //    if (className == cr.Name)
            //    {
            //        foreach (JHSchool.Data.JHStudentRecord  studRec in JHSchool.Data.JHStudent.SelectByClassID(cr.ID))
            //        {
            //            // 取得一般生座號
            //            if(studRec.Status == K12.Data.StudentRecord.StudentStatus.一般 )
            //            if(studRec.SeatNo.HasValue )
            //            tmpClassSeatNo.Add(studRec.SeatNo.Value);
            //        }
            //        tmpClassSeatNo.Sort();

            //        if (tmpClassSeatNo.Count > 0)
            //        {
            //            for (int i = 1; i <= tmpClassSeatNo[tmpClassSeatNo.Count - 1]; i++)
            //                if (!tmpClassSeatNo.Contains(i))
            //                    tmpNo.Add(i.ToString());

            //            class_name = true;
            //            tmpNo.Add((tmpClassSeatNo[tmpClassSeatNo.Count - 1] + 1).ToString());
            //        }
            //        break;
            //    }

            //if (tmpNo.Count == 0 && class_name == false)
            //    tmpNo.Add("1");

            // 這段query 主要取的班級內一般生座號並排序
            if (!string.IsNullOrWhiteSpace(className))
            {
                QueryHelper qh = new QueryHelper();
                string strSQL = "select student.seat_no from student inner join class on student.ref_class_id=class.id where class.class_name='" + className + "' and student.status=1 and student.seat_no is not null order by student.seat_no;";
                DataTable dt = qh.Select(strSQL);
                foreach (DataRow dr in dt.Rows)
                {                    
                    tmpClassSeatNo.Add(dr[0].ToString());
                }
               
                if (tmpClassSeatNo.Count > 0)
                {
                    // 取得最後一號+1                    
                    int lastNo;

                    if (int.TryParse(tmpClassSeatNo[tmpClassSeatNo.Count - 1], out lastNo))
                    {
                        lastNo += 1; 
                        // 填入空號
                        for (int i = 1; i <= lastNo; i++)
                        {
                            string no = i.ToString();
                            if (!tmpClassSeatNo.Contains(no))
                                tmpNo.Add(no);
                        }
                    }
                }
            }

            if (tmpNo.Count == 0)
                tmpNo.Add("1");

            return tmpNo;
        }


        /// <summary>
        /// 取得年級最後一號
        /// </summary>
        /// <param name="className"></param>
        /// <returns> 學號</returns>
        public static string GetGradeYearLastStudentNumber(string className)
        {
            string retVal = "";
            //Dictionary<string, List<string>> tmpGradeStudNum;
            //tmpGradeStudNum = new Dictionary<string, List<string>>();
          
            //// 取得一般生學號最後一號
            //foreach (JHSchool.Data.JHStudentRecord studRec in JHSchool.Data.JHStudent.SelectAll ())
            //    if(studRec.Status == K12.Data.StudentRecord.StudentStatus.一般 )
            //    if (studRec.Class != null)
            //    {
            //        if(studRec.Class.GradeYear.HasValue )
                    
            //        if (tmpGradeStudNum.ContainsKey(studRec.Class.GradeYear.Value+""))
            //        {
            //            tmpGradeStudNum[studRec.Class.GradeYear.Value +""].Add(studRec.StudentNumber);

            //        }
            //        else
            //        {
            //            List<string> tmp = new List<string>();
            //            tmp.Add(studRec.StudentNumber);
            //            tmpGradeStudNum.Add(studRec.Class.GradeYear.Value +"", tmp);
            //            tmp = null;
            //        }
            //    }

            //// 排序年級學號
            //foreach (KeyValuePair<string, List<string>> Val in tmpGradeStudNum)
            //    Val.Value.Sort(new Comparison<string>(stringCompare));

            //string str = "", strStudNum = "";
            //int strNo;

            //foreach (JHSchool.Data.JHClassRecord  cr in JHSchool.Data.JHClass.SelectAll ())
            //    if (cr.Name == className)
            //    {
                    
            //        str = cr.GradeYear.Value +"";
            //        break;
            //    }
            //if (tmpGradeStudNum.ContainsKey(str))
            //{
            //    int num = 0;
            //    if (tmpGradeStudNum[str].Count > 0)
            //        int.TryParse(tmpGradeStudNum[str][tmpGradeStudNum[str].Count - 1], out num);
            //    strStudNum = (num + 1).ToString();

            //}
            //int.TryParse(strStudNum, out strNo);

            //if (strNo > 0)
            //    return strStudNum;
            //else
            //    return "學號有非數字無法取得";

            if (!string.IsNullOrWhiteSpace(className))
            {
                int grYear = 0;
                // 取的班級年級
                foreach (JHClassRecord rec in JHClass.SelectAll().Where(x => x.Name == className))
                {
                    if(rec.GradeYear.HasValue)
                        grYear = rec.GradeYear.Value;
                }
                retVal = "學號有非數字無法取得";
                if (grYear>0)
                {
                    QueryHelper qh = new QueryHelper();                    
                    string strSQL = "select student_number from student inner join class on student.ref_class_id=class.id where student.status=1 and class.grade_year="+grYear+" and student_number<>'' order by student_number desc limit 1;";
                    DataTable dt = qh.Select(strSQL);
                    foreach (DataRow dr in dt.Rows)
                    {
                        int no;
                        if(int.TryParse(dr[0].ToString(),out no))
                            retVal = (no + 1).ToString();
                    }
                }
            }

            return retVal;
        }

        private static int stringCompare(string x, string y)
        {
            return x.CompareTo(y);
        }

        /// <summary>
        /// 取得努力程度對照表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<decimal, int> getEffortScore()
        {            
            Dictionary<decimal, int> EffortScore = new Dictionary<decimal, int>();
            
            ConfigData cd = JHSchool.School.Configuration["努力程度對照表"];
            if (!string.IsNullOrEmpty(cd["xml"]))
            {
                XmlElement element = XmlHelper.LoadXml(cd["xml"]);

                foreach (XmlElement each in element.SelectNodes("Effort"))
                {
                    int code = int.Parse(each.GetAttribute("Code"));
                    decimal score;
                    if (!decimal.TryParse(each.GetAttribute("Score"), out score))
                        score = 0;

                    if (!EffortScore.ContainsKey(score))
                        EffortScore.Add(score, code);
                }
            }
            return EffortScore;
        }

        /// <summary>
        /// 取得學生修課成績
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="SchoolYear"></param>
        /// <param name="Semester"></param>
        /// <returns></returns>
        /// 
        public static List<ExamScoreEntity> GetStudExamScore(string StudentID, int SchoolYear, int Semester, ScoreType ST)
        {
            int cacheIndex = 0;
            List<ExamScoreEntity> ExamScoreEntitys = new List<ExamScoreEntity>();

            List<JHSchool.Data.JHCourseRecord> currentCourseRecords = new List<JHCourseRecord>();

            List<JHSCAttendRecord> SCAttendList = JHSCAttend.SelectByStudentID(StudentID);

            // 取得學生學年度學期修課
            foreach (JHSCAttendRecord SCRC in SCAttendList)
            {
                if (SCRC.Course.SchoolYear.Value == SchoolYear && SCRC.Course.Semester.Value == Semester)
                {
                    currentCourseRecords.Add(SCRC.Course);
                }
            }

            //List<string> RefAssessmentSetupIDList = new List<string>();

            //// 組對應評量設定ID
            //foreach (JHCourseRecord course in currentCourseRecords)
            //{
            //    RefAssessmentSetupIDList.Add(course.RefAssessmentSetupID);
            //}

            //// 取得評量設定資料
            //List<JHAssessmentSetupRecord> AssessmentSetupRecordList = new List<JHAssessmentSetupRecord>();
            //AssessmentSetupRecordList = JHAssessmentSetup.SelectByIDs(RefAssessmentSetupIDList);



            //JHAssessmentSetupRecord asr = new JHAssessmentSetupRecord();
            //// 取得評分樣版            
            //List<JHAEIncludeRecord> AEIncludeRecordList = new List<JHAEIncludeRecord>();
            //AEIncludeRecordList = JHAEInclude.SelectAll();

            //Dictionary<string, List<JHAEIncludeRecord>> AEIncludeRecordListDic = new Dictionary<string, List<JHAEIncludeRecord>>();

            //foreach (JHAEIncludeRecord ae in AEIncludeRecordList)
            //{

            //    if (AEIncludeRecordListDic.ContainsKey(ae.RefAssessmentSetupID))
            //    {
            //        AEIncludeRecordListDic[ae.RefAssessmentSetupID].Add(ae);
            //    }
            //    else
            //    {

            //        List<JHAEIncludeRecord> aeList = new List<JHAEIncludeRecord>();
            //        aeList.Add(ae);
            //        AEIncludeRecordListDic.Add(ae.RefAssessmentSetupID, aeList);
            //    }
            //}           


            //// 給考試名稱用, 因為 Data.AEIncludeRecord 的 ExamName慢到暴            
            //Dictionary<string, string> ExamNameDic = new Dictionary<string, string>();

            //foreach (JHExamRecord ExamRec in JHExam.SelectAll())
            //    ExamNameDic.Add(ExamRec.ID, ExamRec.Name);

            if (ST == ScoreType.HsinChu)
            {
                List<HC.JHAEIncludeRecord> HCAEIncludeRecordList = new List<HC.JHAEIncludeRecord>();

                foreach (JHAEIncludeRecord aer in JHAEInclude.SelectAll())
                {
                    HC.JHAEIncludeRecord ac = new HC.JHAEIncludeRecord(aer);
                    HCAEIncludeRecordList.Add(ac);
                }

                foreach (JHCourseRecord course in currentCourseRecords)
                {
                    string rsID = course.RefAssessmentSetupID;
                    if (!string.IsNullOrEmpty(rsID))
                    {
                        foreach (HC.JHAEIncludeRecord ae in HCAEIncludeRecordList)
                        {
                            if (ae.RefAssessmentSetupID == rsID)
                            {
                                ExamScoreEntity ese = new ExamScoreEntity();
                                ese.cacheIndex = cacheIndex++;
                                ese.CourseID = course.ID;
                                ese.CourseName = course.Name;
                                ese.ExamID = ae.RefExamID;
                                ese.ExamName = ae.Exam.Name;
                                ese.ScoreCanInput = ae.UseScore;
                                ese.AssScoreCanInput = ae.UseAssignmentScore;
                                ese.TextDescriptionCanInput = ae.UseText;
                                ese.RefAssessmentSetupID = ae.RefAssessmentSetupID;
                                ExamScoreEntitys.Add(ese);
                            }
                        }                    
                    }                
                }            
            }

            if (ST == ScoreType.KaoHsiung)
            {
                List<KH.JHAEIncludeRecord> KHAEIncludeRecordList = new List<KH.JHAEIncludeRecord>();

                foreach (JHAEIncludeRecord aer in JHAEInclude.SelectAll())
                {
                    KH.JHAEIncludeRecord ac = new KH.JHAEIncludeRecord(aer);
                    KHAEIncludeRecordList.Add(ac);
                }

                foreach (JHCourseRecord course in currentCourseRecords)
                {
                    string rsID = course.RefAssessmentSetupID;
                    if (!string.IsNullOrEmpty(rsID))
                    {
                        foreach (KH.JHAEIncludeRecord ae in KHAEIncludeRecordList)
                        {
                            if (ae.RefAssessmentSetupID == rsID)
                            {
                                ExamScoreEntity ese = new ExamScoreEntity();
                                ese.cacheIndex = cacheIndex++;
                                ese.CourseID = course.ID;
                                ese.CourseName = course.Name;
                                ese.ExamID = ae.RefExamID;
                                ese.ExamName = ae.Exam.Name;
                                ese.ScoreCanInput = ae.UseScore;
                                ese.EffortCanInput = ae.UseEffort;
                                ese.TextDescriptionCanInput = ae.UseText;
                                ese.RefAssessmentSetupID = ae.RefAssessmentSetupID;
                                ExamScoreEntitys.Add(ese);
                            }
                        }
                    }
                }        
            
            
            }

            //foreach (JHCourseRecord course in currentCourseRecords)
            //{
            //    string rsID = course.RefAssessmentSetupID;
            //    if (rsID != null)
            //    {
            //        if (AEIncludeRecordListDic.ContainsKey(rsID))
            //        {
            //            foreach (JHAEIncludeRecord ae in AEIncludeRecordListDic[rsID])
            //            {
            //                ExamScoreEntity ese = new ExamScoreEntity();
            //                ese.cacheIndex = cacheIndex++;
            //                ese.CourseID = course.ID;
            //                ese.CourseName = course.Name;
            //                ese.ExamID = ae.RefExamID;
            //                ese.ExamName = ae.Exam.Name;
            //                if (ExamNameDic.ContainsKey(ae.RefExamID))
            //                    ese.ExamName = ExamNameDic[ae.RefExamID];
                            
            //                ese.ScoreCanInput = ae.UseScore;
            //                // 高雄版用到
            //                if(ST == ScoreType.KaoHsiung )
            //                    ese.EffortCanInput = ae.UseEffort;

            //                ese.TextDescriptionCanInput = ae.UseText;
            //                ese.RefAssessmentSetupID = ae.RefAssessmentSetupID;
            //                ExamScoreEntitys.Add(ese);
            //            }
            //        }
            //    }
            //}

            List<JHSchool.Data.JHSCETakeRecord> _SCETakeRecList = JHSchool.Data.JHSCETake.SelectByStudentID(StudentID);

            // 新竹成績
            List<HC.JHSCETakeRecord> HC_SCETakeRecList=new List<HC.JHSCETakeRecord> ();
            // 高雄成績
            List<KH.JHSCETakeRecord> KH_SCETakeRecList=new List<KH.JHSCETakeRecord> ();
            
            // 成績 Record 因地轉型
            if (ST == ScoreType.HsinChu)
            {
                HC_SCETakeRecList = HC_JHSCETakeRecord_ExtendMethod.AsHCJHSCETakeRecords(_SCETakeRecList);
            
            }

            if (ST == ScoreType.KaoHsiung)
            {
                KH_SCETakeRecList = KH_JHSCETakeRecord_ExtendMethod.AsKHJHSCETakeRecords(_SCETakeRecList);
                
            }

            foreach (ExamScoreEntity ese in ExamScoreEntitys)
            {
                ese.ScoreType = ST;

                if (ST == ScoreType.HsinChu)
                {
                    foreach (HC.JHSCETakeRecord rec in HC_SCETakeRecList)
                        if (ese.CourseID == rec.RefCourseID && ese.ExamID == rec.RefExamID)
                        {
                            ese.HC_JHSCETakeRecord = rec;
                        }
                }

                if(ST ==ScoreType.KaoHsiung )
                {
                    foreach (KH.JHSCETakeRecord rec in KH_SCETakeRecList)
                        if (ese.CourseID == rec.RefCourseID && ese.ExamID == rec.RefExamID)
                        {
                            ese.KH_JHSCETakeRecord = rec;
                        }
                    
                }
            }

            List<JHSCAttendRecord> studAttendRecList = JHSCAttend.SelectByStudentID(StudentID);

            // 填入 SCAttendID
            foreach (ExamScoreEntity ese in ExamScoreEntitys)
                foreach (JHSCAttendRecord scAttend in studAttendRecList)
                    if (ese.CourseID == scAttend.RefCourseID)
                        ese.SCAttendID = scAttend.ID;
                
            // 處理當新增成績時 null 狀態

            foreach (ExamScoreEntity ese in ExamScoreEntitys)
            {
                if (ese.ScoreType == ScoreType.HsinChu)
                {
                    if (ese.HC_JHSCETakeRecord == null)
                    {
                        JHSCETakeRecord rec = new JHSCETakeRecord();
                        HC.JHSCETakeRecord HCRec = new HC.JHSCETakeRecord(rec);
                        ese.HC_JHSCETakeRecord = HCRec;
                        ese.HC_JHSCETakeRecord.RefExamID = ese.ExamID;
                        ese.HC_JHSCETakeRecord.RefSCAttendID = ese.SCAttendID;
                        ese.HC_JHSCETakeRecord.RefStudentID = StudentID;                        


                    }                
                }

                if (ese.ScoreType == ScoreType.KaoHsiung)
                {
                    if (ese.KH_JHSCETakeRecord == null)
                    {
                        JHSCETakeRecord rec = new JHSCETakeRecord();
                        KH.JHSCETakeRecord KHRec = new KH.JHSCETakeRecord(rec);
                        ese.KH_JHSCETakeRecord = KHRec;                        
                        ese.KH_JHSCETakeRecord.RefExamID = ese.ExamID;
                        ese.KH_JHSCETakeRecord.RefSCAttendID = ese.SCAttendID;
                        ese.KH_JHSCETakeRecord.RefStudentID = StudentID;
                       
                    }                
                }            
            }
            
            return ExamScoreEntitys;
        }


        /// <summary>
        /// 儲存學生資料
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="ExamScoreEntityList"></param>
        public static void SetStudExamScore(List<ExamScoreEntity> ExamScoreEntityList)
        {
            List<HC.JHSCETakeRecord> HC_JHSCETakeRecordList_Update = new List<HC.JHSCETakeRecord>();

            //List<HC.JHSCETakeRecord> HC_JHSCETakeRecordList_Insert = new List<HC.JHSCETakeRecord>();

            List<KH.JHSCETakeRecord> KH_JHSCETakeRecordList_Update = new List<KH.JHSCETakeRecord>();

            //List<KH.JHSCETakeRecord> KH_JHSCETakeRecordList_Insert = new List<KH.JHSCETakeRecord>();

            List<JHSCETakeRecord> SCETakeRecordList_Update = new List<JHSCETakeRecord>();

            //List<JHSCETakeRecord> SCETakeRecordList_Insert = new List<JHSCETakeRecord>();

            foreach (ExamScoreEntity ese in ExamScoreEntityList)
            {
                if (ese.ScoreType == ScoreType.HsinChu)
                {
                    //if (string.IsNullOrEmpty(ese.HC_JHSCETakeRecord.SCTakeID))
                    //    HC_JHSCETakeRecordList_Insert.Add(ese.HC_JHSCETakeRecord);
                    //else
                        HC_JHSCETakeRecordList_Update.Add(ese.HC_JHSCETakeRecord);
                }

                if (ese.ScoreType == ScoreType.KaoHsiung)
                {
                    //if (string.IsNullOrEmpty(ese.KH_JHSCETakeRecord.SCTakeID))
                    //    KH_JHSCETakeRecordList_Insert.Add(ese.KH_JHSCETakeRecord);
                    //else                                                   
                        KH_JHSCETakeRecordList_Update.Add(ese.KH_JHSCETakeRecord);
                }
            }


            //if (HC_JHSCETakeRecordList_Insert.Count > 0)
            //    SCETakeRecordList_Insert = HC_JHSCETakeRecord_ExtendMethod.AsJHSCETakeRecords(HC_JHSCETakeRecordList_Insert);

            if (HC_JHSCETakeRecordList_Update.Count > 0)
                SCETakeRecordList_Update = HC_JHSCETakeRecord_ExtendMethod.AsJHSCETakeRecords(HC_JHSCETakeRecordList_Update);


            //if (KH_JHSCETakeRecordList_Insert.Count > 0)
            //    SCETakeRecordList_Insert = KH_JHSCETakeRecord_ExtendMethod.AsJHSCETakeRecords(KH_JHSCETakeRecordList_Insert);

            if (KH_JHSCETakeRecordList_Update.Count > 0)
                SCETakeRecordList_Update = KH_JHSCETakeRecord_ExtendMethod.AsJHSCETakeRecords(KH_JHSCETakeRecordList_Update);

            //// 新增成績
            //if (SCETakeRecordList_Insert.Count > 0)
            //    JHSCETake.Insert(SCETakeRecordList_Insert);
            
            // 更新成績
            if (SCETakeRecordList_Update.Count > 0)
            {
                
                
                foreach (JHSCETakeRecord sce in SCETakeRecordList_Update)
                {
                    if (sce == null)
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("修課資料不全，檢查");
                        return;                    
                    }
                    if (string.IsNullOrEmpty(sce.ID) || string.IsNullOrEmpty(sce.RefExamID) || string.IsNullOrEmpty(sce.RefSCAttendID))
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("修課資料不全，檢查");
                        return;
                    }
                }
                JHSCETake.Update(SCETakeRecordList_Update);
            }
        }



        /// <summary>
        /// 取得某學年度學期某位學生修課狀況
        /// </summary>
        /// <param name="SchoolYear"></param>
        /// <param name="Semester"></param>
        /// <param name="studRec"></param>
        /// <param name="classRec"></param>
        /// <returns></returns>
        public static List<AttendCourseEntity> getStudAttendCourseBySchoolYearSemester(int SchoolYear, int Semester, JHStudentRecord studRec, JHClassRecord classRec)
        {
            List<AttendCourseEntity> ClassAttendCourseRec = new List<AttendCourseEntity>();
            List<AttendCourseEntity> StudAttendCourseRec = new List<AttendCourseEntity>();
            List<AttendCourseEntity> returnAttendCourseRecs = new List<AttendCourseEntity>();

            // 取得該班課程
            List<JHCourseRecord> _ClassCoursesList = JHCourse.SelectByClass(SchoolYear, Semester, classRec.ID);


            // 取得學生設定班級當學年度學期的課程
            foreach (JHCourseRecord cor in _ClassCoursesList)
                if (cor.SchoolYear == SchoolYear && cor.Semester == Semester)
                {
                    AttendCourseEntity ace = new AttendCourseEntity();
                    ace.CourseID = cor.ID;
                    ace.CourseName = cor.Name;
                    ace.CourseRec = cor;
                    if (cor.Credit.HasValue)
                        ace.Credit = cor.Credit.Value+"";
                    ace.SubjectName = cor.Subject;
                    ace.CousreAttendType = AttendCourseEntity.AttendType.學生未修;
                    ClassAttendCourseRec.Add(ace);
                    ace = null;
                }

            List<JHSCAttendRecord> studSCAttendList = JHSCAttend.SelectByStudentID(studRec.ID);

            List<JHCourseRecord> StudAttendCourseList = new List<JHCourseRecord>();
            foreach (JHSCAttendRecord attend in studSCAttendList)
                StudAttendCourseList.Add(attend.Course);            

            // 取得學生當學年度學期修課名稱
            foreach (JHCourseRecord cor in StudAttendCourseList)
                if (cor.SchoolYear == SchoolYear && cor.Semester == Semester)
                {
                    AttendCourseEntity ace = new AttendCourseEntity();
                    ace.CourseID = cor.ID;
                    ace.CourseName = cor.Name;
                    ace.CourseRec = cor;
                    if(cor.Credit.HasValue)
                        ace.Credit = cor.Credit.Value+"";
                    ace.SubjectName = cor.Subject;
                    ace.CousreAttendType = AttendCourseEntity.AttendType.學生本身已修;
                    StudAttendCourseRec.Add(ace);
                    ace = null;
                }

            bool chkAttend = false;
            foreach (AttendCourseEntity aceClass in ClassAttendCourseRec)
            {
                AttendCourseEntity removeItem = null;
                chkAttend = false;
                foreach (AttendCourseEntity aceStud in StudAttendCourseRec)
                    if (aceClass.CourseName == aceStud.CourseName)
                    {
                        chkAttend = true;
                        removeItem = aceStud;
                        break;
                    }

                // 當班級與學生修課相同
                if (chkAttend == true)
                {
                    StudAttendCourseRec.Remove(removeItem);
                    aceClass.CousreAttendType = AttendCourseEntity.AttendType.學生修課與班級相同;
                    returnAttendCourseRecs.Add(aceClass);
                }
                else
                    returnAttendCourseRecs.Add(aceClass);
            }

            foreach (AttendCourseEntity ace in StudAttendCourseRec)
                returnAttendCourseRecs.Add(ace);

            return returnAttendCourseRecs;
        }
 
        /// <summary>
        /// 儲存學生選課(Insert)
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="CourseList"></param>
        public static void SetStudentAttendCourse(string StudentID,List<JHCourseRecord> InesrtCourseList)
        {
            List<JHSCAttendRecord> SCAttendList = new List<JHSCAttendRecord>();
            foreach (JHCourseRecord cor in InesrtCourseList)
            {
                JHSCAttendRecord scattend = new JHSCAttendRecord();
                scattend.RefCourseID = cor.ID;
                scattend.RefStudentID = StudentID;
                SCAttendList.Add(scattend);
            }

            JHSCAttend.Insert(SCAttendList);
        
        }

        /// <summary>
        /// 評量 Null 新增
        /// </summary>
        /// <param name="examScoreList"></param>
        public static void AddNullSCTakeRecord(List<ExamScoreEntity> examScoreList)
        {
            List<JHSCETakeRecord> InsertData = new List<JHSCETakeRecord>();
            foreach (ExamScoreEntity ese in examScoreList)
            {
                JHSCETakeRecord rec = new JHSCETakeRecord();
                rec.RefExamID = ese.ExamID;
                rec.RefSCAttendID = ese.SCAttendID;
                InsertData.Add(rec);
            }
            JHSCETake.Insert(InsertData);
        }

        /// <summary>
        /// 取得DAL轉換後異動名冊實體
        /// </summary>
        /// <param name="SchoolYear"></param>
        /// <param name="Semester"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static StudBatchUpdateRecEntity GetStudBatchUpdateRecEntity(int SchoolYear,int Semester,string Name)
        {
            StudBatchUpdateRecEntity retVal = new StudBatchUpdateRecEntity();

            foreach (JHUpdateReocrdBatchRecord urbr in JHUpdateRecordBatch.SelectBySchoolYearAndSemester(SchoolYear, Semester))
                if (urbr.Name == Name)
                {
                    retVal.ID = urbr.ID;
                    retVal.Name = urbr.Name;
                    retVal.SchoolYear = urbr.SchoolYear;
                    retVal.Semester = urbr.Semester;
                    retVal.UpdateBatchRec = urbr;
                    if(urbr.ADDate.HasValue )
                        retVal.ADDate = urbr.ADDate.Value;
                    retVal.ADNumber = urbr.ADNumber;                    
                    break;
                }
            return retVal;
        }

        /// <summary>
        /// 儲存異動名冊實體轉成DAL
        /// </summary>
        /// <param name="sbure"></param>
        public static void SetStudBatchUpdateRecEntity(StudBatchUpdateRecEntity sbure,UpdateBatchEditDALMode ubedm)
        {
            if (ubedm == UpdateBatchEditDALMode.新增)
                JHUpdateRecordBatch.Insert(sbure.UpdateBatchRec);

            if (ubedm == UpdateBatchEditDALMode.更新)
                JHUpdateRecordBatch.Update(sbure.UpdateBatchRec);
        }

        
        /// <summary>
        /// 取得學生異動名冊需要資料
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="UT"></param>
        /// <returns>UpdateRecID,StudBatchUpdateRecContentEntity</returns>
        public static Dictionary<string,StudBatchUpdateRecContentEntity> GetStudBatchUpdateRecContentEntityDic(DateTime BeginDate, DateTime EndDate, UpdateType UT,List<string> StudIDList)
        {
            Dictionary<string, StudBatchUpdateRecContentEntity> retVal = new Dictionary<string, StudBatchUpdateRecContentEntity>();
            Dictionary<string ,List<StudUpdateRecordEntity>> studUPRecDic = GetStudListUpdateRecordEntityListByUpdateType(StudIDList, UT);
            // 監護人
            Dictionary<string,string > GuardianNameDic = GetGuardianName();

            // 戶籍地址
            Dictionary <string,string>StudPermanentAddressDic =GetStudPermanentAddress ();

            foreach (KeyValuePair<string,List<StudUpdateRecordEntity>> data in studUPRecDic)
            {
                foreach (StudUpdateRecordEntity sure in data.Value)
                {
                    // 異動日期在開始結束期間
                    if (sure.GetUpdateDate() >= BeginDate && sure.GetUpdateDate() <= EndDate)
                    {
                        StudBatchUpdateRecContentEntity sburce = new StudBatchUpdateRecContentEntity(null);
                        sburce.SetAddress(sure.GetAddress());
                        if(sure.GetBirthday().HasValue )
                        sburce.SetBirthday(sure.GetBirthday().Value.ToShortDateString ());
                        sburce.SetBirthPlace(sure.GetBirthPlace());
                        sburce.SetClassName(sure.GetClassName());
                        sburce.SetComment(sure.GetComment());
                        sburce.SetEnrollmentSchoolYear(sure.GetEnrollmentSchoolYear());
                        sburce.SetGender(sure.GetGender());
                        if (!string.IsNullOrEmpty(sure.GetGender()))
                        {
                            if (sure.GetGender() == "男")
                                sburce.SetGenderID("1");

                            if (sure.GetGender() == "女")
                                sburce.SetGenderID("2");
                        }

                        sburce.SetGradeYear(sure.GetGradeYear());
                        sburce.SetGraduate(sure.GetGraduate());
                        sburce.SetGraduateCertificateNumber(sure.GetGraduateCertificateNumber());
                        sburce.SetGraduateSchoolYear(sure.GetGraduateSchoolYear());

                        sburce.SetID(sure.UID);
                        sburce.SetIDNumber(sure.GetIDNumber());
                        sburce.SetImportExportSchool(sure.GetImportExportSchool());
                        if(sure.GetLastADDate().HasValue )
                            sburce.SetLastADDate(sure.GetLastADDate().Value.ToShortDateString ());
                        sburce.SetLastADNumber(sure.GetLastADNumber());
                        sburce.SetName(sure.GetName());
                        if(sure.GetNewBirthday ().HasValue )
                            sburce.SetNewBirthday(sure.GetNewBirthday ().Value.ToShortDateString ());
                        sburce.SetNewIDNumber(sure.GetNewIDNumber ());
                        sburce.SetNewName(sure.GetNewName());
                        sburce.SetNewGender(sure.GetNewGender());
                        sburce.SetPrimarySchoolName(sure.GetGraduateSchool());
                        sburce.SetStudentNumber(sure.GetStudentNumber());
                        sburce.SetUpdateCode(sure.GetUpdateCode());
                        sburce.SetUpdateCodeType(UT.ToString());
                        if(sure.GetUpdateDate().HasValue )
                            sburce.SetUpdateDate(sure.GetUpdateDate().Value.ToShortDateString ());
                        sburce.SetUpdateDescription(sure.GetUpdateDescription());
                        if(GuardianNameDic.ContainsKey(data.Key ))
                            sburce.SetGuardian(GuardianNameDic[data.Key]);
                        if (StudPermanentAddressDic.ContainsKey(data.Key))
                            sburce.SetPermanentAddress(StudPermanentAddressDic[data.Key]);
                        retVal.Add(sure.UID, sburce);    
                    }
                }                   
            }
            return retVal;
        }

        /// <summary>
        /// 取得所有學生戶籍地址,return: StudentID,Address
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetStudPermanentAddress()
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            foreach (JHAddressRecord rec in JHAddress.SelectAll ())
                if (!retVal.ContainsKey(rec.RefStudentID))
                {
                    string str = rec.PermanentCounty + rec.PermanentTown + rec.PermanentDistrict + rec.PermanentArea + rec.PermanentDetail;
                    retVal.Add(rec.RefStudentID, str);
                }

            return retVal;        
        }

        /// <summary>
        /// 取得監護人資料
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetGuardianName()
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            foreach (JHParentRecord rec in JHParent.SelectAll())
                if (!retVal.ContainsKey(rec.RefStudentID))
                    retVal.Add(rec.RefStudentID, rec.CustodianName);
            return retVal;
        }

        /// <summary>
        /// 取得異動代碼文字
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetUpdateRecCodeString(string code)
        {
            string retValue = string.Empty;

            switch (code)
            { 
                case "1":retValue="新生";break;
                case "2":retValue ="畢業";break ;
                case "3":retValue ="轉入";break ;
                case "4":retValue ="轉出";break ;
                case "5":retValue ="休學";break;
                case "6":retValue ="復學";break;
                case "7":retValue ="中輟";break ;
                case "8":retValue ="續讀";break ;
                case "9": retValue = "更正學籍"; break;
                case "10": retValue = "延長修業年限"; break;
                case "11": retValue = "死亡"; break;  
            }
            return retValue;
        }

        /// <summary>
        /// 文字轉換代碼
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetUpdateRecCodeByString(string str)
        {
            string retValue = string.Empty;

            switch (str)
            {
                case "新生": retValue = "1"; break;
                case "畢業": retValue = "2"; break;
                case "轉入": retValue = "3"; break;
                case "轉出": retValue = "4"; break;
                case "休學": retValue = "5"; break;
                case "復學": retValue = "6"; break;
                case "中輟": retValue = "7"; break;
                case "續讀": retValue = "8"; break;
                case "更正學籍": retValue = "9"; break;
            }
            return retValue;
        }

        /// <summary>
        /// 文字轉換代碼3~9，學籍異動使用。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetUpdateRecCodeByString39(string str)
        {
            string retValue = string.Empty;

            switch (str)
            {
                case "轉入": retValue = "3"; break;
                case "轉出": retValue = "4"; break;
                case "休學": retValue = "5"; break;
                case "復學": retValue = "6"; break;
                case "中輟": retValue = "7"; break;
                case "續讀": retValue = "8"; break;
                case "更正學籍": retValue = "9"; break;
                case "延長修業年限": retValue = "10"; break;
                case "死亡": retValue = "11"; break;
            }
            return retValue;
        }

    }
}
