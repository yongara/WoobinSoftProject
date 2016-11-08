using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Win32;

namespace MobiClick
{
    internal class RegUtils
    {
        internal enum RegKeyType : int
        {
            CurrentUser = 1,
            LocalMachine = 2
        }

        internal RegUtils() { }

        // byte[]
        internal void GetKeyValue(RegKeyType KeyType, string RegKey, string Name, out byte[] Value)
        {
            RegistryKey oRegKey = null;

            switch ((int)KeyType)
            {
                case 1:
                    oRegKey = Registry.CurrentUser;
                    break;
                case 2:
                    oRegKey = Registry.LocalMachine;
                    break;
            }
            oRegKey = oRegKey.OpenSubKey(RegKey);

            Value = (byte[])oRegKey.GetValue(Name);

            oRegKey.Close();
        }

        // int
        internal void GetKeyValue(RegKeyType KeyType, string RegKey, string Name, out int Value)
        {
            RegistryKey oRegKey = null;

            switch ((int)KeyType)
            {
                case 1:
                    oRegKey = Registry.CurrentUser;
                    break;
                case 2:
                    oRegKey = Registry.LocalMachine;
                    break;
            }
            oRegKey = oRegKey.OpenSubKey(RegKey);

            Value = (int)oRegKey.GetValue(Name,null);

            oRegKey.Close();

        }

        // string
        internal void GetKeyValue(RegKeyType KeyType, string RegKey, string Name, out string Value)
        {
            RegistryKey oRegKey = null;

            switch ((int)KeyType)
            {
                case 1:
                    oRegKey = Registry.CurrentUser;
                    break;
                case 2:
                    oRegKey = Registry.LocalMachine;
                    break;
            }
            oRegKey = oRegKey.OpenSubKey(RegKey);

            Value = (string)oRegKey.GetValue(Name);

            oRegKey.Close();

        }

        // byte[]
        internal void SetKeyValue(RegKeyType KeyType, string RegKey, string Name, byte[] Value)
        {
            RegistryKey oRegKey = null;

            switch ((int)KeyType)
            {
                case 1:
                    oRegKey = Registry.CurrentUser;
                    break;
                case 2:
                    oRegKey = Registry.LocalMachine;
                    break;
            }

            oRegKey = oRegKey.OpenSubKey(RegKey, true);
            oRegKey.SetValue(Name, Value);
            oRegKey.Close();
            User32Utils.Notify_SettingChange();
            WinINetUtils.Notify_OptionSettingChanges();
        }

        // string
        internal void SetKeyValue(RegKeyType KeyType, string RegKey, string Name, string Value)
        {
            RegistryKey oRegKey = null ;

            switch ((int)KeyType)
            {
                case 1:
                    oRegKey = Registry.CurrentUser;
                    break;
                case 2:
                    oRegKey = Registry.LocalMachine;
                    break;
            }

            oRegKey = oRegKey.OpenSubKey(RegKey, true);
            oRegKey.SetValue(Name, Value);
            oRegKey.Close();
            User32Utils.Notify_SettingChange();
            WinINetUtils.Notify_OptionSettingChanges();
        }

        // int
        internal void SetKeyValue(RegKeyType KeyType, string RegKey, string Name, int Value)
        {
            RegistryKey oRegKey = null;

            switch ((int)KeyType)
            {
                case 1:
                    oRegKey = Registry.CurrentUser;
                    break;
                case 2:
                    oRegKey = Registry.LocalMachine;
                    break;
            }

            oRegKey = oRegKey.OpenSubKey(RegKey, true);
            oRegKey.SetValue(Name, Value);
            oRegKey.Close();
            User32Utils.Notify_SettingChange();
            WinINetUtils.Notify_OptionSettingChanges();
        }
    }
}
