using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.IO;
using System.Xml;

namespace MobileClickInstagram
{
    class InstagramCommon
    {
        public static string dbConnectionInfo = string.Empty;
        public static string ftpConnectionInfo = string.Empty;
        public static string instagramEncKey = string.Empty;
        public static string instagramMacKey = string.Empty;


        static InstagramCommon()
        {

        }

        public static string GetInstagramUserKey(string macKey)
        {
            string result = string.Empty;
            try
            {
                WebClient wc = new WebClient();
                string encKey = InstagramEncryption.AES256_encrypt(macKey);
                string uriString = "http://www.woobinsoft.kr/GetEncryptTicket.aspx?uk=" + System.Web.HttpUtility.UrlEncode(encKey);

                Stream stream = wc.OpenRead(uriString);
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();

                //if (string.IsNullOrEmpty(result) == true)
                //{
                //    result = string.Empty;
                //}
                //else
                //{
                //    WriteTextLog("GetInstagramUserKey===>", result);

                //    XmlDocument xmlDoc = new XmlDocument();
                //    xmlDoc.LoadXml(result);

                //    result = xmlDoc.DocumentElement.ChildNodes[0].InnerText;
                //}
            }
            catch (Exception ex)
            {
                result = string.Empty;
                WriteTextLog("GetInstagramUserKey", ex.Message.ToString());
            }

            return result;
        }

        public static string AddInstagramMacAddressInfo(string macKey,string userName,string telNo)
        {
            string result ="001";
            string inParams = string.Format("{0}||{1}||{2}", macKey, userName, telNo);
            try
            {
                WebClient wc = new WebClient();
                string encKey = InstagramEncryption.AES256_encrypt(inParams);
                string uriString = "http://www.woobinsoft.kr/AddInstagramMacAddressInfo.aspx?uk=" + System.Web.HttpUtility.UrlEncode(encKey);

                Stream stream = wc.OpenRead(uriString);
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();

                if (string.IsNullOrEmpty(result) == true)
                {
                    result = "003";
                }
                else
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(result);

                    result = xmlDoc.DocumentElement.ChildNodes[0].InnerText;
                }
            }
            catch (Exception ex)
            {
                result = "N";
                WriteTextLog("GetInstagramMacAddressCheck", ex.Message.ToString());
            }

            return result;
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

        public enum InstagramTableType
        {
            Account,
            SearchKeyword,
            SpamKeyword,
            Unfollow,
            ReplyKwyword,
            None
        }

        public static void Delay(int second)
        {
            TimeSpan span;
            DateTime time = DateTime.Now.AddSeconds((double)second);
            do
            {
                span = time.Subtract(DateTime.Now);
                Application.DoEvents();
                Thread.Sleep(50);
            }
            while (span.TotalSeconds > 0.0);
        }

        public static int RandomKeyValue(int start, int end)
        {
            int rtn = 0;

            Random random = new Random();
            rtn = random.Next(start, end);

            return rtn;
        }

        #region GetMacAddress - MacAdress 를 가져옵니다.
        /// <summary>
        /// mac address 를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            string macaddress = GetMacAddress(true);
            if (string.IsNullOrEmpty(macaddress) == true)
            {
                macaddress = GetMacAddress(false);
            }

            return macaddress;
        }

        public static string GetMacAddress(bool isIP)
        {
            string mac = string.Empty;

            try
            {
                if (isIP)
                {
                    mac = InstagramNetworkInterfaceProvider.GetMacAddress();
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
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
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

    }
}
