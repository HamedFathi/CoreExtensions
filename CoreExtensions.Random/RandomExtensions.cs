using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreExtensions
{
    public static class RandomExtensions
    {
        /// <summary>
        ///     A Random extension method that flip a coin toss.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true 50% of time, otherwise false.</returns>
        public static bool CoinToss(this Random @this)
        {
            return @this.Next(2) == 0;
        }

        public static decimal NextDecimal(this Random rg)
        {
            var sign = rg.Next(2) == 1;
            return rg.NextDecimal(sign);
        }

        public static decimal NextDecimal(this Random rg, bool sign)
        {
            var scale = (byte)rg.Next(29);
            return new decimal(rg.NextInt32(),
                rg.NextInt32(),
                rg.NextInt32(),
                sign,
                scale);
        }

        public static decimal NextDecimal(this Random rg, decimal maxValue)
        {
            return rg.NextNonNegativeDecimal() / decimal.MaxValue * maxValue;
            ;
        }

        public static decimal NextDecimal(this Random rg, decimal minValue, decimal maxValue)
        {
            if (minValue >= maxValue)
            {
                throw new InvalidOperationException();
            }
            var range = maxValue - minValue;
            return rg.NextDecimal(range) + minValue;
        }

        public static double NextDouble(this Random rnd, double min, double max)
        {
            return rnd.NextDouble() * (max - min) + min;
        }

        public static int NextInt32(this Random rg)
        {
            var firstBits = rg.Next(0, 1 << 4) << 28;
            var lastBits = rg.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        public static long NextInt64(this Random rg, long maxValue)
        {
            return (long)(rg.NextNonNegativeLong() / (double)long.MaxValue * maxValue);
        }

        public static long NextInt64(this Random rg, long minValue, long maxValue)
        {
            if (minValue >= maxValue)
            {
                throw new InvalidOperationException();
            }
            var range = maxValue - minValue;
            return rg.NextInt64(range) + minValue;
        }

        public static long NextInt64(this Random rnd)
        {
            var buffer = new byte[sizeof(long)];
            rnd.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        public static decimal NextNonNegativeDecimal(this Random rg)
        {
            return rg.NextDecimal(false);
        }

        public static long NextNonNegativeLong(this Random rg)
        {
            var bytes = new byte[sizeof(long)];
            rg.NextBytes(bytes);
            // strip out the sign bit
            bytes[7] = (byte)(bytes[7] & 0x7f);
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        ///     A Random extension method that return a random value from the specified values.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing arguments.</param>
        /// <returns>One of the specified value.</returns>
        public static T OneOf<T>(this Random @this, params T[] values)
        {
            return values[@this.Next(values.Length)];
        }

        /// <summary>
        /// Returns an infinite sequence of random integers using the standard 
        /// .NET random number generator.
        /// </summary>
        /// <returns>An infinite sequence of random integers</returns>
        public static IEnumerable<int> Random()
        {
            return Random(new Random());
        }

        /// <summary>
        /// Returns an infinite sequence of random integers using the supplied
        /// random number generator.
        /// </summary>
        /// <param name="rand">Random generator used to produce random numbers</param>
        /// <returns>An infinite sequence of random integers</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="rand"/> is <see langword="null"/>.</exception>
        public static IEnumerable<int> Random(Random rand)
        {
            if (rand == null) throw new ArgumentNullException(nameof(rand));

            return RandomImpl(rand, r => r.Next());
        }

        /// <summary>
        /// Returns an infinite sequence of random integers between 0 and <paramref name="maxValue"/>/>.
        /// </summary>
        /// <param name="maxValue">exclusive upper bound for the random values returned</param>
        /// <returns>An infinite sequence of random integers</returns>
        public static IEnumerable<int> Random(int maxValue)
        {
            if (maxValue < 0) throw new ArgumentOutOfRangeException(nameof(maxValue));

            return Random(new Random(), maxValue);
        }

        /// <summary>
        /// Returns an infinite sequence of random integers between 0 and <paramref name="maxValue"/>/>
        /// using the supplied random number generator.
        /// </summary>
        /// <param name="rand">Random generator used to produce values</param>
        /// <param name="maxValue">Exclusive upper bound for random values returned</param>
        /// <returns>An infinite sequence of random integers</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="rand"/> is <see langword="null"/>.</exception>
        public static IEnumerable<int> Random(Random rand, int maxValue)
        {
            if (rand == null) throw new ArgumentNullException(nameof(rand));
            if (maxValue < 0) throw new ArgumentOutOfRangeException(nameof(maxValue));

            return RandomImpl(rand, r => r.Next(maxValue));
        }

        /// <summary>
        /// Returns an infinite sequence of random integers between <paramref name="minValue"/> and
        /// <paramref name="maxValue"/>.
        /// </summary>
        /// <param name="minValue">Inclusive lower bound of the values returned</param>
        /// <param name="maxValue">Exclusive upper bound of the values returned</param>
        /// <returns>An infinite sequence of random integers</returns>
        public static IEnumerable<int> Random(int minValue, int maxValue)
        {
            return Random(new Random(), minValue, maxValue);
        }

        /// <summary>
        /// Returns an infinite sequence of random integers between <paramref name="minValue"/> and
        /// <paramref name="maxValue"/> using the supplied random number generator.
        /// </summary>
        /// <param name="rand">Generator used to produce random numbers</param>
        /// <param name="minValue">Inclusive lower bound of the values returned</param>
        /// <param name="maxValue">Exclusive upper bound of the values returned</param>
        /// <returns>An infinite sequence of random integers</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="rand"/> is <see langword="null"/>.</exception>
        public static IEnumerable<int> Random(Random rand, int minValue, int maxValue)
        {
            if (rand == null) throw new ArgumentNullException(nameof(rand));
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue),
                    string.Format("The argument minValue ({0}) is greater than maxValue ({1})", minValue, maxValue));

            return RandomImpl(rand, r => r.Next(minValue, maxValue));
        }

        /// <summary>
        /// Returns an infinite sequence of random double values between 0.0 and 1.0
        /// </summary>
        /// <returns>An infinite sequence of random doubles</returns>
        public static IEnumerable<double> RandomDouble()
        {
            return RandomDouble(new Random());
        }

        /// <summary>
        /// Returns an infinite sequence of random double values between 0.0 and 1.0
        /// using the supplied random number generator.
        /// </summary>
        /// <param name="rand">Generator used to produce random numbers</param>
        /// <returns>An infinite sequence of random doubles</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="rand"/> is <see langword="null"/>.</exception>
        public static IEnumerable<double> RandomDouble(Random rand)
        {
            if (rand == null) throw new ArgumentNullException(nameof(rand));

            return RandomImpl(rand, r => r.NextDouble());
        }

        /// <summary>
        /// This is the underlying implementation that all random operators use to
        /// produce a sequence of random values.
        /// </summary>
        /// <typeparam name="T">The type of value returned (either Int32 or Double)</typeparam>
        /// <param name="rand">Random generators used to produce the sequence</param>
        /// <param name="nextValue">Generator function that actually produces the next value - specific to T</param>
        /// <returns>An infinite sequence of random numbers of type T</returns>
        private static IEnumerable<T> RandomImpl<T>(Random rand, Func<Random, T> nextValue)
        {
            while (true)
                yield return nextValue(rand);
        }
    }
}
