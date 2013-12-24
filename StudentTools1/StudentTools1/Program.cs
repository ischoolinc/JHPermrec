using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA;
using Framework;

namespace StudentTools1
{
    public class Program
    {
        [FISCA.MainMethod ()]
        public static void Main()
        {

            K12.Presentation.NLDPanels.Student.ListPaneContexMenu["產生常用學生基本資料"].Enable = User.Acl["JHSchool.Student.Ribbon0030"].Executable;
            K12.Presentation.NLDPanels.Student.ListPaneContexMenu["產生常用學生基本資料"].Click += delegate 
            {
                if (K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0)
                {
                    Form.StudentDataExportForm sdef = new StudentTools1.Form.StudentDataExportForm();
                    sdef.ShowDialog();
                }
            };

            K12.Presentation.NLDPanels.Student.ListPaneContexMenu["刪除學期歷程"].Enable = User.Acl["JHSchool.Student.Ribbon0030"].Executable;
            K12.Presentation.NLDPanels.Student.ListPaneContexMenu["刪除學期歷程"].Click += delegate
            {
                if (K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0)
                {
                    Form.DelStudentHistory dsh = new Form.DelStudentHistory();
                    dsh.ShowDialog();
                }
            };   
        }
    }
}
