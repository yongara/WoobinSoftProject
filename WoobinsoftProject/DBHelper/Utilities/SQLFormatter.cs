using System;
using System.Collections.Generic;
using System.Text;

namespace DBHelper.Utilities
{
    public class SQLFormatter : ISqlFormatter
    {
        #region // Member Variable //
        private string _sqlTemplate;
        List<string> _list;
        #endregion / Member Variable /

        #region // Constructor //
        public SQLFormatter()
        {
            _sqlTemplate = "";
            _list = new List<string>();
        }
        #endregion / Constructor /

        #region // ISqlFormatter Members //
        public virtual string SqlTemplate
        {
            get
            { return this._sqlTemplate; }
            set
            { this._sqlTemplate = value ==  null ? "" : value; }
        }

        public virtual string Result
        {
            get
            {
                object[] o = (object[])_list.ToArray();

                try
                {
                    return string.Format(this._sqlTemplate, o);
                }
                catch(Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }
            }
        }

        public virtual List<string> DataValues
        {
            get { return this._list; }
        }

        public virtual int Count
        {
            get
            { return this._list.Count; }
            set
            { throw new NotSupportedException("This operation is not supported in this class"); }
        }

        public virtual bool AddData<VType>(IList<VType> values, string seperator, string appendChar, AppendOptions appendOps, bool encloseIfBrackets)
        {
            bool ret = false;

            if (values == null) return ret; if (values.Count == 0) return ret;

            if (seperator == null) seperator = "";
            if (appendChar == null || appendOps == AppendOptions.None) appendChar = "";

            seperator += " ";

            string v = null;
            StringBuilder sb = new StringBuilder();

            try
            {
                foreach (VType vt in values)
                {
                    v = vt.GetType() == typeof(DateTime) ? ((DateTime)(object)vt).ToString("yyyy-mm-dd") : vt.ToString();
                    v = v == null ? "" : v;

                    switch (appendOps)
                    {
                        case AppendOptions.None:
                            {
                                sb.Append(string.Format("{0}{1}", v, seperator)); break;
                            }
                        case AppendOptions.First:
                            {
                                sb.Append(string.Format("{0}{1}{2}", appendChar, v, seperator)); break;
                            }
                        case AppendOptions.Last:
                            {
                                sb.Append(string.Format("{0}{1}{2}", v, appendChar, seperator)); break;
                            }
                        case AppendOptions.Both:
                            {
                                string s2 = appendChar;
                                if (encloseIfBrackets)
                                {
                                    if (appendChar.Contains("(")) s2 = ")";
                                    else if (appendChar.Contains("[")) s2 = "]";
                                    else if (appendChar.Contains("{")) s2 = "}";
                                }
                                sb.Append(string.Format("{0}{1}{2}{3}", appendChar, v, s2, seperator)); break;
                            }
                    }
                }
                string add = sb.ToString().TrimEnd(seperator.ToCharArray());
                this._list.Add(add);
                ret = true;
            }
            catch
            {
                ret = false;
            }

            return ret;
        }
        #endregion / ISqlFormatter Members /
    }
}
