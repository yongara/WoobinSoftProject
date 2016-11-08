using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace MobiClick
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class frmWebBrowser : Form
    {
        public frmWebBrowser()
        {
            InitializeComponent();
        }

        private void frmWebBrowser_Load(object sender, EventArgs e)
        {
            naverWebBrowser.ObjectForScripting = false;
            naverWebBrowser.Navigate("m.naver.com");


           

        }

        private void ExecJavascript(string sValue1, string sValue2)
        {
            try
            {
                naverWebBrowser.Document.InvokeScript("CallScript", new object[] { sValue1, sValue2 });
            }
            catch
            {
            }
        }


        public void CallForm(object msg)
        {
            string sMsg = (string)msg;
            {
                // 받은 msg 값을 가지고 처리하는 로직.

            }
        }

        /*  Web javascript 를 설정한다.
        function CallScrript(va1, va2)
        {
	        alert('Val1 : ' + val1 + " / Val2 : ' + val2);
        }
         * 
         * */




    }
}
