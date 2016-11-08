using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Security.Principal;
using System.Diagnostics;
using DBHelper.Base;
using DBHelper.SQLServer;
using System.IO;
using System.Data;
using DBHelper;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace MobiClick
{
    class Common
    {
        #region 전역변수

        public static string basicWebDomain = "www.woobinsoft.kr";
        public static string subWebDomain = "woobinsoft.cafe24.com";
        public static string baseInitKey = "2gwgegrgdgehru475iwk5o69837462gs";

        public static string Wow64Path = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Internet Settings\5.0\User Agent";
        public static string NormalPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\5.0\User Agent";

        public static string Wow64ProxyPath = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Internet Settings";
        public static string NormalProxyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings";

        public static string regKeyRoot = string.Empty;
        public static string regKeyProxyRoot = string.Empty;
        public static RegistryMode registryMode = RegistryMode.Normal;
        public static string connectionString = string.Format("server={0};Initial Catalog={1};Integrated Security=false;UID={2};PWD={3};Network Library=DBMSSOCN;", "woobinsoft.cafe24.com", "woobinsoft", "woobinsoft", "tkagowjs1!");
        public static string naverOpenAPIKey = "1ba0cd404aab8eb19ca1c822028a54df";
        public static string naverBlogAndCafeSearchString = "http://openapi.naver.com/search?key={0}&query={1}&display={2}&start={3}&target={4}&sort=sim";
        public static string naverCafeSearchString = "http://openapi.naver.com/search?key={0}&query={1}&display={2}&start={3}&target={4}&sort=sim";
        public static string naverSearchSnMaxTerm = "50";
        public static string naverSearchStartNum = "1";
        public static string proxyIpListPath = AppDomain.CurrentDomain.BaseDirectory + "proxyIpList.txt";
        public static bool IsProxy = false;
        public static int proxyIpTermSecond = 60;
        public static Dictionary<int, string> dicProxyList = new Dictionary<int, string>();
        public static NetWorkConnectionType networkConnertType = NetWorkConnectionType.Mobile;
        public static ProxyIPChangeType proxyipChangeType = ProxyIPChangeType.LoopBase;
        public static bool isUserAgentRandomApply = true;//useragent random
        public static bool isMouseScrollExecute = false;//마우스 스크롤 사용여부

        #endregion

        #region 구조체    
        
        public struct DBInfo
        {
            public string domain;
            public string port;
            public string user;
            public string password;
        }

        public struct FTPInfo
        {
            public string domain;
            public string port;
            public string user;
            public string password;
        }


        /// <summary>
        /// 
        /// </summary>
        public struct KeyWordDataEntity
        {
            public int MaxPage;
            public int DelayTimeST;
            public int DelayTimeET;
            public string KeyWord;
            public string URL;
            public int ScrollByTerm;
            public int PageByLoopTime;
            public int ThreadTime;
        }

        public struct UserInfo
        {
            public string UserID;
            public string AuthYn;
            public string KeyCode;
            public string MacAddress;
            public int AllowKeywordCount;
            public string FromDt;
            public string ToDt;
        }

        /// <summary>
        /// UserAgent 정보를 담는  struct 입니다 .
        /// </summary>
        public struct UserAgentInfo
        {
            public string Agent;
            public string Compatible;
            public string Version;
            public string Platform;

        }
        #endregion

        #region Enum 타입입니다.
        /// <summary>
        /// 레지스트리 모드 enum 설정값입니다 
        /// </summary>
        public enum RegistryMode
        {
            WoW64,
            Normal
        }

        public enum SiteType
        {
            Blog,
            Cafe,
            CafeArticle,
            News,
            Webkr
        }

        public enum BrowserType
        {
            InternerExplorer,
            Chrome,
            Safari,
            Opera
        }


        public enum NetWorkConnectionType
        {
            Mobile,
            Proxy
        }

        public enum ProxyIPChangeType
        {
            TimeBase,
            LoopBase
        }


        public enum AllowProgramType
        {
            MobiClick,
            MobiInsta,
            MobiNote,
            MobiCacao,
            MobiFace,
            MobiTweeter
        }
        #endregion

        #region Common - 생성자 입니다.
        /// <summary>
        /// 생성자입니다.
        /// </summary>
        static Common()
        {
            //Platform 을 설정합니다.
            if (Kernel32Utilscs.Is64Bit() == true)
            {
                registryMode = RegistryMode.WoW64;
            }
            else
            {
                registryMode = RegistryMode.Normal;
            }

            //레지스트리 키의 경로를 설정합니다.
            switch (registryMode)
            {
                case RegistryMode.Normal:
                    regKeyRoot = NormalPath;
                    regKeyProxyRoot = NormalProxyPath;
                    break;
                case RegistryMode.WoW64:
                    regKeyRoot = Wow64Path;
                    regKeyProxyRoot = NormalProxyPath;
                    break;
                default:
                    regKeyRoot = NormalPath;
                    regKeyProxyRoot = NormalProxyPath;
                    break;
            }
        }
        #endregion

        #region RestoreRegistryForUserAgent - UserAgent 를 초기화 합니다
        /// <summary>
        /// UserAgent 를 초기화 합니다 
        /// </summary>
        /// <returns></returns>
        public static bool RestoreRegistryForUserAgent()
        {
            bool result = true;
            try
            {
                RegistryKey userAgentkey = Registry.LocalMachine.CreateSubKey(NormalPath);
                userAgentkey.SetValue("", "");
                foreach (string key in userAgentkey.GetValueNames())
                {
                    userAgentkey.DeleteValue(key, true);
                }

                try
                {
                    userAgentkey.DeleteSubKeyTree("Post Platform");
                }
                catch { }

                //WriteTextLog("mode", Common.registryMode.ToString() + Kernel32Utilscs.Is64Bit().ToString());

               if (Kernel32Utilscs.Is64Bit() == true)           
                {
                    userAgentkey = Registry.LocalMachine.CreateSubKey(Wow64Path);
                    userAgentkey.SetValue("", "");
                    foreach (string key in userAgentkey.GetValueNames())
                    {
                        userAgentkey.DeleteValue(key, true);
                    }

                    try
                    {
                        userAgentkey.DeleteSubKeyTree("Post Platform");
                    }
                    catch { }
                }

            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
                return false;
            }

            return result;
        }
        #endregion

        #region ChangeUserAgent - User Agent 를 변경합니다.
        /// <summary>
        /// User Agent 를 변경합니다.
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static bool ChangeUserAgent(string userAgent,ref string errMessage)
        {
            UserAgentInfo agent = SplitByUserAgent(userAgent);

            bool result = true;

            try
            {
                RegistryKey userAgentkey = Registry.LocalMachine.CreateSubKey(regKeyRoot);
                userAgentkey.SetValue("", agent.Agent);
                userAgentkey.SetValue("Compatible", agent.Compatible);
                userAgentkey.SetValue("Version", agent.Version);
                userAgentkey.SetValue("Platform", agent.Platform);
                try
                {
                    userAgentkey.DeleteSubKeyTree("Post Platform");
                }
                catch { }
            }
            catch(Exception ex)
            {
                WriteTextLog("ChangeUserAgent", ex.Message.ToString());
                errMessage = ex.Message.ToString();
                return false;
            }

            return result;

        }
        #endregion

        #region SplitByUserAgent -  userAgent 를 변경하기 위하여 문자열을 변경합니다.
        /// <summary>
        /// userAgent 를 변경하기 위하여 문자열을 변경합니다. 
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        private static UserAgentInfo SplitByUserAgent(string userAgent)
        {
            UserAgentInfo agent = new UserAgentInfo();
            string[] arrUserAgent = userAgent.Split(';');

            if (arrUserAgent.Length > 0)
            {
                for (int i = 0; i < arrUserAgent.Length; i++)
                {
                    if (i == 0)
                    {
                        string[] arrHeader = arrUserAgent[0].Split('(');
                        agent.Agent = arrHeader[0].Trim();
                        agent.Compatible = arrHeader[1].Trim();
                    }
                    else
                    {
                        if (i == 1)
                        {
                            agent.Version = arrUserAgent[i].ToString();
                        }
                        else
                        {
                            agent.Platform += "; " + arrUserAgent[i].ToString();
                        }
                    }
                }

                agent.Platform = agent.Platform.Substring(1).Trim() + " //";
            }

            return agent;
        }
        #endregion

        #region UserAgentList
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<string> UserAgentList()
        {
            List<string> result = new List<string>();
            DataTable dt = new DataTable();
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected) Connect(ref da);
         
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                string procedureName = "SP_USER_AGENT_LIST";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                dt = da.GetData(command);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        result.Add(dr["USER_AGENT"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                DisConnect(ref da);
            }                     

            return result;
        }
        #endregion

        #region IsAdministrator - Admin여부
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (null != identity)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }
        #endregion

        #region RandomKeyValue - 체류시간을 랜덤하게 설정합니다
        //체류시간을 랜덤하게 설정합니다 
        //초단위 입니다. 
        public static int RandomKeyValue()
        {
            return RandomKeyValue(20, 60); ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int RandomKeyValue(int start, int end)
        {
            int rtn = 0;

            Random random = new Random();
            rtn = random.Next(start, end);

            return rtn;
        }
        #endregion

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

        #region GetUserINfo - 사용자 정보를 가져옵니다.
        /// <summary>
        /// 사용자 정보를 가져옵니다.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetUserINfo(string userID)
        {
            DataTable dt = new DataTable();
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@USERID", userID);
                string procedureName = "SP_USER_INFO_SELECT";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                dt = da.GetData(command);

            }
            catch (Exception ex)
            {
                WriteTextLog("GetUserINfo", ex.Message.ToString());
            }
            finally
            {
                DisConnect(ref da);
            }

            return dt;
        }
        #endregion

        #region GetKeyWordInfo - 키워드 정보를 가져옵니다.
        /// <summary>
        /// 키워드 정보를 가져옵니다.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<KeyWordDataEntity> GetKeyWordInfo(string keyCode)
        {
            List<KeyWordDataEntity> result = new List<KeyWordDataEntity>();
            DataTable dt = new DataTable();
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@KEYCODE", keyCode);
                string procedureName = "SP_KEYWORDINFO_LIST_SELECT";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                dt = da.GetData(command);

            }
            catch (Exception ex)
            {
                WriteTextLog("GetKeyWordInfo", ex.Message.ToString());
            }
            finally
            {
                DisConnect(ref da);
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    result.Add(new KeyWordDataEntity
                    {
                        KeyWord = dr["KEYWORD"].ToString(),
                        URL = dr["URL"].ToString(),
                        MaxPage = Convert.ToInt32(dr["MAXPAGE"].ToString()),
                        DelayTimeST = Convert.ToInt32(dr["DELAYTIME_ST"].ToString()),
                        DelayTimeET = Convert.ToInt32(dr["DELAYTIME_ET"].ToString()),
                        ScrollByTerm = Common.RandomKeyValue(10, 100),
                        PageByLoopTime = Common.RandomKeyValue(3, 5),
                        ThreadTime = Common.RandomKeyValue(300, 1000)
                    });
                }              
            }

            return result;
        }

        public static List<KeyWordDataEntity> KeyWordMix(List<KeyWordDataEntity> list)
        {
            try
            {
                Random rnd = new Random();
                var randomizedList = from item in list
                                     orderby rnd.Next()
                                     select item;

                list = randomizedList.ToList<KeyWordDataEntity>();
            }
            catch (Exception ex)
            {
                WriteTextLog("KeyWordMix", ex.Message.ToString());
            }

            return list;
        }


        #endregion

        #region GetKeyWordList - 키워드 리스트를 가져옵니다.
        /// <summary>
        /// 키워드 리스트를 가져옵니다.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetKeyWordList(string keyCode)
        {
            DataTable result = new DataTable();
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@KEYCODE", keyCode);
                string procedureName = "SP_KEYWORDINFO_SELECT";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                result = da.GetData(command);

                result.Columns[4].ReadOnly = false;               
            }
            catch (Exception ex)
            {
                WriteTextLog("GetKeyWordInfo", ex.Message.ToString());
            }
            finally
            {
                DisConnect(ref da);
            }

            return result;
        }
        #endregion

        #region IsUserIDCheck - UserID 중복확인을 체크합니다.
        /// <summary>
        /// UserID 중복확인을 체크합니다.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool IsUserIDCheck(string userid)
        {
            bool result = false;
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@USERID", userid);

                string procedureName = "SP_USERID_CHECKED";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                string value = da.GetSingleValue(command).ToString();
                if (string.IsNullOrEmpty(value) == true || value == "N")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                DisConnect(ref da);
            }

            return result;
        }
        #endregion

        #region IsUserLoginCheck - 사용자로그인 체크
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsUserLoginCheck(string userid, string password)
        {
            bool result = false;
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@USERID", userid);
                param.Add("@PWD", password);

                string procedureName = "SP_NEW_USER_LOGIN_CHECK";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                string value = da.GetSingleValue(command).ToString();
                if (string.IsNullOrEmpty(value) == true || value == "N")
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                DisConnect(ref da);
            }

            return result;
        }
        #endregion

        #region GetDBInfo - DB 정보 설정
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static DBInfo GetDBInfo(string userid, string password)
        {
            DBInfo dbinfo = new DBInfo();
          
            try
            {
               

            }
            catch
            {
            }
           

            return dbinfo;
        }
        #endregion

        #region InsertUserInfo - 사용자정보를 추가합니다.
        /// <summary>
        /// 사용자정보를 추가합니다.
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <param name="macAddress"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        public static string InsertUserInfo(string userid, string password, string userName, string userTelNo)
        {
            string result = "001";
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected) Connect(ref da);
           
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@USERID", userid);
                param.Add("@PASSWORD", password);
                param.Add("@USER_NAME", userName);
                param.Add("@USER_TEL_NO", userTelNo);

                string procedureName = "SP_NEW_USERINFO_INSERT";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                result = da.GetSingleValue(command).ToString();
            }
            catch (Exception ex)
            {
                WriteTextLog("SP_NEW_USERINFO_INSERT:", ex.Message.ToString());
                result = "003";
            }
            finally
            {
                DisConnect(ref da);
            }

            return result;
        }
        #endregion

        #region GetMacAddress - MacAdress 를 가져옵니다.
        /// <summary>
        /// mac address 를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            return GetMacAddress(true); 
        }

        public static string GetMacAddress(bool isIP)
        {
            string mac = string.Empty;
           
            try
            {
                if (isIP)
                {
                    mac = MobiClick.NetworkInterfaceProvider.GetMacAddress();                    
                }
                else
                {
                    //무선네트웍 카드만 처리합니다.
                    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    if (nics == null || nics.Length < 1) { return mac; }

                    string result = string.Empty;
                    foreach (NetworkInterface nic in nics)
                    {
                        if (nic.NetworkInterfaceType != NetworkInterfaceType.Wireless80211) continue;
                        if (nic.OperationalStatus == OperationalStatus.Up)
                        {
                            result += nic.GetPhysicalAddress().ToString();
                            break;
                        }
                    }                   

                    string str = string.Empty;
                    foreach (char chr in result)
                    {
                        str += chr;
                        if (str.Length == 2)
                        {
                            mac += ":" + str;
                            str = string.Empty;
                        }
                    }
                    mac = mac.Substring(1);
                   
                }
            }
            catch (Exception ex)
            {
                WriteTextLog("mac", ex.Message.ToString());
            }

            return mac.ToUpper();
        }
        #endregion

        #region GetLocalIP - Local IP 를 가져옵니다
        /// <summary>
        /// Local IP 를 가져옵니다
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            string ipAdress = null;
            IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in ipHostEntry.AddressList)
            {
                if (ip.AddressFamily== System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipAdress = ip.ToString();
                    break;
                }               
            }

            return ipAdress;
        } 
        #endregion

        #region GetExternalIp - 외부아이피를 가져옵니다.
        /// <summary>
        /// 외부아이피를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public static string GetExternalIp()
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch { return null; }
        }
        #endregion

        #region GetIEVersion - IE 버전을 체크 합니다.
        /// <summary>
        /// IE 버전을 체크 합니다.
        /// </summary>
        /// <returns></returns>
        private string GetIEVersion()
        {
            string key = @"Software\Microsoft\Internet Explorer";
            RegistryKey dkey = Registry.LocalMachine.OpenSubKey(key, false);
            string data = dkey.GetValue("Version").ToString();
            return data;
        }
        #endregion
        
        #region GetOriginalUrl - 최종URL 을 반환 합니다.
        /// <summary>
        /// 원래 URL을 반환합니다.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetOriginalUrl(string url)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = false;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                result = response.Headers["Location"];
            }
            catch { }
            return result;
        }
        #endregion

        #region SettingRegistery -  레지스트리 키를 설정합니다.
        /// <summary>
        /// 레지스트리를 설정합니다.
        /// </summary>
        public static void SettingRegistery(ref string errMessage )
        {
            RegistryKey currentUserKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Zoom");
            RegistryKey currentUserIEMainKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main");

            try
            {
                string placement = "2C,00,00,00,00,00,00,00,01,00,00,00,FF,FF,FF,FF,FF,FF,FF,FF,FF,FF,FF,FF,FF,FF,FF,FF,00,00,00,00,00,00,00,00,A9,01,00,00,AC,02,00,00";
                //string placement = "2c,00,00,00,00,00,00,00,01,00,00,00,ff,ff,ff,ff,ff,ff,ff,ff,ff,ff,ff,ff,ff,ff,ff,ff,00,00,00,00,00,00,00,00,a9,01,00,00,ac,02,00,00";
                placement = placement.Replace(",", "");

                byte[] data = new byte[placement.Length / 2]; //16진수를 바이트로 만들면 길이가 반으로 줄어든다.
                for (int i = 0; i < placement.Length; i += 2)
                {
                    //16진수 문자열에서 2개씩 잘라 내어 바이트 배열에 저장한다.
                    data[i / 2] = (byte)Convert.ToByte(placement.Substring(i, 2), 16);
                }

                currentUserKey.SetValue("ZoomFactor", 100000, RegistryValueKind.DWord);
                currentUserKey.Flush();

                currentUserIEMainKey.SetValue("Window_Placement", data, RegistryValueKind.Binary);
                currentUserIEMainKey.SetValue("FullScreen", "no", RegistryValueKind.String);
                currentUserIEMainKey.Flush();                
            }
            catch (Exception ex){
                errMessage = ex.Message.ToString();            
            }
        }
        
        #endregion

        public static DataTable GetUpGradeList(ref string serverVersion)
        {
            DataTable dt = new DataTable();
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected) Connect(ref da);

            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                string procedureName = "SP_UPDATEFILE_INFO";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                da.GetData(command, ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {                   
                    foreach (DataRow dr in dt.Rows)
                    {
                        serverVersion = dr["VERSION"].ToString();
                        break;                     
                    }
                }

            }
            catch (Exception ex)
            {
                WriteTextLog("GetUpGradeList", ex.Message.ToString());
            }
            finally
            {
                DisConnect(ref da);
            }

            return dt;
        }

        public static void GetUpGradeProgramInfo(ref string fileVersion)
        {
           
            DataAccess da = new SQLServerDB();            
            if (!da.IsConnected) Connect(ref da);
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                string procedureName = "SP_UPDATEFILE_INFO_AUTO";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                fileVersion = da.GetSingleValue(command).ToString();               
            }
            catch (Exception ex)
            {
                WriteTextLog("GetUpGradeList", ex.Message.ToString());
            }
            finally
            {
                DisConnect(ref da);
            }            
        }

        #region UpdateChangePassword - 비밀번호를 변경합니다.
        /// <summary>
        /// 비밀번호를 변경합니다.
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="userID"></param>
        /// <param name="macAddress"></param>
        /// <param name="allowKeyCount"></param>
        /// <returns></returns>
        public static void UpdateChangePassword(string userID, string password)
        {
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected) Connect(ref da);           
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@USERID", userID);
                param.Add("@PASSWORD", password);

                string procedureName = "SP_PASSWORD_UPDATE";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                da.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                WriteTextLog("UpdateChangePassword", ex.Message.ToString() + ex.StackTrace.ToString());
            }
            finally
            {
                DisConnect(ref da);
            }
        }
        #endregion 

        #region HasConnection - 인터넷이 연결되어 있는지 확인합니다.
        public static bool HasConnection()
        {
            try
            {
                //System.Net.IPHostEntry i = System.Net.Dns.GetHostEntry("www.google.com");
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.co.kr"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        } 
        #endregion

        #region Connect,DisConnect
        private static void Connect(ref DataAccess da)
        {
            da.Connect(connectionString, true);
        }

        private static void DisConnect(ref DataAccess da)
        {
            da.Disconnect();
            da.Dispose();
            da = null;
        }
        #endregion
    }
}
