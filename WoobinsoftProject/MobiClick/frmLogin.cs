using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MobiClick
{
    public partial class frmLogin : Form
    {
        private bool _isLogin = false;
        private string _userID = string.Empty;
        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }
        }

        public string UserID
        {
            get
            {
                return _userID;
            }
        }


        public frmLogin()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            bool isLogin = Common.IsUserLoginCheck(idTextBox.Text, pwTextBox.Text);
            if (isLogin)
            {
                _isLogin = isLogin;
                _userID = idTextBox.Text;
                MessageBox.Show("로그인에 성공했습니다.","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("로그인에 실패했습니다.\r\n정보를 확인해 주세요","", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _isLogin = isLogin;
                return;
            }

          
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _isLogin = false;
            this.Close();

        }

        private void userRegButton_Click(object sender, EventArgs e)
        {
            frmUserRegister frm = new frmUserRegister();
            frm.ShowDialog();
            if (frm.IsRegister == true)
            {
                MessageBox.Show("사용자등록이 완료되었습니다.\r\n로그인 해주세요!");
            }
            else
            {
                MessageBox.Show("사용자등록에 실패했습니다.");
            }
        }

        private void pwTextBox_Enter(object sender, EventArgs e)
        {
            loginButton_Click(null, null);
        }
    }
}
