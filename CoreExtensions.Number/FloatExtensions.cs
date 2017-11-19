using System;

namespace CoreExtensions
{
    /// <summary>
    ///     Extension methods for the Float data type
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        ///     Gets a TimeSpan from a float number of days.
        /// </summary>
        /// <param name="days">The number of days the TimeSpan will contain.</param>
        /// <returns>A TimeSpan containing the specified number of days.</returns>
        /// <remarks>
        ///     Contributed by jceddy
        /// </remarks>
        public static TimeSpan Days(this float days)
        {
            return TimeSpan.FromDays(days);
        }

        /// <summary>
        ///     Gets a TimeSpan from a float number of hours.
        /// </summary>
        /// <param name="hours">The number of hours the TimeSpan will contain.</param>
        /// <returns>A TimeSpan containing the specified number of hours.</returns>
        /// <remarks>
        ///     Contributed by jceddy
        /// </remarks>
        public static TimeSpan Hours(this float hours)
        {
            return TimeSpan.FromHours(hours);
        }

        /// <summary>Checks whether the value is in range</summary>
        /// <param name="value">The Value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        public static bool InRange(this float value, float minValue, float maxValue)
        {
            return value >= minValue && value <= maxValue;
        }

        /// <summary>
        ///     Gets a TimeSpan from a float number of milliseconds.
        /// </summary>
        /// <param name="milliseconds">The number of milliseconds the TimeSpan will contain.</param>
        /// <returns>A TimeSpan containing the specified number of milliseconds.</returns>
        /// <remarks>
        ///     Contributed by jceddy
        /// </remarks>
        public static TimeSpan Milliseconds(this float milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds);
        }

        /// <summary>
        ///     Gets a TimeSpan from a float number of minutes.
        /// </summary>
        /// <param name="minutes">The number of minutes the TimeSpan will contain.</param>
        /// <returns>A TimeSpan containing the specified number of minutes.</returns>
        /// <remarks>
        ///     Contributed by jceddy
        /// </remarks>
        public static TimeSpan Minutes(this float minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }

        /// <summary>
        /// Toes the percent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="percentOf">The percent of.</param>
        /// <returns></returns>
        public static decimal PercentageOf(this float value, int percentOf)
        {
            return (decimal)(value / percentOf * 100);
        }

        /// <summary>
        /// Toes the percent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="percentOf">The percent of.</param>
        /// <returns></returns>
        public static decimal PercentageOf(this float value, float percentOf)
        {
            return (decimal)(value / percentOf * 100);
        }

        /// <summary>
        /// Toes the percent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="percentOf">The percent of.</param>
        /// <returns></returns>
        public static decimal PercentageOf(this float value, double percentOf)
        {
            return (decimal)(value / percentOf * 100);
        }

        /// <summary>
        /// Toes the percent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="percentOf">The percent of.</param>
        /// <returns></returns>
        public static decimal PercentageOf(this float value, long percentOf)
        {
            return (decimal)(value / percentOf * 100);
        }

        /// <summary>
        ///     Gets a TimeSpan from a float number of seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds the TimeSpan will contain.</param>
        /// <returns>A TimeSpan containing the specified number of seconds.</returns>
        /// <remarks>
        ///     Contributed by jceddy
        /// </remarks>
        public static TimeSpan Seconds(this float seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }
    }
}
