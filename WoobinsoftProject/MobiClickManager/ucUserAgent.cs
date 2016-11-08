using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MobiClickManager
{
    public partial class ucUserAgent : UserControl
    {
        public ucUserAgent()
        {
            InitializeComponent();
        }

        private void regButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("등록하시겠습니까?", "등록", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                string userAgent = userAgentTextBox.Text;
                Common.InsertUserAgent(userAgent);
            }
            userAgentDataGridView.DataSource = Common.UserAgentList();
            userAgentTextBox.Text = userAgentDataGridView.Rows[0].Cells[0].Value.ToString();      
        }
       
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("삭제하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                string userAgent = userAgentTextBox.Text;
                Common.DeleteUserAgent(userAgent);
            }
            userAgentDataGridView.DataSource = Common.UserAgentList();
            userAgentTextBox.Text = userAgentDataGridView.Rows[0].Cells[0].Value.ToString();      
        }

        private void ucUserAgent_Load(object sender, EventArgs e)
        {
            userAgentDataGridView.DataSource = Common.UserAgentList();
            userAgentTextBox.Text = userAgentDataGridView.Rows[0].Cells[0].Value.ToString();      
        }

        private void userAgentDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RowSelected();
        }

        private void RowSelected()
        {
            userAgentTextBox.Text = userAgentDataGridView.SelectedRows[0].Cells[0].Value.ToString();           
        }

    }
}
