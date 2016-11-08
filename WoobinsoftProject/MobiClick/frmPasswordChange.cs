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
    public partial class frmPasswordChange : Form
    {
        bool _isPasswordEqual = false;
        string _userID = string.Empty;
        public string UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }

        public frmPasswordChange()
        {
            InitializeComponent();         
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userIDTextBox.Text) == true)
            {
                MessageBox.Show("사용자아이디가 입력되지않았습니다\r\n사용자아이디를 확인해주세요");
                return;
            }

            if (string.IsNullOrEmpty(newPasswordTextBox.Text) == true)
            {
                MessageBox.Show("비밀번호가 입력되지 않았습니다.\r\n비밀번호를 입력해주세요");
                return;
            }

            if (_isPasswordEqual == false)
            {
                MessageBox.Show("비밀번호가 동일하지 않습니다.\r\n비밀번호를 확인해주세요");
                return;
            }

            Common.UpdateChangePassword(userIDTextBox.Text, newPasswordTextBox.Text);
            MessageBox.Show("비밀번호가 변경되었습니다.");
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newPasswordCheckTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(newPasswordTextBox.Text) == false)
            {
                if (newPasswordTextBox.Text !=newPasswordCheckTextBox.Text)
                {
                    displayLabel.Text = "* 비밀번호가 동일하지 않습니다.";
                    _isPasswordEqual = false;
                }
                else
                {
                    displayLabel.Text = "* 비밀번호가 동일합니다.";
                    _isPasswordEqual = true;
                }
            }
        }

        private void frmPasswordChange_Load(object sender, EventArgs e)
        {
            userIDTextBox.Text = _userID;
        }
    }
}
