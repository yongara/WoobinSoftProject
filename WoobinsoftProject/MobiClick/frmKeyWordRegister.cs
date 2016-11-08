using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MobiClick
{
    public partial class frmKeyWordRegister : Form
    {
        private string _keyWord;
        private string _url;
        private int _maxPage;
        private int _delay_st;
        private int _delay_et;
        private string _keyCode;
        private bool _isEdit;   

        public bool IsEdit
        {
            set
            {
                _isEdit = value;
            }
        }
       
        public string KeyCode
        {
            set
            {
                _keyCode = value;
            }

        }
        public string KeyWord
        {
            get
            {
                return _keyWord;
            }
            set
            {
                _keyWord = value;
            }
        }

        public string URL
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
            }
        }

        public int MaxPage
        {
            get
            {
                return _maxPage;
            }
            set
            {
                _maxPage = value;
            }
        }
        public int DelayST
        {
            get
            {
                return _delay_st;
            }
            set
            {
                _delay_st = value;
            }
        }
        public int DelayEt
        {
            get
            {
                return _delay_et;
            }
            set
            {
                _delay_et = value;
            }
        }
      
        public frmKeyWordRegister()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            if (_isEdit == true)
            {
                keyWordTextBox.Text = _keyWord;
                urlTextBox.Text = _url;
                maxSearchPageComboBox.SelectedItem = _maxPage;
                delayTimeStartTextBox.Text = _delay_st.ToString();
                delayTimeEndTextBox.Text = _delay_et.ToString();              

                addButton.Text = "수정";
            }
            else
            {
                keyWordTextBox.Text = "";
                urlTextBox.Text = "";
                maxSearchPageComboBox.SelectedItem = "14";
                delayTimeStartTextBox.Text = "20";
                delayTimeEndTextBox.Text ="60";               
            }
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            _keyWord = keyWordTextBox.Text;
            _url = urlTextBox.Text;
            _maxPage = Convert.ToInt32(maxSearchPageComboBox.SelectedItem);
            _delay_st = Convert.ToInt32(delayTimeStartTextBox.Text);
            _delay_et = Convert.ToInt32(delayTimeEndTextBox.Text);

            string resultCode = Common.InsertKeyWordInfo(_keyCode, _keyWord, _url, _maxPage, _delay_st, _delay_et);
            string message = string.Empty;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            switch (resultCode)
            {
                case "001": message = "키워드 추가에 성공했습니다."; icon = MessageBoxIcon.Information; break;
                case "002": message = "키워드 수정에 성공했습니다"; icon = MessageBoxIcon.Information; break;
                case "003": message = "키워드 허용건수를 초과했습니다.\r\n기존 키워드를 삭제후 다시 추가해주세요!"; icon = MessageBoxIcon.Warning; break;
                case "004": message = "키워드 추가중 에러가 발생했습니다.\r\n관리자에게 문의해 주세요!"; icon = MessageBoxIcon.Error; break;
                default: message = "키워드 추가에 성공했습니다"; icon = MessageBoxIcon.Information; break;
            }

            MessageBox.Show(message, "", MessageBoxButtons.OK, icon);        

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {         
            this.Close();
        }

        private void addKeywordFromFilebutton_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                DataTable dt = new DataTable();
                dt.Columns.Add("키워드");             
                dt.Columns.Add("URL");
              
                using (StreamReader reader = new StreamReader(filePath,Encoding.Default))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] arr = line.Split(',');
                        if (arr.Length !=2) continue;
                        //데이터를 추가합니다.
                        dt.LoadDataRow(arr, true);
                    }
                }

                //추가할 데이터를 확인합니다.
                frmKeywrodBulk frm = new frmKeywrodBulk();
                frm.KeyWordData = dt;
                frm.ShowDialog();

                if (frm.IsSuccess == true)
                {
                    string resultCode = "000";
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string keyword = dr[0].ToString();
                            string url = dr[1].ToString();                         
                            int maxPage = 14;
                            int delaySt = 20;
                            int delayEt = 60;

                            resultCode = Common.InsertKeyWordInfo(_keyCode, keyword, url, maxPage, delaySt, delayEt);
                            if (resultCode == "003" || resultCode == "004") break;                           
                        }

                        string message = string.Empty;
                        MessageBoxIcon icon = MessageBoxIcon.Information;
                        switch (resultCode)
                        {
                            case "001": message = "키워드 추가에 성공했습니다."; icon = MessageBoxIcon.Information; break;
                            case "002": message = "키워드 수정에 성공했습니다"; icon = MessageBoxIcon.Information; break;
                            case "003": message = "키워드 허용건수를 초과했습니다.\r\n기존 키워드를 삭제후 다시 추가해주세요!"; icon = MessageBoxIcon.Warning; break;
                            case "004": message = "키워드 추가중 에러가 발생했습니다.\r\n관리자에게 문의해 주세요!"; icon = MessageBoxIcon.Error; break;
                            default: message = "키워드 추가에 성공했습니다"; icon = MessageBoxIcon.Information; break;
                        }

                        MessageBox.Show(message, "", MessageBoxButtons.OK, icon);     
                    }

                    this.Close();
                }
            }
        }
    }
}
