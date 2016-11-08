using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace MobiClick
{
    class FtpUtils
    {
        static string _server = "woobinsoft.cafe24.com";
        static string _port = "5721";
        static string _userid = "woobinsoft";
        static string _password = "tkagowjs1!";

        public static void DownloadFileByWebClient(string oriFile, string targetFile)
        {
            string originalString = string.Format("ftp://{0}:{1}/{2}", _server, _port, oriFile);
            WebClient wc = new WebClient();
            try
            {
                wc.Headers.Add("user-agent","Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/7.0;)");
                wc.Credentials = new NetworkCredential(_userid, _password);
                System.Uri uri = new Uri(originalString);
                //wc.DownloadFile(uri, targetFile);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompletedCallBack);
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallBack);
                wc.DownloadFileAsync(uri, targetFile);
               
                Thread.Sleep(3000);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                wc.Dispose();
            }
        }

        private static void DownloadProgressCallBack(object sender, DownloadProgressChangedEventArgs e)
        {
           
        }

        private static void DownloadFileCompletedCallBack(object sender, AsyncCompletedEventArgs e)
        {

        }        
    }
}
