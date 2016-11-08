using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;

namespace MobileClickInstagram
{
    public partial class frmMain : Form
    {
        #region 전역변수
        //Selenium Chrome
        private ChromeOptions _chromeOptions = new ChromeOptions();
        private ChromeDriverService _chromeDriverService = ChromeDriverService.CreateDefaultService();
        private IWebDriver _chromeWebDriver;
        private Proxy _proxy = new Proxy();
        private string _articleBody;
        private string _artCssName;
        private int _maxArticleCount = 10;

        private List<string> _keyWordList = new List<string>();//검색할 키워드 리스트
        private List<string> _spamWordList = new List<string>();//스팸단어 리스트
        private List<string> _unFollowList = new List<string>();//언팔로우 대상자 리스트
        private List<string> _replyWordList = new List<string>();//언팔로우 대상자 리스트 
        //private Dictionary<string, string> _userList = new Dictionary<string, string>();

        private DataTable _keyWordTable = new DataTable();//검색할 키워드 테이블
        private DataTable _spamWordTable = new DataTable();//스팸단어 테이블
        private DataTable _unFollowTable = new DataTable();//언팔로우 대상자 테이블
        private DataTable _replyWordTable = new DataTable();//언팔로우 대상자 테이블 
        private DataTable _accountTable = new DataTable();//인스타그램 사용자 테이블

        private string loadXMLPath = string.Empty;
        private string loadXMLPathBase = AppDomain.CurrentDomain.BaseDirectory + "InstagramData.xml";
        private XmlDocument _doc = new XmlDocument();

        private bool _isLogin = false;
        private bool _isExecute = false;
        private int _searchKeywordDelayTime = 60;
        private int _executeProgramCount = 5;

        #endregion

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

        // 커서 위치 가져하기
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Point pt);

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
            Init();
        }
        #endregion

        #region Init - 초기화
        /// <summary>
        /// 초기화
        /// </summary>
        private void Init()
        {
            //기본 XML 파일이 없경을우 XML을 생성합니다.
            InstagramDataXMLValidate();

            _keyWordTable.Columns.Add("검색키워드");
            _spamWordTable.Columns.Add("스팸키워드");
            _unFollowTable.Columns.Add("언팔로우대상");
            _replyWordTable.Columns.Add("댓글키워드");
            _accountTable.Columns.Add("인스타그램계정");
            _accountTable.Columns.Add("인스타그램비번");

            GetAllListSetting();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //로컬의 맥어드레스를 가져옵니다.
            string macKey = InstagramCommon.GetMacAddress();
            InstagramCommon.instagramMacKey = macKey;

            //MessageBox.Show(InstagramCommon.instagramMacKey);

            string encKey = InstagramCommon.GetInstagramUserKey(InstagramCommon.instagramMacKey);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(encKey);

            string serverMacKey = string.Empty;
            string serverAllowCount = string.Empty;
            string serverFromDate = string.Empty;
            string serverToDate = string.Empty;

            if (xmlDoc.DocumentElement.ChildNodes.Count > 0)
            {
                serverMacKey = xmlDoc.DocumentElement.GetElementsByTagName("MACADDRESS")[0].InnerText;
                serverAllowCount = xmlDoc.DocumentElement.GetElementsByTagName("ALLOW_APP_CNT")[0].InnerText;
                serverFromDate = xmlDoc.DocumentElement.GetElementsByTagName("FROM_DT")[0].InnerText;
                serverToDate = xmlDoc.DocumentElement.GetElementsByTagName("TO_DT")[0].InnerText;                           
            }

            if (string.IsNullOrEmpty(serverMacKey) == true)
            {
                if (MessageBox.Show("유효하지않은 사용자입니다.\r\n사용자로 등록을 시작 하시겠습니까?", "등록", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MessageBox.Show("사용자PC의MacAddress값으로 등록후\r\n관리자에게 승인요청을 해야 정상 사용 가능합니다.", "등록", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmUserRegister frmReg = new frmUserRegister();
                    frmReg.ShowDialog();

                    if (frmReg.IsRegister == true)
                    {
                        MessageBox.Show("정상적으로 사용자 등록이 처리되었습니다.\r\n사용을 원하시면 관리자에게 요청 후\r\n등록된 macAddress 로 2일간 사용이 가능합니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("사용자 등록을 취소하였습니다.", "취소", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    MessageBox.Show("프로그램을 종료합니다.");
                    closeButton_Click(null, null);                        

                }
                else
                {
                    MessageBox.Show("프로그램을 종료합니다.");
                    closeButton_Click(null, null);
                }
            }
            else
            {
                int.TryParse(serverAllowCount, out _executeProgramCount);

                DateTime currentDay = DateTime.Now;
                DateTime endDay ;
                DateTime.TryParse(serverToDate, out endDay);

                if (currentDay > endDay.AddDays(1)) {
                    MessageBox.Show("사용기간이 종료되었습니다.\r\n관리자에게 사용기간 연장을 요청하세요.", "종료", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    closeButton_Click(null, null);
                }
            }

        }

        private void InstagramDataXMLValidate()
        {
            string initXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><DATA><KEYWORD></KEYWORD><USER></USER><SPAM></SPAM><UNFOLLOW></UNFOLLOW><REPLY></REPLY></DATA>";

            if (File.Exists(loadXMLPathBase) == false)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(initXML);
                doc.Save(loadXMLPathBase);
            }

            List<string> fileNameList = new List<string>();
            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            FileInfo[] fileInfo = di.GetFiles("*.xml");
            if (fileInfo.Length > 0)
            {
                foreach (FileInfo file in fileInfo)
                {
                    int value = 0;
                    string name = file.Name.Substring(0, file.Name.IndexOf("."));
                    bool isNumeric = int.TryParse(name.Substring(name.Length - 1), out value);

                    if (isNumeric)
                    {
                        fileNameList.Add(name);
                    }
                }
            }

            if (fileNameList.Count <= _executeProgramCount)
            {
                for (int i = 1; i <= _executeProgramCount; i++)
                {
                    string baseFileName = ("InstagramData") + i.ToString();
                    if (fileNameList.Contains(baseFileName) == false)
                    {
                        string destFileName = AppDomain.CurrentDomain.BaseDirectory + baseFileName + ".xml";
                        File.Copy(loadXMLPathBase, destFileName);
                        loadXMLPath = destFileName;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("허용된 개수 이상의 프로그램이 실행되었습니다.\r\n프로그램을 종료합니다.");
                closeButton_Click(null, null);
            }

            if (loadXMLPath == string.Empty)
            {
                MessageBox.Show("유효한 파일경로가 존재하지 않습니다..\r\n프로그램을 종료합니다.");
                closeButton_Click(null, null);
            }
        }
        #endregion

        #region 이전작업 히스토리
        /*
         * ListBox4 --> 언팔로우 리스트
         * ListView1 --> 스팸단어 리스트
         * ListView3 --> 프록시 리스트
         * ListView2 --> 키워드 리스트
         * ListView5 --> 덧글 리스트
           
         * 
         * jisung_kim1257/4554881kjs/30/60/100/30/30
         * FileSystem.FileOpen(1, Application.StartupPath + @"\아이디.txt", OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);
            string expression = FileSystem.LineInput(1);
            this.TextBox10.Text = Strings.Split(expression, "/", -1, CompareMethod.Binary)[0]; 아이디
            this.TextBox13.Text = Strings.Split(expression, "/", -1, CompareMethod.Binary)[1]; 패스워드
            this.TextBox1.Text = Strings.Split(expression, "/", -1, CompareMethod.Binary)[2]; 30
            this.TextBox4.Text = Strings.Split(expression, "/", -1, CompareMethod.Binary)[3]; 60
            this.TextBox8.Text = Strings.Split(expression, "/", -1, CompareMethod.Binary)[4]; 100
            this.TextBox9.Text = Strings.Split(expression, "/", -1, CompareMethod.Binary)[5]; 30
            this.TextBox7.Text = Strings.Split(expression, "/", -1, CompareMethod.Binary)[6]; 30

         ** 
         */

        #endregion

        #region loginButton_Click - 로그인 버튼 클릭
        /// <summary>
        /// 로그인 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(instaUserIdTextBox.ToString()) == true)
            {
                MessageBox.Show("인스타그램 사용자 아이디를 입력해주세요.", "입력", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.instaUserIdTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty( instaPasswordTextBox.Text.ToString()) == true)
            {
                MessageBox.Show("인스타그램 사용자 패스워드를 입력해주세요.", "취소", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.instaPasswordTextBox.Focus();
                return;
            }
            
            _unFollowList.Clear();
            _keyWordList.Clear();
            _spamWordList.Clear();
            _replyWordList.Clear();


            if (_isLogin == false)
            {
                ExecuteLoginAndUnfollowListSetting();
                _isLogin = true;
                settingOptionGroupBox.Enabled = true;
                executeButton.Enabled = true;
                loginButton.Text = "로그아웃";
            }
            else
            {
                _isLogin = false;
                _unFollowList.Clear();
                settingOptionGroupBox.Enabled = false;
                executeButton.Enabled = false;
                loginButton.Text = "로그인";
                _chromeWebDriver.Quit();
            }
        }
        #endregion

        #region executeButton_Click - 작업시작 버튼 클릭
        /// <summary>
        /// 작업시작 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void executeButton_Click(object sender, EventArgs e)
        {
            try
            {
                _isExecute = true;
                executeButton.Enabled = false;
                executeButton.Text = "실행중";
                AutoJob_BackGroundWorker();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "에러", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _isExecute = false;
                executeButton.Enabled = true;
                executeButton.Text = "작업시작";
            }
        }
        #endregion

        #region AutoJob_BackGroundWorker - 팔로잉 및 댓글 좋아요 작업을 실행합니다.
        /// <summary>
        /// 팔로잉 및 댓글 좋아요 작업을 실행합니다.
        /// </summary>
        public void AutoJob_BackGroundWorker()
        {
            _keyWordList.Clear();
            _replyWordList.Clear();
            _spamWordList.Clear();
            if (_keyWordTable.Rows.Count > 0)
            {
                foreach (DataRow dr in _keyWordTable.Rows)
                {
                    _keyWordList.Add(dr[0].ToString());
                }
            }
            else
            {
                MessageBox.Show("등록된 키워드가 없습니다.키워드를 등록해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _isExecute = false;
                executeButton.Enabled = true;
                executeButton.Text = "작업시작";
                return;
            }

            if (_spamWordTable.Rows.Count > 0)
            {
                foreach (DataRow dr in _spamWordTable.Rows)
                {
                    _spamWordList.Add(dr[0].ToString());
                }
            }
            if (_replyWordTable.Rows.Count > 0)
            {
                foreach (DataRow dr in _replyWordTable.Rows)
                {
                    _replyWordList.Add(dr[0].ToString());
                }
            }

            //1.검색 키워드 리스트를 가져옵니다.
            if (_keyWordList.Count > 0)
            {
                // 1-1.검색 키워드 리스트를 기준으로 LOOP 를 실행합니다.
                foreach (string keyword in _keyWordList)
                {
                    // 1-3.검색키워드를 기준으로 검색 실행
                    ExecuteSearch(keyword);

                    //스크롤을 맨 아래로 내립니다.
                    ScrollDown();

                    //더읽기를 클릭하여 더읽기가 있는경우에만 실행합니다.
                    if (ReadMoreClick(keyword) == true)
                    {
                        //20회 스크롤 실행
                        ScrollDown(70, 100, 20);

                        for (int i = 0; i < _maxArticleCount; i++)
                        {
                            ScrollDown(70, 100, 15);
                        }
                    }

                    // 1-4.검색결과물의 게시물을 캐치하여 게시물리스트 생성
                    List<string> articleList = GetArticleList();

                    if (articleList.Count > 0)
                    {
                        // 1-5.검색 게시물을 기준으로 loop 를 실행
                        foreach (string article in articleList)
                        {
                            //  - 검색게시물 URL 생성하기 -->"https://instagram.com"+검색게시물 아이템
                            string articleURL = "https://instagram.com" + article;
                            //  - 검색게시물 URL 이동하기
                            _chromeWebDriver.Navigate().GoToUrl(articleURL);
                            InstagramCommon.Delay(3);
                            //  - 아티클의 CSS 값 가져오기
                            GetArticleCssName();
                            //  - 아티클의 바디값 가져오기
                            GetArticleBody();
                            InstagramCommon.Delay(1);

                            //  - 아티클의 바디에 스팸문자열이 있는지 확인하기
                            if (IsCheckBodyBySpamList() == false)
                            {
                                //  - 덧글작업 , 좋아요작업 , 팔로우작업
                                // 덧글작업
                                int replyIndex = InstagramCommon.RandomKeyValue(0, _replyWordList.Count - 1);

                                string replyWord = _replyWordList[replyIndex];
                                WriteReply(replyWord);
                                // 좋아요 작업
                                LikeClick();
                                // 팔로우 작업                               
                                FollowClick();
                            }
                            else
                            {
                                //스팸키워드 존재                               
                            }
                        }
                    }

                    // 1-6.언팔로우작업
                    foreach (string unfallow in _unFollowList)
                    {
                        ExecuteUnFollow(unfallow.Replace("@", ""));
                    }

                    // 1-2.검색 키워드 1개의 작업이 끝나고 60초를 대기합니다.
                    Thread.Sleep(_searchKeywordDelayTime);
                }
            }
        }
        #endregion

        #region ExecuteLoginAndUnfollowListSetting - 로그인 및 언팔로우 리스트를 설정합니다.
        /// <summary>
        /// 로그인 실행시 처리합니다.
        /// </summary>
        private void ExecuteLoginAndUnfollowListSetting()
        {
            try
            {
                string openURL = "https://www.instagram.com/";
                WebURLOpen(openURL);

                string id = instaUserIdTextBox.Text;
                string pw = instaPasswordTextBox.Text;

                if (string.IsNullOrEmpty(id) == true)
                {
                    MessageBox.Show("아이디를 입력해주세요");
                    instaUserIdTextBox.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(pw) == true)
                {
                    MessageBox.Show("패스워드를 입력해주세요");
                    instaPasswordTextBox.Focus();
                    return;
                }

                //인스타그램에 로그인 처리를 실행합니다.
                InstagramLogin(id, pw);
                InstagramCommon.Delay(1);

                //언팔로어 리스트가 없을경우 언팔로어 페이지로 가서 언팔로어 리스트 가져와서 언팔로어 리스트에 추가
                if (_unFollowTable.Rows.Count == 0)
                {
                    this._chromeWebDriver.Navigate().GoToUrl("https://unfollowers.com/");
                    GetUnFollow(id);
                }
                else
                {
                    //언팔로어 리스트가 있는경우 해당 리스트를 언팔로우 리스트에 추가한다.
                    if (_unFollowTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in _unFollowTable.Rows)
                        {
                            _unFollowList.Add(dr[0].ToString());
                        }
                    }
                }
                //인스타그램 메인페이지로 이동합니다.
                this._chromeWebDriver.Navigate().GoToUrl(openURL);
            }
            catch (Exception ex)
            {                
            }
        }
        #endregion

        #region ReadMoreClick - 더읽기 버튼 클릭 합니다.
        /// <summary>
        /// 더읽기 버튼 클릭
        /// 해시태그 검색 후 인스타그램의 맨 마지막의 더읽기 버튼을 클릭하여 게시리스트를 더 많이 가져올 수 있도록 처리
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public bool ReadMoreClick(string keyword)
        {
            bool flag = true;
            try
            {
                var hrefLinkElement = this._chromeWebDriver.FindElement(By.XPath("//article[@class='" + GetArticleCssNameByLogin() + "']"));
                hrefLinkElement.FindElement(By.XPath(string.Format("//a[contains(@href,'{0}')]", string.Format("explore/tags/{0}/", keyword)))).Click();

                #region MyRegion
                //  //article[@class='" + this._artCssName + "']/div[2]/div[3]/a
                //  //article[@class='" + this._artCssName + "']/div[2]/div[3]
                //   var hrefLinkElement = driver.FindElementByXPath(string.Format("//a[contains(@href,'{0}')]", "x_fsn=cali:"));
                //  #react-root > section > main > article > div:nth-child(4) > div._pupj3 > a

                // //*[@id="react-root"]/section/main/article/div[2]/div[1]/div/form/button
                // //*[@id="react-root"]/section/main/article/div[2]/div[1]/div/form/div[6]/button

                #endregion
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                flag = false;
            }

            return flag;
        }
        #endregion

        #region WriteReply - 댓글을 작성합니다.
        /// <summary>
        /// 댓글을 작성합니다.
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        public bool WriteReply(string reply)
        {
            bool flag = true;

            try
            {
                InstagramCommon.Delay(5);
                this._chromeWebDriver.FindElement(By.XPath("//article[@class='" + this._artCssName + "']/div[2]/section[2]/form/input")).Click();
                this._chromeWebDriver.FindElement(By.XPath("//article[@class='" + this._artCssName + "']/div[2]/section[2]/form/input")).SendKeys(reply);
                InstagramCommon.Delay(5);
                this._chromeWebDriver.FindElement(By.XPath("//article[@class='" + this._artCssName + "']/div[2]/section[2]/form/input")).SendKeys(OpenQA.Selenium.Keys.Enter);
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }
        #endregion

        #region FollowClick - 팔로우버튼을 클릭합니다.
        /// <summary>
        /// 팔로우버튼을 클릭합니다.
        /// 이미 팔로잉상태가 아닌경우만 처리합니다.
        /// </summary>
        /// <returns></returns>
        public bool FollowClick()
        {
            bool flag = true;
            try
            {
                string fallowButtonText = this._chromeWebDriver.FindElement(By.XPath("//article[@class='" + _artCssName + "']/header/span/button")).Text;
                if (fallowButtonText != "팔로잉")
                {
                    //아티클의  css 값으로 검색하여 버튼을 클릭
                    this._chromeWebDriver.FindElement(By.XPath("//article[@class='" + _artCssName + "']/header/span/button")).Click();
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }
        #endregion

        #region LikeClick - 좋아요 버튼을 클릭합니다.
        /// <summary>
        /// 좋아요 버튼을 클릭합니다.
        /// </summary>
        /// <returns></returns>
        public bool LikeClick(bool isToggle)
        {
            bool flag = true;
            try
            {
                InstagramCommon.Delay(5);
                if (isToggle)
                {
                    this._chromeWebDriver.FindElement(By.XPath("//article[@class='" + this._artCssName + "']/div[2]/section[2]/a")).Click();
                }
                else
                {
                    string likeInnerText = this._chromeWebDriver.FindElement(By.XPath("//article[@class='" + this._artCssName + "']/div[2]/section[2]/a")).Text;
                    if (likeInnerText.Contains("취소") == false)
                    {
                        this._chromeWebDriver.FindElement(By.XPath("//article[@class='" + this._artCssName + "']/div[2]/section[2]/a")).Click();
                    }
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }

        public bool LikeClick()
        {
            return LikeClick(false);
        }

        #endregion

        #region InstagramLogin - 인스타그램 로그인을 실행합니다.
        /// <summary>
        /// 인스타그램 로그인을 실행합니다.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PW"></param>
        /// <returns></returns>
        public bool InstagramLogin(string ID, string PW)
        {
            bool flag = true;
            try
            {
                InstagramCommon.Delay(5);
                // //*[@id="react-root"]/section/main/article/div[2]/div[2]/p/a
                // //*[@id="react-root"]/section/main/article/div[2]/div[2]/p/a
                // #react-root > section > main > article > div._jxjmg > div:nth-child(1) > div > form > div:nth-child(4) > input

                // this._chromeWebDriver.FindElement(By.XPath("//input[@name='email' and @placeholder='이메일']"));
                try
                {
                    //가입화면으로 변경된 경우에는 일반 로그인 화면으로 전환하기 위해 확인합니다.
                    var emailElement = this._chromeWebDriver.FindElement(By.XPath("//input[@name='email' and @placeholder='이메일']"));
                    this._chromeWebDriver.FindElement(By.XPath("//article[@class='" + GetArticleCssNameByLogin() + "']/div[2]/div[2]/p/a")).Click();
                    InstagramCommon.Delay(1);
                }
                catch (NoSuchElementException ex)
                {
                    //엘리먼트를 검색하지 못했을경우 처리(로그남김)                  
                }

                this._chromeWebDriver.FindElement(By.Name("username")).Click();
                this._chromeWebDriver.FindElement(By.Name("username")).SendKeys(ID);
                this._chromeWebDriver.FindElement(By.XPath("//input[@name='password' and @placeholder='비밀번호']")).SendKeys(PW);
                string str = this._chromeWebDriver.PageSource.Split(new string[] { "<button class=\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
                str = str.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)[0];

                this._chromeWebDriver.FindElement(By.XPath("//button[@class='" + str + "']")).Click();
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }
        #endregion

        #region ScrollDown - 스크롤 다운 실행
        /// <summary>
        /// 스크롤 다운 실행
        /// </summary>
        /// <param name="val">1회스크롤값 - 로 설정할 경우 위쪽으로 올라감</param>
        /// <param name="tick">1회 실행시 시간</param>
        /// <param name="scrollCount">스크롤횟수</param>
        public void ScrollDown(int val, int tick, int scrollCount)
        {
            IJavaScriptExecutor driver = (IJavaScriptExecutor)this._chromeWebDriver;
            object[] args = new object[] { "" };
            if (scrollCount > 0)
            {
                //scrollCount 만큼 실행한다.
                for (int i = 0; i < scrollCount; i++)
                {
                    driver.ExecuteScript(string.Format("window.scrollBy(0,{0})", val.ToString()), args);
                    //tick 만큼 지연후 실행
                    if (tick > 0) Thread.Sleep(tick);
                }
            }
            else
            {
                //scroll count 가 0일경우 1회만 실행한다.               
                driver.ExecuteScript(string.Format("window.scrollBy(0,{0})", val.ToString()), args);
                //tick 만큼 지연후 실행
                if (tick > 0) Thread.Sleep(tick);
            }
        }
        public void ScrollDown(int val, int tick)
        {
            ScrollDown(val, tick, 0);
        }
        public void ScrollDown(int val)
        {
            ScrollDown(val);
        }

        /// <summary>
        /// 맨 아래로 스크롤 실행
        /// </summary>
        public void ScrollDown()
        {
            IJavaScriptExecutor driver = (IJavaScriptExecutor)this._chromeWebDriver;
            object[] args = new object[] { "" };
            driver.ExecuteScript("window.scrollBy(0,9999999)", args);
        }
        #endregion

        #region WebOpen - 웹을 오픈합니다.
        /// <summary>
        /// 웹을 오픈합니다.
        /// </summary>
        /// <param name="openURL"></param>
        /// <param name="IP"></param>
        /// <returns></returns>
        public bool WebURLOpen(string openURL)
        {
            bool flag = true;

            try
            {
                //시크릿모드로 실행
                this._chromeOptions.AddArgument("-incognito");
                //프록시 IP 가 있는경우 프록시 IP 로 실행

                _chromeWebDriver = new ChromeDriver(_chromeDriverService, _chromeOptions);
                _chromeWebDriver.Navigate().GoToUrl(openURL);

                ProcessForMinize();//cmd 창을 숨김니다.
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;

        }
        #endregion

        #region ExecuteUnFollow - 개별 언팔로우를 실행합니다.
        /// <summary>
        /// 개별 언팔로우를 실행합니다.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool ExecuteUnFollow(string ID)
        {
            bool flag = true;
            try
            {
                //ID 에 해당하는 페이지를 찾아가서 팔로잉 이있을경우 팔로잉을 클릭하여 언팔로우 처리를 합니다.
                this._chromeWebDriver.Navigate().GoToUrl("https://www.instagram.com/" + ID + "/");
                InstagramCommon.Delay(2);
                string tmpSource = this._chromeWebDriver.PageSource.Split(new string[] { "<button class=\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
                tmpSource = tmpSource.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)[0];

                //팔로잉이 있을경우는 이미 팔로우 하고 있는경우이므로 다시 클릭하여 언팔로우 처리를합니다.
                if (this._chromeWebDriver.FindElement(By.XPath("//button[@class='" + tmpSource + "']")).GetAttribute("outerText").IndexOf("팔로잉") >= 0)
                {
                    this._chromeWebDriver.FindElement(By.XPath("//button[@class='" + tmpSource + "']")).Click();
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }
        #endregion

        #region GetUnFollow - 언팔로우할 리스트를 전역 리스트에 저장합니다.
        /// <summary>
        /// 언팔로우할 리스트를 전역 리스트에 저장합니다.
        /// </summary>
        /// <param name="loginId"></param>
        public void GetUnFollow(string loginId)
        {
            try
            {
                ///html/body/div/div[1]/div[1]/div/div/div[3]/div/a[2]
                _chromeWebDriver.FindElement(By.XPath("//a[@class='instagram signin-button']")).Click();
                InstagramCommon.Delay(1);

                _chromeWebDriver.Navigate().GoToUrl("https://app.statusbrew.com/");
                InstagramCommon.Delay(1);

                // 로그인한 사용자의 아이템을 클릭할수 있도록 확인 필요
                // /html/body/ui-view/div/md-content/md-content/md-content/section/md-list/md-list-item[1]/md-card/a
                //_chromeWebDriver.FindElement(By.XPath("//md-content/md-content/md-content/section/md-list/md-list-item[1]/md-card/a")).Click();

                //로그인한 사용자를 확인하여 해당  Element 를 클릭할 수 있도록 합니다.
                IWebElement listElement = _chromeWebDriver.FindElement(By.XPath("//md-content/md-content/md-content/section/md-list"));
                System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> elementList = listElement.FindElements(By.TagName("md-list-item"));
                if (elementList.Count > 0)
                {
                    bool isUser = false;
                    foreach (IWebElement el in elementList)
                    {
                        if (el.Text.Contains(loginId) == true)
                        {
                            isUser = true;
                            el.FindElement(By.XPath("//md-card/a")).Click();
                            break;
                        }
                    }

                    //사용자가 있을경우에 처리합니다.
                    if (isUser == true)
                    {
                        //스크롤을 맽 밑으로 내리고 초동안 머무른다.
                        ScrollDown(500, 1000, 20);

                        // 맛팔하지 않은 리스트를 언팔 리스트에 저장합니다.             
                        if (_chromeWebDriver.FindElement(By.XPath("//*[@id=\"users_list\"]")).FindElements(By.ClassName("username")).Count > 0)
                        {
                            _unFollowList.Clear();
                            foreach (IWebElement element in _chromeWebDriver.FindElement(By.XPath("//*[@id=\"users_list\"]")).FindElements(By.ClassName("username")))
                            {
                                string uf_userName = element.Text;
                                _unFollowList.Add(uf_userName);

                            }
                        }
                    }
                }


                #region MyRegion
                /*
                //사인
                _chromeWebDriver.FindElement(By.XPath("//a[@class='instagram signin-button']")).Click();
                InstagramCommon.Dela2(1);
                //언팔로우 클릭
                _chromeWebDriver.FindElement(By.XPath("//ul[@class='uf-navbar-nav nav']/li[2]/a/span[2]")).Click();
                InstagramCommon.Dela2(1);

                //60초동안 delay
                for (int i = 0; i < 60; i++)
                {
                    this.ScrollDown();
                    InstagramCommon.Dela2(1);
                }

                InstagramCommon.Dela2(5);

                string[] array = _chromeWebDriver.PageSource.Split(new string[] { "<span class=\"username\"" }, StringSplitOptions.RemoveEmptyEntries);
                _unFollowList.Clear();

                if (array.Length > 0)
                {
                    foreach (string unfallowId in array)
                    {
                        string tempUnfallow = unfallowId.Split(new string[] { "@" }, StringSplitOptions.RemoveEmptyEntries)[1];
                        tempUnfallow = tempUnfallow.Split(new string[] { "<" }, StringSplitOptions.RemoveEmptyEntries)[0];
                        _unFollowList.Add(tempUnfallow);
                        Application.DoEvents();
                    }
                }
                 * 
                 * */

                #endregion
            }
            catch (Exception ex)
            {
            }

        }
        #endregion

        #region IsCheckBodyBySpamList - 인스타그램 본문에 스팸문자가 있는지 확인합니다.
        /// <summary>
        /// 인스타그램 본문에 스팸문자가 있는지 확인합니다.
        /// </summary>
        /// <returns></returns>
        public bool IsCheckBodyBySpamList()
        {
            bool value = false;
            foreach (string word in _spamWordList)
            {
                if (_articleBody.Contains(word))
                {
                    value = true;
                    break;
                }
            }
            return value;
        }
        #endregion

        #region GetArticleCssName - 아티클의 class(css) 값을 전역변수에 설정합니다.
        /// <summary>
        /// 아티클의 class(css) 값을 전역변수에 설정합니다.
        /// </summary>
        /// <returns></returns>
        public bool GetArticleCssName()
        {
            bool flag = true;
            try
            {
                string pageSource = this._chromeWebDriver.PageSource;
                string tmpSource = pageSource.Split(new string[] { "<article class=\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
                tmpSource = tmpSource.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)[0];
                _artCssName = tmpSource;
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }
        #endregion

        #region GetArticleCssNameByLogin - 아티클의  class(css) 값을 가져옵니다.
        /// <summary>
        /// 아티클의  class(css) 값을 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public string GetArticleCssNameByLogin()
        {
            string artCssName = "";
            try
            {
                string pageSource = this._chromeWebDriver.PageSource;
                string tmpSource = pageSource.Split(new string[] { "<article class=\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
                tmpSource = tmpSource.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)[0];
                artCssName = tmpSource;
            }
            catch (Exception ex)
            {
                artCssName = "";
            }

            return artCssName;
        }
        #endregion

        #region GetArticleBody - 아티클의 본문을 가져옵니다.(타이틀만 가져옴)
        /// <summary>
        /// 아티클의 본문을 가져옵니다.(타이틀만 가져옴)
        /// </summary>
        /// <returns></returns>
        public bool GetArticleBody()
        {
            bool flag = true;
            try
            {
                string pageSource = this._chromeWebDriver.PageSource;
                string tmpSource = pageSource.Split(new string[] { "<title>" }, StringSplitOptions.RemoveEmptyEntries)[1];
                tmpSource = tmpSource.Split(new string[] { "</title>" }, StringSplitOptions.RemoveEmptyEntries)[0];
                _articleBody = tmpSource;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }
        #endregion

        #region ExecuteSearch - 해시 태그로 게시물을 검색 검색합니다.
        /// <summary>
        /// 해시 태그로 게시물을 검색 검색합니다.
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public bool ExecuteSearch(string searchText)
        {
            bool flag = true;
            try
            {
                this._chromeWebDriver.Navigate().GoToUrl("https://instagram.com/explore/tags/" + searchText + "/");
                InstagramCommon.Delay(5);
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;

        }
        #endregion

        #region GetArticleList - 검색된 게시물의 리스트를 생성합니다.
        /// <summary>
        /// 검색된 게시물의 리스트를 생성합니다.
        /// </summary>
        /// <returns></returns>
        public List<string> GetArticleList()
        {
            List<string> articleList = new List<string>();
            try
            {
                InstagramCommon.Delay(5);
                //href="/p/BBC6N73FCr_/?tagged=%EB%8F%99%ED%83%84" 
                string[] array = this._chromeWebDriver.PageSource.Split(new string[] { "href=\"/p" }, StringSplitOptions.RemoveEmptyEntries);

                if (array.Length > 1)
                {
                    //split 하는 경우 1번째는 가비지데이터라 2번째 파일부터 처리한다.
                    for (int i = 1; i < array.Length; i++)
                    {
                        int endIndex = array[i].IndexOf("\"");
                        int startIndex = array[i].IndexOf("/");
                        string tempItem = array[i].Substring(startIndex + 1, endIndex - startIndex - 1);
                        tempItem = string.Format("/p/{0}", tempItem);
                        articleList.Add(tempItem);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return articleList;
        }
        #endregion

        #region InLike - 사용안함
        ////사용안함
        //public bool InLike()
        //{
        //    bool flag;

        //    InstagramCommon.Dela2(3);
        //    _chromeWebDriver.FindElement(By.XPath("//a[@class='-cx-PRIVATE-PostInfo__likeButton -cx-PRIVATE-LikeButton__root -cx-PRIVATE-Util__hideText coreSpriteHeartOpen']")).Click();
        //    flag = true;
        //    return flag;
        //} 
        #endregion

        #region Event Handler
        private void instaUserIdTextBox_Click(object sender, EventArgs e)
        {
            instaUserIdTextBox.Text = "";
        }

        private void instaPasswordTextBox_Click(object sender, EventArgs e)
        {
            instaPasswordTextBox.Text = "";
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(loadXMLPath) == true)
                {
                    File.Delete(loadXMLPath);
                }

                if (_chromeWebDriver != null)
                {
                    this._chromeWebDriver.Quit();
                }
                this.Close();
            }
            catch (Exception ex) { }
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
            Process[] processes = Process.GetProcessesByName("chrome");
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

        public void MouseWheelDownScrolling()
        {
            // 마우스 포지션을 설정 합니다.
            SetCursorPos(200, 200);
            for (int i = 300; i >= 0; i--)
            {
                mouse_event((int)MouseEventFlags.WHEEL, 0, 0, -999, 0);
            }
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

        #region scrollNumericUpDown_ValueChanged - 스크롤 횟수를 설정합니다.
        /// <summary>
        /// 스크롤 횟수를 설정합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scrollNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _maxArticleCount = Convert.ToInt32(scrollNumericUpDown.Value);
        }
        #endregion

        private void loginAndJobListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (loginAndJobListComboBox.SelectedIndex != -1)
                {
                    instaUserIdTextBox.Text = ((DataRowView)loginAndJobListComboBox.SelectedItem).Row[0].ToString();
                    instaPasswordTextBox.Text = ((DataRowView)loginAndJobListComboBox.SelectedItem).Row[1].ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void GetAllListSetting()
        {
            try
            {
                _doc = new XmlDocument();
                _doc.Load(loadXMLPath);

                XmlNode account = _doc.SelectSingleNode("//DATA/USER");
                XmlNode keyword = _doc.SelectSingleNode("//DATA/KEYWORD");
                XmlNode spamKeyword = _doc.SelectSingleNode("//DATA/SPAM");
                XmlNode unFollow = _doc.SelectSingleNode("//DATA/UNFOLLOW");
                XmlNode replyKeyword = _doc.SelectSingleNode("//DATA/REPLY");

                _accountTable.Clear();
                _keyWordTable.Clear();
                _spamWordTable.Clear();
                _unFollowTable.Clear();
                _replyWordTable.Clear();

                if (account.ChildNodes.Count > 0)
                {
                    XmlNodeList nodeList = account.ChildNodes;
                    foreach (XmlNode node in nodeList)
                    {
                        string id = node.ChildNodes[0].InnerText;
                        string pass = node.ChildNodes[1].InnerText;
                        _accountTable.LoadDataRow(new string[] { id, pass }, true);
                    }
                    loginAndJobListComboBox.DataSource = _accountTable;
                    loginAndJobListComboBox.DisplayMember = "인스타그램계정";
                    loginAndJobListComboBox.ValueMember = "인스타그램비번";
                }

                if (keyword.ChildNodes.Count > 0)
                {
                    XmlNodeList nodeList = keyword.ChildNodes;
                    foreach (XmlNode node in nodeList) _keyWordTable.LoadDataRow(new string[] { node.ChildNodes[0].InnerText }, true);
                }

                if (spamKeyword.ChildNodes.Count > 0)
                {
                    XmlNodeList nodeList = spamKeyword.ChildNodes;
                    foreach (XmlNode node in nodeList) _spamWordTable.LoadDataRow(new string[] { node.ChildNodes[0].InnerText }, true);
                }

                if (unFollow.ChildNodes.Count > 0)
                {
                    XmlNodeList nodeList = unFollow.ChildNodes;
                    foreach (XmlNode node in nodeList) _unFollowTable.LoadDataRow(new string[] { node.ChildNodes[0].InnerText }, true);
                }

                if (replyKeyword.ChildNodes.Count > 0)
                {
                    XmlNodeList nodeList = replyKeyword.ChildNodes;
                    foreach (XmlNode node in nodeList) _replyWordTable.LoadDataRow(new string[] { node.ChildNodes[0].InnerText }, true);
                }

                accountDataGridView.DataSource = _accountTable;
                keywordDataGridView.DataSource = _keyWordTable;
                spamDataGridView.DataSource = _spamWordTable;
                unFollowDataGridView.DataSource = _unFollowTable;
                replyKeywordDataGridView.DataSource = _replyWordTable;
            }
            catch (Exception ex)
            {
            }

        }

        private void AddXMLData(InstagramCommon.InstagramTableType executeType, string value1, string value2)
        {
            AddXMLData(executeType, value1, value2, true);
        }
        private void AddXMLData(InstagramCommon.InstagramTableType executeType, string value1, string value2, bool isReBind)
        {
            try
            {
                _doc.Load(loadXMLPath);

                XmlNode objNode = _doc.CreateElement("ROW");
                XmlNode subNode1;
                XmlNode subNode2;

                switch (executeType)
                {
                    case InstagramCommon.InstagramTableType.Account:
                        XmlNode account = _doc.SelectSingleNode("//DATA/USER");
                        subNode1 = _doc.CreateElement("ID");
                        subNode1.InnerText = value1;
                        subNode2 = _doc.CreateElement("PASS");
                        subNode2.InnerText = value2;

                        account.AppendChild(objNode);
                        objNode.AppendChild(subNode1);
                        objNode.AppendChild(subNode2);

                        break;
                    case InstagramCommon.InstagramTableType.SearchKeyword:
                        XmlNode keyword = _doc.SelectSingleNode("//DATA/KEYWORD");
                        subNode1 = _doc.CreateElement("VALUE");
                        subNode1.InnerText = value1;

                        keyword.AppendChild(objNode);
                        objNode.AppendChild(subNode1);
                        break;
                    case InstagramCommon.InstagramTableType.SpamKeyword:
                        XmlNode spamKeyword = _doc.SelectSingleNode("//DATA/SPAM");
                        subNode1 = _doc.CreateElement("VALUE");
                        subNode1.InnerText = value1;

                        spamKeyword.AppendChild(objNode);
                        objNode.AppendChild(subNode1);
                        break;
                    case InstagramCommon.InstagramTableType.ReplyKwyword:
                        XmlNode replyKwyword = _doc.SelectSingleNode("//DATA/REPLY");
                        subNode1 = _doc.CreateElement("VALUE");
                        subNode1.InnerText = value1;

                        replyKwyword.AppendChild(objNode);
                        objNode.AppendChild(subNode1);
                        break;
                    case InstagramCommon.InstagramTableType.Unfollow:
                        XmlNode unFollow = _doc.SelectSingleNode("//DATA/UNFOLLOW");
                        subNode1 = _doc.CreateElement("VALUE");
                        subNode1.InnerText = value1;

                        unFollow.AppendChild(objNode);
                        objNode.AppendChild(subNode1);
                        break;
                    default: break;
                }
                _doc.Save(loadXMLPath);
                if (isReBind)
                {
                    GetAllListSetting();
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void DeleteXMLData(InstagramCommon.InstagramTableType executeType, string value1)
        {
            DeleteXMLData(executeType, value1, true);
        }
        private void DeleteXMLData(InstagramCommon.InstagramTableType executeType, string value1, bool isReBind)
        {
            try
            {
                _doc.Load(loadXMLPath);
                XmlNode objNode = _doc.CreateElement("ROW");
                int i = 0;
                switch (executeType)
                {
                    case InstagramCommon.InstagramTableType.Account:
                        XmlNode account = _doc.SelectSingleNode("//DATA/USER");
                        if (account.ChildNodes.Count > 0)
                        {
                            XmlNodeList nodeList = account.ChildNodes;
                            foreach (XmlNode node in nodeList)
                            {
                                string id = node.ChildNodes[0].InnerText;
                                if (value1 == id)
                                {
                                    node.ParentNode.RemoveChild(node);
                                    break;
                                }
                                i++;
                            }
                        }
                        break;
                    case InstagramCommon.InstagramTableType.SearchKeyword:
                        XmlNode keyword = _doc.SelectSingleNode("//DATA/KEYWORD");
                        if (keyword.ChildNodes.Count > 0)
                        {
                            XmlNodeList nodeList = keyword.ChildNodes;
                            foreach (XmlNode node in nodeList)
                            {
                                string id = node.ChildNodes[0].InnerText;
                                if (value1 == id)
                                {
                                    node.ParentNode.RemoveChild(node);
                                    break;
                                }
                                i++;
                            }
                        }
                        break;
                    case InstagramCommon.InstagramTableType.SpamKeyword:
                        XmlNode spamKeyword = _doc.SelectSingleNode("//DATA/SPAM");
                        if (spamKeyword.ChildNodes.Count > 0)
                        {
                            XmlNodeList nodeList = spamKeyword.ChildNodes;
                            foreach (XmlNode node in nodeList)
                            {
                                string id = node.ChildNodes[0].InnerText;
                                if (value1 == id)
                                {
                                    node.ParentNode.RemoveChild(node);
                                    break;
                                }
                                i++;
                            }
                        }
                        break;
                    case InstagramCommon.InstagramTableType.ReplyKwyword:
                        XmlNode replyKwyword = _doc.SelectSingleNode("//DATA/REPLY");
                        if (replyKwyword.ChildNodes.Count > 0)
                        {
                            XmlNodeList nodeList = replyKwyword.ChildNodes;
                            foreach (XmlNode node in nodeList)
                            {
                                string id = node.ChildNodes[0].InnerText;
                                if (value1 == id)
                                {
                                    node.ParentNode.RemoveChild(node);
                                    break;
                                }
                                i++;
                            }
                        }
                        break;
                    case InstagramCommon.InstagramTableType.Unfollow:
                        XmlNode unFollow = _doc.SelectSingleNode("//DATA/UNFOLLOW");
                        if (unFollow.ChildNodes.Count > 0)
                        {
                            XmlNodeList nodeList = unFollow.ChildNodes;
                            foreach (XmlNode node in nodeList)
                            {
                                string id = node.ChildNodes[0].InnerText;
                                if (value1 == id)
                                {
                                    node.ParentNode.RemoveChild(node);
                                    break;
                                }
                                i++;
                            }
                        }
                        break;
                    default: break;
                }
                _doc.Save(loadXMLPath);

                if (isReBind)
                {
                    GetAllListSetting();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void searchKeywordDelayTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _searchKeywordDelayTime = Convert.ToInt32(searchKeywordDelayTimeNumericUpDown.Value);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                string objName = ((Button)sender).Name;
                string value1 = string.Empty;
                string value2 = null;
                InstagramCommon.InstagramTableType tableType = InstagramCommon.InstagramTableType.None;

                switch (objName)
                {
                    case "addAccountButton":
                        tableType = InstagramCommon.InstagramTableType.Account;
                        value1 = userIDAccountTextBox.Text;
                        value2 = passAccountTextBox.Text;
                        break;
                    case "addKeywordButton":
                        tableType = InstagramCommon.InstagramTableType.SearchKeyword;
                        value1 = searchKeywordTextBox.Text;
                        break;
                    case "addSpamKeywordButton":
                        tableType = InstagramCommon.InstagramTableType.SpamKeyword;
                        value1 = spamKeywordTextBox.Text;
                        break;
                    case "addUnfollowButton":
                        tableType = InstagramCommon.InstagramTableType.Unfollow;
                        value1 = unFollowTextBox.Text;
                        break;
                    case "addReplyKeywordButton":
                        tableType = InstagramCommon.InstagramTableType.ReplyKwyword;
                        value1 = replyKeytextBox.Text;
                        break;
                }

                //테이블타입이 없거나 추가할 값이 없는경우 처리
                if (tableType == InstagramCommon.InstagramTableType.None || string.IsNullOrEmpty(value1) == true)
                {
                    MessageBox.Show("추가할 값이 선택 되지 않았습니다. ");
                    return;
                }
                else
                {
                    AddXMLData(tableType, value1, value2);
                    MessageBox.Show("정상적으로 추가되었습니다.");
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void DelButton_Click(object sender, EventArgs e)
        {
            string objName = ((Button)sender).Name;
            string value1 = string.Empty;
            InstagramCommon.InstagramTableType tableType = InstagramCommon.InstagramTableType.None;

            switch (objName)
            {
                case "delAccountButton":
                    tableType = InstagramCommon.InstagramTableType.Account;
                    value1 = userIDAccountTextBox.Text;
                    break;
                case "delKeywordButton":
                    tableType = InstagramCommon.InstagramTableType.SearchKeyword;
                    value1 = searchKeywordTextBox.Text;
                    break;
                case "delSpamKeywordButton":
                    tableType = InstagramCommon.InstagramTableType.SpamKeyword;
                    value1 = spamKeywordTextBox.Text;
                    break;
                case "delUnfollowButton":
                    tableType = InstagramCommon.InstagramTableType.Unfollow;
                    value1 = unFollowTextBox.Text;
                    break;
                case "delReplyKeywordButton":
                    tableType = InstagramCommon.InstagramTableType.ReplyKwyword;
                    value1 = replyKeytextBox.Text;
                    break;
            }

            //테이블타입이 없거나 추가할 값이 없는경우 처리
            if (tableType == InstagramCommon.InstagramTableType.None || string.IsNullOrEmpty(value1) == true)
            {
                MessageBox.Show("삭제할 값이 선택 되지 않았습니다. ");
                return;
            }
            else
            {
                DeleteXMLData(tableType, value1);
                MessageBox.Show("정상적으로 삭제되었습니다.");
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (File.Exists(loadXMLPath) == true)
            {
                File.Delete(loadXMLPath);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(loadXMLPath) == true)
            {
                File.Delete(loadXMLPath);
            }
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string objName = ((DataGridView)sender).Name;

            switch (objName)
            {
                case "accountDataGridView":
                    userIDAccountTextBox.Text = accountDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                    passAccountTextBox.Text = accountDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                    break;
                case "keywordDataGridView":
                    searchKeywordTextBox.Text = keywordDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                    break;
                case "spamDataGridView":
                    spamKeywordTextBox.Text = spamDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                    break;
                case "unFollowDataGridView":
                    unFollowTextBox.Text = unFollowDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                    break;
                case "replyKeywordDataGridView":
                    replyKeytextBox.Text = replyKeywordDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                    break;
            }
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            try
            {

                string filePath = string.Empty;
                DataTable dt = new DataTable();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog1.FileName;

                    dt.Columns.Add("VALUE");

                    using (StreamReader reader = new StreamReader(filePath, Encoding.Default))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (string.IsNullOrEmpty(line) == false)
                            {
                                dt.LoadDataRow(new string[] { line }, true);
                            }
                        }
                    }
                }

                string objName = ((Button)sender).Name;
                string value1 = string.Empty;
                string value2 = string.Empty;
                InstagramCommon.InstagramTableType tableType = InstagramCommon.InstagramTableType.None;

                if (dt.Rows.Count > 0)
                {
                    switch (objName)
                    {
                        case "accountOpenFileButton":
                            tableType = InstagramCommon.InstagramTableType.Account;
                            foreach (DataRow dr in dt.Rows)
                            {
                                string[] arr = dr[0].ToString().Split(new string[] { "," }, StringSplitOptions.None);
                                AddXMLData(tableType, arr[0], arr[2], false);
                            }
                            break;
                        case "keywordOpenFileButton":
                            tableType = InstagramCommon.InstagramTableType.SearchKeyword;
                            foreach (DataRow dr in dt.Rows) AddXMLData(tableType, dr[0].ToString(), null, false);
                            break;
                        case "spamOpenFileButton":
                            tableType = InstagramCommon.InstagramTableType.SpamKeyword;
                            foreach (DataRow dr in dt.Rows) AddXMLData(tableType, dr[0].ToString(), null, false);
                            break;
                        case "unfollowOpenFileButton":
                            tableType = InstagramCommon.InstagramTableType.Unfollow;
                            foreach (DataRow dr in dt.Rows) AddXMLData(tableType, dr[0].ToString(), null, false);
                            break;
                        case "replyOpenFileButton":
                            tableType = InstagramCommon.InstagramTableType.ReplyKwyword;
                            foreach (DataRow dr in dt.Rows) AddXMLData(tableType, dr[0].ToString(), null, false);
                            break;
                    }

                    GetAllListSetting();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void AllDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string objName = ((Button)sender).Name;
                _doc.Load(loadXMLPath);

                switch (objName)
                {
                    case "accountAllDeleteButton":
                        XmlNode account = _doc.SelectSingleNode("//DATA/USER");
                        if (account.ChildNodes.Count > 0)
                        {
                            account.RemoveAll();
                        }
                        break;
                    case "keywordAllDeleteButton":
                        XmlNode keyword = _doc.SelectSingleNode("//DATA/KEYWORD");
                        if (keyword.ChildNodes.Count > 0)
                        {
                            keyword.RemoveAll();
                        }
                        break;
                    case "spamAllDeleteButton":
                        XmlNode spamKeyword = _doc.SelectSingleNode("//DATA/SPAM");
                        if (spamKeyword.ChildNodes.Count > 0)
                        {
                            spamKeyword.RemoveAll();
                        }
                        break;
                    case "replyAllDeleteButton":
                        XmlNode replyKeyword = _doc.SelectSingleNode("//DATA/REPLY");
                        if (replyKeyword.ChildNodes.Count > 0)
                        {
                            replyKeyword.RemoveAll();
                        }
                        break;
                    case "unfollowAllDeleteButton":
                        XmlNode unFollow = _doc.SelectSingleNode("//DATA/UNFOLLOW");
                        if (unFollow.ChildNodes.Count > 0)
                        {
                            unFollow.RemoveAll();
                        }
                        break;
                    default: break;
                }
                _doc.Save(loadXMLPath);


                GetAllListSetting();
            }
            catch (Exception ex)
            {
            }
        }

        #region ProcessForMinize - chromedriver 프로세서를 최소화 합니다.

        /// <summary>
        /// IEDriverServer 프로세서를 최소화 합니다.
        /// </summary>
        private void ProcessForMinize()
        {
            Process[] processes = Process.GetProcessesByName("chromedriver");
            foreach (Process proc in processes)
            {
                // 윈도우 핸들러
                IntPtr procHandler = FindWindow(null, proc.MainWindowTitle);
                // 활성화
                ShowWindowAsync(procHandler, Convert.ToInt32(WNDSTATE.SW_HIDE));
            }
        }
        #endregion



    }
}
