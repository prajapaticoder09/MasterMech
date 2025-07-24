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
    public partial class ItemSearchResultForm : Form
    {
        public int mSelectedRow = 0;
        public bool mbSelected = false;
        public string NoSelectMsg;
        public bool mbShowSelect = true;
        public ItemSearchResultForm()
        {
            InitializeComponent();
        }

        private void ItemSearchResultForm_Load(object sender, EventArgs e)
        {
            this.ItemSearchdataGridView.Left = this.Padding.Left + 10;
            this.ItemSearchdataGridView.Width = this.Width - this.Padding.Right - this.Padding.Left - 40;
            buttonSelect.Visible = mbShowSelect;
        }

        private void ItemSearchResultForm_Resize(object sender, EventArgs e)
        {
            this.ItemSearchdataGridView.Left = this.Padding.Left + 10;
            this.ItemSearchdataGridView.Width = this.Width - this.Padding.Right - this.Padding.Left - 40;
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (this.ItemSearchdataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show(NoSelectMsg);
                return;

            }
            else
            {
                mSelectedRow = this.ItemSearchdataGridView.SelectedRows[0].Index;
                mbSelected = true;
                this.Hide();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            mSelectedRow = 0;
            mbSelected = true;
            this.Hide();
        }

        private void ItemSearchdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                this.ItemSearchdataGridView.Rows[e.RowIndex].Selected = true;
        }

        private void ItemSearchdataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.ItemSearchdataGridView.Rows[e.RowIndex].Selected = true;
            mSelectedRow = this.ItemSearchdataGridView.SelectedRows[0].Index;
            mbSelected = true;
            this.Hide();
        }
    }
}
