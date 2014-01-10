using System;
using System.Collections.Generic;
using System.Text;
using JHSchool;
using Framework.Security;
using Framework;
using FISCA.Presentation;
using JHSchool.Affair;

namespace JHPermrec.UpdateRecord
{
    public class Program
    {
        [FISCA.MainMethod()]
        public static void Main()
        {
            // 異動匯入匯出是否可以使用
            bool CanUseUpRecIE01, CanUseUpRecIE02, CanUseUpRecIE03, CanUseUpRecIE04, CanUseUpRecIE05, CanUseUpRecIE06, CanUseUpRecIE07, CanUseUpRecIE08, CanUseUpRecIE09;
            CanUseUpRecIE01 = CanUseUpRecIE02 = CanUseUpRecIE03 = CanUseUpRecIE04 = CanUseUpRecIE05 = CanUseUpRecIE06 = CanUseUpRecIE07 = CanUseUpRecIE08 = CanUseUpRecIE09 = false;

            // 更正學籍異動精靈是否可以使用
            bool CanUseWizardUpCode09 = false;


            // 新竹專用
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.HsinChu)
            {  // 學生異動資料
                Student.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<JHPermrec.UpdateRecord.UpdateRecordItem>());
                // 設定可使用的異動類別與新增時順序
                DAL.DALTransfer.CheckCanInputUpdateType = new List<JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType>();
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.轉入);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.轉出);                
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.復學);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.中輟);                
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.新生);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.畢業);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.更正學籍);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.延長修業年限);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.死亡);

                // 異動匯入匯出
                CanUseUpRecIE01 = CanUseUpRecIE02 = CanUseUpRecIE03 = CanUseUpRecIE04 = CanUseUpRecIE06 = CanUseUpRecIE07 =CanUseUpRecIE09= true;

                // 更正學籍異動精靈
                CanUseWizardUpCode09 = true;
            }

            // 高雄專用
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.KaoHsiung)
            {
                // 學生異動資料
                Student.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<JHPermrec.UpdateRecord.UpdateRecordItem>());
                // 設定可使用的異動類別與新增時順序
                DAL.DALTransfer.CheckCanInputUpdateType = new List<JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType>();
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.轉入);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.轉出);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.休學);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.復學);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.中輟);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.續讀);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.新生);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.畢業);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.更正學籍);

                // 異動匯入匯出
                CanUseUpRecIE01 = CanUseUpRecIE02 = CanUseUpRecIE03 = CanUseUpRecIE04 = CanUseUpRecIE05=CanUseUpRecIE06 = CanUseUpRecIE07 =CanUseUpRecIE08=CanUseUpRecIE09= true;

                // 更正學籍異動精靈
                CanUseWizardUpCode09 = true;
            }


            // 台中專用
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.TaiChung)
            {
                // 學生異動資料
                Student.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<JHPermrec.UpdateRecord.UpdateRecordItem>());
                // 設定可使用的異動類別與新增時順序
                DAL.DALTransfer.CheckCanInputUpdateType = new List<JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType>();
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.轉入);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.轉出);                
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.復學);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.中輟);
                
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.新生);
                DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.畢業);

                // 異動匯入匯出
                CanUseUpRecIE01 = CanUseUpRecIE02 = CanUseUpRecIE03 = CanUseUpRecIE04 = CanUseUpRecIE06 = CanUseUpRecIE07 =  true;

            }

            //// 學生異動資料
            ////Student.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<JHPermrec.UpdateRecord.UpdateRecordItem>());
            //// 設定可使用的異動類別與新增時順序
            ////DAL.DALTransfer.CheckCanInputUpdateType = new List<JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType>();
            ////DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.轉入);
            ////DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.轉出);
            ////DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.休學);
            ////DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.復學);
            ////DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.中輟);
            ////DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.續讀);
            ////DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.新生);
            ////DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.畢業);
            ////DAL.DALTransfer.CheckCanInputUpdateType.Add(JHPermrec.UpdateRecord.DAL.DALTransfer.UpdateType.更正學籍);

            // 批次新生異動
            Student.Instance.RibbonBarItems["教務"]["新生作業"]["新生異動"].Enable = User.Acl["JHSchool.Student.Ribbon0110"].Executable;
            //Student.Instance.RibbonBarItems["教務"]["新生作業"]["新生異動"].Image = Properties.Resources.user_write_64;
            MenuButton rbItemTool01 = Student.Instance.RibbonBarItems["教務"]["新生作業"]["新生異動"];
            rbItemTool01.Click += delegate
            {
                Batch.BatchAddNewStudUpdateRecord BASUR = new JHPermrec.UpdateRecord.Batch.BatchAddNewStudUpdateRecord(Batch.BatchAddNewStudUpdateRecord.FormLoadType.學生);
                BASUR.ShowDialog();
            };

            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["新生作業"].Enable = User.Acl["JHSchool.EduAdmin.Ribbon0090"].Executable;
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["新生作業"].Image = Properties.Resources.user_write_64;
            MenuButton rbItemTool02 = EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["新生作業"]["批次產生新生異動"];
            rbItemTool02.Click += delegate
            {
                Batch.BatchAddNewStudUpdateRecord BASUR = new JHPermrec.UpdateRecord.Batch.BatchAddNewStudUpdateRecord(Batch.BatchAddNewStudUpdateRecord.FormLoadType.教務);
                BASUR.ShowDialog();            
            };

            Student.Instance.RibbonBarItems["教務"]["畢業作業"]["畢業異動"].Enable = User.Acl["JHSchool.Student.Ribbon0111"].Executable;
            //Student.Instance.RibbonBarItems["教務"]["畢業作業"]["畢業異動"].Image = Properties.Resources.graduated_write_64;

            MenuButton rbItemTool03 = Student.Instance.RibbonBarItems["教務"]["畢業作業"]["畢業異動"];
            MenuButton rbItemTool04 = EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["畢業作業"]["批次產生畢業異動"];
            //MenuButton rbItemTool05 = EduAdmin.Instance.RibbonBarItems["學籍"]["畢業作業"]["產生異業證書字號"];

            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["畢業作業"].Enable = User.Acl["JHSchool.EduAdmin.Ribbon0080"].Executable;
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["畢業作業"].Image = Properties.Resources.graduated_write_64;

            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["異動作業"].Enable = User.Acl["JHSchool.EduAdmin.Ribbon0100"].Executable;
            //EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["異動作業"].Image = Properties.Resources.demographic_reload_64;
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["異動作業"].Image = Properties.Resources.history_save_64;

            Student.Instance.RibbonBarItems["教務"]["異動作業"].Enable = User.Acl["JHSchool.Student.Ribbon0100"].Executable;
            //Student.Instance.RibbonBarItems["教務"]["轉入作業"].Image = Properties.Resources.demographic_reload_64;

            MenuButton rbItemTool09 = EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["異動作業"]["轉入異動"];
            MenuButton rbItemTool11 = Student.Instance.RibbonBarItems["教務"]["異動作業"]["轉入異動"]; 

            rbItemTool03.Click += delegate
            {
                Batch.BatchStudGraduateUpdateRec BSGRUR = new JHPermrec.UpdateRecord.Batch.BatchStudGraduateUpdateRec(JHPermrec.UpdateRecord.Batch.BatchStudGraduateUpdateRec.FormLoadType.學生);
                BSGRUR.ShowDialog();
            };

            rbItemTool04.Click += delegate
            {
                Batch.BatchStudGraduateUpdateRec BSGRUR = new JHPermrec.UpdateRecord.Batch.BatchStudGraduateUpdateRec(JHPermrec.UpdateRecord.Batch.BatchStudGraduateUpdateRec.FormLoadType.教務);
                BSGRUR.ShowDialog();
            };

            //rbItemTool05.Click += delegate
            //{
            //    Batch.BatchStudGraduateDocNo BSGD = new JHPermrec.UpdateRecord.Batch.BatchStudGraduateDocNo();
            //    BSGD.ShowDialog();
            //};

            rbItemTool09.Click += delegate
            {
                Transfer.AddTransStud ATS = new JHPermrec.UpdateRecord.Transfer.AddTransStud();
                ATS.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                ATS.ShowDialog(FISCA.Presentation.MotherForm.Form);

            };

            rbItemTool11.Click += delegate
            {
                
                Transfer.AddTransStud ATS = new JHPermrec.UpdateRecord.Transfer.AddTransStud();
                ATS.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                ATS.ShowDialog(FISCA.Presentation.MotherForm.Form);

            };

            // 匯出匯入異動啟動            
            RibbonBarButton rbUpdateRecordExport = Student.Instance.RibbonBarItems["資料統計"]["匯出"];
            RibbonBarButton rbUpdateRecordImport = Student.Instance.RibbonBarItems["資料統計"]["匯入"];
            // 匯入匯出註冊權限
            Catalog StudUpdateRecFuncButtonRoleAcl = RoleAclSource.Instance["學生"]["功能按鈕"];


            if (CanUseUpRecIE01)
            {
                // 匯出新生異動
                rbUpdateRecordExport["異動相關匯出"]["匯出新生異動"].Enable = User.Acl["JHSchool.Student.RibbonExportUpdateRecCode01"].Executable;
                rbUpdateRecordExport["異動相關匯出"]["匯出新生異動"].Click += delegate
                {
                    
                    SmartSchool.API.PlugIn.Export.Exporter exporter = new JHPermrec.UpdateRecord.ImportExport.ExportUpdateRecCode01();
                    ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                    exporter.InitializeExport(wizard);
                    wizard.ShowDialog();
                };

                // 匯入新生異動
                rbUpdateRecordImport["異動相關匯入"]["匯入新生異動"].Enable = User.Acl["JHSchool.Student.RibbonImportUpdateRecCode01"].Executable;
                rbUpdateRecordImport["異動相關匯入"]["匯入新生異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Import.Importer importer = new JHPermrec.UpdateRecord.ImportExport.ImportUpdateRecCode01();
                    ImportExport.ImportStudentV2 wizard = new ImportExport.ImportStudentV2(importer.Text, importer.Image);
                    importer.InitializeImport(wizard);
                    wizard.ShowDialog();
                };

                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonExportUpdateRecCode01", "匯出新生異動"));
                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonImportUpdateRecCode01", "匯入新生異動"));

            }

            if (CanUseUpRecIE02)
            {
                // 匯出畢業異動
                rbUpdateRecordExport["異動相關匯出"]["匯出畢業異動"].Enable = User.Acl["JHSchool.Student.RibbonExportUpdateRecCode02"].Executable;
                rbUpdateRecordExport["異動相關匯出"]["匯出畢業異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Export.Exporter exporter = new JHPermrec.UpdateRecord.ImportExport.ExportUpdateRecCode02();
                    ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                    exporter.InitializeExport(wizard);
                    wizard.ShowDialog();
                };

                // 匯入畢業異動
                rbUpdateRecordImport["異動相關匯入"]["匯入畢業異動"].Enable = User.Acl["JHSchool.Student.RibbonImportUpdateRecCode02"].Executable;
                rbUpdateRecordImport["異動相關匯入"]["匯入畢業異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Import.Importer importer = new JHPermrec.UpdateRecord.ImportExport.ImportUpdateRecCode02();
                    ImportExport.ImportStudentV2 wizard = new ImportExport.ImportStudentV2(importer.Text, importer.Image);
                    importer.InitializeImport(wizard);
                    wizard.ShowDialog();
                };

                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonExportUpdateRecCode02", "匯出畢業異動"));
                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonImportUpdateRecCode02", "匯入畢業異動"));
            }

            if (CanUseUpRecIE03)
            {
                // 匯出轉入異動
                rbUpdateRecordExport["異動相關匯出"]["匯出轉入異動"].Enable = User.Acl["JHSchool.Student.RibbonExportUpdateRecCode03"].Executable;
                rbUpdateRecordExport["異動相關匯出"]["匯出轉入異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Export.Exporter exporter = new JHPermrec.UpdateRecord.ImportExport.ExportUpdateRecCode03();
                    ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                    exporter.InitializeExport(wizard);
                    wizard.ShowDialog();
                };

                // 匯入轉入異動
                rbUpdateRecordImport["異動相關匯入"]["匯入轉入異動"].Enable = User.Acl["JHSchool.Student.RibbonImportUpdateRecCode03"].Executable;
                rbUpdateRecordImport["異動相關匯入"]["匯入轉入異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Import.Importer importer = new JHPermrec.UpdateRecord.ImportExport.ImportUpdateRecCode03();
                    ImportExport.ImportStudentV2 wizard = new ImportExport.ImportStudentV2(importer.Text, importer.Image);
                    importer.InitializeImport(wizard);
                    wizard.ShowDialog();
                };

                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonExportUpdateRecCode03", "匯出轉入異動"));
                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonImportUpdateRecCode03", "匯入轉入異動"));

            }

            if (CanUseUpRecIE04)
            {
                // 匯出轉出異動
                rbUpdateRecordExport["異動相關匯出"]["匯出轉出異動"].Enable = User.Acl["JHSchool.Student.RibbonExportUpdateRecCode04"].Executable;
                rbUpdateRecordExport["異動相關匯出"]["匯出轉出異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Export.Exporter exporter = new JHPermrec.UpdateRecord.ImportExport.ExportUpdateRecCode04();
                    ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                    exporter.InitializeExport(wizard);
                    wizard.ShowDialog();
                };

                // 匯入轉出異動
                rbUpdateRecordImport["異動相關匯入"]["匯入轉出異動"].Enable = User.Acl["JHSchool.Student.RibbonImportUpdateRecCode04"].Executable;
                rbUpdateRecordImport["異動相關匯入"]["匯入轉出異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Import.Importer importer = new JHPermrec.UpdateRecord.ImportExport.ImportUpdateRecCode04();
                    ImportExport.ImportStudentV2 wizard = new ImportExport.ImportStudentV2(importer.Text, importer.Image);
                    importer.InitializeImport(wizard);
                    wizard.ShowDialog();
                };

                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonExportUpdateRecCode04", "匯出轉出異動"));
                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonImportUpdateRecCode04", "匯入轉出異動"));

            }

            if (CanUseUpRecIE05)
            {
                // 匯出休學異動
                rbUpdateRecordExport["異動相關匯出"]["匯出休學異動"].Enable = User.Acl["JHSchool.Student.RibbonExportUpdateRecCode05"].Executable;
                rbUpdateRecordExport["異動相關匯出"]["匯出休學異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Export.Exporter exporter = new JHPermrec.UpdateRecord.ImportExport.ExportUpdateRecCode05();
                    ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                    exporter.InitializeExport(wizard);
                    wizard.ShowDialog();
                };

                // 匯入休學異動
                rbUpdateRecordImport["異動相關匯入"]["匯入休學異動"].Enable = User.Acl["JHSchool.Student.RibbonImportUpdateRecCode05"].Executable;
                rbUpdateRecordImport["異動相關匯入"]["匯入休學異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Import.Importer importer = new JHPermrec.UpdateRecord.ImportExport.ImportUpdateRecCode05();
                    ImportExport.ImportStudentV2 wizard = new ImportExport.ImportStudentV2(importer.Text, importer.Image);
                    importer.InitializeImport(wizard);
                    wizard.ShowDialog();
                };

                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonExportUpdateRecCode05", "匯出休學異動"));
                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonImportUpdateRecCode05", "匯入休學異動"));
            }

            if (CanUseUpRecIE06)
            {
                // 匯出復學異動
                rbUpdateRecordExport["異動相關匯出"]["匯出復學異動"].Enable = User.Acl["JHSchool.Student.RibbonExportUpdateRecCode06"].Executable;
                rbUpdateRecordExport["異動相關匯出"]["匯出復學異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Export.Exporter exporter = new JHPermrec.UpdateRecord.ImportExport.ExportUpdateRecCode06();
                    ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                    exporter.InitializeExport(wizard);
                    wizard.ShowDialog();
                };

                // 匯入復學異動
                rbUpdateRecordImport["異動相關匯入"]["匯入復學異動"].Enable = User.Acl["JHSchool.Student.RibbonImportUpdateRecCode06"].Executable;
                rbUpdateRecordImport["異動相關匯入"]["匯入復學異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Import.Importer importer = new JHPermrec.UpdateRecord.ImportExport.ImportUpdateRecCode06();
                    ImportExport.ImportStudentV2 wizard = new ImportExport.ImportStudentV2(importer.Text, importer.Image);
                    importer.InitializeImport(wizard);
                    wizard.ShowDialog();
                };
                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonExportUpdateRecCode06", "匯出復學異動"));
                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonImportUpdateRecCode06", "匯入復學異動"));

            }

            if (CanUseUpRecIE07)
            {
                // 匯出中輟異動
                rbUpdateRecordExport["異動相關匯出"]["匯出中輟異動"].Enable = User.Acl["JHSchool.Student.RibbonExportUpdateRecCode07"].Executable;
                rbUpdateRecordExport["異動相關匯出"]["匯出中輟異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Export.Exporter exporter = new JHPermrec.UpdateRecord.ImportExport.ExportUpdateRecCode07();
                    ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                    exporter.InitializeExport(wizard);
                    wizard.ShowDialog();
                };

                // 匯入中輟異動
                rbUpdateRecordImport["異動相關匯入"]["匯入中輟異動"].Enable = User.Acl["JHSchool.Student.RibbonImportUpdateRecCode07"].Executable;
                rbUpdateRecordImport["異動相關匯入"]["匯入中輟異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Import.Importer importer = new JHPermrec.UpdateRecord.ImportExport.ImportUpdateRecCode07();
                    ImportExport.ImportStudentV2 wizard = new ImportExport.ImportStudentV2(importer.Text, importer.Image);
                    importer.InitializeImport(wizard);
                    wizard.ShowDialog();
                };

                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonExportUpdateRecCode07", "匯出中輟異動"));
                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonImportUpdateRecCode07", "匯入中輟異動"));
            }

            if (CanUseUpRecIE08)
            {
                // 匯出續讀異動
                rbUpdateRecordExport["異動相關匯出"]["匯出續讀異動"].Enable = User.Acl["JHSchool.Student.RibbonExportUpdateRecCode08"].Executable;
                rbUpdateRecordExport["異動相關匯出"]["匯出續讀異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Export.Exporter exporter = new JHPermrec.UpdateRecord.ImportExport.ExportUpdateRecCode08();
                    ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                    exporter.InitializeExport(wizard);
                    wizard.ShowDialog();
                };

                // 匯入續讀異動
                rbUpdateRecordImport["異動相關匯入"]["匯入續讀異動"].Enable = User.Acl["JHSchool.Student.RibbonImportUpdateRecCode08"].Executable;
                rbUpdateRecordImport["異動相關匯入"]["匯入續讀異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Import.Importer importer = new JHPermrec.UpdateRecord.ImportExport.ImportUpdateRecCode08();
                    ImportExport.ImportStudentV2 wizard = new ImportExport.ImportStudentV2(importer.Text, importer.Image);
                    importer.InitializeImport(wizard);
                    wizard.ShowDialog();
                };
                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonExportUpdateRecCode08", "匯出續讀異動"));
                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonImportUpdateRecCode08", "匯入續讀異動"));

            }

            if (CanUseUpRecIE09)
            {
                // 匯出更正學籍異動
                rbUpdateRecordExport["異動相關匯出"]["匯出更正學籍異動"].Enable = User.Acl["JHSchool.Student.RibbonExportUpdateRecCode09"].Executable;
                rbUpdateRecordExport["異動相關匯出"]["匯出更正學籍異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Export.Exporter exporter = new JHPermrec.UpdateRecord.ImportExport.ExportUpdateRecCode09();
                    ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                    exporter.InitializeExport(wizard);
                    wizard.ShowDialog();
                };

                // 匯入更正學籍異動
                rbUpdateRecordImport["異動相關匯入"]["匯入更正學籍異動"].Enable = User.Acl["JHSchool.Student.RibbonImportUpdateRecCode09"].Executable;
                rbUpdateRecordImport["異動相關匯入"]["匯入更正學籍異動"].Click += delegate
                {
                    SmartSchool.API.PlugIn.Import.Importer importer = new JHPermrec.UpdateRecord.ImportExport.ImportUpdateRecCode09();
                    ImportExport.ImportStudentV2 wizard = new ImportExport.ImportStudentV2(importer.Text, importer.Image);
                    importer.InitializeImport(wizard);
                    wizard.ShowDialog();
                };
                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonExportUpdateRecCode09", "匯出更正學籍異動"));
                StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonImportUpdateRecCode09", "匯入更正學籍異動"));

            }

            // 匯出學籍異動
            rbUpdateRecordExport["異動相關匯出"]["匯出學籍異動"].Enable = User.Acl["JHSchool.Student.RibbonExportUpdateRecCode100"].Executable;
            rbUpdateRecordExport["異動相關匯出"]["匯出學籍異動"].Click += delegate
            {

                SmartSchool.API.PlugIn.Export.Exporter exporter = new JHPermrec.UpdateRecord.ImportExport.ExportUpdateRecCode100();
                ImportExport.ExportStudentV2 wizard = new ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
                exporter.InitializeExport(wizard);
                wizard.ShowDialog();
            };

            // 匯入學籍異動
            rbUpdateRecordImport["異動相關匯入"]["匯入學籍異動"].Enable = User.Acl["JHSchool.Student.RibbonImportUpdateRecCode100"].Executable;
            rbUpdateRecordImport["異動相關匯入"]["匯入學籍異動"].Click += delegate
            {
                SmartSchool.API.PlugIn.Import.Importer importer = new JHPermrec.UpdateRecord.ImportExport.ImportUpdateRecCode100();
                ImportExport.ImportStudentV2 wizard = new ImportExport.ImportStudentV2(importer.Text, importer.Image);
                importer.InitializeImport(wizard);
                wizard.ShowDialog();
            };

            StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonExportUpdateRecCode100", "匯出學籍異動"));
            StudUpdateRecFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonImportUpdateRecCode100", "匯入學籍異動"));



            // 更正學籍異動精靈
            if (CanUseWizardUpCode09)
            {
                MenuButton rbItemRibbonWizard_UpdateCode09 = Student.Instance.RibbonBarItems["教務"]["異動作業"]["更正學籍"];                
                rbItemRibbonWizard_UpdateCode09.Enable = User.Acl["JHSchool.Student.RibbonWizard_UpdateCode09"].Executable;
                rbItemRibbonWizard_UpdateCode09.Click += delegate
                {
                    bool check = true;
                    if (K12.Presentation.NLDPanels.Student.SelectedSource.Count == 0)
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("請選擇學生.");
                        check = false;
                    }

                    if (K12.Presentation.NLDPanels.Student.SelectedSource.Count > 1)
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("只能處理單筆學生異動.");
                        check = false;
                    }

                    if (check)
                    {
                        Wizard.Wizard_UpdateCode09Form_1 wuc9f = new JHPermrec.UpdateRecord.Wizard.Wizard_UpdateCode09Form_1();
                        wuc9f.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        wuc9f.ShowDialog(FISCA.Presentation.MotherForm.Form);
                    }
                };            
            }
            Catalog ribbon = RoleAclSource.Instance["教務作業"];
            ribbon.Add(new RibbonFeature("JHSchool.EduAdmin.Ribbon0060", "異動名冊"));

            MenuButton rbItem1 = EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["異動作業"]["異動名冊"];
            //rbItem1.Size = RibbonBarButton.MenuButtonSize.Large;
            //rbItem1.Image = Properties.Resources.history_save_64;
            //rbItem1.Image = JHPermrec.UpdateRecord.GovernmentalDocument.GDResources.btnItemNameList_Image;
            rbItem1.Enable = User.Acl["JHSchool.EduAdmin.Ribbon0060"].Executable;
            rbItem1.Click += delegate
            {

                JHPermrec.UpdateRecord.GovernmentalDocument.Process.NameList.RegisterNames();
                new JHPermrec.UpdateRecord.GovernmentalDocument.NameList.ListForm().ShowDialog();

            };

            RibbonBarItem ViewForm1 = EduAdmin.Instance.RibbonBarItems["批次作業/檢視"];
            //ViewForm1["檢視"]["異動資料檢視"].Image = Properties.Resources.contract_zoom_128;
            ViewForm1["異動作業"]["異動資料檢視"].Enable = User.Acl["JHSchool.EduAdmin.Ribbon0070"].Executable;
            ViewForm1["異動作業"]["異動資料檢視"].Click += delegate
            {
                UpdateRecordViewForm.UpdateRecordViewForm UpReViewForm = new JHPermrec.UpdateRecord.UpdateRecordViewForm.UpdateRecordViewForm();
                UpReViewForm.ShowDialog();
            };


            // 學生異動資料註冊權限
            Catalog detail = RoleAclSource.Instance["學生"]["資料項目"];
            detail.Add(new DetailItemFeature(typeof(JHPermrec.UpdateRecord.UpdateRecordItem)));
            
            Catalog catalog = RoleAclSource.Instance["學生"]["功能按鈕"];            
            catalog.Add(new RibbonFeature("JHSchool.Student.Ribbon0110", "新生異動"));
            catalog.Add(new RibbonFeature("JHSchool.Student.Ribbon0111", "畢業異動"));
            catalog.Add(new RibbonFeature("JHSchool.Student.Ribbon0100", "轉入異動"));
            catalog.Add(new RibbonFeature("JHSchool.Student.RibbonWizard_UpdateCode09", "更正學籍"));
            Catalog catalog1 = RoleAclSource.Instance["教務作業"]["新生作業"];
            Catalog catalog2 = RoleAclSource.Instance["教務作業"]["畢業作業"];
            Catalog catalog3 = RoleAclSource.Instance["教務作業"]["轉入作業"];
            catalog1.Add(new RibbonFeature("JHSchool.EduAdmin.Ribbon0090","批次新生異動"));
            catalog2.Add(new RibbonFeature("JHSchool.EduAdmin.Ribbon0080", "批次畢業異動"));
            //catalog2.Add(new RibbonFeature("JHSchool.EduAdmin.Ribbon00801", "產生異業證書字號"));
            catalog3.Add(new RibbonFeature("JHSchool.EduAdmin.Ribbon0100", "轉入異動"));

            Catalog ribbon1 = RoleAclSource.Instance["教務作業"];
            ribbon1.Add(new RibbonFeature("JHSchool.EduAdmin.Ribbon0070", "異動資料檢視"));
        }
    }
}
