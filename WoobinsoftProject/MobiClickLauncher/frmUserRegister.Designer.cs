namespace MobiClick
{
    partial class frmUserRegister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserRegister));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.usernamelLbel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.telNumberCodeLlabel = new System.Windows.Forms.Label();
            this.telNumberTextBox = new System.Windows.Forms.TextBox();
            this.passwordCheckLabel = new System.Windows.Forms.Label();
            this.displayLabel = new System.Windows.Forms.Label();
            this.idSearchButton = new System.Windows.Forms.Button();
            this.passwordCheckTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userIDTextBox = new System.Windows.Forms.TextBox();
            this.userIDLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(421, 51);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(29, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "[사용자등록]";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.userNameTextBox);
            this.panel2.Controls.Add(this.usernamelLbel);
            this.panel2.Controls.Add(this.closeButton);
            this.panel2.Controls.Add(this.addButton);
            this.panel2.Controls.Add(this.telNumberCodeLlabel);
            this.panel2.Controls.Add(this.telNumberTextBox);
            this.panel2.Controls.Add(this.passwordCheckLabel);
            this.panel2.Controls.Add(this.displayLabel);
            this.panel2.Controls.Add(this.idSearchButton);
            this.panel2.Controls.Add(this.passwordCheckTextBox);
            this.panel2.Controls.Add(this.passwordTextBox);
            this.panel2.Controls.Add(this.passwordLabel);
            this.panel2.Controls.Add(this.userIDTextBox);
            this.panel2.Controls.Add(this.userIDLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(421, 238);
            this.panel2.TabIndex = 1;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(99, 135);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(277, 21);
            this.userNameTextBox.TabIndex = 15;
            this.userNameTextBox.UseSystemPasswordChar = true;
            // 
            // usernamelLbel
            // 
            this.usernamelLbel.AutoSize = true;
            this.usernamelLbel.Location = new System.Drawing.Point(40, 139);
            this.usernamelLbel.Name = "usernamelLbel";
            this.usernamelLbel.Size = new System.Drawing.Size(57, 12);
            this.usernamelLbel.TabIndex = 14;
            this.usernamelLbel.Text = "사용자명:";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(301, 200);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 13;
            this.closeButton.Text = "닫기";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(214, 199);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 12;
            this.addButton.Text = "등록";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // telNumberCodeLlabel
            // 
            this.telNumberCodeLlabel.AutoSize = true;
            this.telNumberCodeLlabel.Location = new System.Drawing.Point(40, 168);
            this.telNumberCodeLlabel.Name = "telNumberCodeLlabel";
            this.telNumberCodeLlabel.Size = new System.Drawing.Size(57, 12);
            this.telNumberCodeLlabel.TabIndex = 11;
            this.telNumberCodeLlabel.Text = "전화번호:";
            // 
            // telNumberTextBox
            // 
            this.telNumberTextBox.Location = new System.Drawing.Point(99, 164);
            this.telNumberTextBox.Name = "telNumberTextBox";
            this.telNumberTextBox.Size = new System.Drawing.Size(277, 21);
            this.telNumberTextBox.TabIndex = 10;
            // 
            // passwordCheckLabel
            // 
            this.passwordCheckLabel.AutoSize = true;
            this.passwordCheckLabel.Location = new System.Drawing.Point(12, 97);
            this.passwordCheckLabel.Name = "passwordCheckLabel";
            this.passwordCheckLabel.Size = new System.Drawing.Size(85, 12);
            this.passwordCheckLabel.TabIndex = 7;
            this.passwordCheckLabel.Text = "비밀번호 확인:";
            // 
            // displayLabel
            // 
            this.displayLabel.AutoSize = true;
            this.displayLabel.Location = new System.Drawing.Point(99, 118);
            this.displayLabel.Name = "displayLabel";
            this.displayLabel.Size = new System.Drawing.Size(11, 12);
            this.displayLabel.TabIndex = 6;
            this.displayLabel.Text = "*";
            // 
            // idSearchButton
            // 
            this.idSearchButton.Location = new System.Drawing.Point(304, 34);
            this.idSearchButton.Name = "idSearchButton";
            this.idSearchButton.Size = new System.Drawing.Size(72, 23);
            this.idSearchButton.TabIndex = 5;
            this.idSearchButton.Text = "중복확인";
            this.idSearchButton.UseVisualStyleBackColor = true;
            this.idSearchButton.Click += new System.EventHandler(this.idSearchButton_Click);
            // 
            // passwordCheckTextBox
            // 
            this.passwordCheckTextBox.Location = new System.Drawing.Point(99, 94);
            this.passwordCheckTextBox.Name = "passwordCheckTextBox";
            this.passwordCheckTextBox.Size = new System.Drawing.Size(277, 21);
            this.passwordCheckTextBox.TabIndex = 4;
            this.passwordCheckTextBox.UseSystemPasswordChar = true;
            this.passwordCheckTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.passwordCheckTextBox_KeyUp);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(99, 64);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(277, 21);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(40, 67);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(57, 12);
            this.passwordLabel.TabIndex = 2;
            this.passwordLabel.Text = "비밀번호:";
            // 
            // userIDTextBox
            // 
            this.userIDTextBox.Location = new System.Drawing.Point(98, 35);
            this.userIDTextBox.Name = "userIDTextBox";
            this.userIDTextBox.Size = new System.Drawing.Size(200, 21);
            this.userIDTextBox.TabIndex = 1;
            // 
            // userIDLabel
            // 
            this.userIDLabel.AutoSize = true;
            this.userIDLabel.Location = new System.Drawing.Point(52, 39);
            this.userIDLabel.Name = "userIDLabel";
            this.userIDLabel.Size = new System.Drawing.Size(45, 12);
            this.userIDLabel.TabIndex = 0;
            this.userIDLabel.Text = "아이디:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 289);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(421, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmUserRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 311);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUserRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label telNumberCodeLlabel;
        private System.Windows.Forms.TextBox telNumberTextBox;
        private System.Windows.Forms.Label passwordCheckLabel;
        private System.Windows.Forms.Label displayLabel;
        private System.Windows.Forms.Button idSearchButton;
        private System.Windows.Forms.TextBox passwordCheckTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox userIDTextBox;
        private System.Windows.Forms.Label userIDLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Label usernamelLbel;
    }
}