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
    public partial class frmUserRegister : Form
    {

        private bool _isRegister = false;
        private bool _isPasswordEqual = false;
        private bool _isIdDupleCheck = false;
        private string _macAddress = string.Empty;
        public bool IsRegister
        {
            get
            {
                return _isRegister;
            }
        }

        public frmUserRegister()
        {
            InitializeComponent();
        }

        private void idSearchButton_Click(object sender, EventArgs e)
        {
            if (Common.IsUserIDCheck(userIDTextBox.Text) == false)
            {
                MessageBox.Show("동일한 아이디가 존재합니다.");
                return;
            }
            else
            {
                MessageBox.Show("동일한아이디가 존재하지 않습니다.\r\n사용가능한 아이디 입니다.");
                _isIdDupleCheck = true;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (_isIdDupleCheck == false)
            {
                MessageBox.Show("ID 중복체크가 필요합니다.\r\n아이디중복을 확인해주세요");
                return;
            }

            if (_isPasswordEqual == false)
            {
                MessageBox.Show("비밀번호가 동일하지 않습니다.\r\n비밀번호를 확인해주세요");
                return;
            }

            if (string.IsNullOrEmpty(userNameTextBox.Text) == true)
            {
                MessageBox.Show("사용자명이 없습니다.\r\n사용자명을 입력해주세요");
                return;
            }

            if (string.IsNullOrEmpty(telNumberTextBox.Text) == true)
            {
                MessageBox.Show("전화번호가 없습니다.\r\n전화번호를 입력해주세요");
                return;
            }

            string userid = userIDTextBox.Text;
            string password = passwordTextBox.Text;
            string userName = _macAddress;
            string telNumber = telNumberTextBox.Text;

            string result = Common.InsertUserInfo(userid, password, userName, telNumber);
            string message = string.Empty;
            switch (result)
            {
                case "001": message = "사용자 등록이 정상적으로 처리되었습니다.";
                    _isRegister = true;
                    break;               
                case "002": message = "이미 등록된 사용자입니다."; break;
                case "003": message = "사용자등록중 에러가 발생했습니다."; break;
            }

            MessageBox.Show(message);
            this.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void passwordCheckTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordTextBox.Text) == false)
            {
                if (passwordTextBox.Text != passwordCheckTextBox.Text)
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
    }
}
