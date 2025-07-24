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
    public partial class LoginForm : Form
    {
        public int nStatus = 0;
        public string sUserID;
        public bool bOKButtonClicked = false;
        public string sFY;
        public string sUserType;
        public LoginForm()
        {
            InitializeComponent();
            textBoxPWD.PasswordChar = '*';
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            nStatus = 0;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string lsMessage = "";

            if (textBoxUID.Text.Length == 0)
                lsMessage = "Please enter User ID.\n";

            if (textBoxPWD.Text.Length == 0)
                lsMessage += "Please enter User Password";

            if(lsMessage.Length !=0)
            {
                MessageBox.Show(lsMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UserDtl lobjUserDtl = new UserDtl();
            lobjUserDtl.sUserID = textBoxUID.Text;
            lobjUserDtl.sPwd = MasterMechUtil.Encrypt(textBoxPWD.Text);

            if(lobjUserDtl.ValidLogin(MasterMechUtil.ConnStr))
            {
                sUserID = lobjUserDtl.sUserID;
                sUserType = lobjUserDtl.sUserType;
                lobjUserDtl.UpdateLoginTime(MasterMechUtil.ConnStr, lobjUserDtl.sUserID);
                nStatus = 1;
                this.textBoxUID.Text = "";
                this.textBoxPWD.Text = "";
                bOKButtonClicked = true;
                sFY = comboBoxFy.Text;
                LoadFY();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid User ID or Password. Try Again");
                this.textBoxUID.Focus();
            }

        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!bOKButtonClicked)
                nStatus = 0;
        }

        private void LoadFY()
        {
            string[] lsFYList = MasterMechUtil.FYList();
            string lsCurrFY = MasterMechUtil.CurrFY();

            this.comboBoxFy.Items.Clear();

            for (int lnCount = 0; lnCount < lsFYList.Length; lnCount++)
            {
                this.comboBoxFy.Items.Add(lsFYList[lnCount]);
                if (lsFYList[lnCount].Equals(lsCurrFY))
                    this.comboBoxFy.SelectedIndex = lnCount;

            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            LoadFY();
            this.textBoxUID.Focus();
        }
    }
}
