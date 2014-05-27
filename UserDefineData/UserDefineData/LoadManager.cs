using System;
using System.Collections.Generic;
using System.Text;
using FISCA.Presentation;
using FISCA.Permission;
using K12.Presentation;

namespace UserDefineData
{
    public enum SystemType { 高中, 國中 }

    public class LoadManager
    {

        // 設定權限字串
        private static string strSetUserDefineDataAcl = "Student.ischool_UserDefineData_SetUserDefineDataForm";
        private static string strUserDefineDataImportAcl = "Student.ischool_UserDefineData_Import";
        private static string strUserDefineDataExportAcl = "Student.ischool_UserDefineData_Export";

        /// <summary>
        /// 取的登入系統是高中或國中
        /// </summary>
        /// <returns></returns>
        public static SystemType GetSystemType()
        {
            K12.Data.Configuration.ConfigData cd = K12.Data.School.Configuration["ischool_Metadata"];
            if (cd["EducationalSystem"] == "SeniorHighSchool")
                return SystemType.高中;
            else
                return SystemType.國中;
        }

        /// <summary>
        /// 啟動
        /// </summary>
        public static void Start()
        {
            Global.StartDefaultValue();
            if (GetSystemType() == SystemType.高中)
                StartSHSchool();
            else
                StartJHSchool();
        }

        // 啟用高中
        private static void StartSHSchool()
        { 
        

        }

        // 啟用國中
        private static void StartJHSchool()
        {

            // 註冊與載入自訂資料欄位


            // 註冊權限
            Catalog StudUserDefineDataFuncButtonRoleAcl = RoleAclSource.Instance["學生"]["功能按鈕"];
            StudUserDefineDataFuncButtonRoleAcl.Add(new RibbonFeature(strSetUserDefineDataAcl , "設定自訂資料欄位樣版"));
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
                    
                    SmartSchool.API.PlugIn.Export.Exporter exporter = new ImportExport.ExportUserDefData ();
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
