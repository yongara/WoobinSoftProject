namespace MobiClickManager
{
    partial class frmManagerMain
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManagerMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuToolStrip = new System.Windows.Forms.ToolStrip();
            this.productToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.userAgentToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 574);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(915, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuToolStrip
            // 
            this.menuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productToolStripButton,
            this.toolStripSeparator1,
            this.userAgentToolStripButton,
            this.toolStripSeparator2});
            this.menuToolStrip.Location = new System.Drawing.Point(0, 0);
            this.menuToolStrip.Name = "menuToolStrip";
            this.menuToolStrip.Size = new System.Drawing.Size(915, 25);
            this.menuToolStrip.TabIndex = 0;
            this.menuToolStrip.Text = "toolStrip1";
            // 
            // productToolStripButton
            // 
            this.productToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.productToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("productToolStripButton.Image")));
            this.productToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.productToolStripButton.Name = "productToolStripButton";
            this.productToolStripButton.Size = new System.Drawing.Size(103, 22);
            this.productToolStripButton.Text = "회원 및 제품관리";
            this.productToolStripButton.Click += new System.EventHandler(this.productToolStripButton_Click);
            // 
            // userAgentToolStripButton
            // 
            this.userAgentToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.userAgentToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("userAgentToolStripButton.Image")));
            this.userAgentToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.userAgentToolStripButton.Name = "userAgentToolStripButton";
            this.userAgentToolStripButton.Size = new System.Drawing.Size(98, 22);
            this.userAgentToolStripButton.Text = "User Agent 관리";
            this.userAgentToolStripButton.Click += new System.EventHandler(this.userAgentToolStripButton_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 25);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(915, 549);
            this.mainPanel.TabIndex = 1;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // frmManagerMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 596);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmManagerMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MobiClick Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuToolStrip.ResumeLayout(false);
            this.menuToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip menuToolStrip;
        private System.Windows.Forms.ToolStripButton productToolStripButton;
        private System.Windows.Forms.ToolStripButton userAgentToolStripButton;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

    }
}

