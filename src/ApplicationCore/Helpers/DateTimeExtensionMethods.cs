using System;
using System.Globalization;

namespace Vnit.ApplicationCore.Helpers
{
    public static class DateTimeExtensionMethods
    {
        #region DateTime
        public static string ToDateTimeHourMinString(this DateTime date)
        {
            return date.ToString("HH:mm");
        }
        public static string ToDateTimeYYYYMMDDString(this DateTime date)
        {
            return date.ToString("yyyyMMdd");
        }
        public static string ToDateTimeDDMMYYYYString(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }
        public static string ToDateTimeLocalString(this DateTime date, string format = "dd/MM/yyyy HH:mm")
        {
            return date.ToString(format);
        }
        public static bool IsDateTimeValid(this DateTime date)
        {

            // sql datetime valid 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM

            if (date <= DateTime.MinValue) return false;
            if (date >= DateTime.MaxValue) return false;
            return true;
        }
        public static DateTime ToDateTime(this string dateString, string format = "dd/MM/yyyy")
        {
            DateTime dt = DateTime.Now;

            if (!string.IsNullOrEmpty(dateString))
            {
                if (format.IsNullOrEmpty())
                {
                    var formats = new[] { "dd/MM/yyyy", "d/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/M/yy", "dd/MM/yy" };
                    DateTime.TryParseExact(dateString, formats, CultureInfo.CreateSpecificCulture("vi-VN"), DateTimeStyles.None, out dt);
                }
                else
                {
                    //format =  "dd/MM/yyyy" ;
                    DateTime.TryParseExact(dateString, format, CultureInfo.CreateSpecificCulture("vi-VN"), DateTimeStyles.None, out dt);
                }

            }
            return dt;
        }
        public static DateTime ToDateTimeAddOneDay(this string dateString, string format = "dd/MM/yyyy")
        {
            DateTime dt = DateTime.Now;

            if (!string.IsNullOrEmpty(dateString))
            {
                if (format.IsNullOrEmpty())
                {
                    var formats = new[] { "dd/MM/yyyy", "d/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/M/yy", "dd/MM/yy" };
                    DateTime.TryParseExact(dateString, formats, CultureInfo.CreateSpecificCulture("vi-VN"), DateTimeStyles.None, out dt);
                }
                else
                {
                    //format =  "dd/MM/yyyy" ;
                    DateTime.TryParseExact(dateString, format, CultureInfo.CreateSpecificCulture("vi-VN"), DateTimeStyles.None, out dt);
                }

            }
            return dt.AddDays(1);
        }
        #endregion

        #region TimeSpan
        public static TimeSpan ToTimeSpan(this string timespan, string format = "")
        {
            DateTime dt;
            if (format.IsNullOrEmpty())
            {
                var formats = new[] { "HH:mm", "HH:mm:ss", "h:mm tt" };
                if (!DateTime.TryParseExact(timespan, formats, CultureInfo.InvariantCulture,
                                                           DateTimeStyles.None, out dt))
                {
                    // handle validation error
                }
            }
            else
            {
                if (!DateTime.TryParseExact(timespan, format, CultureInfo.InvariantCulture,
                                                           DateTimeStyles.None, out dt))
                {
                    // handle validation error
                }
            }

            TimeSpan time = dt.TimeOfDay;
            return time;
        }

        public static string ToTimeSpanString(this TimeSpan timeSpan, string format = "{0:hh\\:mm}")
        {
            return string.Format(format, timeSpan);
        }

        public static string TimeSpanToMinutes(this TimeSpan ts, string format = "{0:mm}")
        {
            double delta = ts.TotalSeconds;
            if (delta < 0)
            {
                return "ít phút";
            }
            if (delta < 60)
            {
                return ts.Seconds + " giây";
            }
            if (delta < 3600) // 60 mins * 60 sec
            {
                return ts.Minutes + " phút";
            }
            if (delta < 86400)  // 24 hrs * 60 mins * 60 sec
            {
                return ts.Hours + " giờ";
            }
            
            return string.Format(format, ts);
        }
        #endregion
    }
}
