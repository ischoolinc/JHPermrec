using System;
using System.Windows.Forms;
using FISCA.DSAUtil;
using Framework;
using JHSchool.Permrec.Feature.Legacy;
using System.Xml;

namespace JHPermrec.UpdateRecord.GovernmentalDocument.NameList
{
    public partial class UpdateRecordADN : FISCA.Presentation.Controls.BaseForm
    {
        //private string _id;
        public event EventHandler DataSaved;

        public UpdateRecordADN()
        {
            InitializeComponent();
        }

        private ISummaryProvider _provider;
        internal void Initialize(ISummaryProvider provider)
        {
            _provider = provider;
            txtNumber.Text = provider.ADNumber;
            dtpDate.Text = provider.ADDate;
            CheckSaveable();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumber.Text))
            {
                MsgBox.Show("請輸入核準文號。");
                return;
            }

            DateTime date;
            if (!DateTime.TryParse(dtpDate.DateString, out date))
            {
                MsgBox.Show("日期格式不正確。");
                return;
            }

            // 修改名冊本身的日期與文號
            DSXmlHelper helper = new DSXmlHelper("AuthorizeBatchRequest");
            helper.AddElement("AuthorizeBatch");
            helper.AddElement("AuthorizeBatch", "Field");
            helper.AddElement("AuthorizeBatch/Field", "ADNumber", txtNumber.Text);
            helper.AddElement("AuthorizeBatch/Field", "ADDate", dtpDate.DateString);
            helper.AddElement("AuthorizeBatch", "Condition");
            helper.AddElement("AuthorizeBatch/Condition", "ID", _provider.ID);

            try
            {
                EditStudent.ModifyUpdateRecordBatch(new DSRequest(helper));
                
            }
            catch (Exception ex)
            {
                MsgBox.Show("編輯核准文號失敗：" + ex);
            }

            // 修改其包含的異動紀錄文號
            helper = new DSXmlHelper("UpdateRequest");
            helper.AddElement("UpdateRecord");
            helper.AddElement("UpdateRecord", "Field");
            helper.AddElement("UpdateRecord/Field", "ADNumber", txtNumber.Text);
            helper.AddElement("UpdateRecord/Field", "ADDate", dtpDate.DateString);
            helper.AddElement("UpdateRecord", "Condition");

            if (_provider.GetEntities().Length <= 0) //名冊中沒有任何學生，就不更新學生的核準文號了。
                return;

            foreach (IEntryFormat entity in _provider.GetEntities())
                helper.AddElement("UpdateRecord/Condition", "ID", entity.ID);

            try
            {
                EditStudent.ModifyUpdateRecord(new DSRequest(helper));
                if (DataSaved != null)
                    DataSaved(this, null);
            }
            catch (Exception ex)
            {
                MsgBox.Show("編輯核准文號失敗：" + ex);
            }


            string batchName = "";
            string schoolYear = "";
            string semester = "";

            if (_provider.ID != "")
            {
                DSResponse dsrsp = QueryStudent.GetUpdateRecordBatch(_provider.ID);

                DSXmlHelper helper_ = dsrsp.GetContent();

                //填上名冊的 學年、學期、名稱
                foreach (XmlNode node in helper_.GetElements("UpdateRecordBatch"))
                {
                    schoolYear = node.SelectSingleNode("SchoolYear").InnerText;
                    semester = node.SelectSingleNode("Semester").InnerText;
                    batchName = node.SelectSingleNode("Name").InnerText;
                }
                // log，2018/3/1 穎驊新增，因應高雄 [10-03][01] 整個學年度核准過的文號的異動名冊全部不見了 項目
                // 本異動名冊 原只有新增會有系統紀錄，現在調整刪除、登打文號都會有紀錄
                JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
                string desc = "登錄文號" + schoolYear + "學年度,第" + semester + "學期," + batchName + "名冊,日期:" + dtpDate.DateString + ",文號:"+ txtNumber.Text;
                prlp.SaveLog("教務.名冊", "登錄文號", desc);
               
            }

       


            this.Close();
            
        }

        private void UpdateRecordADN_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void dtpDate_TextChanged(object sender, EventArgs e)
        {
            CheckSaveable();
        }

        private void CheckSaveable()
        {
            btnOK.Enabled = IsValid(); ;
        }

        private bool IsValid()
        {
            if (!dtpDate.IsValid)
                return false;
            return true;
        }
    }
}