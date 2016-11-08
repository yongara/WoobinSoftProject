using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MobiClick
{
    internal class Kernel32Utilscs
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static string ReadINIFile(string section, string key)
        {
            string result = string.Empty;
            int iResult = 0;
            StringBuilder sb = null;
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "MobiClick.ini";

            try
            {
                sb = new StringBuilder(255);
                iResult = GetPrivateProfileString(section, key, "", sb, 255, filePath);
                result = sb.ToString();
            }
            catch { }


            return result;
        }



        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool lpSystemInfo);

        public static bool Is64Bit()
        {
            if (IntPtr.Size == 8 || (IntPtr.Size == 4 && Is32BitProcessOn64BitProcessor()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool Is32BitProcessOn64BitProcessor()
        {
            bool retVal;

            IsWow64Process(Process.GetCurrentProcess().Handle, out retVal);

            return retVal;
        }
    }
}
