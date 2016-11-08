using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Text;
using UCGuideSQLHelper;

namespace UCGuideSQLHelper.Base
{
    public abstract class DataAccess : IBackend
    {
        #region // Events //
        public virtual event ConnectionStateChangeEventHandler ConnectionStateChanged;
        public virtual event CommandExecutionStateEventHandler CommandExecutionState;
        #endregion / Events /

        #region // Constants //
        public const int ERROR_RESULT = int.MaxValue;
        #endregion / Constants /

        #region // Member Variables //
        private BackendType backEndType;
        private string conString;
        private bool isTransactionActive = false;
        private DbTransaction currentTransaction = null;

        protected readonly char[] TrimChars = new char[] { ':', '@', '?' };
        protected bool isConnected = false;

        protected Exception lastException;
        #endregion / Member Variables /

        #region // Constructor //
        public DataAccess(BackendType backendType)
        {
            this.backEndType = backendType;
        }
        #endregion / Constructor /

        #region // IBackendSource Members //
        // ConnectionStateChanged event is decleared in top
        public virtual string ConnectionString
        {
            get { return this.conString; }
            set { this.conString = value; }
        }

        public virtual DbConnection Connection
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public virtual BackendType BackendType
        {
            get { return this.backEndType; }
        }

        public virtual Exception LastException
        {
            get { return this.lastException; }
        }

        public virtual bool Connect(bool reconnect)
        {
            return this.Connect(this.ConnectionString, reconnect);
        }

        public virtual bool Connect(string connectionString, bool reconnect)
        {
            bool ret = true;
            bool recreated = false;
            try
            {
                this.ConnectionString = connectionString;

                if (reconnect)
                {
                    if (this.Connection != null)
                    {
                        this.Disconnect();
                        this.Connection.ConnectionString = this.ConnectionString;
                        this.Connection.Open();
                    }
                    else
                    {
                        this.createConnection();
                        this.Connection.Open();
                        recreated = true;
                    }
                }
                else
                {
                    this.createConnection();
                    this.Connection.Open();
                    recreated = true;
                }

                if (recreated && this.Connection != null)
                    this.Connection.StateChange += new StateChangeEventHandler(connStateChanged);
            }
            catch (Exception ex)
            {
                this.lastException = ex;
                ret = false;
            }
            return ret;
        }

        public virtual bool IsConnected
        {
            get
            {
                bool ret = false;
                if (this.Connection != null) ret = (this.Connection.State == ConnectionState.Open);
                return ret;
            }
        }

        public virtual bool Disconnect()
        {
            if (this.Connection == null) return false;

            try
            {
                if (this.IsTransactionActive) this.currentTransaction.Dispose();
            }
            catch { }
            finally
            {
                this.currentTransaction = null;
                this.IsTransactionActive = false;
            }

            if (this.Connection.State != ConnectionState.Closed)
            {
                try
                {
                    this.Connection.Close();
                }
                catch { }
                try
                {
                    this.Connection.Dispose();
                }
                catch { }
            }
            return true;
        }

        public virtual void Dispose()
        {
            this.Disconnect();
        }

        #endregion  / IBackendSource Members /

        #region // Properties - (Non IBackendSource/IBackend Members) //
        protected bool IsTransactionActive
        {
            get { return this.isTransactionActive; }
            private set { this.isTransactionActive = value; }
        }
        #endregion / Properties - (Non IBackendSource/IBackend Members) /

        #region // Public Functions //
        public virtual TSqlFormatter GetNewSqlFormatter<TSqlFormatter>()
            where TSqlFormatter : ISqlFormatter, new()
        {
            TSqlFormatter ret = new TSqlFormatter();
            return ret;
        }
        public virtual TSqlFormatter GetNewSqlFormatter<TSqlFormatter>(string sqlQueryTemplate)
            where TSqlFormatter : ISqlFormatter, new()
        {
            TSqlFormatter ret = new TSqlFormatter();
            ret.SqlTemplate = sqlQueryTemplate;

            return ret;
        }

        // *** Transaction Handling ***
        public DbTransaction BeginTransaction()
        {
            return this._beginTransaction(IsolationLevel.Unspecified, false);
        }
        public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return this._beginTransaction(isolationLevel, false);
        }
        public void CommitTransaction()
        {
            this._transCommitRollback(true);
        }
        public void RollbackTransaction()
        {
            this._transCommitRollback(false);
        }
        #endregion / Public Functions /

        #region // Abstract Functions //

        // *** Protected ***
        protected abstract void createConnection();
        protected abstract DbCommand getCommand(ref string sqlCommand, CommandType cmdType);

        // *** Public ***
        // IBackend Members
        public abstract DataTable GetData(string sql);
        public abstract DataTable GetData(DbCommand command);
        public abstract void GetData(string sql, ref DataTable dt);
        public abstract void GetData(DbCommand command, ref DataTable dt);
        public abstract int ExecuteNonQuery(string sqlCommand, CommandType cmdType);
        public abstract int ExecuteNonQuery(DbCommand command);
        public abstract int ExecuteNonQuery(string sql, CommandType cmdType, ParameterEngine param);
        public abstract object GetSingleValue(DbCommand command);
        public abstract object GetSingleValue(string sql, CommandType cmdType);
        public abstract object GetSingleValue(string sql, CommandType cmdType, ParameterEngine param);
        public abstract DbCommand GenerateCommand(string sql, CommandType cmdType, ParameterEngine param);
        public abstract DbParameter CreateParameter();

        #endregion / Abstract Functions /

        #region // Helper Functions - Protected //
        protected DbCommand _getCommand<TCommand>(ref string sqlCommand, CommandType cmdType)
            where TCommand : DbCommand, new()
        {
            if (this.Connection == null) return null;

            TCommand ret = new TCommand();

            ret.CommandType = cmdType;
            ret.CommandText = sqlCommand;
            ret.Connection = this.Connection;
            ret.CommandTimeout = 0;

            if (this.IsTransactionActive)
            {
                if (this.currentTransaction != null)
                    ret.Transaction = this.currentTransaction;
                else
                    throw new MSDataLayerException("Cannot assign null transaction to command.");
            }

            return ret;
        }
        protected DbDataReader _getReader<TCommand>(ref string sql)
            where TCommand : DbCommand, new()
        {
            if (this.Connection == null) return null;

            TCommand cmd = new TCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = this.Connection;
            cmd.CommandTimeout = 0;

            if (this.IsTransactionActive)
            {
                if (this.currentTransaction != null)
                    cmd.Transaction = this.currentTransaction;
                else
                    throw new MSDataLayerException("Cannot assign null transaction to command.");
            }

            return cmd.ExecuteReader();
        }
        #endregion / Helper Functions - Protected /

        #region // Helper Functions - Private //
        private void connStateChanged(object sender, StateChangeEventArgs e)
        {
            if (this.ConnectionStateChanged != null) this.ConnectionStateChanged(e.CurrentState);
        }
        private void cmdStateChanged(object sender, CommandExecutionStatus e)
        {
            if (this.CommandExecutionState != null) this.CommandExecutionState(CommandExecutionStatus.None);
        }

        object transLock = new object();
        private DbTransaction _beginTransaction(IsolationLevel il, bool useIL)
        {
            Exception tex = null;

            lock (transLock)
            {
                try
                {
                    if (this.IsConnected)
                    {
                        if (!this.IsTransactionActive)
                        {
                            this.currentTransaction = (useIL ? this.Connection.BeginTransaction(il) : this.Connection.BeginTransaction());
                            this.IsTransactionActive = true;
                        }
                        else
                            tex = new MSDataLayerException("A transaction is already being active.");
                    }
                    else
                        tex = new MSDataLayerException("Transaction cannot begin when connection is not established.");
                }
                catch (Exception ex)
                {
                    tex = ex;
                }
            }

            if (tex != null) throw tex;

            return this.currentTransaction;
        }
        private void _transCommitRollback(bool isCommit)
        {
            Exception tex = null;

            lock (transLock)
            {
                try
                {
                    if (this.currentTransaction != null)
                    {
                        try
                        {
                            if (isCommit)
                                this.currentTransaction.Commit();
                            else
                                this.currentTransaction.Rollback();

                            this.currentTransaction.Dispose();
                        }
                        catch (Exception ex)
                        {
                            tex = ex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    tex = ex;
                }
            }

            this.IsTransactionActive = false;
            this.currentTransaction = null;

            if (tex != null) throw tex;
        }
        #endregion / Helper Functions - Private /
    }
}
