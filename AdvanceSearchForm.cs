using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterMech
{
    public partial class AdvanceSearchForm : Form
    {
        CustomerForm objCustomerform = new CustomerForm();
       // InvoiceForm lobjInvoice = new InvoiceForm();
        public AdvanceSearchForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if ((textBoxFirstName.Text.Length == 0)&&(textBoxLastName.Text.Length == 0)&&(textBoxCity.Text.Length == 0))
            {
                MessageBox.Show("Please enter the User Name  to search.");
                this.textBoxFirstName.Focus();
                return;
            }


            CustomerClass lObjCust = new CustomerClass();
            List<CustomerClass> lObjCusts = new List<CustomerClass>();

            lObjCust.SearchAdvance(MasterMechUtil.ConnStr, textBoxFirstName.Text, textBoxLastName.Text, textBoxCity.Text, lObjCusts);
            if (lObjCusts.Count == 0)
            {
                MessageBox.Show("No Test found!");
                this.textBoxFirstName.Focus();
                return;
            }
            else if (lObjCusts.Count == 1)
            {
                //lobjInvoice.LoadCustomer(lObjCusts[0].lnCustNo);
                objCustomerform.LoadCustomer(lObjCusts[0].lnCustNo);
                objCustomerform.ShowDialog();
               // lobjInvoice.ShowDialog();
                return;
            }

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
            {
                //lobjInvoice.LoadCustomer(lnItemDesc);
                objCustomerform.LoadCustomer(lnItemDesc);
                objCustomerform.ShowDialog();
               // lobjInvoice.ShowDialog();
            }
            else
                this.textBoxFirstName.Focus();

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
