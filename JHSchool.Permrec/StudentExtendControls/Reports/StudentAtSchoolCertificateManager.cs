using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Aspose.Words;
using Aspose.Words.Tables;
using Aspose.Words.Reporting;
using Framework;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using Aspose.Words.Drawing;
using System.Globalization;

namespace JHSchool.Permrec.StudentExtendControls.Reports
{
    class StudentAtSchoolCertificateManager
    {
        List<DAL.StudentEntity> _StudentList;
        private BackgroundWorker bkWorkPrint;
        private bool _isDefaultTemplate = true;
        private bool _isDefaultTemplateChinese = false;  // [ischoolkingdom] Vicky新增，[11-04][02]在學證明書(英文)
        private byte[] _buffer = null;
        
        
        private byte[] defalutTemplate;
        private byte[] defalutTemplate_Chi;
        private string base64 = "";
        private bool _isUpload = false;
        private string _Semester = "";
        private string _CertDoc = "";
        private string _CertNo = "";
        
        
        /// <summary>
        /// 取得是否讀取設樣版
        /// </summary>
        public bool GetisDefaultTemplate()
        {
            return _isDefaultTemplate;        
        }

        public StudentAtSchoolCertificateManager()
        {
            _StudentList = new List<JHSchool.Permrec.StudentExtendControls.Reports.DAL.StudentEntity>();

            GetUserDefineTemplateFromSystem();
            // 判斷使用新竹或高雄樣版
            if (Program.ModuleType == Program.ModuleFlag.HsinChu)
            {
                defalutTemplate = JHSchool.Permrec.StudentExtendControls.Reports.RptResource.在學證明書中英文;
                defalutTemplate_Chi = JHSchool.Permrec.StudentExtendControls.Reports.RptResource.在學證明書_無成績_新竹;
            }
            else
            {
                defalutTemplate = JHSchool.Permrec.StudentExtendControls.Reports.RptResource.在學證明書中英文;
                defalutTemplate_Chi =JHSchool.Permrec.StudentExtendControls.Reports.RptResource.在學證明書中文_高雄_;
            }
            
        }

        /// <summary>
        /// 設定學期
        /// </summary>
        /// <param name="Semester"></param>
        public void SetSemester(string  Semester,bool isUseNo)
        {
            if (isUseNo)
            {
                _Semester = Semester;
            }
            else
            {
                if (Semester == "1")
                    _Semester = "上";

                if (Semester == "2")
                    _Semester = "下";
            }
        }

        /// <summary>
        /// 設定文號
        /// </summary>
        /// <param name="CertDoc"></param>
        public void SetCertDoc(string CertDoc)
        {
            _CertDoc = CertDoc;
        }

        /// <summary>
        /// 設定字第
        /// </summary>
        /// <param name="CertNo"></param>
        public void SetCertNo(string CertNo)
        {
            _CertNo = CertNo;
        }

        // 取得系統今年中文年
        private string GetSystemTodayChineseDate()
        {
            return "中華民國" + (DateTime.Now.Year - 1911) + "年" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日";
        }

        /// <summary>
        /// 取得使用者自訂範本
        /// </summary>
        public void GetUserDefineTemplateFromSystem()
        {
            School.Configuration.Sync("在學證明書_無成績");
            ConfigData cd = School.Configuration["在學證明書_無成績"];

            bool.TryParse(cd["Default"], out _isDefaultTemplate);

            if (cd.Contains("CustomizeTemplate"))
            {
                string templateBase64 = cd["CustomizeTemplate"];
                _buffer = Convert.FromBase64String(templateBase64);
            }
            else
            {
                #region 產生空白設定檔
                cd["Default"] = "true";
                cd["CustomizeTemplate"] = "";
                cd.Save();
                #endregion
            }        
        }
        // [ischoolkingdom] Vicky新增，[11-04][02]在學證明書(英文)，新增預設中文樣板、中英文預設樣板選擇
        /// <summary>
        /// 儲存預設樣版(中文)
        /// </summary>
        public void SaveDefaulTemplate_Chi()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "中文在學證明書(無成績).doc";
            sfd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (Program.ModuleType == Program.ModuleFlag.HsinChu)
                    {
                        defalutTemplate_Chi = RptResource.在學證明書_無成績_新竹;
                    }
                    else
                    {
                        defalutTemplate_Chi = RptResource.在學證明書中文_高雄_;
                    }
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(defalutTemplate_Chi, 0, defalutTemplate_Chi.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        /// <summary>
        /// 儲存預設樣版(中英文)
        /// </summary>
        public void SaveDefaulTemplate_ChiEng()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "中英文在學證明書(無成績).doc";
            sfd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {                    
                    
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(defalutTemplate, 0, defalutTemplate.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }        
        }

        /// <summary>
        /// 設定使用者自訂範本
        /// </summary>
        public void SetUserDefineTemplateToSystem()
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇自訂的在學證明書範本";
            ofd.Filter = "Word檔案 (*.doc)|*.doc";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (FileFormatUtil.DetectFileFormat(ofd.FileName).LoadFormat == LoadFormat.Doc)
                    {
                        FileStream fs = new FileStream(ofd.FileName, FileMode.Open);

                        byte[] tempBuffer = new byte[fs.Length];
                        fs.Read(tempBuffer, 0, tempBuffer.Length);
                        base64 = Convert.ToBase64String(tempBuffer);
                        _isUpload = true;
                        fs.Close();
                        SaveTemplateToSystem();
                        PermRecLogProcess prlp = new PermRecLogProcess();
                        prlp.SaveLog("學生.報表", "上傳", "上傳在學證明書樣版.");        

                        MsgBox.Show("上傳成功。");
                    }
                    else
                        MsgBox.Show("上傳檔案格式不符");
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "開啟檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        
        }

        /// <summary>
        /// 儲存使用者定義
        /// </summary>
        public void SaveTemplateToSystem()
        {

            ConfigData cd = School.Configuration["在學證明書_無成績"];

            cd["Default"] = _isDefaultTemplate.ToString();
            cd["CustomizeTemplate"] = base64;
            cd.Save();

        }

        /// <summary>
        /// 儲存使用者自訂樣版
        /// </summary>
        public void SaveUserDefineTemplate()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "自訂在學證明書(無成績).doc";
            sfd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    if (_buffer != null && Aspose.Words.FileFormatUtil.DetectFileFormat(new MemoryStream(_buffer)).LoadFormat == Aspose.Words.LoadFormat.Doc)
                        fs.Write(_buffer, 0, _buffer.Length);
                    else
                        fs.Write(defalutTemplate, 0, defalutTemplate.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        
        }

        /// <summary>
        /// 列印資料
        /// </summary>
        /// <param name="StudentIDList"></param>        
        public void PrintData(List<string> StudentIDList,bool isDefaultTemplate, bool isDefaultTemplateChinese)
        {
            GetUserDefineTemplateFromSystem();            
            DAL.DALTransfer.UseStudPhotp = true; // [ischoolkingdom] Vicky新增，[11-04][02]在學證明書(英文)，新增照片顯示
            _StudentList = DAL.DALTransfer.GetStudentEntityList(StudentIDList);
            _isDefaultTemplate = isDefaultTemplate;
            _isDefaultTemplateChinese = isDefaultTemplateChinese; 
            bkWorkPrint = new BackgroundWorker();
            bkWorkPrint.DoWork += new DoWorkEventHandler(bkWorkPrint_DoWork);
            bkWorkPrint.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkWorkPrint_RunWorkerCompleted);
            bkWorkPrint.ProgressChanged += new ProgressChangedEventHandler(bkWorkPrint_ProgressChanged);
            bkWorkPrint.WorkerReportsProgress = true;
            bkWorkPrint.RunWorkerAsync();
        }

        void bkWorkPrint_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FISCA.Presentation.MotherForm.SetStatusBarMessage("資料產生中..");
        }

        void bkWorkPrint_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Document doc = (Document)e.Result;

            string filePath = Application.StartupPath + "\\Reports\\";
            string fileName = "在學證明書(無成績).doc";
            try
            {
                doc.Save(filePath + fileName, SaveFormat.Doc);
                System.Diagnostics.Process.Start(filePath + fileName);                
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = fileName;
                sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        doc.Save(sd.FileName, Aspose.Words.SaveFormat.Doc);

                    }
                    catch
                    {
                        MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            SaveSelectItem();
            FISCA.Presentation.MotherForm.SetStatusBarMessage("資料產生完成.");
        }

        void bkWorkPrint_DoWork(object sender, DoWorkEventArgs e)
        {
            Document doc = new Document ();
            doc.Sections.Clear ();
            int cot = 0;
            foreach (DAL.StudentEntity se in _StudentList)
            {
                Dictionary<string, object> baseData = new Dictionary<string, object>();
                // [ischoolkingdom] Vicky新增，[11-04][02]在學證明書(英文)，新增 學生英文姓名、學校英文名稱、英文生日、今天英文日期、校長英文名、新生照片，整理舊校長教務主任名的code。
                baseData.Add("學生姓名", se.StudentName);
                baseData.Add("學生英文姓名", se.StudentEnglishName);
                baseData.Add("學校中文名稱", se.SchoolChineseName);
                baseData.Add("學校英文名稱", se.SchoolEnglishName);
                baseData.Add("學校地址", se.SchoolAddress);
                baseData.Add("英文生日", se.GetEnglishBirthday());
                baseData.Add("生日年", se.GetBirthdayChineseYear());
                baseData.Add("生日月", se.GetBirthdayMonth());
                baseData.Add("生日日", se.GetBirthdayDay ());
                baseData.Add("班級名稱", se.ClassName);
                baseData.Add("班級年級", se.GradeYear);
                baseData.Add("今天英文日期", DateTime.Now.ToString("MMMM dd, yyyy", new CultureInfo("en-US")));
                baseData.Add("今天民國日期", GetSystemTodayChineseDate());
                baseData.Add("文號", _CertDoc);
                baseData.Add("第號", _CertNo);
                baseData.Add("學期", _Semester);
                baseData.Add("身分證號", se.IDNumber);
                baseData.Add("校長姓名", se.ChancellorChineseName);
                baseData.Add("校長英文姓名", se.ChancellorEnglishName);
                baseData.Add("教務主任姓名", se.EduDirectorName);
                baseData.Add("新生照片", se.GetPhotoImage());
                baseData.Add("學號", se.StudentNumber);

                List<string> rptKeys = new List<string>();
                List<object> rptValues = new List<object>();

                foreach (KeyValuePair<string, object> item in baseData)
                {
                    rptKeys.Add(item.Key);
                    rptValues.Add(item.Value);
                }
                
                Document docTemplate;

                // [ischoolkingdom] Vicky新增，[11-04][02]在學證明書(英文)
                if (_isDefaultTemplate && !_isDefaultTemplateChinese) //選擇中英文預設
                    docTemplate = new Document(new MemoryStream(defalutTemplate));
                else
                {
                    if (_isDefaultTemplate && _isDefaultTemplateChinese) //選擇中文預設
                        docTemplate = new Document(new MemoryStream(defalutTemplate_Chi));
                    else  //選擇自訂
                    {
                        if (_buffer.Length < 1)
                            docTemplate = new Document(new MemoryStream(defalutTemplate));
                        else
                            docTemplate = new Document(new MemoryStream(_buffer));
                    }                    
                }

                docTemplate.MailMerge.FieldMergingCallback = new InsertDocumentAtMailMergeHandler();
                docTemplate.MailMerge.RemoveEmptyParagraphs = true;
                docTemplate.MailMerge.Execute(rptKeys.ToArray(), rptValues.ToArray());
                doc.Sections.Add(doc.ImportNode(docTemplate.Sections[0],true ));

                bkWorkPrint.ReportProgress((int)(((double)cot++ * 100.0) / (double)_StudentList.Count));
            }
            e.Result = doc;
        }

        private void SaveSelectItem()
        {
            ConfigData cd = School.Configuration["在學證明書_無成績"];

            cd["Default"] = _isDefaultTemplate.ToString();
            cd.Save();
        }


        private class InsertDocumentAtMailMergeHandler : IFieldMergingCallback
        {
            // [ischoolkingdom] Vicky新增，[11-04][02]在學證明書(英文)，新增照片
            void IFieldMergingCallback.FieldMerging(FieldMergingArgs e)
            {
                if (e.FieldName == "新生照片")
                {
                    byte[] photo = e.FieldValue as byte[];
                    if (photo == null)
                        return;
                    DocumentBuilder photoBuilder = new DocumentBuilder(e.Document);
                    photoBuilder.MoveToField(e.Field, true);
                    e.Field.Remove();

                    Shape photoShape = new Shape(e.Document, ShapeType.Image);
                    photoShape.ImageData.SetImage(photo);
                    double shapeHeight = 0;
                    double shapeWidth = 0;
                    photoShape.WrapType = WrapType.Inline;//設定文繞圖

                    //resize

                    double origSizeRatio = photoShape.ImageData.ImageSize.HeightPoints / photoShape.ImageData.ImageSize.WidthPoints;
                    Cell curCell = photoBuilder.CurrentParagraph.ParentNode as Cell;
                    shapeHeight = (curCell.ParentNode as Row).RowFormat.Height;
                    shapeWidth = curCell.CellFormat.Width;
                    photoShape.Height = shapeHeight;
                    photoShape.Width = shapeWidth;
                    photoBuilder.InsertNode(photoShape);
                }


            }

            void IFieldMergingCallback.ImageFieldMerging(ImageFieldMergingArgs args)
            {
                // Do nothing.
            }
        }


    }
}
