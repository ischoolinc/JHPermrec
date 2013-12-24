using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation;
using FISCA.Permission;
using Campus.Windows;
using K12.Data;

namespace UserDefineData
{
    // 自訂資料欄位
    [FeatureCode("JHSchool.Student.UserDefineData", "自訂資料欄位(Beta)")]
    public partial class UserDefineDataItem : DetailContent
    {

        private BackgroundWorker _BGWorker;
        private ChangeListener ChangeManager = new ChangeListener();
        private bool _isBusy = false;
        // 放待刪除資料
        private List<DAL.UserDefData> _DeleteDataList;
        // 放待新新增資料
        private List<DAL.UserDefData> _InsertDataList;

        private Dictionary<string, string> _UseDefineDataType;
        //private Dictionary<string,DAL.UserDefData> _UserDefDataDict;
        List<DAL.UserDefData> _UserDefDataList;
        private List<string> _CheckSameList;


        
        PermRecLogProcess prlp;
        public UserDefineDataItem()
        {
            InitializeComponent();
            _DeleteDataList = new List<UserDefineData.DAL.UserDefData>();
            _InsertDataList = new List<UserDefineData.DAL.UserDefData>();
            //_UserDefDataDict = new Dictionary<string, UserDefineData.DAL.UserDefData>();
            _UserDefDataList = new List<DAL.UserDefData>();
            _CheckSameList = new List<string>();
            // 取得使用者設定欄位型態
            _UseDefineDataType = Global.GetUserConfigData();
            prlp = new PermRecLogProcess();
            Group = "自訂資料欄位";
            _BGWorker = new BackgroundWorker();
            _BGWorker.DoWork += new DoWorkEventHandler(_BGWorker_DoWork);
            _BGWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BGWorker_RunWorkerCompleted);
            ChangeManager.Add(new DataGridViewSource(dgv));
            ChangeManager.StatusChanged += delegate(object sender, ChangeEventArgs e)
            {
                this.CancelButtonVisible = (e.Status == ValueStatus.Dirty);
                this.SaveButtonVisible = (e.Status == ValueStatus.Dirty);
            };
        }

        void _BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_isBusy)
            {
                _isBusy = false;
                _BGWorker.RunWorkerAsync();
                return;
            }
            DataBindToDataGridView();
        }

        void _BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // modify by 小郭, 2013/12/16
            // _UserDefDataList = UDTTransfer.GetDataFromUDT(PrimaryKey);
            _UserDefDataList = UDTTransfer.GetDataFromUDTIncludeUserConfig(PrimaryKey);
        }

        /// <summary>
        /// 將讀取資料填入DataGridView
        /// </summary>
        private void DataBindToDataGridView()
        {
            try
            {
                this.Loading = true;
                ChangeManager.SuspendListen();
                dgv.Rows.Clear();
                _DeleteDataList.Clear();
                
                
                foreach (DAL.UserDefData udd in _UserDefDataList)
                {
                    int rowIdx =dgv.Rows.Add();
                    dgv.Rows[rowIdx].Tag = udd;
                    dgv.Rows[rowIdx].Cells[FieldName.Index].Value = udd.FieldName;
                    dgv.Rows[rowIdx].Cells[Value.Index].Value = udd.Value;
                    udd.Deleted = true;
                    
                    // 當有資料才放入刪除
                    if(!string.IsNullOrEmpty(udd.UID))
                        _DeleteDataList.Add(udd);

                    prlp.SetBeforeSaveText(udd.FieldName+"欄位名稱",udd.FieldName);
                    prlp.SetBeforeSaveText(udd.FieldName+"值", udd.Value);                   
                }
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show(ex.Message);
            }

            this.CancelButtonVisible = false;
            this.SaveButtonVisible = false;
            this.ContentValidated = true;

            ChangeManager.Reset();
            ChangeManager.ResumeListen();
            this.Loading = false;
            //dgv.Columns[FieldName.Index].ReadOnly = true;
        }

        /// <summary>
        /// 按下儲存
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSaveButtonClick(EventArgs e)
        {
            try
            {
                // 檢查資料
                bool canSave = true;
                dgv.EndEdit();
                foreach (DataGridViewRow drv in dgv.Rows)
                {
                    foreach (DataGridViewCell cell in drv.Cells)
                        if (cell.ErrorText != "")
                            canSave = false;
                }

                if (canSave)
                {

                    // 刪除舊資料 UDT
                    if (_DeleteDataList.Count > 0)
                    {
                        // 真實刪除
                        foreach (DAL.UserDefData ud in _DeleteDataList)
                            ud.Deleted = true;
                        UDTTransfer.DeleteDataToUDT(_DeleteDataList);
                    }
                    _InsertDataList.Clear();

                    // 儲存資料到 UDT
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow)
                            continue;
                        DAL.UserDefData udd = new UserDefineData.DAL.UserDefData();
                        // 資料轉型                      
                        if (row.Cells[Value.Index].Value != null)
                            udd.Value = row.Cells[Value.Index].Value.ToString();
                        udd.RefID = PrimaryKey;

                        string key = string.Empty;
                        if (row.Cells[FieldName.Index].Value != null)
                        {
                            key = row.Cells[FieldName.Index].Value.ToString();
                            udd.FieldName = key;
                        }

                        prlp.SetAfterSaveText(key + "欄位名稱", key);
                        prlp.SetAfterSaveText(key + "值", udd.Value);


                        _InsertDataList.Add(udd);
                    }

                    // 新增至 UDT
                    UDTTransfer.InsertDataToUDT(_InsertDataList);

                    if (LoadManager.GetSystemType() == SystemType.國中)
                    {
                        prlp.SetActionBy("學生", "自訂資料欄位");
                        prlp.SetAction("修改自訂資料欄位");
                        StudentRecord studRec = Student.SelectByID(PrimaryKey);
                        prlp.SetDescTitle("學生姓名:" + studRec.Name + ",學號:" + studRec.StudentNumber + ",");
                        prlp.SaveLog("", "", "student", PrimaryKey);
                    }

                }
                
                this.CancelButtonVisible = false;
                this.SaveButtonVisible = false;
                _BGWorker.RunWorkerAsync(); 
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show("儲存失敗!"+ex.Message);
            }
        }

        /// <summary>
        /// 更換學生
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            this.Loading = true;
            this.CancelButtonVisible = false;
            this.SaveButtonVisible = false;

            if (_BGWorker.IsBusy)
            {
                _isBusy = true;
                return;
            }
            else
                _BGWorker.RunWorkerAsync();    
        }

        /// <summary>
        /// 按下取消
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCancelButtonClick(EventArgs e)
        {
            this.CancelButtonVisible = false;
            this.SaveButtonVisible = false;
            _BGWorker.RunWorkerAsync(); 
        }

        private void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgv.EndEdit();
            bool validated = true;
            _CheckSameList.Clear();

            // 檢查資料
            foreach (DataGridViewRow row in dgv.Rows)
            {
                // 清空錯誤訊息
                row.Cells[FieldName.Index].ErrorText = "";
                row.Cells[Value.Index].ErrorText = "";

                string FName = string.Empty;
                if (row.IsNewRow)
                    continue;
                decimal dd;
                DateTime dt;

                if (row.Cells[FieldName.Index].Value != null)
                    FName = row.Cells[FieldName.Index].Value.ToString();

                if (FName != string.Empty)
                {
                    if (_UseDefineDataType.ContainsKey(FName))
                    {
                        if (row.Cells[Value.Index].Value == null)
                            continue;

                        if (row.Cells[Value.Index].Value.ToString() == string.Empty)
                            continue;

                        string str = row.Cells[Value.Index].Value.ToString();
                        if (_UseDefineDataType[FName] == "Number")
                        {
                            if (!decimal.TryParse(str, out dd))
                            {
                                row.Cells[Value.Index].ErrorText = "非數字型態";
                                validated = false;
                            }
                        }

                        if (_UseDefineDataType[FName] == "Date")
                        {
                            if (!DateTime.TryParse(str, out dt))
                            {
                                row.Cells[Value.Index].ErrorText = "非日期型態";
                                validated = false;
                            }
                        }
                    }
                }
                else
                {
                    row.Cells[FieldName.Index].ErrorText = "不允許空白";
                    validated = false;
                }
            }

            dgv.BeginEdit(false);
            this.ContentValidated = validated;
            if (validated)
            {
                this.SaveButtonVisible = true;
                this.CancelButtonVisible = true;
            }
            else
            {
                this.SaveButtonVisible = true;
                this.CancelButtonVisible = true;
            }

        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool validated = true;
            _CheckSameList.Clear();

            // 檢查資料
            foreach (DataGridViewRow row in dgv.Rows)
            {
                // 清空錯誤訊息
                row.Cells[FieldName.Index].ErrorText = "";
                row.Cells[Value.Index].ErrorText = "";

                string FName = string.Empty;
                if (row.IsNewRow)
                    continue;

                if (row.Cells[FieldName.Index].Value != null)
                    FName = row.Cells[FieldName.Index].Value.ToString();

                if (_CheckSameList.Contains(FName))
                {
                    row.Cells[FieldName.Index].ErrorText = "欄位名稱重複";
                    validated = false;
                }

                _CheckSameList.Add(FName);
            }

            foreach (DataGridViewRow row in dgv.Rows)
                foreach (DataGridViewCell cell in row.Cells )
                    if (cell.ErrorText != string.Empty)
                    {
                        validated = false;
                        break;
                    }
            this.ContentValidated = validated;
            if (validated)
            {
                this.SaveButtonVisible = true;
                this.CancelButtonVisible = true;
            }
            else
            {
                this.SaveButtonVisible = true;
                this.CancelButtonVisible = true;
            }
        }

        private void dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                bool validated = true;
                _CheckSameList.Clear();

                // 檢查資料
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    // 清空錯誤訊息
                    row.Cells[FieldName.Index].ErrorText = "";
                    row.Cells[Value.Index].ErrorText = "";

                    string FName = string.Empty;
                    if (row.IsNewRow)
                        continue;

                    if (row.Cells[FieldName.Index].Value != null)
                        FName = row.Cells[FieldName.Index].Value.ToString();

                    if (_CheckSameList.Contains(FName))
                    {
                        row.Cells[FieldName.Index].ErrorText = "欄位名稱重複";
                        validated = false;
                    }

                    _CheckSameList.Add(FName);
                }

                foreach (DataGridViewRow row in dgv.Rows)
                    foreach (DataGridViewCell cell in row.Cells)
                        if (cell.ErrorText != string.Empty)
                        {
                            validated = false;
                            break;
                        }
                this.ContentValidated = validated;
                if (validated)
                {
                    this.SaveButtonVisible = true;
                    this.CancelButtonVisible = true;
                }
                else
                {
                    this.SaveButtonVisible = true;
                    this.CancelButtonVisible = true;
                }
            }
        }
    }
}
