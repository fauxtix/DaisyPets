using System.Globalization;

namespace DaisyPets.Core.Application.Formatting
{
    public class DataFormat
    {
        public static string DateToDB(string date)
        {
            DateTime dt = Convert.ToDateTime(date);
            return dt.ToString("MMddyyyy");
        }

        public static string GetDBDate(string date)
        {
            return GetDateTime(date).ToShortDateString();
        }

        public static string GetDBDate(object date)
        {
            return GetDateTime(date).ToShortDateString();
        }

        public static string GetDBDate(DateTime date)
        {
            return date.ToShortDateString();
        }

        public static string GetCurrentDate()
        {
            DateTime dt = System.DateTime.Now;
            return dt.ToString("MMddyyyy");
        }

        public static string DateToDisp(string date)
        {
            string[] dateString = new string[3];
            DateTime dt = Convert.ToDateTime(date);
            dateString = dt.ToString("dd MMM yyyy").Split(Convert.ToChar(" "));
            return dateString[0] + " " + dateString[1] + ", " + dateString[2];
        }

        public static string GetDateFromDBDate(string date)
        {
            string dateReturn = string.Empty;
            if (date.Trim().Length < 8)
                date = "0" + date.Trim();

            string month = date.Substring(0, 2);
            string date1 = date.Substring(2, 2);
            string year = date.Substring(4);
            dateReturn = month + "/" + date1 + "/" + year;

            dateReturn = DateToDisp(dateReturn);
            return dateReturn;
        }

        public static string GetMonth(string date)
        {
            string dateToDB = DateToDB(date);
            return dateToDB.Substring(0, 2);
        }

        public static string GetYear(string date)
        {
            string dateToDB = DateToDB(date);
            return dateToDB.Substring(4, 4);
        }

        public static string GetDate(string date)
        {
            string dateToDB = DateToDB(date);
            return dateToDB.Substring(2, 2);
        }

        #region Core Formatting Metods
        public static bool IsValidDate(string date)
        {
            bool retValue = false;
            DateTime result = new DateTime();
            if (DateTime.TryParse(date, out result))
                retValue = true;

            return retValue;
        }

        public static bool IsValidDate(object date)
        {
            bool retValue = false;
            DateTime result = new DateTime();

            if (date != null)
            {
                if (DateTime.TryParse(date.ToString(), out result))
                    retValue = true;
            }

            return retValue;
        }

        public static DateTime GetDateTime(string date)
        {
            DateTime retValue = new DateTime();
            if (IsValidDate(date))
                retValue = Convert.ToDateTime(date);

            return retValue;
        }

        public static DateTime GetDateTime(object date)
        {
            DateTime retValue = new DateTime();
            if (IsValidDate(date))
                retValue = Convert.ToDateTime(date);

            return retValue;
        }


        public static bool IsNumeric(object value)
        {
            bool retValue = false;

            if (value != null)
                retValue = IsNumeric(value.ToString());

            return retValue;
        }

        public static bool IsNumeric(string value)
        {
            bool retValue = false;
            double result = 0;
            if (value != null)
                retValue = double.TryParse(value, out result);

            return retValue;
        }


        public static bool IsInteger(object value)
        {
            bool retValue = false;

            if (value != null)
                retValue = IsInteger(value.ToString());

            return retValue;
        }

        public static bool IsInteger(string value)
        {
            bool retValue = false;
            int result = 0;
            if (value != null)
                retValue = int.TryParse(value, out result);

            return retValue;
        }

        public static bool IsBoolean(string value)
        {
            bool retValue = false;
            bool result = false;
            if (value != null)
                retValue = Boolean.TryParse(value, out result);

            return retValue;
        }


        public static bool IsBoolean(object value)
        {
            bool retValue = false;

            if (value != null)
                retValue = IsBoolean(value.ToString());

            return retValue;
        }

        public static string GetString(object value)
        {
            string retValue = string.Empty;
            if (value != null)
                retValue = value.ToString();

            return retValue;
        }


        public static int GetInteger(object value)
        {
            int retValue = 0;

            if (IsInteger(value))
                retValue = Convert.ToInt16(value);

            return retValue;
        }


        public static int GetInteger(string value)
        {
            int retValue = 0;

            if (IsInteger(value))
                retValue = Convert.ToInt16(value);

            return retValue;
        }

        public static double GetDouble(object value)
        {
            double retValue = 0;

            if (IsNumeric(value))
                retValue = Convert.ToDouble(value);

            return retValue;
        }


        public static double GetDouble(string value)
        {
            double retValue = 0;

            if (IsNumeric(value))
                retValue = Convert.ToDouble(value);

            return retValue;
        }

        public static bool GetBoolean(object value)
        {
            bool retValue = false;

            if (IsBoolean(value))
                retValue = Convert.ToBoolean(value);

            return retValue;
        }


        public static bool GetBoolean(string value)
        {
            bool retValue = false;

            if (IsBoolean(value))
                retValue = Convert.ToBoolean(value);

            return retValue;
        }


        #endregion

        #region DataParse
        public static decimal DecimalParse(object ob)
        {
            if (ob == null)
                return 0;
            if (ob is DBNull)
                return 0;

            decimal a = 0;
            if (ob.GetType() == typeof(bool))
            {
                if ((bool)ob)
                    return 1;
                else
                    return 0;
            }
            else if (decimal.TryParse(ob + "", out a))
            {

            }
            return a;
        }

        public static decimal DecimalParse(System.DBNull ob)
        {
            return 0;
        }

        public static bool BoolParse(object ob)
        {
            double a = 0;
            if (ob == null || ob is DBNull)
                return false;
            else if (Double.TryParse(ob.ToString(), out a))
            {
                if (a > 0)
                    return true;
            }
            else if ((ob + "").ToUpper() == "TRUE")
                return true;
            else if ((ob + "").ToUpper() == "YES")
                return true;

            return false;
        }

        public static bool BoolParse(System.DBNull ob)
        {
            return false;
        }

        public static int IntParse(System.DBNull ob)
        {
            return 0;
        }

        public static int IntParse(object ob)
        {
            int a = 0;
            if (ob == null || ob is DBNull)
                return 0;
            else if (ob.GetType() == typeof(bool))
            {
                if ((bool)ob)
                    return 1;
                else
                    return 0;
            }
            else if (int.TryParse(ob + "", out a))
            {

            }
            return a;
        }

        public static DateTime DateParse(object ob)
        {
            //if (ob is System.DBNull)
            //    return DateTime.Now;
            DateTime dtime = DateTime.MinValue;
            DateTime.TryParse(ob + "", out dtime);
            return dtime;
        }

        public static string DateParseMySQLString(string s)
        {
            DateTime d = DateTime.MinValue;
            DateTimeFormatInfo df1 = new DateTimeFormatInfo();
            df1.DateSeparator = "-";
            DateTimeFormatInfo df2 = new DateTimeFormatInfo();
            df2.DateSeparator = "/";
            DateTimeFormatInfo df3 = new DateTimeFormatInfo();
            df3.DateSeparator = "\\";
            DateTimeFormatInfo df4 = new DateTimeFormatInfo();
            df4.DateSeparator = ".";

            if (DateTime.TryParseExact(s, "dd-MM-yyyy", df1, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d.ToString("yyyy-MM-dd 00:00:00");
            }

            if (DateTime.TryParseExact(s, "dd-MM-yyyy", df2, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d.ToString("yyyy-MM-dd 00:00:00");
            }

            if (DateTime.TryParseExact(s, "dd-MM-yyyy", df3, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d.ToString("yyyy-MM-dd 00:00:00");
            }

            if (DateTime.TryParseExact(s, "dd-MM-yyyy", df4, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d.ToString("yyyy-MM-dd 00:00:00");
            }

            if (DateTime.TryParseExact(s, "dd-MM-yyyy HH:mm:ss", df4, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d.ToString("yyyy-MM-dd HH:mm:ss");
            }

            return "0001-01-01 00:00:00";
        }

        public static string DateParseMySQLtoCSharp(object ob)
        {
            DateTime dtime = DateTime.MinValue;
            if (DateTime.TryParse(ob + "", out dtime))
            {
                if (dtime == DateTime.MinValue)
                    return "";
                else
                    return dtime.ToString("dd-MM-yyyy");
            }
            else
                return "";
        }

        public static DateTime DateParseExact(string s)
        {
            DateTime d = DateTime.MinValue;
            DateTimeFormatInfo df1 = new DateTimeFormatInfo();
            df1.DateSeparator = "-";
            DateTimeFormatInfo df2 = new DateTimeFormatInfo();
            df2.DateSeparator = "/";
            DateTimeFormatInfo df3 = new DateTimeFormatInfo();
            df3.DateSeparator = "\\";
            DateTimeFormatInfo df4 = new DateTimeFormatInfo();
            df4.DateSeparator = ".";

            if (DateTime.TryParseExact(s, "dd-MM-yyyy", df1, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd/MM/yyyy", df2, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd\\MM\\yyyy", df3, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd.MM.yyyy", df4, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            // =============================================

            if (DateTime.TryParseExact(s, "d-M-yyyy", df1, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd-M-yyyy", df1, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d-MM-yyyy", df1, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d/M/yyyy", df2, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d/MM/yyyy", df2, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd/M/yyyy", df2, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d\\M\\yyyy", df3, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d\\MM\\yyyy", df3, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd\\M\\yyyy", df3, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d.M.yyyy", df4, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d.MM.yyyy", df4, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd.M.yyyy", df4, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            return DateTime.MinValue;
        }

        public static DateTime DateTimeParseExact(string s)
        {
            DateTime d = DateTime.MinValue;
            DateTimeFormatInfo df1 = new DateTimeFormatInfo();
            df1.DateSeparator = "-";
            df1.TimeSeparator = ":";
            df1.PMDesignator = "pm";
            df1.AMDesignator = "am";

            DateTimeFormatInfo df2 = new DateTimeFormatInfo();
            df2.DateSeparator = "/";
            df2.TimeSeparator = df1.TimeSeparator;
            df2.PMDesignator = df1.PMDesignator;
            df2.AMDesignator = df1.AMDesignator;

            DateTimeFormatInfo df3 = new DateTimeFormatInfo();
            df3.DateSeparator = "\\";
            df3.TimeSeparator = df1.TimeSeparator;
            df3.PMDesignator = df1.PMDesignator;
            df3.AMDesignator = df1.AMDesignator;

            DateTimeFormatInfo df4 = new DateTimeFormatInfo();
            df4.DateSeparator = ".";
            df4.TimeSeparator = df1.TimeSeparator;
            df4.PMDesignator = df1.PMDesignator;
            df4.AMDesignator = df1.AMDesignator;

            if (DateTime.TryParseExact(s, "dd-MM-yyyy hh:mm tt", df1, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd/MM/yyyy hh:mm tt", df2, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd\\MM\\yyyy hh:mm tt", df3, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd.MM.yyyy hh:mm tt", df4, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            return DateTime.MinValue;
        }

        #endregion

    }
}
