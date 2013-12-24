using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHPermrec.UpdateRecord.DAL
{
    public class AttendCourseEntity
    {
        public enum AttendType { 學生未修,學生本身已修,學生修課與班級相同}
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public string  Credit { get; set; }
        public string SubjectName { get; set; }
        public JHSchool.Data.JHCourseRecord CourseRec { get; set; }
        public AttendType CousreAttendType { get; set; }
    }
}
