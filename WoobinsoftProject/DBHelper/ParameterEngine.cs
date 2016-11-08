using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data.Odbc;

namespace DBHelper
{
    public sealed class ParameterEngine
    {
        #region // Member Variables //
        List<DbParameter> _lst = new List<DbParameter>();

        private IBackend Backend;
        private BackendType BackendType;
        #endregion / Member Variables /

        #region // Constructor //
        private ParameterEngine(IBackend backend)
        {
            if (backend == null) throw new MSDataLayerException("The backend should not to be null.");

            this.Backend = backend;
            this.BackendType = backend.BackendType;

            if (this.BackendType != BackendType.SQL && this.BackendType != BackendType.Odbc && this.BackendType != BackendType.OleDb && this.BackendType != BackendType.Oracle)
            {
                throw new MSDataLayerException("This type of backend is not supported by parameter engine.");
            }
        }

        public static ParameterEngine New(IBackend backend)
        {
            ParameterEngine ret = new ParameterEngine(backend);
            return ret;
        }
        #endregion / Constructor /

        #region // Properties //
        public List<DbParameter> Parameters
        {
            get { return this._lst; }
        }
        public DbParameter this[int index]
        {
            get
            {
                if (index >= 0 && index < this._lst.Count)
                    return this._lst[index];
                else
                    throw new Exception("Invalid index.");
            }
        }
        public int Count
        {
            get { return this._lst.Count; }
        }
        #endregion / Properties /

        #region // Functions - Public - Add //
        public bool Add(string name, object value)
        {
            DbParameter param = this.newParam();

            param.ParameterName = name;
            param.Value = value;

            this._lst.Add(param);
            return true;
        }
        public bool Add(string name, DbType dataTypeEnum, int size, object value)
        {
            DbParameter param = this.newParam();

            param.DbType = dataTypeEnum;
            param.ParameterName = name;
            param.Value = value;
            param.Size = size;

            this._lst.Add(param);
            return true;
        }
        public bool Add(string name, DbType dataTypeEnum, int size, string sourceColumn, object value)
        {
            DbParameter param = this.newParam();

            param.DbType = dataTypeEnum;
            param.ParameterName = name;
            param.Value = value;
            param.Size = size;
            param.SourceColumn = sourceColumn;

            this._lst.Add(param);
            return true;
        }
        public bool Add(string name, DbType dataTypeEnum, int size, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            DbParameter param = this.newParam();
            param.ParameterName = name;
            param.Value = value;
            param.DbType = dataTypeEnum;
            param.Size = size;
            param.Direction = direction;
            param.SourceColumn = sourceColumn;
            param.SourceVersion = sourceVersion;

            this._lst.Add(param);
            return true;
        }
        public bool AddOutputParameter(string name)
        {
            return this.AddOutputParameter(name, 0);
        }
        public bool AddOutputParameter(string name, int size)
        {
            DbParameter param = this.newParam();

            param.ParameterName = name;
            param.Value = null;
            param.Direction = ParameterDirection.Output;
            if (size > 0) param.Size = size;

            this._lst.Add(param);
            return true;
        }
        public bool AddOutputParameter(string name, DbType dataType, int size)
        {
            DbParameter param = this.newParam();

            param.ParameterName = name;
            param.DbType = dataType;
            param.Value = null;
            param.Direction = ParameterDirection.Output;
            if (size > 0) param.Size = size;

            this._lst.Add(param);
            return true;
        }
        public object RetrieveOutputParameterValue(string parameterName)
        {
            foreach (IDbDataParameter param in this._lst)
            {
                if (param.ParameterName == parameterName && param.Direction == ParameterDirection.Output)
                {
                    return param.Value;
                }
            }

            throw new MSDataLayerException("Cannot find an output parameter with specified name.");
        }
        #endregion / Functions - Public - Add /

        #region // Functions - Public - Enumerator //
        public IEnumerator<DbParameter> GetEnumerator()
        {
            for (int i = 0; i < this._lst.Count; i++)
            {
                yield return this._lst[i];
            }
        }
        #endregion / Functions - Public - Other /

        #region // Functions - Internal //
        internal void Inject(ref DbCommand command)
        {
            IDataParameterCollection pCol = ((DbCommand)command).Parameters;

            foreach (DbParameter p in this._lst)
            {
                pCol.Add(p);
            }
        }
        #endregion / Functions - Internal /

        #region // Functions - Private //
        private DbParameter newParam()
        {
            DbParameter ret = this.Backend.CreateParameter();
            return ret;
        }
        #endregion / Functions - Private /
    }
}