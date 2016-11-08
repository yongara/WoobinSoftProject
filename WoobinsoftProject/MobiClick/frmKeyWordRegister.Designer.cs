namespace MobiClick
{
    partial class frmKeyWordRegister
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.keyWordTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.addKeywordFromFilebutton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.delayTimeEndTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.maxSearchPageComboBox = new System.Windows.Forms.ComboBox();
            this.delayTimeStartTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(482, 27);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(96, 30);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "닫기";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(380, 27);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(96, 30);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "저장";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(83, 49);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(378, 21);
            this.urlTextBox.TabIndex = 3;
            // 
            // keyWordTextBox
            // 
            this.keyWordTextBox.Location = new System.Drawing.Point(83, 15);
            this.keyWordTextBox.Name = "keyWordTextBox";
            this.keyWordTextBox.Size = new System.Drawing.Size(141, 21);
            this.keyWordTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "URL:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "키워드:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.addButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 65);
            this.panel1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(31, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "키워드등록";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.addKeywordFromFilebutton);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.delayTimeEndTextBox);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.maxSearchPageComboBox);
            this.panel2.Controls.Add(this.delayTimeStartTextBox);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.urlTextBox);
            this.panel2.Controls.Add(this.keyWordTextBox);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 65);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(586, 200);
            this.panel2.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(354, 12);
            this.label10.TabIndex = 17;
            this.label10.Text = "** 파일에서 추가시 txt 파일에 아래와 같은 형태로 한줄씩 추가**";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(41, 179);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(354, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "ex)  : 동탄맛집,Cafe,http://m.cafe.naver.com/yyssm/2324567";
            // 
            // addKeywordFromFilebutton
            // 
            this.addKeywordFromFilebutton.Location = new System.Drawing.Point(460, 163);
            this.addKeywordFromFilebutton.Name = "addKeywordFromFilebutton";
            this.addKeywordFromFilebutton.Size = new System.Drawing.Size(114, 33);
            this.addKeywordFromFilebutton.TabIndex = 6;
            this.addKeywordFromFilebutton.Text = "파일에서 추가...";
            this.addKeywordFromFilebutton.UseVisualStyleBackColor = true;
            this.addKeywordFromFilebutton.Click += new System.EventHandler(this.addKeywordFromFilebutton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(166, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "페이지까지 검색";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(254, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(307, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "초 (매 작업시마다 이범위 사이의 랜덤값이 적용 됩니다)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(149, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "초 ~";
            // 
            // delayTimeEndTextBox
            // 
            this.delayTimeEndTextBox.Location = new System.Drawing.Point(187, 116);
            this.delayTimeEndTextBox.Name = "delayTimeEndTextBox";
            this.delayTimeEndTextBox.Size = new System.Drawing.Size(63, 21);
            this.delayTimeEndTextBox.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "체류시간:";
            // 
            // maxSearchPageComboBox
            // 
            this.maxSearchPageComboBox.FormattingEnabled = true;
            this.maxSearchPageComboBox.Items.AddRange(new object[] {
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.maxSearchPageComboBox.Location = new System.Drawing.Point(83, 83);
            this.maxSearchPageComboBox.Name = "maxSearchPageComboBox";
            this.maxSearchPageComboBox.Size = new System.Drawing.Size(77, 20);
            this.maxSearchPageComboBox.TabIndex = 8;
            // 
            // delayTimeStartTextBox
            // 
            this.delayTimeStartTextBox.Location = new System.Drawing.Point(82, 116);
            this.delayTimeStartTextBox.Name = "delayTimeStartTextBox";
            this.delayTimeStartTextBox.Size = new System.Drawing.Size(63, 21);
            this.delayTimeStartTextBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "최대페이지:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 265);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(586, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmKeyWordRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 287);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKeyWordRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.TextBox keyWordTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox delayTimeEndTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox maxSearchPageComboBox;
        private System.Windows.Forms.TextBox delayTimeStartTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button addKeywordFromFilebutton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
    }
}