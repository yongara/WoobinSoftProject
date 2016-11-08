using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Microsoft.Win32;

namespace MobiClick
{
    public partial class frmMain : Form
    {

        #region  Thread 객체
        Thread work; //메인작업 수행
        //Thread proxyWork;//Proxy 관련 작업 수행
        //Thread ieDriverWork;//IE 드라이버 관련 수행
        Thread rankWork;
        #endregion

        #region 전역변수

        List<Common.KeyWordDataEntity> _loopData = new List<Common.KeyWordDataEntity>();
        Dictionary<string, int> _dicRankList = new Dictionary<string, int>();
        Common.UserInfo userInfo;
        bool _isJobStart = false;
        string _lastJobIP = string.Empty;
        string _lastJobDate = string.Empty;
        int loopTime = 0;
        //int ipChangeTime = 0;
        //int rankUpdateTime = 0;
        //int processMinimizeTime = 0;
        Common.RegistryMode _ieExecuteMode = Common.RegistryMode.WoW64;
        List<string> userAgentList = new List<string>();
        private IniFileHandler iniHandler = new IniFileHandler();

        private bool isIEDriverHide = false;
        private bool isKeywordRandom = true;
        private bool isProxyTimeBase = false;//5분단위 proxy IP 변경 수행 loop 수행 실행
        private int proxyDelaySecond = 0;
        private int ipDuppleAllowCount = 3;
        private int proxyIpTryConnectCount = 3;

        #region WIN32 API 선언
        [DllImport("User32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //[DllImport("user32.dll")]
        //public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern void SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr SetFocus(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        // P/Invoke declarations
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pt);

        //메세지보내기
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        //입력제어
        //[DllImport("user32.dll")]
        //private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        // 커서 위치 설정하기
        [DllImport("user32.dll")]
        private static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        const short SWP_NOMOVE = 0X2;
        const short SWP_NOSIZE = 1;
        const short SWP_NOZORDER = 0X4;
        const int SWP_SHOWWINDOW = 0x0040;

        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public ShowWindowCommands showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        public enum ShowWindowCommands : int
        {
            Hide = 0,
            Normal = 1,
            Minimized = 2,
            Maximized = 3,
        }

        public enum WNDSTATE : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_MAX = 10
        }


        //private const int WM_MOUSEWHEEL = 0x20a;
        //private const int WM_MOUSEMOVE = 0x201;
        //private const int WM_LBUTTONDOWN = 0x202;
        //private const int WM_LBUTTONUP = 0x204;

        [Flags]
        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002, // 왼쪽 마우스 버튼 눌림
            LEFTUP = 0x00000004, // 왼쪽 마우스 버튼 떼어짐
            MIDDLEDOWN = 0x00000020, // 휠 버튼 눌림
            MIDDLEUP = 0x00000040,// 휠 버튼 떼어짐
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008, // 오른쪽 마우스 버튼 눌림
            RIGHTUP = 0x00000010,// 오른쪽 마우스 버튼 떼어짐
            WHEEL = 0x00000800,//휠 스크롤
            XDOWN = 0x00000080,
            XUP = 0x00000100
        }

        //Use the values of this enum for the 'dwData' parameter
        //to specify an X button when using MouseEventFlags.XDOWN or
        //MouseEventFlags.XUP for the dwFlags parameter.
        public enum MouseEventDataXButtons : uint
        {
            XBUTTON1 = 0x00000001,
            XBUTTON2 = 0x00000002
        }
        #endregion

        #endregion

        #region CallBack Delegate 함수
        delegate void CallBack(object obj);

        //CrossThreadHandleException 을 회피하기 위한 처리입니다. 
        delegate void CallBackGridDelegate(DataGridView dgv, DataTable dt);
        private void CallBackGrid(DataGridView dgv, DataTable dt)
        {
            if (dgv.InvokeRequired)
            {
                dgv.Invoke(new CallBackGridDelegate(CallBackGrid), new object[] { dgv, dt });
            }
            else
            {
                dgv.DataSource = dt;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgv.Update();
            }
        }

        //CrossThreadHandleException 을 회피하기 위한 처리입니다. 
        delegate void CallBackRichTextBoxIPDelegate(RichTextBox textBox);
        private void CallBackRichTextBoxIP(RichTextBox textBox)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new CallBackRichTextBoxIPDelegate(CallBackRichTextBoxIP), new object[] { textBox });
            }
            else
            {
                textBox.AppendText(string.Format("[{0}]  :  {1}\r\n", _lastJobIP, _lastJobDate));
            }
        }

        //CrossThreadHandleException 을 회피하기 위한 처리입니다. 
        delegate void CallBackRichTextBoxDelegate(RichTextBox textBox, string value);
        private void CallBackRichTextBox(RichTextBox textBox, string value)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new CallBackRichTextBoxDelegate(CallBackRichTextBox), new object[] { textBox, value });
            }
            else
            {
                textBox.AppendText(string.Format("{0}", value));
            }
        }

        //CrossThreadHandleException 을 회피하기 위한 처리입니다. 
        delegate void CallBackTextBoxDelegate(TextBox textBox, string value);
        private void CallBackTextBox(TextBox textBox, string value)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new CallBackTextBoxDelegate(CallBackTextBox), new object[] { textBox, value });
            }
            else
            {
                textBox.Text = string.Format("[{0}]", value);
            }
        }

        delegate void CallBackDisplayLabelDelegate(Label lbl, string value);
        private void CallBackDisplayLabel(Label lbl, string value)
        {
            if (lbl.InvokeRequired)
            {
                lbl.Invoke(new CallBackDisplayLabelDelegate(CallBackDisplayLabel), new object[] { lbl, value });
            }
            else
            {
                lbl.Text = value;
            }
        }
        delegate void CallBackButtonDelegate(Button button, string value);
        private void CallBackButton(Button button, string value)
        {
            if (button.InvokeRequired)
            {
                button.Invoke(new CallBackButtonDelegate(CallBackButton), new object[] { button, value });
            }
            else
            {
                button.Text = value;
            }
        }
        delegate void CallBackProgressBarDelegate(ProgressBar button, int value);
        private void CallBackProgressBar(ProgressBar progress, int value)
        {
            if (progress.InvokeRequired)
            {
                progress.Invoke(new CallBackProgressBarDelegate(CallBackProgressBar), new object[] { progress, value });
            }
            else
            {
                progress.Value = value;
            }
        }
        #endregion

        #region 생성자
        public frmMain()
        {
            InitializeComponent();
            userInfo = new Common.UserInfo();
        }
        #endregion

        #region AutoJob_BackGroundWorker - 웹페이지 검색 작업을 수행합니다.
        /// <summary>
        /// 자동검색 작업을 실행합니다.
        /// </summary>
        private void AutoJob_BackGroundWorker()
        {
            //프록시IP는 먼저 변경후 시작합니다.
            if (Common.IsProxy == true)
            {
                ProxyHandler.ProxyIpListSetting();
                int cnt = Common.dicProxyList.Count;
                if (cnt > 0)
                {
                    int k;
                    bool isHasConnection = false;
                    for (k = 1; k <= proxyIpTryConnectCount; k++)
                    {
                        int key = Common.RandomKeyValue(1, cnt);
                        string errMessage = string.Empty;
                        WinINetUtils.ChangeIEProxyServer(Common.dicProxyList[key], ref errMessage);
                        if (string.IsNullOrEmpty(errMessage) == false)
                        {
                            string message = "프록시 아이피 변경 실패(관리자에게 문의하세요)-->" + errMessage;
                            JobStateChange(true, message);
                            break;
                        }
                        else
                        {
                            CallBackDisplayLabel(delayTimeModLabel, string.Format("프록시IP[{0}] {1}회 연결시도중...", Common.dicProxyList[key], k.ToString()));
                            Common.WriteTextLog("프록시IP 연결시도", string.Format("프록시IP[{0}] {1}회 연결시도", Common.dicProxyList[key], k.ToString()));
                            isHasConnection = Common.HasConnection();
                            if (isHasConnection == true)
                            {
                                break;
                            }
                        }
                    }

                    if (isHasConnection == false)
                    {
                        string message = string.Format("프록시IP를 {0}번 변경했으나 IP 불량으로  인터넷연결이 지연되고 있습니다.", proxyIpTryConnectCount.ToString());
                        JobStateChange(true, message);
                    }
                }
            }

            string beforeIP = Common.GetExternalIp();
            int ipRunCnt = 0;

            while (_isJobStart == true)
            {
                if (_loopData.Count > 0)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(Common.GetMacAddress()) == false && userInfo.MacAddress != Common.GetMacAddress())
                        {
                            string message = "Mac주소가 동일하지 않습니다.Mac변경버튼을 클릭하세요!";
                            JobStateChange(true, message);
                            break;
                        }
                       
                        //종료일이 지나면 자동으로 사용을 중지합니다.
                        DateTime endDate = DateTime.Now;
                        DateTime.TryParse(userInfo.ToDt, out endDate);
                        //현재일자의 기준시간을 설정합니다.
                        DateTime baseTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                        if (endDate < baseTime)
                        {
                            string message = string.Format("사용기간이 종료되었습니다.종료일자는 {0} 입니다!", userInfo.ToDt);
                            JobStateChange(true, message);
                            break;
                        }

                        //동일한 IP 로 3회 이상 진행시에는 경고창과 함께 종료합니다.
                        if (beforeIP == Common.GetExternalIp())
                        {
                            if (ipRunCnt >= ipDuppleAllowCount)
                            {
                                string message = string.Format("동일IP 로 {0}사이클이 실행되었습니다.포스팅안전을 위해 작업을 중지합니다.IP:{1}", ipDuppleAllowCount.ToString(), Common.GetExternalIp());
                                JobStateChange(true, message);
                                break;
                            }
                            else
                            {
                                ipRunCnt++;
                            }
                        }
                        else
                        {
                            ipRunCnt = 1;
                            beforeIP = Common.GetExternalIp();
                        }

                        int valueTerm = 100 / _loopData.Count;
                        int progressValue = 0;
                        CallBackProgressBar(progressBar1, progressValue);

                        if (isKeywordRandom == true)
                        {
                            //loop 데이터를  random 하게 섞습니다.
                            _loopData = Common.KeyWordMix(_loopData);
                        }
                        //작업을 실행합니다.
                        foreach (Common.KeyWordDataEntity entity in _loopData)
                        {
                            //작업을 시작합니다.
                            InternetExplorerOptions option = new InternetExplorerOptions();
                            option.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                            option.IgnoreZoomLevel = true;
                            option.EnsureCleanSession = true;
                            isIEDriverHide = true;
                            var driver = new InternetExplorerDriver(option);

                            try
                            {
                                CallBackDisplayLabel(delayTimeModLabel, entity.URL);
                                //naver 페이지를 호출합니다.
                                driver.Navigate().GoToUrl("m.naver.com");

                                isIEDriverHide = false;
                                //((IJavascriptExecutor)driver).executeScript("window.resizeTo(1024, 768);");
                                //driver.ExecuteScript("window.resizeTo(360, 600)");                               
                                SetActivateWindow();

                                // 검색어 필드를 찾습니다.
                                var searchField = driver.FindElementById("query");

                                //검색필드에 검색어를 보냅니다.
                                searchField.SendKeys(entity.KeyWord);
                                searchField.Click();

                                //검색버튼을 클릭합니다.
                                var searchButton = driver.FindElementByClassName("sch_submit");
                                //searchButton.Click();
                                searchButton.Submit();

                                loopTime = 0;
                                SetActivateWindow();
                                while (loopTime <= entity.PageByLoopTime)
                                {  //검색된 페이지를 스크롤합니다.                                    
                                    MouseWheelScrolling(Common.RandomKeyValue(-120, -1));
                                    Thread.Sleep(entity.ThreadTime);
                                }
                                MouseWheelUpScrolling();
                                loopTime = 0;
                                string paramSearch = "x_fsn=cali:1";
                                try
                                {
                                    var hrefLinkElement = driver.FindElementByXPath(string.Format("//a[contains(@href,'{0}')]", "x_fsn=cali:"));
                                    string paramHref = hrefLinkElement.GetAttribute("href");

                                    string[] arrayParam = paramHref.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (arrayParam.Length > 0)
                                    {
                                        foreach (string str in arrayParam)
                                        {
                                            if (str.Contains("x_fsn=cali:") == true)
                                            {
                                                paramSearch = str;
                                                break;
                                            }
                                        }
                                    }
                                }
                                catch { }

                                //두번째 페이지의 시작 아이템을 확인하는 로직입니다.
                                string startLinkValue = string.Format("page=2&{0}&start=11&display=15", paramSearch);
                                int startItemValue = 6;
                                try
                                {
                                    var startLinkElement = driver.FindElementByXPath(string.Format("//a[contains(@href,'{0}')]", startLinkValue));
                                    startItemValue = 11;
                                }
                                catch { startItemValue = 6; }
                              
                                List<string> searchPageList = new List<string>();
                                //검색할 페이지 데이터를 설정합니다.
                                for (int i = 0; i < entity.MaxPage; i++)
                                {
                                    string addValue = string.Format("page={0}&{1}&start={2}&display=15", (i + 2).ToString(), i == 0 ? paramSearch : string.Empty, (i * 15 + 11).ToString());
                                    searchPageList.Add(addValue);
                                    //searchPageList.Add(addValue);
                                    //string addValue = string.Format("page={0}&start={1}&display=15", (i + 2).ToString(), (i * 15 + 11).ToString());                                   
                                }

                                int page = 0;

                                bool isSearchSuccess = false;
                                //해당 페이지에서 대상 URL 을 찾을 때까지 검색합니다.
                                for (page = 0; page < entity.MaxPage; page++)
                                {
                                    try
                                    {
                                        var linkElement = driver.FindElementByXPath(string.Format("//a[contains(@href,'{0}')]", entity.URL));
                                        //순위를 처리합니다.
                                        string rankId = linkElement.FindElement(By.XPath("..")).GetAttribute("id");

                                        try
                                        {
                                            string[] arr = rankId.Split('_');
                                            if (arr.Length > 1)
                                            {
                                                int rank = 0;
                                                int.TryParse(arr[1], out rank);
                                                if (rank > 0)
                                                {
                                                    string rankKey = string.Format("{0}||{1}", entity.KeyWord, entity.URL);
                                                    if (_dicRankList.ContainsKey(rankKey) == true)
                                                    {
                                                        //key 를 삭제합니다.
                                                        _dicRankList.Remove(rankKey);
                                                    }
                                                    _dicRankList.Add(rankKey, rank);
                                                }
                                            }
                                        }
                                        catch { }
                                        //정상적으로 찾는경우 클릭실행
                                        linkElement.Click();
                                        isSearchSuccess = true;
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        #region 검색키워드
                                        //page=2&start=11&display=15&sm=mtb_nmr
                                        //page=1&start=1&display=10&sm=mtb_pge
                                        //page=2&start=11&display=15&sm=mtb_pge
                                        //page=3&start=26&display=15&sm=mtb_pge
                                        //page=4&start=41&display=15&sm=mtb_pge 
                                        #endregion

                                        //Common.WriteTextLog("FindElementByXPath", ex.Message.ToString());
                                        loopTime = 0;
                                        SetActivateWindow();
                                        while (loopTime <= entity.PageByLoopTime)
                                        {
                                            MouseWheelScrolling(Common.RandomKeyValue(-120, -1));
                                            Thread.Sleep(entity.ThreadTime);
                                        }
                                        MouseWheelUpScrolling();
                                        loopTime = 0;

                                        var nextElement = driver.FindElementByXPath(string.Format("//a[contains(@href,'{0}')]", searchPageList[page]));
                                        nextElement.Click();
                                    }
                                }

                                //정상적으로 검색했을 경우에만 처리합니다.
                                if (isSearchSuccess == true)
                                {
                                    //컨텐츠 페이지 체류시간을 설정합니다.
                                    int delayTime = Common.RandomKeyValue(entity.DelayTimeST, entity.DelayTimeET);
                                    loopTime = 0;
                                    SetActivateWindow();
                                    while (loopTime <= delayTime)
                                    {
                                        //driver.ExecuteScript(string.Format("window.scrollBy(0,{0})", Common.RandomKeyValue(1, 50)), "");
                                        MouseWheelScrolling(Common.RandomKeyValue(-120, -1), true);
                                        Thread.Sleep(entity.ThreadTime);
                                        DisplayDlayTimeLabelDisplay(Convert.ToInt32(delayTime), loopTime);
                                    }
                                    loopTime = 0;
                                }
                                //delayTime 의 메세지를 변경 합니다.
                                CallBackDisplayLabel(delayTimeModLabel, "");
                                driver.Quit();
                            }
                            catch { driver.Quit(); }
                            progressValue = progressValue + valueTerm;
                            CallBackProgressBar(progressBar1, progressValue);

                            if (_isJobStart == false) break;
                        }

                        CallBackProgressBar(progressBar1, 100);
                        //delayTime 의 메세지를 변경 합니다.
                        CallBackDisplayLabel(delayTimeModLabel, "다음주기 작업 준비중...");

                        //Loop 를 종료하면 IP 를 변경한다.
                        if (Common.IsProxy == true)
                        {
                            if (isProxyTimeBase == false)
                            {
                                //Loop 기준으로 IP 를 변경할때 사용합니다.
                                ProxyHandler.ProxyIpListSetting();
                                int cnt = Common.dicProxyList.Count;
                                if (cnt > 0)
                                {
                                    int k;
                                    bool isHasConnection = false;
                                    for (k = 1; k <= proxyIpTryConnectCount; k++)
                                    {
                                        int key = Common.RandomKeyValue(1, cnt);
                                        string errMessage = string.Empty;
                                        WinINetUtils.ChangeIEProxyServer(Common.dicProxyList[key], ref errMessage);
                                        if (string.IsNullOrEmpty(errMessage) == false)
                                        {
                                            string message = "프록시 아이피 변경 실패(관리자에게 문의하세요)-->" + errMessage;
                                            JobStateChange(true, message);
                                            break;
                                        }
                                        else
                                        {
                                            CallBackDisplayLabel(delayTimeModLabel, string.Format("프록시IP[{0}] {1}회 연결시도중...", Common.dicProxyList[key], k.ToString()));
                                            Common.WriteTextLog("프록시IP 연결시도", string.Format("프록시IP[{0}] {1}회 연결시도", Common.dicProxyList[key], k.ToString()));
                                            isHasConnection = Common.HasConnection();
                                            if (isHasConnection == true)
                                            {
                                                break;
                                            }
                                        }
                                    }

                                    if (isHasConnection == false)
                                    {
                                        string message = string.Format("프록시IP를 {0}번 변경했으나 IP 불량으로  인터넷연결이 지연되고 있습니다.", proxyIpTryConnectCount.ToString());
                                        JobStateChange(true, message);
                                    }
                                }
                                //Common.isProxyIPChangeByLoopBase = true;
                                Thread.Sleep(10000);
                            }
                        }
                        else
                        {
                            //Mobile 에서 IP 를 업데이트 하기 위해 DB 의  ipchange 키를 설정합니다.
                            Common.UpdateIPChangFlage(userInfo.UserID, "Y");
                            Thread.Sleep(10000);
                        }

                        //user agent 가  random 인 경우에만 처리합니다.
                        if (Common.isUserAgentRandomApply == true)
                        {
                            // useragent 를 변경할수 있도록 처리합니다.
                            UserAgentRandomApplyLoop();
                            Thread.Sleep(1000);
                        }

                        CallBackProgressBar(progressBar1, 0);
                    }
                    catch (Exception ex)
                    {
                        Common.WriteTextLog("AutoJob_BackGroundWorker", ex.Message.ToString());
                    }
                }
                else
                {
                    string message = "경고 : 키워드 건수가 존재하지 않습니다.키워드를 추가하세요!";
                    JobStateChange(true, message);
                    break;
                }
            }
        }
        #endregion

        #region AutoJob_BackGroundWorker2 - 웹페이지 검색 작업을 수행합니다.
        /// <summary>
        /// 자동검색 작업을 실행합니다.
        /// </summary>
        private void AutoJob_BackGroundWorker2()
        {
            //프록시IP는 먼저 변경후 시작합니다.
            if (Common.IsProxy == true)
            {
                ProxyHandler.ProxyIpListSetting();
                int cnt = Common.dicProxyList.Count;
                if (cnt > 0)
                {
                    int k;
                    bool isHasConnection = false;
                    for (k = 1; k <= proxyIpTryConnectCount; k++)
                    {
                        int key = Common.RandomKeyValue(1, cnt);
                        string errMessage = string.Empty;
                        WinINetUtils.ChangeIEProxyServer(Common.dicProxyList[key], ref errMessage);
                        if (string.IsNullOrEmpty(errMessage) == false)
                        {
                            string message = "프록시 아이피 변경 실패(관리자에게 문의하세요)-->" + errMessage;
                            JobStateChange(true, message);
                            break;
                        }
                        else
                        {
                            CallBackDisplayLabel(delayTimeModLabel, string.Format("프록시IP[{0}] {1}회 연결시도중...", Common.dicProxyList[key], k.ToString()));
                            Common.WriteTextLog("프록시IP 연결시도", string.Format("프록시IP[{0}] {1}회 연결시도", Common.dicProxyList[key], k.ToString()));
                            isHasConnection = Common.HasConnection();
                            if (isHasConnection == true)
                            {
                                break;
                            }
                        }
                    }

                    if (isHasConnection == false)
                    {
                        string message = string.Format("프록시IP를 {0}번 변경했으나 IP 불량으로  인터넷연결이 지연되고 있습니다.", proxyIpTryConnectCount.ToString());
                        JobStateChange(true, message);
                    }
                }
            }

            string beforeIP = Common.GetExternalIp();
            int ipRunCnt = 0;

            while (_isJobStart == true)
            {
                if (_loopData.Count > 0)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(Common.GetMacAddress()) == false && userInfo.MacAddress != Common.GetMacAddress())
                        {
                            string message = "Mac주소가 동일하지 않습니다.Mac변경버튼을 클릭하세요!";
                            JobStateChange(true, message);
                            break;
                        }

                        //종료일이 지나면 자동으로 사용을 중지합니다.
                        DateTime endDate = DateTime.Now;
                        DateTime.TryParse(userInfo.ToDt, out endDate);
                        //현재일자의 기준시간을 설정합니다.
                        DateTime baseTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                        if (endDate < baseTime)
                        {
                            string message = string.Format("사용기간이 종료되었습니다.종료일자는 {0} 입니다!", userInfo.ToDt);
                            JobStateChange(true, message);
                            break;
                        }

                        //동일한 IP 로 3회 이상 진행시에는 경고창과 함께 종료합니다.
                        if (beforeIP == Common.GetExternalIp())
                        {
                            if (ipRunCnt >= ipDuppleAllowCount)
                            {
                                string message = string.Format("동일IP 로 {0}사이클이 실행되었습니다.포스팅안전을 위해 작업을 중지합니다.IP:{1}", ipDuppleAllowCount.ToString(), Common.GetExternalIp());
                                JobStateChange(true, message);
                                break;
                            }
                            else
                            {
                                ipRunCnt++;
                            }
                        }
                        else
                        {
                            ipRunCnt = 1;
                            beforeIP = Common.GetExternalIp();
                        }

                        int valueTerm = 100 / _loopData.Count;
                        int progressValue = 0;
                        CallBackProgressBar(progressBar1, progressValue);
                        if (isKeywordRandom == true)
                        {
                            //loop 데이터를  random 하게 섞습니다.
                            _loopData = Common.KeyWordMix(_loopData);
                        }
                        foreach (Common.KeyWordDataEntity entity in _loopData)
                        {
                            //작업을 시작합니다.
                            InternetExplorerOptions option = new InternetExplorerOptions();
                            option.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                            option.IgnoreZoomLevel = true;
                            option.EnsureCleanSession = true;
                            isIEDriverHide = true;
                            var driver = new InternetExplorerDriver(option);

                            try
                            {
                                CallBackDisplayLabel(delayTimeModLabel, entity.URL);

                                //naver 페이지를 호출합니다.
                                driver.Navigate().GoToUrl("m.naver.com");
                                isIEDriverHide = false;
                                SetActivateWindow();

                                // 검색어 필드를 찾습니다.
                                var searchField = driver.FindElementById("query");

                                //검색필드에 검색어를 보냅니다.
                                searchField.SendKeys(entity.KeyWord);
                                searchField.Click();

                                //검색버튼을 클릭합니다.
                                var searchButton = driver.FindElementByClassName("sch_submit");
                                searchButton.Submit();

                                loopTime = 0;
                                while (loopTime <= entity.PageByLoopTime)
                                {    //검색된 페이지를 스크롤합니다.  
                                    driver.ExecuteScript(string.Format("window.scrollBy(0,{0})", Common.RandomKeyValue(1, 50)), "");
                                    Thread.Sleep(entity.ThreadTime);
                                }
                                MouseWheelUpScrolling();
                                loopTime = 0;

                                string paramSearch = "x_fsn=cali:1";
                                try
                                {
                                    var hrefLinkElement =  driver.FindElementByXPath(string.Format("//a[contains(@href,'{0}')]", "x_fsn=cali:"));
                                    string paramHref = hrefLinkElement.GetAttribute("href");

                                    string[] arrayParam = paramHref.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (arrayParam.Length > 0)
                                    {
                                        foreach (string str in arrayParam)
                                        {
                                            if (str.Contains("x_fsn=cali:") == true)
                                            {
                                                paramSearch = str;
                                                break;
                                            }
                                        }
                                    }
                                }
                                catch{}                               

                                //두번째 페이지의 시작 아이템을 확인하는 로직입니다.
                                string startLinkValue = string.Format("page=2&{0}&start=11&display=15",paramSearch);
                                int startItemValue = 6;
                                try
                                {
                                    var startLinkElement = driver.FindElementByXPath(string.Format("//a[contains(@href,'{0}')]", startLinkValue));
                                    startItemValue = 11;                                   
                                }
                                catch { startItemValue = 6; }

                                List<string> searchPageList = new List<string>();
                                //검색할 페이지 데이터를 설정합니다.
                                for (int i = 0; i < entity.MaxPage; i++)
                                {
                                    string addValue = string.Format("page={0}&{1}&start={2}&display=15", (i + 2).ToString(), i == 0 ? paramSearch : string.Empty, (i * 15 + startItemValue).ToString());
                                    searchPageList.Add(addValue);
                                }

                                bool isSearchSuccess = false;
                                //해당 페이지에서 대상 URL 을 찾을 때까지 검색합니다.
                                for (int page = 0; page < entity.MaxPage; page++)
                                {
                                    try
                                    {
                                        var linkElement = driver.FindElementByXPath(string.Format("//a[contains(@href,'{0}')]", entity.URL));
                                        //순위를 처리합니다.
                                        string rankId = linkElement.FindElement(By.XPath("..")).GetAttribute("id");
                                        try
                                        {
                                            string[] arr = rankId.Split('_');
                                            if (arr.Length > 1)
                                            {
                                                int rank = 0;
                                                int.TryParse(arr[1], out rank);
                                                if (rank > 0)
                                                {
                                                    string rankKey = string.Format("{0}||{1}", entity.KeyWord, entity.URL);
                                                    if (_dicRankList.ContainsKey(rankKey) == true)
                                                    {
                                                        //key 를 삭제합니다.
                                                        _dicRankList.Remove(rankKey);
                                                    }
                                                    _dicRankList.Add(rankKey, rank);
                                                }
                                            }
                                        }
                                        catch { }
                                        isSearchSuccess = true;
                                        linkElement.Click();
                                        break;
                                    }
                                    catch
                                    {
                                        #region 검색키워드
                                        //page=2&start=11&display=15&sm=mtb_nmr
                                        //page=1&start=1&display=10&sm=mtb_pge
                                        //page=2&start=11&display=15&sm=mtb_pge
                                        //page=3&start=26&display=15&sm=mtb_pge
                                        //page=4&start=41&display=15&sm=mtb_pge 
                                        #endregion

                                        loopTime = 0;
                                        while (loopTime <= entity.PageByLoopTime)
                                        {
                                            driver.ExecuteScript(string.Format("window.scrollBy(0,{0})", Common.RandomKeyValue(1, 50)), "");
                                            Thread.Sleep(entity.ThreadTime);
                                        }
                                        MouseWheelUpScrolling();
                                        loopTime = 0;

                                        var nextElement = driver.FindElementByXPath(string.Format("//a[contains(@href,'{0}')]", searchPageList[page]));
                                        nextElement.Click();

                                    }
                                }

                                //검색에 성공했을 경우에만 컨테츠를 체류 합니다.
                                if (isSearchSuccess == true)
                                {
                                    //컨텐츠 페이지 체류시간을 설정합니다.
                                    int delayTime = Common.RandomKeyValue(entity.DelayTimeST, entity.DelayTimeET);
                                    loopTime = 0;
                                    while (loopTime <= delayTime)
                                    {
                                        driver.ExecuteScript(string.Format("window.scrollBy(0,{0})", Common.RandomKeyValue(1, 50)), "");
                                        Thread.Sleep(entity.ThreadTime);
                                        DisplayDlayTimeLabelDisplay(Convert.ToInt32(delayTime), loopTime);
                                    }
                                    loopTime = 0;
                                }

                                //delayTime 의 메세지를 변경 합니다.
                                CallBackDisplayLabel(delayTimeModLabel, "");
                                driver.Quit();
                            }
                            catch (Exception ex) { Common.WriteTextLog("AutoJob_BackGroundWorker2", ex.Message.ToString()); driver.Quit(); }
                            progressValue = progressValue + valueTerm;
                            CallBackProgressBar(progressBar1, progressValue);
                            if (_isJobStart == false) break;
                        }

                        CallBackProgressBar(progressBar1, 100);
                        //delayTime 의 메세지를 변경 합니다.
                        CallBackDisplayLabel(delayTimeModLabel, "다음주기 작업 준비중...");

                        //Loop 를 종료하면 IP 를 변경한다.
                        if (Common.IsProxy == true)
                        {
                            if (isProxyTimeBase == false)
                            {
                                //Loop 기준으로 IP 를 변경할때 사용합니다.
                                ProxyHandler.ProxyIpListSetting();
                                int cnt = Common.dicProxyList.Count;
                                if (cnt > 0)
                                {
                                    int k;
                                    bool isHasConnection = false;
                                    for (k = 1; k <= proxyIpTryConnectCount; k++)
                                    {
                                        int key = Common.RandomKeyValue(1, cnt);
                                        string errMessage = string.Empty;
                                        WinINetUtils.ChangeIEProxyServer(Common.dicProxyList[key], ref errMessage);
                                        if (string.IsNullOrEmpty(errMessage) == false)
                                        {
                                            string message = "프록시 아이피 변경 실패(관리자에게 문의하세요)-->" + errMessage;
                                            JobStateChange(true, message);
                                            break;
                                        }
                                        else
                                        {
                                            CallBackDisplayLabel(delayTimeModLabel, string.Format("프록시IP[{0}] {1}회 연결시도중...", Common.dicProxyList[key], k.ToString()));
                                            Common.WriteTextLog("프록시IP 연결시도", string.Format("프록시IP[{0}] {1}회 연결시도", Common.dicProxyList[key], k.ToString()));
                                            isHasConnection = Common.HasConnection();
                                            if (isHasConnection == true)
                                            {
                                                break;
                                            }
                                        }
                                    }

                                    if (isHasConnection == false)
                                    {
                                        string message = string.Format("프록시IP를 {0}번 변경했으나 IP 불량으로  인터넷연결이 지연되고 있습니다.", proxyIpTryConnectCount.ToString());
                                        JobStateChange(true, message);
                                    }
                                }
                                //Common.isProxyIPChangeByLoopBase = true;
                                Thread.Sleep(10000);
                            }
                        }
                        else
                        {
                            //Mobile 에서 IP 를 업데이트 하기 위해 DB 의  ipchange 키를 설정합니다.
                            Common.UpdateIPChangFlage(userInfo.UserID, "Y");
                            Thread.Sleep(10000);
                        }

                        //user agent 가  random 인 경우에만 처리합니다.
                        if (Common.isUserAgentRandomApply == true)
                        {
                            // useragent 를 변경할수 있도록 처리합니다.
                            UserAgentRandomApplyLoop();
                            Thread.Sleep(1000);
                        }

                        CallBackProgressBar(progressBar1, 0);
                    }
                    catch (Exception ex)
                    {
                        Common.WriteTextLog("AutoJob_BackGroundWorker2", ex.Message.ToString());
                    }
                }
                else
                {
                    string message = "경고 : 키워드 건수가 존재하지 않습니다.키워드를 추가하세요!";
                    JobStateChange(true, message);
                    break;
                }
            }
        }
        #endregion
                
        #region TimerTick - 컨텐트의 loop 시간을 설정합니다
        private void timeCheckTimer_Tick(object sender, EventArgs e)
        {
            loopTime++;
            if (isIEDriverHide == true)
            {
                ProcessForMinize();//cmd 창을 숨김니다.
            }

            //proxy를 실행하면서 isProxyTimeBase  가 true 일경우에만 실행합니다.
            if (Common.IsProxy == true && isProxyTimeBase == true)
            {
                proxyDelaySecond++;
                if (proxyDelaySecond >= 300)
                {
                    //proxy IP 를 변경 합니다.
                    ProxyHandler.ProxyIpListSetting();
                    int cnt = Common.dicProxyList.Count;
                    if (cnt > 0)
                    {
                        int k;
                        bool isHasConnection = false;
                        for (k = 1; k <= proxyIpTryConnectCount; k++)
                        {
                            int key = Common.RandomKeyValue(1, cnt);
                            string errMessage = string.Empty;
                            WinINetUtils.ChangeIEProxyServer(Common.dicProxyList[key], ref errMessage);
                            if (string.IsNullOrEmpty(errMessage) == false)
                            {
                                string message = "프록시 아이피 변경 실패(관리자에게 문의하세요)-->" + errMessage;
                                JobStateChange(true, message);
                                break;
                            }
                            else
                            {
                                CallBackDisplayLabel(delayTimeModLabel, string.Format("프록시IP[{0}] {1}회 연결시도중...", Common.dicProxyList[key], k.ToString()));
                                Common.WriteTextLog("프록시IP 연결시도", string.Format("프록시IP[{0}] {1}회 연결시도", Common.dicProxyList[key], k.ToString()));
                                isHasConnection = Common.HasConnection();
                                if (isHasConnection == true)
                                {
                                    break;
                                }
                            }
                        }

                        if (isHasConnection == false)
                        {
                            string message = string.Format("프록시IP를 {0}번 변경했으나 IP 불량으로  인터넷연결이 지연되고 있습니다.", proxyIpTryConnectCount.ToString());
                            JobStateChange(true, message);
                        }
                    }
                    //IP 를 변경한후 초기 셋팅합니다.
                    proxyDelaySecond = 0;
                }
            }
        }

        #endregion

        #region jobStartButton_Click - 웹브라우져 작업을 시작하는 이벤트입니다.
        /// <summary>
        /// 작업시작 이벤트입니다. 
        /// 작업시작시 쓰레드를 실행합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jobStartButton_Click(object sender, EventArgs e)
        {
            if (_isJobStart == false)
            {
                progressBar1.Value = 0;
                _loopData.Clear();
                _loopData = Common.GetKeyWordInfo(userInfo.KeyCode);

                _isJobStart = true;
                jobStartButton.Text = "실행중";
                if (Common.isMouseScrollExecute == true)
                {
                    work = new Thread(new ThreadStart(AutoJob_BackGroundWorker));
                }
                else
                {
                    work = new Thread(new ThreadStart(AutoJob_BackGroundWorker2));
                    //work = new Thread(new ThreadStart(AutoJob_BackGroundWorkerByWatiN));
                    //work.SetApartmentState(ApartmentState.STA);
                }
               
                work.IsBackground = true;
                work.Start();
            }
            else
            {
                _isJobStart = false;
                jobStartButton.Text = "작업시작";
                CallBackDisplayLabel(delayTimeModLabel, "");

                work.Abort();
                progressBar1.Value = 0;
            }
        }
        #endregion

        #region JobStateChange - 버튼 상태 변경 및 작업쓰레드 상태를 변경합니다.
        /// <summary>
        /// 버튼 상태 변경 및 작업쓰레드 상태를 변경합니다.
        /// </summary>
        /// <param name="isStop"></param>
        private void JobStateChange(bool isStop)
        {
            JobStateChange(isStop, string.Empty);
        }
        private void JobStateChange(bool isStop, string message)
        {

            if (isStop == false)
            {
                _loopData.Clear();
                _loopData = Common.GetKeyWordInfo(userInfo.KeyCode);

                _isJobStart = true;
                CallBackButton(jobStartButton, "실행중");
                if (Common.isMouseScrollExecute == true)
                {
                    work = new Thread(new ThreadStart(AutoJob_BackGroundWorker));
                }
                else
                {
                    work = new Thread(new ThreadStart(AutoJob_BackGroundWorker2));
                  //  work = new Thread(new ThreadStart(AutoJob_BackGroundWorkerByWatiN));
                  //  work.SetApartmentState(ApartmentState.STA);
                }                
                work.IsBackground = true;
                work.Start();
            }
            else
            {
                if (_isJobStart == true)
                {
                    _isJobStart = false;
                    CallBackButton(jobStartButton, "작업시작");
                    CallBackDisplayLabel(delayTimeModLabel, message);
                    work.Abort();
                }

            }
        }
        #endregion

        #region closeButton_Click - 프로그램을 종료합니다.
        /// <summary>
        /// 프로그램을 종료합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, EventArgs e)
        {
            ProcessKill();
            this.Close();
        }
        #endregion

        #region ProcessForMinize - IEDriverServer 프로세서를 최소화 합니다.

        /// <summary>
        /// IEDriverServer 프로세서를 최소화 합니다.
        /// </summary>
        private void ProcessForMinize()
        {
            Process[] processes = Process.GetProcessesByName("IEDriverServer");
            foreach (Process proc in processes)
            {
                // 윈도우 핸들러
                IntPtr procHandler = FindWindow(null, proc.MainWindowTitle);
                // 활성화
                ShowWindowAsync(procHandler, Convert.ToInt32(WNDSTATE.SW_HIDE));
            }
        }
        #endregion

        #region ProcessKill - 프로세서를 삭제합니다.
        /// <summary>
        /// 프로세서를 삭제합니다.
        /// </summary>
        private void ProcessKill()
        {
            try
            {
                Process[] arrProces = Process.GetProcessesByName("IEDriverServer");
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

        #region userAgentApplyButton_Click - UserAgent 적용 버튼
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userAgentApplyButton_Click(object sender, EventArgs e)
        {
            string selValue = userAgentApplyComboBox.SelectedValue.ToString();
            string errMessage = string.Empty;
            if (Common.ChangeUserAgent(selValue, ref errMessage) == true)
            {
                MessageBox.Show("User Agent 를 변경하였습니다.");
                userAgentCurrentTextBox.Text = selValue;
            }
            else
            {
                if (string.IsNullOrEmpty(errMessage) == false)
                {
                    MessageBox.Show("관리자에게 문의 하세요!-->" + errMessage);
                }
                else
                {
                    MessageBox.Show("User Agent 를 변경에 실패하였습니다.");
                }
            };

        }
        #endregion

        #region userAgentRestoreButton_Click - User agent 를 PC 버전으로 복구 합니다.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userAgentRestoreButton_Click(object sender, EventArgs e)
        {

            if (Common.RestoreRegistryForUserAgent() == true)
            {
                MessageBox.Show("User Agent 를 복원하였습니다.");
            }
            else
            {
                MessageBox.Show("User Agent 를 복원에 실패하였습니다.");
            };

            //User Agent 설정을 현행화 합니다.            
            CallBackTextBox(userAgentCurrentTextBox, GetCurrentUserAgent());
        }
        #endregion

        #region userAgentTestButton_Click - userAgent 를 테스트 합니다.
        /// <summary>
        /// user Agent 를 테스트 합니다. 
        /// www.naver.com 으로 실행할때 mobil 페이지로 실행되는지 확인합니다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userAgentTestButton_Click(object sender, EventArgs e)
        {
            //IE 브라우져를 띄워서 모바일로 들어가는지 확인합니다.         
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "iexplore.exe";
            startInfo.Arguments = "www.naver.com";
            startInfo.WorkingDirectory = @"C:\WINDOWS\Temp";
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.ErrorDialog = true;
            Process.Start(startInfo);
        }
        #endregion

        #region addKeyWordButton_Click - 키워드를 추가합니다.
        /// <summary>
        /// 키워드를 추가합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addKeyWordButton_Click(object sender, EventArgs e)
        {
            frmKeyWordRegister form = new frmKeyWordRegister();
            form.KeyCode = userInfo.KeyCode;
            form.IsEdit = false;
            form.ShowDialog();

            BindKeyWordList(userInfo.KeyCode);
        }
        #endregion

        #region saveKeyWordButton_Click - 키워드를 파일로 저장합니다.
        /// <summary>
        /// 키워드를 파일로 저장합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveKeyWordButton_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            SaveFileDialog dig = new SaveFileDialog();
            dig.Filter = "텍스트파일|*.txt";
            dig.Title = "키워드리스트 파일 저장";
            if (DialogResult.OK == dig.ShowDialog())
            {
                if (string.IsNullOrEmpty(dig.FileName) == true)
                {
                    MessageBox.Show("파일명을 작성하지 않았습니다.");
                    return;
                }

                filePath = dig.FileName;
                using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.Default))
                {
                    DataTable dt = Common.GetKeyWordList(userInfo.KeyCode);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string keyword = dt.Rows[i][0].ToString();
                            string url = dt.Rows[i][1].ToString();
                            sw.WriteLine(string.Format("{0},{1}", keyword, url));
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("파일명을 작성하지 않았습니다.");
                return;
            }
        }
        #endregion

        #region deleteKeyWordButton_Click - 키워드를 삭제합니다.
        /// <summary>
        /// 키워드를 삭제합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteKeyWordButton_Click(object sender, EventArgs e)
        {
            if (keyWordAddDataGridView.SelectedRows.Count > 0)
            {
                try
                {
                    string keyword = keyWordAddDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                    string url = keyWordAddDataGridView.SelectedRows[0].Cells[1].Value.ToString();

                    Common.DeleteKeyWordInfo(userInfo.KeyCode, keyword, url);
                }
                catch
                {
                    MessageBox.Show("키워드 삭제중 에러가 발생했습니다.");
                    return;
                }
                BindKeyWordList(userInfo.KeyCode);
                MessageBox.Show("선택된 키워드가 정상적으로 삭제 되었습니다.");
                return;
            }
            else
            {
                MessageBox.Show("삭제할 데이터가 선택되지 않았습니다.\r\n데이터를 선택해 주세요");
                return;
            }
        }
        #endregion

        #region frmMain_Load - 페이지를 로드합니다.
        /// <summary>
        /// winddow onload 시 기초작업을 설정합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            //프로그램에 로그인합니다.
            frmLogin login = new frmLogin();
            login.ShowDialog();
            userInfo.UserID = login.UserID;

            if (login.IsLogin == false)
            {
                this.Close();
                return;
            }

            //사용자정보를 초기화 합니다.
            SettingUserInfo(userInfo.UserID);
            DateTime endDate;
            DateTime.TryParse(userInfo.ToDt, out endDate);
            if (endDate.AddDays(1) < DateTime.Now)
            {
                string alertMessage = string.Format("사용기간종료 ({0})까지 입니다.\r\n프로그램을 종료합니다.", userInfo.ToDt);
                MessageBox.Show(alertMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            string clientUpgradeProgramVersion = "1900000000";
            string serverUpgradeProgramVersion = "1900000000";
            string downloadFileName = "MobiClickAutoUpGrade.exe";
            try
            {
                //서버의 다운로드 파일의 버전을 가져옵니다.
                Common.GetUpGradeProgramInfo(ref serverUpgradeProgramVersion);
                clientUpgradeProgramVersion = iniHandler.GetIniValue("FILELIST", downloadFileName);
                if (string.IsNullOrEmpty(serverUpgradeProgramVersion) == false)
                {
                    if (serverUpgradeProgramVersion != clientUpgradeProgramVersion)
                    {
                        iniHandler.SetIniValue("FILELIST", downloadFileName, serverUpgradeProgramVersion);
                        FtpUtils.DownloadFileByWebClient(downloadFileName, AppDomain.CurrentDomain.BaseDirectory + downloadFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("MobiClickAutoUpGrade", ex.Message.ToString());
            }

            string serverVersion = "1900000000";
            string clientVersion = "1900000000";
            try
            {
                //서버의 다운로드 파일의 버전을 가져옵니다.
                Common.GetUpGradeList(ref serverVersion);
                clientVersion = iniHandler.GetIniValue("VERSION", "CLIENT");



                int iServerVersion = 0;
                int iClientVersion = 0;
                int.TryParse(serverVersion, out iServerVersion);
                int.TryParse(clientVersion, out iClientVersion);

                if (iServerVersion > iClientVersion)
                {
                    OpenMobiClickAutoUpGrade();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("frmMain_Load", ex.Message.ToString());
            }

            proxyTimeBaseCheckBox.Checked = false;
            proxyTimeBaseCheckBox.Enabled = false;
            isProxyTimeBase = false;
            proxyDelaySecond = 0;

            //keyword 리스트르 바인딩 합니다.
            BindKeyWordList(userInfo.KeyCode);

            //User Info 를  display 합니다. 
            SettingDisplay();

            //랜카드를 display 합니다.
            SetNetworkInterfaces();

            //맥어드레스를 설정합니다.
            macAddressDisplayLabel.Text = Common.GetMacAddress();

            //UserAgent 를 Display 합니다.
            //User Agent 설정을 현행화 합니다.            
            CallBackTextBox(userAgentCurrentTextBox, GetCurrentUserAgent());

            //UserAgent리스트를 설정합니다.
            //userAGentList 를 가져옵니다.
            userAgentList = Common.UserAgentList();
            userAgentApplyComboBox.DataSource = userAgentList;

            //IE실행 파일을 설정합니다.
            if (Kernel32Utilscs.Is64Bit() == true)
            {
                _ieExecuteMode = Common.RegistryMode.WoW64;
                SetIEExecuteServer(_ieExecuteMode);
            }
            else
            {
                _ieExecuteMode = Common.RegistryMode.Normal;
                SetIEExecuteServer(_ieExecuteMode);
            }

            //userAgent 변경
            string applyAgent = RandomByUserAgentString();
            string errMessage = string.Empty;
            if (Common.ChangeUserAgent(applyAgent, ref errMessage) == true)
            {
                CallBackTextBox(userAgentCurrentTextBox, applyAgent);
            }

            //레지스트리 사용권한을 설정합니다.
            SetAccessControlForTheRegistryKey();

            Common.SettingRegistery(ref errMessage);//Zoom 값을 100%로 변환 합니다.
            if (string.IsNullOrEmpty(errMessage) == false)
            {
                MessageBox.Show("레지스트리 변경이 권한문제로 정상적이지 않습니다.\r\n프록시IP 사용시 IP 가 변경되지 않습니다.\r\n관리자에게 문의하세요!");
            }

            //사이트순위를 업데이트 합니다
            rankWork = new Thread(new ThreadStart(RankAndIpChangeUpdate));
            rankWork.IsBackground = true;
            rankWork.Start();

            //timer 를 시작합니다.
            timeCheckTimer.Enabled = true;
            timeCheckTimer.Start();
        }
        #endregion

        #region RankAndIpChangeUpdate - 순위 및 IP 변경내역 업데이트
        /// <summary>
        /// 순위 및 IP 변경내역 업데이트
        /// </summary>
        private void RankAndIpChangeUpdate()
        {
            while (true)
            {
                ChangeByIpAddress();
                RankUpdate();
                Thread.Sleep(20000);
            }
        }
        #endregion

        #region UserAgentRandomApplyLoop - 작업 1 사이클마다 user Agent 를 변경합니다.
        /// <summary>
        /// 작업 1 사이클마다 user Agent 를 변경합니다.
        /// 변경후 20  초가  쓰레드를 중지합니다.
        /// </summary>
        private void UserAgentRandomApplyLoop()
        {
            if (Common.isUserAgentRandomApply == true)
            {
                try
                {
                    string applyAgent = RandomByUserAgentString();
                    string errMessage = string.Empty;
                    if (Common.ChangeUserAgent(applyAgent, ref errMessage) == true)
                    {
                        CallBackTextBox(userAgentCurrentTextBox, applyAgent);
                    }
                    else
                    {
                        CallBackDisplayLabel(delayTimeModLabel, "User Agent 변경 실패(관리자문의)-->" + errMessage);
                    }
                }
                catch { }
            }
        }
        #endregion

        #region RandomByUserAgentString -  user Agent String 를 랜덤하게 가져옵니다.
        /// <summary>
        /// user Agent String 를 랜덤하게 가져옵니다.
        /// </summary>
        /// <returns></returns>
        private string RandomByUserAgentString()
        {
            string returnValue = string.Empty;
            Random rdm = new Random();
            int listCount = userAgentList.Count;
            int value = rdm.Next(1, listCount);

            Dictionary<int, string> dic = new Dictionary<int, string>();

            int cnt = 0;
            foreach (string str in userAgentList)
            {
                cnt++;
                dic.Add(cnt, str);
            }

            dic.TryGetValue(value, out returnValue);

            return returnValue;
        }
        #endregion

        #region RankUpdate - 순위를 업데이트 하는 메서드 입니다.
        /// <summary>
        /// 순위를 업데이트 하는 메서드 입니다. 
        /// </summary>
        private void RankUpdate()
        {
            DataTable dt = Common.GetKeyWordList(userInfo.KeyCode);

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string keyword = dt.Rows[i][0].ToString();
                    string url = dt.Rows[i][1].ToString();
                    string rankKey = string.Format("{0}||{1}", keyword, url);

                    int sn = 0;
                    if (_dicRankList.ContainsKey(rankKey) == true)
                    {
                        _dicRankList.TryGetValue(rankKey, out sn);
                    }

                    switch (sn)
                    {
                        case 0: dt.Rows[i][4] = "확인전"; break;
                        default: dt.Rows[i][4] = sn.ToString() + "위"; break;
                    }
                }

                dt.AcceptChanges();

                CallBackGrid(keyWordAddDataGridView, dt);
            }
        }
        #endregion

        #region SetIEExecuteServer - IE 실행파일을 설정합니다.
        /// <summary>
        /// IE 실행파일을 설정합니다.
        /// </summary>
        /// <param name="mode"></param>
        private void SetIEExecuteServer(Common.RegistryMode mode)
        {
            try
            {
                string ieExecuteServerName = AppDomain.CurrentDomain.BaseDirectory + "IEDriverServer.exe";
                if (File.Exists(ieExecuteServerName) == true) { File.Delete(ieExecuteServerName); }
                switch (mode)
                {
                    case Common.RegistryMode.Normal: File.Copy(AppDomain.CurrentDomain.BaseDirectory + "" + "IEDriverServerx86", ieExecuteServerName, true); break;
                    case Common.RegistryMode.WoW64: File.Copy(AppDomain.CurrentDomain.BaseDirectory + "" + "IEDriverServerx64", ieExecuteServerName, true); break;
                }
            }
            catch { }
        }
        #endregion

        #region SettingDisplay - 기본정보를 설정합니다.
        /// <summary>
        /// 기본정보를 설정합니다.
        /// </summary>
        private void SettingDisplay()
        {
            useIdDisplayLlabel.Text = userInfo.UserID;
            //authYNDisplayLabel.Text = string.IsNullOrEmpty(userInfo.AuthYn) == true || userInfo.AuthYn == "N" ? "인증전" : "인증완료";
            keywordCountDisplayLabel.Text = userInfo.AllowKeywordCount.ToString();
            startDateLabel.Text = userInfo.FromDt;
            endDateLabel.Text = userInfo.ToDt;
        }
        #endregion

        #region SettingUserInfo - 사용자정보를 업데이트 합니다.
        /// <summary>
        /// 사용자정보를 업데이트 합니다.
        /// </summary>
        /// <param name="userID"></param>
        private void SettingUserInfo(string userID)
        {
            DataTable dt = Common.GetUserINfo(userID);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                int allowkeywordcount = 0;
                int.TryParse(dr["ALLOW_KEYWORD_CNT"].ToString(), out allowkeywordcount);

                userInfo.KeyCode = dr["KEY_CODE"].ToString();
                userInfo.MacAddress = dr["MACADDRESS"].ToString();
                userInfo.AllowKeywordCount = allowkeywordcount;
                userInfo.AuthYn = dr["AUTH_YN"].ToString();
                userInfo.FromDt = dr["FROM_DT"].ToString();
                userInfo.ToDt = dr["TO_DT"].ToString();
            }
        }
        #endregion

        #region BindKeyWordList - 키워드리스트를 바인딩 합니다.
        /// <summary>
        ///  키워드리스트를 바인딩 합니다.
        /// </summary>
        /// <param name="keycode"></param>
        private void BindKeyWordList(string keycode)
        {
            CallBackGrid(keyWordAddDataGridView, Common.GetKeyWordList(keycode));
        }
        #endregion

        #region SetNetworkInterfaces -  네트웍카드 리스트를 보여줍니다.
        /// <summary>
        /// 네트웍카드 리스트를 보여줍니다.
        /// </summary>
        private void SetNetworkInterfaces()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics == null || nics.Length < 1) { return; }

            foreach (NetworkInterface nic in nics)
            {
                if (nic.NetworkInterfaceType != NetworkInterfaceType.Wireless80211) continue;
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    lanCareListComboBox.Items.Add(nic.Description);
                }
            }

            if (lanCareListComboBox.Items.Count > 0)
            {
                lanCareListComboBox.SelectedIndex = 0;
            }
        }
        #endregion

        #region GetCurrentUserAgent - User Agent를 가져옵니다.
        /// <summary>
        /// User Agent를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        private string GetCurrentUserAgent()
        {
            string js = @"<script type='text/javascript'>function getUserAgent(){document.write(navigator.userAgent)}</script>";
            string userAgent = string.Empty;
            try
            {
                WebBrowser wb = new WebBrowser();
                wb.Url = new Uri("about:blank");
                wb.Document.Write(js);
                wb.Document.InvokeScript("getUserAgent");

                userAgent = wb.DocumentText.Substring(js.Length);
            }
            catch { }

            return userAgent;
        }
        #endregion

        #region DisplayDlayTimeLabelDisplay - 컨텐츠 페이지 당 체류시간에 대한 내용을 보여줍니다.
        /// <summary>
        /// 컨텐츠 페이지 당 체류시간에 대한 내용을 보여줍니다.
        /// </summary>
        /// <param name="delayTime"></param>
        /// <param name="currentSecond"></param>
        private void DisplayDlayTimeLabelDisplay(int delayTime, int currentSecond)
        {
            int iCloseDelay = delayTime - currentSecond;
            string delayMessage = string.Format("총 {0}초 중 {1}초 경과 -> 잔여체류시간 {2}초 ", delayTime.ToString(), currentSecond.ToString(), iCloseDelay.ToString());
            CallBackDisplayLabel(delayTimeModLabel, delayMessage);
        }
        #endregion

        #region ChangeByIpAddress - ip 의 변동사항을 설정합니다.
        /// <summary>
        /// ip 의 변동사항을 설정합니다.
        /// </summary>
        private void ChangeByIpAddress()
        {
            //while (true)
            //{
            try
            {
                string ipAddress = Common.GetExternalIp().ToString();
                if (string.IsNullOrEmpty(ipAddress) == false && ipAddress != _lastJobIP)
                {
                    _lastJobIP = ipAddress;
                    _lastJobDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");

                    CallBackRichTextBoxIP(ipChangeRichTextBox);

                    Application.DoEvents();
                }
            }
            catch
            {
            }
        }
        #endregion

        #region frmMain_FormClosed - 프로그램 종료시 Selenuim 의 IE 컨트롤 프로세서들을 중지 시킵니다.
        /// <summary>
        /// 프로그램 종료시 Selenuim 의 IE 컨트롤 프로세서들을 중지 시킵니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Common.IsProxy = false;
            string message = string.Empty;
            WinINetUtils.ChangeIEProxyServer(string.Empty, ref message);
            ProcessKill();
        }
        #endregion

        #region macChangeButton_Click - mac 주소를 동기화 합니다.
        /// <summary>
        /// mac 주소를 동기화 합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void macChangeButton_Click(object sender, EventArgs e)
        {
            try
            {
                Common.UpdateProductInfo(userInfo.KeyCode, userInfo.UserID, Common.GetMacAddress(), userInfo.AllowKeywordCount);

                MessageBox.Show("등록된 Mac 주소가 현재PC 의 주소로 변경되었습니다.");

                //user 정보를 변경 합니다.
                SettingUserInfo(userInfo.UserID);

                //delayTime 의 메세지를 변경 합니다.
                CallBackDisplayLabel(delayTimeModLabel, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region maxSearchSnComboBox_SelectedIndexChanged - 최대순번값을 설정합니다.
        ///// <summary>
        ///// 최대순번값을 설정합니다.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void maxSearchSnComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Common.naverSearchSnMaxTerm = maxSearchSnComboBox.SelectedItem.ToString();
        //} 
        #endregion

        #region programInfoToolStripButton_Click - 프로그램 정보를 보여줍니다
        /// <summary>
        /// 프로그램 정보를 보여줍니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void programInfoToolStripButton_Click(object sender, EventArgs e)
        {
            frmAboutBox aboutForm = new frmAboutBox();
            aboutForm.ShowDialog();
        }
        #endregion

        #region ieExecuteChangeToolStripButton_Click - 셀레늄에서 IE 실행파일 버전 변경
        /// <summary>
        /// 셀레늄에서 IE 실행파일 버전 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ieExecuteChangeToolStripButton_Click(object sender, EventArgs e)
        {
            //브라우져 실행 파일을 변경합니다.
            string modeString = "";
            if (_ieExecuteMode == Common.RegistryMode.WoW64)
            {
                _ieExecuteMode = Common.RegistryMode.Normal;
                SetIEExecuteServer(_ieExecuteMode);
                modeString = "32bit";
            }
            else
            {
                _ieExecuteMode = Common.RegistryMode.WoW64;
                SetIEExecuteServer(_ieExecuteMode);
                modeString = "64bit";
            }

            MessageBox.Show(string.Format("IEDriverServer 실행파일이 {0}용으로 변경되었습니다.", modeString));
        }
        #endregion

        #region mobileDownloadToolStripButton_Click - 모바일 IP 변경 앱 다운로드
        /// <summary>
        /// 모바일 IP 변경 앱 다운로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mobileDownloadToolStripButton_Click(object sender, EventArgs e)
        {
            string targetFilePath = string.Empty;
            string downloadFileName = "MobilClickIpChanger.apk";
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                targetFilePath = folderBrowserDialog1.SelectedPath;
                targetFilePath += "\\" + downloadFileName;
            }
            else
            {
                MessageBox.Show("파일저장경로의 선택을 취소하였습니다.");
                return;
            }

            if (string.IsNullOrEmpty(targetFilePath) == true)
            {
                MessageBox.Show("파일저장경로가 선택되지 않았습니다.");
                return;
            }

            try
            {
                FtpUtils.DownloadFileByWebClient(downloadFileName, targetFilePath);
                MessageBox.Show("파일이 다운로드 되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion

        #region proxyIPConfigToolStripButton_Click
        /// <summary>
        /// 프록시IP 환경설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void proxyIPConfigToolStripButton_Click(object sender, EventArgs e)
        {
            frmProxyConfig form = new frmProxyConfig();
            form.ShowDialog();
        }
        #endregion

        #region proxyUseCheckBox_CheckedChanged -프록시 사용여부
        /// <summary>
        /// 프록시IP 사용여부 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void proxyUseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (proxyUseCheckBox.Checked == true)
            {
                if (File.Exists(Common.proxyIpListPath) == false)
                {
                    MessageBox.Show("Proxy IP List 가 존재하지 않습니다.\r\nList 가 포함된 파일을 선택해주세요", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    proxyUseCheckBox.Checked = false;
                    return;
                }
                Common.IsProxy = true;
                Common.networkConnertType = Common.NetWorkConnectionType.Proxy;
                proxyTimeBaseCheckBox.Enabled = true;
            }
            else
            {
                Common.IsProxy = false;
                Common.networkConnertType = Common.NetWorkConnectionType.Mobile;
                Common.dicProxyList.Clear();

                proxyTimeBaseCheckBox.Checked = false;
                proxyTimeBaseCheckBox.Enabled = false;
                isProxyTimeBase = false;
                proxyDelaySecond = 0;

                string message = string.Empty;
                WinINetUtils.ChangeIEProxyServer(string.Empty, ref message);
                if (string.IsNullOrEmpty(message) == false)
                {
                    MessageBox.Show(message);
                }
            }
        }
        #endregion

        #region userAgentRandomApplyCheckBox_CheckedChanged - UserAgent 를 무작위로 수행합니다.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userAgentRandomApplyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Common.isUserAgentRandomApply = userAgentRandomApplyCheckBox.Checked;
            if (Common.isUserAgentRandomApply == true)
            {
                userAgentApplyComboBox.Enabled = false;
            }
            else
            {
                userAgentApplyComboBox.Enabled = true;
            }
        }
        #endregion

        #region SetActivateWindow -  window 를 활성화 합니다.
        [DllImport("user32")]
        public static extern int MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool bRepaint);
        /// <summary>
        /// window 를 활성화 합니다.
        /// </summary>
        private void SetActivateWindow()
        {
            Process[] processes = Process.GetProcessesByName("iexplore");
            if (processes.Length > 0)
            {
                foreach (Process proc in processes)
                {
                    // 윈도우 핸들러
                    IntPtr procHandler = FindWindow(null, proc.MainWindowTitle);
                    var form = Control.FromHandle(procHandler);
                    if (form != null)
                    {
                        //SetWindowPos(procHandler, -1, 0, 0, 350, 650, SWP_NOZORDER | SWP_NOSIZE);
                        MoveWindow(procHandler, 0, 0, 350, 700, true);
                    }

                    //SetFocus(procHandler);
                    //SetForegroundWindow(procHandler);
                }
            }
        }
        #endregion

        #region MouseWheelScrolling - 마우스 휠버튼을 스크롤 합니다.
        public void MouseWheelScrolling(int delta)
        {
            MouseWheelScrolling(delta, false);
        }
        public void MouseWheelScrolling(int delta, bool ismove)
        {
            //Process[] processes = Process.GetProcesses();
            Process[] processes = Process.GetProcessesByName("iexplore");
            foreach (Process proc in processes)
            {                    // 윈도우 핸들러
                IntPtr procHandler = FindWindow(null, proc.MainWindowTitle);

                int ptrPhwnd = 0;
                int ptrNhwnd = 0;
                WNDSTATE intShowCmd = WNDSTATE.SW_SHOW;
                Point ptPoint = new Point();
                Size szSize = new Size();

                // 해당  window 의위치를 가져옵니다.
                GetWindowPos(procHandler, ref ptrPhwnd, ref ptrNhwnd, ref ptPoint, ref szSize, ref intShowCmd);

                //마우스를 약간 움직입니다.
                mouse_event((int)MouseEventFlags.MOVE, ptPoint.X, ptPoint.Y + 250, 1, 0);

                // 마우스 포지션을 설정 합니다.
                SetCursorPos(200, 200);

                //마우스의 휠을 돌립니다.
                mouse_event((int)MouseEventFlags.WHEEL, ptPoint.X, ptPoint.Y, delta, 0);

            }
        }

        public void MouseWheelUpScrolling()
        {
            // 마우스 포지션을 설정 합니다.
            SetCursorPos(200, 200);
            for (int i = 300; i >= 0; i--) mouse_event((int)MouseEventFlags.WHEEL, 0, 0, 20, 0);
        }
        #endregion

        #region GetPlacement -  윈도우 핸들의 위치를 가져옵니다.
        /// <summary>
        /// 윈도우 핸들의 위치를 가져옵니다.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        private static WINDOWPLACEMENT GetPlacement(IntPtr hwnd)
        {
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            GetWindowPlacement(hwnd, ref placement);
            return placement;
        }
        #endregion

        #region GetWindowPos - 윈도우 핸들의 위치를 가져옵니다.
        /// <summary>
        /// 윈도우 핸들의 위치를 가져옵니다.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="ptrPhwnd"></param>
        /// <param name="ptrNhwnd"></param>
        /// <param name="ptPoint"></param>
        /// <param name="szSize"></param>
        /// <param name="intShowCmd"></param>
        private void GetWindowPos(IntPtr hwnd, ref int ptrPhwnd, ref int ptrNhwnd, ref Point ptPoint, ref Size szSize, ref WNDSTATE intShowCmd)
        {
            WINDOWPLACEMENT wInf = new WINDOWPLACEMENT();
            wInf.length = System.Runtime.InteropServices.Marshal.SizeOf(wInf);
            GetWindowPlacement(hwnd, ref wInf);
            szSize = new Size(wInf.rcNormalPosition.Right - (wInf.rcNormalPosition.Left * 2), wInf.rcNormalPosition.Bottom - (wInf.rcNormalPosition.Top * 2));
            ptPoint = new Point(wInf.rcNormalPosition.Left, wInf.rcNormalPosition.Top);
        }
        #endregion

        #region scrollExecuteSettingCheckBox_CheckedChanged - 마우스로 스크로를 실행하는지 여부를 처리합니다.
        /// <summary>
        /// 마우스로 스크로를 실행하는지 여부를 처리합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scrollExecuteSettingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Common.isMouseScrollExecute = scrollExecuteSettingCheckBox.Checked;
        }
        #endregion

        #region OpenMobiClickAutoUpGrade - 업그레이드 모듈을 실행합니다.
        /// <summary>
        /// 업그레이드 모듈을 실행합니다.
        /// </summary>
        private void OpenMobiClickAutoUpGrade()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "MobiClickAutoUpGrade.exe";
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("OpenMobiClickAutoUpGrade", ex.Message.ToString());
            }
        }
        #endregion

        #region keyWordRandomCheckBox_CheckedChanged - 키워드 순서를 랜덤으로 실행하도록 처리합니다.
        /// <summary>
        /// 키워드 순서를 랜덤으로 실행하도록 처리합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyWordRandomCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isKeywordRandom = keyWordRandomCheckBox.Checked;
        }
        #endregion

        #region proxyTimeBaseCheckBox_CheckedChanged - proxy 시간 기준의 ip 를 변경 합니다.
        /// <summary>
        /// proxy 시간 기준의 ip 를 변경 합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void proxyTimeBaseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isProxyTimeBase = proxyTimeBaseCheckBox.Checked;
            proxyDelaySecond = 0;
        }
        #endregion

        #region ipForceChangeButton_Click - IP 를 강제로 변경합니다.
        /// <summary>
        /// IP 를 강제로 변경합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ipForceChangeButton_Click(object sender, EventArgs e)
        {
            string errMessage = string.Empty;
            //Loop 를 종료하면 IP 를 변경한다.
            if (Common.IsProxy == true)
            {
                //Loop 기준으로 IP 를 변경할때 사용합니다.
                ProxyHandler.ProxyIpListSetting();
                int cnt = Common.dicProxyList.Count;
                if (cnt > 0)
                {
                    int k;
                    bool isHasConnection = false;
                    StringBuilder iplist = new StringBuilder();
                    for (k = 1; k <= proxyIpTryConnectCount; k++)
                    {
                        int key = Common.RandomKeyValue(1, cnt);
                        WinINetUtils.ChangeIEProxyServer(Common.dicProxyList[key], ref errMessage);
                        if (string.IsNullOrEmpty(errMessage) == false)
                        {
                            MessageBox.Show("프록시 아이피 변경 실패(관리자에게 문의하세요)-->" + errMessage);
                            return;
                        }
                        else
                        {
                            iplist.AppendFormat("{0} 번째 프록시 IP ==> {1} \r\n", k.ToString(), Common.dicProxyList[key]);
                            isHasConnection = Common.HasConnection();
                            if (isHasConnection == true)
                            {
                                break;
                            }
                        }
                    }

                    if (isHasConnection == false)
                    {
                        string message = iplist.ToString() + string.Format("프록시IP를 {0}번 변경했으나 IP불량으로 인터넷연결이 지연되고 있습니다\r\n프록시IP 또는 프록시연결 프로그램을 확인해주세요!", proxyIpTryConnectCount.ToString());
                        MessageBox.Show(message);
                        return;
                    }
                }
            }
            else
            {
                try
                {
                    //Mobile 에서 IP 를 업데이트 하기 위해 DB 의  ipchange 키를 설정합니다.
                    Common.UpdateIPChangFlage(userInfo.UserID, "Y");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("모바일 아이피 변경 실패(관리자에게 문의하세요)-->" + ex.Message.ToString());
                    return;
                }
            }
            //ChangeByIpAddress();
            MessageBox.Show("IP 변경에 성공했습니다.잠시후 display 됩니다.");
        }
        #endregion

        #region proxyIPConfigSettingButton_Click -  프록시IP 환경설정
        /// <summary>
        /// 프록시IP 환경설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void proxyIPConfigSettingButton_Click(object sender, EventArgs e)
        {
            frmProxyConfig form = new frmProxyConfig();
            form.ShowDialog();
        }
        #endregion

        #region passwordChangeToolStripButton_Click - 사용자의 비밀번호를 변경합니다.
        /// <summary>
        ///사용자의 비밀번호를 변경합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passwordChangeToolStripButton_Click(object sender, EventArgs e)
        {
            frmPasswordChange form = new frmPasswordChange();
            form.UserID = userInfo.UserID;
            form.ShowDialog();
        }
        #endregion

        #region ipDuppleNumericUpDown_ValueChanged -  ip 중복 허용회수 값 변경
        /// <summary>
        /// ip 중복 허용회수 값 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ipDuppleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ipDuppleAllowCount = Convert.ToInt32(ipDuppleNumericUpDown.Value);
        }
        #endregion

        #region badProxyTryConnectCountNnumericUpDown_ValueChanged - 불량 프록시IP 재시도회수
        /// <summary>
        /// 불량 프록시 재시도회수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void badProxyTryConnectCountNnumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            proxyIpTryConnectCount = Convert.ToInt32(badProxyTryConnectCountNnumericUpDown.Value);
        }
        #endregion

        #region SetAccessControlForTheRegistryKey - 레지스트리의 사용권한을 설정합니다.
        /// <summary>
        /// 레지스트리의 사용권한을 설정합니다.
        /// </summary>
        private void SetAccessControlForTheRegistryKey()
        {
            string user = Environment.UserDomainName + @"\" + Environment.UserName;
            RegistryAccessRule rule = new RegistryAccessRule(user
                                                          , RegistryRights.FullControl
                                                          , InheritanceFlags.ContainerInherit
                                                          , PropagationFlags.None
                                                          , AccessControlType.Allow);

            try
            {

                //Internet Setting 및 
                using (RegistryKey rk = Registry.LocalMachine.CreateSubKey(Common.NormalPath))
                {
                    RegistrySecurity rs = new RegistrySecurity();
                    rs.AddAccessRule(rule);
                    rk.SetAccessControl(rs);
                }

                using (RegistryKey rk = Registry.LocalMachine.CreateSubKey(Common.NormalProxyPath))
                {
                    RegistrySecurity rs = new RegistrySecurity();
                    rs.AddAccessRule(rule);
                    rk.SetAccessControl(rs);
                }

                using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(Common.NormalPath))
                {
                    RegistrySecurity rs = new RegistrySecurity();
                    rs.AddAccessRule(rule);
                    rk.SetAccessControl(rs);
                }

                using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(Common.NormalProxyPath))
                {
                    RegistrySecurity rs = new RegistrySecurity();
                    rs.AddAccessRule(rule);
                    rk.SetAccessControl(rs);
                }           

                using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Zoom"))
                {
                    RegistrySecurity rs = new RegistrySecurity();
                    rs.AddAccessRule(rule);
                    rk.SetAccessControl(rs);
                }

                using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main"))
                {
                    RegistrySecurity rs = new RegistrySecurity();
                    rs.AddAccessRule(rule);
                    rk.SetAccessControl(rs);
                }


                //64bit 시스템일경우에만 실행 합니다.
                if (Kernel32Utilscs.Is64Bit() == true)
                {
                    using (RegistryKey rk = Registry.LocalMachine.CreateSubKey(Common.Wow64Path))
                    {
                        RegistrySecurity rs = new RegistrySecurity();
                        rs.AddAccessRule(rule);
                        rk.SetAccessControl(rs);
                    }

                    using (RegistryKey rk = Registry.LocalMachine.CreateSubKey(Common.Wow64ProxyPath))
                    {
                        RegistrySecurity rs = new RegistrySecurity();
                        rs.AddAccessRule(rule);
                        rk.SetAccessControl(rs);
                    }

                    using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(Common.Wow64Path))
                    {
                        RegistrySecurity rs = new RegistrySecurity();
                        rs.AddAccessRule(rule);
                        rk.SetAccessControl(rs);
                    }

                    using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(Common.Wow64ProxyPath))
                    {
                        RegistrySecurity rs = new RegistrySecurity();
                        rs.AddAccessRule(rule);
                        rk.SetAccessControl(rs);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("레지스트리 권한을 설정", ex.Message.ToString());
            }
        } 
        #endregion
      
    }
}
