using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

namespace DBHelper
{
    #region // Delegates //
    public delegate void ConnectionStateChangeEventHandler(ConnectionState state);
    public delegate void CommandExecutionStateEventHandler(CommandExecutionStatus status);
    #endregion / Delegates /

    #region // Interfaces //
    public interface IBackendSource : IDisposable
    {
        string ConnectionString
        { get; set; }

        DbConnection Connection
        { get; }

        BackendType BackendType
        { get; }

        bool IsConnected
        { get; }

        Exception LastException
        { get; }

        bool Connect(bool reconnect);
        bool Connect(string conString, bool reconnect);
        bool Disconnect();

        event ConnectionStateChangeEventHandler ConnectionStateChanged;
        event CommandExecutionStateEventHandler CommandExecutionState;
    }
    public interface IBackend : IBackendSource
    {
        DataTable GetData(string sql);
        DataTable GetData(DbCommand command);
        void GetData(string sql, ref DataTable dt);
        void GetData(DbCommand command, ref DataTable dt);
        int ExecuteNonQuery(string sqlCommand, CommandType cmdType);
        int ExecuteNonQuery(DbCommand command);
        int ExecuteNonQuery(string sql, CommandType cmdType, ParameterEngine param);
        object GetSingleValue(DbCommand command);
        object GetSingleValue(string sql, CommandType cmdType);
        object GetSingleValue(string sql, CommandType cmdType, ParameterEngine param);
        DbCommand GenerateCommand(string sql, CommandType cmdType, ParameterEngine param);
        DbParameter CreateParameter();
        DbTransaction BeginTransaction();
        DbTransaction BeginTransaction(IsolationLevel isolationLevel);
        void CommitTransaction();
        void RollbackTransaction();
    }
    public interface ISqlFormatter
    {
        string SqlTemplate
        { get; set; }

        string Result
        { get; }

        List<string> DataValues
        { get; }

        int Count
        { get; set; }

        bool AddData<VType>(IList<VType> values, string seperator, string appendChar, AppendOptions appendOps, bool encloseIfBrackets);
    }
    #endregion / Interfaces /

    #region // Enumerations //
    public enum BackendType
    {
        Illegal = 0,
        SQL,
        Oracle,
        Odbc,
        OleDb,
    }
    public enum AppendOptions
    {
        None = 0,
        First,
        Last,
        Both
    }
    // This enum might be going to be help full in a multi threaded data access situations
    // and analysis/profiling purposes
    public enum CommandExecutionStatus
    {
        None = 0,
        Started,
        Finished,
        Aborted
    }
    #endregion / Enumerations /

    #region // Exception Classes //
    public class MSDataLayerException : ApplicationException
    {
        private string msg;
        public MSDataLayerException(string message)
            : base(message)
        {
            this.msg = message;
        }
    }
    #endregion / Exception Classes /
}
