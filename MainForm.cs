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
    public partial class MainForm : System.Windows.Forms.Form
    {
        private string sUserID;
        private string sUserType;
        public bool ExitApp;
        public MainForm()
        {
            InitializeComponent();
        }
        public MainForm( string isUserID, string isUserType)
        {
            sUserID = isUserID;
            sUserType = isUserType;
            ExitApp = false;
            InitializeComponent();
        }

        private void newToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UserForm lobje = new UserForm();
            lobje.ShowDialog();
        }

        private void openToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UserForm lobje = new UserForm(MasterMechUtil.OPMode.Open);
            lobje.ShowDialog();
        }

        private void deleteToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UserForm lobje = new UserForm(MasterMechUtil.OPMode.Delete);
            lobje.ShowDialog();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ItemForm lobjItem1 = new ItemForm();
            lobjItem1.ShowDialog();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ItemForm lobje = new ItemForm(MasterMechUtil.OPMode.Open);
            lobje.ShowDialog();
        }

        private void newToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CustomerForm lobjItem1 = new CustomerForm();
            lobjItem1.ShowDialog();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ItemForm lobje = new ItemForm(MasterMechUtil.OPMode.Delete);
            lobje.ShowDialog();
        }

        private void openToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CustomerForm lobjItem1 = new CustomerForm(MasterMechUtil.OPMode.Open);
            lobjItem1.ShowDialog();
        }

        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CustomerForm lobjItem1 = new CustomerForm(MasterMechUtil.OPMode.Delete);
            lobjItem1.ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceForm lobjInvoice = new InvoiceForm();
            lobjInvoice.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceForm lobjInvoice = new InvoiceForm(MasterMechUtil.OPMode.Open);
            lobjInvoice.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceForm lobjInvoice = new InvoiceForm(MasterMechUtil.OPMode.Delete);
            lobjInvoice.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm lobjLoginForm = new LoginForm();
            lobjLoginForm.ShowDialog();
        }
    }
}
