using System;
using System.Collections.Generic;

namespace CoreExtensions
{
    public static class LongExtensions
    {
        /// <summary>
        ///     Converts a Unix Time Stamp (long / Int64 representing the number of seconds since Jan 1, 1970) to a DateTime
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ConvertFromUnixTimestamp(this long timestamp)
        {
            var dt = new DateTime(1970, 1, 1);
            return dt.AddSeconds(timestamp);
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this long number, int percent)
        {
            return (decimal)(number * percent / 100);
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this long number, float percent)
        {
            return (decimal)(number * percent / 100);
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this long number, double percent)
        {
            return (decimal)(number * percent / 100);
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this long number, decimal percent)
        {
            return (number * percent / 100);
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this long number, long percent)
        {
            return (number * percent / 100);
        }

        public static long SumOfDigets(this long number)
        {
            long sum = 0;
            while (number / 10 != 0)
            {
                sum = (sum + number % 10);
                number = number / 10;
            }
            return (sum + number);
        }

        /// <summary>
        ///     Gets a TimeSpan from a long number of ticks.
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns>A TimeSpan containing the specified number of ticks.</returns>
        /// <remarks>
        ///     Contributed by jceddy
        /// </remarks>
        public static TimeSpan Ticks(this long ticks)
        {
            return TimeSpan.FromTicks(ticks);
        }

        public static IEnumerable<long> Times(this long times)
        {
            return Times(times, i => i);
        }

        public static IEnumerable<T> Times<T>(this long times, Func<long, T> func)
        {
            for (long i = 0; i < times; i++)
            {
                yield return func(i);
            }
        }

        /// <summary>
        ///     Performs the specified action n times based on the underlying long value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        public static void Times(this long value, Action action)
        {
            for (var i = 0; i < value; i++)
                action();
        }

        /// <summary>
        ///     Performs the specified action n times based on the underlying long value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        public static void Times(this long value, Action<long> action)
        {
            for (var i = 0; i < value; i++)
                action(i);
        }

        /// <summary>
        ///     Converts the value to ordinal string. (English)
        /// </summary>
        /// <param name="i">Object value</param>
        /// <returns>Returns string containing ordinal indicator adjacent to a numeral denoting. (English)</returns>
        public static string ToOrdinal(this long i)
        {
            var suffix = "th";
            switch (i % 100)
            {
                case 11:
                case 12:
                case 13:
                    break;

                default:
                    switch (i % 10)
                    {
                        case 1:
                            suffix = "st";
                            break;

                        case 2:
                            suffix = "nd";
                            break;

                        case 3:
                            suffix = "rd";
                            break;
                    }
                    break;
            }

            return string.Format("{0}{1}", i, suffix);
        }

        /// <summary>
        ///     Converts the value to ordinal string with specified format. (English)
        /// </summary>
        /// <param name="i">Object value</param>
        /// <param name="format">A standard or custom format string that is supported by the object to be formatted.</param>
        /// <returns>Returns string containing ordinal indicator adjacent to a numeral denoting. (English)</returns>
        public static string ToOrdinal(this long i, string format)
        {
            return string.Format(format, i.ToOrdinal());
        }
    }
}
