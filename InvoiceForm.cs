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
   
    public partial class InvoiceForm : Form
    {
        private MasterMechUtil.OPMode mnMode;
        bool mbSameState = true;
        public int? mnupdate;
        public int? mnLineOpMode = 0;
        public int LineModeUpdate = 2;
        public int LinemodeAdd = 1;
        public int mSelectedRow = -1;
        public int? mnupdRow;
        public InvoiceForm()
        {
            InitializeComponent();
        }

        public InvoiceForm(MasterMechUtil.OPMode inMode)
        {
            mnMode = (MasterMechUtil.OPMode)inMode;
             InitializeComponent();
        }

        private void buttonSearchMobile_Click(object sender, EventArgs e)
        {
            if (textBoxCustoMob.Text.Length == 0)
            {
                MessageBox.Show("Please enter the User Name  to search.");
                this.textBoxCustoMob.Focus();
                return;
            }

            CustomerClass lObjCust = new CustomerClass();
            List<CustomerClass> lObjCusts = new List<CustomerClass>();

            lObjCust.SearchCustMobile(MasterMechUtil.ConnStr, textBoxCustoMob.Text, lObjCusts);
            if (lObjCusts.Count == 0)
            {
                MessageBox.Show("No Test found!");
                this.textBoxCustoMob.Focus();
                return;
            }
            else if (lObjCusts.Count == 1)
            {
                LoadCustomer(lObjCusts[0].lnCustNo);
                return;
            }

            ItemSearchResultForm lObjSearchCustF = new ItemSearchResultForm();
            lObjSearchCustF.NoSelectMsg = "No Customer selected. Select a customer row and then click select";


            // Build the header
            lObjSearchCustF.ItemSearchdataGridView.ReadOnly = true;
            lObjSearchCustF.ItemSearchdataGridView.AllowUserToAddRows = false;
            lObjSearchCustF.ItemSearchdataGridView.ColumnCount = 18;
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
            lObjSearchCustF.ItemSearchdataGridView.Columns[16].Name = "CustLastVisit";


            // Load the data in the grid
            int lnCnt = 1;
            foreach (CustomerClass lObjSearchCust in lObjCusts)
                lObjSearchCustF.ItemSearchdataGridView.Rows.Add(lnCnt++, lObjSearchCust.lnCustNo, lObjSearchCust.lsCustFName,
                lObjSearchCust.lsCustLName, lObjSearchCust.lsCustMobNo, lObjSearchCust.lsCustEmail,
                lObjSearchCust.lsCustSts, lObjSearchCust.CustType, lObjSearchCust.lsCustStAddr,
                lObjSearchCust.lsCustArAddr, lObjSearchCust.lsCustCity, lObjSearchCust.lsCustState, lObjSearchCust.CustPinCode,
                lObjSearchCust.lsCustCountry, lObjSearchCust.lsCustGSTNo, lObjSearchCust.lsCustRemarks, lObjSearchCust.dCustlLastVisit);

            lObjSearchCustF.ShowDialog();
            int lnSelectedRow = lObjSearchCustF.mSelectedRow;
            int lnItemDesc = (int)lObjSearchCustF.ItemSearchdataGridView.Rows[lnSelectedRow].Cells[1].Value;
            if (lObjSearchCustF.mbSelected)

                LoadCustomer(lnItemDesc);
            else
                this.textBoxCustoMob.Focus();

        }

        public void LoadCustomer(int lnItemDesc)
        {
            CustomerClass lObjItUde = new CustomerClass();
            //UserDtl lObjTest = new UserDtl(Utility.msConnStr, Utility.msUserID);
            if (lObjItUde.Load(MasterMechUtil.ConnStr, lnItemDesc))
            {
                textBoxCustNo.Text = lnItemDesc.ToString();
                textBoxFName.Text = lObjItUde.lsCustFName;
                textBoxLName.Text = lObjItUde.lsCustLName;
                textBoxCustMob.Text = lObjItUde.lsCustMobNo;
                textBoxEmail.Text = lObjItUde.lsCustEmail;
                textBoxStatus.Text = lObjItUde.lsCustSts;
                textBoxType.Text = lObjItUde.CustType.ToString();
                textBoxStreetAdd.Text = lObjItUde.lsCustStAddr;
                textBoxAreaAdd.Text = lObjItUde.lsCustArAddr;
                textBoxCity.Text = lObjItUde.lsCustCity;
                textBoxState.Text = lObjItUde.lsCustState;
                textBoxPinCode.Text = lObjItUde.CustPinCode.ToString();
                textBoxCountry.Text = lObjItUde.lsCustCountry;
                textBoxGSTNo.Text = lObjItUde.lsCustGSTNo;
                textBoxRemarks.Text = lObjItUde.lsCustRemarks;
                textBoxLastVisit.Text = lObjItUde.dCustlLastVisit.ToString();

                /*.Text = lObjItUde.dCreated.ToString();
                labelCreatedBy.Text = lObjItUde.lsCreatedBy;
                labelMmodified.Text = lObjItUde.dModified.ToString();
                labelModifiedVisit.Text = lObjItUde.lsModifiedBy;*/
            }
        }

        private void buttonSerachItemDesc_Click(object sender, EventArgs e)
        {
            if (textBoxItemDesc.Text.Length == 0)
            {
                MessageBox.Show("Please enter the User Name  to search.");
                this.textBoxItemDesc.Focus();
                return;
            }

            Item lObjItem = new Item();
            List<Item> lObjItems = new List<Item>();

            // Get the list of customers based on Mobile No search
            lObjItem.SearchItems(MasterMechUtil.ConnStr, textBoxItemDesc.Text, lObjItems);
            if (lObjItems.Count == 0)
            {
                MessageBox.Show("No Test found!");
                this.textBoxItemDesc.Focus();
                return;
            }
            else if (lObjItems.Count == 1)
            {
                LoadItemData(lObjItems[0].ItemNo);
                return;
            }

            ItemSearchResultForm lObjSearchItemDesc = new ItemSearchResultForm();
            lObjSearchItemDesc.NoSelectMsg = "No Customer selected. Select a customer row and then click select";


            // Build the header
            lObjSearchItemDesc.ItemSearchdataGridView.ReadOnly = true;
            lObjSearchItemDesc.ItemSearchdataGridView.AllowUserToAddRows = false;
            lObjSearchItemDesc.ItemSearchdataGridView.ColumnCount = 14;
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[0].Name = "S.NO";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[1].Name = "ItemNo";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[2].Name = "ItemDesc";
            
            
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[3].Name = "ItemPrice";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[4].Name = "ItemUOM";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[7].Name = "ItemSts";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[5].Name = "CGSTRate";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[6].Name = "SGSTRate";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[7].Name = "IGSTRate";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[8].Name = "UPCCode";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[9].Name = "HSNCode";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[10].Name = "SACCode";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[11].Name = "ItemType";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[12].Name = "ItemCatg";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[13].Name = "ItemSts";



            int lnCnt = 1;
            foreach (Item lObjSearchItems in lObjItems)
                lObjSearchItemDesc.ItemSearchdataGridView.Rows.Add(lnCnt++, lObjSearchItems.ItemNo, lObjSearchItems.ItemDesc,
                lObjSearchItems.ItemPrice,
                lObjSearchItems.ItemUOM, lObjSearchItems.CGSTRate,
                lObjSearchItems.SGSTRate, lObjSearchItems.IGSTRate, lObjSearchItems.UPCCode, lObjSearchItems.HSNCode,
                lObjSearchItems.SACCode,lObjSearchItems.ItemType,lObjSearchItems.ItemCatg,lObjSearchItems.ItemSts);


            lObjSearchItemDesc.ShowDialog();
            int lnSelectedRow = lObjSearchItemDesc.mSelectedRow;
            int lnItemDesc = (int)lObjSearchItemDesc.ItemSearchdataGridView.Rows[lnSelectedRow].Cells[1].Value;
            if (lObjSearchItemDesc.mbSelected)
                LoadItemData(lnItemDesc);
            else
                this.textBoxItemDesc.Focus();
        }

        private void LoadItemData(int lnItemDesc)
        {
            Item lObjItUde = new Item();
            //UserDtl lObjTest = new UserDtl(Utility.msConnStr, Utility.msUserID);
            if (lObjItUde.Load(MasterMechUtil.ConnStr, lnItemDesc))
            {
                textBoxItemNo.Text = lnItemDesc.ToString();
                textBoxItemDesc.Text = lObjItUde.ItemDesc;
                
                
                
                textBoxItemPrice.Text = lObjItUde.ItemPrice.ToString();
                textBoxItemUOM.Text = lObjItUde.ItemUOM;

                textBoxCGST.Text = lObjItUde.CGSTRate.ToString();
                textBoxSGST.Text = lObjItUde.SGSTRate.ToString();
                textBoxIGST.Text = lObjItUde.IGSTRate.ToString();
                textBoxUPCCode.Text = lObjItUde.UPCCode;
                textBoxHSNCode.Text = lObjItUde.HSNCode;
                textBoxSACCode.Text = lObjItUde.SACCode;
                textBoxItemType.Text = lObjItUde.ItemType;
                textBoxItemCatg.Text = lObjItUde.ItemCatg;
                textBoxItemSts.Text = lObjItUde.ItemSts;
            }
        }

        //private void buttonSearchCustomer_Click(object sender, EventArgs e)
        //{
        //    AdvanceSearchForm lobjAdSe = new AdvanceSearchForm();
        //     lobjAdSe.ShowDialog();
        //}

        private void textBoxItemQty_Validating(object sender, CancelEventArgs e)
        {
            if (((TextBox)sender).Text.Length > 0)
            {
                bool lbValidSGST = Regex.IsMatch(((TextBox)sender).Text, @"^\d{0,2}(?:\.\d{0,2})?$", RegexOptions.IgnoreCase);
                if (!lbValidSGST)
                {
                    ((TextBox)sender).ForeColor = Color.Red;
                    ((TextBox)sender).Font = new Font(((TextBox)sender).Font, FontStyle.Bold);
                    labelerrorQty.Visible = true;
                    e.Cancel = true;
                }
            }
        }

        private void textBoxItemQty_Validated(object sender, EventArgs e)
        {
            float lnTotalTax = 0;
            float lnItemDiscount = 0;
            float lnTotalPrice = 0;
            float lnDiscountPrice = 0;

            if (textBoxItemDiscount.Text.Length > 0)
                lnItemDiscount = float.Parse(textBoxItemDiscount.Text);


            if (textBoxItemQty.Text.Length > 0)
            {
                lnTotalPrice = float.Parse(textBoxItemPrice.Text) * float.Parse(textBoxItemQty.Text);
                lnDiscountPrice = lnTotalPrice - lnItemDiscount;

                if (textBoxSGST.Text.Length > 0 && mbSameState)
                    textBoxSGSTAmt.Text = (lnDiscountPrice * float.Parse(textBoxSGST.Text) / 100.0).ToString("F");

                if (textBoxCGST.Text.Length > 0 && mbSameState)
                    textBoxCGSTAmt.Text = (lnDiscountPrice * float.Parse(textBoxCGST.Text) / 100.0).ToString("F");

                if (textBoxIGST.Text.Length > 0 && !mbSameState)
                    textBoxIGSTAmt.Text = (lnDiscountPrice * float.Parse(textBoxIGST.Text) / 100.0).ToString("F");
                
                textBoxItemNetAmount.Text = lnDiscountPrice.ToString("F");
                textBoxItemGrossAmount.Text = lnTotalPrice.ToString("F");

                if (textBoxSGSTAmt.Text.Length > 0 && mbSameState)
                    lnTotalTax = float.Parse(textBoxSGSTAmt.Text);

                if (textBoxCGSTAmt.Text.Length > 0 && mbSameState)
                    lnTotalTax += float.Parse(textBoxCGSTAmt.Text);

                if (textBoxIGSTAmt.Text.Length > 0 && !mbSameState)
                    lnTotalTax += float.Parse(textBoxIGSTAmt.Text);

                textBoxTax.Text = lnTotalTax.ToString("F");

                textBoxItemTotalAmount.Text = (float.Parse(textBoxItemNetAmount.Text) + lnTotalTax).ToString("F");
            }
        }

        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            dataGridViewInvoice.ReadOnly = true;
            dataGridViewInvoice.AllowUserToAddRows = false;
            dataGridViewInvoice.ColumnCount = 25;
            dataGridViewInvoice.Columns[0].Name = "Mode";
            dataGridViewInvoice.Columns[1].Name = "S.no";
            dataGridViewInvoice.Columns[2].Name = "ItemNo";
            dataGridViewInvoice.Columns[3].Name = "ItemDesc";
            dataGridViewInvoice.Columns[4].Name = "ItemPrice";
            dataGridViewInvoice.Columns[5].Name = "ItemUOM";
            dataGridViewInvoice.Columns[6].Name = "Qty";
            dataGridViewInvoice.Columns[7].Name = "GrossProfit";
            dataGridViewInvoice.Columns[8].Name = "Discount";
            dataGridViewInvoice.Columns[9].Name = "CGST%";
            dataGridViewInvoice.Columns[10].Name = "CGST";
            dataGridViewInvoice.Columns[11].Name = "SGST%";
            dataGridViewInvoice.Columns[12].Name = "SGST";
            dataGridViewInvoice.Columns[13].Name = "IGST%";
            dataGridViewInvoice.Columns[14].Name = "IGST";
            dataGridViewInvoice.Columns[15].Name = "NetAmount";
            dataGridViewInvoice.Columns[16].Name = "Tax";
            dataGridViewInvoice.Columns[17].Name = "TotalAmount";
            dataGridViewInvoice.Columns[18].Name = "UPCCode";
            dataGridViewInvoice.Columns[19].Name = "HSNCode";
            dataGridViewInvoice.Columns[20].Name = "SACCode";
            dataGridViewInvoice.Columns[21].Name = "ItemType";
            dataGridViewInvoice.Columns[22].Name = "ItemCatg";
            dataGridViewInvoice.Columns[23].Name = "ItemSts";
            dataGridViewInvoice.Columns[24].Name = "InvoiceItemSNo";


            textBoxHSNCode.Visible = false;
            textBoxSACCode.Visible = false;
            textBoxUPCCode.Visible = false;
            textBoxItemType.Visible = false;
            textBoxItemCatg.Visible = false;
            textBoxItemSts.Visible = false;

            mnLineOpMode = LinemodeAdd;

            switch (mnMode)
            {
                case MasterMechUtil.OPMode.New:
                    buttonSearchMobile.Visible = false;
                    buttonSearchMobileNo.Visible = false;
                    buttonAdvanceSearchInvoice.Visible = false;
                    buttonSearchCustomer.Visible = false;
                    break;

                case MasterMechUtil.OPMode.Open:
                    buttonAdvanceSearchInvoice.Visible = false;
                    buttonSearchCustomer.Visible = false;
                    break;

                case MasterMechUtil.OPMode.Delete:
                    this.buttonInvoiceSave.Text = "Delete";
                    break;
            }

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

            if (textBoxItemNo.Text.Equals(""))
            {
                MessageBox.Show("Item not slected. Please search the item");
                return;
            }

            if (textBoxItemQty.Text.Equals(""))
            {
                MessageBox.Show("Enter the Quantity before adding the line");
                textBoxItemQty.Focus();
                return;
            }
            if (mnLineOpMode == LinemodeAdd)
            {
                if (LineAddAlReady())
                {
                    MessageBox.Show("Item" + textBoxItemDesc.Text + "already added. Please select the line and update");
                    return;
                }
                else
                {
                    dataGridViewInvoice.Rows.Add(MasterMechUtil.OPMode.New, dataGridViewInvoice.Rows.Count + 1, textBoxItemNo.Text, textBoxItemDesc.Text,
                     textBoxItemPrice.Text, textBoxItemUOM.Text, textBoxItemQty.Text, textBoxItemGrossAmount.Text, textBoxItemDiscount.Text, textBoxCGST.Text, textBoxCGSTAmt.Text,
                      textBoxSGST.Text, textBoxSGSTAmt.Text, textBoxIGST.Text, textBoxIGSTAmt.Text, textBoxItemNetAmount.Text, textBoxTax.Text, textBoxItemTotalAmount.Text, textBoxUPCCode.Text,
                         textBoxHSNCode.Text, textBoxSACCode.Text, textBoxItemType.Text, textBoxItemCatg.Text, textBoxItemSts.Text, null); ;

                    UpdateHeader();
                }
            }
            else if (mnLineOpMode == LineModeUpdate)
            {
                DataGridViewRow lobjRow;
                lobjRow = dataGridViewInvoice.Rows[(int)mnupdRow];
                 
               
                    lobjRow.Cells[0].Value = MasterMechUtil.OPMode.Open;
               // lobjRow.Cells[1].Value =
                lobjRow.Cells[2].Value = textBoxItemNo.Text;
                lobjRow.Cells[3].Value = textBoxItemDesc.Text;
                lobjRow.Cells[4].Value = textBoxItemPrice.Text;
                lobjRow.Cells[5].Value = textBoxItemUOM.Text;
                lobjRow.Cells[6].Value = textBoxItemQty.Text;
                lobjRow.Cells[7].Value = textBoxItemGrossAmount.Text;
                lobjRow.Cells[8].Value = textBoxItemDiscount.Text;
                lobjRow.Cells[9].Value = textBoxCGST.Text;
                lobjRow.Cells[10].Value = textBoxCGSTAmt.Text;

                lobjRow.Cells[11].Value = textBoxSGST.Text;
                lobjRow.Cells[12].Value = textBoxSGSTAmt.Text;
                lobjRow.Cells[13].Value = textBoxIGST.Text;
                lobjRow.Cells[14].Value = textBoxIGSTAmt.Text;
                lobjRow.Cells[15].Value = textBoxItemNetAmount.Text;
                lobjRow.Cells[16].Value = textBoxTax.Text;
                lobjRow.Cells[17].Value = textBoxItemTotalAmount.Text;

                lobjRow.Cells[18].Value = textBoxUPCCode.Text;
                lobjRow.Cells[19].Value = textBoxHSNCode.Text;
                lobjRow.Cells[20].Value = textBoxSACCode.Text;
                lobjRow.Cells[21].Value = textBoxItemType.Text;
                lobjRow.Cells[22].Value = textBoxItemCatg.Text;
                lobjRow.Cells[23].Value = textBoxItemSts.Text;

                UpdateHeader();
            }
        }

        private void textBoxItemDiscount_Validating(object sender, CancelEventArgs e)
        {
            if (((TextBox)sender).Text.Length > 0)
            {
                bool lbValidSGST = Regex.IsMatch(((TextBox)sender).Text, @"^\d{0,6}(?:\.\d{0,6})?$", RegexOptions.IgnoreCase);
                if (!lbValidSGST)
                {
                    ((TextBox)sender).ForeColor = Color.Red;
                    ((TextBox)sender).Font = new Font(((TextBox)sender).Font, FontStyle.Bold);
                    labelErrorDiscount.Visible = true;
                    e.Cancel = true;
                }
            }
        }

        private void textBoxItemDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxItemDiscount.Font.Bold)
            {
                textBoxItemDiscount.ForeColor = Color.Black;
                textBoxItemDiscount.Font = new Font(textBoxItemDiscount.Font, FontStyle.Regular);
                labelErrorDiscount.Visible = false;
            }
        }

        private void textBoxItemQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxItemQty.Font.Bold)
            {
                textBoxItemQty.ForeColor = Color.Black;
                textBoxItemQty.Font = new Font(textBoxItemQty.Font, FontStyle.Regular);
                labelerrorQty.Visible = false;
            }
        }

        private void dataGridViewInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int lnRowNo = e.RowIndex;

            // if the header row is clicked
            if (lnRowNo < 0)
                return;

            textBoxItemNo.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[2].Value.ToString();
            textBoxItemDesc.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[3].Value.ToString();
            textBoxItemPrice.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[4].Value.ToString();
            textBoxItemUOM.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[5].Value.ToString();
            textBoxItemQty.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[6].Value.ToString();
            textBoxItemGrossAmount.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[7].Value.ToString();
            textBoxItemDiscount.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[8].Value.ToString();
            textBoxCGST.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[9].Value.ToString();
            textBoxCGSTAmt.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[10].Value.ToString();
            textBoxSGST.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[11].Value.ToString();
            textBoxSGSTAmt.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[12].Value.ToString();
            textBoxIGST.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[13].Value.ToString();
            textBoxIGSTAmt.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[14].Value.ToString(); 
            textBoxItemNetAmount.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[15].Value.ToString();
            textBoxTax.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[16].Value.ToString();
            textBoxItemTotalAmount.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[17].Value.ToString();

            if(dataGridViewInvoice.Rows[lnRowNo].Cells[18].Value == null)
              textBoxUPCCode.Text = null;
            else
            textBoxUPCCode.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[18].Value.ToString();

            if (dataGridViewInvoice.Rows[lnRowNo].Cells[19].Value == null)
                textBoxHSNCode.Text = null;
            else
            textBoxHSNCode.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[19].Value.ToString();

            if (dataGridViewInvoice.Rows[lnRowNo].Cells[20].Value == null)
                textBoxSACCode.Text = null;
            else
            textBoxSACCode.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[20].Value.ToString();

            textBoxItemType.Text = dataGridViewInvoice.Rows[lnRowNo].Cells[21].Value.ToString();
            textBoxItemCatg.Text= dataGridViewInvoice.Rows[lnRowNo].Cells[22].Value.ToString();
            textBoxItemSts.Text= dataGridViewInvoice.Rows[lnRowNo].Cells[23].Value.ToString();

           
            buttonAdd.Text = "Updated";
            mnupdRow = lnRowNo;
            mnLineOpMode = LineModeUpdate;
            buttonCancel.Visible = true;
            buttonDeleted.Visible = true;
            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            textBoxItemNo.Clear();
            textBoxItemDesc.Clear();
            textBoxItemPrice.Clear();
            textBoxItemUOM.Clear();
            textBoxItemQty.Clear();
            textBoxItemGrossAmount.Clear();
            textBoxItemDiscount.Clear();
            textBoxCGST.Clear();
            textBoxCGSTAmt.Clear();
            textBoxSGST.Clear();
            textBoxSGSTAmt.Clear();
            textBoxIGST.Clear();
            textBoxIGSTAmt.Clear();
            textBoxItemNetAmount.Clear();
            textBoxTax.Clear();
            textBoxItemTotalAmount.Clear();
            textBoxUPCCode.Clear();
            textBoxHSNCode.Clear();
            textBoxSACCode.Clear();
            mSelectedRow = -1;
            mnupdRow = null;
            mnLineOpMode = LinemodeAdd;
            buttonCancel.Visible = false;
            buttonDeleted.Enabled = false;
            buttonAdd.Text = "ADD";

        }

        private bool LineAddAlReady()
        {
            for (int Incount = 0; Incount <= dataGridViewInvoice.Rows.Count - 1; Incount++)
            {
                if (dataGridViewInvoice.Rows[Incount].Cells[2].Value.ToString() == textBoxItemNo.Text)
                    return true;
            }   
            return false;
        }

        private void buttonInvoiceSave_Click(object sender, EventArgs e)
        {
            switch (mnMode)
            {
                case MasterMechUtil.OPMode.New:
                    if (!ValidInput())
                        return;
                    SaveCustomer();
                    break;

                case MasterMechUtil.OPMode.Open:
                    if (!ValidInput())
                        return;
                    SaveCustomer();
                    break;

                case MasterMechUtil.OPMode.Delete:

                    if (textBoxInvoiceNo.Text.Length > 0)
                    {
                        if (MessageBox.Show("Are you sure ?", "Deleting User" + textBoxInvoiceNo.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                            DeleteInvoice();
                    }

                    else
                    {
                        MessageBox.Show("First Select a User ID");
                        this.buttonInvoiceSave.Text = "Delete";
                    }
                    break;
            }
        }


        private bool ValidInput()
        {
            if (textBoxFName.Text.Length == 0)
            {
                labelErrorFirstN.Visible = true;
                return false;
            }

            if (textBoxLName.Text.Length == 0)
            {
                labelErrorLastN.Visible = true;
                return false;
            }

            if (textBoxStatus.Text.Length == 0)
            {
                labelErrorStatus.Visible = true;
                return false;
            }

            if (textBoxType.Text.Length == 0)
            {
                labelErrorType.Visible = true;
                return false;
            }

            if (textBoxState.Text.Length == 0)
            {
                labelErrorState.Visible = true;
                return false;
            }

            if(!ValidNumber(textBoxMilage.Text))
            {
                labelMilage.Visible = true;
                return false;
            }

            return true;
        }



        private void SaveCustomer()
        {
            Invoice lobjINCust = new Invoice(MasterMechUtil.ConnStr, MasterMechUtil.sUserID);
            lobjINCust.InvoiceCustomer.lsCustFName = textBoxFName.Text;
            lobjINCust.InvoiceCustomer.lsCustLName = textBoxLName.Text;
            lobjINCust.InvoiceCustomer.lsCustMobNo = textBoxCustMob.Text;
            lobjINCust.InvoiceCustomer.lsCustEmail = textBoxEmail.Text;
            lobjINCust.InvoiceCustomer.lsCustSts = textBoxStatus.Text;
            lobjINCust.InvoiceCustomer.CustType = textBoxType.Text;


            if (textBoxStreetAdd.Text.Length > 0)
                lobjINCust.InvoiceCustomer.lsCustStAddr = textBoxStreetAdd.Text;
            else
                lobjINCust.InvoiceCustomer.lsCustStAddr = null;

            if (textBoxAreaAdd.Text.Length > 0)
                lobjINCust.InvoiceCustomer.lsCustArAddr = textBoxAreaAdd.Text;
            else
                lobjINCust.InvoiceCustomer.lsCustArAddr = null;

            if (textBoxPinCode.Text.Length > 0)
                lobjINCust.InvoiceCustomer.CustPinCode = textBoxPinCode.Text;
            else
                lobjINCust.InvoiceCustomer.CustPinCode = null;

            if (textBoxCity.Text.Length > 0)
                lobjINCust.InvoiceCustomer.lsCustCity = textBoxCity.Text;
            else
                lobjINCust.InvoiceCustomer.lsCustCity = null;

            if (textBoxState.Text.Length > 0)
                lobjINCust.InvoiceCustomer.lsCustState = textBoxState.Text;
            else
                lobjINCust.InvoiceCustomer.lsCustState = null;

             if (textBoxCountry.Text.Length > 0)
                lobjINCust.InvoiceCustomer.lsCustCountry = textBoxCountry.Text;
            else
                lobjINCust.InvoiceCustomer.lsCustCountry = null;

            if (textBoxGSTNo.Text.Length > 0)
                lobjINCust.InvoiceCustomer.lsCustGSTNo = textBoxGSTNo.Text;
            else
                lobjINCust.InvoiceCustomer.lsCustGSTNo = null;

            if (textBoxRemarks.Text.Length > 0)
                lobjINCust.InvoiceCustomer.lsCustRemarks = textBoxRemarks.Text;
            else
                lobjINCust.InvoiceCustomer.lsCustRemarks = null;

            lobjINCust.InvoiceCustomer.lsCreatedBy = textBoxInCreatedBy.Text;

            if (textBoxCustNo.Text.Length > 0)
                lobjINCust.InvoiceCustomer.lnCustNo = int.Parse(textBoxCustNo.Text);
            else
                lobjINCust.InvoiceCustomer.lnCustNo = 0;


            if(textBoxRegNo.Text.Length > 0)
                lobjINCust.VehicleRegNo = textBoxRegNo.Text;
            else
                lobjINCust.VehicleRegNo = null;


            if (textBoxModel.Text.Length > 0)
                lobjINCust.VehicleModel= textBoxModel.Text;
            else
                lobjINCust.VehicleModel = null;


            if (textBoxChassisNo.Text.Length > 0)
                lobjINCust.ChassisNo = textBoxChassisNo.Text;
            else
                lobjINCust.ChassisNo = null;

            if (textBoxEngineNo.Text.Length > 0)
                lobjINCust.EngineNo = textBoxEngineNo.Text;
            else
                lobjINCust.EngineNo = null;


            if (textBoxMilage.Text.Length > 0)
                lobjINCust.Mileage = int.Parse (textBoxMilage.Text);
            else
                lobjINCust.Mileage = null;


            if (textBoxServiceType.Text.Length > 0)
                lobjINCust.ServiceType = textBoxServiceType.Text;
            else
                lobjINCust.ServiceType = null;


            if (textBoxService.Text.Length > 0)
                lobjINCust.ServiceAssoName = textBoxService.Text;
            else
                lobjINCust.ServiceAssoName = null;


            if (textBoxServiceAssociateMob.Text.Length > 0)
                lobjINCust.ServiceAssoMobNo = textBoxServiceAssociateMob.Text;
            else
                lobjINCust.ServiceAssoMobNo = null;


            if (textBoxPartsTotal.Text.Length > 0)
                lobjINCust.PartsTotal = double.Parse(textBoxPartsTotal.Text);
            else
                lobjINCust.PartsTotal = null;

            if (textBoxPartsSGST.Text.Length > 0)
                lobjINCust.PartsSGSTTotal = double.Parse(textBoxPartsSGST.Text);
            else
                lobjINCust.PartsSGSTTotal = null;



            if (textBoxPartsIGST.Text.Length > 0)
                lobjINCust.PartsIGSTTotal = double.Parse(textBoxPartsIGST.Text);
            else
                lobjINCust.PartsIGSTTotal = null;

            if (textBoxPartsCGST.Text.Length > 0)
                lobjINCust.PartsCGSTTotal = double.Parse(textBoxPartsCGST.Text);
            else
                lobjINCust.PartsCGSTTotal = null;

            if (textBoxLabourTotal.Text.Length > 0)
                lobjINCust.LabourTotal = double.Parse(textBoxLabourTotal.Text);
            else
                lobjINCust.LabourTotal = null;


            if (textBoxLabourSGST.Text.Length > 0)
                lobjINCust.LabourSGSTTotal = double.Parse(textBoxLabourSGST.Text);
            else
                lobjINCust.LabourSGSTTotal = null;

            if (textBoxLabourIGST.Text.Length > 0)
                lobjINCust.LabourIGSTTotal = double.Parse(textBoxLabourIGST.Text);
            else
                lobjINCust.LabourIGSTTotal = null;


            if (textBoxLabourCGST.Text.Length > 0)
                lobjINCust.LabourCGSTTotal = double.Parse(textBoxLabourCGST.Text);
            else
                lobjINCust.LabourCGSTTotal = null;


            if (textBoxItemTotalAmount.Text.Length > 0)
                lobjINCust.TotalAmount = double.Parse(textBoxItemTotalAmount.Text);
            else
                lobjINCust.TotalAmount = null;

            if (textBoxTcGST.Text.Length > 0)
                lobjINCust.TotalCGST = double.Parse(textBoxTcGST.Text);
            else
                lobjINCust.TotalCGST = null;

            if (textBoxTsGST.Text.Length > 0)
                lobjINCust.TotalSGST = double.Parse(textBoxTsGST.Text);
            else
                lobjINCust.TotalSGST = null;

            if (textBoxTiGST.Text.Length > 0)
                lobjINCust.TotalIGST = double.Parse(textBoxTiGST.Text);
            else
                lobjINCust.TotalIGST = null;

            if (textBoxTDiscount.Text.Length > 0)
                lobjINCust.DiscountAmount = double.Parse(textBoxTDiscount.Text);
            else
                lobjINCust.DiscountAmount = null;

            if (textBoxTTax.Text.Length > 0)
                lobjINCust.TotalTax = double.Parse(textBoxTTax.Text);
            else
                lobjINCust.TotalTax = null;

            if (textBoxGrandTotal.Text.Length > 0)
                lobjINCust.GrandTotal = double.Parse(textBoxGrandTotal.Text);
            else
                lobjINCust.GrandTotal = null;

            if (textBoxInvoiceNo.Text.Length > 0)
                lobjINCust.InvoiceSNo = int.Parse(textBoxInvoiceNo.Text);
            else
                lobjINCust.InvoiceSNo = null;


            for (int lnCnt = 0; lnCnt < dataGridViewInvoice.Rows.Count; lnCnt++)
            {
                InvoiceItem lobjInvoiceItem = new InvoiceItem(MasterMechUtil.ConnStr, MasterMechUtil.sUserID);
                if (dataGridViewInvoice.Rows[lnCnt].Cells[24].Value != null)
                    lobjInvoiceItem.InvoiceItemSNo = int.Parse(dataGridViewInvoice.Rows[lnCnt].Cells[24].Value.ToString());
                else
                    lobjInvoiceItem.InvoiceItemSNo = null;

                lobjInvoiceItem.ItemNo = int.Parse(dataGridViewInvoice.Rows[lnCnt].Cells[2].Value.ToString());
                lobjInvoiceItem.ItemDesc = dataGridViewInvoice.Rows[lnCnt].Cells[3].Value.ToString();
                lobjInvoiceItem.ItemType = dataGridViewInvoice.Rows[lnCnt].Cells[21].Value.ToString();
                lobjInvoiceItem.ItemCatg = dataGridViewInvoice.Rows[lnCnt].Cells[22].Value.ToString();
                lobjInvoiceItem.ItemPrice = float.Parse(dataGridViewInvoice.Rows[lnCnt].Cells[4].Value.ToString());
                lobjInvoiceItem.ItemUOM = dataGridViewInvoice.Rows[lnCnt].Cells[5].Value.ToString();
                lobjInvoiceItem.ItemSts = dataGridViewInvoice.Rows[lnCnt].Cells[23].Value.ToString();
                lobjInvoiceItem.Qty = float.Parse(dataGridViewInvoice.Rows[lnCnt].Cells[6].Value.ToString());

                if (dataGridViewInvoice.Rows[lnCnt].Cells[18].Value.ToString().Length > 0)
                    lobjInvoiceItem.UPCCode = dataGridViewInvoice.Rows[lnCnt].Cells[18].Value.ToString();
                else
                    lobjInvoiceItem.UPCCode = null;
                if (dataGridViewInvoice.Rows[lnCnt].Cells[20].Value.ToString().Length > 0)
                    lobjInvoiceItem.SACCode = dataGridViewInvoice.Rows[lnCnt].Cells[20].Value.ToString();
                else
                    lobjInvoiceItem.SACCode = null;
                if (dataGridViewInvoice.Rows[lnCnt].Cells[19].Value.ToString().Length > 0)
                    lobjInvoiceItem.HSNCode = dataGridViewInvoice.Rows[lnCnt].Cells[19].Value.ToString();
                else
                    lobjInvoiceItem.HSNCode = null;

                if (dataGridViewInvoice.Rows[lnCnt].Cells[17].Value.ToString().Length > 0)
                    lobjInvoiceItem.TotalAmount = float.Parse(dataGridViewInvoice.Rows[lnCnt].Cells[17].Value.ToString());
                else
                    lobjInvoiceItem.TotalAmount = 0;

                if (dataGridViewInvoice.Rows[lnCnt].Cells[8].Value.ToString().Length > 0)
                    lobjInvoiceItem.DiscountAmount = float.Parse(dataGridViewInvoice.Rows[lnCnt].Cells[8].Value.ToString());
                else
                    lobjInvoiceItem.DiscountAmount = 0;

                if (dataGridViewInvoice.Rows[lnCnt].Cells[14].Value.ToString().Length > 0)
                    lobjInvoiceItem.IGSTAmount = float.Parse(dataGridViewInvoice.Rows[lnCnt].Cells[14].Value.ToString());
                else
                    lobjInvoiceItem.IGSTAmount = 0;

                if (dataGridViewInvoice.Rows[lnCnt].Cells[10].Value.ToString().Length > 0)
                    lobjInvoiceItem.CGSTAmount = float.Parse(dataGridViewInvoice.Rows[lnCnt].Cells[10].Value.ToString());
                else
                    lobjInvoiceItem.CGSTAmount = 0;

                if (dataGridViewInvoice.Rows[lnCnt].Cells[12].Value.ToString().Length > 0)
                    lobjInvoiceItem.SGSTAmount = float.Parse(dataGridViewInvoice.Rows[lnCnt].Cells[12].Value.ToString());
                else
                    lobjInvoiceItem.SGSTAmount = 0;

                if (dataGridViewInvoice.Rows[lnCnt].Cells[9].Value.ToString().Length > 0)
                    lobjInvoiceItem.CGSTRate = float.Parse(dataGridViewInvoice.Rows[lnCnt].Cells[9].Value.ToString());
                else
                    lobjInvoiceItem.CGSTRate = 0;

                if (dataGridViewInvoice.Rows[lnCnt].Cells[11].Value.ToString().Length > 0)
                    lobjInvoiceItem.SGSTRate = float.Parse(dataGridViewInvoice.Rows[lnCnt].Cells[11].Value.ToString());
                else
                    lobjInvoiceItem.SGSTRate = 0;

                if (dataGridViewInvoice.Rows[lnCnt].Cells[13].Value.ToString().Length > 0)
                    lobjInvoiceItem.IGSTRate = float.Parse(dataGridViewInvoice.Rows[lnCnt].Cells[13].Value.ToString());
                else
                    lobjInvoiceItem.IGSTRate = 0;
                lobjINCust.InvoiceItem.Add(lobjInvoiceItem);
            }


            if (!lobjINCust.Save())
                
                MessageBox.Show("Test Data could not be saved");
        }

         
        private void UpdateHeader()
        {
            textBoxPartsTotal.Text = textBoxPartsSGST.Text = textBoxPartsIGST.Text = textBoxPartsCGST.Text = "0.00";
            textBoxLabourTotal.Text = textBoxLabourSGST.Text = textBoxLabourIGST.Text = textBoxLabourCGST.Text = "0.00";
            textBoxTDiscount.Text = textBoxGrandTotal.Text = textBoxTTax.Text = "0.00";


            for(int lnCount = 0; lnCount < dataGridViewInvoice.RowCount; lnCount++)
            {
                // ignore for deleted item
                if ((MasterMechUtil.OPMode)dataGridViewInvoice.Rows[lnCount].Cells[0].Value == MasterMechUtil.OPMode.Delete)
                continue;

                if(dataGridViewInvoice.Rows[lnCount].Cells[21].Value.ToString() == "PARTS")
                {
                    if (textBoxPartsTotal.Text != "" && dataGridViewInvoice.Rows[lnCount].Cells[15].Value.ToString() != "")
                        textBoxPartsTotal.Text = (float.Parse(textBoxPartsTotal.Text) + float.Parse(dataGridViewInvoice.Rows[lnCount].Cells[15].Value.ToString())).ToString();


                    if (textBoxPartsSGST.Text != "" && dataGridViewInvoice.Rows[lnCount].Cells[12].Value.ToString() != "")
                        textBoxPartsSGST.Text = (float.Parse(textBoxPartsSGST.Text) + float.Parse(dataGridViewInvoice.Rows[lnCount].Cells[12].Value.ToString())).ToString();


                    if (textBoxPartsCGST.Text != "" && dataGridViewInvoice.Rows[lnCount].Cells[10].Value.ToString() != "")
                        textBoxPartsCGST.Text = (float.Parse(textBoxPartsCGST.Text) + float.Parse(dataGridViewInvoice.Rows[lnCount].Cells[10].Value.ToString())).ToString();


                    if (textBoxPartsIGST.Text != "" && dataGridViewInvoice.Rows[lnCount].Cells[14].Value.ToString() != "")
                        textBoxPartsIGST.Text = (float.Parse(textBoxPartsIGST.Text) + float.Parse(dataGridViewInvoice.Rows[lnCount].Cells[14].Value.ToString())).ToString();
                }

                else if(dataGridViewInvoice.Rows[lnCount].Cells[21].Value.ToString() == "LABOUR")
                {
                    if (textBoxLabourTotal.Text != "" && dataGridViewInvoice.Rows[lnCount].Cells[15].Value.ToString() != "")
                        textBoxLabourTotal.Text = (float.Parse(textBoxLabourTotal.Text) + float.Parse(dataGridViewInvoice.Rows[lnCount].Cells[15].Value.ToString())).ToString();


                    if (textBoxLabourSGST.Text != "" && dataGridViewInvoice.Rows[lnCount].Cells[12].Value.ToString() != "")
                        textBoxLabourSGST.Text = (float.Parse(textBoxLabourSGST.Text) + float.Parse(dataGridViewInvoice.Rows[lnCount].Cells[12].Value.ToString())).ToString();


                    if (textBoxLabourCGST.Text != "" && dataGridViewInvoice.Rows[lnCount].Cells[10].Value.ToString() != "")
                        textBoxLabourCGST.Text = (float.Parse(textBoxLabourCGST.Text) + float.Parse(dataGridViewInvoice.Rows[lnCount].Cells[10].Value.ToString())).ToString();


                    if (textBoxLabourIGST.Text != "" && dataGridViewInvoice.Rows[lnCount].Cells[14].Value.ToString() != "")
                        textBoxLabourIGST.Text = (float.Parse(textBoxLabourIGST.Text) + float.Parse(dataGridViewInvoice.Rows[lnCount].Cells[14].Value.ToString())).ToString();
                }
                //Item DisCount
                if (textBoxTDiscount.Text != "" && dataGridViewInvoice.Rows[lnCount].Cells[8].Value.ToString() != "")
                    textBoxTDiscount.Text = (float.Parse(textBoxTDiscount.Text) + float.Parse(dataGridViewInvoice.Rows[lnCount].Cells[8].Value.ToString())).ToString();
            }

            textBoxTNA.Text = (float.Parse(textBoxPartsTotal.Text) + float.Parse(textBoxLabourTotal.Text)).ToString();
            textBoxTsGST.Text = (float.Parse(textBoxPartsSGST.Text) + float.Parse(textBoxLabourSGST.Text)).ToString();
            textBoxTcGST.Text = (float.Parse(textBoxPartsCGST.Text) + float.Parse(textBoxLabourCGST.Text)).ToString();
            textBoxTiGST.Text = (float.Parse(textBoxPartsIGST.Text) + float.Parse(textBoxLabourIGST.Text)).ToString();
            textBoxTTax.Text = (float.Parse(textBoxTsGST.Text) + float.Parse(textBoxTcGST.Text) + float.Parse(textBoxTiGST.Text)).ToString();
            textBoxGrandTotal.Text = (float.Parse(textBoxTNA.Text) - float.Parse(textBoxTDiscount.Text)).ToString();
        }

        private void buttonSearchMobileNo_Click(object sender, EventArgs e)
        {
            if (textBoxMobileNo.Text.Length == 0)
            {
                MessageBox.Show("Please enter the User Name  to search.");
                this.textBoxMobileNo.Focus();
                return;
            }

            Invoice lObjInvoice = new Invoice();
            InvoiceItem lobjInvoiceItem = new InvoiceItem();
            List<Invoice> lObjInvoices = new List<Invoice>();
            List<InvoiceItem> lobjInvoiceItems = new List<InvoiceItem>();

            lObjInvoice.SearchInvoice(MasterMechUtil.ConnStr, textBoxMobileNo.Text, lObjInvoices);
            
            if (lObjInvoices.Count == 0)
            {
                MessageBox.Show("No Test found!");
                this.textBoxMobileNo.Focus();
                return;
            }
            else if (lObjInvoices.Count == 1)
            {
                LoadInvoice(lObjInvoices[0].InvoiceSNo);
                return;
            }


            ItemSearchResultForm lObjSearchCustF = new ItemSearchResultForm();
            lObjSearchCustF.NoSelectMsg = "No Customer selected. Select a customer row and then click select";


            // Build the header
            lObjSearchCustF.ItemSearchdataGridView.ReadOnly = true;
            lObjSearchCustF.ItemSearchdataGridView.AllowUserToAddRows = false;
            lObjSearchCustF.ItemSearchdataGridView.ColumnCount = 11;
            lObjSearchCustF.ItemSearchdataGridView.Columns[0].Name = "S.No";
            lObjSearchCustF.ItemSearchdataGridView.Columns[1].Name = "InvoiceSNo";
            lObjSearchCustF.ItemSearchdataGridView.Columns[2].Name = "InvoiceNo";
            lObjSearchCustF.ItemSearchdataGridView.Columns[3].Name = "InvoiceStu";
            lObjSearchCustF.ItemSearchdataGridView.Columns[4].Name = "CustNo";
            lObjSearchCustF.ItemSearchdataGridView.Columns[5].Name = "CustFName";
            lObjSearchCustF.ItemSearchdataGridView.Columns[6].Name = "CustLName";
            lObjSearchCustF.ItemSearchdataGridView.Columns[7].Name = "CustMobNo";
            lObjSearchCustF.ItemSearchdataGridView.Columns[8].Name = "CustEmail";
            lObjSearchCustF.ItemSearchdataGridView.Columns[9].Name = "CustSts";
            lObjSearchCustF.ItemSearchdataGridView.Columns[10].Name = "CustType";
            


            // Load the data in the grid
            int lnCnt = 1;
            foreach (Invoice lObjSearchInvoice in lObjInvoices)
                lObjSearchCustF.ItemSearchdataGridView.Rows.Add(lnCnt++, lObjSearchInvoice.InvoiceSNo, lObjSearchInvoice.InvoiceNo, lObjSearchInvoice.InvoiceSts,
                lObjSearchInvoice.InvoiceCustomer.lnCustNo, lObjSearchInvoice.InvoiceCustomer.lsCustFName, lObjSearchInvoice.InvoiceCustomer.lsCustLName,
                lObjSearchInvoice.InvoiceCustomer.lsCustMobNo, lObjSearchInvoice.InvoiceCustomer.lsCustEmail, lObjSearchInvoice.InvoiceCustomer.lsCustSts,
                lObjSearchInvoice.InvoiceCustomer.CustType);
              lObjSearchCustF.ShowDialog();


            int lnSelectedRow = lObjSearchCustF.mSelectedRow;
            int lnInvoiceNo = (int)lObjSearchCustF.ItemSearchdataGridView.Rows[lnSelectedRow].Cells[1].Value;

            lobjInvoiceItem.SearchInvoiceItem(MasterMechUtil.ConnStr, lnInvoiceNo, lobjInvoiceItems);
            int lnCntitem = 1;
            foreach (InvoiceItem lobjInvoiceitem in lobjInvoiceItems)
                dataGridViewInvoice.Rows.Add(MasterMechUtil.OPMode.Open, lnCntitem++, lobjInvoiceitem.ItemNo, lobjInvoiceitem.ItemDesc, lobjInvoiceitem.ItemPrice, lobjInvoiceitem.ItemUOM,
                    lobjInvoiceitem.Qty,lobjInvoiceitem.ItemPrice+lobjInvoiceitem.Qty,lobjInvoiceitem.DiscountAmount,lobjInvoiceitem.CGSTRate,lobjInvoiceitem.CGSTAmount,
                    lobjInvoiceitem.SGSTRate,lobjInvoiceitem.SGSTAmount,lobjInvoiceitem.IGSTRate,lobjInvoiceitem.IGSTAmount,(lobjInvoiceitem.ItemPrice + lobjInvoiceitem.Qty)-lobjInvoiceitem.DiscountAmount,
                   lobjInvoiceitem.SGSTAmount+lobjInvoiceitem.CGSTAmount+lobjInvoiceitem.IGSTAmount,lobjInvoiceitem.TotalAmount,lobjInvoiceitem.UPCCode,lobjInvoiceitem.HSNCode,lobjInvoiceitem.SACCode,lobjInvoiceitem.ItemType,lobjInvoiceitem.ItemCatg,lobjInvoiceitem.ItemSts,null);//, float.Parse(textBoxItemPrice.Text) * float.Parse(textBoxItemQty.Text));

            if (lObjSearchCustF.mbSelected)
                LoadInvoice(lnInvoiceNo);
            else
                this.textBoxCustMob.Focus();

        }

         public void LoadInvoice(int? lnInvoiceNo)
        {
            Invoice lobjInvoice = new Invoice();
            //UserDtl lObjTest = new UserDtl(Utility.msConnStr, Utility.msUserID);
            if (lobjInvoice.Load(MasterMechUtil.ConnStr, lnInvoiceNo))
            {
                textBoxInvoiceNo.Text = lnInvoiceNo.ToString();
                //textBoxInvoiceNo.Text = lobjInvoice.InvoiceNo;
                textBoxInvoiceDate.Text = lobjInvoice.InvoiceDate.ToString();
                textBoxInvoiceRemarks.Text = lobjInvoice.InvoiceRemarks;
                textBoxCustNo.Text = lobjInvoice.InvoiceCustomer.lnCustNo.ToString();
                textBoxFName.Text = lobjInvoice.InvoiceCustomer.lsCustFName;
                textBoxLName.Text = lobjInvoice.InvoiceCustomer.lsCustLName;
                textBoxCustMob.Text = lobjInvoice.InvoiceCustomer.lsCustMobNo;
                textBoxEmail.Text = lobjInvoice.InvoiceCustomer.lsCustEmail;
                textBoxStatus.Text = lobjInvoice.InvoiceCustomer.lsCustSts;
                textBoxType.Text = lobjInvoice.InvoiceCustomer.CustType.ToString();
                textBoxStreetAdd.Text = lobjInvoice.InvoiceCustomer.lsCustStAddr;
                textBoxAreaAdd.Text = lobjInvoice.InvoiceCustomer.lsCustArAddr;
                textBoxCity.Text = lobjInvoice.InvoiceCustomer.lsCustCity;
                textBoxState.Text = lobjInvoice.InvoiceCustomer.lsCustState;
                textBoxPinCode.Text = lobjInvoice.InvoiceCustomer.CustPinCode.ToString();
                textBoxCountry.Text = lobjInvoice.InvoiceCustomer.lsCustCountry;
                textBoxGSTNo.Text = lobjInvoice.InvoiceCustomer.lsCustGSTNo;
                textBoxRemarks.Text = lobjInvoice.InvoiceCustomer.lsCustRemarks;
                textBoxLastVisit.Text = lobjInvoice.InvoiceCustomer.dCustlLastVisit.ToString();
                textBoxInCreated.Text = lobjInvoice.Created.ToString();
                textBoxInCreatedBy.Text = lobjInvoice.CreatedBy.ToString();
                textBoxPartsTotal.Text = lobjInvoice.PartsTotal.ToString();
                textBoxPartsSGST.Text = lobjInvoice.PartsSGSTTotal.ToString();
                textBoxPartsIGST.Text = lobjInvoice.PartsIGSTTotal.ToString();
                textBoxPartsCGST.Text = lobjInvoice.PartsCGSTTotal.ToString();
                textBoxLabourTotal.Text = lobjInvoice.LabourTotal.ToString();
                textBoxLabourSGST.Text = lobjInvoice.LabourSGSTTotal.ToString();
                textBoxLabourIGST.Text = lobjInvoice.LabourIGSTTotal.ToString();
                textBoxLabourCGST.Text = lobjInvoice.LabourCGSTTotal.ToString();
                textBoxTNA.Text = lobjInvoice.TotalAmount.ToString();
                textBoxTiGST.Text = lobjInvoice.TotalIGST.ToString();
                textBoxTsGST.Text = lobjInvoice.TotalSGST.ToString();
                textBoxTcGST.Text = lobjInvoice.TotalCGST.ToString();
                textBoxTDiscount.Text = lobjInvoice.DiscountAmount.ToString();
                textBoxTTax.Text = lobjInvoice.TotalTax.ToString();
                textBoxGrandTotal.Text = lobjInvoice.GrandTotal.ToString();
                textBoxRegNo.Text = lobjInvoice.VehicleRegNo;
                textBoxModel.Text = lobjInvoice.VehicleModel;
                textBoxChassisNo.Text = lobjInvoice.ChassisNo;
                textBoxEngineNo.Text = lobjInvoice.EngineNo;
                textBoxMilage.Text = lobjInvoice.Mileage.ToString();
                textBoxServiceType.Text = lobjInvoice.ServiceType;
                textBoxService.Text = lobjInvoice.ServiceAssoName;
                textBoxServiceAssociateMob.Text = lobjInvoice.ServiceAssoMobNo;

                /*.Text = lObjItUde.dCreated.ToString();
                labelCreatedBy.Text = lObjItUde.lsCreatedBy;
                labelMmodified.Text = lObjItUde.dModified.ToString();
                labelModifiedVisit.Text = lObjItUde.lsModifiedBy;*/
            }
        }

        private void buttonDeleted_Click(object sender, EventArgs e)
        {
            if(dataGridViewInvoice.SelectedRows.Count == 0)
            {
                MessageBox.Show("please select the grid");
            }
            else
            {
                dataGridViewInvoice.Rows.RemoveAt(dataGridViewInvoice.SelectedRows[0].Index);
            }
        }

        private void textBoxFName_Enter(object sender, EventArgs e)
        {
            labelErrorFirstN.Visible = false;
        }

        private void textBoxLName_Enter(object sender, EventArgs e)
        {
            labelErrorLastN.Visible = false;
        }

        private void textBoxState_Enter(object sender, EventArgs e)
        {
            labelErrorState.Visible = false;
        }

        private void textBoxType_Enter(object sender, EventArgs e)
        {
            labelErrorType.Visible = false;
        }

        private void textBoxStatus_Enter(object sender, EventArgs e)
        {
            labelErrorStatus.Visible = false;
        }

        private void textBoxCustMob_Validating(object sender, CancelEventArgs e)
        {
            if (this.textBoxCustMob.Text.Length > 0)
            {
                bool lbValidMobNO = Regex.IsMatch(this.textBoxCustMob.Text, @"^(\d{10})$", RegexOptions.IgnoreCase);
                if (!lbValidMobNO)
                {
                    textBoxCustMob.ForeColor = Color.Red;
                    textBoxCustMob.Font = new Font(textBoxCustMob.Font, FontStyle.Bold);
                    labelErrorMobile.Visible = true;
                    e.Cancel = true;
                }
            }
        }

        private void textBoxCustMob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxCustMob.Font.Bold)
            {
                textBoxCustMob.ForeColor = Color.Black;
                textBoxCustMob.Font = new Font(textBoxCustMob.Font, FontStyle.Regular);
                labelErrorMobile.Visible = false;
            }
        }

        private void textBoxEmail_Validating(object sender, CancelEventArgs e)
        {
            if (this.textBoxEmail.Text.Length > 0)
            {
                bool lbValidEmail = Regex.IsMatch(this.textBoxEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (!lbValidEmail)
                {
                    textBoxEmail.ForeColor = Color.Red;
                    textBoxEmail.Font = new Font(textBoxEmail.Font, FontStyle.Bold);
                    labelErrorEmailID.Visible = true;
                    e.Cancel = true;
                }
            }
        }

        private void textBoxEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxEmail.Font.Bold)
            {
                textBoxEmail.ForeColor = Color.Black;
                textBoxEmail.Font = new Font(textBoxEmail.Font, FontStyle.Regular);
                labelErrorEmailID.Visible = false;
            }
        }

        private void textBoxServiceAssociateMob_Validating(object sender, CancelEventArgs e)
        {
            if (this.textBoxServiceAssociateMob.Text.Length > 0)
            {
                bool lbValidMobNO = Regex.IsMatch(this.textBoxServiceAssociateMob.Text, @"^(\d{10})$", RegexOptions.IgnoreCase);
                if (!lbValidMobNO)
                {
                    textBoxServiceAssociateMob.ForeColor = Color.Red;
                    textBoxServiceAssociateMob.Font = new Font(textBoxServiceAssociateMob.Font, FontStyle.Bold);
                    labelErrorServiceMob.Visible = true;
                    e.Cancel = true;
                }
            }
        }

        private void textBoxServiceAssociateMob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxServiceAssociateMob.Font.Bold)
            {
                textBoxServiceAssociateMob.ForeColor = Color.Black;
                textBoxServiceAssociateMob.Font = new Font(textBoxServiceAssociateMob.Font, FontStyle.Regular);
                labelErrorServiceMob.Visible = false;
            }
        }

        private void textBoxTDiscount_Validating(object sender, CancelEventArgs e)
        {
            if (((TextBox)sender).Text.Length > 0)
            {
                bool lbValidSGST = Regex.IsMatch(((TextBox)sender).Text, @"^\d{0,6}(?:\.\d{0,6})?$", RegexOptions.IgnoreCase);
                if (!lbValidSGST)
                {
                    ((TextBox)sender).ForeColor = Color.Red;
                    ((TextBox)sender).Font = new Font(((TextBox)sender).Font, FontStyle.Bold);
                    labelErrorTotalDiscount.Visible = true;
                    e.Cancel = true;
                }
            }
        }

        private void textBoxTDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxTDiscount.Font.Bold)
            {
                textBoxTDiscount.ForeColor = Color.Black;
                textBoxTDiscount.Font = new Font(textBoxTDiscount.Font, FontStyle.Regular);
                labelErrorTotalDiscount.Visible = false;
            }
        }



        private void DeleteInvoice()
        {
            Invoice lobjDeleteCust = new Invoice();
            lobjDeleteCust.InvoiceSNo = int.Parse(textBoxInvoiceNo.Text); //int.Parse(textBoxCustNo.Text);
            lobjDeleteCust.Delete(MasterMechUtil.ConnStr, lobjDeleteCust.InvoiceSNo);

        }


        public static bool ValidNumber(string isNumber)
        {


            for (int lnCnt = 0; lnCnt < isNumber.Length; lnCnt++)
            {
                if (((byte)isNumber[lnCnt]) < 48 || ((byte)isNumber[lnCnt]) > 57)
                {
                    return false;
                }

            }
            return true;
        }

        private void textBoxMilage_Enter(object sender, EventArgs e)
        {
            labelMilage.Visible = false;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("ITEM DETAILS", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 30));
            e.Graphics.DrawString("_______________________________________________________________________", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 60));
            e.Graphics.DrawString("Item No: " + textBoxItemNo.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 90));
            e.Graphics.DrawString("Description: " + textBoxItemDesc.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 120));
            e.Graphics.DrawString("Price: " + textBoxItemPrice.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 150));
            e.Graphics.DrawString("UOM: " + textBoxItemUOM.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 180));
            e.Graphics.DrawString("Quentity: " + textBoxItemQty.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 210));
            e.Graphics.DrawString("Gross Amount: " + textBoxItemGrossAmount.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 240));
            e.Graphics.DrawString("Discount: " + textBoxItemDiscount.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 270));
            e.Graphics.DrawString("SGST%: " + textBoxSGST.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 300));
            e.Graphics.DrawString("SGST Amount: " + textBoxSGSTAmt.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 330));
            e.Graphics.DrawString("CGST%: " + textBoxCGST.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 360));
            e.Graphics.DrawString("CGST Amount: " + textBoxCGSTAmt.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 90));
            e.Graphics.DrawString("IGST%: " + textBoxIGST.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 120));
            e.Graphics.DrawString("IGST Amount: " + textBoxIGSTAmt.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 150));
            e.Graphics.DrawString("Item Type: " + textBoxItemType.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 180));
            e.Graphics.DrawString("Item Category: " + textBoxItemCatg.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 210));
            e.Graphics.DrawString("Item Status: " + textBoxItemSts.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 240));
            e.Graphics.DrawString("Tax: " + textBoxTax.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 270));
            e.Graphics.DrawString("Total Amount: " + textBoxItemTotalAmount.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 300));
            e.Graphics.DrawString("Net Amount: " + textBoxItemNetAmount.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 330));
            e.Graphics.DrawString("_______________________________________________________________________", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(10, 390));

            e.Graphics.DrawString("CUSTOMER DETAILS", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 420));
            e.Graphics.DrawString("_______________________________________________________________________", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(10, 450));
            e.Graphics.DrawString("First Name: " + textBoxFName.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 480));
            e.Graphics.DrawString("Last Name: " + textBoxLName.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 510));
            e.Graphics.DrawString("Mobile Number: " + textBoxCustMob.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 540));
            e.Graphics.DrawString("Email Id: " + textBoxEmail.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 570));
            e.Graphics.DrawString("Pin Code: " + textBoxPinCode.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 600));
            e.Graphics.DrawString("City: " + textBoxCity.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 630));
            e.Graphics.DrawString("State : " + textBoxState.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 480));
            e.Graphics.DrawString("Country: " + textBoxCountry.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 510));
            e.Graphics.DrawString("Type: " + textBoxType.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 540));
            e.Graphics.DrawString("Status: " + textBoxStatus.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 570));
            e.Graphics.DrawString("GST No: " + textBoxGSTNo.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 600));
            e.Graphics.DrawString("_______________________________________________________________________", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(10, 660));


            e.Graphics.DrawString("Invoice No: " + textBoxInvoiceNo.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 700));
            e.Graphics.DrawString("Customer No: " + textBoxCustNo.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 700));
            e.Graphics.DrawString("_______________________________________________________________________", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(10, 730));

            e.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 760));
            e.Graphics.DrawString("Time: " + DateTime.Now.ToLongTimeString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(450, 760));
        }

        private void buttonInvoicePrint_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void PrintPreviewbutton_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void buttonInvoiceClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
