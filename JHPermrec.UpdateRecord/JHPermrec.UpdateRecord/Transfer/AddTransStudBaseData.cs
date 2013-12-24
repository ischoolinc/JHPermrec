using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using FISCA.DSAUtil;
using Framework;
using JHSchool.Feature.Legacy;

namespace JHPermrec.UpdateRecord.Transfer
{
    public partial class AddTransStudBaseData : FISCA.Presentation.Controls.BaseForm
    {
        private string _StudentID;
        private bool _isInitialized = false;
        Dictionary<string, string> _zip_code_mapping;
        private JHSchool.Data.JHParentRecord ParentRec;
        private JHSchool.Data.JHAddressRecord AddressRec;
        private JHSchool.Data.JHStudentRecord  student;

        private EnhancedErrorProvider _warnings;
        private EnhancedErrorProvider _errors = new EnhancedErrorProvider();

        public AddTransStudBaseData(JHSchool.Data.JHStudentRecord studentEntity)
        {                        
            InitializeComponent();
            student = studentEntity;
            _StudentID = student.ID;
            ParentRec = JHSchool.Data.JHParent.SelectByStudentID(_StudentID);
            AddressRec = JHSchool.Data.JHAddress.SelectByStudentID(_StudentID);
            LoadStudParentInfo();
            LoadStudAddress();

            string msg = "班級:";
            if (student.Class != null)            
                msg += student.Class.Name;
            if (student.SeatNo.HasValue)
                msg += ", 座號:" + student.SeatNo.Value;
             msg+=", 姓名:" + student.Name + ", 學號:" + student.StudentNumber;
            
            lblStudName.Text = msg;
            chkNextYes.Checked = true;
            cboAlive.Items.Add("存");
            cboAlive.Items.Add("歿");
            cboAlive.Items.Add("");

            // 讀取稱謂
            DSXmlHelper helper = Config.GetRelationship().GetContent();
            foreach (XmlNode node in helper.GetElements("Relationship"))
            {
                //cboRelationship.Items.Add(new KeyValuePair<string, string>(node.InnerText, node.InnerText));
                cboRelationship.Items.Add(node.InnerText);
            }

            _warnings = new EnhancedErrorProvider();

            _zip_code_mapping = new Dictionary<string, string>();

            AddTransBackgroundManager.AddTransStudBaseDataObj = this;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 載入學生父母監護人資料
        private void LoadStudParentInfo()
        {
            LoadGuardian();
        }

        // 載入學生地址資料
        private void LoadStudAddress()
        {
            DisplayAddress("戶籍地址");        
        }


        private void btnMother_Click(object sender, EventArgs e)
        {
            lblAlive.Visible = true;
            cboAlive.Visible = true;
            lblRelation.Visible = false;
            cboRelationship.Visible = false;
            btnGuardian.Enabled = true;
            btnFather.Enabled = true;
            btnMother.Enabled = false;
            btnParentType.Text = btnMother.Text;
            txtParentName.Text = ParentRec.Mother.Name;
            txtIDNumber.Text = ParentRec.Mother.IDNumber;
            txtParentPhone.Text = ParentRec.Mother.Phone;
            cboAlive.Text = ParentRec.Mother.Living;

            lnkCopyGuardian.Visible = true;
            lnkCopyFather.Visible = false;
            lnkCopyMother.Visible = false;
        }

        private void btnFather_Click(object sender, EventArgs e)
        {
            lblAlive.Visible = true;
            cboAlive.Visible = true;
            lblRelation.Visible = false;
            cboRelationship.Visible = false;

            btnGuardian.Enabled = true;
            btnFather.Enabled = false;
            btnMother.Enabled = true;

            btnParentType.Text = btnFather.Text;
            txtParentName.Text = ParentRec.Father.Name;
            txtIDNumber.Text = ParentRec.Father.IDNumber;
            txtParentPhone.Text = ParentRec.Father.Phone;
            cboAlive.Text = ParentRec.Father.Living;
            lnkCopyGuardian.Visible = true;
            lnkCopyFather.Visible = false;
            lnkCopyMother.Visible = false;
        }

        private void btnGuardian_Click(object sender, EventArgs e)
        {
            LoadGuardian();
        }

        private void btnPAddress_Click(object sender, System.EventArgs e)
        {
            if (_errors.HasError)
            {
                MsgBox.Show("資料錯誤，請修正資料");
                return;
            }
            DisplayAddress("戶籍地址");
        }

        private void btnFAddress_Click(object sender, System.EventArgs e)
        {
            if (_errors.HasError)
            {
                MsgBox.Show("資料錯誤，請修正資料");
                return;
            }

            DisplayAddress("聯絡地址");
        }

        private void btnOAddress_Click(object sender, System.EventArgs e)
        {
            if (_errors.HasError)
            {
                MsgBox.Show("資料錯誤，請修正資料");
                return;
            }

            DisplayAddress("其它地址");
        }


        private void lnkCopyFather_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtParentName.Text = ParentRec.Father.Name;
            txtIDNumber.Text = ParentRec.Father.IDNumber;
            txtParentPhone.Text = ParentRec.Father.Phone;
            if (btnParentType.Text == "監護人")
                cboRelationship.Text = "父";
            
        }

        private void lnkCopyMother_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtParentName.Text = ParentRec.Mother.Name;
            txtIDNumber.Text = ParentRec.Mother.IDNumber;
            txtParentPhone.Text = ParentRec.Mother.Phone;
            if (btnParentType.Text == "監護人")
                cboRelationship.Text = "母";

        }


        private void lnkCopyGuardian_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtParentName.Text = ParentRec.Custodian.Name;
            txtIDNumber.Text = ParentRec.Custodian.IDNumber;
            txtParentPhone.Text = ParentRec.Custodian.Phone;
 

        }


        private void LoadGuardian()
        {
            btnGuardian.Enabled = false;
            btnFather.Enabled = true;
            btnMother.Enabled = true;

            lblRelation.Visible = true;
            cboRelationship.Visible = true;
            lblAlive.Visible = false;
            cboAlive.Visible = false;
            btnParentType.Text = btnGuardian.Text;

            txtParentName.Text = ParentRec.Custodian.Name;
            txtIDNumber.Text = ParentRec.Custodian.IDNumber;
            // Add parent phone.
            txtParentPhone.Text = ParentRec.Custodian.Phone;
            cboRelationship.Text = ParentRec.Custodian.Relationship;

            lnkCopyGuardian.Visible = false;
            lnkCopyFather.Visible = true;
            lnkCopyMother.Visible = true;
        }


        private void txtParentPhone_TextChanged(object sender, EventArgs e)
        {
            if (btnParentType.Text == "監護人")
                ParentRec.Custodian.Phone  = txtParentPhone.Text;

            if (btnParentType.Text == "父親")
                ParentRec.Father.Phone = txtParentPhone.Text;

            if (btnParentType.Text == "母親")            
                ParentRec.Mother.Phone = txtParentPhone.Text;
            
        }

        private void txtParentName_TextChanged(object sender, EventArgs e)
        {

            if (btnParentType.Text == "監護人")
                ParentRec.Custodian.Name  = txtParentName.Text;

            if (btnParentType.Text == "父親")
                ParentRec.Father.Name  = txtParentName.Text;

            if (btnParentType.Text == "母親")
                ParentRec.Mother.Name  = txtParentName.Text;
            
        }

        private void cboRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnParentType.Text == "監護人")
                ParentRec.Custodian.Relationship = cboRelationship.Text;
        }

        private void cboAlive_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (btnParentType.Text == "父親")
                ParentRec.Father.Living = cboAlive.Text;

            if (btnParentType.Text == "母親")
                ParentRec.Mother.Living = cboAlive.Text;
        }   


        private void cboCounty_TextChanged(object sender, EventArgs e)
        {
            cboTown.SelectedItem = null;
            cboTown.Items.Clear();
            if (cboCounty.Text != "")
            {
                XmlElement[] townList = Framework.Feature.Config.GetTownList(cboCounty.Text);
                _zip_code_mapping = new Dictionary<string, string>();
                foreach (XmlElement each in townList)
                {
                    string name = each.GetAttribute("Name");

                    if (!_zip_code_mapping.ContainsKey(name))
                        _zip_code_mapping.Add(name, each.GetAttribute("Code"));

                    cboTown.Items.Add(name);
                }
            }


            if (btnAddressType.Text == "戶籍地址")
                AddressRec.Permanent.County  = cboCounty.Text;

            if (btnAddressType.Text == "聯絡地址")
                AddressRec.Mailing.County  = cboCounty.Text;

            if (btnAddressType.Text == "其它地址")
                AddressRec.Address1.County  = cboCounty.Text;

            ShowFullAddress();
        }


        private void CheckTownChange()
        {
            string value = cboTown.Text;
            if (!string.IsNullOrEmpty(value))
            {
                if (_zip_code_mapping.ContainsKey(value))
                    txtZipcode.Text = _zip_code_mapping[value];
            }

            if (btnAddressType.Text == "戶籍地址")
                AddressRec.Permanent.Town = cboTown.Text;

            if (btnAddressType.Text == "聯絡地址")
                AddressRec.Mailing.Town = cboTown.Text;

            if (btnAddressType.Text == "其它地址")
                AddressRec.Address1.Town = cboTown.Text;

            ShowFullAddress();
        }

        private void ShowFullAddress()
        {
            string fullAddress = "";
            if (txtZipcode.Text != "")
                fullAddress += "[" + txtZipcode.Text + "]";
            fullAddress += cboCounty.Text;
            fullAddress += cboTown.Text;
            fullAddress += txtDistrict.Text;
            fullAddress += txtArea.Text;
            fullAddress += txtDetail.Text;
            this.lblFullAddress.Text = fullAddress;
        }

        private void txtZipcode_TextChanged(object sender, EventArgs e)
        {
            ShowFullAddress();
            if (_date_updating) return;

            decimal d;
            if (!string.IsNullOrEmpty(txtZipcode.Text) && !decimal.TryParse(txtZipcode.Text, out d))
            {
                _errors.SetError(txtZipcode, "郵遞區號必須為數字形態");
                return;
            }
            else
                _errors.SetError(txtZipcode, ""); //清除錯誤。


            if (btnAddressType.Text == "戶籍地址")
                AddressRec.Permanent.ZipCode =txtZipcode.Text ;

            if (btnAddressType.Text == "聯絡地址")
                AddressRec.Mailing.ZipCode  =txtZipcode.Text ;

            if (btnAddressType.Text == "其它地址")
                AddressRec.Address1.ZipCode  = txtZipcode.Text;

        }

        private void txtZipcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (_date_updating) return;

            if (e.KeyCode == Keys.Enter)
                CheckZipCode();
        }

        private void txtZipcode_Validated(object sender, EventArgs e)
        {
            if (_date_updating) return;

            if (!_errors.ContainError(txtZipcode))
                CheckZipCode();
        }

        private void CheckZipCode()
        {
            KeyValuePair<string, string> ctPair = Framework.Feature.Config.FindTownByZipCode(txtZipcode.Text);
            if (ctPair.Key == null)
                _warnings.SetError(txtZipcode, "查無此郵遞區號對應縣市鄉鎮資料。");
            else
            {
                _warnings.SetError(txtZipcode, string.Empty);

                string county = ctPair.Key;
                string town = ctPair.Value;

                cboCounty.Text =county;
                cboTown.Text =town;
            }
        }

        private bool _date_updating = false;
        private void DisplayAddress(string strAddressType)
        {
            _date_updating = true;
            btnAddressType.Text = strAddressType;
            if (btnAddressType.Text == "戶籍地址")
            {
                lnklblAddress1.Text = "複製聯絡地址";
                lnklblAddress2.Text = "複製其它地址";
                cboCounty.Text = AddressRec.Permanent.County;
                cboTown.Text = AddressRec.Permanent.Town;
                txtDistrict.Text = AddressRec.Permanent.District;
                txtArea.Text = AddressRec.Permanent.Area;
                txtDetail.Text = AddressRec.Permanent.Detail;
                txtZipcode.Text = AddressRec.Permanent.ZipCode;
            }

            if (btnAddressType.Text == "聯絡地址")
            {
                lnklblAddress1.Text = "複製戶籍地址";
                lnklblAddress2.Text = "複製其它地址";
                cboCounty.Text = AddressRec.Mailing.County;
                cboTown.Text = AddressRec.Mailing.Town;
                txtDistrict.Text = AddressRec.Mailing.District;
                txtArea.Text = AddressRec.Mailing.Area;
                txtDetail.Text = AddressRec.Mailing.Detail;
                txtZipcode.Text = AddressRec.Mailing.ZipCode;
            }

            if (btnAddressType.Text == "其它地址")
            {
                lnklblAddress1.Text = "複製聯絡地址";
                lnklblAddress2.Text = "複製戶籍地址";
                cboCounty.Text = AddressRec.Address1.County;
                cboTown.Text = AddressRec.Address1.Town;
                txtDistrict.Text = AddressRec.Address1.District;
                txtArea.Text = AddressRec.Address1.Area;
                txtDetail.Text = AddressRec.Address1.Detail;
                txtZipcode.Text = AddressRec.Address1.ZipCode;
            }


            _date_updating = false;
        }

        private void txtDistrict_TextChanged(object sender, EventArgs e)
        {
            ShowFullAddress();


            if (btnAddressType.Text == "戶籍地址")            
                AddressRec.Permanent.District  = txtDistrict.Text;
            

            if (btnAddressType.Text == "聯絡地址")            
                AddressRec.Mailing.District = txtDistrict.Text;
            
            if (btnAddressType.Text == "其它地址")            
                AddressRec.Address1.District = txtDistrict.Text;
        }

        private void txtArea_TextChanged(object sender, EventArgs e)
        {
            ShowFullAddress();
            if (btnAddressType.Text == "戶籍地址")
                AddressRec.Permanent.Area  =txtArea.Text ;

            if (btnAddressType.Text == "聯絡地址")
                AddressRec.Mailing.Area  =txtArea.Text;

            if (btnAddressType.Text == "其它地址")
                AddressRec.Address1.Area  = txtArea.Text;
        }

        private void lnklblAddress1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (btnAddressType.Text == "戶籍地址")
            {
                //複製聯絡地址
                txtZipcode.Text = AddressRec.Mailing.ZipCode;
                cboCounty.Text = AddressRec.Mailing.County;
                cboTown.Text = AddressRec.Mailing.Town;
                txtDistrict.Text = AddressRec.Mailing.District;
                txtArea.Text = AddressRec.Mailing.Area;
                txtDetail.Text = AddressRec.Mailing.Detail;
            }

            if (btnAddressType.Text == "聯絡地址")
            {
                // 複製戶籍地址
                txtZipcode.Text = AddressRec.Permanent.ZipCode;
                cboCounty.Text = AddressRec.Permanent.County;
                cboTown.Text = AddressRec.Permanent.Town;
                txtDistrict.Text = AddressRec.Permanent.District;
                txtArea.Text = AddressRec.Permanent.Area;
                txtDetail.Text = AddressRec.Permanent.Detail;
            }

            if (btnAddressType.Text == "其它地址")
            {
                //複製聯絡地址
                txtZipcode.Text = AddressRec.Mailing.ZipCode;
                cboCounty.Text = AddressRec.Mailing.County;
                cboTown.Text = AddressRec.Mailing.Town;
                txtDistrict.Text = AddressRec.Mailing.District;
                txtArea.Text = AddressRec.Mailing.Area;
                txtDetail.Text = AddressRec.Mailing.Detail;
            }

        }

        private void lnklblAddress2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (btnAddressType.Text == "戶籍地址")
            {
                // 複製其它地址
                txtZipcode.Text = AddressRec.Address1.ZipCode;
                cboCounty.Text = AddressRec.Address1.County;
                cboTown.Text = AddressRec.Address1.Town;
                txtDistrict.Text = AddressRec.Address1.District;
                txtArea.Text = AddressRec.Address1.Area;
                txtDetail.Text = AddressRec.Address1.Detail;
            }

            if (btnAddressType.Text == "聯絡地址")
            {
                // 複製其它地址
                txtZipcode.Text = AddressRec.Address1.ZipCode;
                cboCounty.Text = AddressRec.Address1.County;
                cboTown.Text = AddressRec.Address1.Town;
                txtDistrict.Text = AddressRec.Address1.District;
                txtArea.Text = AddressRec.Address1.Area;
                txtDetail.Text = AddressRec.Address1.Detail;
            }

            if (btnAddressType.Text == "其它地址")
            {
                // 複製戶籍地址
                txtZipcode.Text = AddressRec.Permanent.ZipCode;
                cboCounty.Text = AddressRec.Permanent.County;
                cboTown.Text = AddressRec.Permanent.Town;
                txtDistrict.Text = AddressRec.Permanent.District;
                txtArea.Text = AddressRec.Permanent.Area;
                txtDetail.Text = AddressRec.Permanent.Detail;

            }
        }


        private void cboTown_TextChanged(object sender, EventArgs e)
        {
            if (_date_updating) return;
            CheckTownChange();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ParentRec != null)
                JHSchool.Data.JHParent.Update(ParentRec);

            if (AddressRec != null)
                JHSchool.Data.JHAddress.Update(AddressRec);

            // log
            JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
            prlp.SaveLog("學生.轉入異動", "新增", "新增地址與父母監護人資料..");

            this.Close();
        }

        private void txtIDNumber_TextChanged(object sender, EventArgs e)
        {
            if (btnParentType.Text == "監護人")
            {
                ParentRec.Custodian.IDNumber = txtIDNumber.Text;
            }
            else if (btnParentType.Text == "父親")
            {
                ParentRec.Father.IDNumber = txtIDNumber.Text;
            }
            else
            {
                ParentRec.Mother.IDNumber = txtIDNumber.Text;
            }
        }

        private void txtDetail_TextChanged(object sender, EventArgs e)
        {
            ShowFullAddress();

            if (btnAddressType.Text == "戶籍地址")            
                AddressRec.Permanent.Detail = txtDetail.Text;
            
            if (btnAddressType.Text == "聯絡地址")            
                AddressRec.Mailing.Detail = txtDetail.Text;            

            if (btnAddressType.Text == "其它地址")            
                AddressRec.Address1.Detail = txtDetail.Text;
        }
    }
}
