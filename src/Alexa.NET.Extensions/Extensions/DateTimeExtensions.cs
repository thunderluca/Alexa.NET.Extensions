using System.Globalization;

namespace System
{
    public static class DateTimeExtensions
    {
        public static DateTime AddWeeks(this DateTime dateTime, int weeks, bool useGregorianCalendar)
        {
            return useGregorianCalendar 
                ? new GregorianCalendar().AddWeeks(dateTime, weeks)
                : CultureInfo.InvariantCulture.Calendar.AddWeeks(dateTime, weeks);
        }

        public static DateTime AsNewUtcDateTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, DateTimeKind.Utc);
        }

        public static string ToUtcString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public static string ToAlexaDate(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }
    }
}
