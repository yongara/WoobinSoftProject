using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MobiClickManager
{
    public partial class frmManagerMain : Form
    {
        public frmManagerMain()
        {
            InitializeComponent();

            ucProductInfo productInfo = new ucProductInfo();
            productInfo.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(productInfo);
        }        

        private void productToolStripButton_Click(object sender, EventArgs e)
        {         
            ucProductInfo productInfo = new ucProductInfo();
            productInfo.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(productInfo);
        }      

        private void userAgentToolStripButton_Click(object sender, EventArgs e)
        {           
            ucUserAgent userAgent = new ucUserAgent();
            userAgent.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(userAgent);
        }
    }
}
