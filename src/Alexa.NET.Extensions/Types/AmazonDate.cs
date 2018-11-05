using Alexa.NET.Extensions.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Alexa.NET.Extensions.Types
{
    public class AmazonDate
    {
        private const char SplitChar = '-';
        private const string GenericSegment = "-XX";
        private const string ShortDateFormat = "yyyy-MM-dd";
        private const string WeekString = "W";
        private const string WeekendSuffix = "WE";

        public AmazonDate(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public static AmazonDate Parse(string value, IEnumerable<Season> seasons, bool useGregorianCalendar = false)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Contains(GenericSegment))
            {
                value = value.Replace(GenericSegment, string.Empty);
            }

            var season = seasons.FirstOrDefault(s => value.EndsWith(s.Name, StringComparison.OrdinalIgnoreCase));
            if (season != null)
            {
                return ParseSeason(value, season);
            }

            if (value.Length == 4)
            {
                return ParseYear(value);
            }

            if (DateTime.TryParseExact(value, ShortDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime date))
            {
                return new AmazonDate(date, date);
            }

            var segments = value.Split(SplitChar);
            if (segments.Length >= 2 && segments[1].StartsWith(WeekString, StringComparison.OrdinalIgnoreCase))
            {
                return ParseWeekOfYear(segments, useGregorianCalendar);
            }

            return ParseMonthOfYear(segments);
        }

        private static AmazonDate ParseSeason(string value, Season season)
        {
            //Season
            var yearSegment = value.Split(SplitChar).FirstOrDefault() ?? string.Empty;
            if (!int.TryParse(yearSegment, out int year))
            {
                throw new ArgumentException("Invalid year: " + yearSegment, nameof(value));
            }

            var startDate = new DateTime(year, season.StartMonth, season.StartDay).AsNewUtcDateTime();
            var endDate = new DateTime(year, season.EndMonth, season.EndDay).AsNewUtcDateTime();

            return new AmazonDate(startDate, endDate);
        }

        private static AmazonDate ParseYear(string value)
        {
            if (int.TryParse(value, out int year))
            {
                //Single year
                var startDate = new DateTime(year, 1, 1).AsNewUtcDateTime();
                var endDate = new DateTime(year, 12, 31).AsNewUtcDateTime();

                return new AmazonDate(startDate, endDate);
            }
            else
            {
                //Decade
                var startDate = new DateTime(DateTime.Today.Year, 1, 1).AsNewUtcDateTime();
                var endDate = startDate.AddYears(9);

                return new AmazonDate(startDate, endDate);
            }
        }

        private static AmazonDate ParseWeekOfYear(string[] segments, bool useGregorianCalendar)
        {
            //Week
            if (!int.TryParse(segments[0], out int year))
            {
                throw new ArgumentException("Invalid year: " + segments[0], nameof(segments));
            }

            if (!int.TryParse(segments[1].Replace(WeekString, string.Empty), out int weekNumber))
            {
                throw new ArgumentException("Invalid week number: " + segments[1], nameof(segments));
            }

            var startDate = new DateTime(year, 1, 1).AddWeeks(weekNumber - 1, useGregorianCalendar).AsNewUtcDateTime();
            var endDate = startDate.AddDays(6);

            if (!segments.Last().EndsWith(WeekendSuffix, StringComparison.OrdinalIgnoreCase))
            {
                return new AmazonDate(startDate, endDate);
            }

            //Weekend of the week
            if (startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday)
            {
                do
                {
                    startDate = startDate.AddDays(1);
                } while (startDate.DayOfWeek != DayOfWeek.Saturday);
                endDate = startDate.AddDays(1);
            }
            else if (startDate.DayOfWeek == DayOfWeek.Sunday)
            {
                endDate = startDate;
                startDate = startDate.AddDays(-1);
            }

            return new AmazonDate(startDate, endDate);
        }

        private static AmazonDate ParseMonthOfYear(string[] segments)
        {
            //Month of year
            if (!int.TryParse(segments[0], out int year))
            {
                throw new ArgumentException("Invalid year: " + segments[0], nameof(segments));
            }

            if (!int.TryParse(segments[1], out int month))
            {
                throw new ArgumentException("Invalid month: " + segments[1], nameof(segments));
            }

            var startDate = new DateTime(year, month, 1).AsNewUtcDateTime();
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return new AmazonDate(startDate, endDate);
        }
    }
}
