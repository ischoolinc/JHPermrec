using System;
using System.Collections.Generic;
using System.Text;
using JHSchool.Data;
using System.Threading;
using SmartSchool.API.PlugIn;
using Framework;

namespace JHPermrec.UpdateRecord.ImportExport
{
    class ImportUpdateRecCode02 : SmartSchool.API.PlugIn.Import.Importer
    {
        bool CheckHasData = false;
        public ImportUpdateRecCode02()
        {
            this.Image = null;
            this.Text = "匯入畢業異動";        
        }

        public override void  InitializeImport(SmartSchool.API.PlugIn.Import.ImportWizard wizard)
        {
                        // 取得學生資料
            Dictionary<string, JHStudentRecord> Students = new Dictionary<string, JHStudentRecord>();            

            // 取得異動資料
            Dictionary<string, List<JHUpdateRecordRecord>> UpdateRecs = new Dictionary<string, List<JHUpdateRecordRecord>>();
            wizard.PackageLimit = 3000;
            //wizard.ImportableFields.AddRange("學年度", "學期", "異動年級", "異動日期", "入學年月", "畢業年月", "畢修業別", "備註", "學籍核准日期", "學籍核准文號", "畢業證書字號", "異動班級", "異動姓名", "異動身分證號", "異動出生地", "異動學號", "異動性別", "異動生日", "核准日期", "核准文號", "異動類別", "畢(結)業證書字號");
            //wizard.RequiredFields.AddRange("異動類別", "異動日期", "學年度", "學期");
            wizard.ImportableFields.AddRange("學年度", "學期", "異動年級", "異動日期", "入學年月", "畢業年月", "畢修業別", "備註", "學籍核准日期", "學籍核准文號", "畢業證書字號", "異動班級", "異動姓名", "異動身分證號", "異動出生地", "異動學號", "異動性別", "異動生日", "核准日期", "核准文號","畢(結)業證書字號");
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
                        // 畢業
                        if (UpdRec.UpdateCode == "2")
                        {
                            if (UpdateRecs.ContainsKey(UpdRec.StudentID))
                                UpdateRecs[UpdRec.StudentID].Add(UpdRec);
                        }                        
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
                CheckHasData = false;



                foreach (string field in e.SelectFields)
                {
                    string value = e.Data[field].Trim();

                    //// 驗證$無法匯入
                    //if (value.IndexOf('$') > -1)
                    //{
                    //    e.ErrorFields.Add(field, "儲存格有$無法匯入.");
                    //    break;
                    //}

                    // 檢查系統內是否已經有畢業異動
                    if(CheckHasData ==false )
                    if (UpdateRecs.ContainsKey(e.Data.ID))
                        if (UpdateRecs[e.Data.ID].Count > 0)
                        {
                            e.WarningFields.Add(field, "系統內已有畢業異動,匯入將會取代系統內畢業異動");
                            CheckHasData = true;
                        }

                    switch (field)
                    {
                        default:
                            break;

                        //case "異動類別":
                        //    if (value != "畢業")
                        //    {
                        //        InputFormatPass &= false;
                        //        e.ErrorFields.Add(field, "必須填入畢業");
                        //    }
                        //    if (value == "畢業")
                        //    {
                        //        if (UpdateRecs.ContainsKey(e.Data.ID))
                        //            if (UpdateRecs[e.Data.ID].Count > 0)
                        //            {
                        //                e.WarningFields.Add(field, "系統內已有畢業異動,匯入將會取代系統內畢業異動");
                        //            }
                        //    }                            
                        //    break;

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
                            if (!string.IsNullOrEmpty(value) && i < 1 )
                            {                                
                                e.WarningFields.Add(field, "必須填入年月格式，例如200901");                            
                            }
                            break;

                        case "畢業年月":
                            int.TryParse(value, out i);
                            if (!string.IsNullOrEmpty(value) && i < 1)
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

                        case "學籍核准日期":
                            if (!string.IsNullOrEmpty(value))
                            if (DateTime.TryParse(value, out dt) == false)
                            {
                                InputFormatPass &= false;
                                e.ErrorFields.Add(field, "必須填入日期，例如2009/1/1");
                            }
                            break;   

                        case "核准日期":
                            if (!string.IsNullOrEmpty(value))
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
                    
                    foreach (RowData data in id_Rows[id])
                    {
                        if (!UpdateRecs.ContainsKey(id))
                            continue;
                        int.TryParse(data["學年度"], out schoolYear);
                        int.TryParse(data["學期"], out Semester);
                        DateTime.TryParse(data["異動日期"], out dt);
                        if (data.ContainsKey("異動年級"))
                            GrYear = data["異動年級"];
                        JHUpdateRecordRecord updateRec=null ;
                        foreach (JHUpdateRecordRecord urr in UpdateRecs[id])
                        {
                            if (urr.UpdateCode == "2")
                                DelList.Add(urr);
                            //if (urr.SchoolYear == schoolYear && urr.Semester == Semester && urr.GradeYear == GrYear)
                            //{
                            //    DateTime dt1;
                            //    DateTime.TryParse(urr.UpdateDate, out dt1);
                            //    if (dt == dt1)                                
                            //        updateRec = urr;                                
                            //}
                        }
                        bool isInsert =true;

                        if (updateRec == null)
                        {
                            updateRec = new JHUpdateRecordRecord();
                            updateRec.StudentID = id;
                        }
                        else
                            isInsert = false;

                        updateRec.UpdateCode = "2";

                        // 當已經有畢業異動使用新增方式
                        if (CheckHasData)
                            isInsert = true;

                            bool checkData = false;
                            foreach (string field in e.ImportFields)
                            {
                                string value = data[field].Trim();
                                
                                switch (field)
                                {
                                    //case "異動類別":
                                    //    if (value == "畢業")
                                    //    {
                                    //        updateRec.UpdateCode = "2";
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
                                        if (string.IsNullOrEmpty(value))
                                            updateRec.EnrollmentSchoolYear = string.Empty;
                                        else
                                            updateRec.EnrollmentSchoolYear = value;
                                        break;
                                    case "畢業年月":
                                        if (string.IsNullOrEmpty(value))
                                            updateRec.GraduateSchoolYear = string.Empty;
                                        else
                                            updateRec.GraduateSchoolYear = value;
                                        break;

                                    case "畢修業別":
                                            updateRec.Graduate = value;
                                        break;

                                    case "備註":
                                        updateRec.Comment = value;
                                        break;

                                    case "學籍核准日期":
                                        DateTime dtLD;
                                        if (DateTime.TryParse(value, out dtLD))
                                            updateRec.LastADDate = dtLD.ToShortDateString();
                                        break;

                                    case "學籍核准文號":
                                        updateRec.LastADNumber = value;
                                        break;

                                    case "畢業證書字號":
                                        updateRec.GraduateCertificateNumber = value;
                                        break;
                                    case "畢(結)業證書字號":
                                        updateRec.GraduateCertificateNumber = value;
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

                                    case "異動出生地":
                                        updateRec.BirthPlace = value;
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
                    // 先清空舊
                    if (DelList.Count > 0)
                        Delete(DelList);

                    if (InsertList.Count > 0)
                        Insert(InsertList);

                    if (UpdateList.Count > 0)
                        Update(UpdateList);


                    JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
                    prlp.SaveLog("學生.匯入異動", "匯入畢業異動","匯入畢業異動：共新增"+InsertList.Count +"筆資料,共更新:"+UpdateList.Count +"筆資料");
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
            catch (Exception ex) { FISCA.Presentation.Controls.MsgBox.Show("更新資料發生異常."); }
        
        }

        private void Insert(object item)
        {
            try
            {
                List<JHUpdateRecordRecord> InsertPackage =(List<JHUpdateRecordRecord>)item;
                JHUpdateRecord.Insert(InsertPackage);
            }
            catch (Exception ex) { FISCA.Presentation.Controls.MsgBox.Show("新增資料發生異常."); }            
        }

        private void Delete(object item)
        {
            try
            {
                List<JHUpdateRecordRecord> DelPackage = (List<JHUpdateRecordRecord>)item;
                JHUpdateRecord.Delete(DelPackage);
            }
            catch (Exception ex) { FISCA.Presentation.Controls.MsgBox.Show("刪除資料發生異常."); }
        }  
    }
}
