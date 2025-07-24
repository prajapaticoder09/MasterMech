
namespace MasterMech
{
    partial class SearchResultForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchResultForm));
            this.SerachResultdataGridView = new System.Windows.Forms.DataGridView();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SerachResultdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SerachResultdataGridView
            // 
            this.SerachResultdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SerachResultdataGridView.Location = new System.Drawing.Point(12, 32);
            this.SerachResultdataGridView.Name = "SerachResultdataGridView";
            this.SerachResultdataGridView.RowHeadersWidth = 51;
            this.SerachResultdataGridView.RowTemplate.Height = 29;
            this.SerachResultdataGridView.Size = new System.Drawing.Size(1100, 306);
            this.SerachResultdataGridView.TabIndex = 0;
            this.SerachResultdataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SerachResultdataGridView_CellClick);
            this.SerachResultdataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SerachResultdataGridView_CellDoubleClick);
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(830, 505);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(94, 29);
            this.buttonSelect.TabIndex = 1;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(1018, 505);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 29);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SearchResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 566);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.SerachResultdataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchResultForm";
            this.Text = "SearchResultForm";
            this.Load += new System.EventHandler(this.SearchResultForm_Load);
            this.Resize += new System.EventHandler(this.SearchResultForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.SerachResultdataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView SerachResultdataGridView;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonCancel;
    }
}