using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaiChung.StudentRecordReport
{
    /// <summary>
    /// 學生資料
    /// </summary>
    public class StudentInfo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 入學時學校
        /// </summary>
        public string EnterSchool { get; set; }

        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber { get; set; }
        
        /// <summary>
        /// 出生年月日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 身分證字號
        /// </summary>
        public string ID_Number { get; set; }

        /// <summary>
        /// 入學年月
        /// </summary>
        public string EnterYearMonth { get; set; }

        /// <summary>
        /// 身分註記
        /// </summary>
        public string Type1 { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 學期歷程
        /// </summary>
        public List<StudSemsHistory> StudSemsHistoryList = new List<StudSemsHistory>();

        /// <summary>
        /// 異動資料
        /// </summary>
        public List<StudUpdateRecord> StudUpdateRecordList = new List<StudUpdateRecord>();

        /// <summary>
        /// 領域成績
        /// </summary>
        public List<StudDomainScore> StudDomainScoreList = new List<StudDomainScore>();

        /// <summary>
        /// 科目成績
        /// </summary>
        public List<StudSubjectScore> StudSubjectScoreList = new List<StudSubjectScore>();

        /// <summary>
        /// 學期成績
        /// </summary>
        public List<StudSemsScore> StudSemsScoreList = new List<StudSemsScore>();
        
        /// <summary>
        /// 畢業成績
        /// </summary>
        public List<StudGraduateScore> StudGraduateScoreList = new List<StudGraduateScore>();

        /// <summary>
        /// 出缺席紀錄統計
        /// </summary>
        public List<StudAbsenceCount> StudAbsenceCountList = new List<StudAbsenceCount>();

        /// <summary>
        /// 日常生活具體內容
        /// </summary>
        public List<StudTextScore> StudTextScoreList = new List<StudTextScore>();
    }
}
