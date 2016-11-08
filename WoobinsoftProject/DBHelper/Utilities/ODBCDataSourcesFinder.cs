using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace DBHelper.Utilities
{
    public static class ODBCDataSourcesFinder
    {
        #region // Internal Enumeration //
        public enum DSNType
        {
            System = 0,
            User,
            File
        }
        #endregion / Internal Enumeration /

        #region // Internal Class //
        public class ODBCDataSource
        {
            private string _name = "";
            private string _driver = "";
            private DSNType _dsnType = DSNType.System;

            internal ODBCDataSource(string name, string driver, DSNType dsnType)
            {
                this._name = name;
                this._driver = driver;
                this._dsnType = dsnType;
            }

            public string Name
            {
                get { return this._name; }
            }
            public string Driver
            {
                get { return this._driver; }
            }
            public DSNType DSNType
            {
                get { return this._dsnType; }
            }

            public override string ToString()
            {
                return this._name;
            }
        }
        #endregion / Internal Class /

        #region // Public Functions //
        public static List<ODBCDataSource> GetAllDSNs()
        {
            RegistryKey rkLMSF = Registry.LocalMachine.OpenSubKey("Software", RegistryKeyPermissionCheck.ReadSubTree);
            RegistryKey rkCUSF = Registry.CurrentUser.OpenSubKey("Software", RegistryKeyPermissionCheck.ReadSubTree);

            RegistryKey rkSys = rkLMSF.OpenSubKey("ODBC\\ODBC.INI\\ODBC Data Sources", RegistryKeyPermissionCheck.ReadSubTree);
            RegistryKey rkUser = rkCUSF.OpenSubKey("ODBC\\ODBC.INI\\ODBC Data Sources", RegistryKeyPermissionCheck.ReadSubTree);
            RegistryKey rkFile = rkLMSF.OpenSubKey("ODBC\\ODBC.INI\\ODBC File DSN", RegistryKeyPermissionCheck.ReadSubTree);

            string[] names = null;
            List<ODBCDataSource> ret = new List<ODBCDataSource>();

            // System DSNs
            if (rkSys != null)
            {
                names = rkSys.GetValueNames();
                foreach (string name in names)
                {
                    ODBCDataSource ins = new ODBCDataSource(name, rkSys.GetValue(name, "").ToString(), DSNType.System);
                    ret.Add(ins);
                }
                rkSys.Close();
            }

            // User DSNs
            if (rkUser != null)
            {
                names = rkUser.GetValueNames();
                foreach (string name in names)
                {
                    ODBCDataSource ins = new ODBCDataSource(name, rkUser.GetValue(name, "").ToString(), DSNType.User);
                    ret.Add(ins);
                }
                rkUser.Close();
            }

            // User File
            if (rkFile != null)
            {
                names = rkFile.GetValueNames();
                foreach (string name in names)
                {
                    if (name.ToUpper() != "DEFAULTDSNDIR")
                    {
                        ODBCDataSource ins = new ODBCDataSource(name, rkFile.GetValue(name, "").ToString(), DSNType.File);
                        ret.Add(ins);
                    }
                }
                rkFile.Close();
            }

            // Close keys
            rkCUSF.Close(); rkLMSF.Close();

            return ret;
        }

        public static List<ODBCDataSource> GetSystemDSNs()
        {
            RegistryKey rkLMSF = Registry.LocalMachine.OpenSubKey("Software", RegistryKeyPermissionCheck.ReadSubTree);
            RegistryKey rkSys = rkLMSF.OpenSubKey("ODBC\\ODBC.INI\\ODBC Data Sources", RegistryKeyPermissionCheck.ReadSubTree);

            string[] names = null;
            List<ODBCDataSource> ret = new List<ODBCDataSource>();

            // System DSNs
            if (rkSys != null)
            {
                names = rkSys.GetValueNames();
                foreach (string name in names)
                {
                    ODBCDataSource ins = new ODBCDataSource(name, rkSys.GetValue(name, "").ToString(), DSNType.System);
                    ret.Add(ins);
                }
                rkSys.Close();
            }

            rkLMSF.Close();

            return ret;
        }

        public static List<ODBCDataSource> GetUserDSNs()
        {
            RegistryKey rkCUSF = Registry.CurrentUser.OpenSubKey("Software", RegistryKeyPermissionCheck.ReadSubTree);
            RegistryKey rkUser = rkCUSF.OpenSubKey("ODBC\\ODBC.INI\\ODBC Data Sources", RegistryKeyPermissionCheck.ReadSubTree);

            string[] names = null;
            List<ODBCDataSource> ret = new List<ODBCDataSource>();

            // User DSNs
            if (rkUser != null)
            {
                names = rkUser.GetValueNames();
                foreach (string name in names)
                {
                    ODBCDataSource ins = new ODBCDataSource(name, rkUser.GetValue(name, "").ToString(), DSNType.User);
                    ret.Add(ins);
                }
                rkUser.Close();
            }

            rkCUSF.Close();

            return ret;
        }

        public static List<ODBCDataSource> GetFileDSNs()
        {
            RegistryKey rkLMSF = Registry.LocalMachine.OpenSubKey("Software", RegistryKeyPermissionCheck.ReadSubTree);
            RegistryKey rkFile = rkLMSF.OpenSubKey("ODBC\\ODBC.INI\\ODBC File DSN", RegistryKeyPermissionCheck.ReadSubTree);

            string[] names = null;
            List<ODBCDataSource> ret = new List<ODBCDataSource>();

            // User File
            if (rkFile != null)
            {
                names = rkFile.GetValueNames();
                foreach (string name in names)
                {
                    if (name.ToUpper() != "DEFAULTDSNDIR")
                    {
                        ODBCDataSource ins = new ODBCDataSource(name, rkFile.GetValue(name, "").ToString(), DSNType.File);
                        ret.Add(ins);
                    }
                }
                rkFile.Close();
            }

            rkLMSF.Close();

            return ret;
        }
        #endregion / Public Functions /
    }
}