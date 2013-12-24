using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JHSchool.Data;
using Aspose.Words;
using System.IO;
using K12.Data;
using Aspose.Words.Drawing;
using Campus.Report;
using JHSchool.Behavior.BusinessLogic;

namespace TaiChung.StudentRecordReport
{
    public partial class PrintForm :FISCA.Presentation.Controls.BaseForm
    {
        BackgroundWorker _bkWorker;
        /// <summary>
        /// 學生系統編號
        /// </summary> 
        List<string> _StudentIDList;

        /// <summary>
        /// 學生資料
        /// </summary>
        List<StudentInfo> _StudentInfoList;

        Dictionary<string, JHStudentRecord> _tmpStudRecDict = new Dictionary<string, JHStudentRecord>();
        Dictionary<string, List<JHUpdateRecordRecord>> _tmpStudUpdateRecDict = new Dictionary<string, List<JHUpdateRecordRecord>>();
        Dictionary<string, JHSemesterHistoryRecord> _tmpStudSemHistory = new Dictionary<string, JHSemesterHistoryRecord>();
        Dictionary<string, List<JHSemesterScoreRecord>> _tmpStudSemsScoreDict = new Dictionary<string, List<JHSemesterScoreRecord>>();
        Dictionary<string, List<AutoSummaryRecord>> _tmpAttendanceRecordDict = new Dictionary<string, List<AutoSummaryRecord>>();
        Dictionary<string, JHBeforeEnrollmentRecord> _tmpBeforeEnrollmentRecordDict = new Dictionary<string, JHBeforeEnrollmentRecord>();
        Dictionary<string, List<StudTextScore>> _tmpStudTextScore = new Dictionary<string, List<StudTextScore>>();
        Dictionary<string, List<StudGraduateScore>> _tmpStudGraduateScore = new Dictionary<string, List<StudGraduateScore>>();
        Dictionary<string, string> _tmpStudPhoto = new Dictionary<string, string>();
        Dictionary<string, string> _tmpStudTypeDict = new Dictionary<string, string>();
        
        ReportConfiguration _config;

        public PrintForm(List<string> StudentIDList)
        {
            InitializeComponent();
            _bkWorker = new BackgroundWorker();
            _StudentInfoList = new List<StudentInfo>();
            _bkWorker.DoWork += new DoWorkEventHandler(_bkWorker_DoWork);
            _bkWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkWorker_RunWorkerCompleted);
            _StudentIDList = StudentIDList;
        }

        void _bkWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPrint.Enabled = true;
            //
            try
            {
                FillData();
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show("資料產生發生錯誤." + ex.Message);
            }
        }

        void _bkWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadInitData();
            FillStudInfo();
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.MinimumSize = this.Size;
            
            // 讀取列印設定
            _config = new ReportConfiguration(Global.ReportName);
            Global._PrintModeString = _config.GetString("領域科目設定", string.Empty);
            if (string.IsNullOrEmpty(Global._PrintModeString))
                Global._PrintModeString = "Domain";

            Global._BusinessContractor = txtBusinessContractor.Text = _config.GetString("業務承辦", "");
            Global._RegisteredLeader = txtRegisteredLeader.Text = _config.GetString("註冊組長", "");

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 載入所需要暫存資料
        /// </summary>
        private void LoadInitData()
        {
            _tmpAttendanceRecordDict.Clear();
            _tmpBeforeEnrollmentRecordDict.Clear();
            _tmpStudRecDict.Clear();
            _tmpStudSemHistory.Clear();
            _tmpStudSemsScoreDict.Clear();
            _tmpStudUpdateRecDict.Clear();
            _tmpStudTextScore.Clear();
            _tmpStudGraduateScore.Clear();
            _tmpStudPhoto.Clear();

            // 取的等第對照
            Global._ScoreLevelMapping = Utility.GetScoreLevelMapping();

            // 取得假別設定
            Global._types = Utility.GetTypes();

            // 取得身分註記
            _tmpStudTypeDict = Utility.GetStudIDTypeList(_StudentIDList);

            // 學生基本
            foreach (JHStudentRecord studRec in JHStudent.SelectByIDs(_StudentIDList))
                _tmpStudRecDict.Add(studRec.ID, studRec);

            //異動
            List<JHUpdateRecordRecord> upRecList = JHUpdateRecord.SelectByStudentIDs(_StudentIDList);
            foreach(string id in _StudentIDList)
                _tmpStudUpdateRecDict.Add(id,(from data in upRecList where data.StudentID==id select data).ToList());

        
            // 取得畢業照片
            _tmpStudPhoto = K12.Data.Photo.SelectGraduatePhoto(_StudentIDList);

            // 學期歷程            
            foreach (JHSemesterHistoryRecord rec in JHSemesterHistory.SelectByStudentIDs(_StudentIDList))
                _tmpStudSemHistory.Add(rec.RefStudentID, rec);

            // 學期成績
            List<JHSemesterScoreRecord> semscorelist=JHSemesterScore.SelectByStudentIDs(_StudentIDList);
            foreach(string id in _StudentIDList)            
                _tmpStudSemsScoreDict.Add(id,(from data in semscorelist where data.RefStudentID ==id select data).ToList());
            
            // 畢業成績
            _tmpStudGraduateScore = Utility.GetStudGraduateDictJH(_StudentIDList);

            // 缺曠
            List<AutoSummaryRecord> attendanceList = AutoSummary.Select(_StudentIDList,null);
            foreach (string id in _StudentIDList)
                _tmpAttendanceRecordDict.Add(id, (from data in attendanceList where data.RefStudentID == id select data).ToList());

            // 具體建議
            _tmpStudTextScore = Utility.GetStudentTextScoreDict(_StudentIDList);
            
        }

        /// <summary>
        /// 填入學生資料
        /// </summary>
        private void FillStudInfo()
        {
            _StudentInfoList.Clear();
            foreach (string studId in _StudentIDList)
            {
                StudentInfo si = new StudentInfo();
                JHStudentRecord studRec = null;
                if (_tmpStudRecDict.ContainsKey(studId))
                    studRec = _tmpStudRecDict[studId];

                if (studRec == null)
                    continue;

                si.Birthday = studRec.Birthday;
                si.ID_Number = studRec.IDNumber;
                si.Name = studRec.Name;
                si.StudentNumber = studRec.StudentNumber;
                si.Gender = studRec.Gender;
                
                // 取得現有縣市
                List<string> countyList = Utility.GetCountyList();

                // 取得學期歷程
                if (_tmpStudSemHistory.ContainsKey(studId))
                {
                    foreach (SemesterHistoryItem shi in _tmpStudSemHistory[studId].SemesterHistoryItems)
                    {
                        StudSemsHistory ssh = new StudSemsHistory();
                        ssh.SchoolYear = shi.SchoolYear;
                        ssh.Semester = shi.Semester;
                        ssh.GradeYear = shi.GradeYear;
                        ssh.ClassTeacher = shi.Teacher;
                        ssh.SchoolDayCount = shi.SchoolDayCount;                        
                        si.StudSemsHistoryList.Add(ssh);
                    }
                }

                si.EnterSchool = "";
                si.EnterYearMonth = "";                

                // 取得異動
                if (_tmpStudUpdateRecDict.ContainsKey(studId))
                {                    
                    foreach (JHUpdateRecordRecord rec in (from data in _tmpStudUpdateRecDict[studId] orderby DateTime.Parse(data.UpdateDate) select data))
                    {
                        
                        StudUpdateRecord sur = new StudUpdateRecord();
                        if(!string.IsNullOrEmpty(rec.ADDate))
                            sur.ADDate = DateTime.Parse(rec.ADDate);

                        sur.ADDocNo = rec.ADNumber;
                        sur.UpdateDate = DateTime.Parse(rec.UpdateDate);
                        switch (rec.UpdateCode)
                        {
                            case "1": sur.UpdateType = "新生"; break;
                            case "2": sur.UpdateType = "畢業"; break;
                            case "3": sur.UpdateType = "轉入"; break;
                            case "4": sur.UpdateType = "轉出"; break;
                            case "5": sur.UpdateType = "休學"; break;
                            case "6": sur.UpdateType = "復學"; break;
                            case "7": sur.UpdateType = "中輟"; break;
                            case "8": sur.UpdateType = "續讀"; break;
                            case "9": sur.UpdateType = "更正學籍"; break;
                        }
                        sur.ADGov = "臺中市政府教育局";
                        sur.County = "";
                        if(string.IsNullOrEmpty(sur.SchoolName))
                            sur.SchoolName =rec.ImportExportSchool ;

                        // 入學學校使用畢業國小
                        if (rec.UpdateCode == "1")
                            if(!string.IsNullOrEmpty(rec.GraduateSchool))
                                si.EnterSchool = rec.GraduateSchool;

                        // 縣市
                        if (!string.IsNullOrWhiteSpace(sur.SchoolName))
                        {
                            foreach (string str in countyList)
                                if (sur.SchoolName.Contains(str))
                                {
                                    sur.County = str;
                                    sur.SchoolName=sur.SchoolName.Replace(str, "");
                                    break;
                                }
                        }                       

                        si.StudUpdateRecordList.Add(sur);
                        if(string.IsNullOrEmpty(si.EnterYearMonth))
                            si.EnterYearMonth=rec.EnrollmentSchoolYear;

                        
                    }
                }

                // 身分註記
                si.Type1 = "一般生";
                if (_tmpStudTypeDict.ContainsKey(studId))
                    si.Type1 = _tmpStudTypeDict[studId];

                Dictionary<string, StudDomainScore> tmpDomainDict = new Dictionary<string, StudDomainScore>();
                // 取得學期成績
                if (_tmpStudSemsScoreDict.ContainsKey(studId))
                {
                    tmpDomainDict.Clear();
                    foreach (JHSemesterScoreRecord rec in _tmpStudSemsScoreDict[studId])
                    {                       
                        //領域
                        foreach (DomainScore ds in rec.Domains.Values)
                        {
                            StudDomainScore sds = new StudDomainScore();
                            
                                sds.DomainName = ds.Domain;                                
                                sds.Period = ds.Period;
                                sds.SchoolYear = ds.SchoolYear;
                                sds.Semester = ds.Semester;
                                sds.Score = ds.Score;
                                sds.Level = Utility.GetScoreLevel(ds.Score);

                            // 處理彈性課程
                                if (sds.DomainName == "")
                                    continue;
                                                        
                                // 語文
                                if (sds.DomainName == "語文")
                                {
                                    foreach (SubjectScore ss in (from data in rec.Subjects.Values where data.SchoolYear == sds.SchoolYear && data.Semester == sds.Semester && data.Domain == "語文" select data))
                                    {
                                        StudSubjectScore sjs = new StudSubjectScore();
                                        sjs.Period = ss.Period;
                                        sjs.SchoolYear = ss.SchoolYear;
                                        sjs.Semester = ss.Semester;
                                        sjs.SubjectName = ss.Subject;
                                        sjs.Score = ss.Score;
                                        sjs.DomainName = "語文";
                                        sjs.Level = Utility.GetScoreLevel(ss.Score);
                                        sds.StudSubjectScoreList.Add(sjs);
                                    }
                                }
 
                            ////彈性課程
                            //    if (sds.DomainName == "彈性課程")
                            //    {
                            //        foreach (SubjectScore ss in (from data in rec.Subjects.Values where data.SchoolYear == sds.SchoolYear && data.Semester == sds.Semester && data.Domain == "彈性課程" select data))
                            //        {
                            //            StudSubjectScore sjs = new StudSubjectScore();
                            //            sjs.Period = ss.Period;
                            //            sjs.SchoolYear = ss.SchoolYear;
                            //            sjs.Semester = ss.Semester;
                            //            sjs.SubjectName = ss.Subject;
                            //            sjs.Score = ss.Score;
                            //            sjs.DomainName = "彈性課程";
                            //            sjs.Level = Utility.GetScoreLevel(ss.Score);
                            //            sds.StudSubjectScoreList.Add(sjs);
                            //        }
                            //    }

                            //// 空白
                            //    if (sds.DomainName == "")
                            //    {
                            //        foreach (SubjectScore ss in (from data in rec.Subjects.Values where data.SchoolYear == sds.SchoolYear && data.Semester == sds.Semester && data.Domain == "" select data))
                            //        {
                            //            StudSubjectScore sjs = new StudSubjectScore();
                            //            sjs.Period = ss.Period;
                            //            sjs.SchoolYear = ss.SchoolYear;
                            //            sjs.Semester = ss.Semester;
                            //            sjs.SubjectName = ss.Subject;
                            //            sjs.Score = ss.Score;
                            //            sjs.DomainName = "";
                            //            sjs.Level = Utility.GetScoreLevel(ss.Score);
                            //            sds.StudSubjectScoreList.Add(sjs);
                            //        }
                            //    }

                                string key = sds.SchoolYear + "_" + sds.Semester + "_" + sds.DomainName;
                                if (tmpDomainDict.ContainsKey(key))
                                {
                                    foreach (StudSubjectScore sss1 in sds.StudSubjectScoreList)
                                        tmpDomainDict[key].StudSubjectScoreList.Add(sss1);
                                }
                                else
                                    tmpDomainDict.Add(key, sds);
                        }



                        // 科目
                        decimal SumPeriod = 0;
                        foreach (SubjectScore ss in rec.Subjects.Values)
                        {
                            StudSubjectScore sjs = new StudSubjectScore();
                            sjs.Period = ss.Period;
                            sjs.SchoolYear = ss.SchoolYear;
                            sjs.Semester = ss.Semester;
                            sjs.SubjectName = ss.Subject;
                            sjs.Score = ss.Score;
                            if (string.IsNullOrEmpty(ss.Domain))
                                sjs.DomainName = "彈性課程";
                            else
                                sjs.DomainName = ss.Domain;
                            sjs.Level = Utility.GetScoreLevel(ss.Score);

                            si.StudSubjectScoreList.Add(sjs);
                            if (!string.IsNullOrEmpty(ss.Domain))
                                if (ss.Period.HasValue)
                                    SumPeriod += ss.Period.Value;

                        }

                        // 學習成績
                        StudSemsScore sss = new StudSemsScore();
                        sss.SchoolYear = rec.SchoolYear;
                        sss.Semester = rec.Semester;
                        sss.Score = rec.LearnDomainScore;
                        sss.Period = SumPeriod;
                        sss.Level = Utility.GetScoreLevel(rec.LearnDomainScore);

                        si.StudSemsScoreList.Add(sss);
                    }

                    // 放入領域成績
                    foreach (KeyValuePair<string, StudDomainScore> data in tmpDomainDict)
                        si.StudDomainScoreList.Add(data.Value);
                }

                // 取得缺曠
                if (_tmpAttendanceRecordDict.ContainsKey(studId))
                {  
                    foreach (AutoSummaryRecord rec in _tmpAttendanceRecordDict[studId])
                    {
                        StudAbsenceCount  sac = new StudAbsenceCount ();
                        sac.SchoolYear=rec.SchoolYear;
                        sac.Semester=rec.Semester;
                        foreach (AbsenceCountRecord recCount in rec.AbsenceCounts)
                        {
                            if (Global._types.ContainsKey(recCount.PeriodType))
                            {
                                if (Global._types[recCount.PeriodType].Contains(recCount.Name))
                                {
                                    if (sac.AbsenceCount.ContainsKey(recCount.PeriodType))
                                    {
                                        if (sac.AbsenceCount[recCount.PeriodType].ContainsKey(recCount.Name))
                                            sac.AbsenceCount[recCount.PeriodType][recCount.Name] += recCount.Count;
                                        else
                                            sac.AbsenceCount[recCount.PeriodType].Add(recCount.Name, recCount.Count);
                                    }
                                    else
                                    {
                                        Dictionary<string, int> data = new Dictionary<string, int>();
                                        data.Add(recCount.Name, recCount.Count);
                                        sac.AbsenceCount.Add(recCount.PeriodType, data);
                                    }
                                }
                            }
                        }
                        si.StudAbsenceCountList.Add(sac);
                    }
                
                }
                // 取得日常生活表現具體建議
                if (_tmpStudTextScore.ContainsKey(studId))
                    si.StudTextScoreList = _tmpStudTextScore[studId];


                // 取得畢業成績
                if (_tmpStudGraduateScore.ContainsKey(studId))
                    si.StudGraduateScoreList = _tmpStudGraduateScore[studId];

                // 取得照片
                if (_tmpStudPhoto.ContainsKey(studId))
                    si.Photo = _tmpStudPhoto[studId];

                _StudentInfoList.Add(si);
            }
        }

        /// <summary>
        /// 填入資料
        /// </summary>        
        private void FillData()
        {
            // 填到 word
            Document doc = new Document();
            doc.Sections.Clear();

            List<string> DomainList = new List<string>();
            DomainList.Add("語文");
            DomainList.Add("數學");
            DomainList.Add("自然與生活科技");
            DomainList.Add("社會");
            DomainList.Add("健康與體育");
            DomainList.Add("藝術與人文");
            DomainList.Add("綜合活動");

            foreach (StudentInfo studInfo in _StudentInfoList)
            {
                Document docData = new Document(new MemoryStream(Properties.Resources.template));

                // 填學生基本
                DocumentBuilder builder = new DocumentBuilder(docData);
                builder.MoveToParagraph(0, 0);
                builder.Write(K12.Data.School.ChineseName);
                //docData.Sections[0].Body.FirstParagraph.Runs[0].Text = K12.Data.School.ChineseName;

                // 姓名
                builder.MoveToCell(Global._T_StudBaseInfoIdx, 0, 1, 0);
                builder.Write(studInfo.Name);
                
                // 性別
                builder.MoveToCell(Global._T_StudBaseInfoIdx, 0, 3, 0);
                builder.Write(studInfo.Gender);

                // 入學學校
                builder.MoveToCell(Global._T_StudBaseInfoIdx, 0, 5, 0);
                builder.Write(studInfo.EnterSchool);


                // 學號
                builder.MoveToCell(Global._T_StudBaseInfoIdx, 0, 7, 0);
                builder.Write(studInfo.StudentNumber);

                // 生日
                builder.MoveToCell(Global._T_StudBaseInfoIdx,1,1,0);
                builder.Write(Utility.ConvertDate1(studInfo.Birthday));

                // 身分證
                builder.MoveToCell(Global._T_StudBaseInfoIdx, 1, 3, 0);
                builder.Write(studInfo.ID_Number);

                // 入學年月
                builder.MoveToCell(Global._T_StudBaseInfoIdx, 1,5, 0);
                builder.Write(studInfo.EnterYearMonth);

                // 身分註記
                builder.MoveToCell(Global._T_StudBaseInfoIdx, 2, 1, 0);
                builder.Write(studInfo.Type1);

                studInfo.StudUpdateRecordList = (from data in studInfo.StudUpdateRecordList orderby data.UpdateDate select data).ToList();
                int upRecIdx = 4;
                foreach (StudUpdateRecord sur in studInfo.StudUpdateRecordList)
                {
                    
                    // 日期	類別	核准機關	核准日期	核准文號	縣市	學校名稱
                    
                    builder.MoveToCell(Global._T_StudBaseInfoIdx, upRecIdx, 1, 0);
                    builder.Write(Utility.ConvertDate1(sur.UpdateDate));

                    builder.MoveToCell(Global._T_StudBaseInfoIdx, upRecIdx, 2, 0);
                    builder.Write(sur.UpdateType);

                    builder.MoveToCell(Global._T_StudBaseInfoIdx, upRecIdx, 3, 0);
                    builder.Write(sur.ADGov);

                    builder.MoveToCell(Global._T_StudBaseInfoIdx, upRecIdx,4, 0);                    
                    builder.Write(Utility.ConvertDate1(sur.ADDate));

                    builder.MoveToCell(Global._T_StudBaseInfoIdx, upRecIdx, 5, 0);
                    builder.Write(sur.ADDocNo);

                    builder.MoveToCell(Global._T_StudBaseInfoIdx, upRecIdx, 6, 0);
                    builder.Write(sur.County);

                    builder.MoveToCell(Global._T_StudBaseInfoIdx, upRecIdx, 7, 0);
                    builder.Write(sur.SchoolName);

                    upRecIdx++;
                }
                
                // 照片
                if (!string.IsNullOrEmpty(studInfo.Photo))
                {
                    builder.MoveToCell(Global._T_StudBaseInfoIdx, 4, 8, 0);
                    byte[] photo = Convert.FromBase64String(studInfo.Photo);
                    if (photo != null)
                    {                       
                        Shape photoShape = new Shape(docData, ShapeType.Image);
                        photoShape.ImageData.SetImage(photo);
                        photoShape.WrapType = WrapType.Inline;
                        
                        Cell cell1 = builder.CurrentParagraph.ParentNode as Cell;
                        cell1.CellFormat.LeftPadding = 0;
                        cell1.CellFormat.RightPadding = 0;
                        //photoShape.Height = (cell1.ParentNode as Row).RowFormat.Height;
                        //photoShape.Height = 120;
                        //photoShape.Width = cell1.CellFormat.Width;                        
                        photoShape.Width = ConvertUtil.MillimeterToPoint(35);
                        photoShape.Height = ConvertUtil.MillimeterToPoint(45);
                        builder.InsertNode(photoShape);
                        
                    }

                }
                // 處理成績
                // 學期歷程
                studInfo.StudSemsHistoryList = (from data in studInfo.StudSemsHistoryList orderby data.SchoolYear, data.Semester select data).ToList();
                int sshColIdx = 1;
                int sshdataIdx =-1;
                foreach (StudSemsHistory ssh in studInfo.StudSemsHistoryList)
                {
                    
                    builder.MoveToCell(Global._T_StudScoreInfoIdx,0, sshColIdx, 0);
                    builder.Write(ssh.SchoolYear+"學年度\n第"+ssh.Semester+"學期");                    
                    sshColIdx++;
                    sshdataIdx += 3;
                    ssh.dataStartIdx = sshdataIdx;
                }

                //if (Global._PrintModeString == "Domain")
                //{
                    // 合併欄位使用
                    Dictionary<string,ScoreNameMergeIdx> ScoreNameMergeIdxDict= new Dictionary<string,ScoreNameMergeIdx> ();
                    // 放入領域與科目
                    foreach (StudDomainScore sds in studInfo.StudDomainScoreList)
                        if(!DomainList.Contains(sds.DomainName))
                            DomainList.Add(sds.DomainName);                

                // 依照排序
                foreach(string str in DomainList)
                    foreach(StudDomainScore sds in studInfo.StudDomainScoreList.Where(x=>x.DomainName==str))
                    {
                        ScoreNameMergeIdx snm=null;
                        bool isNew=false;
                        if(ScoreNameMergeIdxDict.ContainsKey(sds.DomainName))
                            snm=ScoreNameMergeIdxDict[sds.DomainName];
                        else
                        {
                            snm=new ScoreNameMergeIdx ();
                            snm.SubNameList = new List<string>();
                            isNew =true;
                        }
                          foreach(StudSubjectScore sss in sds.StudSubjectScoreList)
                                if(!snm.SubNameList.Contains(sss.SubjectName))
                                    snm.SubNameList.Add(sss.SubjectName);
                          if (isNew)
                          {
                              snm.Name = sds.DomainName;                              
                              ScoreNameMergeIdxDict.Add(snm.Name, snm);
                          }
                    }
                    
                    foreach(KeyValuePair<string,ScoreNameMergeIdx> data in ScoreNameMergeIdxDict)
                    {
                        //if(data.Key=="語文" || data.Key=="彈性課程")
                        if(data.Key=="語文")
                        {
                            if (data.Value.SubNameList.Count > 0)
                            {
                                data.Value.isColMerge = false;
                                data.Value.isRowMerge = true;
                                data.Value.MergeColCount = 0;
                                data.Value.MergeRowCount = data.Value.SubNameList.Count;
                            }
                            else
                            {
                                data.Value.isColMerge = true;
                                data.Value.isRowMerge = false;
                                data.Value.MergeColCount = 1;
                                data.Value.MergeRowCount = 0;
                            }
                        }
                        else
                        {
                            data.Value.isColMerge=true;
                            data.Value.isRowMerge=false;
                            data.Value.MergeColCount=1;
                            data.Value.MergeRowCount=0;                            
                        }
                    
                    }

                    // 處理領域成績
                    studInfo.StudDomainScoreList = (from data in studInfo.StudDomainScoreList orderby data.SchoolYear, data.Semester select data).ToList();

                    int scoreRowIdx = 2;
                    List<string> NameList = new List<string>();
                    NameList.Add("語文");
                    foreach (string str in ScoreNameMergeIdxDict.Keys)
                        //if (str != "語文" && str != "彈性課程")
                        if (str != "語文")
                            NameList.Add(str);

          //          NameList.Add("彈性課程");

                    foreach (string Name in NameList)
                    {
                        if (ScoreNameMergeIdxDict.ContainsKey(Name))
                        {
                            ScoreNameMergeIdx snm= ScoreNameMergeIdxDict[Name];
                            // colum 合併
                            if (snm.isColMerge)
                            {
                                Cell left = docData.Sections[0].Body.Tables[Global._T_StudScoreInfoIdx].Rows[scoreRowIdx].Cells[0];
                                Cell right = docData.Sections[0].Body.Tables[Global._T_StudScoreInfoIdx].Rows[scoreRowIdx].Cells[1];
                                Utility.HorizontallyMergeCells(left, right);
                            }
                            
                            // Row 合併
                            if (snm.isRowMerge)
                            {
                                for (int r1 = 1; r1 < snm.MergeRowCount; r1++)
                                {
                                    Cell top = docData.Sections[0].Body.Tables[Global._T_StudScoreInfoIdx].Rows[scoreRowIdx].Cells[0];
                                    Cell bottom = docData.Sections[0].Body.Tables[Global._T_StudScoreInfoIdx].Rows[scoreRowIdx+r1].Cells[0];
                                    Utility.VerticallyMergeCells(top, bottom);

                                     top = docData.Sections[0].Body.Tables[Global._T_StudScoreInfoIdx].Rows[scoreRowIdx].Cells[20];
                                     bottom = docData.Sections[0].Body.Tables[Global._T_StudScoreInfoIdx].Rows[scoreRowIdx + r1].Cells[20];
                                     Utility.VerticallyMergeCells(top, bottom);

                                     top = docData.Sections[0].Body.Tables[Global._T_StudScoreInfoIdx].Rows[scoreRowIdx].Cells[21];
                                     bottom = docData.Sections[0].Body.Tables[Global._T_StudScoreInfoIdx].Rows[scoreRowIdx + r1].Cells[21];
                                     Utility.VerticallyMergeCells(top, bottom);
                                }
                                
                                // 放科目名稱
                                int rowSubj=scoreRowIdx;
                                foreach (string name in snm.SubNameList)
                                {
                                    builder.MoveToCell(Global._T_StudScoreInfoIdx, rowSubj, 1,0);
                                    builder.Write(name);
                                    rowSubj++;
                                }
                            }

                            // 放畢業成績
                            foreach (StudGraduateScore sgs in studInfo.StudGraduateScoreList)
                                if (sgs.Name == Name)
                                {
                                    builder.MoveToCell(Global._T_StudScoreInfoIdx, scoreRowIdx, 20, 0);
                                    builder.Write(Utility.ConvertDecimal1(sgs.Score));

                                    builder.MoveToCell(Global._T_StudScoreInfoIdx, scoreRowIdx, 21, 0);
                                    builder.Write(sgs.Level);

                                    break;
                                }

                            builder.MoveToCell(Global._T_StudScoreInfoIdx, scoreRowIdx, 0, 0);
                            builder.Write(Name);
                            

                            foreach (StudDomainScore sds in studInfo.StudDomainScoreList.Where(x => x.DomainName == Name))
                            {
                                int col = 0;
                                foreach (StudSemsHistory ssh in (from data in studInfo.StudSemsHistoryList where data.SchoolYear == sds.SchoolYear && data.Semester == sds.Semester select data))
                                {
                                    col = ssh.dataStartIdx;

                                    // 當有科目成績
                                    if (sds.StudSubjectScoreList.Count > 0)
                                    {
                                        int rowSubjs = scoreRowIdx;
                                        foreach (string name in snm.SubNameList)
                                        {
                                            foreach (StudSubjectScore sss in (from data in sds.StudSubjectScoreList where data.SubjectName == name && data.SchoolYear == sds.SchoolYear && data.Semester == sds.Semester select data))
                                            {
                                                if (col > 0)
                                                {
                                                    builder.MoveToCell(Global._T_StudScoreInfoIdx, rowSubjs, col, 0);
                                                    builder.Write(Utility.ConvertDecimal1(sss.Period));

                                                    builder.MoveToCell(Global._T_StudScoreInfoIdx, rowSubjs, col + 1, 0);
                                                    builder.Write(Utility.ConvertDecimal1(sss.Score));

                                                    builder.MoveToCell(Global._T_StudScoreInfoIdx, rowSubjs, col + 2, 0);
                                                    builder.Write(sss.Level);
                                                }
                                            }
                                            rowSubjs++;
                                        }
                                    }
                                    else
                                    {
                                        if (col > 0)
                                        {
                                            builder.MoveToCell(Global._T_StudScoreInfoIdx, scoreRowIdx, col, 0);
                                            builder.Write(Utility.ConvertDecimal1(sds.Period));

                                            builder.MoveToCell(Global._T_StudScoreInfoIdx, scoreRowIdx, col + 1, 0);
                                            builder.Write(Utility.ConvertDecimal1(sds.Score));

                                            builder.MoveToCell(Global._T_StudScoreInfoIdx, scoreRowIdx, col + 2, 0);
                                            builder.Write(sds.Level);
                                        }
                                    }
                                }
                            }
                            if (snm.isRowMerge)
                                scoreRowIdx += snm.MergeRowCount;
                            else
                                scoreRowIdx++;
                        }
                    }
                //} // 領域成績

                //// 處理科目成績
                //if (Global._PrintModeString == "Subject")
                //{
                //    int scoreRowIdx = 2;


                //    // 合併欄位使用
                //    Dictionary<string, ScoreNameMergeIdx> ScoreNameMergeIdxDict = new Dictionary<string, ScoreNameMergeIdx>();
                //    // 放入領域與科目
                //    foreach (StudSubjectScore sss in studInfo.StudSubjectScoreList)
                //    {
                //        ScoreNameMergeIdx snm = null;
                //        bool isNew = false;
                //        if (ScoreNameMergeIdxDict.ContainsKey(sss.DomainName))
                //            snm = ScoreNameMergeIdxDict[sss.DomainName];
                //        else
                //        {
                //            snm = new ScoreNameMergeIdx();
                //            snm.SubNameList = new List<string>();
                //            isNew = true;
                //        }                        
                //             snm.SubNameList.Add(sss.SubjectName);
                //        if (isNew)
                //        {
                //            snm.Name = sss.DomainName;
                //            ScoreNameMergeIdxDict.Add(snm.Name, snm);
                //        }
                //    }

                //    foreach (KeyValuePair<string, ScoreNameMergeIdx> data in ScoreNameMergeIdxDict)
                //    {
                //        data.Value.isColMerge = false;
                //        data.Value.isRowMerge = true;
                //        data.Value.MergeColCount = 0;
                //        data.Value.MergeRowCount = data.Value.SubNameList.Count;
                //    }



                //    List<string> subjectNameList = new List<string>();
                //    foreach (StudSubjectScore sss in studInfo.StudSubjectScoreList)
                //        if (!subjectNameList.Contains(sss.SubjectName))
                //            subjectNameList.Add(sss.SubjectName);

                //    int addRow = 0;
                //    if(subjectNameList.Count>15)
                //    {
                //        addRow = subjectNameList.Count - 15;
                //        Table t = docData.Sections[0].Body.Tables[Global._T_StudScoreInfoIdx];
                //        Row r1 = docData.Sections[0].Body.Tables[Global._T_StudScoreInfoIdx].Rows[5];


                //        for (int r = 0; r < addRow; r++)
                //        {
                //            Row newRow = (Row)r1.Clone(true);
                //            t.AppendChild(newRow);
                //        }

                //    }
                //    foreach (string name in subjectNameList)
                //    {
                //        builder.MoveToCell(Global._T_StudScoreInfoIdx, scoreRowIdx, 0, 0);
                //        builder.Write(name);

                //        foreach (StudSubjectScore ss in studInfo.StudSubjectScoreList.Where(x => x.SubjectName == name))
                //        {
                //            int col = 0;
                //            foreach (StudSemsHistory ssh in (from data in studInfo.StudSemsHistoryList where data.SchoolYear == ss.SchoolYear && data.Semester == ss.Semester select data))
                //                col = ssh.dataStartIdx;

                //            if (col > 0)
                //            {
                //                builder.MoveToCell(Global._T_StudScoreInfoIdx, scoreRowIdx, col, 0);
                //                builder.Write(Utility.ConvertDecimal1(ss.Period));

                //                builder.MoveToCell(Global._T_StudScoreInfoIdx, scoreRowIdx, col + 1, 0);
                //                builder.Write(Utility.ConvertDecimal1(ss.Score));

                //                builder.MoveToCell(Global._T_StudScoreInfoIdx, scoreRowIdx, col + 2, 0);
                //                builder.Write(ss.Level);
                //            }
                //        }
                //        scoreRowIdx++;
                //    }
                //}

                // 處理學期成績
                foreach (StudSemsScore sss in studInfo.StudSemsScoreList)
                {
                    int col = 0;
                    foreach (StudSemsHistory ssh in (from data in studInfo.StudSemsHistoryList where data.SchoolYear == sss.SchoolYear && data.Semester == sss.Semester select data))
                        col = ssh.dataStartIdx;

                    if (col > 0)
                    {
                        builder.MoveToCell(Global._T_StudScoreExInfoIdx, 0, col-1, 0);
                        builder.Write(Utility.ConvertDecimal1(sss.Period));

                        builder.MoveToCell(Global._T_StudScoreExInfoIdx, 0, col , 0);
                        builder.Write(Utility.ConvertDecimal1(sss.Score));

                        builder.MoveToCell(Global._T_StudScoreExInfoIdx, 0, col + 1, 0);
                        builder.Write(sss.Level);
                    }
                
                }
                foreach(StudGraduateScore sgs in studInfo.StudGraduateScoreList)
                    if (sgs.Name == "學習領域")
                    {
                        builder.MoveToCell(Global._T_StudScoreExInfoIdx, 0, 19, 0);
                        builder.Write(Utility.ConvertDecimal1(sgs.Score));

                        builder.MoveToCell(Global._T_StudScoreExInfoIdx, 0, 20, 0);
                        builder.Write(sgs.Level);

                        break;
                    }

                // 處理缺曠
                
                int ci11 = 1, ci12 = 2, ci21 = 3, ci22 = 4, ci31 = 5, ci32 = 6;
                foreach (StudSemsHistory ssh in studInfo.StudSemsHistoryList)
                {
                    if ((ssh.GradeYear == 1 || ssh.GradeYear == 7) && ssh.Semester == 1)
                    {
                        builder.MoveToCell(Global._T_StudAbsentInfoIdx, 2, ci11, 0);
                        if (ssh.SchoolDayCount.HasValue)
                            builder.Write(ssh.SchoolDayCount.Value.ToString());
                    }

                    if ((ssh.GradeYear == 1 || ssh.GradeYear == 7) && ssh.Semester == 2)
                    {
                        builder.MoveToCell(Global._T_StudAbsentInfoIdx, 2, ci12, 0);
                        if (ssh.SchoolDayCount.HasValue)
                            builder.Write(ssh.SchoolDayCount.Value.ToString());
                    }
                    if ((ssh.GradeYear == 2 || ssh.GradeYear == 8) && ssh.Semester == 1)
                    {
                        builder.MoveToCell(Global._T_StudAbsentInfoIdx, 2, ci21, 0);
                        if (ssh.SchoolDayCount.HasValue)
                            builder.Write(ssh.SchoolDayCount.Value.ToString());
                    }

                    if ((ssh.GradeYear == 2 || ssh.GradeYear == 8) && ssh.Semester == 2)
                    {
                        builder.MoveToCell(Global._T_StudAbsentInfoIdx, 2, ci22, 0);
                        if (ssh.SchoolDayCount.HasValue)
                            builder.Write(ssh.SchoolDayCount.Value.ToString());
                    }

                    if ((ssh.GradeYear == 3 || ssh.GradeYear == 9) && ssh.Semester == 1)
                    {
                        builder.MoveToCell(Global._T_StudAbsentInfoIdx, 2, ci31, 0);
                        if (ssh.SchoolDayCount.HasValue)
                            builder.Write(ssh.SchoolDayCount.Value.ToString());
                    }

                    if ((ssh.GradeYear == 3 || ssh.GradeYear == 9) && ssh.Semester == 2)
                    {
                        builder.MoveToCell(Global._T_StudAbsentInfoIdx, 2, ci32, 0);
                        if (ssh.SchoolDayCount.HasValue)
                            builder.Write(ssh.SchoolDayCount.Value.ToString());
                    }


                    List<int> sumList = new List<int>();
                    foreach (StudAbsenceCount sac in (from data in studInfo.StudAbsenceCountList where data.SchoolYear == ssh.SchoolYear && data.Semester == ssh.Semester select data))
                    {
                        if ((ssh.GradeYear == 1 || ssh.GradeYear == 7) && ssh.Semester == 1)
                        {
                            sumList.Clear();
                            for(int i=0;i<5;i++)
                                sumList.Add(0);                            

                            foreach (KeyValuePair<string,Dictionary<string,int>> da in sac.AbsenceCount)
                                foreach (KeyValuePair<string, int> da1 in da.Value)
                                {                            
                                    if(da1.Key=="事假")
                                        sumList [0]+= da1.Value;
                                    else if(da1.Key=="病假")
                                        sumList[1] += da1.Value;
                                    else if(da1.Key=="曠課")
                                        sumList[2] += da1.Value;
                                    else
                                        sumList[3] += da1.Value;

                                    sumList[4] += da1.Value;
                                }

                            for (int row = 3; row <= 7; row++)
                            {
                                builder.MoveToCell(Global._T_StudAbsentInfoIdx, row, ci11, 0);
                                if(sumList[row-3]>0)
                                    builder.Write(sumList[row-3].ToString());
                            }                                 
                        }

                        if ((ssh.GradeYear == 1 || ssh.GradeYear == 7) && ssh.Semester == 2)
                        {                           
                            sumList.Clear();
                            for (int i = 0; i < 5; i++)
                                sumList.Add(0);

                            foreach (KeyValuePair<string, Dictionary<string, int>> da in sac.AbsenceCount)
                                foreach (KeyValuePair<string, int> da1 in da.Value)
                                {
                                    if (da1.Key == "事假")
                                        sumList[0] += da1.Value;
                                    else if (da1.Key == "病假")
                                        sumList[1] += da1.Value;
                                    else if (da1.Key == "曠課")
                                        sumList[2] += da1.Value;
                                    else
                                        sumList[3] += da1.Value;

                                    sumList[4] += da1.Value;
                                }

                            for (int row = 3; row <= 7; row++)
                            {
                                builder.MoveToCell(Global._T_StudAbsentInfoIdx, row, ci12, 0);
                                if (sumList[row - 3] > 0)
                                    builder.Write(sumList[row - 3].ToString());
                            }          
                        }

                        if ((ssh.GradeYear == 2 || ssh.GradeYear == 8) && ssh.Semester == 1)
                        {
                            sumList.Clear();
                            for (int i = 0; i < 5; i++)
                                sumList.Add(0);

                            foreach (KeyValuePair<string, Dictionary<string, int>> da in sac.AbsenceCount)
                                foreach (KeyValuePair<string, int> da1 in da.Value)
                                {
                                    if (da1.Key == "事假")
                                        sumList[0] += da1.Value;
                                    else if (da1.Key == "病假")
                                        sumList[1] += da1.Value;
                                    else if (da1.Key == "曠課")
                                        sumList[2] += da1.Value;
                                    else
                                        sumList[3] += da1.Value;

                                    sumList[4] += da1.Value;
                                }

                            for (int row = 3; row <= 7; row++)
                            {
                                builder.MoveToCell(Global._T_StudAbsentInfoIdx, row, ci21, 0);
                                if (sumList[row - 3] > 0)
                                    builder.Write(sumList[row - 3].ToString());
                            }          

                        }

                        if ((ssh.GradeYear == 2 || ssh.GradeYear == 8) && ssh.Semester == 2)
                        {
                            sumList.Clear();
                            for (int i = 0; i < 5; i++)
                                sumList.Add(0);

                            foreach (KeyValuePair<string, Dictionary<string, int>> da in sac.AbsenceCount)
                                foreach (KeyValuePair<string, int> da1 in da.Value)
                                {
                                    if (da1.Key == "事假")
                                        sumList[0] += da1.Value;
                                    else if (da1.Key == "病假")
                                        sumList[1] += da1.Value;
                                    else if (da1.Key == "曠課")
                                        sumList[2] += da1.Value;
                                    else
                                        sumList[3] += da1.Value;

                                    sumList[4] += da1.Value;
                                }

                            for (int row = 3; row <= 7; row++)
                            {
                                builder.MoveToCell(Global._T_StudAbsentInfoIdx, row, ci22, 0);
                                if (sumList[row - 3] > 0)
                                    builder.Write(sumList[row - 3].ToString());
                            }          
                        }

                        if ((ssh.GradeYear == 3 || ssh.GradeYear == 9) && ssh.Semester == 1)
                        {
                            sumList.Clear();
                            for (int i = 0; i < 5; i++)
                                sumList.Add(0);

                            foreach (KeyValuePair<string, Dictionary<string, int>> da in sac.AbsenceCount)
                                foreach (KeyValuePair<string, int> da1 in da.Value)
                                {
                                    if (da1.Key == "事假")
                                        sumList[0] += da1.Value;
                                    else if (da1.Key == "病假")
                                        sumList[1] += da1.Value;
                                    else if (da1.Key == "曠課")
                                        sumList[2] += da1.Value;
                                    else
                                        sumList[3] += da1.Value;

                                    sumList[4] += da1.Value;
                                }

                            for (int row = 3; row <= 7; row++)
                            {
                                builder.MoveToCell(Global._T_StudAbsentInfoIdx, row, ci31, 0);
                                if (sumList[row - 3] > 0)
                                    builder.Write(sumList[row - 3].ToString());
                            }          

                        }

                        if ((ssh.GradeYear == 3 || ssh.GradeYear == 9) && ssh.Semester == 2)
                        {
                            sumList.Clear();
                            for (int i = 0; i < 5; i++)
                                sumList.Add(0);

                            foreach (KeyValuePair<string, Dictionary<string, int>> da in sac.AbsenceCount)
                                foreach (KeyValuePair<string, int> da1 in da.Value)
                                {
                                    if (da1.Key == "事假")
                                        sumList[0] += da1.Value;
                                    else if (da1.Key == "病假")
                                        sumList[1] += da1.Value;
                                    else if (da1.Key == "曠課")
                                        sumList[2] += da1.Value;
                                    else
                                        sumList[3] += da1.Value;

                                    sumList[4] += da1.Value;
                                }

                            for (int row = 3; row <= 7; row++)
                            {
                                builder.MoveToCell(Global._T_StudAbsentInfoIdx, row, ci32, 0);
                                if (sumList[row - 3] > 0)
                                    builder.Write(sumList[row - 3].ToString());
                            }          
                        }


                    }
                }

                int col1 = 2;
                int col2 = 3;
                // 處理日常生活具體建議
                foreach (StudSemsHistory ssh in studInfo.StudSemsHistoryList)
                {
                    foreach(StudTextScore sts in (from data in studInfo.StudTextScoreList where data.SchoolYear== ssh.SchoolYear && data.Semester==ssh.Semester select data))
                    {
                        // 一上
                        if ((ssh.GradeYear == 1 || ssh.GradeYear == 7) && ssh.Semester == 1)
                        {
                            builder.MoveToCell(Global._T_StudPerformanceInfoIdx, 1, col1, 0);
                            builder.Write(sts.DailyLifeRecommend);
                            builder.MoveToCell(Global._T_StudPerformanceInfoIdx, 1, col2, 0);
                            builder.Write(ssh.ClassTeacher);
                        }

                        // 一下
                        if ((ssh.GradeYear == 1 || ssh.GradeYear == 7) && ssh.Semester == 2)
                        {
                            builder.MoveToCell(Global._T_StudPerformanceInfoIdx, 2, col1, 0);
                            builder.Write(sts.DailyLifeRecommend);
                            builder.MoveToCell(Global._T_StudPerformanceInfoIdx, 2, col2, 0);
                            builder.Write(ssh.ClassTeacher);
                        }

                        // 二上
                        if ((ssh.GradeYear == 2 || ssh.GradeYear == 8) && ssh.Semester == 1)
                        {
                            builder.MoveToCell(Global._T_StudPerformanceInfoIdx, 3, col1, 0);
                            builder.Write(sts.DailyLifeRecommend);
                            builder.MoveToCell(Global._T_StudPerformanceInfoIdx, 3, col2, 0);
                            builder.Write(ssh.ClassTeacher);
                        }

                        // 二下
                        if ((ssh.GradeYear == 2 || ssh.GradeYear == 8) && ssh.Semester == 2)
                        {
                            builder.MoveToCell(Global._T_StudPerformanceInfoIdx, 4, col1, 0);
                            builder.Write(sts.DailyLifeRecommend);
                            builder.MoveToCell(Global._T_StudPerformanceInfoIdx, 4, col2, 0);
                            builder.Write(ssh.ClassTeacher);
                        }

                        // 三上
                        if ((ssh.GradeYear == 3 || ssh.GradeYear == 9) && ssh.Semester == 1)
                        {
                            builder.MoveToCell(Global._T_StudPerformanceInfoIdx, 5, col1, 0);
                            builder.Write(sts.DailyLifeRecommend);
                            builder.MoveToCell(Global._T_StudPerformanceInfoIdx, 5, col2, 0);
                            builder.Write(ssh.ClassTeacher);
                        }

                        // 三下
                        if ((ssh.GradeYear == 3 || ssh.GradeYear == 9) && ssh.Semester == 2)
                        {
                            builder.MoveToCell(Global._T_StudPerformanceInfoIdx, 6, col1, 0);
                            builder.Write(sts.DailyLifeRecommend);
                            builder.MoveToCell(Global._T_StudPerformanceInfoIdx, 6, col2, 0);
                            builder.Write(ssh.ClassTeacher);
                        }   

                    }
                }



                // 承辦人員
                // 業務承辦人
                builder.MoveToCell(Global._T_StudContractorsIdx, 0, 1, 0);
                builder.Write(Global._BusinessContractor);

                // 註冊組長
                builder.MoveToCell(Global._T_StudContractorsIdx, 0, 3, 0);
                builder.Write(Global._RegisteredLeader);

                // 教務主任
                builder.MoveToCell(Global._T_StudContractorsIdx, 0, 5, 0);                
                builder.Write(JHSchoolInfo.EduDirectorName);

                //校長
                builder.MoveToCell(Global._T_StudContractorsIdx, 0, 7, 0);
                builder.Write(JHSchoolInfo.ChancellorChineseName);


                doc.Sections.Add(doc.ImportNode(docData.Sections[0], true));
            }




            string _FilePathAndName = Application.StartupPath + "\\Reports\\學籍表.doc";
            // 當沒有設定檔案名稱
            if (string.IsNullOrEmpty(_FilePathAndName))
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = "學籍表.doc";
                sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        doc.Save(sd.FileName, Aspose.Words.SaveFormat.Doc);                        
                            System.Diagnostics.Process.Start(sd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);                        
                        return;
                    }
                }
            }
            else
            {
                try
                {
                    doc.Save(_FilePathAndName, SaveFormat.Doc);                    
                        System.Diagnostics.Process.Start(_FilePathAndName);
                }
                catch
                {
                    SaveFileDialog sd = new SaveFileDialog();
                    sd.Title = "另存新檔";
                    sd.FileName = "學籍表.doc";
                    sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                    if (sd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            doc.Save(sd.FileName, Aspose.Words.SaveFormat.Doc);
                            System.Diagnostics.Process.Start(sd.FileName);
                        }
                        catch
                        {
                            MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);                            
                            return;
                        }
                    }
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                btnPrint.Enabled = false;
                _config.SetString("註冊組長", txtRegisteredLeader.Text);
                _config.SetString("業務承辦", txtBusinessContractor.Text);
                Global._BusinessContractor = txtBusinessContractor.Text;
                Global._RegisteredLeader = txtRegisteredLeader.Text;
                _config.Save();
                Global._PrintModeString = _config.GetString("領域科目設定", string.Empty);
                if (string.IsNullOrEmpty(Global._PrintModeString))
                    Global._PrintModeString = "Domain";

                _bkWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show(ex.Message);
                return;
            }
        }

        private void lnkHDay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ConfigForm.SelectTypeForm form = new ConfigForm.SelectTypeForm(Global.ReportName);
            form.ShowDialog();
        }

        private void lnkPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ConfigForm.PrintConfigForm form = new ConfigForm.PrintConfigForm();
            form.ShowDialog();
        }
    }
}
