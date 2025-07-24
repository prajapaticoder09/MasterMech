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
    public partial class SearchResultForm : Form
    {
        public int mSelectedRow = 0;
        public bool mbSelected = false;
        public string NoSelectMsg;
        public bool mbShowSelect = true;
        public SearchResultForm()
        {
            InitializeComponent();
        }

        private void SearchResultForm_Load(object sender, EventArgs e)
        {
            this.SerachResultdataGridView.Left = this.Padding.Left + 10;
            this.SerachResultdataGridView.Width = this.Width - this.Padding.Right - this.Padding.Left - 40;
            buttonSelect.Visible = mbShowSelect;
        }

        private void SearchResultForm_Resize(object sender, EventArgs e)
        {
            this.SerachResultdataGridView.Left = this.Padding.Left + 10;
            this.SerachResultdataGridView.Width = this.Width - this.Padding.Right - this.Padding.Left - 40;
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (this.SerachResultdataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show(NoSelectMsg);
                return;

            }
            else
            {
                mSelectedRow = this.SerachResultdataGridView.SelectedRows[0].Index;
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

        private void SerachResultdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                this.SerachResultdataGridView.Rows[e.RowIndex].Selected = true;
        }

        private void SerachResultdataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.SerachResultdataGridView.Rows[e.RowIndex].Selected = true;
            mSelectedRow = this.SerachResultdataGridView.SelectedRows[0].Index;
            mbSelected = true;
            this.Hide();
        }
    }
}
