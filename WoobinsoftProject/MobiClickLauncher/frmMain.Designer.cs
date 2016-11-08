namespace MobiClick
{
    partial class frmMain
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
            this.executeMobiClickButton = new System.Windows.Forms.Button();
            this.executeMobiInstaButton = new System.Windows.Forms.Button();
            this.executeMobiNoteButton = new System.Windows.Forms.Button();
            this.executeMobiCacaoButton = new System.Windows.Forms.Button();
            this.executeMobiFaceButton = new System.Windows.Forms.Button();
            this.executeMobiTweterButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // executeMobiClickButton
            // 
            this.executeMobiClickButton.BackColor = System.Drawing.Color.AliceBlue;
            this.executeMobiClickButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.executeMobiClickButton.Location = new System.Drawing.Point(33, 26);
            this.executeMobiClickButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.executeMobiClickButton.Name = "executeMobiClickButton";
            this.executeMobiClickButton.Size = new System.Drawing.Size(107, 90);
            this.executeMobiClickButton.TabIndex = 0;
            this.executeMobiClickButton.Text = "모비클릭";
            this.executeMobiClickButton.UseVisualStyleBackColor = false;
            this.executeMobiClickButton.Click += new System.EventHandler(this.executeMobiClickButton_Click);
            // 
            // executeMobiInstaButton
            // 
            this.executeMobiInstaButton.BackColor = System.Drawing.Color.AliceBlue;
            this.executeMobiInstaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.executeMobiInstaButton.Location = new System.Drawing.Point(179, 26);
            this.executeMobiInstaButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.executeMobiInstaButton.Name = "executeMobiInstaButton";
            this.executeMobiInstaButton.Size = new System.Drawing.Size(107, 90);
            this.executeMobiInstaButton.TabIndex = 1;
            this.executeMobiInstaButton.Text = "모비인스타";
            this.executeMobiInstaButton.UseVisualStyleBackColor = false;
            // 
            // executeMobiNoteButton
            // 
            this.executeMobiNoteButton.BackColor = System.Drawing.Color.AliceBlue;
            this.executeMobiNoteButton.Enabled = false;
            this.executeMobiNoteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.executeMobiNoteButton.Location = new System.Drawing.Point(324, 26);
            this.executeMobiNoteButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.executeMobiNoteButton.Name = "executeMobiNoteButton";
            this.executeMobiNoteButton.Size = new System.Drawing.Size(107, 90);
            this.executeMobiNoteButton.TabIndex = 2;
            this.executeMobiNoteButton.Text = "모비쪽지";
            this.executeMobiNoteButton.UseVisualStyleBackColor = false;
            // 
            // executeMobiCacaoButton
            // 
            this.executeMobiCacaoButton.BackColor = System.Drawing.Color.AliceBlue;
            this.executeMobiCacaoButton.Enabled = false;
            this.executeMobiCacaoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.executeMobiCacaoButton.Location = new System.Drawing.Point(33, 135);
            this.executeMobiCacaoButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.executeMobiCacaoButton.Name = "executeMobiCacaoButton";
            this.executeMobiCacaoButton.Size = new System.Drawing.Size(107, 90);
            this.executeMobiCacaoButton.TabIndex = 3;
            this.executeMobiCacaoButton.Text = "모비카카오";
            this.executeMobiCacaoButton.UseVisualStyleBackColor = false;
            // 
            // executeMobiFaceButton
            // 
            this.executeMobiFaceButton.BackColor = System.Drawing.Color.AliceBlue;
            this.executeMobiFaceButton.Enabled = false;
            this.executeMobiFaceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.executeMobiFaceButton.Location = new System.Drawing.Point(179, 135);
            this.executeMobiFaceButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.executeMobiFaceButton.Name = "executeMobiFaceButton";
            this.executeMobiFaceButton.Size = new System.Drawing.Size(107, 90);
            this.executeMobiFaceButton.TabIndex = 4;
            this.executeMobiFaceButton.Text = "모비페이스";
            this.executeMobiFaceButton.UseVisualStyleBackColor = false;
            // 
            // executeMobiTweterButton
            // 
            this.executeMobiTweterButton.BackColor = System.Drawing.Color.AliceBlue;
            this.executeMobiTweterButton.Enabled = false;
            this.executeMobiTweterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.executeMobiTweterButton.Location = new System.Drawing.Point(324, 135);
            this.executeMobiTweterButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.executeMobiTweterButton.Name = "executeMobiTweterButton";
            this.executeMobiTweterButton.Size = new System.Drawing.Size(107, 90);
            this.executeMobiTweterButton.TabIndex = 5;
            this.executeMobiTweterButton.Text = "모비트위터";
            this.executeMobiTweterButton.UseVisualStyleBackColor = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(468, 258);
            this.Controls.Add(this.executeMobiTweterButton);
            this.Controls.Add(this.executeMobiFaceButton);
            this.Controls.Add(this.executeMobiCacaoButton);
            this.Controls.Add(this.executeMobiNoteButton);
            this.Controls.Add(this.executeMobiInstaButton);
            this.Controls.Add(this.executeMobiClickButton);
            this.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MobiClick Launcher";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button executeMobiClickButton;
        private System.Windows.Forms.Button executeMobiInstaButton;
        private System.Windows.Forms.Button executeMobiNoteButton;
        private System.Windows.Forms.Button executeMobiCacaoButton;
        private System.Windows.Forms.Button executeMobiFaceButton;
        private System.Windows.Forms.Button executeMobiTweterButton;
    }
}

