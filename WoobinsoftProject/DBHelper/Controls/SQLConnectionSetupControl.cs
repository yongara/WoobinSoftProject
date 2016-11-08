using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

using DBHelper.Utilities;

namespace DBHelper.Controls
{
    #region // Delegates //
    public delegate void SQLConnectionSetupActionDelegate(object sender, SQLConnectionSetupControl.SQLConnectionSetup_ActionEventArgs e);
    #endregion / Delegates /

    [ToolboxBitmap(typeof(SQLConnectionSetupControl), "ControlImages.SQLConnectionSetupControl.gif")]
    public partial class SQLConnectionSetupControl : UserControl
    {
        #region // Enumerations //
        public enum LogonMode : int
        {
            Windows = 0,
            SQL
        }
        public enum ActionType
        {
            Test = 0,
            OK,
            Cancel
        }
        #endregion / Enumerations /

        #region // Inner Classes //
        public class SQLConnectionSetup_ActionEventArgs : EventArgs
        {
            public readonly LogonMode LogonMode;
            public readonly ActionType Action;
            public readonly string ConnectionString;

            internal SQLConnectionSetup_ActionEventArgs(ActionType actionType, LogonMode logonMode, string conString)
            {
                this.Action = actionType;
                this.LogonMode = logonMode;
                this.ConnectionString = conString;
            }
        }
        #endregion / Inner Classes /

        //********************************************************************************

        #region // Member Variables //
        bool loadOnStart = false;
        bool closeParentDialog = false;
        bool updateStatusParent = false;
        bool autoSizeParentForm = false;
        string parentFormStatusMsg = string.Empty;
        int timeOut = -1;
        #endregion / Member Variables /

        #region // Consumer Events //
        public event SQLConnectionSetupActionDelegate Action;
        #endregion / Consumer Events /

        #region // Constructor //
        public SQLConnectionSetupControl()
        {
            InitializeComponent();
            this.Action = new SQLConnectionSetupActionDelegate(OnAction);
        }
        #endregion / Constructor /

        #region // Events //
        private void SQLConnectionSetupControl_Load(object sender, EventArgs e)
        {
            this.ParentForm.SizeChanged += new EventHandler(ParentForm_SizeChanged);
            if (this.loadOnStart) this.LoadServers();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadServers();
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            cboDatabases_DropDown(btnReload, null);
        }
        private void cboServer_DropDown(object sender, EventArgs e)
        {
            if (cboServer.DataSource == null) this.LoadServers();
        }
        private void cboServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cboDatabases.DataSource = null;
        }
        private void cboDatabases_DropDown(object sender, EventArgs e)
        {
            if (ServerName == string.Empty)
            {
                this.cboDatabases.DataSource = null;
                return;
            }

            if (this.cboDatabases.DataSource == null) this.LoadDatabases();
        }
        private void optWindowsSQL_CheckedChanged(object sender, EventArgs e)
        {
            bool v = optLSQL.Checked;

            if (v)
                this.AuthenticationMode = LogonMode.SQL;
            else
                this.AuthenticationMode = LogonMode.Windows;

            this.txtUserName.Enabled = v; this.txtPassword.Enabled = v;
        }
        private void txtUserNamePassword_TextChanged(object sender, EventArgs e)
        {
            this.cboDatabases.DataSource = null;
        }
        #endregion / Events /

        #region // Public Properties //

        // ********* Data *********
        [DefaultValue(typeof(SQLConnectionSetupControl.LogonMode), "Windows")]
        public LogonMode AuthenticationMode
        {
            get
            {
                if (optLWindows.Checked) return LogonMode.Windows;
                else return LogonMode.SQL;
            }
            set
            {
                if (value == LogonMode.Windows) optLWindows.Checked = true;
                else optLSQL.Checked = true;

                this.cboDatabases.DataSource = null;
            }
        }
        public string ConnectionString
        {
            get
            {
                string ret = "Data Source=" + this.ServerName;

                if (this.AuthenticationMode == LogonMode.Windows)
                {
                    ret += ";Initial Catalog=" + this.DatabaseName;
                    ret += ";Integrated Security=true";
                }
                else
                {
                    ret += ";Database=" + this.DatabaseName;
                    ret += ";User ID=" + this.UserName;
                    ret += ";Password=" + this.Password;
                }
                if (this.TimeOut > -1) ret += ";Connect Timeout=" + this.TimeOut.ToString();
                return ret;
            }
        }

        public string ServerName
        {
            get { return this.cboServer.Text; }
            set { this.cboServer.Text = value; }
        }
        public string DatabaseName
        {
            get { return this.cboDatabases.Text; }
            set { this.cboDatabases.Text = value; }
        }
        public string UserName
        {
            get { return this.txtUserName.Text; }
            set { this.txtUserName.Text = value; }
        }
        public string Password
        {
            get { return this.txtPassword.Text; }
            set { this.txtPassword.Text = value; }
        }
        [DefaultValue(-1)]
        public int TimeOut
        {
            get { return this.timeOut; }
            set { this.timeOut = value; }
        }

        // ********* Behaviour / Appearance *********
        [DefaultValue(false)]
        public bool AutoSizeParentForm
        {
            get { return this.autoSizeParentForm; }
            set
            {
                this.autoSizeParentForm = value;

                if (autoSizeParentForm && this.ParentForm != null)
                {
                    Form pForm = this.ParentForm;
                    Padding pad = pForm.Padding;
                    System.Drawing.Size sz = this.Size;

                    if (pForm.WindowState == FormWindowState.Maximized) pForm.WindowState = FormWindowState.Normal;

                    this.Location = new Point(pad.Left, pad.Top);
                    this.ParentForm.ClientSize = new Size(sz.Width + pad.Left + pad.Right,
                        sz.Height + pad.Top + pad.Bottom);
                }
            }
        }
        [DefaultValue(false)]
        public bool CloseParentFormOnOKCancel
        {
            get { return this.closeParentDialog; }
            set { this.closeParentDialog = value; }
        }
        [DefaultValue(false)]
        public bool UpdateParentFormStatus
        {
            get { return this.updateStatusParent; }
            set { this.updateStatusParent = value; }
        }
        [DefaultValue(true)]
        public bool ShowTestButton
        {
            get { return this.btnTest.Visible; }
            set { this.btnTest.Visible = value; }
        }
        [DefaultValue("")]
        public string ParentFormStatusMessage
        {
            get { return this.parentFormStatusMsg; }
            set { this.parentFormStatusMsg = value; }
        }

        // ********* Buttons *********
        [DefaultValue("&OK")]
        public string OKButtonText
        {
            get { return this.btnOK.Text; }
            set { this.btnOK.Text = value; }
        }
        [DefaultValue("&Cancel")]
        public string CancelButtonText
        {
            get { return this.btnCancel.Text; }
            set { this.btnCancel.Text = value; }
        }

        // ********* Reserved / Future *********
        [DefaultValue(false)]
        private bool LoadServersOnStart
        {
            get { return this.loadOnStart; }
            set { this.loadOnStart = value; }
        }
        #endregion / Public Properties /

        #region // Public - Functions //
        public bool CheckConnection()
        {
            bool bRet = true;
            try
            {
                SqlConnection con = new SqlConnection(this.ConnectionString);
                con.Open(); con.Close();
            }
            catch { bRet = false; }

            return bRet;
        }
        #endregion / Public - Functions /

        #region // Events - Main Buttons //
        private void btnTest_Click(object sender, EventArgs e)
        {
            if (this.ServerName == string.Empty || this.DatabaseName == string.Empty)
            {
                MessageBox.Show("Server/Database should not be empty.", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                SqlConnection con = new SqlConnection(this.ConnectionString);
                con.Open(); con.Close();
                MessageBox.Show("Connection successfully established.", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnOKCancel_Click(object sender, EventArgs e)
        {
            string conString = this.ConnectionString;
            ActionType aType = ActionType.Test;

            if (sender == btnOK)
                aType = ActionType.OK;
            else if (sender == btnCancel)
                aType = ActionType.Cancel;

            SQLConnectionSetup_ActionEventArgs eargs = new SQLConnectionSetup_ActionEventArgs(aType, this.AuthenticationMode, this.ConnectionString);

            if (this.CloseParentFormOnOKCancel && this.ParentForm != null)
                this.ParentForm.Close();

            this.Action(this, eargs);
        }
        #endregion / Events - Main Buttons /


        #region // Private Functions - Loading //
        private void LoadServers()
        {
            this.cboServer.DataSource = null;
            this.cboDatabases.DataSource = null;

            this._updateParentForm(true);

            List<SQLInstancesFinder.SQLServerInstance> lst = SQLInstancesFinder.GetNetworkSQLServerInstances();

            this._updateParentForm(false);

            this.cboServer.DataSource = lst;
            this.cboServer.DisplayMember = "ServerInstance";
        }
        private void LoadDatabases()
        {
            this.cboDatabases.DataSource = null;
            List<string> lst = null;

            this._updateParentForm(true);
            try
            {

                if (this.AuthenticationMode == LogonMode.Windows)
                    lst = SQLInstancesFinder.GetDatabasesFromServer(cboServer.Text);
                else
                    lst = SQLInstancesFinder.GetDatabasesFromServer(cboServer.Text, txtUserName.Text, txtPassword.Text);

            }
            catch { }
            finally
            {
                this._updateParentForm(false);
            }

            this.cboDatabases.DataSource = lst;
        }
        #endregion / Private Functions - Loading /

        #region // Private Functions - Helpers/Utilities //
        private string prvPFText = string.Empty;
        private void _updateParentForm(bool isStart)
        {
            if (isStart)
            {
                if (this.UpdateParentFormStatus)
                {
                    prvPFText = this.ParentForm.Text;
                    this.ParentForm.Text = this.ParentFormStatusMessage;
                }
            }
            else
            {
                if (this.UpdateParentFormStatus) this.ParentForm.Text = prvPFText;
            }
        }
        #endregion / Private Functions - Helpers/Utilities /

        private void ParentForm_SizeChanged(object sender, EventArgs e)
        {
            this.AutoSizeParentForm = this.autoSizeParentForm;
        }
        private void OnAction(object sender, SQLConnectionSetupControl.SQLConnectionSetup_ActionEventArgs e)
        {
            // Do Nothing
        }
    }
}