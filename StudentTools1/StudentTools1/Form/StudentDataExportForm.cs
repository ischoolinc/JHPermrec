using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudentTools1.Form
{
    public partial class StudentDataExportForm : FISCA.Presentation.Controls.BaseForm 
    {
        public StudentDataExportForm()
        {
            InitializeComponent();
        }

        private void StudentDataExportForm_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.MinimumSize = this.Size;
            // 預設排序
            chkSortByClassSeatNo.Checked = true;
            labelX1.Visible = false;
            // 選項初始
            lvSelItems.Items.Add("性別");
            lvSelItems.Items.Add("身分證號");
            lvSelItems.Items.Add("完整戶籍地址");
            lvSelItems.Items.Add("完整聯絡地址");
            lvSelItems.Items.Add("監護人姓名");
            lvSelItems.Items.Add("戶籍電話");
            lvSelItems.Items.Add("聯絡電話");
            lvSelItems.Items.Add("西元生日(年/月/日)");
            lvSelItems.Items.Add("民國生日(0年/0月/0日)");
            lvSelItems.Items.Add("民國生日(0年0月0日)");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // 檢查畫面是否勾選
            if (lvSelItems.CheckedItems.Count <1)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請勾選匯出資料");
                return;
            }
            if (chkSortByClassSeatNo.Checked == false && chkSortByStudNum.Checked == false)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請選擇排序方式");
                return;
            }

            if (K12.Presentation.NLDPanels.Student.SelectedSource.Count == 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請選擇學生");
                return;
            }

            btnExport.Enabled = false;
            labelX1.Visible = true;
            // 取得學生資料
            List<DAL.StudDataEntity> StudDataList = DAL.DALTransfer.GetStudentDataList(K12.Presentation.NLDPanels.Student.SelectedSource);
             
            // 排序(班座)
            if (chkSortByClassSeatNo.Checked)
                StudDataList = DAL.DALTransfer.SortBy(StudDataList, DAL.DALTransfer.OrderBy.班座);

            // 排序(學號)
            if (chkSortByStudNum.Checked)
                StudDataList = DAL.DALTransfer.SortBy(StudDataList, DAL.DALTransfer.OrderBy.學號);

            // 產生報表
            // 欄位表頭
            List<Export.ColumnIndexEntity> ColumnList = new List<StudentTools1.Export.ColumnIndexEntity>();

            int ColIdx = 4;
            List<string> ColumnNameList =new List<string> ();
            // 基本提供
            ColumnNameList.Add("學號");
            ColumnNameList.Add("班級");
            ColumnNameList.Add("座號");
            ColumnNameList.Add("姓名");

            // 延伸
            foreach (ListViewItem lvi in lvSelItems.CheckedItems)
            {
                ColumnNameList.Add(lvi.Text);
            }


            foreach (string str in ColumnNameList)
            {
                Export.ColumnIndexEntity cieName = new StudentTools1.Export.ColumnIndexEntity();
                cieName.Visible = true;
                switch (str)
                { 
                    case "學號":
                        cieName.ColumnIndex = 0;
                        cieName.ColumnName = str;
                        ColumnList.Add(cieName);
                        break;

                    case "班級":
                        cieName.ColumnIndex = 1;
                        cieName.ColumnName = str;
                        ColumnList.Add(cieName);
                        break;

                    case "座號":
                        cieName.ColumnIndex =2;
                        cieName.ColumnName = str;
                        ColumnList.Add(cieName);
                        break;

                    case "姓名":
                        cieName.ColumnIndex = 3;
                        cieName.ColumnName = str;
                        ColumnList.Add(cieName);
                        break;
                
                    default :
                        cieName.ColumnIndex = ColIdx;
                        cieName.ColumnName = str;
                        ColumnList.Add(cieName);
                        ColIdx++;
                        break;

                }
            }
            // 產生到 Excel
            Export.StudDataExporter sde = new StudentTools1.Export.StudDataExporter(ColumnList,StudDataList);
            btnExport.Enabled = true;
            labelX1.Visible = false;
        }
    }
}
