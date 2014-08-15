
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Framework.Legacy;
using IntelliSchool.DSA.ClientFramework.ControlCommunication;
using JHSchool.Permrec.Feature.Legacy;

namespace JHPermrec.UpdateRecord.GovernmentalDocument.NameList
{
    public partial class BuildWizard : FISCA.Presentation.Controls.BaseForm
    {
        INameListProvider _NameListProvider;
        BackgroundWorker _BackgroundWorker = new BackgroundWorker();

        public BuildWizard()
        {
            InitializeComponent();

            
            
            comboBoxEx1.Items.AddRange(new object[] { Framework.Legacy.GlobalOld.SystemConfig.DefaultSchoolYear.ToString(), (GlobalOld.SystemConfig.DefaultSchoolYear - 1).ToString() });
            comboBoxEx2.Items.AddRange(new object[] { "1","2"});            
            comboBoxEx1.Text = GlobalOld.SystemConfig.DefaultSchoolYear.ToString();
            comboBoxEx2.Text = GlobalOld.SystemConfig.DefaultSemester.ToString();

            // 這是產生名冊選項需分三縣市

            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.HsinChu)
            {
                // 新生
                AddProvider(new EnrollmentListProvider());
                // 畢業
                AddProvider(new GraduatingStudentListProvider());
                // 轉入
                AddProvider(new TransferringStudentUpdateRecordListProvider());
                // 轉出
                AddProvider(new Transferring01StudentUpdateRecordListProvider());
                // 更正學籍
                AddProvider(new UpdateStudentPermrecListProvider());
            }

            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.KaoHsiung)
            {
                // 新生
                AddProvider(new EnrollmentListProvider());
                // 畢業
                AddProvider(new GraduatingStudentListProvider());
                // 轉入
                AddProvider(new TransferringStudentUpdateRecordListProvider());
                //// 中輟
                AddProvider(new ExtendingStudentListProvider());
                // 轉出
                AddProvider(new Transferring01StudentUpdateRecordListProvider());
                // 復學
                AddProvider(new ResumeStudentUpdateRecordListProvider());
                //休學
                AddProvider(new SuspensionStudentUpdateRecordListProvider());
                //// 續讀
                AddProvider(new ContinueStudentUpdateRecordListProvider());

                // 更正學籍
                //AddProvider(new UpdateStudentPermrecListProvider());

                // 死亡
                AddProvider(new DeadStudentListProvider());
            }

            // 台中縣
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.TaiChung)
            {
                // 新生
                AddProvider(new EnrollmentListProvider());
                // 畢業
                AddProvider(new GraduatingStudentListProvider());
                // 轉入
                AddProvider(new TransferringStudentUpdateRecordListProvider());
                // 轉出
                AddProvider(new Transferring01StudentUpdateRecordListProvider());            
            }


            lvSorter1.ColumnSorter.Add(columnHeader3, SortType.Int);
            lvSorter1.ColumnSorter.Add(columnHeader6, SortType.Int);
            lvSorter1.ColumnSorter.Add(columnHeader5, SortType.DateTime);
            lvSorter1.ColumnSorter.Add(columnHeader1, SortType.Int);
            lvSorter1.ColumnSorter.Add(columnHeader2, SortType.String);
            lvSorter1.ColumnSorter.Add(columnHeader4, SortType.String);
            lvSorter1.ColumnSorter.Add(columnHeader7, SortType.String);
            lvViewManager1.SetDynamicGrouping(columnHeader1);
            lvViewManager1.SetDynamicGrouping(columnHeader2);
            lvViewManager1.SetDynamicGrouping(columnHeader5);
            lvViewManager1.SetDynamicGrouping(columnHeader6);
           

        }

        private void AddProvider(INameListProvider nameListProvider)
        {
            // 高雄特別移除無法新增中輟與續讀名冊
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.KaoHsiung)
            {
                if (nameListProvider.Title != "中輟學生名冊" && nameListProvider.Title != "續讀學生名冊")
                {
                    System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(nameListProvider.Title, 0);
                    listViewItem.Tag = nameListProvider;
                    listView1.Items.Add(listViewItem);
                }            
            }
            else
            {
                System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(nameListProvider.Title, 0);
                listViewItem.Tag = nameListProvider;
                listView1.Items.Add(listViewItem);            
            }


            //System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(nameListProvider.Title, 0);
            //listViewItem.Tag = nameListProvider;
            //listView1.Items.Add(listViewItem);
            
        }

        #region 下一步
        private void BuildWizard_Load(object sender, EventArgs e)
        {
            int xm, ym;
            xm = (this.Width - panelEx1.Width - 20) / 2;
            ym = (this.Height - panelEx1.Height - 40) / 2;
            this.Top += ym;
            this.Left += xm;
            this.SuspendLayout();
            panelEx1.SuspendLayout();
            panelEx2.SuspendLayout();
            panelEx3.SuspendLayout();
            this.Height = panelEx1.Height + 40;
            this.Width = panelEx1.Width + 20;
            panelEx1.Visible = true;
            panelEx2.Visible = false;
            panelEx3.Visible = false;
            panelEx1.Dock = DockStyle.Fill;
            panelEx2.Dock = DockStyle.None;
            panelEx3.Dock = DockStyle.None;
            panelEx1.ResumeLayout();
            panelEx2.ResumeLayout();
            panelEx3.ResumeLayout();
            
            this.ResumeLayout();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            int xm, ym;
            xm = (this.Width - panelEx2.Width - 20) / 2;
            ym = (this.Height - panelEx2.Height - 40) / 2;
            this.Top += ym;
            this.Left += xm;
            this.SuspendLayout();
            panelEx1.SuspendLayout();
            panelEx2.SuspendLayout();
            panelEx3.SuspendLayout();
            this.Height = panelEx2.Height + 40;
            this.Width = panelEx2.Width + 20;
            panelEx1.Visible = false;
            panelEx2.Visible = true;
            panelEx3.Visible = false;
            panelEx1.Dock = DockStyle.None;
            panelEx2.Dock = DockStyle.Fill;
            panelEx3.Dock = DockStyle.None;
            this.Visible = true;
            panelEx1.ResumeLayout();
            panelEx2.ResumeLayout();
            panelEx3.ResumeLayout();
            this.ResumeLayout();
            _NameListProvider = (INameListProvider)listView1.SelectedItems[0].Tag;
            listView2.Items.Clear();

            _BackgroundWorker.DoWork += new DoWorkEventHandler(_BackgroundWorker_DoWork);
            _BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BackgroundWorker_RunWorkerCompleted);
            _BackgroundWorker.WorkerSupportsCancellation = true;
            _BackgroundWorker.RunWorkerAsync();
            
            comboBoxEx1_TextChanged(null, null);
        }

        void _BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dictionary<string, string> ClassGradeIdx = new Dictionary<string, string>();

            ClassGradeIdx=DAL.DALTransfer.GetUpateRecordStudentGradeYearList((List<XmlElement>)e.Result);

            Dictionary<string, string> StudGradeIdx = new Dictionary<string, string>();

            foreach (JHSchool.Data.JHStudentRecord studRec in JHSchool.Data.JHStudent.SelectAll())
            { 
                string grYear=string.Empty ;

                // 過濾已刪除學生
                if (studRec.Status == K12.Data.StudentRecord.StudentStatus.刪除)
                    continue;

                if (studRec.Class != null)
                    if (studRec.Class.GradeYear.HasValue)
                        grYear = studRec.Class.GradeYear.Value + "";
                StudGradeIdx.Add(studRec.ID, grYear);
            }

            if (!e.Cancelled)
            {
                pictureBox1.Visible = false;
                listView2.SuspendLayout();
                foreach (XmlElement var in (List<XmlElement>)e.Result)
                {
                    string StudID = var.GetAttribute("RefStudentID");

                    // 過濾刪除學生
                    if (!StudGradeIdx.ContainsKey(StudID))
                        continue;

                    string strGradeYear="";
                    if (ClassGradeIdx.ContainsKey(StudID))
                        strGradeYear = ClassGradeIdx[StudID];
                    else
                    {
                        if (StudGradeIdx.ContainsKey(StudID))
                            strGradeYear = StudGradeIdx[StudID];
                    }

                    ListViewItem item = new ListViewItem(new string[] { 
                        var.SelectSingleNode("UpdateCode").InnerText,
                        var.SelectSingleNode("UpdateDate").InnerText,
                        //var.SelectSingleNode("GradeYear").InnerText,
                        strGradeYear,
                        var.SelectSingleNode("Department").InnerText,
                        var.SelectSingleNode("StudentNumber").InnerText,
                        var.SelectSingleNode("Name").InnerText,
                        var.SelectSingleNode("UpdateDescription").InnerText
                    });
                    item.Tag = var;
                    listView2.Items.Add(item);
                }
                listView2.ResumeLayout();
                cboGradeYear.Items.Clear();
                List<string> tmp = new List<string>();

                foreach (ListViewItem lvi in listView2.Items)
                {
                    if (!tmp.Contains(lvi.SubItems[2].Text))
                        tmp.Add(lvi.SubItems[2].Text);                
                }
                tmp.Sort();
                foreach (string str in tmp)
                    cboGradeYear.Items.Add(str);
                
                dateTimeTextBox1_TextChanged(null, null);
            }
        }

        void _BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = _NameListProvider.GetExpectantList();
            if (_BackgroundWorker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            int xm, ym;
            xm = (this.Width - panelEx3.Width - 20) / 2;
            ym = (this.Height - panelEx3.Height - 40) / 2;
            this.Top += ym;
            this.Left += xm;
            this.SuspendLayout();
            panelEx1.SuspendLayout();
            panelEx2.SuspendLayout();
            panelEx3.SuspendLayout();
            this.Height = panelEx3.Height + 40;
            this.Width = panelEx3.Width + 20;
            panelEx1.Visible = false;
            panelEx2.Visible = false;
            panelEx3.Visible = true;
            panelEx1.Dock = DockStyle.None;
            panelEx2.Dock = DockStyle.None;
            panelEx3.Dock = DockStyle.Fill;
            this.Visible = true;
            panelEx1.ResumeLayout();
            panelEx2.ResumeLayout();
            panelEx3.ResumeLayout();
            
            this.ResumeLayout();

            // log
            JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
            string desc = "新增" + comboBoxEx1.Text + "學年度,第" + comboBoxEx2.Text + "學期," + txtNameListName.Text + "名冊";
            prlp.SaveLog("教務.名冊", "新增",desc);

        } 
        #endregion

        private void Exit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxEx1_TextChanged(object sender, EventArgs e)
        {
            if (_NameListProvider != null)
                txtNameListName.Text = comboBoxEx1.Text + "_" + comboBoxEx2.Text + "_" + _NameListProvider.Title;
            int i = 0;

            dateTimeTextBox1.Text = dateTimeTextBox2.Text = "";
            if (int.TryParse(comboBoxEx1.Text, out i))
            {
                if (comboBoxEx2.Text == "1")
                {
                    dateTimeTextBox1.Text = "" + (i + 1911) + "/8/1";
                    dateTimeTextBox2.Text = "" + (i + 1911 + 1) + "/1/31";
                }
                if (comboBoxEx2.Text == "2")
                {
                    dateTimeTextBox1.Text = "" + (i + 1911 + 1) + "/2/1";
                    dateTimeTextBox2.Text = "" + (i + 1911 + 1) + "/7/31";
                }
                buttonX3.Enabled = true;
            }
            else
                buttonX3.Enabled = false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonX1.Enabled = (listView1.SelectedItems.Count == 1);
        }

        private void dateTimeTextBox1_TextChanged(object sender, EventArgs e)
        {
            DateTime begin = DateTime.Now;
            DateTime end = DateTime.Now;
            DateTime updateDate = DateTime.Now;
            if (DateTime.TryParse(dateTimeTextBox1.Text, out begin) && DateTime.TryParse(dateTimeTextBox2.Text, out end))
            {
                // 未分年級
                if (chkGradeYear.Checked)
                {


                    foreach (ListViewItem var in listView2.Items)
                    {
                        XmlElement element = (XmlElement)var.Tag;

                        if (DateTime.TryParse(element.SelectSingleNode("UpdateDate").InnerText, out updateDate) && updateDate >= begin && updateDate <= end)
                        {
                            var.ForeColor = Color.Blue;
                            var.Checked = true;
                        }
                        else
                        {
                            var.ForeColor = listView2.ForeColor;
                            var.Checked = false;
                        }
                    }
                }
                else
                {
                    // 使用年級分
                    foreach (ListViewItem var in listView2.Items)
                    {
                        XmlElement element = (XmlElement)var.Tag;

                        if (DateTime.TryParse(element.SelectSingleNode("UpdateDate").InnerText, out updateDate) && updateDate >= begin && updateDate <= end && var.SubItems[2].Text ==cboGradeYear.Text)
                        {
                            var.ForeColor = Color.Blue;
                            var.Checked = true;
                        }
                        else
                        {
                            var.ForeColor = listView2.ForeColor;
                            var.Checked = false;
                        }
                    }                
                }
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            // 檢查勾選學生人數
            int checkSelectCount = 0;
            List<XmlElement> list = new List<XmlElement>();
            foreach (ListViewItem var in listView2.Items)
            {
                if (var.Checked)
                {
                    list.Add((XmlElement)var.Tag);
                    checkSelectCount++;
                }
            }

            // 當沒有勾選學生
            if (checkSelectCount == 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請勾選學生..");
                return;
            }

            UpdateRecordBatch.InsertUpdateRecordBatch(txtNameListName.Text, comboBoxEx1.Text, comboBoxEx2.Text, _NameListProvider.CreateNameList(comboBoxEx1.Text, comboBoxEx2.Text, list));
            //_NameListProvider.CreateNameList(comboBoxEx1.Text, comboBoxEx2.Text, list).OwnerDocument.Save("D:\\1234.xml");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BuildWizard_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_BackgroundWorker.IsBusy)
            {
                _BackgroundWorker.CancelAsync();
            }
            if (this.DialogResult != DialogResult.OK)
                this.DialogResult = DialogResult.Cancel;
        }

        private void chkGradeYear_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGradeYear.Checked)
                cboGradeYear.Enabled = false;
            else
                cboGradeYear.Enabled = true;
            dateTimeTextBox1_TextChanged(null, null);
        }
    }
}
