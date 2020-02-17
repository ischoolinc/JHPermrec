using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.Xml;
using System.IO;
using JHPermrec.UpdateRecord.DAL;

namespace JHPermrec.UpdateRecord.GovernmentalDocument.NameList
{
    // ��J�W�U
    public class TransferringStudentUpdateRecordList : ReportBuilder
    {
        protected override void Build(XmlElement source, string location)
        {
            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.HsinChu)
                ProcessHsinChu(source, location);

            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.KaoHsiung)
                ProcessKaoHsiung(source, location);

            if (JHSchool.Permrec.Program.ModuleType == JHSchool.Permrec.Program.ModuleFlag.TaiChung)
                ProcessTaiChung(source, location);
        }



        public override string Copyright
        {
            get { return "IntelliSchool"; }
        }

        public override string Description
        {
            get { return "�����줽��95�~11��s�L�޲z��U�W�d�榡"; }
        }

        // �B�z�s�˼˪�
        private void ProcessHsinChu(System.Xml.XmlElement source, string location)
        {
            // ����ഫ
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

            Workbook template = new Workbook();

            //�qResources��TemplateŪ�X��
            template.Open(new MemoryStream(GDResources.JTransferStudentUpdateRecordTemplate_HsinChu), FileFormatType.Excel2003);

            //�n���ͪ�excel��
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JTransferStudentUpdateRecordTemplate_HsinChu), FileFormatType.Excel2003);


            Worksheet wst = wb.Worksheets[0];
            wst.Name = "��J���ʦW�U";


            #region �ƻs�˦�-�w�]�˦��B��e

            //�]�w�w�]�˦�
            wb.DefaultStyle = template.DefaultStyle;

            string SchoolInfoAndSchoolYear = StudBatchUpdateRecEntity.GetContentSchoolName() + "  " + source.SelectSingleNode("@�Ǧ~��").InnerText + wst.Cells[0, 0].StringValue;

            wst.Cells[0, 0].PutValue(SchoolInfoAndSchoolYear);

            #endregion

            #region ��l�ܼ�
            int recCount = 0;
            int totalRec = data.Count;

            #endregion

            int row = 2;
            //�Nxml��ƶ�J��excel
            foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
            {
                wst.Cells[row, 0].PutValue(sburce.GetClassName());
                wst.Cells[row, 1].PutValue(sburce.GetName());
                wst.Cells[row, 2].PutValue(sburce.GetStudentNumber());
                wst.Cells[row, 3].PutValue(sburce.GetIDNumber());


                DateTime dt;
                string strDate = "";
                if (DateTime.TryParse(sburce.GetBirthday(), out dt))
                {
                    strDate = "����" + (dt.Year - 1911) + "�~" + dt.Month + "��" + dt.Day + "��";
                }
                wst.Cells[row, 4].PutValue(strDate);
                wst.Cells[row, 5].PutValue(sburce.GetGender());
                wst.Cells[row, 6].PutValue(sburce.GetGuardian());
                wst.Cells[row, 7].PutValue(sburce.GetAddress());
                wst.Cells[row, 8].PutValue(sburce.GetUpdateCodeType());
                wst.Cells[row, 9].PutValue(sburce.GetUpdateDescription());
                wst.Cells[row, 10].PutValue(sburce.GetImportExportSchool());
                wst.Cells[row, 11].PutValue(sburce.GetComment());

                recCount++;
                row++;
                //�^���i��
                ReportProgress((int)(((double)recCount * 100.0) / ((double)totalRec)));
            }



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
                tmpMaxRow = wb.Worksheets[wbIdx1].Cells.MaxDataRow;
                tmpMaxCol = wb.Worksheets[wbIdx1].Cells.MaxDataColumn + 1;
                wb.Worksheets[wbIdx1].Cells.CreateRange(1, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            }

            //�x�s
            wb.Save(location, FileFormatType.Excel2003);

        }

        // �B�z�����˪�
        private void ProcessKaoHsiung(System.Xml.XmlElement source, string location)
        {
            // ����ഫ
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);
            int peoTotalCount = 0;  // �`�H��
            int peoBoyCount = 0;    // �k�ͤH��
            int peoGirlCount = 0;   // �k�ͤH��

            int tmpY, tmpM, tmpD;
            tmpY = DateTime.Now.Year;
            tmpM = DateTime.Now.Month;
            tmpD = DateTime.Now.Day;
            string tmpRptY, tmpRptM;
            string strPrintDate = (tmpY - 1911).ToString() + "/";
            tmpRptY = (tmpY - 1911).ToString();
            if (tmpM < 10)
            {
                strPrintDate += "0" + tmpM.ToString() + "/";
                tmpRptM = "0" + tmpM.ToString();
            }
            else
            {
                strPrintDate += tmpM.ToString() + "/";
                tmpRptM = tmpM.ToString();
            }
            if (tmpD < 10)
                strPrintDate += "0" + tmpD.ToString();
            else
                strPrintDate += tmpD.ToString();

            Workbook template = new Workbook();

            //�qResources��TemplateŪ�X��
            template.Open(new MemoryStream(GDResources.JTransferringStudentUpdateRecordListTemplate), FileFormatType.Excel2003);

            //�n���ͪ�excel��
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(GDResources.JTransferringStudentUpdateRecordListTemplate), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];

            #region �ƻs�˦�-�w�]�˦��B��e

            //�]�w�w�]�˦�
            wb.DefaultStyle = template.DefaultStyle;

            #endregion

            #region ��l�ܼ�
            int rowi = 0, rowj = 1;
            int recCount = 0;
            int totalRec = data.Count;

            #endregion
            rowj = 4;

            wb.Worksheets[0].Cells[rowi, 4].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + "  " + StudBatchUpdateRecEntity.GetContentSchoolYear() + "�Ǧ~�ײ�" + StudBatchUpdateRecEntity.GetContentSemester() + "�Ǵ�");
            wb.Worksheets[0].Cells[rowi, 8].PutValue("�C�L����G" + strPrintDate);
            wb.Worksheets[0].Cells[rowi + 1, 8].PutValue("�C�L�ɶ��G" + DateTime.Now.ToLongTimeString());

            #region ���ʬ���


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
                {
                    wb.Worksheets[0].Cells[rowj, 5].PutValue(UpdateRecordUtil.ChangeDate1911(sburce.GetBirthday()));
                }
                if (!string.IsNullOrEmpty(sburce.GetUpdateDate()))
                {
                    wb.Worksheets[0].Cells[rowj, 6].PutValue(UpdateRecordUtil.ChangeDate1911(sburce.GetUpdateDate()));
                }
                wb.Worksheets[0].Cells[rowj, 7].PutValue(sburce.GetImportExportSchool());
                wb.Worksheets[0].Cells[rowj, 8].PutValue(sburce.GetAddress());
                wb.Worksheets[0].Cells[rowj, 9].PutValue(sburce.GetLastADNumber());
                #endregion
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


            // �έp�H��
            rowj++;
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

            //�x�s
            wb.Save(location, FileFormatType.Excel2003);

        }

        // �B�z�x���˪�
        private void ProcessTaiChung(System.Xml.XmlElement source, string location)
        {
            // ����ഫ
            Dictionary<string, JHPermrec.UpdateRecord.DAL.StudBatchUpdateRecContentEntity> data = StudBatchUpdateRecEntity.ConvertGetContentData(source);

            #region �إ� Excel
            int tmpY, tmpM;
            tmpY = DateTime.Now.Year;
            tmpM = DateTime.Now.Month;
            string tmpRptY, tmpRptM;
            tmpRptY = (tmpY - 1911).ToString();
            if (tmpM < 10)
                tmpRptM = "0" + tmpM.ToString();
            else
                tmpRptM = tmpM.ToString();

            string strPrintDate = UpdateRecordUtil.ChangeDate1911(DateTime.Now.ToString());

            // �x����J�P�s�ͼ˦��ۦP
            //�q Resources �N�s�ͦW�UtemplateŪ�X��
            Workbook template = new Workbook();
            //template.Worksheets[0].PageSetup.
            //template.Open(new MemoryStream(GDResources.JEnrollmentListTemplate_TaiChung), FileFormatType.Excel2003);
            template.Open(new MemoryStream(GDResources.JTransferListTemplate_TaiChung), FileFormatType.Excel2003);
            //���� excel
            Workbook wb = new Aspose.Cells.Workbook();
            //wb.Open(new MemoryStream(GDResources.JEnrollmentListTemplate_TaiChung), FileFormatType.Excel2003);
            wb.Open(new MemoryStream(GDResources.JTransferListTemplate_TaiChung), FileFormatType.Excel2003);
            #endregion

            #region �ƻs�˦�-�w�]�˦��B��e

            //�]�w�w�]�˦�
            wb.DefaultStyle = template.DefaultStyle;

            #endregion

            #region ��l�ܼ�
            int rowj = 1;
            int recCount = 0;
            int totalRec = data.Count;

            #endregion
            wb.Worksheets[0].Cells[0, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName() + "  ��J�ǥͦW�U");
            wb.Worksheets[0].Cells[1, 6].PutValue(tmpRptY + "�~" + tmpRptM + "����");
            //wb.Worksheets[0].Cells[0, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolName () + "  ������ǤJ�ǾǥͦW�U");
            //wb.Worksheets[0].Cells[1, 10].PutValue(tmpRptY + "�~" + tmpM + "���s");

            Range templateRow = template.Worksheets[0].Cells.CreateRange(4, 7, false);

            //string strGradeYear="";
            rowj = 4;
            //�Nxml��ƶ�J��excel
            foreach (StudBatchUpdateRecContentEntity sburce in data.Values)
            {
                //��J�e���ƻs�榡
                wb.Worksheets[0].Cells.CreateRange(rowj, 7, false).Copy(templateRow);
                //if (rowj == 4)
                //strGradeYear = sburce.GetClassYear ();

                recCount++;
                //�N�ǥ͸�ƶ�J�A����m��
                wb.Worksheets[0].Cells[rowj, 0].PutValue(sburce.GetStudentNumber());
                wb.Worksheets[0].Cells[rowj, 1].PutValue(sburce.GetName());
                wb.Worksheets[0].Cells[rowj, 2].PutValue(sburce.GetGradeYear());
                wb.Worksheets[0].Cells[rowj, 3].PutValue(StudBatchUpdateRecEntity.GetContentSemester());

                //���ʦ~��
                DateTime dt;
                if (DateTime.TryParse(sburce.GetUpdateDate(), out dt))
                {
                    wb.Worksheets[0].Cells[rowj, 4].PutValue("" + (dt.Year - 1911));
                    wb.Worksheets[0].Cells[rowj, 5].PutValue("" + dt.Month);
                }

                //���ʱ���
                wb.Worksheets[0].Cells[rowj, 6].PutValue(sburce.GetImportExportSchool());

                //��]
                wb.Worksheets[0].Cells[rowj, 7].PutValue(sburce.GetUpdateDescription());

                rowj++;

                //�^���i��
                ReportProgress((int)(((double)recCount * 100.0) / ((double)totalRec)));
            }


            // Title
            //wb.Worksheets[0].Cells[1, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolYear() + "�Ǧ~�ײ�" + StudBatchUpdateRecEntity.GetContentSemester () + "�Ǵ� "+strGradeYear +"�~��");
            wb.Worksheets[0].Cells[1, 0].PutValue(StudBatchUpdateRecEntity.GetContentSchoolYear() + "�Ǧ~�ײ�" + StudBatchUpdateRecEntity.GetContentSemester() + "�Ǵ� ���ʡG��J");

            //�X�p�H��
            wb.Worksheets[0].Cells[rowj, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 0].PutValue("�X�p");
            wb.Worksheets[0].Cells[rowj, 1].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 1].PutValue("" + data.Count + " �W");
            wb.Worksheets[0].Cells[rowj, 3].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[rowj, 3].PutValue("�H�U�ť�");

            // �e��
            //Style st2 = wb.Styles[wb.Styles.Add()];
            //StyleFlag sf2 = new StyleFlag();
            //sf2.Borders = true;

            //st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            //st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            //st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            //st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            //int tmpMaxRow = 0, tmpMaxCol = 0;
            //for (int wbIdx1 = 0; wbIdx1 < wb.Worksheets.Count; wbIdx1++)
            //{
            //    tmpMaxRow = wb.Worksheets[wbIdx1].Cells.MaxDataRow - 3;
            //    tmpMaxCol = wb.Worksheets[wbIdx1].Cells.MaxDataColumn + 1;
            //    wb.Worksheets[wbIdx1].Cells.CreateRange(4, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            //}

            //�x�s Excel
            wb.Save(location, FileFormatType.Excel2003);

        }

        public override string ReportName
        {
            get { return "��J�ǥͦW�U"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
