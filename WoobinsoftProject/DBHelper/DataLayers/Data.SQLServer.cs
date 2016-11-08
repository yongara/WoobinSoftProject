using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

using DBHelper;
using DBHelper.Base;

namespace DBHelper.SQLServer
{
    public class SQLServerDB : DataAccess
    {
        #region // Member Variables //
        SqlConnection con = null;
        #endregion / Member Variables /

        #region // Constructor //
        public SQLServerDB()
            : base(BackendType.SQL)
        {
        }
        #endregion / Constructor /

        #region // Properties //
        public override DbConnection Connection
        {
            get { return this.con; }
        }
        #endregion / Properties /

        #region // Functions - Protected //
        protected override void createConnection()
        {
            con = new SqlConnection(this.ConnectionString);
        }
        protected override DbCommand getCommand(ref string sqlCommand, CommandType cmdType)
        {
            return base._getCommand<SqlCommand>(ref sqlCommand, cmdType);
        }
        #endregion / Functions - Protected /

        #region // Functions - Public //
        public override DataTable GetData(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                DbDataReader reader = base._getReader<SqlCommand>(ref sql);
                dt.Load(reader);
                reader.Dispose();
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            return dt;
        }

        public override DataTable GetData(DbCommand command)
        {
            DataTable dt = new DataTable();
            try
            {
                DbDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Dispose();
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            return dt;
        }

        public override void GetData(string sql, ref DataTable dt)
        {
            try
            {
                DbDataReader reader = base._getReader<SqlCommand>(ref sql);
                dt.Load(reader);
                reader.Dispose();
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
        }

        public override void GetData(DbCommand command, ref DataTable dt)
        {
            try
            {
                DbDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Dispose();
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
        }

        public override int ExecuteNonQuery(string sqlCommand, CommandType cmdType)
        {
            int ret = 0;
            try
            {
                SqlCommand cmd = (SqlCommand)getCommand(ref sqlCommand, cmdType);

                if (cmd != null) ret = cmd.ExecuteNonQuery();
                else throw new MSDataLayerException("Command cannot be created since connection is not initialized.");

                this.lastException = null;
            }
            catch (MSDataLayerException tEx)
            { throw tEx; }
            catch (Exception ex)
            { ret = DataAccess.ERROR_RESULT; this.lastException = ex; }

            return ret;
        }

        public override int ExecuteNonQuery(DbCommand command)
        {
            int ret = 0;
            try
            {
                if (command != null) ret = command.ExecuteNonQuery();
                else throw new MSDataLayerException("Command object is null.");

                this.lastException = null;
            }
            catch (MSDataLayerException tEx)
            { throw tEx; }
            catch (Exception ex)
            { ret = DataAccess.ERROR_RESULT; this.lastException = ex; }

            return ret;
        }

        public override int ExecuteNonQuery(string sql, CommandType cmdType, ParameterEngine param)
        {
            int ret = 0;
            try
            {
                SqlCommand cmd = (SqlCommand)this.GenerateCommand(sql, cmdType, param);

                if (cmd != null) ret = cmd.ExecuteNonQuery();
                else throw new MSDataLayerException("Command cannot be created since connection is not initialized.");

                this.lastException = null;
            }
            catch (MSDataLayerException tEx)
            { throw tEx; }
            catch (Exception ex)
            { ret = DataAccess.ERROR_RESULT; this.lastException = ex; }

            return ret;
        }

        public override object GetSingleValue(DbCommand command)
        {
            object ret = null;
            try
            {
                ret = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            return ret;
        }

        public override object GetSingleValue(string sql, CommandType cmdType)
        {
            object ret = null;
            try
            {
                DbCommand cmd = getCommand(ref sql, cmdType);
                ret = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            return ret;
        }

        public override object GetSingleValue(string sql, CommandType cmdType, ParameterEngine param)
        {
            object ret = null;
            try
            {
                DbCommand cmd = this.GenerateCommand(sql, cmdType, param);
                ret = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                base.lastException = ex;
                throw ex;
            }
            return ret;
        }

        public override DbCommand GenerateCommand(string sql, CommandType cmdType, ParameterEngine param)
        {
            DbCommand cmd = this.getCommand(ref sql, cmdType);

            if (cmd != null && param != null) param.Inject(ref cmd);

            return cmd;
        }

        public override DbParameter CreateParameter()
        {
            return new SqlParameter();
        }
        #endregion / Functions - Public /
    }
}
