using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Presentation;
using FISCA;
using FISCA.Permission;
using K12.Data;

namespace TaiChung.StudentRecordReport
{
    /// <summary>
    /// 台中學籍表
    /// </summary>
    public class Program
    {
        [MainMethod()]
        public static void Main()
        {
            string studRegStr="TaiChung.Student.StudentRecordReport";
            string classRegStr = "TaiChung.Class.StudentRecordReport";
            
            Catalog catalogStud = RoleAclSource.Instance["學生"]["報表"];
            catalogStud.Add(new ReportFeature(studRegStr, "台中學籍表"));

            Catalog catalogClass = RoleAclSource.Instance["班級"]["報表"];
            catalogClass.Add(new ReportFeature(classRegStr, "台中學籍表"));

            // 學生
            MenuButton mbStud = MotherForm.RibbonBarItems["學生", "資料統計"]["報表"]["成績相關報表"]["台中學籍表"];
            mbStud.Enable = UserAcl.Current[studRegStr].Executable;
            mbStud.Click += delegate {
                if (K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0)
                {
                    List<string> studIDList = Utility.GetStudentIDListByStudentID(K12.Presentation.NLDPanels.Student.SelectedSource);
                    PrintForm pf = new PrintForm(studIDList);
                    pf.ShowDialog();
                }
                else
                {
                    FISCA.Presentation.Controls.MsgBox.Show("請選擇學生");
                    return;
                }
            };

            // 班級
            MenuButton mbClass = MotherForm.RibbonBarItems["班級", "資料統計"]["報表"]["成績相關報表"]["台中學籍表"];
            mbClass.Enable = UserAcl.Current[studRegStr].Executable;
            mbClass.Click += delegate
            {
                if (K12.Presentation.NLDPanels.Class.SelectedSource.Count > 0)
                {
                    PrintForm pr = new PrintForm(Utility.GetStudentIDList18ByClassID(K12.Presentation.NLDPanels.Class.SelectedSource));
                    pr.ShowDialog();
                }
                else
                {
                    FISCA.Presentation.Controls.MsgBox.Show("請選擇班級");
                    return;
                }

            };
        }
    }
}
