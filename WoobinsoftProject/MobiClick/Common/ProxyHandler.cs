using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;

namespace MobiClick
{
    class ProxyHandler
    {      
        /// <summary>
        /// 프록시IP 를 설정합니다.
        /// </summary>
        public static void ProxyIpListSetting()
        {
            if (File.Exists(Common.proxyIpListPath) == false)
            {
                return;
            }

            string[] lines = File.ReadAllLines(Common.proxyIpListPath);
            Common.dicProxyList.Clear();

            if (lines.Length > 0)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    Common.dicProxyList.Add(i + 1, lines[i]);
                }
            }
        }       
    }

}
