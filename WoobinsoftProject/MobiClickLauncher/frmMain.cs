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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string oAuthURL ="http://"+ Common.basicWebDomain + "/RedirectOpenKey.aspx?OpenKey="+Common.baseInitKey;

            //
            
            frmLogin login = new frmLogin();
            login.ShowDialog();
            if (login.IsLogin==false)
            {
                MessageBox.Show("정상적으로 로그인되지 않았습니다. 프로그램을 종료합니다.", "로그인", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            else
            {
                //정상적으로 로그인 된경우 사용자 정보를 가져옵니다. 

                //사용되는 프로그램을 확인하여 사용되는 프로그램을 설정합니다.

                //사용되는 프로그램의 파일의 업데이트를 확인합니다.

                //권한코드를 가져옵니다.









            }            
        }

        private void executeMobiClickButton_Click(object sender, EventArgs e)
        {

        }

        
    }
}
