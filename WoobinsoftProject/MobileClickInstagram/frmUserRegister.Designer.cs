namespace MobileClickInstagram
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
            this.macAddressTextBox = new System.Windows.Forms.TextBox();
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
            this.panel1.Size = new System.Drawing.Size(341, 51);
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
            this.panel2.Controls.Add(this.macAddressTextBox);
            this.panel2.Controls.Add(this.userIDLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(341, 162);
            this.panel2.TabIndex = 1;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(85, 63);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(232, 21);
            this.userNameTextBox.TabIndex = 15;
            // 
            // usernamelLbel
            // 
            this.usernamelLbel.AutoSize = true;
            this.usernamelLbel.Location = new System.Drawing.Point(26, 67);
            this.usernamelLbel.Name = "usernamelLbel";
            this.usernamelLbel.Size = new System.Drawing.Size(57, 12);
            this.usernamelLbel.TabIndex = 14;
            this.usernamelLbel.Text = "사용자명:";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(242, 119);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 13;
            this.closeButton.Text = "닫기";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(155, 118);
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
            this.telNumberCodeLlabel.Location = new System.Drawing.Point(26, 96);
            this.telNumberCodeLlabel.Name = "telNumberCodeLlabel";
            this.telNumberCodeLlabel.Size = new System.Drawing.Size(57, 12);
            this.telNumberCodeLlabel.TabIndex = 11;
            this.telNumberCodeLlabel.Text = "전화번호:";
            // 
            // telNumberTextBox
            // 
            this.telNumberTextBox.Location = new System.Drawing.Point(85, 92);
            this.telNumberTextBox.Name = "telNumberTextBox";
            this.telNumberTextBox.Size = new System.Drawing.Size(232, 21);
            this.telNumberTextBox.TabIndex = 10;
            // 
            // macAddressTextBox
            // 
            this.macAddressTextBox.Location = new System.Drawing.Point(84, 35);
            this.macAddressTextBox.Name = "macAddressTextBox";
            this.macAddressTextBox.ReadOnly = true;
            this.macAddressTextBox.Size = new System.Drawing.Size(233, 21);
            this.macAddressTextBox.TabIndex = 1;
            // 
            // userIDLabel
            // 
            this.userIDLabel.AutoSize = true;
            this.userIDLabel.Location = new System.Drawing.Point(14, 39);
            this.userIDLabel.Name = "userIDLabel";
            this.userIDLabel.Size = new System.Drawing.Size(69, 12);
            this.userIDLabel.TabIndex = 0;
            this.userIDLabel.Text = "맥어드레스:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 213);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(341, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmUserRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 235);
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
        private System.Windows.Forms.TextBox macAddressTextBox;
        private System.Windows.Forms.Label userIDLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Label usernamelLbel;
    }
}