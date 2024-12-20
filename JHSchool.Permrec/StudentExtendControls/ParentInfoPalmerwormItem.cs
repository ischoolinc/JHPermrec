using System;
using System.Data;
using System.Text;
using K12.Data;
using FISCA.LogAgent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using FISCA.DSAUtil;
using FISCA.Presentation;
using Framework;
using JHSchool.Data;
using JHSchool.Permrec.Feature.Legacy;
using FCode = Framework.Security.FeatureCodeAttribute;
using FISCA.UDT;
using FISCA.Data;
using StudentInfoExtention.Model;
using Framework.DataChangeManage;
using System.Globalization;
using System.Text.RegularExpressions;

namespace JHSchool.Permrec.StudentExtendControls
{
    [FCode("JHSchool.Student.Detail0083", "父母親及監護人資料")]
    internal partial class ParentInfoPalmerwormItem : FISCA.Presentation.DetailContent
    {
        private bool _isInitialized = false;
        JHParentRecord _StudParentRec;
        private ChangeListener _DataListener_Father;
        private ChangeListener _DataListener_Mother;
        private ChangeListener _DataListener_Guardian;
        private bool isBGBusy = false;
        private BackgroundWorker BGWorker;
        private BackgroundWorker _getRelationshipBackgroundWorker;
        private BackgroundWorker _getJobBackgroundWorker;
        private BackgroundWorker _getEduDegreeBackgroundWorker;
        private BackgroundWorker _getNationalityBackgroundWorker;
        private ParentType _ParentType;
        PermRecLogProcess prlp;

        private bool load_completed = false;
        private string cboNationality_ori = "";

        private ParentExtentionInfo ParentExtentionInfo;//抓資料庫抓出來的家長延伸資料
        private ParentExtentionInfo WrapParentExtentionInfo;//修改的家長延伸資料

        string birthCountryCheckPattern = @"^\d{3}$";
        string birthYearCheckpattern = @"^\d{4}$";

        private QueryHelper _Qp = new QueryHelper();
        ErrorProvider errorBrithCountry = new ErrorProvider();
        ErrorProvider errorBrithYear = new ErrorProvider();
        private enum ParentType
        {
            Father,
            Mother,
            Guardian
        }

        public ParentInfoPalmerwormItem()
        {
            InitializeComponent();
            Group = "家長及監護人資料";
            prlp = new PermRecLogProcess();
            _StudParentRec = new JHParentRecord();
            _ParentType = ParentType.Guardian;

            _DataListener_Father = new ChangeListener();
            _DataListener_Guardian = new ChangeListener();
            _DataListener_Mother = new ChangeListener();
            _DataListener_Father.StatusChanged += new EventHandler<ChangeEventArgs>(_DataListener_Father_StatusChanged);
            _DataListener_Guardian.StatusChanged += new EventHandler<ChangeEventArgs>(_DataListener_Guardian_StatusChanged);
            _DataListener_Mother.StatusChanged += new EventHandler<ChangeEventArgs>(_DataListener_Mother_StatusChanged);

            // 加入父親 Listener
            _DataListener_Father.Add(new TextBoxSource(txtParentName));
            _DataListener_Father.Add(new TextBoxSource(txtIDNumber));
            _DataListener_Father.Add(new TextBoxSource(txtParentPhone));
            _DataListener_Father.Add(new ComboBoxSource(cboNationality,ComboBoxSource.ListenAttribute.Text));
            _DataListener_Father.Add(new ComboBoxSource(cboJob,ComboBoxSource.ListenAttribute.Text));
            _DataListener_Father.Add(new ComboBoxSource(cboEduDegree, ComboBoxSource.ListenAttribute.Text));
            _DataListener_Father.Add(new ComboBoxSource(cboAlive, ComboBoxSource.ListenAttribute.Text));
            _DataListener_Father.Add(new TextBoxSource(textBirthYear));
            _DataListener_Father.Add(new TextBoxSource(textBirthCountry));
            _DataListener_Father.Add(new TextBoxSource(textEmail));
            _DataListener_Father.Add(new TextBoxSource(textFromChina));
            _DataListener_Father.Add(new TextBoxSource(textFromForeign));
            _DataListener_Father.Add(new RadioButtonSource(rdbspuy));
            _DataListener_Father.Add(new RadioButtonSource(rdbspun));

            // 加入母親 Listener
            _DataListener_Mother.Add(new TextBoxSource(txtParentName));
            _DataListener_Mother.Add(new TextBoxSource(txtIDNumber));
            _DataListener_Mother.Add(new TextBoxSource(txtParentPhone));
            _DataListener_Mother.Add(new ComboBoxSource(cboNationality, ComboBoxSource.ListenAttribute.Text));
            _DataListener_Mother.Add(new ComboBoxSource(cboJob, ComboBoxSource.ListenAttribute.Text));
            _DataListener_Mother.Add(new ComboBoxSource(cboEduDegree, ComboBoxSource.ListenAttribute.Text));
            _DataListener_Mother.Add(new ComboBoxSource(cboAlive, ComboBoxSource.ListenAttribute.Text));
            _DataListener_Mother.Add(new TextBoxSource(textBirthYear));
            _DataListener_Mother.Add(new TextBoxSource(textBirthCountry));
            _DataListener_Mother.Add(new TextBoxSource(textEmail));
            _DataListener_Mother.Add(new TextBoxSource(textFromChina));
            _DataListener_Mother.Add(new TextBoxSource(textFromForeign));
            _DataListener_Mother.Add(new RadioButtonSource(rdbspuy));
            _DataListener_Mother.Add(new RadioButtonSource(rdbspun));

            // 加入監護人 Listener
            _DataListener_Guardian.Add(new TextBoxSource(txtParentName));
            _DataListener_Guardian.Add(new TextBoxSource(txtIDNumber));
            _DataListener_Guardian.Add(new TextBoxSource(txtParentPhone));
            _DataListener_Guardian.Add(new ComboBoxSource(cboNationality, ComboBoxSource.ListenAttribute.Text));
            _DataListener_Guardian.Add(new ComboBoxSource(cboJob, ComboBoxSource.ListenAttribute.Text));
            _DataListener_Guardian.Add(new ComboBoxSource(cboEduDegree, ComboBoxSource.ListenAttribute.Text));
            _DataListener_Guardian.Add(new ComboBoxSource(cboRelationship, ComboBoxSource.ListenAttribute.Text));
            _DataListener_Guardian.Add(new TextBoxSource(textEmail));

            JHParent.AfterUpdate += new EventHandler<K12.Data.DataChangedEventArgs>(JHParent_AfterUpdate);

            BGWorker = new BackgroundWorker();
            BGWorker.DoWork += new DoWorkEventHandler(BGWorker_DoWork);
            BGWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGWorker_RunWorkerCompleted);

            _getEduDegreeBackgroundWorker = new BackgroundWorker();            
            _getEduDegreeBackgroundWorker.DoWork += new DoWorkEventHandler(_getEduDegreeBackgroundWorker_DoWork);
            _getEduDegreeBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_getEduDegreeBackgroundWorker_RunWorkerCompleted);
            _getEduDegreeBackgroundWorker.RunWorkerAsync ();

            _getNationalityBackgroundWorker = new BackgroundWorker();
            _getNationalityBackgroundWorker.DoWork += new DoWorkEventHandler(_getNationalityBackgroundWorker_DoWork);
            _getNationalityBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_getNationalityBackgroundWorker_RunWorkerCompleted);
            _getNationalityBackgroundWorker.RunWorkerAsync();

            _getRelationshipBackgroundWorker = new BackgroundWorker();
            _getRelationshipBackgroundWorker.DoWork +=new DoWorkEventHandler(_getRelationshipBackgroundWorker_DoWork);
            _getRelationshipBackgroundWorker.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(_getRelationshipBackgroundWorker_RunWorkerCompleted);
            _getRelationshipBackgroundWorker.RunWorkerAsync();

            _getJobBackgroundWorker = new BackgroundWorker();
            _getJobBackgroundWorker.DoWork += new DoWorkEventHandler(_getJobBackgroundWorker_DoWork);
            _getJobBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_getJobBackgroundWorker_RunWorkerCompleted);
            _getJobBackgroundWorker.RunWorkerAsync();
            Disposed += new EventHandler(ParentInfoPalmerwormItem_Disposed);
        }

        void ParentInfoPalmerwormItem_Disposed(object sender, EventArgs e)
        {
            JHParent.AfterUpdate -= new EventHandler<K12.Data.DataChangedEventArgs>(JHParent_AfterUpdate);
        }

        void JHParent_AfterUpdate(object sender, K12.Data.DataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, K12.Data.DataChangedEventArgs>(JHParent_AfterUpdate), sender, e);
            }
            else
            {
                if (PrimaryKey != "")
                {
                    if (!BGWorker.IsBusy)
                        BGWorker.RunWorkerAsync();
                }
            }
        }

        void _getJobBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //DSXmlHelper helper = (e.Result as DSResponse).GetContent();

            //foreach (XmlNode node in helper.GetElements("Job"))
            //{
            //    cboJob.Items.Add(new KeyValuePair<string, string>(node.InnerText, node.InnerText));
            //}
            
            // 職業
            foreach (string str in Utility.GetJobList())
                cboJob.Items.Add(new KeyValuePair<string, string>(str, str));
        }

        void _getJobBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //e.Result = Config.GetJobList();
        }

        void _getNationalityBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //DSXmlHelper helper = (e.Result as DSResponse).GetContent();

            //foreach (XmlNode node in helper.GetElements("Nationality"))
            //{
            //    cboNationality.Items.Add(new KeyValuePair<string, string>(node.InnerText, node.InnerText));
            //}

            //國籍(舊版)
            //foreach (string str in Utility.GetNationalityList())
            //    cboNationality.Items.Add(new KeyValuePair<string, string>(str,str));

            // [ischoolkingdom] Vicky新增，[09-02][04] 家長國籍管理，由教務作業預設文件 調取家長國籍對照
            List<DAO.UDT_NationalityMapping> nation_list = new List<DAO.UDT_NationalityMapping>();
            AccessHelper accessHelper = new AccessHelper();
            nation_list = accessHelper.Select<DAO.UDT_NationalityMapping>();

            foreach (DAO.UDT_NationalityMapping item in nation_list)
            {
                cboNationality.Items.Add(new KeyValuePair<string, string>(item.Name, item.Name));
            }


        }

        void _getNationalityBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //e.Result = Config.GetNationalityList();
        }

        void _getEduDegreeBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //DSXmlHelper helper = (e.Result as DSResponse).GetContent();

            //foreach (XmlNode node in helper.GetElements("EducationDegree"))
            //{
            //    cboEduDegree.Items.Add(new KeyValuePair<string, string>(node.InnerText, node.InnerText));
            //}
            
            // 最高學歷
            foreach(string str in Utility.GetEducationDegreeList())
                cboEduDegree.Items.Add(new KeyValuePair<string, string>(str,str));
        }

        void _getEduDegreeBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //e.Result = Config.GetEduDegreeList();
        }

        void BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isBGBusy)
            {
                isBGBusy = false;
                BGWorker.RunWorkerAsync();
                return;
            }
            Initialize();
            BindDataToForm();
        }

        void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _StudParentRec = JHParent.SelectByStudentID(PrimaryKey);
            GetParentExtentionInfo();
        }

        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            errorBrithCountry.Clear();
            errorBrithYear.Clear();
            this.Loading = true;
            if (BGWorker.IsBusy)
                isBGBusy = true;
            else
                BGWorker.RunWorkerAsync();
        }

        private void BindDataToForm()
        {   
            // Log
            prlp.SetBeforeSaveText("父親姓名",_StudParentRec.Father.Name);
            prlp.SetBeforeSaveText("父親身分證號",_StudParentRec.Father.IDNumber );
            prlp.SetBeforeSaveText("父親電話",_StudParentRec.Father.Phone );
            prlp.SetBeforeSaveText("父親存歿",_StudParentRec.Father.Living );
            prlp.SetBeforeSaveText("父親國籍",_StudParentRec.Father.Nationality );
            prlp.SetBeforeSaveText("父親職業",_StudParentRec.Father.Job );
            prlp.SetBeforeSaveText("父親最高學歷",_StudParentRec.Father.EducationDegree );
            prlp.SetBeforeSaveText("母親姓名",_StudParentRec.Mother.Name );
            prlp.SetBeforeSaveText("母親身分證號",_StudParentRec.Mother.IDNumber );
            prlp.SetBeforeSaveText("母親電話",_StudParentRec.Mother.Phone);
            prlp.SetBeforeSaveText("母親存歿",_StudParentRec.Mother.Living);
            prlp.SetBeforeSaveText("母親國籍",_StudParentRec.Mother.Nationality);
            prlp.SetBeforeSaveText("母親職業",_StudParentRec.Mother.Job);
            prlp.SetBeforeSaveText("母親最高學歷",_StudParentRec.Mother.EducationDegree);
            prlp.SetBeforeSaveText("監護人姓名",_StudParentRec.Custodian.Name );
            prlp.SetBeforeSaveText("監護人身分證號",_StudParentRec.Custodian.IDNumber );
            prlp.SetBeforeSaveText("監護人電話",_StudParentRec.Custodian.Phone );
            prlp.SetBeforeSaveText("監護人關係",_StudParentRec.Custodian.Relationship);
            prlp.SetBeforeSaveText("監護人國籍",_StudParentRec.Custodian.Nationality );
            prlp.SetBeforeSaveText("監護人職業",_StudParentRec.Custodian.Job );
            prlp.SetBeforeSaveText("監護人最高學歷", _StudParentRec.Custodian.EducationDegree);

            this.Loading = false;
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            LoadDALDefaultData();

        
        }

        // 載入 DAL 預設取到值
        private void LoadDALDefaultData()
        {
            //DataListenerPause();
            //// 初始化
            //txtIDNumber.Text = "";
            //txtParentName.Text = "";
            //txtParentPhone.Text = "";
            //cboAlive.Text = "";
            //cboEduDegree.Text = "";
            //cboJob.Text = "";
            //cboNationality.Text = "";
            //cboRelationship.Text = "";

            if (_ParentType == ParentType.Guardian)
            {
                LoadGuardian();
            }

            if (_ParentType == ParentType.Father)
            {
                LoadFather();
            }

            if (_ParentType == ParentType.Mother)
            {
                LoadMother();
            }        
        }

        private void DataListenerPause()
        {
            _DataListener_Father.SuspendListen();
            _DataListener_Mother.SuspendListen();
            _DataListener_Guardian.SuspendListen();
        }

        private void btnGuardian_Click(object sender, EventArgs e)
        {
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            LoadGuardian();
            _ParentType = ParentType.Guardian;
            errorBrithCountry.Clear();
            errorBrithYear.Clear();
            errorProvider1.Clear();
        }

        protected override void OnCancelButtonClick(EventArgs e)
        {
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            errorBrithCountry.Clear();
            errorBrithYear.Clear();
            LoadDALDefaultData();
        }

        protected override void OnSaveButtonClick(EventArgs e)
        {


            // 回存資料
            if (_ParentType == ParentType.Guardian)
            {
                _StudParentRec.Custodian.EducationDegree = cboEduDegree.Text;
                _StudParentRec.Custodian.IDNumber = txtIDNumber.Text;
                _StudParentRec.Custodian.Job = cboJob.Text;
                _StudParentRec.Custodian.Name = txtParentName.Text;
                _StudParentRec.Custodian.Nationality = cboNationality.Text;
                _StudParentRec.Custodian.Phone = txtParentPhone.Text;
                _StudParentRec.Custodian.Relationship = cboRelationship.Text;
            }

            if (_ParentType == ParentType.Father)
            {
                _StudParentRec.Father.EducationDegree = cboEduDegree.Text;
                _StudParentRec.Father.IDNumber = txtIDNumber.Text;
                _StudParentRec.Father.Job = cboJob.Text;
                _StudParentRec.Father.Living = cboAlive.Text;
                _StudParentRec.Father.Name = txtParentName.Text;
                _StudParentRec.Father.Nationality = cboNationality.Text;
                _StudParentRec.Father.Phone = txtParentPhone.Text;

                if (this.textBirthCountry.Text != "" && !Regex.IsMatch(this.textBirthCountry.Text, birthCountryCheckPattern))
                {
                    if (Regex.IsMatch(this.textBirthYear.Text, birthYearCheckpattern) || this.textBirthCountry.Text == "" || this.textBirthYear.Text == "")
                    {
                        errorBrithYear.Clear();
                        errorBrithCountry.SetError(this.textBirthCountry, "請輸入3碼的國籍及出生代碼");
                        return;
                    }
                    else
                    {
                        errorBrithYear.SetError(this.textBirthYear, "請輸入西元紀年(4碼)");
                        errorBrithCountry.SetError(this.textBirthCountry, "請輸入3碼的國籍及出生代碼");
                        return;
                    }
                }
                else
                {
                    if (this.textBirthYear.Text != "" && !Regex.IsMatch(this.textBirthYear.Text, birthYearCheckpattern))
                    {
                        errorBrithCountry.Clear();
                        errorBrithYear.SetError(this.textBirthYear, "請輸入西元紀年(4碼)");
                        return;
                    }

                }
            }

            if (_ParentType == ParentType.Mother)
            {
                _StudParentRec.Mother.EducationDegree = cboEduDegree.Text;
                _StudParentRec.Mother.IDNumber = txtIDNumber.Text;
                _StudParentRec.Mother.Job = cboJob.Text;
                _StudParentRec.Mother.Living = cboAlive.Text;
                _StudParentRec.Mother.Name = txtParentName.Text;
                _StudParentRec.Mother.Nationality = cboNationality.Text;
                _StudParentRec.Mother.Phone = txtParentPhone.Text;
                if (this.textBirthCountry.Text != "" && !Regex.IsMatch(this.textBirthCountry.Text, birthCountryCheckPattern))
                {
                    if (Regex.IsMatch(this.textBirthYear.Text, birthYearCheckpattern) || this.textBirthCountry.Text == "" || this.textBirthYear.Text == "")
                    {
                        errorBrithYear.Clear();
                        errorBrithCountry.SetError(this.textBirthCountry, "請輸入3碼的國籍及出生代碼");
                        return;
                    }
                    else
                    {
                        errorBrithYear.SetError(this.textBirthYear, "請輸入西元紀年(4碼)");
                        errorBrithCountry.SetError(this.textBirthCountry, "請輸入3碼的國籍及出生代碼");
                        return;
                    }
                }
                else
                {
                    if (this.textBirthYear.Text != "" && !Regex.IsMatch(this.textBirthYear.Text, birthYearCheckpattern))
                    {
                        errorBrithCountry.Clear();
                        errorBrithYear.SetError(this.textBirthYear, "請輸入西元紀年(4碼)");
                        return;
                    }

                }
            }
            WrapFromValue();
            ParentExtenstionUpdate();
            prlp.SetAfterSaveText("父親姓名", _StudParentRec.Father.Name);
            prlp.SetAfterSaveText("父親身分證號", _StudParentRec.Father.IDNumber);
            prlp.SetAfterSaveText("父親電話", _StudParentRec.Father.Phone);
            prlp.SetAfterSaveText("父親存歿", _StudParentRec.Father.Living);
            prlp.SetAfterSaveText("父親國籍", _StudParentRec.Father.Nationality);
            prlp.SetAfterSaveText("父親職業", _StudParentRec.Father.Job);
            prlp.SetAfterSaveText("父親最高學歷", _StudParentRec.Father.EducationDegree);
            prlp.SetAfterSaveText("母親姓名", _StudParentRec.Mother.Name);
            prlp.SetAfterSaveText("母親身分證號", _StudParentRec.Mother.IDNumber);
            prlp.SetAfterSaveText("母親電話", _StudParentRec.Mother.Phone);
            prlp.SetAfterSaveText("母親存歿", _StudParentRec.Mother.Living);
            prlp.SetAfterSaveText("母親國籍", _StudParentRec.Mother.Nationality);
            prlp.SetAfterSaveText("母親職業", _StudParentRec.Mother.Job);
            prlp.SetAfterSaveText("母親最高學歷", _StudParentRec.Mother.EducationDegree);
            prlp.SetAfterSaveText("監護人姓名", _StudParentRec.Custodian.Name);
            prlp.SetAfterSaveText("監護人身分證號", _StudParentRec.Custodian.IDNumber);
            prlp.SetAfterSaveText("監護人電話", _StudParentRec.Custodian.Phone);
            prlp.SetAfterSaveText("監護人關係", _StudParentRec.Custodian.Relationship);
            prlp.SetAfterSaveText("監護人國籍", _StudParentRec.Custodian.Nationality);
            prlp.SetAfterSaveText("監護人職業", _StudParentRec.Custodian.Job);
            prlp.SetAfterSaveText("監護人最高學歷", _StudParentRec.Custodian.EducationDegree);
            JHParent.Update(_StudParentRec);
            prlp.SetActionBy("學籍", "學生父母及監護人資訊");
            prlp.SetAction("修改學生父母及監護人資訊");
            JHStudentRecord studRec = JHStudent.SelectByID(PrimaryKey);
            prlp.SetDescTitle("學生姓名:" + studRec.Name + ",學號:" + studRec.StudentNumber + ",");
            //Student.Instance.SyncDataBackground(PrimaryKey);
            Program.CustodianField.Reload();
            prlp.SaveLog("", "", "student", PrimaryKey);
            BindDataToForm();

        }

        void _DataListener_Mother_StatusChanged(object sender, ChangeEventArgs e)
        {
            if (Framework.User.Acl[GetType()].Editable)
                SaveButtonVisible = (e.Status == ValueStatus.Dirty);
            else
                SaveButtonVisible = false;

            CancelButtonVisible = (e.Status == ValueStatus.Dirty);
        }

        void _DataListener_Guardian_StatusChanged(object sender, ChangeEventArgs e)
        {
            if (Framework.User.Acl[GetType()].Editable)
                SaveButtonVisible = (e.Status == ValueStatus.Dirty);
            else
                SaveButtonVisible = false;

            CancelButtonVisible = (e.Status == ValueStatus.Dirty);
        }

        void _DataListener_Father_StatusChanged(object sender, ChangeEventArgs e)
        {
            if (Framework.User.Acl[GetType()].Editable)
                SaveButtonVisible = (e.Status == ValueStatus.Dirty);
            else
                SaveButtonVisible = false;

            CancelButtonVisible = (e.Status == ValueStatus.Dirty);
        }

        public DetailContent GetContent()
        {
            return new ParentInfoPalmerwormItem();
        }


        
        void _getRelationshipBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //DSXmlHelper helper = (e.Result as DSResponse).GetContent();

            //foreach (XmlNode node in helper.GetElements("Relationship"))
            //{
            //    cboRelationship.Items.Add(new KeyValuePair<string, string>(node.InnerText, node.InnerText));
            //}

            // 稱謂
            foreach(string str in Utility.GetRelationshipList())
                cboRelationship.Items.Add(new KeyValuePair<string, string>(str, str));
        }

        void _getRelationshipBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //e.Result = Config.GetRelationship();
        }
  
        private void Initialize()
        {
            if (_isInitialized) return;

            KeyValuePair<string, string> kvp = new KeyValuePair<string, string>("", "請選擇");
            cboRelationship.Items.Add(kvp);
            cboRelationship.DisplayMember = "Value";
            cboRelationship.ValueMember = "Key";

            cboJob.Items.Add(kvp);
            cboJob.DisplayMember = "Value";
            cboJob.ValueMember = "Key";


            cboEduDegree.Items.Add(kvp);
            cboEduDegree.DisplayMember = "Value";
            cboEduDegree.ValueMember = "Key";


            //載入國籍
            cboNationality.Items.Add(kvp);
            cboNationality.DisplayMember = "Value";
            cboNationality.ValueMember = "Key";

            //載入存殁
            kvp = new KeyValuePair<string, string>("存", "存");
            cboAlive.Items.Add(kvp);
            kvp = new KeyValuePair<string, string>("歿", "歿");
            cboAlive.Items.Add(kvp);
            cboAlive.DisplayMember = "Value";
            cboAlive.ValueMember = "Key";
            _isInitialized = true;
        }

        private void btnFather_Click(object sender, EventArgs e)
        {
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            LoadFather();
            _ParentType = ParentType.Father;
            errorBrithCountry.Clear();
            errorBrithYear.Clear();
            errorProvider1.Clear();
        }

        private void btnMother_Click(object sender, EventArgs e)
        {
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            LoadMother();
            _ParentType = ParentType.Mother;
            errorBrithCountry.Clear();
            errorBrithYear.Clear();
            errorProvider1.Clear();
        }

        // 載入監護人資料
        private void LoadGuardian()
        {
            load_completed = false;

            DataListenerPause();
            btnGuardian.Enabled = false;
            btnFather.Enabled = true;
            btnMother.Enabled = true;

            cboAlive.Visible = false;
            lblAlive.Visible = false;
            cboRelationship.Visible = true;
            lblRelationship.Visible = true;

            btnParentType.Text = btnGuardian.Text;
            txtParentName.Text = _StudParentRec.Custodian.Name;
            txtIDNumber.Text = _StudParentRec.Custodian.IDNumber;
            txtParentPhone.Text = _StudParentRec.Custodian.Phone;
            cboRelationship.Text = _StudParentRec.Custodian.Relationship;
            cboNationality.Text = _StudParentRec.Custodian.Nationality;
            cboJob.Text = _StudParentRec.Custodian.Job;
            cboEduDegree.Text = _StudParentRec.Custodian.EducationDegree;
            //cboRelationship.SetComboBoxValue(_StudParentRec.Custodian.Relationship);
            //cboNationality.SetComboBoxValue(_StudParentRec.Custodian.Nationality);
            //cboJob.SetComboBoxValue(_StudParentRec.Custodian.Job);
            //cboEduDegree.SetComboBoxValue(_StudParentRec.Custodian.EducationDegree);

            cboNationality_ori = _StudParentRec.Custodian.Nationality;
            load_completed = true;

            lnkCopyGuardian.Visible = false;
            lnkCopyFather.Visible = true;
            lnkCopyMother.Visible = true;
            if(ParentExtentionInfo != null)
            {
                this.textEmail.Text = ParentExtentionInfo.GuardianEmail;
                this.textBirthYear.Enabled = false;
                this.textBirthCountry.Enabled = false;
                this.textFromChina.Enabled = false;
                this.textFromForeign.Enabled = false;
                this.rdbspun.Enabled = false;
                this.rdbspuy.Enabled = false;
                this.textBirthYear.Text = "";
                this.textBirthCountry.Text = "";
                this.textFromChina.Text = "";
                this.textFromForeign.Text = "";
                this.rdbspun.Checked = false;
                this.rdbspuy.Checked = false;
            }
            else
            {
                this.textEmail.Text = "";
                this.textBirthYear.Enabled = false;
                this.textBirthCountry.Enabled = false;
                this.textFromChina.Enabled = false;
                this.textFromForeign.Enabled = false;
                this.rdbspun.Enabled = false;
                this.rdbspuy.Enabled = false;
                this.textBirthYear.Text = "";
                this.textBirthCountry.Text = "";
                this.textFromChina.Text = "";
                this.textFromForeign.Text = "";
                this.rdbspun.Checked = false;
                this.rdbspuy.Checked = false;
            }


            _DataListener_Guardian.Reset();
            _DataListener_Guardian.ResumeListen();
            
        }

        // 載入父親資料
        private void LoadFather()
        {
            load_completed = false;

            DataListenerPause();
            btnGuardian.Enabled = true;
            btnFather.Enabled = false;
            btnMother.Enabled = true;

            cboAlive.Visible = true;
            lblAlive.Visible = true;
            cboRelationship.Visible = false;
            lblRelationship.Visible = false;

            btnParentType.Text = btnFather.Text;
            txtParentName.Text = _StudParentRec.Father.Name;
            txtIDNumber.Text = _StudParentRec.Father.IDNumber;
            txtParentPhone.Text = _StudParentRec.Father.Phone;

            //cboAlive.Text = _StudParentRec.Father.Living;

            cboNationality.Text = _StudParentRec.Father.Nationality;
            cboJob.Text = _StudParentRec.Father.Job;
            cboEduDegree.Text = _StudParentRec.Father.EducationDegree;
            cboAlive.SetComboBoxValue(_StudParentRec.Father.Living);
            //cboNationality.SetComboBoxValue(_StudParentRec.Father.Nationality);
            //cboJob.SetComboBoxValue(_StudParentRec.Father.Job);
            //cboEduDegree.SetComboBoxValue(_StudParentRec.Father.EducationDegree);

            cboNationality_ori = _StudParentRec.Father.Nationality;
            load_completed = true;

            lnkCopyGuardian.Visible = true;
            lnkCopyFather.Visible = false;
            lnkCopyMother.Visible = false;

            this.textBirthYear.Enabled = true;
            this.textBirthCountry.Enabled = true;
            this.textFromChina.Enabled = true;
            this.textFromForeign.Enabled = true;
            this.rdbspun.Enabled = true;
            this.rdbspuy.Enabled = true;
            if (ParentExtentionInfo != null) { 
                this.textEmail.Text = ParentExtentionInfo.FatherEmail;
                this.textBirthYear.Text = ParentExtentionInfo.FatherBirthYear;
                this.textBirthCountry.Text = ParentExtentionInfo.FatherBirthCountry;
                this.textFromChina.Text = ParentExtentionInfo.FatherFromChina;
                this.textFromForeign.Text = ParentExtentionInfo.FatherFromForeign;

                if (ParentExtentionInfo.FatherIsUnemployed == "true")
                {
                    this.rdbspuy.Checked = true;
                }
                else if (ParentExtentionInfo.FatherIsUnemployed == "false")
                {
                    this.rdbspun.Checked = true;
                }
                else
                {
                    this.rdbspun.Checked = false;
                    this.rdbspuy.Checked = false;
                }
            }
            else
            {
                this.textEmail.Text = "";
                this.textBirthYear.Text = "";
                this.textBirthCountry.Text = "";
                this.textFromChina.Text = "";
                this.textFromForeign.Text = "";
                this.rdbspun.Checked = false;
                this.rdbspuy.Checked = false;
            }

            _DataListener_Father.Reset();
            _DataListener_Father.ResumeListen();
        }

        // 載入母親資料
        private void LoadMother()
        {
            load_completed = false;

            DataListenerPause();
            btnGuardian.Enabled = true;
            btnFather.Enabled = true;
            btnMother.Enabled = false;
            cboAlive.Visible = true;
            lblAlive.Visible = true;
            cboRelationship.Visible = false;
            lblRelationship.Visible = false;
            btnParentType.Text = btnMother.Text;
            txtParentName.Text = _StudParentRec.Mother.Name;
            txtIDNumber.Text = _StudParentRec.Mother.IDNumber; txtParentPhone.Text = _StudParentRec.Mother.Phone;

            //cboAlive.Text = _StudParentRec.Mother.Living;

            cboNationality.Text = _StudParentRec.Mother.Nationality;
            cboJob.Text = _StudParentRec.Mother.Job;
            cboEduDegree.Text = _StudParentRec.Mother.EducationDegree;
            cboAlive.SetComboBoxValue(_StudParentRec.Mother.Living);
            //cboNationality.SetComboBoxValue(_StudParentRec.Mother.Nationality);
            //cboJob.SetComboBoxValue(_StudParentRec.Mother.Job);
            //cboEduDegree.SetComboBoxValue(_StudParentRec.Mother.EducationDegree);

            cboNationality_ori = _StudParentRec.Mother.Nationality;
            load_completed = true;

            lnkCopyGuardian.Visible = true;
            lnkCopyFather.Visible = false;
            lnkCopyMother.Visible = false;

            this.textBirthYear.Enabled = true;
            this.textBirthCountry.Enabled = true;
            this.textFromChina.Enabled = true;
            this.textFromForeign.Enabled = true;
            this.rdbspun.Enabled = true;
            this.rdbspuy.Enabled = true;
            if (ParentExtentionInfo != null)
            {
                this.textEmail.Text = ParentExtentionInfo.MotherEmail;
                this.textBirthYear.Text = ParentExtentionInfo.MotherBirthYear;
                this.textBirthCountry.Text = ParentExtentionInfo.MotherBirthCountry;
                this.textFromChina.Text = ParentExtentionInfo.MotherFromChina;
                this.textFromForeign.Text = ParentExtentionInfo.MotherFromForeign;

                if (ParentExtentionInfo.MotherIsUnemployed == "true")
                {
                    this.rdbspuy.Checked = true;
                }
                else if (ParentExtentionInfo.MotherIsUnemployed == "false")
                {
                    this.rdbspun.Checked = true;
                }
                else
                {
                    this.rdbspun.Checked = false;
                    this.rdbspuy.Checked = false;
                }
            }
            else
            {
                this.textEmail.Text = "";
                this.textBirthYear.Text = "";
                this.textBirthCountry.Text = "";
                this.textFromChina.Text = "";
                this.textFromForeign.Text = "";
                this.rdbspun.Checked = false;
                this.rdbspuy.Checked = false;
            }
            _DataListener_Mother.Reset();
            _DataListener_Mother.ResumeListen();
        }


        private void lnkCopyGuardian_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorBrithCountry.Clear();
            errorBrithYear.Clear();
            txtParentName.Text = _StudParentRec.Custodian.Name;
            txtIDNumber.Text = _StudParentRec.Custodian.IDNumber;
            cboNationality.SetComboBoxValue(_StudParentRec.Custodian.Nationality);
            cboNationality.Text=_StudParentRec.Custodian.Nationality;
            cboJob.SetComboBoxValue(_StudParentRec.Custodian.Job );
            cboJob.Text = _StudParentRec.Custodian.Job;
            cboEduDegree.SetComboBoxValue(_StudParentRec.Custodian.EducationDegree);
            cboEduDegree.Text = _StudParentRec.Custodian.EducationDegree;
            txtParentPhone.Text = _StudParentRec.Custodian.Phone;
            textEmail.Text = ParentExtentionInfo.GuardianEmail;
            textBirthYear.Text = "";
            textBirthCountry.Text = "";
            textFromChina.Text = "";
            textFromForeign.Text = "";
            rdbspun.Checked = false;
            rdbspuy.Checked = false;
        }

        
        private void lnkCopyFather_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorBrithCountry.Clear();
            errorBrithYear.Clear();
            txtParentName.Text = _StudParentRec.Father.Name;
            txtIDNumber.Text = _StudParentRec.Father.IDNumber;
            cboNationality.SetComboBoxValue(_StudParentRec.Father.Nationality);
            cboNationality.Text = _StudParentRec.Father.Nationality;
            cboJob.SetComboBoxValue(_StudParentRec.Father.Job);
            cboJob.Text = _StudParentRec.Father.Job;
            cboEduDegree.SetComboBoxValue(_StudParentRec.Father.EducationDegree);
            cboEduDegree.Text = _StudParentRec.Father.EducationDegree;
            if (btnParentType.Text == "監護人") { }
                cboRelationship.SetComboBoxValue("家長1");
                cboRelationship.Text = "家長1";
            txtParentPhone.Text = _StudParentRec.Father.Phone;
            textEmail.Text = ParentExtentionInfo.FatherEmail;
        }

        private void lnkCopyMother_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorBrithCountry.Clear();
            errorBrithYear.Clear();
            txtParentName.Text = _StudParentRec.Mother.Name;
            txtIDNumber.Text = _StudParentRec.Mother.IDNumber;
            cboNationality.SetComboBoxValue(_StudParentRec.Mother.Nationality);
            cboNationality.Text = _StudParentRec.Mother.Nationality;
            cboJob.SetComboBoxValue(_StudParentRec.Mother.Job);
            cboJob.Text = _StudParentRec.Mother.Job;
            cboEduDegree.SetComboBoxValue(_StudParentRec.Mother.EducationDegree);
            cboEduDegree.Text = _StudParentRec.Mother.EducationDegree;
            if (btnParentType.Text == "監護人")
                cboRelationship.SetComboBoxValue("家長2");
                cboRelationship.Text = "家長2";
            txtParentPhone.Text = _StudParentRec.Mother.Phone;
            textEmail.Text = ParentExtentionInfo.MotherEmail;
        }

        //2017/4/19 穎驊新增 監聽 國籍欄位 內容改變事件
        private void cboNationality_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(cboNationality, string.Empty);
            if (load_completed && cboNationality.Text != cboNationality_ori)
            {
                List<string> nation_name_list = new List<string>();
                List<DAO.UDT_NationalityMapping> nation_list = new List<DAO.UDT_NationalityMapping>();
                AccessHelper accessHelper = new AccessHelper();
                nation_list = accessHelper.Select<DAO.UDT_NationalityMapping>();

                foreach (DAO.UDT_NationalityMapping item in nation_list)
                {
                    nation_name_list.Add(item.Name);
                }

                if (!nation_name_list.Contains(cboNationality.Text) && cboNationality.Text != "")
                {

                    //errorProvider1.Icon = new Icon(SystemIcons.Warning, 8 ,8);

                    errorProvider1.SetError(cboNationality, "此國籍名稱，不存在於教務作業>對照/代碼>國籍中英文對照表 的設定中，建議檢察。");
                }
            } 



        }

        //0630新增

        private void GetParentExtentionInfo()
        {
            string sql = @"SELECT 
	ref_student_id 
	,guardian_email
	,father_birth_year
 	,father_birth_country
 	,father_email
	,father_from_china
	,father_from_foreign
	,father_is_unemployed
	,mother_birth_year
	,mother_birth_country
	,mother_email
	,mother_from_china
	,mother_from_foreign
	,mother_is_unemployed
FROM  
    student_info_ext
WHERE ref_student_id = {0}";

            sql = string.Format(sql, PrimaryKey);
            DataTable dt = _Qp.Select(sql);

            if (dt.Rows.Count == 0)
            {
                this.ParentExtentionInfo = new ParentExtentionInfo(this.PrimaryKey);
                ParentExtentionInfo.NoExtensoinRecord = true;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                this.ParentExtentionInfo = new ParentExtentionInfo("" + dr["ref_student_id"]);
                this.ParentExtentionInfo.GuardianEmail = "" + dr["guardian_email"];
                this.ParentExtentionInfo.FatherBirthYear = "" + dr["father_birth_year"];
                this.ParentExtentionInfo.FatherBirthCountry = "" + dr["father_birth_country"];
                this.ParentExtentionInfo.FatherEmail = "" + dr["father_email"];
                this.ParentExtentionInfo.FatherFromChina = "" + dr["father_from_china"];
                this.ParentExtentionInfo.FatherFromForeign = "" + dr["father_from_foreign"];
                this.ParentExtentionInfo.FatherIsUnemployed = "" + dr["father_is_unemployed"];
                this.ParentExtentionInfo.MotherBirthYear = "" + dr["mother_birth_year"];
                this.ParentExtentionInfo.MotherBirthCountry = "" + dr["mother_birth_country"];
                this.ParentExtentionInfo.MotherEmail = "" + dr["mother_email"];
                this.ParentExtentionInfo.MotherFromChina = "" + dr["mother_from_china"];
                this.ParentExtentionInfo.MotherFromForeign = "" + dr["mother_from_foreign"];
                this.ParentExtentionInfo.MotherIsUnemployed = "" + dr["mother_is_unemployed"];
                ParentExtentionInfo.NoExtensoinRecord = false;
            }
        }

        private void WrapFromValue()
        {
            this.WrapParentExtentionInfo = new ParentExtentionInfo(this.PrimaryKey);
            switch (btnParentType.Text)
            {
                case "監護人":
                    WrapParentExtentionInfo.GuardianEmail = this.textEmail.Text;
                    break;
                case "家長1":
                case "父親":
                    WrapParentExtentionInfo.FatherBirthYear = this.textBirthYear.Text;
                    WrapParentExtentionInfo.FatherBirthCountry = this.textBirthCountry.Text;
                    WrapParentExtentionInfo.FatherEmail = this.textEmail.Text;
                    WrapParentExtentionInfo.FatherFromChina = this.textFromChina.Text;
                    WrapParentExtentionInfo.FatherFromForeign = this.textFromForeign.Text;
                    if (rdbspuy.Checked)
                    {
                        WrapParentExtentionInfo.FatherIsUnemployed = "true";
                    }else if (rdbspun.Checked)
                    {
                        WrapParentExtentionInfo.FatherIsUnemployed = "false";
                    }
                    else
                    {
                        WrapParentExtentionInfo.FatherIsUnemployed = "";
                    }
                    break;
                case "家長2":
                case "母親":
                    WrapParentExtentionInfo.MotherBirthYear = this.textBirthYear.Text;
                    WrapParentExtentionInfo.MotherBirthCountry = this.textBirthCountry.Text;
                    WrapParentExtentionInfo.MotherEmail = this.textEmail.Text;
                    WrapParentExtentionInfo.MotherFromChina = this.textFromChina.Text;
                    WrapParentExtentionInfo.MotherFromForeign = this.textFromForeign.Text;
                    if (rdbspuy.Checked)
                    {
                        WrapParentExtentionInfo.MotherIsUnemployed = "true";
                    }
                    else if (rdbspun.Checked)
                    {
                        WrapParentExtentionInfo.MotherIsUnemployed = "false";
                    }
                    else
                    {
                        WrapParentExtentionInfo.FatherIsUnemployed = "";
                    }
                    break;
                default:
                    break;

            }

        }

        private void ParentExtenstionUpdate()
        {
            string sql = "";
            string sql2 = "";
            string sql3 = "";
            if (this.ParentExtentionInfo.NoExtensoinRecord == true)
            {
                if(btnParentType.Text == "監護人")
                {
                    sql = @"            
INSERT INTO  
                student_info_ext
                (
                  ref_student_id 
                  , guardian_email
                )VALUES
                (
                   {0} 
                  , {1}
                    )
RETURNING *
";
                    sql = string.Format(sql
                        , WrapParentExtentionInfo.StudentID
                        , WrapParentExtentionInfo.GuardianEmail != "" ? $"'{WrapParentExtentionInfo.GuardianEmail}'" : "NULL");
                }
                else if(btnParentType.Text == "父親"|| btnParentType.Text == "家長1")
                {
                    sql2 = @"            INSERT INTO  
                student_info_ext
                (
                  ref_student_id 
                  , father_birth_year
                  , father_birth_country
                  , father_email
                  , father_from_china
                  , father_from_foreign
                  , father_is_unemployed
                )VALUES
                (
                   {0} 
                  , {1}
                  , {2}
                  , {3}
                  , {4}
                  , {5}
                  , {6}
                    )
RETURNING *
";
                    sql2 = string.Format(sql2
                        , WrapParentExtentionInfo.StudentID
                        , WrapParentExtentionInfo.FatherBirthYear != "" ? $"'{WrapParentExtentionInfo.FatherBirthYear}'" : "NULL"
                        , WrapParentExtentionInfo.FatherBirthCountry != "" ? $"'{WrapParentExtentionInfo.FatherBirthCountry}'" : "NULL"
                        , WrapParentExtentionInfo.FatherEmail != "" ? $"'{WrapParentExtentionInfo.FatherEmail}'" : "NULL"
                        , WrapParentExtentionInfo.FatherFromChina != "" ? $"'{WrapParentExtentionInfo.FatherFromChina}'" : "NULL"
                        , WrapParentExtentionInfo.FatherFromForeign != "" ? $"'{WrapParentExtentionInfo.FatherFromForeign}'" : "NULL"
                        , WrapParentExtentionInfo.FatherIsUnemployed != "" ? $"'{WrapParentExtentionInfo.FatherIsUnemployed}'" : "NULL");
                }
                else if (btnParentType.Text == "母親" || btnParentType.Text == "家長2")
                {
                    sql3 = @"            INSERT INTO  
                student_info_ext
                (
                  ref_student_id 
                  , mother_birth_year
                  , mother_birth_country
                  , mother_email
                  , mother_from_china
                  , mother_from_foreign
                  , mother_is_unemployed
                )VALUES
                (
                   {0} 
                  , {1}
                  , {2}
                  , {3}
                  , {4}
                  , {5}
                  , {6}
                    )
RETURNING *
";
                    sql3 = string.Format(sql3
                        , WrapParentExtentionInfo.StudentID
                        , WrapParentExtentionInfo.MotherBirthYear != "" ? $"'{WrapParentExtentionInfo.MotherBirthYear}'" : "NULL"
                        , WrapParentExtentionInfo.MotherBirthCountry != "" ? $"'{WrapParentExtentionInfo.MotherBirthCountry}'" : "NULL"
                        , WrapParentExtentionInfo.MotherEmail != "" ? $"'{WrapParentExtentionInfo.MotherEmail}'" : "NULL"
                        , WrapParentExtentionInfo.MotherFromChina != "" ? $"'{WrapParentExtentionInfo.MotherFromChina}'" : "NULL"
                        , WrapParentExtentionInfo.MotherFromForeign != "" ? $"'{WrapParentExtentionInfo.MotherFromForeign}'" : "NULL"
                        , WrapParentExtentionInfo.MotherIsUnemployed != "" ? $"'{WrapParentExtentionInfo.MotherIsUnemployed}'" : "NULL");        
                }
            }
            else if (this.ParentExtentionInfo.NoExtensoinRecord == false)
            {
                if (btnParentType.Text == "監護人")
                {
                    sql = @"UPDATE 
     student_info_ext
SET  
	 guardian_email = {1}
WHERE 
     ref_student_id = {0}
returning *";
                    sql = string.Format(sql
                        , WrapParentExtentionInfo.StudentID
                        , WrapParentExtentionInfo.GuardianEmail != "" ? $"'{WrapParentExtentionInfo.GuardianEmail}'" : "NULL"
                        );
                }
                else if (btnParentType.Text == "父親" || btnParentType.Text == "家長1")
                {
                    sql2 = @"UPDATE 
     student_info_ext
SET  
	 father_birth_year= {1}
 	 ,father_birth_country = {2}
  	 ,father_email = {3}
  	 ,father_from_china = {4}
  	 ,father_from_foreign = {5}
  	 ,father_is_unemployed = {6}
WHERE 
     ref_student_id = {0}
returning *";
                    sql2 = string.Format(sql2
                        , WrapParentExtentionInfo.StudentID
                        , WrapParentExtentionInfo.FatherBirthYear != "" ? $"'{WrapParentExtentionInfo.FatherBirthYear}'" : "NULL"
                        , WrapParentExtentionInfo.FatherBirthCountry != "" ? $"'{WrapParentExtentionInfo.FatherBirthCountry}'" : "NULL"
                        , WrapParentExtentionInfo.FatherEmail != "" ? $"'{WrapParentExtentionInfo.FatherEmail}'" : "NULL"
                        , WrapParentExtentionInfo.FatherFromChina != "" ? $"'{WrapParentExtentionInfo.FatherFromChina}'" : "NULL"
                        , WrapParentExtentionInfo.FatherFromForeign != "" ? $"'{WrapParentExtentionInfo.FatherFromForeign}'" : "NULL"
                        , WrapParentExtentionInfo.FatherIsUnemployed != "" ? $"'{WrapParentExtentionInfo.FatherIsUnemployed}'" : "NULL");
                }
                else if (btnParentType.Text == "母親" || btnParentType.Text == "家長2")
                {
                    sql3 = @"UPDATE 
     student_info_ext
SET  
	 mother_birth_year= {1}
 	 ,mother_birth_country = {2}
  	 ,mother_email = {3}
  	 ,mother_from_china = {4}
  	 ,mother_from_foreign = {5}
  	 ,mother_is_unemployed = {6}
WHERE 
     ref_student_id = {0}
returning *";
                    sql3 = string.Format(sql3
                        , WrapParentExtentionInfo.StudentID
                        , WrapParentExtentionInfo.MotherBirthYear != "" ? $"'{WrapParentExtentionInfo.MotherBirthYear}'" : "NULL"
                        , WrapParentExtentionInfo.MotherBirthCountry != "" ? $"'{WrapParentExtentionInfo.MotherBirthCountry}'" : "NULL"
                        , WrapParentExtentionInfo.MotherEmail != "" ? $"'{WrapParentExtentionInfo.MotherEmail}'" : "NULL"
                        , WrapParentExtentionInfo.MotherFromChina != "" ? $"'{WrapParentExtentionInfo.MotherFromChina}'" : "NULL"
                        , WrapParentExtentionInfo.MotherFromForeign != "" ? $"'{WrapParentExtentionInfo.MotherFromForeign}'" : "NULL"
                        , WrapParentExtentionInfo.MotherIsUnemployed != "" ? $"'{WrapParentExtentionInfo.MotherIsUnemployed}'" : "NULL");
                }
            }

            try {
                K12.Data.StudentRecord studentRecord = K12.Data.Student.SelectByID(WrapParentExtentionInfo.StudentID);
                StringBuilder sb = new StringBuilder();
                if (btnParentType.Text == "監護人")
                {
                    DataTable dt = _Qp.Select(sql);
                    sb.AppendLine($"{studentRecord.Name}，學號：{studentRecord.StudentNumber}");
                    if (this.ParentExtentionInfo.GuardianEmail != WrapParentExtentionInfo.GuardianEmail)
                    {
                        sb.AppendLine($"【學生監護人Email】由「{ParentExtentionInfo.GuardianEmail}」改為「{WrapParentExtentionInfo.GuardianEmail}」");
                    }
                    ApplicationLog.Log("學生-資料項目-父母親及監護人資料", "修改", "student", WrapParentExtentionInfo.StudentID, sb.ToString());
                    DataRow dr = dt.Rows[0];
                    string studentID = "" + dr["ref_student_id"];
                    if (this.PrimaryKey == studentID)
                    {
                        this.ParentExtentionInfo = new ParentExtentionInfo("" + dr["ref_student_id"]);
                        this.ParentExtentionInfo.GuardianEmail = "" + dr["guardian_email"];
                        ParentExtentionInfo.NoExtensoinRecord = false;
                    }
                }
                else if(btnParentType.Text == "父親" || btnParentType.Text == "家長1")
                {
                    DataTable dt = _Qp.Select(sql2);
                    sb.AppendLine($"{studentRecord.Name}，學號：{studentRecord.StudentNumber}");
                    if (this.ParentExtentionInfo.FatherBirthYear != WrapParentExtentionInfo.FatherBirthYear)
                    {
                        sb.AppendLine($"【學生父親出生年次】由「{ParentExtentionInfo.FatherBirthYear}」改為「{WrapParentExtentionInfo.FatherBirthYear}」");
                    }
                    if (this.ParentExtentionInfo.FatherBirthCountry != WrapParentExtentionInfo.FatherBirthCountry)
                    {
                        sb.AppendLine($"【學生父親原屬國家地區】由「{ParentExtentionInfo.FatherBirthCountry}」改為「{WrapParentExtentionInfo.FatherBirthCountry}」");
                    }
                    if (this.ParentExtentionInfo.FatherEmail != WrapParentExtentionInfo.FatherEmail)
                    {
                        sb.AppendLine($"【學生父親Email】由「{ParentExtentionInfo.FatherEmail}」改為「{WrapParentExtentionInfo.FatherEmail}」");
                    }
                    if (this.ParentExtentionInfo.FatherFromChina != WrapParentExtentionInfo.FatherFromChina)
                    {
                        sb.AppendLine($"【學生父親為大陸配偶_省份】由「{ParentExtentionInfo.FatherFromChina}」改為「{WrapParentExtentionInfo.FatherFromChina}」");
                    }
                    if (this.ParentExtentionInfo.FatherFromForeign != WrapParentExtentionInfo.FatherFromForeign)
                    {
                        sb.AppendLine($"【學生父親為外籍配偶_國籍】由「{ParentExtentionInfo.FatherFromForeign}」改為「{WrapParentExtentionInfo.FatherFromForeign}」");
                    }
                    if (this.ParentExtentionInfo.FatherIsUnemployed != WrapParentExtentionInfo.FatherIsUnemployed)
                    {
                        sb.AppendLine($"【學生父親為失業勞工】由「{ParentExtentionInfo.FatherIsUnemployed}」改為「{WrapParentExtentionInfo.FatherIsUnemployed}」");
                    }
                    ApplicationLog.Log("學生-資料項目-父母親及監護人資料", "修改", "student", WrapParentExtentionInfo.StudentID, sb.ToString());
                    DataRow dr = dt.Rows[0];
                    string studentID = "" + dr["ref_student_id"];
                    if (this.PrimaryKey == studentID)
                    {
                        this.ParentExtentionInfo = new ParentExtentionInfo("" + dr["ref_student_id"]);
                        this.ParentExtentionInfo.FatherBirthYear = "" + dr["father_birth_year"];
                        this.ParentExtentionInfo.FatherBirthCountry = "" + dr["father_birth_country"];
                        this.ParentExtentionInfo.FatherEmail = "" + dr["father_email"];
                        this.ParentExtentionInfo.FatherFromChina = "" + dr["father_from_china"];
                        this.ParentExtentionInfo.FatherFromForeign = "" + dr["father_from_foreign"];
                        this.ParentExtentionInfo.FatherIsUnemployed = "" + dr["father_is_unemployed"];
                        ParentExtentionInfo.NoExtensoinRecord = false;
                    }
                }
                else if(btnParentType.Text == "母親" || btnParentType.Text == "家長2")
                {
                    DataTable dt = _Qp.Select(sql3);
                    sb.AppendLine($"{studentRecord.Name}，學號：{studentRecord.StudentNumber}");
                    if (this.ParentExtentionInfo.MotherBirthYear != WrapParentExtentionInfo.MotherBirthYear)
                    {
                        sb.AppendLine($"【學生母親出生年次】由「{ParentExtentionInfo.MotherBirthYear}」改為「{WrapParentExtentionInfo.MotherBirthYear}」");
                    }
                    if (this.ParentExtentionInfo.MotherBirthCountry != WrapParentExtentionInfo.MotherBirthCountry)
                    {
                        sb.AppendLine($"【學生母親原屬國家地區】由「{ParentExtentionInfo.MotherBirthCountry}」改為「{WrapParentExtentionInfo.MotherBirthCountry}」");
                    }
                    if (this.ParentExtentionInfo.MotherEmail != WrapParentExtentionInfo.MotherEmail)
                    {
                        sb.AppendLine($"【學生母親Email】由「{ParentExtentionInfo.MotherEmail}」改為「{WrapParentExtentionInfo.MotherEmail}」");
                    }
                    if (this.ParentExtentionInfo.MotherFromChina != WrapParentExtentionInfo.MotherFromChina)
                    {
                        sb.AppendLine($"【學生母親為大陸配偶_省份】由「{ParentExtentionInfo.MotherFromChina}」改為「{WrapParentExtentionInfo.MotherFromChina}」");
                    }
                    if (this.ParentExtentionInfo.MotherFromForeign != WrapParentExtentionInfo.MotherFromForeign)
                    {
                        sb.AppendLine($"【學生母親為外籍配偶_國籍】由「{ParentExtentionInfo.MotherFromForeign}」改為「{WrapParentExtentionInfo.MotherFromForeign}」");
                    }
                    if (this.ParentExtentionInfo.MotherIsUnemployed != WrapParentExtentionInfo.MotherIsUnemployed)
                    {
                        sb.AppendLine($"【學生母親為失業勞工】由「{ParentExtentionInfo.MotherIsUnemployed}」改為「{WrapParentExtentionInfo.MotherIsUnemployed}」");
                    }
                    ApplicationLog.Log("學生-資料項目-父母親及監護人資料", "修改", "student", WrapParentExtentionInfo.StudentID, sb.ToString());
                    DataRow dr = dt.Rows[0];
                    string studentID = "" + dr["ref_student_id"];
                    if (this.PrimaryKey == studentID)
                    {
                        this.ParentExtentionInfo = new ParentExtentionInfo("" + dr["ref_student_id"]);
                        this.ParentExtentionInfo.MotherBirthYear = "" + dr["mother_birth_year"];
                        this.ParentExtentionInfo.MotherBirthCountry = "" + dr["mother_birth_country"];
                        this.ParentExtentionInfo.MotherEmail = "" + dr["mother_email"];
                        this.ParentExtentionInfo.MotherFromChina = "" + dr["mother_from_china"];
                        this.ParentExtentionInfo.MotherFromForeign = "" + dr["mother_from_foreign"];
                        this.ParentExtentionInfo.MotherIsUnemployed = "" + dr["mother_is_unemployed"];
                        ParentExtentionInfo.NoExtensoinRecord = false;
                    }
                }

            } catch (Exception ex) {
                FISCA.Presentation.Controls.MsgBox.Show("儲存發生錯誤! \r\n" + "錯誤訊息" + ex.Message);
            }

        }
        private void TextBirthYear_TextChanged(object sender, EventArgs e)
        {
            if (this.textBirthYear.Text != "" && !Regex.IsMatch(this.textBirthYear.Text, birthYearCheckpattern))
            {
                errorBrithYear.SetError(this.textBirthYear, "請輸入西元紀年(4碼)");
            }
            else
            {
                errorBrithYear.Clear();
            }
        }

        private void TextBirthCountry_TextChanged(object sender, EventArgs e)
        {
            if (this.textBirthCountry.Text != "" && !Regex.IsMatch(this.textBirthCountry.Text, birthCountryCheckPattern))
            {
                errorBrithCountry.SetError(this.textBirthCountry, "請輸入3碼的國籍及出生代碼");
            }
            else
            {
                errorBrithCountry.Clear();
            }
        }
    }
}
