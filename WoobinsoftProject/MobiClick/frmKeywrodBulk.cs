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
    public partial class frmKeywrodBulk : Form
    {
        public DataTable KeyWordData { get; set; }
        public bool IsSuccess { get; set; }

        public frmKeywrodBulk()
        {
            InitializeComponent();          
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("선택된 키워드가 추가됩니다.");
            IsSuccess = true;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            IsSuccess = false;
            this.Close();
        }

        private void frmKeywrodBulk_Load(object sender, EventArgs e)
        {
            keywordBulkInsertDataGridView.DataSource = KeyWordData; 
        }
    }
}
