using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace MobiClick
{
    public class IniFileHandler
    {
        private string _iniPath;

        public IniFileHandler()
        {
            this._iniPath = AppDomain.CurrentDomain.BaseDirectory + "ConnectionInfo.ini";
        }

        public IniFileHandler(string path)
        {
            this._iniPath = path;  //INI 파일 위치를 생성할때 인자로 넘겨 받음
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        // INI 값을 읽어 온다. 
        public String GetIniValue(String Section, String Key)
        {        
            return  GetIniValue(Section, Key,_iniPath);
        }      
        
        // INI 값 읽기
        public String GetIniValue(String Section, String Key, String iniPath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, iniPath);
            return temp.ToString();
        }

        // INI 값을 셋팅
        public void SetIniValue(String Section, String Key, String Value)
        {
            SetIniValue(Section, Key, Value, _iniPath);
        }

        // INI 값 설정
        public void SetIniValue(String Section, String Key, String Value, String iniPath)
        {
            WritePrivateProfileString(Section, Key, Value, iniPath);
        }
    }
}
