using System;
using System.Collections.Generic;
using System.Text;

namespace MobiClick
{
    public class CEngine
    {
        RegUtils _regUtils = new RegUtils();

        public CEngine() { }

        public void SetProxyName(string ProxyAddress)
        { 
            string szRegKey = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings\";
            string szName = "ProxyServer";

            _regUtils.SetKeyValue(RegUtils.RegKeyType.CurrentUser, szRegKey, szName, ProxyAddress);

        }

        public void EnableProxy(string ProxyAddress)
        {
            string szRegKey = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings\";
            string szName = "ProxyEnable";
            string szValue = string.Empty;

            _regUtils.GetKeyValue(RegUtils.RegKeyType.CurrentUser, szRegKey, "ProxyServer", out szValue);

            if (szValue != ProxyAddress)
            {
                SetProxyName(ProxyAddress);
            }

            _regUtils.SetKeyValue(RegUtils.RegKeyType.CurrentUser, szRegKey, szName, 1);

            byte[] abValue;
            char[] aaValue;
            szRegKey = szRegKey + "Connections";
            _regUtils.GetKeyValue(RegUtils.RegKeyType.CurrentUser, szRegKey, "DefaultConnectionSettings", out abValue);
            aaValue = new char[abValue.Length];

            for (int i = 0; i < abValue.Length; i++)
            {
                if (i == 8)
                    abValue[i] = 3;
                aaValue[i] = (char)abValue[i];
            }


            _regUtils.SetKeyValue(RegUtils.RegKeyType.CurrentUser, szRegKey, "DefaultConnectionSettings", Encoding.ASCII.GetBytes((new string(aaValue)).Replace(szValue, ProxyAddress)));


        }

        public void DisableProxy(string ProxyAddress)
        {
            string szRegKey = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings\";
            string szName = "ProxyEnable";
            _regUtils.SetKeyValue(RegUtils.RegKeyType.CurrentUser, szRegKey, szName, 0);

            byte[] abValue;
            szRegKey = szRegKey + "Connections";
            _regUtils.GetKeyValue(RegUtils.RegKeyType.CurrentUser, szRegKey, "DefaultConnectionSettings", out abValue);

            for (int i = 0; i < abValue.Length; i++)
            {
                if (i == 8)
                {
                    abValue[i] = 0;
                    break;
                }
            }



            _regUtils.SetKeyValue(RegUtils.RegKeyType.CurrentUser, szRegKey, "DefaultConnectionSettings", abValue);

            
        }

        public string GetProxyName()
        {
            string szRegKey = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings\";
            string szName = "ProxyServer";
            string szProxyAddress = string.Empty;

            _regUtils.GetKeyValue(RegUtils.RegKeyType.CurrentUser, szRegKey, szName, out szProxyAddress);

            return szProxyAddress;
        }

        public int GetProxyStatus()
        {
            string szRegKey = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings\";
            string szName = "ProxyEnable";
            int iProxyStatus = 0;

            _regUtils.GetKeyValue(RegUtils.RegKeyType.CurrentUser, szRegKey, szName, out iProxyStatus);

            return iProxyStatus;
        }
    }
}
