namespace MobiClickManager
{
    partial class ucUserAgent
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.userAgentDataGridView = new System.Windows.Forms.DataGridView();
            this.userAgentPanel = new System.Windows.Forms.Panel();
            this.regButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.userAgentTextBox = new System.Windows.Forms.TextBox();
            this.userAgentLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.userAgentDataGridView)).BeginInit();
            this.userAgentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // userAgentDataGridView
            // 
            this.userAgentDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.userAgentDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.userAgentDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.userAgentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userAgentDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userAgentDataGridView.Location = new System.Drawing.Point(0, 0);
            this.userAgentDataGridView.MultiSelect = false;
            this.userAgentDataGridView.Name = "userAgentDataGridView";
            this.userAgentDataGridView.ReadOnly = true;
            this.userAgentDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.userAgentDataGridView.RowTemplate.Height = 23;
            this.userAgentDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.userAgentDataGridView.Size = new System.Drawing.Size(921, 565);
            this.userAgentDataGridView.TabIndex = 3;
            this.userAgentDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.userAgentDataGridView_CellContentClick);
            // 
            // userAgentPanel
            // 
            this.userAgentPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.userAgentPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.userAgentPanel.Controls.Add(this.userAgentLabel);
            this.userAgentPanel.Controls.Add(this.deleteButton);
            this.userAgentPanel.Controls.Add(this.regButton);
            this.userAgentPanel.Controls.Add(this.userAgentTextBox);
            this.userAgentPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.userAgentPanel.Location = new System.Drawing.Point(0, 0);
            this.userAgentPanel.Name = "userAgentPanel";
            this.userAgentPanel.Size = new System.Drawing.Size(921, 61);
            this.userAgentPanel.TabIndex = 4;
            // 
            // regButton
            // 
            this.regButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.regButton.Location = new System.Drawing.Point(748, 13);
            this.regButton.Name = "regButton";
            this.regButton.Size = new System.Drawing.Size(79, 30);
            this.regButton.TabIndex = 9;
            this.regButton.Text = "등록";
            this.regButton.UseVisualStyleBackColor = true;
            this.regButton.Click += new System.EventHandler(this.regButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(829, 13);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(79, 30);
            this.deleteButton.TabIndex = 8;
            this.deleteButton.Text = "삭제";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // userAgentTextBox
            // 
            this.userAgentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userAgentTextBox.Location = new System.Drawing.Point(119, 18);
            this.userAgentTextBox.Name = "userAgentTextBox";
            this.userAgentTextBox.Size = new System.Drawing.Size(623, 21);
            this.userAgentTextBox.TabIndex = 4;
            // 
            // userAgentLabel
            // 
            this.userAgentLabel.AutoSize = true;
            this.userAgentLabel.Location = new System.Drawing.Point(34, 22);
            this.userAgentLabel.Name = "userAgentLabel";
            this.userAgentLabel.Size = new System.Drawing.Size(79, 12);
            this.userAgentLabel.TabIndex = 10;
            this.userAgentLabel.Text = "User Agent : ";
            // 
            // ucUserAgent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.userAgentPanel);
            this.Controls.Add(this.userAgentDataGridView);
            this.Name = "ucUserAgent";
            this.Size = new System.Drawing.Size(921, 565);
            this.Load += new System.EventHandler(this.ucUserAgent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.userAgentDataGridView)).EndInit();
            this.userAgentPanel.ResumeLayout(false);
            this.userAgentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView userAgentDataGridView;
        private System.Windows.Forms.Panel userAgentPanel;
        private System.Windows.Forms.Button regButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TextBox userAgentTextBox;
        private System.Windows.Forms.Label userAgentLabel;
    }
}
