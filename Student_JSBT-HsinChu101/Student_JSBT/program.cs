using System;
using System.Collections.Generic;
using System.Text;
using FISCA.Presentation;
using Framework;
using Framework.Security;

// 國中學生基測

namespace Student_JSBT_HsinChu101
{
    public class program
    {
        [FISCA.MainMethod()]
        public static void Main()
        { 
        
            // 產生學生基本學力資料
            //RibbonBarItem rbiEducationData = K12.Presentation.NLDPanels.Student.RibbonBarItems["學籍作業"];
            MenuButton rbiEducationData = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["報表"]["學籍相關報表"];
            rbiEducationData["產生竹苗區免試入學報名檔"].Enable = User.Acl["JHSchool.Student.Student_JSBT_HsinChu101"].Executable;
            rbiEducationData["產生竹苗區免試入學報名檔"].Click += delegate
            {

                if (K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0)
                {
                    MainForm mf = new MainForm();
                    mf.ShowDialog();
                }
                else
                    FISCA.Presentation.Controls.MsgBox.Show("請選擇學生");
            };

            Catalog catalog = RoleAclSource.Instance["學生"]["報表"];
            catalog.Add(new RibbonFeature("JHSchool.Student.Student_JSBT_HsinChu101", "產生竹苗區免試入學報名檔"));
        }
    }
}
