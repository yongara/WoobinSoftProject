using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Data.Common;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Threading;


namespace MobiClickAutoUpGrade
{
    public partial class frmMain : Form
    {
        private Queue<string> _downloadUrls = new Queue<string>();
        private string connectionString = string.Format("server={0};Initial Catalog={1};Integrated Security=false;UID={2};PWD={3};Network Library=DBMSSOCN;", "woobinsoft.cafe24.com", "woobinsoft", "woobinsoft", "tkagowjs1!");
        private Dictionary<string, string> _dicServerDownloadList = new Dictionary<string, string>();
        private string _server = "woobinsoft.cafe24.com";
        private string _port = "5721";
        private string _userid = "woobinsoft";
        private string _password = "tkagowjs1!";
        private IniFileHandler iniHandler = new IniFileHandler();
        private bool _downloadComplete = false;
        private string labeltext = "";


        private string _backUpFolder = AppDomain.CurrentDomain.BaseDirectory + "BackupFolder";

        public frmMain()
        {
            InitializeComponent();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            bool isSuccess = true;
            ProcessKill();

            if (Directory.Exists(_backUpFolder) == false)
            {
                Directory.CreateDirectory(_backUpFolder);
            }
            string serverVersion = "1900000000";
            string clientVersion = "1900000000";
            try
            {
                //서버의 다운로드 파일의 버전을 가져옵니다.
                GetUpGradeList(ref serverVersion);

                clientVersion = iniHandler.GetIniValue("VERSION", "CLIENT");

                int iServerVersion = 0;
                int iClientVersion = 0;
                int.TryParse(serverVersion, out iServerVersion);
                int.TryParse(clientVersion, out iClientVersion);

                if (iServerVersion > iClientVersion)
                {
                    string currentBackupFolder = _backUpFolder + "\\" + DateTime.Now.ToString("yyyyMMdd");
                    string oldIniFile = AppDomain.CurrentDomain.BaseDirectory + "ClientFileList.ini";

                    if (Directory.Exists(currentBackupFolder) == false)
                    {
                        Directory.CreateDirectory(currentBackupFolder);
                    }
                    else
                    {
                        if (Directory.GetFiles(currentBackupFolder).Length > 0)
                        {
                            foreach (string fileName in Directory.GetFiles(currentBackupFolder))
                            {
                                File.Delete(fileName);
                            }
                        }
                    }

                    //ini 파일을 백업 합니다.
                    File.Copy(oldIniFile, currentBackupFolder + "\\ClientFileList.ini", true);

                    //client 의 서버버전을 업데이트 합니다.
                    iniHandler.SetIniValue("VERSION", "CLIENT", serverVersion);

                    List<string> downloadList = new List<string>();
                    foreach (KeyValuePair<string, string> keyPair in _dicServerDownloadList)
                    {
                        string oriFile = keyPair.Key;
                        //파일의 개별 버전이 동일하지 않은 경우에만 추가 합니다.
                        if (iniHandler.GetIniValue("FILELIST", oriFile) != keyPair.Value)
                        {                           
                            string sourceFile = AppDomain.CurrentDomain.BaseDirectory + oriFile;
                            string targetFile = currentBackupFolder + "\\" + oriFile;
                            File.Copy(sourceFile, targetFile, true);

                            string originalString = string.Format("ftp://{0}:{1}/{2}", _server, _port, oriFile);
                            downloadList.Add(originalString);

                            iniHandler.SetIniValue("FILELIST", oriFile, keyPair.Value);
                        }
                    }

                    if (downloadList.Count > 0)
                    {
                        downloadFile(downloadList);
                    }
                    else
                    {
                        MessageBox.Show("업데이트파일이 존재하지 않습니다.\r\n모비클릭프로그램을 재실행합니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OpenMobiClick();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("버전이 동일합니다.\r\n모비클릭프로그램을 재실행합니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OpenMobiClick();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                FileResore();
                isSuccess = false;
                WriteTextLog("frmMain_Load", ex.Message.ToString());
                MessageBox.Show("업그레이드를 실패했습니다.\r\n모비클릭프로그램을 재실행합니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OpenMobiClick();
                this.Close();
            }            
        }

        private void downloadFile(IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                _downloadUrls.Enqueue(url);
            }

            updateProgressBar.Visible = true;
            lblStatus.Visible = true;

            DownloadFile(_downloadUrls.Dequeue());
        }

        private void DownloadFile(string url)
        {
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/7.0;)");
            client.Credentials = new NetworkCredential(_userid, _password);
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;

            //var url = _downloadUrls.Dequeue();
            string FileName = url.Substring(url.LastIndexOf("/") + 1, (url.Length - url.LastIndexOf("/") - 1));
            client.DownloadFileAsync(new Uri(url), AppDomain.CurrentDomain.BaseDirectory + "\\" + FileName);
            labeltext = FileName;
            //lblStatus.Text = FileName;
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    throw e.Error;
                }

                if (e.Cancelled)
                {
                    //MessageBox.Show(e.UserState.ToString());
                    throw e.Error;
                }

                if (_downloadUrls.Any())
                {
                    DownloadFile(_downloadUrls.Dequeue());
                }
                else
                {
                    MessageBox.Show("업그레이드가 완료되었습니다.\r\n모비클릭프로그램을 재실행합니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OpenMobiClick();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                WriteTextLog("client_DownloadFileCompleted", ex.Message.ToString());
                MessageBox.Show("Error : "+ex.Message.ToString());
                this.Close();
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            updateProgressBar.Value = e.ProgressPercentage;
            lblStatus.Text = labeltext + " 파일 [" + e.ProgressPercentage.ToString() + "% ] 진행중..";             
        }

        private void GetUpGradeList(ref string serverVersion)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();            
            try
            {             
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "EXEC SP_UPDATEFILE_INFO";

                    // SqlDataAdapter 초기화
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                    // Fill 메서드 실행하여 결과 DataSet을 리턴받음
                    adapter.Fill(ds);

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (i == 0) serverVersion = dr["VERSION"].ToString();
                                _dicServerDownloadList.Add(dr["FILENAME"].ToString(), dr["FILEVERSION"].ToString());
                                i++;
                            }
                        }
                    }
                }              

            }
            catch (Exception ex)
            {
                WriteTextLog("GetUpGradeList", ex.Message.ToString());
            }                 
        }

        #region WriteTextLog - 로그를 작성합니다.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public static void WriteTextLog(string type, string message)
        {
            try
            {       
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log\\";

                if (Directory.Exists(filePath) == false)
                {
                    Directory.CreateDirectory(filePath);
                }

                filePath += string.Format("[{0}].txt", DateTime.UtcNow.AddHours(9).ToLongDateString());

                StreamWriter output = new StreamWriter(filePath, true, Encoding.Default);
                output.WriteLine(string.Format("[ {0} : {1} ]" + type, DateTime.Now.ToLongTimeString(), DateTime.Now.Millisecond.ToString()));
                output.WriteLine(message);
                output.Close();
                output = null;

            }
            catch { }
        }
        #endregion
        
        private void OpenMobiClick()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName =AppDomain.CurrentDomain.BaseDirectory+ "\\MobiClick.exe";           
            Process.Start(startInfo);
        }

        private void FileResore()
        {
            string oldFilePath = _backUpFolder + "\\" + DateTime.Now.ToString("yyyyMMdd");
            string[] sfiles = Directory.GetFiles(oldFilePath);
            foreach (string sfile in sfiles)
            {
                FileInfo file = new FileInfo(sfile);
                file.CopyTo(file.Name, true);
            }
        }

        #region ProcessKill - 프로세서를 삭제합니다.
        /// <summary>
        /// 프로세서를 삭제합니다.
        /// </summary>
        private void ProcessKill()
        {
            try
            {
                Process[] arrProces = Process.GetProcessesByName("MobiClick");
                if (arrProces.GetLength(0) > 0)
                {
                    foreach (Process p in arrProces)
                    {
                        p.Kill();
                    }
                }
            }
            catch { }
        }
        #endregion

    }
}
