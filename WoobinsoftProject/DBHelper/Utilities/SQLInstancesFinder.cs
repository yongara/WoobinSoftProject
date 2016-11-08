using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

namespace DBHelper.Utilities
{
    public static class SQLInstancesFinder
    {
        #region // Internal Class //
        public class SQLServerInstance
        {
            string _serverName;
            string _instanceName;
            bool _isClustered;
            string _version;

            internal SQLServerInstance(string serverName, string instanceName, bool isClustered, string version)
            {
                this._serverName = serverName;
                this._instanceName = instanceName;
                this._isClustered = isClustered;
                this._version = version;
            }

            public override string ToString()
            {
                return this._serverName;
            }

            public string ServerName
            {
                get { return this._serverName; }
            }
            public string InstanceName
            {
                get { return this._instanceName; }
            }
            public bool IsClustered
            {
                get { return this._isClustered; }
            }
            public string Version
            {
                get { return this._version; }
            }
            public string ServerInstance
            {
                get { return ((string)(this._serverName + "\\" + this._instanceName)).TrimEnd('\\'); }
            }
        }
        #endregion / Internal Class /

        #region // Public Functions //

        /// <summary>
        /// Retrieves information about the SQL Server instances running in current network.
        /// </summary>
        /// <returns>A list of 'SQLServerInstance' objects, which contains information about each SQL Server instance.</returns>
        public static List<SQLServerInstance> GetNetworkSQLServerInstances()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");

            List<SQLServerInstance> ret = new List<SQLServerInstance>();

            if (factory.CanCreateDataSourceEnumerator)
            {
                DbDataSourceEnumerator dataSourceEnumerator = factory.CreateDataSourceEnumerator();

                if (dataSourceEnumerator != null)
                {
                    DataTable dt = dataSourceEnumerator.GetDataSources();

                    foreach (DataRow dr in dt.Rows)
                    {
                        SQLServerInstance t = _CreateSQLServerInstance(dr);
                        ret.Add(t);
                    }
                }
            }

            return ret; ;
        }

        /// <summary>
        /// Retrieves the databases present in an SQL Server instance using Windows Authentication.
        /// </summary>
        /// <param name="server">Name of the SQL Server instance.</param>
        /// <returns>Returns names of databases.</returns>
        public static List<string> GetDatabasesFromServer(string server)
        {
            return _getDatabasesFromServer(server, null, null, true);
        }

        /// <summary>
        /// Retrieves the databases present in an SQL Server instance using SQL Server Authentication.
        /// </summary>
        /// <param name="server">Name of the SQL Server instance.</param>
        /// <param name="useName">Login name to connect to SQL Server.</param>
        /// <param name="password">Password for the login.</param>
        /// <returns></returns>
        public static List<string> GetDatabasesFromServer(string server, string useName, string password)
        {
            return _getDatabasesFromServer(server, useName, password, false);
        }

        #endregion / Public Functions /

        #region // Private Functions //
        private static SQLServerInstance _CreateSQLServerInstance(DataRow dr)
        {
            string sServer = dr["ServerName"].ToString();
            string sInstance = (dr["InstanceName"] == DBNull.Value ? "" : dr["InstanceName"].ToString());
            string sClustered = (dr["IsClustered"] == DBNull.Value ? "false" : dr["IsClustered"].ToString().ToLower());
            string sVersion = (dr["Version"] == DBNull.Value ? "" : dr["Version"].ToString());
            bool bClus = false;

            if (sClustered == "false" || sClustered == "0" || sClustered == "no" || sClustered == "") bClus = false;
            else if (sClustered == "true" || sClustered == "1" || sClustered == "yes") bClus = true;
            else throw new MSDataLayerException("Invalid boolean data");

            return new SQLServerInstance(sServer, sInstance, bClus, sVersion);
        }

        private static List<string> _getDatabasesFromServer(string server, string useName, string password, bool useIS)
        {
            if (server == null) server = string.Empty;
            if (useName == null) useName = string.Empty;
            if (password == null) password = string.Empty;

            // Build Connection String
            string sConStr = "Data Source=" + server;

            if (useIS)
                sConStr += ";Integrated Security=True";
            else
            {
                sConStr += (";User ID=" + useName);
                sConStr += (";Password=" + password);
            }

            // Now get databases
            System.Data.SqlClient.SqlConnection con = null;
            List<string> lstRet = null;

            try
            {
                con = new System.Data.SqlClient.SqlConnection(sConStr);
                con.Open();

                DataTable dt = con.GetSchema("Databases");

                lstRet = new List<string>(dt.Rows.Count);
                foreach (DataRow dr in dt.Rows) lstRet.Add(dr["database_name"].ToString());
            }
            catch (Exception ex)
            {
                throw new MSDataLayerException(ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    try
                    {
                        con.Close(); con.Dispose();
                    }
                    catch { }
                }
            }

            return lstRet;
        }
        #endregion / Private Functions /
    }
}