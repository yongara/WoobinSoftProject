using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
namespace MobileClickInstagram
{
    static class InstagramNetworkInterfaceProvider
    {
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int destIp, int srcIP, byte[] macAddr, ref uint physicalAddrLen );

        public static string GetMacAddress()
        {
            string mac = string.Empty;
            try
            {
                IPAddress dst = IPAddress.Parse(InstagramCommon.GetLocalIP()); // the destination IP address

                byte[] macAddr = new byte[6];
                uint macAddrLen = (uint)macAddr.Length;

                if (SendARP(BitConverter.ToInt32(dst.GetAddressBytes(), 0), 0, macAddr, ref macAddrLen) != 0)
                    throw new InvalidOperationException("SendARP failed.");

                string[] str = new string[(int)macAddrLen];
                for (int i = 0; i < macAddrLen; i++)
                {
                    str[i] = macAddr[i].ToString("x2");
                }

                mac = string.Join(":", str);
            }
            catch { }

            return mac;
        }



    }
}
