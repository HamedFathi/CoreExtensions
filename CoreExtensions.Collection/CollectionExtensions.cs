using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CoreExtensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds a range of values to an ICollection.
        /// </summary>
        /// <typeparam name="T">The element type of the ICollection.</typeparam>
        /// <param name="items">The <see cref="ICollection{T}"/> instance on which the extension method is called.</param>
        /// <param name="values">The items we are adding to the ICollection.</param>
        public static void AddRange<T>(this ICollection<T> items, params T[] values)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            foreach (var value in values)
            {
                items.Add(value);
            }
        }

        /// <summary>
        /// Adds a range of values to an ICollection.
        /// </summary>
        /// <typeparam name="T">The element type of the ICollection.</typeparam>
        /// <param name="items">The <see cref="ICollection{T}"/> instance on which the extension method is called.</param>
        /// <param name="values">The items we are adding to the ICollection.</param>
        public static void AddRange<T>(this ICollection<T> items, IEnumerable<T> values)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            foreach (var value in values)
            {
                items.Add(value);
            }
        }

        /// <summary>
        ///     Adds a range of value uniquely to a collection and returns the amount of values added.
        /// </summary>
        /// <typeparam name="T">The generic collection value type.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="values">The values to be added.</param>
        /// <returns>The amount if values that were added.</returns>
        public static int AddRangeUnique<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            var count = 0;
            foreach (var value in values)
            {
                if (collection.AddUnique(value))
                    count++;
            }
            return count;
        }

        /// <summary>
        ///     Adds a value uniquely to to a collection and returns a value whether the value was added or not.
        /// </summary>
        /// <typeparam name="T">The generic collection value type</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="value">The value to be added.</param>
        /// <returns>Indicates whether the value was added or not</returns>
        /// <example>
        ///     <code>
        /// 		list.AddUnique(1); // returns true;
        /// 		list.AddUnique(1); // returns false the second time;
        /// 	</code>
        /// </example>
        public static bool AddUnique<T>(this ICollection<T> collection, T value)
        {
            var alreadyHas = collection.Contains(value);
            if (!alreadyHas)
            {
                collection.Add(value);
            }
            return alreadyHas;
        }

        /// <summary>
        /// Splits the specified collection into chunks.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="chunks">The chunks.</param>
        /// <returns>Enumerable of Enumerables based on chunks split evenly as best as possible</returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this ICollection<T> collection, int chunks)
        {
            var count = collection.Count;
            int ceiling = (int)Math.Ceiling(count / (double)chunks);

            return collection.Select((x, i) => new { value = x, index = i })
                .GroupBy(x => x.index / ceiling)
                .Select(x => x.Select(z => z.value));
        }

        /// <summary>
        /// Determines whether <paramref name="collection"/> contains all of the members of the <paramref name="toFind"/> collection.
        /// </summary>
        public static bool ContainsAll<T>(this ICollection<T> collection, ICollection<T> toFind)
        {
            bool foundAll;

            if (toFind.Count == 0)
            {
                foundAll = false;
            }
            else
            {
                foundAll = true;

                foreach (T item in toFind)
                {
                    if (!collection.Contains(item))
                    {
                        foundAll = false;
                        break;
                    }
                }

            }
            return foundAll;
        }

        /// <summary>
        ///     An ICollection&lt;T&gt; extension method that query if '@this' contains all values.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool ContainsAll<T>(this ICollection<T> @this, params T[] values)
        {
            foreach (T value in values)
            {
                if (!@this.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ContainsAll<T>(this ICollection<T> @this, IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                if (!@this.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Indicates whether <paramref name="collection"/> contains any of the members in <paramref name="toFind"/>.
        /// </summary>
        public static bool ContainsAny<T>(this ICollection<T> collection, ICollection<T> toFind)
        {
            var found = false;

            foreach (var item in toFind)
            {
                if (collection.Contains(item))
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

        /// <summary>
        ///     An ICollection&lt;T&gt; extension method that query if '@this' contains any value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool ContainsAny<T>(this ICollection<T> @this, params T[] values)
        {
            foreach (T value in values)
            {
                if (@this.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsAny<T>(this ICollection<T> @this, IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                if (@this.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Tests if the collection is empty.
        /// </summary>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is empty.</returns>
        public static bool IsEmpty(this ICollection collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection), "The collection cannot be null.");

            return collection.Count == 0;
        }

        /// <summary>
        ///     Tests if the collection is empty.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the items in
        ///     the collection.
        /// </typeparam>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is empty.</returns>
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection), "The collection cannot be null.");

            return collection.Count == 0;
        }

        /// <summary>
        ///     An ICollection&lt;T&gt; extension method that query if the collection is not empty.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if not empty&lt; t&gt;, false if not.</returns>
        public static bool IsNotEmpty<T>(this ICollection<T> @this)
        {
            return @this.Count != 0;
        }

        /// <summary>
        ///     An ICollection&lt;T&gt; extension method that queries if the collection is not (null or is empty).
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if the collection is not (null or empty), false if not.</returns>
        public static bool IsNotNullOrEmpty<T>(this ICollection<T> @this)
        {
            return @this != null && @this.Count != 0;
        }

        /// <summary>
        ///     An ICollection&lt;T&gt; extension method that queries if the collection is null or is empty.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if null or empty&lt; t&gt;, false if not.</returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> @this)
        {
            return @this == null || @this.Count == 0;
        }

        public static void RemoveAll<T>(this ICollection<T> collection, Predicate<T> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            for (var index = collection.Count - 1; index >= 0; index--)
            {
                var currentItem = collection.ElementAt(index);
                if (predicate(currentItem))
                {
                    collection.Remove(currentItem);
                }
            }
        }

        /// <summary>
        ///     An ICollection&lt;T&gt; extension method that removes if contains.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="value">The value.</param>
        public static void RemoveIfContains<T>(this ICollection<T> @this, T value)
        {
            if (@this.Contains(value))
            {
                @this.Remove(value);
            }
        }

        /// <summary>
        ///     An ICollection&lt;T&gt; extension method that removes the range.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        public static void RemoveRange<T>(this ICollection<T> @this, params T[] values)
        {
            foreach (T value in values)
            {
                @this.Remove(value);
            }
        }

        public static void RemoveRange<T>(this ICollection<T> @this, IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                @this.Remove(value);
            }
        }

        /// <summary>
        ///     Remove an item from the collection with predicate
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>
        ///     Contributed by Michael T, http://about.me/MichaelTran
        ///     Renamed by James Curran, to match corresponding HashSet.RemoveWhere()
        /// </remarks>
        public static void RemoveWhere<T>(this ICollection<T> collection, Predicate<T> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            var deleteList = collection.Where(child => predicate(child)).ToList();
            deleteList.ForEach(t => collection.Remove(t));
        }

        /// <summary>
        ///     An ICollection&lt;T&gt; extension method that removes value that satisfy the predicate.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="predicate">The predicate.</param>
        public static void RemoveWhere<T>(this ICollection<T> @this, Func<T, bool> predicate)
        {
            List<T> list = @this.Where(predicate).ToList();
            foreach (T item in list)
            {
                @this.Remove(item);
            }
        }

        /// <summary>
        /// Creates an array of the items in <paramref name="collection"/>.
        /// </summary>
        public static T[] ToArray<T>(this ICollection collection)
        {
            T[] array = new T[collection.Count];
            int index = 0;

            foreach (T item in collection)
            {
                array[index++] = item;
            }

            return array;
        }

        /// <summary>
        /// Creates an array of type K, utilizing the <paramref name="converter"/> to convert from T.
        /// </summary>
        public static K[] ToArray<T, K>(this ICollection<T> collection, Converter<T, K> converter)
        {
            K[] array = new K[collection.Count];
            int index = 0;

            foreach (T item in collection)
            {
                array[index++] = converter(item);
            }

            return array;
        }
    }
}
