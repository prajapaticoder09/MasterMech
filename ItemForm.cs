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
    public partial class ItemForm : Form
    {
        private MasterMechUtil.OPMode mnMode;
        bool ibNewMode = false;
        public ItemForm()
        {
            InitializeComponent();
        }


        public ItemForm(MasterMechUtil.OPMode inMode)
        {
            mnMode = (MasterMechUtil.OPMode)inMode;
            InitializeComponent();
        }
        
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            switch (mnMode)
            {
                case MasterMechUtil.OPMode.New:
                    if (!ValidInput())
                        return;
                    ibNewMode = true;
                    SaveItem();
                    break;

                case MasterMechUtil.OPMode.Open:
                    if (!ValidInput())
                        return;
                    SaveItem();
                    break;

                case MasterMechUtil.OPMode.Delete:

                    if (textBoxItemNo.Text.Length > 0)
                    {
                        if (MessageBox.Show("Are you sure ?", "Deleting User" + textBoxItemNo.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                            DeleteItem();
                    }

                    else
                    {
                        MessageBox.Show("First Select a User ID");
                        this.buttonSave.Text = "Delete";
                    }                    
                    break;
            }
        }

       private bool ValidInput()
       {
            if(textBoxItemDesc.Text.Length == 0)
            {
                labelerrorItemDesc.Visible = true;
               
                return false;
            }
             
            if(comboBoxItemType.Text.Length == 0)
            {
                labelerrorItemType.Visible = true;
                
                return false;
            }

            if(comboBoxItemCatg.Text.Length == 0)
            {
                labelerrorItemCatg.Visible = true;
                
                return false;
            }

            if(!ValidNumber(textBoxItemPrice.Text) || (textBoxItemPrice.Text.Length ==0))
            {
                labelerrorItemPrice.Visible = true;
               
                return false;
            }
            if(textBoxCGSTRate.Text.Length > 0)
            {
                if (validCGST(textBoxCGSTRate.Text)) 
                    
                {
                    textBoxCGSTRate.ForeColor = Color.Red;
                    textBoxCGSTRate.Font = new Font(textBoxCGSTRate.Font, FontStyle.Bold);
                    labelerrorCGSt.Visible = true;
                    return false;

                }
                
            }
            if(textBoxSGSTRate.Text.Length > 0)
            {
                if(validCGST(textBoxSGSTRate.Text))
                {
                    textBoxSGSTRate.ForeColor = Color.Red;
                    textBoxSGSTRate.Font = new Font(textBoxSGSTRate.Font, FontStyle.Bold);
                    labelerrorSGST.Visible = true;
                    return false;
                }
            }

            if(textBoxIGSTRate.Text.Length > 0)
            {
                if(validCGST(textBoxIGSTRate.Text))
                {
                    textBoxIGSTRate.ForeColor = Color.Red;
                    textBoxIGSTRate.Font = new Font(textBoxIGSTRate.Font, FontStyle.Bold);
                    labelerrorIGST.Visible = true;
                    return false;
                }
            }
             
            if(textBoxQtyHand.Text.Length > 0)
            {
                if (!ValidNumber(textBoxQtyHand.Text))
                {
                    labelerrorQty.Visible = true;
                    return false;
                }
            }

            if(textBoxReOrderQty.Text.Length > 0)
            {
                if(!ValidNumber(textBoxReOrderQty.Text))
                {
                    labelerrorREoder.Visible = true;
                    return false;
                }
            }

            if(textBoxReOrdeLvl.Text.Length > 0)
            {
                if(!ValidNumber(textBoxReOrdeLvl.Text))
                {
                    labelerrorREoderlvl.Visible = true;
                    return false;
                }
            }

            if(textBoxNoOfParts.Text.Length > 0)
            {
                if(!ValidNumber(textBoxNoOfParts.Text))
                {
                    labelerrorNoOFParts.Visible = true;
                    return false;
                }
            }
            if(textBoxItemUOM.Text.Length == 0)
            {
                labelerrorItemUOM.Visible = true;
                labelRequiredFields.Visible = true;
                return false;
            }

            if(comboBoxItemSts.Text.Length == 0)
            {
                labelerrorItemSts.Visible = true;
                labelRequiredFields.Visible = true;
                return false;

            }

            return true;
       }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            switch (mnMode)
            {
                case MasterMechUtil.OPMode.New:
                    buttonItemSearch.Visible = false;
                    break;

                case MasterMechUtil.OPMode.Open:
                    buttonItemSearch.Visible = true;
                    break;

                case MasterMechUtil.OPMode.Delete:

                    this.buttonSave.Text = "Delete";
                    break;
            }
        }

        private void textBoxItemDesc_Enter(object sender, EventArgs e)
        {
            labelerrorItemDesc.Visible = false;
           
        }

        private void comboBoxItemType_Enter(object sender, EventArgs e)
        {
            labelerrorItemType.Visible = false;
            
        }

        private void comboBoxItemCatg_Enter(object sender, EventArgs e)
        {
            labelerrorItemCatg.Visible = false;
           
        }

        private void textBoxItemPrice_Enter(object sender, EventArgs e)
        {
            labelerrorItemPrice.Visible = false;
           
        }

        private void textBoxItemUOM_Enter(object sender, EventArgs e)
        {
            labelerrorItemUOM.Visible = false;
           
        }

        private void comboBoxItemSts_Enter(object sender, EventArgs e)
        {
            labelerrorItemSts.Visible = false;
           
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
        public static bool validCGST(string isNumber)
        {

            string pattern = @"^\d{0,2}(?:\.\d{0,2})?$";//@"^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}9[0-9](\s){0,1}(\-){0,1}(\s){0,1}[1-9]{1}[0-9]{7}$";

            if (Regex.IsMatch(isNumber, pattern))
            {
                return false;
            }


            return true;
        }


        private void SaveItem()
        {
            Item lobjItem = new Item();
            
            lobjItem.ItemDesc = textBoxItemDesc.Text;
            lobjItem.ItemType = comboBoxItemType.Text;
            lobjItem.ItemCatg = comboBoxItemCatg.Text;
            lobjItem.ItemPrice = float.Parse(textBoxItemPrice.Text);
            lobjItem.ItemUOM = textBoxItemUOM.Text;
            lobjItem.ItemSts = comboBoxItemSts.Text;

            if (textBoxCGSTRate.Text.Length > 0)
               lobjItem.CGSTRate = float.Parse(textBoxCGSTRate.Text);
            else
             lobjItem.CGSTRate = null;
            

            if (textBoxSGSTRate.Text.Length > 0)
              lobjItem.SGSTRate = float.Parse(textBoxSGSTRate.Text);
             else
             lobjItem.SGSTRate = null;
            

            if (textBoxIGSTRate.Text.Length > 0)
            lobjItem.IGSTRate = float.Parse(textBoxIGSTRate.Text);
            else
              lobjItem.IGSTRate = null;


            if (textBoxUPCCode.Text.Length > 0)
                lobjItem.UPCCode = textBoxUPCCode.Text;
            else
                lobjItem.UPCCode = null;


            if (textBoxHSHCode.Text.Length > 0)
                lobjItem.HSNCode = textBoxHSHCode.Text;
            else
                lobjItem.HSNCode = null;


            if (textBoxSACCode.Text.Length > 0)
                lobjItem.SACCode = textBoxSACCode.Text;
            else
                lobjItem.SACCode = null;


            if (textBoxQtyHand.Text.Length > 0)
               lobjItem.QtyHand = float.Parse(textBoxQtyHand.Text);
            else
               lobjItem.QtyHand = null;
            

            if (textBoxReOrderQty.Text.Length > 0)
              lobjItem.ReOrderQty = float.Parse(textBoxReOrderQty.Text);
             else
             lobjItem.ReOrderQty = null;
            

            if (textBoxReOrdeLvl.Text.Length > 0)
             lobjItem.ReOrderLvl = float.Parse(textBoxReOrdeLvl.Text);
            else
               lobjItem.ReOrderLvl = null;
            

            if (textBoxNoOfParts.Text.Length > 0)               
                lobjItem.NoOfParts = int.Parse(textBoxNoOfParts.Text);
            else
                lobjItem.NoOfParts = null;

            if (textBoxItemRemarks.Text.Length > 0)
                lobjItem.ItemRemarks = textBoxItemRemarks.Text;
            else
                lobjItem.ItemRemarks = null;

            if (textBoxItemNo.Text.Length > 0)
                lobjItem.ItemNo = int.Parse(textBoxItemNo.Text);
            else
                lobjItem.ItemNo = 0;

            if (!lobjItem.Save(MasterMechUtil.ConnStr, lobjItem.ItemNo, ibNewMode))
                // if (!lObjTest.SaveSQLParam())
                //if (!lObjTest.SaveSQL_SP())
                MessageBox.Show("Test Data could not be saved");
            else
                ClearFields();
        }

        private void ClearFields()
        {
            textBoxItemDesc.Clear();
            comboBoxItemType.SelectedIndex=-1;
            comboBoxItemCatg.SelectedIndex = -1;
            textBoxItemPrice.Clear();
            textBoxItemUOM.Clear();
            comboBoxItemSts.SelectedIndex = -1;
            textBoxCGSTRate.Clear();
            textBoxSGSTRate.Clear();
            textBoxIGSTRate.Clear();
            textBoxUPCCode.Clear();
            textBoxHSHCode.Clear();
            textBoxSACCode.Clear();
            textBoxQtyHand.Clear();
            textBoxReOrderQty.Clear();
            textBoxReOrdeLvl.Clear();
            textBoxNoOfParts.Clear();
            textBoxItemRemarks.Clear();

        }

        private void textBoxQtyHand_KeyPress(object sender, KeyPressEventArgs e)
        {
            labelerrorQty.Visible = false;
        }

        private void textBoxReOrderQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            labelerrorREoder.Visible = false;
        }

        private void textBoxReOrdeLvl_KeyPress(object sender, KeyPressEventArgs e)
        {
            labelerrorREoderlvl.Visible = false;
        }

        private void textBoxNoOfParts_KeyPress(object sender, KeyPressEventArgs e)
        {
            labelerrorNoOFParts.Visible = false;
        }

        private void textBoxCGSTRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxCGSTRate.Font.Bold)
            {
                textBoxCGSTRate.ForeColor = Color.Black;
                textBoxCGSTRate.Font = new Font(textBoxCGSTRate.Font, FontStyle.Regular);
                labelerrorCGSt.Visible = false;
            }
        }



        private void buttonItemSearch_Click(object sender, EventArgs e)
        {
            if (textBoxItemDesc.Text.Length == 0)
            {
                MessageBox.Show("Please enter the Item Description  to search.");
                this.textBoxItemDesc.Focus();
                return;
            }


            // object to call search Test Mthod
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

            // More than 1 Test found after the search then show the result in the grid
            ItemSearchResultForm lObjSearchItemDesc = new ItemSearchResultForm();
            lObjSearchItemDesc.NoSelectMsg = "No Customer selected. Select a customer row and then click select";


            // Build the header
            lObjSearchItemDesc.ItemSearchdataGridView.ReadOnly = true;
            lObjSearchItemDesc.ItemSearchdataGridView.AllowUserToAddRows = false;
            lObjSearchItemDesc.ItemSearchdataGridView.ColumnCount = 19;
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[0].Name = "S.NO";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[1].Name = "ItemNo";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[2].Name = "ItemDesc";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[3].Name = "ItemType";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[4].Name = "ItemCatg";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[5].Name = "ItemPrice";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[6].Name = "ItemUOM";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[7].Name = "ItemSts";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[8].Name = "CGSTRate";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[9].Name = "SGSTRate";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[10].Name = "IGSTRate";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[11].Name = "UPCCode";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[12].Name = "HSNCode";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[13].Name = "SACCode";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[14].Name = "QtyHand";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[15].Name = "ReOrderQty";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[16].Name = "ReOrderLvl";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[17].Name = "NoOfParts";
            lObjSearchItemDesc.ItemSearchdataGridView.Columns[18].Name = "ItemRemarks";



            // Load the data in the grid
            int lnCnt = 1;
            foreach (Item lObjSearchItems in lObjItems)
                lObjSearchItemDesc.ItemSearchdataGridView.Rows.Add(lnCnt++, lObjSearchItems.ItemNo, lObjSearchItems.ItemDesc,
                lObjSearchItems.ItemType, lObjSearchItems.ItemCatg, lObjSearchItems.ItemPrice,
                lObjSearchItems.ItemUOM, lObjSearchItems.ItemSts, lObjSearchItems.CGSTRate,
                lObjSearchItems.SGSTRate, lObjSearchItems.IGSTRate, lObjSearchItems.UPCCode, lObjSearchItems.HSNCode,
                lObjSearchItems.SACCode, lObjSearchItems.QtyHand, lObjSearchItems.ReOrderQty, lObjSearchItems.ReOrderLvl,
                lObjSearchItems.NoOfParts, lObjSearchItems.ItemRemarks,lObjSearchItems.Created,lObjSearchItems.CreatedBy,lObjSearchItems.Modified,
                lObjSearchItems.ModifiedBy);


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
                comboBoxItemType.Text = lObjItUde.ItemType;
                comboBoxItemCatg.Text = lObjItUde.ItemCatg;
                textBoxItemPrice.Text = lObjItUde.ItemPrice.ToString();
                textBoxItemUOM.Text = lObjItUde.ItemUOM;
                comboBoxItemSts.Text = lObjItUde.ItemSts;
                textBoxCGSTRate.Text = lObjItUde.CGSTRate.ToString();
                textBoxSGSTRate.Text = lObjItUde.SGSTRate.ToString();
                textBoxIGSTRate.Text = lObjItUde.IGSTRate.ToString();
                textBoxUPCCode.Text = lObjItUde.UPCCode;
                textBoxHSHCode.Text = lObjItUde.HSNCode;
                textBoxSACCode.Text = lObjItUde.SACCode;
                textBoxQtyHand.Text = lObjItUde.QtyHand.ToString();
                textBoxReOrderQty.Text = lObjItUde.ReOrderQty.ToString();
                textBoxReOrdeLvl.Text = lObjItUde.ReOrderLvl.ToString();
                textBoxNoOfParts.Text = lObjItUde.NoOfParts.ToString();
                textBoxItemRemarks.Text = lObjItUde.ItemRemarks;
                labelCreated.Text = lObjItUde.Created.ToString();
                labelCreatedBy.Text = lObjItUde.CreatedBy;
                labelModified.Text = lObjItUde.Modified.ToString();
                labelModifiedBy.Text = lObjItUde.ModifiedBy;
            }
        }

        private void DeleteItem()
        {
            Item lobjDeleteItem = new Item();
            lobjDeleteItem.ItemNo = int.Parse(textBoxItemNo.Text);
            lobjDeleteItem.Delete(MasterMechUtil.ConnStr, lobjDeleteItem.ItemNo);
            ClearFields();
        }

        private void textBoxSGSTRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxSGSTRate.Font.Bold)
            {
                textBoxSGSTRate.ForeColor = Color.Black;
                textBoxSGSTRate.Font = new Font(textBoxSGSTRate.Font, FontStyle.Regular);
                labelerrorCGSt.Visible = false;
            }
        }

        private void textBoxIGSTRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxIGSTRate.Font.Bold)
            {
                textBoxIGSTRate.ForeColor = Color.Black;
                textBoxIGSTRate.Font = new Font(textBoxIGSTRate.Font, FontStyle.Regular);
                labelerrorCGSt.Visible = false;
            }
        }
    }
    
}

