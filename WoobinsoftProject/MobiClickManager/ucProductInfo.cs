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
    public partial class ucProductInfo : UserControl
    {
        private Common.SearchType _searchType = Common.SearchType.Total;
        private Common.SaleType _saleType = Common.SaleType.Total;
        private int _searchCount = 100;
        private int _createKeyCodeCount = 0;
        private Common.SaleType _createKeyCodeSaleType = Common.SaleType.Sale;

        public ucProductInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 제품 및 사용자 데이터를 수정합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, EventArgs e)
        {       
            
            if (DialogResult.OK == MessageBox.Show("수정하시겠습니까?", "수정", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                string userid = useridTextBox.Text;
                string pwd = userPasswordTextBox.Text;
                string keycode = keyCodeTextBox.Text;                
                string macAddr = macAddressTextBox.Text;
                string allowCnt = allowCountTextBox.Text;
                int allowCount = 0;
                Int32.TryParse(allowCnt, out allowCount);             
                string fromDt = fromDtTextBox.Text;
                fromDt = fromDt.Length>=10? fromDt.Substring(0,10):"";
                string toDt = toDtTextBox.Text;
                toDt = toDt.Length >= 10 ? toDt.Substring(0, 10) : "";
                string saleDate = saleDateTextBox.Text;
                string saleCode = string.IsNullOrEmpty(saleCodeTextBox.Text) == false && saleCodeTextBox.Text.Length>3?saleCodeTextBox.Text.Substring(0,3):string.Empty;
                string dealCode = dealCodeTextBox.Text;

                Common.UpdateProductInfo(userid,pwd, keycode, macAddr, allowCount, fromDt, toDt,saleCode,saleDate,dealCode);
            }

            GetSearchList();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("삭제하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                string keyCode = keyCodeTextBox.Text;
                Common.DeleteProductInfo(keyCode);
            }

            GetSearchList();
        }

        private void ucProductInfo_Load(object sender, EventArgs e)
        {
            searchTypeComboBox.SelectedIndex = 0;
            GetSearchList();
        }       

        private void productInfoDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RowSelected();
        }

        private void productInfoDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            RowSelected();
        }

        /// <summary>
        /// 그리드의 Row 선택시 처리합니다.
        /// </summary>
        private void RowSelected()
        {
            if (productInfoDataGridView.SelectedRows.Count > 0)
            {
                useridTextBox.Text = productInfoDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                userPasswordTextBox.Text = productInfoDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                keyCodeTextBox.Text = productInfoDataGridView.SelectedRows[0].Cells[2].Value.ToString();
                macAddressTextBox.Text = productInfoDataGridView.SelectedRows[0].Cells[3].Value.ToString();
                allowCountTextBox.Text = productInfoDataGridView.SelectedRows[0].Cells[4].Value.ToString();
                fromDtTextBox.Text = productInfoDataGridView.SelectedRows[0].Cells[5].Value.ToString();
                toDtTextBox.Text = productInfoDataGridView.SelectedRows[0].Cells[6].Value.ToString();
                saleCodeTextBox.Text = productInfoDataGridView.SelectedRows[0].Cells[8].Value.ToString();
                saleDateTextBox.Text = productInfoDataGridView.SelectedRows[0].Cells[9].Value.ToString();
                dealCodeTextBox.Text = productInfoDataGridView.SelectedRows[0].Cells[10].Value.ToString();
                productNumberTextBox.Text = productInfoDataGridView.SelectedRows[0].Cells[11].Value.ToString();
            }
            else
            {
                useridTextBox.Text = string.Empty;
                userPasswordTextBox.Text = string.Empty;
                keyCodeTextBox.Text = string.Empty;
                macAddressTextBox.Text = string.Empty;
                allowCountTextBox.Text = string.Empty;
                fromDtTextBox.Text = string.Empty;
                toDtTextBox.Text = string.Empty;
                saleCodeTextBox.Text = string.Empty;
                saleDateTextBox.Text = string.Empty;
                dealCodeTextBox.Text = string.Empty;
                productNumberTextBox.Text = string.Empty;
            }
        }

        

        private void searchTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int searchIndex = searchTypeComboBox.SelectedIndex;
            switch (searchIndex)
            {
                case 0: _searchType = Common.SearchType.Total; break;
                case 1: _searchType = Common.SearchType.UserId; break;
                case 2: _searchType = Common.SearchType.KeyCode; break;
                default: _searchType = Common.SearchType.Total; break;
            }
        }
      
        private void searchKeywordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode ==  Keys.Enter)
            {
                GetSearchList();
            }
        }

        private void GetSearchList()
        {
            string keywordText = string.IsNullOrEmpty(searchKeywordTextBox.Text) == true ? string.Empty : searchKeywordTextBox.Text;
            productInfoDataGridView.DataSource = Common.GetProductList(_saleType, _searchType, keywordText,_searchCount.ToString());

            if (productInfoDataGridView.Rows.Count > 0)
            {
                useridTextBox.Text = productInfoDataGridView.Rows[0].Cells[0].Value.ToString();
                userPasswordTextBox.Text = productInfoDataGridView.Rows[0].Cells[1].Value.ToString();
                keyCodeTextBox.Text = productInfoDataGridView.Rows[0].Cells[2].Value.ToString();
                macAddressTextBox.Text = productInfoDataGridView.Rows[0].Cells[3].Value.ToString();
                allowCountTextBox.Text = productInfoDataGridView.Rows[0].Cells[4].Value.ToString();
                fromDtTextBox.Text = productInfoDataGridView.Rows[0].Cells[5].Value.ToString();
                toDtTextBox.Text = productInfoDataGridView.Rows[0].Cells[6].Value.ToString();
                saleCodeTextBox.Text = productInfoDataGridView.Rows[0].Cells[8].Value.ToString();
                saleDateTextBox.Text = productInfoDataGridView.Rows[0].Cells[9].Value.ToString();
                dealCodeTextBox.Text = productInfoDataGridView.Rows[0].Cells[10].Value.ToString();
                productNumberTextBox.Text = productInfoDataGridView.Rows[0].Cells[11].Value.ToString();
            }
            else
            {
                useridTextBox.Text = string.Empty;
                userPasswordTextBox.Text = string.Empty;
                keyCodeTextBox.Text = string.Empty;
                macAddressTextBox.Text = string.Empty;
                allowCountTextBox.Text = string.Empty;
                fromDtTextBox.Text = string.Empty;
                toDtTextBox.Text = string.Empty;
                saleCodeTextBox.Text = string.Empty;
                saleDateTextBox.Text = string.Empty;
                dealCodeTextBox.Text = string.Empty;
                productNumberTextBox.Text = string.Empty;
            }
        }

        private void totalSaleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _saleType = Common.SaleType.Total;
            GetSearchList();
        }

        private void saleTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _saleType = Common.SaleType.Sale;
            GetSearchList();
        }

        private void testTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _saleType = Common.SaleType.Test;
            GetSearchList();
        }

        private void etcTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _saleType = Common.SaleType.Etc;
            GetSearchList();
        }

        private void searchExecuteButton_Click(object sender, EventArgs e)
        {
            GetSearchList();
        }

        private void searchCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _searchCount = Convert.ToInt32(searchCountNumericUpDown.Value);
        }

        /// <summary>
        /// 제품코드를 추가합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createKeyCodeEcecuteButton_Click(object sender, EventArgs e)
        {           
          int result =  Common.CreateProductINfo(_createKeyCodeCount, _createKeyCodeSaleType);

          if (result > 0)
          {
              MessageBox.Show(string.Format("제품코드가 {0}건 생성되었습니다.",result.ToString()));
          }
          else
          {
              MessageBox.Show("생성된 제품코드가 없습니다!!!");
          }
        }

        private void createKeyCodeNewCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _createKeyCodeCount = Convert.ToInt32(createKeyCodeNewCountNumericUpDown.Value);
        }

        private void createCodeSaleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _createKeyCodeSaleType = Common.SaleType.Sale;
        }

        private void createCodeTestRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _createKeyCodeSaleType = Common.SaleType.Test;
        }

    }
}
