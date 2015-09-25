using System;
using System.Collections.Generic;
using System.Text;
using FISCA;
using DataRationality;
using FISCA.Presentation;
using FISCA.Permission;

namespace UserDefineData
{        
    /// <summary>
    /// 自訂資料欄位
    /// </summary>
    public class Program
    {
        [MainMethod("UserDefineData")]
        public static void Main()
        {
            // 設定權限字串
            string strSetUserDefineDataAcl = "Student.ischool_UserDefineData_SetUserDefineDataForm";
            string strUserDefineDataImportAcl = "Student.ischool_UserDefineData_Import";
            string strUserDefineDataExportAcl = "Student.ischool_UserDefineData_Export";


            // 加入合理性檢查
            // 自訂欄位資料欄位重複
            try
            {
                DataRationalityManager.Checks.Add(new DoubleUserDefDataRAT());
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show("自訂資料欄位合理性檢查" + ex.Message);
            }

            // 註冊權限
            Catalog StudUserDefineDataFuncButtonRoleAcl = RoleAclSource.Instance["學生"]["功能按鈕"];
            StudUserDefineDataFuncButtonRoleAcl.Add(new RibbonFeature(strSetUserDefineDataAcl, "設定自訂資料欄位樣版"));
            StudUserDefineDataFuncButtonRoleAcl.Add(new RibbonFeature(strUserDefineDataExportAcl, "匯出自訂資料欄位"));
            StudUserDefineDataFuncButtonRoleAcl.Add(new RibbonFeature(strUserDefineDataImportAcl, "匯入自訂資料欄位"));


            // 設定自訂資料欄位樣版
            if (FISCA.Permission.UserAcl.Current[Global.自訂資料欄位功能代碼].Editable || FISCA.Permission.UserAcl.Current[Global.自訂資料欄位功能代碼].Viewable)
                K12.Presentation.NLDPanels.Student.AddDetailBulider(new FISCA.Presentation.DetailBulider<UserDefineDataItem>());

            Catalog detail = RoleAclSource.Instance["學生"]["資料項目"];
            detail.Add(new DetailItemFeature(typeof(UserDefineDataItem)));

            RibbonBarButton rbSetUserDefineData = K12.Presentation.NLDPanels.Student.RibbonBarItems["其它"]["自訂資料欄位管理"];

            rbSetUserDefineData.Image = Properties.Resources.windows_save_64;
            rbSetUserDefineData.Enable = UserAcl.Current[strSetUserDefineDataAcl].Executable;
            rbSetUserDefineData.Click += delegate
            {
                SetUserDefineDataForm sudd = new SetUserDefineDataForm();
                sudd.ShowDialog();

            };


            // 匯出匯入自訂資料欄位            
            MenuButton rbUserDefDataExport = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["匯出"]["其它相關匯出"];
            MenuButton rbUserDefDataImport = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["匯入"]["其它相關匯入"];

            // 匯出自訂資料欄位
            rbUserDefDataExport["匯出自訂資料欄位"].Enable = UserAcl.Current[strUserDefineDataExportAcl].Executable;
            rbUserDefDataExport["匯出自訂資料欄位"].Click += delegate
            {

                SmartSchool.API.PlugIn.Export.Exporter exporter = new ImportExport.ExportUserDefData();
                ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                exporter.InitializeExport(wizard);
                wizard.ShowDialog();
            };

            // 匯入自訂資料欄位
            rbUserDefDataImport["匯入自訂資料欄位"].Enable = UserAcl.Current[strUserDefineDataImportAcl].Executable;
            rbUserDefDataImport["匯入自訂資料欄位"].Click += delegate
            {
                SmartSchool.API.PlugIn.Import.Importer importer = new ImportExport.ImportUserDefData();
                ImportExport.ImportStudentV2 wizard = new ImportExport.ImportStudentV2(importer.Text, importer.Image);
                importer.InitializeImport(wizard);
                wizard.ShowDialog();
            };

        }
    }
}
