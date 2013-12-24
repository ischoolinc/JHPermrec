using System;
using System.Collections.Generic;
using System.Text;
using JHSchool.Data;
using System.Threading;
using SmartSchool.API.PlugIn;
using Framework;

namespace JHPermrec.UpdateRecord.ImportExport
{
    class ImportUpdateRecCode01:SmartSchool.API.PlugIn.Import.Importer
    {
        bool checkHasData = false;

        public ImportUpdateRecCode01()
        {
            this.Image = null;
            this.Text = "匯入新生異動";        
        }

        public override void InitializeImport(SmartSchool.API.PlugIn.Import.ImportWizard wizard)
        {
            // 取得學生資料
            Dictionary<string, JHStudentRecord> Students = new Dictionary<string, JHStudentRecord>();            

            // 取得異動資料
            Dictionary<string, List<JHUpdateRecordRecord>> UpdateRecs = new Dictionary<string, List<JHUpdateRecordRecord>>();
            wizard.PackageLimit = 3000;
            //wizard.ImportableFields.AddRange("異動類別", "學年度", "學期", "異動年級", "異動日期", "入學年月", "備註", "畢業國小", "異動班級", "異動姓名", "異動身分證號", "異動地址", "異動學號", "異動性別", "異動生日", "核准日期", "核准文號", "入學資格-畢業國小名稱");
            //wizard.RequiredFields.AddRange("異動類別", "異動日期", "學年度", "學期");
            wizard.ImportableFields.AddRange("學年度", "學期", "異動年級", "異動日期", "入學年月", "備註", "畢業國小", "異動班級", "異動姓名", "異動身分證號", "異動地址", "異動學號", "異動性別", "異動生日", "核准日期", "核准文號", "入學資格-畢業國小名稱");
            wizard.RequiredFields.AddRange("異動日期", "學年度", "學期");
            wizard.ValidateStart += delegate(object sender, SmartSchool.API.PlugIn.Import.ValidateStartEventArgs e)
            {
                Students.Clear();
                UpdateRecs.Clear();                

                // 取得學生資料
                foreach (JHStudentRecord studRec in JHStudent.SelectByIDs(e.List))
                    if(!Students.ContainsKey(studRec.ID))
                        Students.Add(studRec.ID, studRec);

                foreach (string str in Students.Keys)
                {
                    List<JHUpdateRecordRecord> UpdRecList = new List<JHUpdateRecordRecord>();
                    UpdateRecs.Add(str, UpdRecList);
                }

                // 取得異動
                MultiThreadWorker<string> loader1 = new MultiThreadWorker<string>();
                loader1.MaxThreads = 3;
                loader1.PackageSize = 250;
                loader1.PackageWorker += delegate(object sender1, PackageWorkEventArgs<string> e1)
                {
                    foreach (JHUpdateRecordRecord UpdRec in JHUpdateRecord.SelectByStudentIDs(e.List))
                    {
                        // 新生
                        if (UpdRec.UpdateCode == "1")
                            if (UpdateRecs.ContainsKey(UpdRec.StudentID))
                                UpdateRecs[UpdRec.StudentID].Add(UpdRec);
                 
                    }                
                };
                loader1.Run(e.List);
            };
            
            wizard.ValidateRow += delegate(object sender, SmartSchool.API.PlugIn.Import.ValidateRowEventArgs e) 
            {
                int i=0;
                
                DateTime dt;
                // 檢查學生是否存在
                JHStudentRecord studRec = null;
                if (Students.ContainsKey(e.Data.ID))
                    studRec = Students[e.Data.ID];
                else
                {
                    e.ErrorMessage = "沒有這位學生" + e.Data.ID;
                    return;
                }

                // 驗證格式資料
                bool InputFormatPass = true;
                checkHasData = false;

                foreach (string field in e.SelectFields)
                {
                    string value = e.Data[field].Trim();

                    //// 驗證$無法匯入
                    //if (value.IndexOf('$') > -1)
                    //{
                    //    e.ErrorFields.Add(field, "儲存格有$無法匯入.");
                    //    break;
                    //}

                    // 檢查是否已經有新生異動
                    if (checkHasData == false)
                    if (UpdateRecs.ContainsKey(e.Data.ID))
                        if (UpdateRecs[e.Data.ID].Count > 0)
                        {
                            e.WarningFields.Add(field, "系統內已有新生異動,匯入將會取代系統內新生異動");
                            checkHasData = true;
                        }


                    switch (field)
                    { 
                        default:
                            break;

                        //case "異動類別":
                        //    if (value != "新生")
                        //    {
                        //        InputFormatPass &= false;
                        //        e.ErrorFields.Add(field, "必須填入新生");
                        //    }

                            //if (value == "新生")
                            //{
                                //if (UpdateRecs.ContainsKey(e.Data.ID))
                                //    if (UpdateRecs[e.Data.ID].Count > 0)
                                //    {
                                //        e.WarningFields.Add(field, "系統內已有新生異動,匯入將會取代系統內新生異動");
                                //    }
                            //}
                            //break;

                        case "學年度":
                            int.TryParse(value, out i);
                            if (string.IsNullOrEmpty(value) || i <1)
                            {
                                InputFormatPass &= false;
                                e.ErrorFields.Add(field, "必須填入整數");

                            }

                            break;
                        case "學期":
                            int.TryParse(value, out i);
                            if (string.IsNullOrEmpty(value) || i< 1)
                            {
                                InputFormatPass &= false;
                                e.ErrorFields.Add(field, "必須填入整數");

                            }

                            if (i > 2)
                            {

                                InputFormatPass &= false;
                                e.ErrorFields.Add(field, "必須填入整數,1或2");
                            }

                            break;

                        case "異動年級":
                            int.TryParse(value, out i);
                            if (string.IsNullOrEmpty(value) || i < 1)
                            {
                                InputFormatPass &= false;
                                e.WarningFields.Add(field, "請填入整數");
                            }
                            break;

                        case "異動日期":
                            if (string.IsNullOrEmpty(value) || DateTime.TryParse(value, out dt) == false)
                            {
                                InputFormatPass &= false;
                                e.ErrorFields.Add(field, "必須填入日期，例如2009/1/1");
                            }
                            break;

                        case "入學年月":
                            int.TryParse(value, out i);
                            if (!string.IsNullOrEmpty(value))
                            if (i < 1 )
                            {                                
                                e.WarningFields.Add(field, "必須填入年月格式，例如200901");                            
                            }
                            break;
                        case "異動生日":
                            if (!string.IsNullOrEmpty(value))
                            if (DateTime.TryParse(value, out dt) == false)
                            {
                                InputFormatPass &= false;
                                e.ErrorFields.Add(field, "必須填入日期，例如2009/1/1");
                            }
                            break;

                        case "核准日期":
                            if(!string.IsNullOrEmpty(value))
                            if (DateTime.TryParse(value, out dt) == false)
                            {
                                InputFormatPass &= false;
                                e.ErrorFields.Add(field, "必須填入日期，例如2009/1/1");
                            }
                            break;

                        case "異動性別":
                            if (value == "男" || value == "女" || value == "")
                            { }
                            else
                                e.WarningFields.Add(field, "請填入男或女");
                            break;
                    }                
                }
            
            };


            wizard.ImportPackage += delegate(object sender, SmartSchool.API.PlugIn.Import.ImportPackageEventArgs e)
            {
                Dictionary<string, List<RowData>> id_Rows = new Dictionary<string, List<RowData>>();
                foreach (RowData data in e.Items)
                {
                    if (!id_Rows.ContainsKey(data.ID))
                        id_Rows.Add(data.ID,new List<RowData> ());
                    id_Rows[data.ID].Add(data);
                }

                List<JHUpdateRecordRecord> InsertList = new List<JHUpdateRecordRecord>();
                List<JHUpdateRecordRecord> UpdateList = new List<JHUpdateRecordRecord>();
                List<JHUpdateRecordRecord> DelList = new List<JHUpdateRecordRecord>();



                foreach (string id in id_Rows.Keys)
                {
                    int schoolYear, Semester;
                    string GrYear="";
                    DateTime dt;

                    if (!UpdateRecs.ContainsKey(id))
                        continue;

                    foreach (RowData data in id_Rows[id])
                    {

                        int.TryParse(data["學年度"], out schoolYear);
                        int.TryParse(data["學期"], out Semester);
                        DateTime.TryParse(data["異動日期"], out dt);
                        if (data.ContainsKey("異動年級"))
                            GrYear = data["異動年級"];
                        JHUpdateRecordRecord updateRec=null ;
                        foreach (JHUpdateRecordRecord urr in UpdateRecs[id])
                        {
                            if (urr.UpdateCode == "1")
                                DelList.Add(urr);
                            if (urr.SchoolYear == schoolYear && urr.Semester == Semester && urr.GradeYear == GrYear)
                            {
                                DateTime dt1;
                                DateTime.TryParse(urr.UpdateDate, out dt1);
                                if (dt == dt1)
                                    updateRec = urr;
                            }
                        }
                        bool isInsert =true;

                        if (updateRec == null)
                        {
                            updateRec = new JHUpdateRecordRecord();
                            updateRec.StudentID = id;                            
                        }
                        else
                            isInsert = false;

                        // 檢查系統內是否已有資料，如果有使用新增方式
                        if (checkHasData)
                            isInsert = true;
                        
                        // 新生異動代碼
                        updateRec.UpdateCode = "1";

                            bool checkData = false;
                            foreach (string field in e.ImportFields)
                            {
                                string value = data[field].Trim();                                
                                //checkData = true;
                                switch (field)
                                {
                                    //case "異動類別":
                                    //    if (value == "新生")
                                    //    {
                                    //        updateRec.UpdateCode = "1";
                                    //        checkData = true;
                                    //    }
                                    //    break;
                                    case "學年度":
                                        int scYear;
                                        if (int.TryParse(value, out scYear))
                                        {
                                            updateRec.SchoolYear = scYear;
                                            checkData = true;
                                        }
                                        break;

                                    case "學期":
                                        int Sems;
                                        if (int.TryParse(value, out Sems))
                                        {
                                            updateRec.Semester = Sems;
                                            checkData = true;
                                        }
                                        break;
                                    case "異動年級":
                                        updateRec.GradeYear = GrYear;
                                        break;
                                    case "異動日期":
                                        DateTime dtd;
                                        if (DateTime.TryParse(value, out dtd))
                                            updateRec.UpdateDate = dtd.ToShortDateString();
                                        break;
                                    case "入學年月":
//                                        if (value.Length == 6)
                                        if (string.IsNullOrEmpty(value))
                                            updateRec.EnrollmentSchoolYear = string.Empty;
                                        else
                                            updateRec.EnrollmentSchoolYear = value;
                                        break;
                                    case "備註":
                                        updateRec.Comment = value;
                                        break;
                                    case "畢業國小":
                                        updateRec.GraduateSchool = value;
                                        break;
                                    case "入學資格-畢業國小名稱":
                                        updateRec.GraduateSchool = value;
                                        break;
                                        
                                    case "異動班級":
                                        updateRec.OriginClassName = value;
                                        break;

                                    case "異動姓名":
                                        updateRec.StudentName = value;
                                        break;

                                    case "異動身分證號":
                                        updateRec.IDNumber = value;
                                        break;

                                    case "異動地址":
                                        updateRec.OriginAddress = value;
                                        break;

                                    case "異動學號":
                                        updateRec.StudentNumber = value;
                                        break;

                                    case "異動性別":
                                        if (value == "男" || value == "女" || value == "")
                                            updateRec.Gender = value;
                                            break;

                                    case "異動生日":
                                        DateTime dtb;
                                        if(DateTime.TryParse(value,out dtb))
                                            updateRec.Birthdate = dtb.ToShortDateString();
                                        break;

                                    case "核准日期":
                                        DateTime dtAd;
                                        if (DateTime.TryParse(value, out dtAd))
                                            updateRec.ADDate = dtAd.ToShortDateString();
                                        break;

                                    case "核准文號":
                                        updateRec.ADNumber = value;
                                        break;                                
                                }                            
                            }

                            if (string.IsNullOrEmpty(updateRec.StudentID) || string.IsNullOrEmpty(updateRec.UpdateDate) || string.IsNullOrEmpty(updateRec.UpdateCode))
                                continue;
                            else
                            {
                                if (isInsert)
                                    InsertList.Add(updateRec);
                                else
                                    UpdateList.Add(updateRec);                            
                            }
                    }
                
                }

                try                
                {
                    // 先清空舊DelList
                    if (DelList.Count > 0)
                        Delete(DelList);


                    if (InsertList.Count > 0)
                        Insert(InsertList);

                    if (UpdateList.Count > 0)
                        Update(UpdateList);


                    JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
                    prlp.SaveLog("學生.匯入異動", "匯入新生異動","匯入新生異動：共新增"+InsertList.Count +"筆資料,共更新:"+UpdateList.Count +"筆資料");
                    JHSchool.Student.Instance.SyncAllBackground();
                    
                }
                catch (Exception ex){}

            
            };
        }

        private void Update(object item)
        {
            try
            {
                List<JHUpdateRecordRecord> UpdatePackage =(List<JHUpdateRecordRecord>)item;

                JHUpdateRecord.Update(UpdatePackage);

            }
            catch (Exception ex){}
        
        }

        private void Insert(object item)
        {
            try
            {
                List<JHUpdateRecordRecord> InsertPackage =(List<JHUpdateRecordRecord>)item;
                JHUpdateRecord.Insert(InsertPackage);
            }
            catch (Exception ex) 
            {
                FISCA.Presentation.Controls.MsgBox.Show("新增資料發生異常.");
            }            
        }

        private void Delete(object item)
        {
            try
            {
                List<JHUpdateRecordRecord> DelPackage = (List<JHUpdateRecordRecord>)item;
                JHUpdateRecord.Delete(DelPackage);
            }
            catch (Exception ex) 
            {
                FISCA.Presentation.Controls.MsgBox.Show("刪除資料發生異常.");
            }
        }  
    }
}
