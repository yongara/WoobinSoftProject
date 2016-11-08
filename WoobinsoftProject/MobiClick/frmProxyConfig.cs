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
    public partial class frmProxyConfig : Form
    {
        private string proxyIpOpenPath = string.Empty;
        public frmProxyConfig()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("프록시IP 경로를 변경하시겠습니까?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(proxyIpOpenPath) == true)
                {
                    MessageBox.Show("경로가 설정되지 않았습니다.");
                }
                else
                {
                    Common.proxyIpListPath = proxyIpOpenPath;
                    this.Close();
                }
            }
        }

        private void fileDialogButton_Click(object sender, EventArgs e)
        {
           
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.FileName = "";
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "텍스트문서 (*.txt)|*.txt";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                proxyIpOpenPath = openFileDialog1.FileName;
                proxyIpPathTextBox.Text = openFileDialog1.FileName;
            }
        }
    }
}
