using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MasterMech
{
    public partial class CustomerForm : Form
    {
        private MasterMechUtil.OPMode mnMode;
        bool ibNewMode = false;
        public CustomerForm()
        {
            InitializeComponent();
        }

        public CustomerForm(MasterMechUtil.OPMode inMode)
        {
            mnMode = (MasterMechUtil.OPMode)inMode;
            InitializeComponent();
        }

        private bool ValidInput()
        {
            if (textBoxCustFName.Text.Length == 0)
            {
                labelCustFName.Visible = true;
                return false;
            }

            if (textBox2CustLName.Text.Length == 0)
            {
                labelCustLName.Visible = true;
                return false;
            }

            if (textBoxCustMobNo.Text.Length == 0)
            {
                labelCustMonNo.Visible = true;
                return false;
            }

            if (textBoxCustEmail.Text.Length == 0)
            {
                labelcustEmail.Visible = true;
                return false;
            }

            if (comboBoxCustSts.Text.Length == 0)
            {
                labelCustSts.Visible = true;
                return false;
            }

            if (comboBoxCustType.Text.Length == 0)
            {
                labelCustType.Visible = true;
                return false;
            }

            
            return true;
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            switch (mnMode)
            {
                case MasterMechUtil.OPMode.New:
                    buttonSearchCustFName.Visible = false;
                    buttonAdvance.Visible = false;
                    break;

                case MasterMechUtil.OPMode.Delete:

                    this.buttonSave.Text = "Delete";
                    break;
            }
        }

        private void textBoxCustFName_Enter(object sender, EventArgs e)
        {
            labelCustFName.Visible = false;
        }

        private void textBox2CustLName_Enter(object sender, EventArgs e)
        {
            labelCustLName.Visible = false;
        }

        private void comboBoxCustSts_Enter(object sender, EventArgs e)
        {
            labelCustSts.Visible = false;
        }

        private void comboBoxCustType_Enter(object sender, EventArgs e)
        {
            labelCustType.Visible = false;
        }

        private void textBoxCustMobNo_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxCustMobNo.Text.Length > 0)
            {
                bool lbValidMobNO = Regex.IsMatch(this.textBoxCustMobNo.Text, @"^(\d{10})$", RegexOptions.IgnoreCase);
                if (!lbValidMobNO)
                {
                    textBoxCustMobNo.ForeColor = Color.Red;
                    textBoxCustMobNo.Font = new Font(textBoxCustMobNo.Font, FontStyle.Bold);
                    labelCustMonNo.Visible = true;
                    e.Cancel = true;
                }
            }
            
        }

        private void textBoxCustEmail_Validating(object sender, CancelEventArgs e)
        {
            if (this.textBoxCustEmail.Text.Length > 0)
            {
                bool lbValidEmail = Regex.IsMatch(this.textBoxCustEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (!lbValidEmail)
                {
                    textBoxCustEmail.ForeColor = Color.Red;
                    textBoxCustEmail.Font = new Font(textBoxCustEmail.Font, FontStyle.Bold);
                    labelcustEmail.Visible = true;
                    e.Cancel = true;
                }
            }
            
        }

        private void textBoxCustMobNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxCustMobNo.Font.Bold)
            {
                textBoxCustMobNo.ForeColor = Color.Black;
                textBoxCustMobNo.Font = new Font(textBoxCustMobNo.Font, FontStyle.Regular);
                labelCustMonNo.Visible = false;
            }
        }

        private void textBoxCustEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxCustEmail.Font.Bold)
            {
                textBoxCustEmail.ForeColor = Color.Black;
                textBoxCustEmail.Font = new Font(textBoxCustEmail.Font, FontStyle.Regular);
                labelcustEmail.Visible = false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            switch (mnMode)
            {
                case MasterMechUtil.OPMode.New:
                    if (!ValidInput())
                        return;
                     ibNewMode = true;
                     SaveCustomer();
                    break;

                case MasterMechUtil.OPMode.Open:
                    if (!ValidInput())
                        return;
                    SaveCustomer();
                    break;


                case MasterMechUtil.OPMode.Delete:

                    if (textBoxCustNo.Text.Length > 0)
                    {
                        if (MessageBox.Show("Are you sure ?", "Deleting User" + textBoxCustNo.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                            DeleteCust();
                    }

                    else
                    {
                        MessageBox.Show("First Select a User ID");
                        this.buttonSave.Text = "Delete";
                    }
                    break;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void SaveCustomer()
        {
            CustomerClass lobjCust = new CustomerClass();
            lobjCust.lsCustFName = textBoxCustFName.Text;
            lobjCust.lsCustLName = textBox2CustLName.Text;
            lobjCust.lsCustMobNo = textBoxCustMobNo.Text;
            lobjCust.lsCustEmail = textBoxCustEmail.Text;
            lobjCust.lsCustSts = comboBoxCustSts.Text;
            lobjCust.CustType = comboBoxCustType.Text;
            
                
            if (textBoxCustStAddr.Text.Length > 0)
                lobjCust.lsCustStAddr = textBoxCustStAddr.Text;
            else
                lobjCust.lsCustStAddr = null;

            if (textBoxCustArAddr.Text.Length > 0)
                lobjCust.lsCustArAddr = textBoxCustArAddr.Text;
            else
                lobjCust.lsCustArAddr = null;

            if (textBoxCustCity.Text.Length > 0)
                lobjCust.lsCustCity = textBoxCustCity.Text;
            else
                lobjCust.lsCustCity = null;

            if (textBoxCustState.Text.Length > 0)
                lobjCust.lsCustState = textBoxCustState.Text;
            else
                lobjCust.lsCustState = null;

            if (textBoxCustPinCode.Text.Length > 0)
                lobjCust.CustPinCode = textBoxCustPinCode.Text;
            else
                lobjCust.CustPinCode = null;

            if (textBoxCustCountry.Text.Length > 0)
                lobjCust.lsCustCountry = textBoxCustCountry.Text;
            else
                lobjCust.lsCustCountry = null;

            if (textBoxCustGSTNo.Text.Length > 0)
                lobjCust.lsCustGSTNo = textBoxCustGSTNo.Text;
            else
                lobjCust.lsCustGSTNo = null;

            if (textBoxCustRemarks.Text.Length > 0)
                lobjCust.lsCustRemarks = textBoxCustRemarks.Text;
            else
                lobjCust.lsCustRemarks = null;

            lobjCust.lsCreatedBy = labelCreatedBy.Text;

            if (textBoxCustNo.Text.Length > 0)
                lobjCust.lnCustNo = int.Parse(textBoxCustNo.Text);
            else
                lobjCust.lnCustNo = 0;




            if (!lobjCust.SaveCCustomer(MasterMechUtil.ConnStr, lobjCust.lnCustNo, ibNewMode))
                // if (!lObjTest.SaveSQLParam())
                //if (!lObjTest.SaveSQL_SP())
                MessageBox.Show("Test Data could not be saved");
            else
                ClearFields();
        }

        private void ClearFields()
        {
            textBoxCustFName.Clear();
            textBox2CustLName.Clear();
            textBoxCustMobNo.Clear();
            textBoxCustEmail.Clear();
            comboBoxCustSts.SelectedIndex = -1;
            comboBoxCustType.SelectedIndex = -1;
            textBoxCustStAddr.Clear();
            textBoxCustArAddr.Clear();
            textBoxCustCity.Clear();
            textBoxCustState.Clear();
            textBoxCustPinCode.Clear();
            textBoxCustCountry.Clear();
            textBoxCustGSTNo.Clear();
            textBoxCustRemarks.Clear();
            textBoxCustNo.Clear();
        }

        private void buttonSearchCustFName_Click(object sender, EventArgs e)
        {
            if (textBoxCustFName.Text.Length == 0)
            {
                MessageBox.Show("Please enter the User Name  to search.");
                this.textBoxCustFName.Focus();
                return;
            }

            // object to call search Test Mthod
            CustomerClass lObjCust = new CustomerClass();
            List<CustomerClass> lObjCusts = new List<CustomerClass>();


            // Get the list of customers based on Mobile No search
            lObjCust.SearchCustDlt(MasterMechUtil.ConnStr, textBoxCustFName.Text, lObjCusts);
            if (lObjCusts.Count == 0)
            {
                MessageBox.Show("No Test found!");
                this.textBoxCustFName.Focus();
                return;
            }
            else if (lObjCusts.Count == 1)
            {
                LoadCustomer(lObjCusts[0].lnCustNo);
                return;
            }

            // More than 1 Test found after the search then show the result in the grid
            ItemSearchResultForm lObjSearchCustF = new ItemSearchResultForm();
            lObjSearchCustF.NoSelectMsg = "No Customer selected. Select a customer row and then click select";


            // Build the header
            lObjSearchCustF.ItemSearchdataGridView.ReadOnly = true;
            lObjSearchCustF.ItemSearchdataGridView.AllowUserToAddRows = false;
            lObjSearchCustF.ItemSearchdataGridView.ColumnCount = 16;
            lObjSearchCustF.ItemSearchdataGridView.Columns[0].Name = "S.NO";
            lObjSearchCustF.ItemSearchdataGridView.Columns[1].Name = "CustNo";
            lObjSearchCustF.ItemSearchdataGridView.Columns[2].Name = "CustFName";
            lObjSearchCustF.ItemSearchdataGridView.Columns[3].Name = "CustLName";
            lObjSearchCustF.ItemSearchdataGridView.Columns[4].Name = "CustMobNo";
            lObjSearchCustF.ItemSearchdataGridView.Columns[5].Name = "CustEmail";
            lObjSearchCustF.ItemSearchdataGridView.Columns[6].Name = "CustSts";
            lObjSearchCustF.ItemSearchdataGridView.Columns[7].Name = "CustType";
            lObjSearchCustF.ItemSearchdataGridView.Columns[8].Name = "CustStAddr";
            lObjSearchCustF.ItemSearchdataGridView.Columns[9].Name = "CustArAddr";
            lObjSearchCustF.ItemSearchdataGridView.Columns[10].Name = "CustCity";
            lObjSearchCustF.ItemSearchdataGridView.Columns[11].Name = "CustState";
            lObjSearchCustF.ItemSearchdataGridView.Columns[12].Name = "CustPinCode";
            lObjSearchCustF.ItemSearchdataGridView.Columns[13].Name = "CustCountry";
            lObjSearchCustF.ItemSearchdataGridView.Columns[14].Name = "CustGSTNo";
            lObjSearchCustF.ItemSearchdataGridView.Columns[15].Name = "CustRemarks";


            // Load the data in the grid
            int lnCnt = 1;
            foreach (CustomerClass lObjSearchCust in lObjCusts)
                lObjSearchCustF.ItemSearchdataGridView.Rows.Add(lnCnt++, lObjSearchCust.lnCustNo, lObjSearchCust.lsCustFName,
                lObjSearchCust.lsCustLName, lObjSearchCust.lsCustMobNo, lObjSearchCust.lsCustEmail,
                lObjSearchCust.lsCustSts, lObjSearchCust.CustType, lObjSearchCust.lsCustStAddr,
                lObjSearchCust.lsCustArAddr, lObjSearchCust.lsCustCity, lObjSearchCust.lsCustState, lObjSearchCust.CustPinCode,
                lObjSearchCust.lsCustCountry, lObjSearchCust.lsCustGSTNo, lObjSearchCust.lsCustRemarks);

            lObjSearchCustF.ShowDialog();
            int lnSelectedRow = lObjSearchCustF.mSelectedRow;
            int lnItemDesc = (int)lObjSearchCustF.ItemSearchdataGridView.Rows[lnSelectedRow].Cells[1].Value;
            if (lObjSearchCustF.mbSelected)
                LoadCustomer(lnItemDesc);
            else
                this.textBoxCustFName.Focus();

        }

        public void LoadCustomer(int lnItemDesc)
        {
            CustomerClass lObjItUde = new CustomerClass();
            //UserDtl lObjTest = new UserDtl(Utility.msConnStr, Utility.msUserID);
            if (lObjItUde.Load(MasterMechUtil.ConnStr, lnItemDesc))
            {
                textBoxCustNo.Text = lnItemDesc.ToString();
                textBoxCustFName.Text = lObjItUde.lsCustFName;
                textBox2CustLName.Text = lObjItUde.lsCustLName;
                textBoxCustMobNo.Text = lObjItUde.lsCustMobNo;
                textBoxCustEmail.Text = lObjItUde.lsCustEmail;
                comboBoxCustSts.Text = lObjItUde.lsCustSts;
                comboBoxCustType.Text = lObjItUde.CustType.ToString();
                textBoxCustStAddr.Text = lObjItUde.lsCustStAddr;
                textBoxCustArAddr.Text = lObjItUde.lsCustArAddr;
                textBoxCustCity.Text = lObjItUde.lsCustCity;
                textBoxCustState.Text = lObjItUde.lsCustState;
                textBoxCustPinCode.Text = lObjItUde.CustPinCode.ToString();
                textBoxCustCountry.Text = lObjItUde.lsCustCountry;
                textBoxCustGSTNo.Text = lObjItUde.lsCustGSTNo;
                textBoxCustRemarks.Text = lObjItUde.lsCustRemarks;
                
                labelCreated.Text = lObjItUde.dCreated.ToString();
                labelCreatedBy.Text = lObjItUde.lsCreatedBy;
                labelMmodified.Text = lObjItUde.dModified.ToString();
                labelModifiedVisit.Text = lObjItUde.lsModifiedBy;
                labelDeletedOn.Text = lObjItUde.dDeletedOn.ToString();
                labelDeletedBy.Text = lObjItUde.lsDeletedBy;
                labelDeleted.Text = lObjItUde.Deleted.ToString();
            }
        }

        private void DeleteCust()
        {
            CustomerClass lobjDeleteCust = new CustomerClass();
            lobjDeleteCust.lnCustNo = int.Parse(textBoxCustNo.Text);
            lobjDeleteCust.Delete(MasterMechUtil.ConnStr, lobjDeleteCust.lnCustNo);
            ClearFields();
        }

        private void buttonAdvance_Click(object sender, EventArgs e)
        {
            AdvanceSearchForm objladvance = new AdvanceSearchForm();
            objladvance.ShowDialog();
        }

       
    }
}
