using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreExtensions
{
    public static class TArrayExtensions
    {
        /// <summary>
        /// Returns a block of items from an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <remarks>Contributed by Chris Gessler</remarks>
        public static T[] BlockCopy<T>(this T[] array, int index, int length)
        {
            return BlockCopy(array, index, length, false);
        }

        /// <summary>
        /// Returns a block of items from an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="padToLength"></param>
        /// <returns></returns>
        /// <remarks>
        /// Test results prove that Array.Copy is many times faster than Skip/Take and LINQ
        /// Item count: 1,000,000
        /// Array.Copy:     15 ms
        /// Skip/Take:  42,464 ms - 42.5 seconds
        /// LINQ:          881 ms
        /// Contributed by Chris Gessler</remarks>
        public static T[] BlockCopy<T>(this T[] array, int index, int length, bool padToLength)
        {
            if (array == null) throw new NullReferenceException();

            int n = length;
            T[] b = null;

            if (array.Length < index + length)
            {
                n = array.Length - index;
                if (padToLength)
                {
                    b = new T[length];
                }
            }

            if (b == null) b = new T[n];
            Array.Copy(array, index, b, 0, n);
            return b;
        }

        /// <summary>
        /// Allows enumeration over an Array in blocks
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="count"></param>
        /// <param name="padToLength"></param>
        /// <returns></returns>
        /// <remarks>Contributed by Chris Gessler</remarks>
        public static IEnumerable<T[]> BlockCopy<T>(this T[] array, int count, bool padToLength = false)
        {
            for (int i = 0; i < array.Length; i += count)
                yield return array.BlockCopy(i, count, padToLength);
        }

        /// <summary>
        /// To clear the contents of the array.
        /// </summary>
        /// <typeparam name="T">The type of array</typeparam>
        /// <param name="clear"> The array to clear</param>
        /// <returns>Cleared array</returns>
        /// <example>
        ///     <code>
        ///         int[] result = new[] { 1, 2, 3, 4 }.ClearAll<int>();
        ///     </code>
        /// </example>
        /// <remarks>
        /// 	Contributed by Mohammad Rahman, http://mohammad-rahman.blogspot.com/
        /// </remarks>
        public static T[] ClearAll<T>(this T[] arrayToClear)
        {
            if (arrayToClear != null)
                for (int i = arrayToClear.GetLowerBound(0); i <= arrayToClear.GetUpperBound(0); ++i)
                    arrayToClear[i] = default(T);
            return arrayToClear;
        }

        /// <summary>
        ///     A T[] extension method that clears at.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The arrayToClear to act on.</param>
        /// <param name="at">at.</param>
        public static void ClearAt<T>(this T[] @this, int at)
        {
            Array.Clear(@this, at, 1);
        }

        /// <summary>
        /// Combine two arrays into one.
        /// </summary>
        /// <typeparam name="T">Type of Array</typeparam>
        /// <param name="combineWith">Base array in which arrayToCombine will add.</param>
        /// <param name="arrayToCombine">Array to combine with Base array.</param>
        /// <returns></returns>
        /// <example>
        /// 	<code>
        /// 		int[] arrayOne = new[] { 1, 2, 3, 4 };
        /// 		int[] arrayTwo = new[] { 5, 6, 7, 8 };
        /// 		Array combinedArray = arrayOne.CombineArray<int>(arrayTwo);
        /// 	</code>
        /// </example>
        /// <remarks>
        /// 	Contributed by Mohammad Rahman, http://mohammad-rahman.blogspot.com/
        /// </remarks>
        public static T[] CombineArray<T>(this T[] combineWith, T[] arrayToCombine)
        {
            if (combineWith != default(T[]) && arrayToCombine != default(T[]))
            {
                int initialSize = combineWith.Length;
                Array.Resize<T>(ref combineWith, initialSize + arrayToCombine.Length);
                Array.Copy(arrayToCombine, arrayToCombine.GetLowerBound(0), combineWith, initialSize, arrayToCombine.Length);
            }
            return combineWith;
        }

        /// <summary>
        ///     A T[] extension method that exists.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static Boolean Exists<T>(this T[] array, Predicate<T> match)
        {
            return Array.Exists(array, match);
        }

        /// <summary>
        ///     A T[] extension method that searches for the first match.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>A T.</returns>
        public static T Find<T>(this T[] array, Predicate<T> match)
        {
            return Array.Find(array, match);
        }

        /// <summary>
        ///     A T[] extension method that searches for the first all.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found all.</returns>
        public static T[] FindAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindAll(array, match);
        }

        /// <summary>
        ///     A T[] extension method that searches for the first index.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindIndex(array, match);
        }

        /// <summary>
        ///     A T[] extension method that searches for the first index.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindIndex<T>(this T[] array, Int32 startIndex, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, match);
        }

        /// <summary>
        ///     A T[] extension method that searches for the first index.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">Number of.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindIndex<T>(this T[] array, Int32 startIndex, Int32 count, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, count, match);
        }

        /// <summary>
        ///     A T[] extension method that searches for the first last.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found last.</returns>
        public static T FindLast<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLast(array, match);
        }

        /// <summary>
        ///     A T[] extension method that searches for the last index.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindLastIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLastIndex(array, match);
        }

        /// <summary>
        ///     A T[] extension method that searches for the last index.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindLastIndex<T>(this T[] array, Int32 startIndex, Predicate<T> match)
        {
            return Array.FindLastIndex(array, startIndex, match);
        }

        /// <summary>
        ///     A T[] extension method that searches for the last index.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">Number of.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindLastIndex<T>(this T[] array, Int32 startIndex, Int32 count, Predicate<T> match)
        {
            return Array.FindLastIndex(array, startIndex, count, match);
        }

        public static void ForEach<TSource>(this TSource[] source, Action<TSource> action)
        {
            foreach (var item in source)
                action(item);
        }

        public static void ForEach<TSource>(this TSource[] source, Action<TSource> action, Func<TSource, bool> predicate)
        {
            foreach (var item in source.Where(predicate))
                action(item);
        }

        public static T[] Remove<T>(this T[] array, Func<T, bool> condition)
        {
            var list = new List<T>();

            foreach (var item in array)
            {
                if (!condition(item))
                {
                    list.Add(item);
                }
            }

            return list.ToArray();
        }

        public static TSource[] Remove<TSource>(this TSource[] source, TSource item)
        {
            var result = new TSource[source.Length - source.Count(s => s.Equals(item))];
            var x = 0;
            foreach (var i in source.Where(i => !Equals(i, item)))
            {
                result[x] = i;
                x++;
            }
            return result;
        }

        public static TSource[] RemoveAll<TSource>(this TSource[] source, Predicate<TSource> predicate)
        {
            var result = new TSource[source.Length - source.Count(s => predicate(s))];
            var i = 0;
            foreach (var item in source.Where(item => !predicate(item)))
            {
                result[i] = item;
                i++;
            }
            return result;
        }

        public static TSource[] RemoveAt<TSource>(this TSource[] source, int index)
        {
            var result = new TSource[source.Length - 1];
            var x = 0;
            for (var i = 0; i < source.Length; i++)
            {
                if (i == index) continue;
                result[x] = source[i];
                x++;
            }
            return result;
        }

        /// <summary>
        ///     A T[] extension method that true for all.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static Boolean TrueForAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.TrueForAll(array, match);
        }
    }
}
