using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilityClasses
{
    public static partial class DateTimeExtensions
    {
        public static string ToJavaScript(this DateTime? date)
        {
            return "Date.UTC(" + date.GetValueOrDefault().Year + "," + (date.GetValueOrDefault().Month - 1) + "," + date.GetValueOrDefault().Day + ")";
        }

        public static string ToJavaScript(this DateTime date)
        {
            DateTime? Temp = date;
            return ToJavaScript(Temp);
        }

        public static string ToSQL(this DateTime? date, bool StringOnlyNoConvert = false)
        {
            if (date == null)
            {
                return "NULL";
            }
            string Year, Month, Day, Hour, Minute, Second, Millisecond;
            Year = date.GetValueOrDefault().Year.ToString();
            Month = "0" + date.GetValueOrDefault().Month;
            Month = Month.Substring(Month.Length - 2);
            Day = "0" + date.GetValueOrDefault().Day;
            Day = Day.Substring(Day.Length - 2);
            Hour = "0" + date.GetValueOrDefault().Hour;
            Hour = Hour.Substring(Hour.Length - 2);
            Minute = "0" + date.GetValueOrDefault().Minute;
            Minute = Minute.Substring(Minute.Length - 2);
            Second = "0" + date.GetValueOrDefault().Second;
            Second = Second.Substring(Second.Length - 2);
            Millisecond = date.GetValueOrDefault().Millisecond.ToString();
            if (StringOnlyNoConvert)
            {
                return "'" + Year + "-" + Month + "-" + Day + " " + Hour + ":" + Minute + ":" + Second + "." + Millisecond +"'";
            }
            return "CONVERT(datetime, '" + Year + "-" + Month + "-" + Day + " " + Hour + ":" + Minute + ":" + Second + "', 120)";
        }

        public static string ToSQL(this DateTime date, bool StringOnlyNoConvert = false)
        {
            DateTime? Temp = date;
            return ToSQL(Temp, StringOnlyNoConvert);
        }

        public static DateTime AddIntervals(this DateTime ReferenceDate, int IntervalsToAdd, int Seasonality)
        {
            DateTime ReturnValue = ReferenceDate;
            switch (Seasonality)
            {
                case 7:  // Daily
                    ReturnValue = ReturnValue.AddDays(IntervalsToAdd);
                    break;

                case 52: // Weekly
                    ReturnValue = ReturnValue.AddDays(IntervalsToAdd * 7);
                    break;

                case 12: // Monthly
                    ReturnValue = ReturnValue.AddMonths(IntervalsToAdd);
                    break;

                case 4:  // Quarterly
                    ReturnValue = ReturnValue.AddMonths(IntervalsToAdd * 3);
                    break;

                case 1:  // Yearly
                    ReturnValue = ReturnValue.AddYears(IntervalsToAdd);
                    break;

                default:
                    break;
            }
            return ReturnValue;
        }


        private static int DateValue(this DateTime dt)
        {
            return dt.Year * 372 + (dt.Month - 1) * 31 + dt.Day - 1;
        }

        public static int YearsBetween(this DateTime dt, DateTime dt2)
        {
            return dt.MonthsBetween(dt2) / 12;
        }
        public static int YearsBetween(this DateTime dt, DateTime dt2, bool includeLastDay)
        {
            return dt.MonthsBetween(dt2, includeLastDay) / 12;
        }
        public static int YearsBetween(this DateTime dt, DateTime dt2, bool includeLastDay, out int excessMonths)
        {
            int months = dt.MonthsBetween(dt2, includeLastDay);
            excessMonths = months % 12;
            return months / 12;
        }
        public static int MonthsBetween(this DateTime dt, DateTime dt2)
        {
            int months = (dt2.DateValue() - dt.DateValue()) / 31;
            return Math.Abs(months);
        }
        public static int MonthsBetween(this DateTime dt, DateTime dt2, bool includeLastDay)
        {
            if (!includeLastDay) return dt.MonthsBetween(dt2);
            int days;
            if (dt2 >= dt)
                days = dt2.AddDays(1).DateValue() - dt.DateValue();
            else
                days = dt.AddDays(1).DateValue() - dt2.DateValue();
            return days / 31;
        }
        public static int WeeksBetween(this DateTime dt, DateTime dt2)
        {
            return dt.DaysBetween(dt2) / 7;
        }
        public static int WeeksBetween(this DateTime dt, DateTime dt2, bool includeLastDay)
        {
            return dt.DaysBetween(dt2, includeLastDay) / 7;
        }
        public static int WeeksBetween(this DateTime dt, DateTime dt2, bool includeLastDay, out int excessDays)
        {
            int days = dt.DaysBetween(dt2, includeLastDay);
            excessDays = days % 7;
            return days / 7;
        }
        public static int DaysBetween(this DateTime dt, DateTime dt2)
        {
            return (dt2.Date - dt.Date).Duration().Days;
        }
        public static int DaysBetween(this DateTime dt, DateTime dt2, bool includeLastDay)
        {
            int days = dt.DaysBetween(dt2);
            if (!includeLastDay) return days;
            return days + 1;
        }

        public static DateTime FirstDayOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1);
        }
        public static DateTime FirstDayOfYear(this DateTime dt, DayOfWeek dow)
        {
            return dt.FirstDayOfYear().NextDay(dow, true);
        }
        public static DateTime LastDayOfYear(this DateTime dt)
        {
            return dt.FirstDayOfYear().AddYears(1).AddDays(-1);
        }
        public static DateTime LastDayOfYear(this DateTime dt, DayOfWeek dow)
        {
            return dt.LastDayOfYear().PreviousDay(dow, true);
        }
        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
        public static DateTime FirstDayOfMonth(this DateTime dt, DayOfWeek dow)
        {
            return dt.FirstDayOfMonth().NextDay(dow, true);
        }
        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }
        public static DateTime LastDayOfMonth(this DateTime dt, DayOfWeek dow)
        {
            return dt.LastDayOfMonth().PreviousDay(dow, true);
        }
        public static DateTime PreviousDay(this DateTime dt)
        {
            return dt.Date.AddDays(-1);
        }
        public static DateTime PreviousDay(this DateTime dt, DayOfWeek dow)
        {
            return dt.PreviousDay(dow, false);
        }
        public static DateTime PreviousDay(this DateTime dt, DayOfWeek dow, bool includeThis)
        {
            int diff = dt.DayOfWeek - dow;
            if ((includeThis && diff < 0) || (!includeThis && diff <= 0)) diff += 7;
            return dt.Date.AddDays(-diff);
        }
        public static DateTime NextDay(this DateTime dt)
        {
            return dt.Date.AddDays(1);
        }
        public static DateTime NextDay(this DateTime dt, DayOfWeek dow)
        {
            return dt.NextDay(dow, false);
        }
        public static DateTime NextDay(this DateTime dt, DayOfWeek dow, bool includeThis)
        {
            int diff = dow - dt.DayOfWeek;
            if ((includeThis && diff < 0) || (!includeThis && diff <= 0)) diff += 7;
            return dt.Date.AddDays(diff);
        }
        public static int DaysInYear(this DateTime dt)
        {
            return (dt.LastDayOfYear() - dt.FirstDayOfYear()).Days + 1;
        }
        public static int DaysInYear(this DateTime dt, DayOfWeek dow)
        {
            return (dt.LastDayOfYear(dow).DayOfYear - dt.FirstDayOfYear(dow).DayOfYear) / 7 + 1;
        }
        public static int DaysInMonth(this DateTime dt)
        {
            return (dt.LastDayOfMonth() - dt.FirstDayOfMonth()).Days + 1;
        }
        public static int DaysInMonth(this DateTime dt, DayOfWeek dow)
        {
            return (dt.LastDayOfMonth(dow).Day - dt.FirstDayOfMonth(dow).Day) / 7 + 1;
        }
        public static bool IsLeapYear(this DateTime dt)
        {
            return dt.DaysInYear() == 366;
        }
        public static DateTime AddWeeks(this DateTime dt, int weeks)
        {
            return dt.AddDays(7 * weeks);
        }
    }
}
