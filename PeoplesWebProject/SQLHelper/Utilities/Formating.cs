using System;

namespace UCGuideSQLHelper.Utilities
{
    public static class Formating
    {
        public const string DATETIME_STANDARD_FORMAT = "yyyy-MM-dd";

        private static Type DTType = typeof(DateTime);

        // Date Formatting
        public static bool IsDate(object val)
        {
            if (val == null || val == DBNull.Value) return false;
            if (val.GetType() == DTType) return true;

            DateTime ret;
            return DateTime.TryParse(val.ToString().Trim(), out ret);
        }
        public static bool IsDate(object val, string compareFormat)
        {
            if (val == null || val == DBNull.Value) return false;
            if (val.GetType() == DTType) return true;

            DateTime ret;
            return DateTime.TryParseExact(val.ToString().Trim(), compareFormat, null, System.Globalization.DateTimeStyles.None, out ret);
        }

        public static DateTime DateParse(object val)
        {
            if (val == null || val == DBNull.Value) return DateTime.MinValue;
            if (val.GetType() == DTType) return (DateTime)val;

            DateTime ret; DateTime.TryParse(val.ToString().Trim(), out ret);
            return ret;
        }
        public static DateTime DateParse(object val, string compareFormat)
        {
            if (val == null || val == DBNull.Value) return DateTime.MinValue;
            if (val.GetType() == DTType) return (DateTime)val;

            DateTime ret; DateTime.TryParseExact(val.ToString().Trim(), compareFormat, null, System.Globalization.DateTimeStyles.None, out ret);
            return ret;
        }

        // Numeric
        public static bool IsNumeric(object val)
        {
            double ret;
            return double.TryParse(val.ToString(), out ret);
        }
    }
}
