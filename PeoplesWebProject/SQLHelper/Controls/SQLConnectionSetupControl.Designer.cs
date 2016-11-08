namespace UCGuideSQLHelper.Controls
{
    partial class SQLConnectionSetupControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TableLayoutPanel tlpServer;
            System.Windows.Forms.GroupBox grpLoginMode;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            System.Windows.Forms.Label lblUserName;
            System.Windows.Forms.Label lblPassword;
            System.Windows.Forms.GroupBox grpDatabases;
            System.Windows.Forms.TableLayoutPanel tlpDatabases;
            System.Windows.Forms.Panel pnlGap;
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cboServer = new System.Windows.Forms.ComboBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.optLSQL = new System.Windows.Forms.RadioButton();
            this.optLWindows = new System.Windows.Forms.RadioButton();
            this.btnReload = new System.Windows.Forms.Button();
            this.cboDatabases = new System.Windows.Forms.ComboBox();
            this.tlpBottom = new System.Windows.Forms.TableLayoutPanel();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            tlpServer = new System.Windows.Forms.TableLayoutPanel();
            grpLoginMode = new System.Windows.Forms.GroupBox();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            lblUserName = new System.Windows.Forms.Label();
            lblPassword = new System.Windows.Forms.Label();
            grpDatabases = new System.Windows.Forms.GroupBox();
            tlpDatabases = new System.Windows.Forms.TableLayoutPanel();
            pnlGap = new System.Windows.Forms.Panel();
            tlpServer.SuspendLayout();
            grpLoginMode.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            grpDatabases.SuspendLayout();
            tlpDatabases.SuspendLayout();
            this.tlpBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpServer
            // 
            tlpServer.ColumnCount = 2;
            tlpServer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpServer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            tlpServer.Controls.Add(this.btnRefresh, 1, 1);
            tlpServer.Controls.Add(this.cboServer, 0, 1);
            tlpServer.Controls.Add(this.lblServer, 0, 0);
            tlpServer.Dock = System.Windows.Forms.DockStyle.Top;
            tlpServer.Location = new System.Drawing.Point(0, 0);
            tlpServer.Name = "tlpServer";
            tlpServer.RowCount = 2;
            tlpServer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            tlpServer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpServer.Size = new System.Drawing.Size(287, 47);
            tlpServer.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(217, 16);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 20);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Re&fresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cboServer
            // 
            this.cboServer.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboServer.FormattingEnabled = true;
            this.cboServer.Location = new System.Drawing.Point(0, 16);
            this.cboServer.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.cboServer.Name = "cboServer";
            this.cboServer.Size = new System.Drawing.Size(211, 20);
            this.cboServer.TabIndex = 0;
            this.cboServer.SelectedIndexChanged += new System.EventHandler(this.cboServer_SelectedIndexChanged);
            this.cboServer.DropDown += new System.EventHandler(this.cboServer_DropDown);
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(0, 0);
            this.lblServer.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(45, 12);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server:";
            // 
            // grpLoginMode
            // 
            grpLoginMode.Controls.Add(tableLayoutPanel1);
            grpLoginMode.Controls.Add(this.optLSQL);
            grpLoginMode.Controls.Add(this.optLWindows);
            grpLoginMode.Dock = System.Windows.Forms.DockStyle.Top;
            grpLoginMode.Location = new System.Drawing.Point(0, 47);
            grpLoginMode.Name = "grpLoginMode";
            grpLoginMode.Padding = new System.Windows.Forms.Padding(3, 3, 3, 8);
            grpLoginMode.Size = new System.Drawing.Size(287, 114);
            grpLoginMode.TabIndex = 1;
            grpLoginMode.TabStop = false;
            grpLoginMode.Text = "Log on mode";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblUserName, 1, 0);
            tableLayoutPanel1.Controls.Add(lblPassword, 1, 1);
            tableLayoutPanel1.Controls.Add(this.txtUserName, 2, 0);
            tableLayoutPanel1.Controls.Add(this.txtPassword, 2, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            tableLayoutPanel1.Location = new System.Drawing.Point(3, 57);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new System.Drawing.Size(281, 49);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // lblUserName
            // 
            lblUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            lblUserName.Location = new System.Drawing.Point(31, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new System.Drawing.Size(78, 24);
            lblUserName.TabIndex = 0;
            lblUserName.Text = "User Name:";
            lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPassword
            // 
            lblPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            lblPassword.Location = new System.Drawing.Point(31, 24);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(78, 25);
            lblPassword.TabIndex = 1;
            lblPassword.Text = "Password:";
            lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUserName
            // 
            this.txtUserName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(115, 3);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(156, 21);
            this.txtUserName.TabIndex = 2;
            this.txtUserName.TextChanged += new System.EventHandler(this.txtUserNamePassword_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(115, 27);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(156, 21);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtUserNamePassword_TextChanged);
            // 
            // optLSQL
            // 
            this.optLSQL.AutoSize = true;
            this.optLSQL.Location = new System.Drawing.Point(15, 39);
            this.optLSQL.Name = "optLSQL";
            this.optLSQL.Size = new System.Drawing.Size(170, 16);
            this.optLSQL.TabIndex = 1;
            this.optLSQL.Text = "&SQL Server Authentication";
            this.optLSQL.UseVisualStyleBackColor = true;
            this.optLSQL.CheckedChanged += new System.EventHandler(this.optWindowsSQL_CheckedChanged);
            // 
            // optLWindows
            // 
            this.optLWindows.AutoSize = true;
            this.optLWindows.Checked = true;
            this.optLWindows.Location = new System.Drawing.Point(15, 18);
            this.optLWindows.Name = "optLWindows";
            this.optLWindows.Size = new System.Drawing.Size(157, 16);
            this.optLWindows.TabIndex = 0;
            this.optLWindows.TabStop = true;
            this.optLWindows.Text = "&Windows Authentication";
            this.optLWindows.UseVisualStyleBackColor = true;
            this.optLWindows.CheckedChanged += new System.EventHandler(this.optWindowsSQL_CheckedChanged);
            // 
            // grpDatabases
            // 
            grpDatabases.Controls.Add(tlpDatabases);
            grpDatabases.Dock = System.Windows.Forms.DockStyle.Top;
            grpDatabases.Location = new System.Drawing.Point(0, 169);
            grpDatabases.Name = "grpDatabases";
            grpDatabases.Size = new System.Drawing.Size(287, 49);
            grpDatabases.TabIndex = 2;
            grpDatabases.TabStop = false;
            grpDatabases.Text = "Databases";
            // 
            // tlpDatabases
            // 
            tlpDatabases.ColumnCount = 2;
            tlpDatabases.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpDatabases.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            tlpDatabases.Controls.Add(this.btnReload, 1, 0);
            tlpDatabases.Controls.Add(this.cboDatabases, 0, 0);
            tlpDatabases.Dock = System.Windows.Forms.DockStyle.Top;
            tlpDatabases.Location = new System.Drawing.Point(3, 17);
            tlpDatabases.Name = "tlpDatabases";
            tlpDatabases.RowCount = 1;
            tlpDatabases.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpDatabases.Size = new System.Drawing.Size(281, 29);
            tlpDatabases.TabIndex = 1;
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.Location = new System.Drawing.Point(204, 3);
            this.btnReload.Margin = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(70, 20);
            this.btnReload.TabIndex = 1;
            this.btnReload.Text = "&Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // cboDatabases
            // 
            this.cboDatabases.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboDatabases.FormattingEnabled = true;
            this.cboDatabases.Location = new System.Drawing.Point(7, 3);
            this.cboDatabases.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.cboDatabases.Name = "cboDatabases";
            this.cboDatabases.Size = new System.Drawing.Size(187, 20);
            this.cboDatabases.TabIndex = 0;
            this.cboDatabases.DropDown += new System.EventHandler(this.cboDatabases_DropDown);
            // 
            // pnlGap
            // 
            pnlGap.Dock = System.Windows.Forms.DockStyle.Top;
            pnlGap.Location = new System.Drawing.Point(0, 161);
            pnlGap.Name = "pnlGap";
            pnlGap.Size = new System.Drawing.Size(287, 8);
            pnlGap.TabIndex = 3;
            // 
            // tlpBottom
            // 
            this.tlpBottom.ColumnCount = 3;
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tlpBottom.Controls.Add(this.btnTest, 0, 0);
            this.tlpBottom.Controls.Add(this.btnOK, 1, 0);
            this.tlpBottom.Controls.Add(this.btnCancel, 2, 0);
            this.tlpBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpBottom.Location = new System.Drawing.Point(0, 219);
            this.tlpBottom.Name = "tlpBottom";
            this.tlpBottom.RowCount = 1;
            this.tlpBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottom.Size = new System.Drawing.Size(287, 39);
            this.tlpBottom.TabIndex = 4;
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTest.Location = new System.Drawing.Point(0, 18);
            this.btnTest.Margin = new System.Windows.Forms.Padding(0, 3, 3, 0);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(77, 21);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "&Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(123, 18);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 21);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOKCancel_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(210, 18);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 21);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnOKCancel_Click);
            // 
            // SQLConnectionSetupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpBottom);
            this.Controls.Add(grpDatabases);
            this.Controls.Add(pnlGap);
            this.Controls.Add(grpLoginMode);
            this.Controls.Add(tlpServer);
            this.MaximumSize = new System.Drawing.Size(287, 258);
            this.MinimumSize = new System.Drawing.Size(287, 258);
            this.Name = "SQLConnectionSetupControl";
            this.Size = new System.Drawing.Size(287, 258);
            this.Load += new System.EventHandler(this.SQLConnectionSetupControl_Load);
            tlpServer.ResumeLayout(false);
            tlpServer.PerformLayout();
            grpLoginMode.ResumeLayout(false);
            grpLoginMode.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            grpDatabases.ResumeLayout(false);
            tlpDatabases.ResumeLayout(false);
            this.tlpBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cboServer;
        private System.Windows.Forms.RadioButton optLSQL;
        private System.Windows.Forms.RadioButton optLWindows;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.ComboBox cboDatabases;
        private System.Windows.Forms.TableLayoutPanel tlpBottom;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblServer;
    }
}