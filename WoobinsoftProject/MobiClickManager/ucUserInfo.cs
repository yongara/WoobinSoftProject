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
    public partial class ucUserInfo : UserControl
    {
        public ucUserInfo()
        {
            InitializeComponent();
        }

        
        private void GeUserInfoToSettingGrid()
        {

            userInfoDataGridView.DataSource =  Common.GetUserList();
            
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("수정하시겠습니까?", "수정", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                string userid = useridTextBox.Text;
                string password = passwordTextBox.Text;
                string keycode = keyCodeTextBox.Text;

                Common.UpdateUserInfo(userid, password, keycode);
            }

            GeUserInfoToSettingGrid();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("삭제하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                string userid = useridTextBox.Text;              
                string keycode = keyCodeTextBox.Text;

                Common.DeleteUserInfo(userid,  keycode);
            }

            GeUserInfoToSettingGrid();
        }

        private void ucUserInfo_Load(object sender, EventArgs e)
        {
            GeUserInfoToSettingGrid();

            useridTextBox.Text = userInfoDataGridView.Rows[0].Cells[0].Value.ToString();
            passwordTextBox.Text = userInfoDataGridView.Rows[0].Cells[1].Value.ToString();
            keyCodeTextBox.Text = userInfoDataGridView.Rows[0].Cells[2].Value.ToString();
        }

        private void userInfoDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RowSelected();
        }

        private void RowSelected()
        {
            useridTextBox.Text = userInfoDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            passwordTextBox.Text = userInfoDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            keyCodeTextBox.Text = userInfoDataGridView.SelectedRows[0].Cells[2].Value.ToString();
        }







    }
}
