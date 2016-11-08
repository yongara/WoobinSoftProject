namespace MobiClick
{
    partial class frmKeywrodBulk
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
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.keywordBulkInsertButtonPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.keywordBulkInsertDataGridView = new System.Windows.Forms.DataGridView();
            this.keywordBulkInsertButtonPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keywordBulkInsertDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(296, 6);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "확인";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(377, 6);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "닫기";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // keywordBulkInsertButtonPanel
            // 
            this.keywordBulkInsertButtonPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.keywordBulkInsertButtonPanel.Controls.Add(this.addButton);
            this.keywordBulkInsertButtonPanel.Controls.Add(this.cancelButton);
            this.keywordBulkInsertButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keywordBulkInsertButtonPanel.Location = new System.Drawing.Point(0, 348);
            this.keywordBulkInsertButtonPanel.Name = "keywordBulkInsertButtonPanel";
            this.keywordBulkInsertButtonPanel.Size = new System.Drawing.Size(464, 42);
            this.keywordBulkInsertButtonPanel.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.keywordBulkInsertDataGridView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(464, 348);
            this.panel2.TabIndex = 3;
            // 
            // keywordBulkInsertDataGridView
            // 
            this.keywordBulkInsertDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.keywordBulkInsertDataGridView.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.keywordBulkInsertDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.keywordBulkInsertDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keywordBulkInsertDataGridView.Location = new System.Drawing.Point(0, 0);
            this.keywordBulkInsertDataGridView.MultiSelect = false;
            this.keywordBulkInsertDataGridView.Name = "keywordBulkInsertDataGridView";
            this.keywordBulkInsertDataGridView.ReadOnly = true;
            this.keywordBulkInsertDataGridView.RowHeadersWidth = 10;
            this.keywordBulkInsertDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.keywordBulkInsertDataGridView.RowTemplate.Height = 23;
            this.keywordBulkInsertDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.keywordBulkInsertDataGridView.Size = new System.Drawing.Size(464, 348);
            this.keywordBulkInsertDataGridView.TabIndex = 0;
            // 
            // frmKeywrodBulk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 390);
            this.ControlBox = false;
            this.Controls.Add(this.keywordBulkInsertButtonPanel);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmKeywrodBulk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "추가할 키워드 확인";
            this.Load += new System.EventHandler(this.frmKeywrodBulk_Load);
            this.keywordBulkInsertButtonPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.keywordBulkInsertDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel keywordBulkInsertButtonPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView keywordBulkInsertDataGridView;
    }
}