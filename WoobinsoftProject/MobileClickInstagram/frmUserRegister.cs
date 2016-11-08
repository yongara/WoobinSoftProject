using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MobileClickInstagram
{
    public partial class frmUserRegister : Form
    {

        private bool _isRegister = false;
                
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

            macAddressTextBox.Text = InstagramCommon.GetMacAddress();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userNameTextBox.Text) == true)
            {
                MessageBox.Show("사용자명이 없습니다.\r\n사용자명을 입력해주세요","입력",  MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(telNumberTextBox.Text) == true)
            {
                MessageBox.Show("전화번호가 없습니다.\r\n전화번호를 입력해주세요","입력",  MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string macAddress = macAddressTextBox.Text;
            string userName = userNameTextBox.Text;
            string telNumber = telNumberTextBox.Text;

           
            string result =InstagramCommon.AddInstagramMacAddressInfo(macAddress, userName, telNumber);
            string message = string.Empty;
            switch (result)
            {
                case "001": message = "사용자 등록이 정상적으로 처리되었습니다.\r\n관리자에게 사용등록을 요청하세요.";
                    _isRegister = true;
                    break;
                case "002": message = "이미 등록된 사용자입니다."; break;
                case "003": message = "사용자등록중 에러가 발생했습니다."; break;
            }
             

            MessageBox.Show(message,"완료",  MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }       

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
