using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartSchool.Common;
using JHSchool.Data;

namespace JHPermrec.UpdateRecord.GovernmentalDocument.Process
{
    public partial class ErrorViewer : BaseForm
    {
        private Dictionary<JHStudentRecord , string> _list;

        public ErrorViewer(Dictionary<JHStudentRecord, string> list)
        {
            InitializeComponent();

            _list = list;
        }

        private void ErrorViewer_Load(object sender, EventArgs e)
        {
            dataGridViewX1.Rows.Clear();
            dataGridViewX1.SuspendLayout();

            foreach (JHStudentRecord var in _list.Keys)
            {
                int index = dataGridViewX1.Rows.Add();
                DataGridViewRow row = dataGridViewX1.Rows[index];
                if (var.Class != null)
                    row.Cells[colClass.Name].Value = var.Class.Name;
                else
                    row.Cells[colClass.Name].Value= "(未分班)";
                row.Cells[colStudentNumber.Name].Value = var.StudentNumber;
                row.Cells[colStudentName.Name].Value = var.Name ;
                row.Cells[colError.Name].Value = _list[var];
            }

            dataGridViewX1.ResumeLayout();
        }


    }
}