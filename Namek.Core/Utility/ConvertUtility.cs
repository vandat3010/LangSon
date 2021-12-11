using System;
using System.Linq;

namespace Namek.Core.Utility
{
    public class ConvertUtility
    {
        public static string FormatTimeVn(DateTime dt, string defaultText)
        {
            return ToDateTime(dt) != new DateTime(1900, 1, 1) ? dt.ToString("dd-mm-yy") : defaultText;
        }

        public static short ToInt16(object obj)
        {
            short retVal;
            try
            {
                retVal = Convert.ToInt16(obj);
            }
            catch
            {
                retVal = 0;
            }
            return retVal;
        }

        public static int ToInt32(object obj)
        {
            int retVal;
            try
            {
                retVal = Convert.ToInt32(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static long ToInt64(object obj)
        {
            long retVal;

            try
            {
                retVal = Convert.ToInt64(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static int ToInt32(object obj, int defaultValue)
        {
            int retVal;
            try
            {
                retVal = int.Parse(obj.ToString());
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }

        public static byte ToByte(object obj, byte defaultValue)
        {
            byte retVal;
            try
            {
                retVal = byte.Parse(obj.ToString());
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }

        public static string ToString(object obj)
        {
            string retVal;

            try
            {
                retVal = Convert.ToString(obj);
            }
            catch
            {
                retVal = "";
            }

            return retVal;
        }

        public static DateTime ToDateTime(object obj)
        {
            DateTime retVal;
            try
            {
                retVal = Convert.ToDateTime(obj);
            }
            catch
            {
                retVal = DateTime.Now;
            }
            if (retVal == new DateTime(1, 1, 1)) return DateTime.Now;

            return retVal;
        }

        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            DateTime retVal;
            try
            {
                retVal = Convert.ToDateTime(obj);
            }
            catch
            {
                retVal = DateTime.Now;
            }
            if (retVal == new DateTime(1, 1, 1)) return defaultValue;

            return retVal;
        }

        public static bool ToBoolean(object obj)
        {
            bool retVal;

            try
            {
                retVal = Convert.ToBoolean(obj);
            }
            catch
            {
                retVal = false;
            }

            return retVal;
        }

        public static double ToDouble(object obj)
        {
            double retVal;

            try
            {
                retVal = Convert.ToDouble(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static double ToDouble(object obj, double defaultValue)
        {
            double retVal;

            try
            {
                retVal = Convert.ToDouble(obj);
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }

        //ham chuyen kieu du lieu dinh dang MM/dd/yyyy sang dd/MM/yyyy
        public static string ConvertMdytoDMY(string date)
        {
            var edate = date.Split(' ');
            try
            {
                var mDate = edate[0];
                var dDate = mDate.Split('/');
                var dateEnd = dDate[1] + "/" + dDate[0] + "/" + dDate[2];
                return dateEnd;
            }
            catch
            {
                return "";
            }
        }

        public static string SetShortTile(string input, int length)
        {
            string output;
            if (input.Length < length)
            {
                output = input;
            }
            else
            {
                var sublengthTile = input.Substring(0, length);
                var tmpTitle = sublengthTile.Split(' ');
                output = string.Join(" ", tmpTitle, 0, tmpTitle.Length - 1) + "...";
            }
            return output;
        }

        public static string SetShortTile(string input)
        {
            if (input.Length > 8)
                return input.Substring(0, 8);
            return input;
        }

        public static string CutExtensionEmail(string input)
        {
            var arrinput = input.Split(' ');
            if (arrinput.Length > 0)
                input = (from t in arrinput
                    where t.Contains("@")
                    select t.Split('@')
                    into arrEmail
                    select "@" + arrEmail[1]).Aggregate(input, (current, tmp) => current.Replace(tmp, ""));
            return input;
        }

        public static DateTime RfcTimeToDateTime(string rfcTime)
        {
            var result = new DateTime(3000, 01, 01);
            try
            {
                var year = Convert.ToInt32(rfcTime.Substring(0, 4));
                var month = Convert.ToInt32(rfcTime.Substring(4, 2));
                var day = Convert.ToInt32(rfcTime.Substring(6, 2));
                var hour = Convert.ToInt32(rfcTime.Substring(8, 2));
                var min = Convert.ToInt32(rfcTime.Substring(10, 2));
                var sec = Convert.ToInt32(rfcTime.Substring(12, 2));
                result = new DateTime(year, month, day, hour, min, sec);
            }
            catch { }

            return result;
        }

        public static string DateTimeToRfcTime(DateTime time)
        {
            var retVal = string.Empty;
            retVal += time.Year.ToString("0000");
            retVal += time.Month.ToString("00");
            retVal += time.Day.ToString("00");
            retVal += time.Hour.ToString("00");
            retVal += time.Minute.ToString("00");
            retVal += time.Second.ToString("00");
            return retVal;
        }

        public static string StringForNull(object x)
        {
            return x == null ? "" : x.ToString();
        }
    }
}