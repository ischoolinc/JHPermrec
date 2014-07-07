using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.API.PlugIn;

namespace JHPermrec.UpdateRecord.ImportExport
{
    // 匯出休學異動
    class ExportUpdateRecCode05:SmartSchool.API.PlugIn.Export.Exporter  
    {
        // 可勾選選項
        List<string> ExportItemList;

        public ExportUpdateRecCode05()
        {
            this.Image = null;
            this.Text = "匯出休學異動";
            ExportItemList = new List<string>();
            ExportItemList.Add("學年度");
            ExportItemList.Add("學期");
            ExportItemList.Add("異動年級");
            ExportItemList.Add("異動日期");
            ExportItemList.Add("備註");
            ExportItemList.Add("異動班級");
            ExportItemList.Add("異動姓名");
            ExportItemList.Add("異動身分證號");
            ExportItemList.Add("異動學號");
            ExportItemList.Add("異動性別");
            ExportItemList.Add("異動生日");
            ExportItemList.Add("異動地址");
            ExportItemList.Add("核准日期");
            ExportItemList.Add("核准文號");
            ExportItemList.Add("原因及事項");
            ExportItemList.Add("學籍核准日期");
            ExportItemList.Add("學籍核准文號");
            ExportItemList.Add("異動座號");
            ExportItemList.Add("異動類別");
        }

        public override void InitializeExport(SmartSchool.API.PlugIn.Export.ExportWizard wizard)
        {
            wizard.ExportableFields.AddRange(ExportItemList);
            wizard.ExportPackage += delegate(object sender, SmartSchool.API.PlugIn.Export.ExportPackageEventArgs e)
            {
                 Dictionary<string, List<DAL.StudUpdateRecordEntity>> StudUpdateRecordEntityListDic = DAL.DALTransfer2.GetStudListUpdateRecordEntityListByUpdateType(e.List, JHPermrec.UpdateRecord.DAL.DALTransfer2.UpdateType.休學);
                int ExportCount = 0;
                foreach (KeyValuePair<string, List<DAL.StudUpdateRecordEntity>> sureKey in StudUpdateRecordEntityListDic)
                    foreach (DAL.StudUpdateRecordEntity sure in sureKey.Value)
                    {
                        RowData row = new RowData();

                        row.ID = sure.StudentID;

                        foreach (string field in e.ExportFields)
                        {
                            if (wizard.ExportableFields.Contains(field))
                            {
                                switch (field)
                                {
                                    case "學年度":
                                        if (sure.SchoolYear > 0)
                                            row.Add(field, "" + sure.SchoolYear); break;
                                    case "學期":
                                        if (sure.Semester > 0)
                                            row.Add(field, "" + sure.Semester); break;
                                    case "異動年級": row.Add(field, "" + sure.GetGradeYear()); break;

                                    case "異動日期":
                                        if (sure.GetUpdateDate().HasValue)
                                            row.Add(field, sure.GetUpdateDate().Value.ToShortDateString()); break;
                                    case "備註": row.Add(field, sure.GetComment()); break;
                                    case "異動班級": row.Add(field, sure.GetClassName()); break;
                                    case "異動姓名": row.Add(field, sure.GetName()); break;
                                    case "異動身分證號": row.Add(field, sure.GetIDNumber()); break;
                                    case "異動學號": row.Add(field, sure.GetStudentNumber()); break;
                                    case "異動性別": row.Add(field, sure.GetGender()); break;
                                    case "異動生日":
                                        if (sure.GetBirthday().HasValue)
                                            row.Add(field, sure.GetBirthday().Value.ToShortDateString()); break;
                                    case "異動地址": row.Add(field, sure.GetAddress()); break;
                                    case "核准日期":
                                        if (sure.GetADDate().HasValue)
                                            row.Add(field, sure.GetADDate().Value.ToShortDateString()); break;
                                    case "核准文號": row.Add(field, sure.GetADNumber()); break;
                                    case "原因及事項": row.Add(field, sure.GetUpdateDescription()); break;
                                    case "學籍核准日期":
                                        if (sure.GetLastADDate().HasValue)
                                            row.Add(field, sure.GetLastADDate().Value.ToShortDateString()); break;
                                    case "學籍核准文號": row.Add(field, sure.GetLastADNumber()); break;
                                    case "異動座號": row.Add(field, sure.GetSeatNo()); break;
                                    case "異動類別": row.Add(field, "休學"); break;
                                }
                            }
                        }
                        ExportCount++;
                        e.Items.Add(row);
                    }
                JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
                prlp.SaveLog("學生.匯出異動", "匯出休學異動", "共匯出休學異動" + ExportCount + "筆資料.");
            };

        }
    }
}
