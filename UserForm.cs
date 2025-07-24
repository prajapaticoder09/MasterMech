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
    public partial class UserForm : Form
    {
        private MasterMechUtil.OPMode mnMode;
        bool ibNewMode = false;
        public UserForm()
        {
            InitializeComponent();
        }

        public UserForm(MasterMechUtil.OPMode inMode)
        {
            mnMode = (MasterMechUtil.OPMode)inMode;
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidInput()
        {
            if(textBoxUID.Text.Length == 0)
            {
                labelUID.Visible = true;
                return false;
            }

            if((textBoxPWD.Text.Length == 0) || (textBoxPWD.Text != textBoxConfirmPWD.Text))
            {
                labelConfirmPWD.Visible = true;
                return false;
            }

            if(textBoxUserName.Text.Length == 0)
            {
                labelUserName.Visible = true;
                return false;
            }

            
            if (validMobileNo(textBoxMobileNo.Text)) 
            {
                textBoxMobileNo.ForeColor = Color.Red;
                textBoxMobileNo.Font = new Font(textBoxMobileNo.Font, FontStyle.Bold);
                labelMobileNo.Visible = true;
                    return false;
            }
           
            

            if(validEmail(textBoxEmailID.Text))
            {
              textBoxEmailID.ForeColor = Color.Red;
              textBoxEmailID.Font = new Font(textBoxEmailID.Font, FontStyle.Bold);
              labelEmailID.Visible = true;
              return false;
                
            }
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            switch (mnMode)
            {
                case MasterMechUtil.OPMode.New:
                    if (!ValidInput())
                        return;
                    
                    ibNewMode = true;
                    SaveUser();
                   break;

                case MasterMechUtil.OPMode.Open:
                    if (!ValidInput())
                        return;
                    SaveUser();
                    break;
                    
                    
                case MasterMechUtil.OPMode.Delete:
                   
                    if(textBoxUID.Text.Length > 0)
                    {
                        if (MessageBox.Show("Are you sure ?", "Deleting User" + textBoxUID.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                            DeleteUser();                   
                    }

                    else
                    {
                        MessageBox.Show("First Select a User ID");
                        this.buttonSave.Text = "Delete";
                    }



                    this.buttonSave.Text = "Delete";
                    break;
            }
        }

       

        public static bool validMobileNo(string isNumber)
        {
            
            string pattern = @"^[0-9]{10}$";//@"^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}9[0-9](\s){0,1}(\-){0,1}(\s){0,1}[1-9]{1}[0-9]{7}$";
            
                if (Regex.IsMatch(isNumber, pattern))
                {
                    return false;
                }
            
            
            return true;
        }

        public static bool validEmail(String isWord)
        {
            string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            if(Regex.IsMatch(isWord, pattern))
            {
                return false;
            }

            return true;
        }

        private void textBoxUID_Enter(object sender, EventArgs e)
        {
            labelUID.Visible = false;
        }

        private void textBoxConfirmPWD_Enter(object sender, EventArgs e)
        { 
            labelConfirmPWD.Visible = false;
        }

        private void textBoxUserName_Enter(object sender, EventArgs e)
        {
            labelUserName.Visible = false;
        }

        private void textBoxMobileNo_Enter(object sender, EventArgs e)
        {
        }

        private void textBoxEmailID_Enter(object sender, EventArgs e)
        {
        }

        private void textBoxPWD_Enter(object sender, EventArgs e)
        {
            labelConfirmPWD.Visible = false;
        }

        private void SaveUser()
        {
            UserDtl lobjUser = new UserDtl();
            lobjUser.sUserID = textBoxUID.Text;
            lobjUser.sPwd = MasterMechUtil.Encrypt ( textBoxPWD.Text);

            lobjUser.sUserName = textBoxUserName.Text;
            lobjUser.sMobNo = textBoxMobileNo.Text;
            lobjUser.sEmailID = textBoxEmailID.Text;
            lobjUser.sUserType = comboBoxUserType.Text;
            lobjUser.sRemarks = textBoxRemarks.Text;

            if (!lobjUser.Save(MasterMechUtil.ConnStr,lobjUser.sUserID, ibNewMode))
               // if (!lObjTest.SaveSQLParam())
                    //if (!lObjTest.SaveSQL_SP())
                    MessageBox.Show("Test Data could not be saved");
                else
                    ClearFields();
        }

        private void ClearFields()
        {
            textBoxUID.Clear();
            textBoxPWD.Clear();
            textBoxConfirmPWD.Clear();
            textBoxUserName.Clear();
            textBoxMobileNo.Clear();
            textBoxEmailID.Clear();
            comboBoxUserType.SelectedIndex = -1;
            textBoxRemarks.Clear();
        }

        private void buttonSearchUserName_Click(object sender, EventArgs e)
        {
            if (textBoxUID.Text.Length == 0)
            {
                MessageBox.Show("Please enter the User Name  to search.");
                this.textBoxUID.Focus();
                return;
            }
            // New User being added
            if (mnMode == MasterMechUtil.OPMode.New)
            {
                UserDtl lobjUserDtl = new UserDtl();
                lobjUserDtl.sUserID = textBoxUID.Text.ToString();

                if (lobjUserDtl.ValidUserID(MasterMechUtil.ConnStr))
                {
                    labelUID.Visible = true;
                    return;
                }
                else
                {
                    MessageBox.Show("valid User ID. You can continue.", "Valid USer ID", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    textBoxUID.Focus();
                }
            }
            else
            {
                // object to call search Test Mthod
                UserDtl lObjUserdtl = new UserDtl(MasterMechUtil.ConnStr, MasterMechUtil.sUserID);
                List<UserDtl> lObjUserdtls = new List<UserDtl>();

                // Get the list of customers based on Mobile No search
                lObjUserdtl.SearchUser(MasterMechUtil.ConnStr, lObjUserdtl.sUserID, textBoxUID.Text.ToString(), lObjUserdtls);
                if (lObjUserdtls.Count == 0)
                {
                    MessageBox.Show("No Test found!");
                    this.textBoxUID.Focus();
                    return;
                }
                else if (lObjUserdtls.Count == 1)
                {
                    LoadUserData(lObjUserdtls[0].sUserID);
                    return;
                }

                // More than 1 Test found after the search then show the result in the grid
                SearchResultForm lObjSearchResult = new SearchResultForm();
                lObjSearchResult.NoSelectMsg = "No Customer selected. Select a customer row and then click select";

                // Build the header
                lObjSearchResult.SerachResultdataGridView.ReadOnly = true;
                lObjSearchResult.SerachResultdataGridView.AllowUserToAddRows = false;
                lObjSearchResult.SerachResultdataGridView.ColumnCount = 13;
                lObjSearchResult.SerachResultdataGridView.Columns[0].Name = "S. No";
                lObjSearchResult.SerachResultdataGridView.Columns[1].Name = "UserID";
                lObjSearchResult.SerachResultdataGridView.Columns[2].Name = "User Name";
                lObjSearchResult.SerachResultdataGridView.Columns[3].Name = "Mob No";
                lObjSearchResult.SerachResultdataGridView.Columns[4].Name = "EmailID";
                lObjSearchResult.SerachResultdataGridView.Columns[5].Name = "User Type";
                lObjSearchResult.SerachResultdataGridView.Columns[6].Name = "Remarks";

                 /* lObjSearchResult.SerachResultdataGridView.Columns[7].Name = "LastLoginTime";
                  lObjSearchResult.SerachResultdataGridView.Columns[8].Name = "Created";
                  lObjSearchResult.SerachResultdataGridView.Columns[9].Name = "CreatedBy";
                  lObjSearchResult.SerachResultdataGridView.Columns[10].Name = "Modified";
                  lObjSearchResult.SerachResultdataGridView.Columns[11].Name = "ModifiedBy";
                  lObjSearchResult.SerachResultdataGridView.Columns[12].Name = "Deleted";
                  lObjSearchResult.SerachResultdataGridView.Columns[13].Name = "DeletedON"; */



                // Load the data in the grid
                int lnCnt = 1;
                foreach (UserDtl lObjSearchUser in lObjUserdtls)
                    lObjSearchResult.SerachResultdataGridView.Rows.Add(lnCnt++, lObjSearchUser.sUserID, lObjSearchUser.sUserName,
                    lObjSearchUser.sMobNo, lObjSearchUser.sEmailID, lObjSearchUser.sUserType,
                    lObjSearchUser.sRemarks,lObjSearchUser.dLastLoginTime,lObjSearchUser.dLastPwdChangeTime,
                    lObjSearchUser.dCreated,lObjSearchUser.sCreatedBy,lObjSearchUser.dModified,lObjSearchUser.sModifiedBy);



                lObjSearchResult.ShowDialog();
                int lnSelectedRow = lObjSearchResult.mSelectedRow;
                string lsUserID = lObjSearchResult.SerachResultdataGridView.Rows[lnSelectedRow].Cells[1].Value.ToString();
                if (lObjSearchResult.mbSelected)
                    LoadUserData(lsUserID);
                else
                    this.textBoxUserName.Focus();
            }
        }

        private void LoadUserData(string lsUserID)
        {
            UserDtl lObjUse = new UserDtl(MasterMechUtil.ConnStr, MasterMechUtil.sUserID);
            //UserDtl lObjTest = new UserDtl(Utility.msConnStr, Utility.msUserID);
            if (lObjUse.Load(MasterMechUtil.ConnStr,lsUserID))
            {
                textBoxUID.Text = lsUserID;
                textBoxUserName.Text = lObjUse.sUserName;
                textBoxMobileNo.Text = lObjUse.sMobNo;
                textBoxEmailID.Text = lObjUse.sEmailID;
                comboBoxUserType.Text = lObjUse.sUserType;
                textBoxRemarks.Text = lObjUse.sRemarks;
                textBoxPWD.Text = MasterMechUtil.Decrypt(lObjUse.sPwd);
                textBoxConfirmPWD.Text = MasterMechUtil.Decrypt(lObjUse.sPwd);
                labelLastLogin.Text =  lObjUse.dLastLoginTime.ToString();
                labelPWD.Text = lObjUse.dLastPwdChangeTime.ToString();
                labelCreated.Text = lObjUse.dCreated.ToString();
                labelCreatedBy.Text = lObjUse.sCreatedBy;
                labelModified.Text = lObjUse.dModified.ToString();
                labelModifiedBy.Text = lObjUse.sModifiedBy;
            }
        }


        private void UserForm_Load(object sender, EventArgs e)
        {
            textBoxPWD.PasswordChar = '*';
            textBoxConfirmPWD.PasswordChar = '*';

            switch (mnMode)
            {
                case MasterMechUtil.OPMode.New:
                    buttonSearchUserName.Visible = false;
                    break;

                case MasterMechUtil.OPMode.Delete:
                   
                    this.buttonSave.Text = "Delete";
                    break;

            }
        }

        private void DeleteUser()
        {
            UserDtl lobjDeleteUser = new UserDtl(MasterMechUtil.ConnStr, MasterMechUtil.sUserID);
            lobjDeleteUser.sUserID = textBoxUID.Text;
            lobjDeleteUser.Delete(MasterMechUtil.ConnStr,MasterMechUtil.sUserID);
            ClearFields();
        }

        private void comboBoxUserType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxUserType_Enter(object sender, EventArgs e)
        {
            labelUsreType.Visible = false;
        }

        private void textBoxMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(textBoxMobileNo.Font.Bold)
            {
                textBoxMobileNo.ForeColor = Color.Black;
                textBoxMobileNo.Font = new Font(textBoxMobileNo.Font, FontStyle.Regular);
                labelMobileNo.Visible = false;
            }
        }

        private void textBoxEmailID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(textBoxEmailID.Font.Bold)
            {
                textBoxEmailID.ForeColor = Color.Black;
                textBoxEmailID.Font = new Font(textBoxEmailID.Font, FontStyle.Regular);
                labelEmailID.Visible = false;
            }
        }

        
    }
}
