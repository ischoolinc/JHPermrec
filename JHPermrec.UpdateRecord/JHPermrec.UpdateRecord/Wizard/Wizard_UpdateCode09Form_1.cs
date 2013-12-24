using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JHPermrec.UpdateRecord.Wizard
{
    public partial class Wizard_UpdateCode09Form_1 : FISCA.Presentation.Controls.BaseForm 
    {
        public Wizard_UpdateCode09Form_1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (K12.Presentation.NLDPanels.Student.SelectedSource.Count < 1)
                return;
            if (chkBirthday.Checked == false && chkGender.Checked == false && chkIDNumber.Checked == false && chkName.Checked == false)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請勾選項目.");
                return;
            }
            
            Wizard_UpdateCode09Form_2 wizard = new Wizard_UpdateCode09Form_2();
            this.Hide();
            this.Close();
            this.Location = new Point(-100, -100);
            wizard._StudentID = K12.Presentation.NLDPanels.Student.SelectedSource[0];
            wizard.isNameEnable = chkName.Checked;
            wizard.isIDNumberEnable = chkIDNumber.Checked;
            wizard.isBirthdayEnable = chkBirthday.Checked;
            wizard.isGenderEnable = chkGender.Checked;            
            wizard.StartPosition = FormStartPosition.CenterParent;
            if (wizard.DialogResult == DialogResult.Cancel)
                wizard.Close();

                wizard.ShowDialog(FISCA.Presentation.MotherForm.Form);
//            this.Visible = false;
            
        }

    }
}
