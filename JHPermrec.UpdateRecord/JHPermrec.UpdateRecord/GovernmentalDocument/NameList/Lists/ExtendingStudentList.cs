using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.Xml;
using System.IO;
using Framework.Legacy;
using JHPermrec.UpdateRecord.DAL;

namespace JHPermrec.UpdateRecord.GovernmentalDocument.NameList
{
    public class ExtendingStudentList : ReportBuilder
    {
        // �����W�U
        protected override void Build(System.Xml.XmlElement source, string location)
        {
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.KaoHsiung)
                ProcessKaoHsiung(source, location);
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.TaiChung)
                ProcessTaiChung(source, location);
        }


        // ������
        private void ProcessKaoHsiung(System.Xml.XmlElement source, string location)
        {
            // ����ഫ
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

            int peoTotalCount = 0;  // �`�H��
            int peoBoyCount = 0;    // �k�ͤH��
            int peoGirlCount = 0;   // �k�ͤH��

            int tmpY, tmpM;
            tmpY = DateTime.Now.Year;
            tmpM = DateTime.Now.Month;
            string tmpRptY, tmpRptM;
            tmpRptY = (tmpY - 1911).ToString();
            if (tmpM < 10)
                tmpRptM = "0" + tmpM.ToString();
            else
                tmpRptM = tmpM.ToString();


            #region �إ� Excel

            //�q Resources �N���y���ʦW�UtemplateŪ�X��
            Workbook template = new Workbook();
            template.Open(new MemoryStream(GDResources.JExtendingStudentListTemplate), FileFormatType.Excel2003);

            //���� excel
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JExtendingStudentListTemplate), FileFormatType.Excel2003);

            #endregion
            #region �ƻs�˦�-�w�]�˦��B��e

            //�]�w�w�]�˦�
            wb.DefaultStyle = template.DefaultStyle;

            #endregion

            int rowi = 0, rowj = 1, numcount = 1, j = 0;
            int recCount = 0;
            int totalRec = data.Count;

            rowj = 4;

            wb.Worksheets[0].Cells[rowi, 4].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + "  " + StudBatchUpdateRecEntity.GetContentSchoolYear() + "�Ǧ~�� " + StudBatchUpdateRecEntity.GetContentSemester() + "�Ǵ�");
            wb.Worksheets[0].Cells[rowi, 8].PutValue("�C�L����G" + UpdateRecordUtil.ChangeDate1911(DateTime.Now.ToString()));
            wb.Worksheets[0].Cells[rowi + 1, 8].PutValue("�C�L�ɶ��G" + DateTime.Now.ToLongTimeString());


            //�Nxml��ƶ�J��excel
            foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
            {
                recCount++;

                #region ��J�ǥ͸��
                wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber());
                wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetClassName());
                wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGender());
                wb.Worksheets[0].Cells[rowj, 3].PutValue(sburce.GetName());
                wb.Worksheets[0].Cells[rowj, 4].PutValue(sburce.GetIDNumber());
                if (sburce.GetGender() == "�k")
                    peoBoyCount++;
                if (sburce.GetGender() == "�k")
                    peoGirlCount++;

                peoTotalCount++;

                if (!string.IsNullOrEmpty(sburce.GetBirthday()))
                    wb.Worksheets[0].Cells[rowj, 5].PutValue(UpdateRecordUtil.ChangeDate1911(sburce.GetBirthday()));

                if (!string.IsNullOrEmpty(sburce.GetUpdateDate()))
                    wb.Worksheets[0].Cells[rowj, 6].PutValue(UpdateRecordUtil.ChangeDate1911(sburce.GetUpdateDate()));

                wb.Worksheets[0].Cells[rowj, 8].PutValue(sburce.GetAddress());
                wb.Worksheets[0].Cells[rowj, 9].PutValue(sburce.GetLastADNumber());

                #endregion

                rowj++;

                //�^���i��
                ReportProgress((int)(((double)recCount * 100.0) / ((double)totalRec)));
            }

            Style st2 = wb.Styles[wb.Styles.Add()];
            StyleFlag sf2 = new StyleFlag();
            sf2.Borders = true;

            st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            int tmpMaxRow = 0, tmpMaxCol = 0;
            for (int wbIdx1 = 0; wbIdx1 < wb.Worksheets.Count; wbIdx1++)
            {
                tmpMaxRow = wb.Worksheets[wbIdx1].Cells.MaxDataRow - 3;
                tmpMaxCol = wb.Worksheets[wbIdx1].Cells.MaxDataColumn + 1;
                wb.Worksheets[wbIdx1].Cells.CreateRange(4, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            }


            // �έp�H��
            rowj++;
            wb.Worksheets[0].Cells.CreateRange(rowj, 2, 1, 2).Merge();
            wb.Worksheets[0].Cells[rowj, 2].PutValue("�k�G" + peoBoyCount.ToString());
            wb.Worksheets[0].Cells[rowj, 4].PutValue("�k�G" + peoGirlCount.ToString());
            wb.Worksheets[0].Cells[rowj, 8].PutValue("�`�p�G" + peoTotalCount.ToString());
            wb.Worksheets[0].Cells.CreateRange(rowj + 1, 0, 1, 10).Merge();
            //            wb.Worksheets[0].Cells[rowj + 1, 0].PutValue("�ժ�                                                          �аȥD��                                                          ���U�ժ�                                                          �ֹ��");
            wb.Worksheets[0].Cells[rowj + 1, 0].PutValue("�ֹ��                                                          ���U�ժ�                                                          �аȥD��                                                          �ժ�");


            // ��ܭ�
            PageSetup pg = wb.Worksheets[0].PageSetup;
            string tmp = "&12 " + tmpRptY + "�~" + tmpRptM + "�� ���" + "�@&N��";
            pg.SetHeader(2, tmp);

            //�x�s Excel
            wb.Save(location, FileFormatType.Excel2003);
        }

        // �B�z�x������
        private void ProcessTaiChung(XmlElement source, string location)
        {
            // ����ഫ
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

            int tmpY, tmpM;
            tmpY = DateTime.Now.Year;
            tmpM = DateTime.Now.Month;
            string tmpRptY, tmpRptM;
            tmpRptY = (tmpY - 1911).ToString();
            if (tmpM < 10)
                tmpRptM = "0" + tmpM.ToString();
            else
                tmpRptM = tmpM.ToString();

            Workbook template = new Workbook();

            //�qResources��TemplateŪ�X��
            template.Open(new MemoryStream(GDResources.JExtendListTemplate_TaiChung), FileFormatType.Excel2003);

            //�n���ͪ�excel��
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JExtendListTemplate_TaiChung), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];

            #region �ƻs�˦�-�w�]�˦��B��e

            //�]�w�w�]�˦�
            wb.DefaultStyle = template.DefaultStyle;

            #endregion

            #region ��l�ܼ�

            int rowj = 1;
            int recCount = 0;
            int totalRec = data.Count;

            #endregion
            rowj = 4;
            wb.Worksheets[0].Cells[0, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + " �ǥͲ��ʦW�U");
            string strSemester = StudBatchUpdateRecEntity.GetContentSemester();
            wb.Worksheets[0].Cells[1, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolYear() + "�Ǧ~�ײ�" + strSemester + "�Ǵ�");
            wb.Worksheets[0].Cells[1, 7].PutValue(tmpRptY + "�~" + tmpRptM + "����");

            #region ���ʬ���

            //�Nxml��ƶ�J��excel
            foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
            {
                recCount++;

                wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber());
                wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetName());
                wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGradeYear());
                wb.Worksheets[0].Cells[rowj, 3].PutValue(strSemester);

                DateTime dt;
                // �Φ褸�ഫ
                if (DateTime.TryParse(sburce.GetUpdateDate(), out dt))
                {
                    wb.Worksheets[0].Cells[rowj, 4].PutValue("" + (dt.Year - 1911));
                    wb.Worksheets[0].Cells[rowj, 5].PutValue("" + dt.Month);
                }

                wb.Worksheets[0].Cells[rowj, 6].PutValue(sburce.GetUpdateDescription());
                wb.Worksheets[0].Cells[rowj, 7].PutValue(sburce.GetComment());

                rowj++;

                //�^���i��
                ReportProgress((int)(((double)recCount * 100.0) / ((double)totalRec)));
            }

            #endregion

            // �e��
            Style st2 = wb.Styles[wb.Styles.Add()];
            StyleFlag sf2 = new StyleFlag();
            sf2.Borders = true;

            st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            int tmpMaxRow = 0, tmpMaxCol = 0;
            for (int wbIdx1 = 0; wbIdx1 < wb.Worksheets.Count; wbIdx1++)
            {
                tmpMaxRow = wb.Worksheets[wbIdx1].Cells.MaxDataRow - 3;
                tmpMaxCol = wb.Worksheets[wbIdx1].Cells.MaxDataColumn + 1;
                wb.Worksheets[wbIdx1].Cells.CreateRange(4, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            }

            //�X�p�H��
            wb.Worksheets[0].Cells[rowj, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 0].PutValue("�X�p");
            wb.Worksheets[0].Cells[rowj, 1].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 1].PutValue(data.Count + " �W");
            wb.Worksheets[0].Cells[rowj, 6].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 6].PutValue("�H�U�ť�");

            //�x�s
            wb.Save(location, FileFormatType.Excel2003);

        }

        public override string Copyright
        {
            get { return "IntelliSchool"; }
        }

        public override string Description
        {
            get { return "�����줽��95�~11��s�L�޲z��U�W�d�榡"; }
        }

        public override string ReportName
        {
            get { return "�����ǥͦW�U"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
