
namespace MasterMech
{
    partial class ItemSearchResultForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ItemSearchdataGridView = new System.Windows.Forms.DataGridView();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ItemSearchdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ItemSearchdataGridView
            // 
            this.ItemSearchdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemSearchdataGridView.Location = new System.Drawing.Point(51, 48);
            this.ItemSearchdataGridView.Name = "ItemSearchdataGridView";
            this.ItemSearchdataGridView.RowHeadersWidth = 51;
            this.ItemSearchdataGridView.RowTemplate.Height = 29;
            this.ItemSearchdataGridView.Size = new System.Drawing.Size(958, 299);
            this.ItemSearchdataGridView.TabIndex = 0;
            this.ItemSearchdataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemSearchdataGridView_CellClick);
            this.ItemSearchdataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemSearchdataGridView_CellDoubleClick);
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(590, 406);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(94, 29);
            this.buttonSelect.TabIndex = 1;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(801, 406);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 29);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ItemSearchResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 450);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.ItemSearchdataGridView);
            this.Name = "ItemSearchResultForm";
            this.Text = "Search Result Form";
            this.Load += new System.EventHandler(this.ItemSearchResultForm_Load);
            this.Resize += new System.EventHandler(this.ItemSearchResultForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ItemSearchdataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView ItemSearchdataGridView;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonCancel;
    }
}