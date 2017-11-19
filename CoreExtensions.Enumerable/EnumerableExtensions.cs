using CoreUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CoreExtensions
{
    public static class EnumerableExtensions
    {
        private static readonly Random Rnd = RandomUtility.GetUniqueRandom();

        public static string Aggregate(this IEnumerable<string> enumeration, string separator)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));

            // Check to see that separator is not null
            if (separator == null)
                throw new ArgumentNullException(nameof(separator));

            string returnValue = String.Join(separator, enumeration.ToArray());

            if (returnValue.Length > separator.Length)
                return returnValue.Substring(separator.Length);

            return returnValue;
        }

        public static string Aggregate<T>(this IEnumerable<T> enumeration, Func<T, string> toString, string separator)
        {
            // Check to see that toString is not null
            if (toString == null)
                throw new ArgumentNullException(nameof(toString));

            return Aggregate(enumeration.Select(toString), separator);
        }

        public static bool AnyOrNotNull(this IEnumerable<string> source)
        {
            var hasData = source.Aggregate((a, b) => a + b).Any();
            if (source != null && source.Any() && hasData)
                return true;
            return false;
        }

        /// <summary>
        ///     Appends an element to the end of the current collection and returns the new collection.
        /// </summary>
        /// <typeparam name="T">The enumerable data type</typeparam>
        /// <param name="source">The data values.</param>
        /// <param name="item">The element to append the current collection with.</param>
        /// <returns>
        ///     The modified collection.
        /// </returns>
        /// <example>
        ///     var integers = Enumerable.Range(0, 3);  // 0, 1, 2
        ///     integers = integers.Append(3);          // 0, 1, 2, 3
        /// </example>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T item)
        {
            foreach (var i in source)
                yield return i;

            yield return item;
        }

        public static bool AreDistinct<T>(this IEnumerable<T> items)
        {
            return items.Count() == items.Distinct().Count();
        }

        public static IEnumerable<T?> AsNullable<T>(this IEnumerable<T> enumeration)
                                                                    where T : struct
        {
            return from item in enumeration
                   select new T?(item);
        }

        /// <summary>
        /// Transforms the given enumeration into a <see cref="ReadOnlyCollection&lt;T&gt;" />.
        /// </summary>
        /// <typeparam name="T">The type of the members in <paramref name="this"/>.</typeparam>
        /// <param name="this">The enumeration to transform.</param>
        /// <returns>A <see cref="ReadOnlyCollection&lt;T&gt;" /> object.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is <c>null</c>.</exception>
        public static ReadOnlyCollection<T> AsReadOnlyCollection<T>(this IEnumerable<T> @this)
        {
            if (@this != null)
                return new ReadOnlyCollection<T>(new List<T>(@this));
            throw new ArgumentNullException(nameof(@this));
        }

        public static T At<T>(this IEnumerable<T> enumeration, int index)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException("enumeration");

            return enumeration.Skip(index).First();
        }

        public static IEnumerable<T> At<T>(this IEnumerable<T> enumeration, params int[] indices)
        {
            return At(enumeration, (IEnumerable<int>)indices);
        }

        public static IEnumerable<T> At<T>(this IEnumerable<T> enumeration, IEnumerable<int> indices)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException("enumeration");

            // Check to see that indices is not null
            if (indices == null)
                throw new ArgumentNullException("indices");

            int currentIndex = 0;

            foreach (int index in indices.OrderBy(i => i))
            {
                while (currentIndex != index)
                {
                    enumeration = enumeration.Skip(1);
                    currentIndex++;
                }

                yield return enumeration.First();
            }
        }

        /// <summary>
        /// Splits the specified enumerable into chunks.
        ///  
        /// This method has slightly more of a performance hit because it Counts the elements in the enumerable
        /// then splits it so it is going through the enumerable twice
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="chunks">The chunks.</param>
        /// <returns>Enumerable of Enumerables based on chunks split evenly as best as possible</returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> enumerable, int chunks)
        {
            var count = enumerable.Count();
            int ceiling = (int)Math.Ceiling(count / (double)chunks);

            return enumerable.Select((x, i) => new { value = x, index = i })
                .GroupBy(x => x.index / ceiling)
                .Select(x => x.Select(z => z.value));
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source, int select, bool repetition = false)
        {
            if (source == null || @select < 0)
                throw new ArgumentNullException();

            return @select == 0
                ? new[] { new T[0] }
                : source.SelectMany((element, index) =>
                    source
                        .Skip(repetition ? index : index + 1)
                        .Combinations(@select - 1, repetition)
                        .Select(c => new[] { element }.Concat(c)));
        }

        /// <summary>An IEnumerable&lt;string&gt; extension method that concatenates the given this.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string Concatenate(this IEnumerable<string> @this)
        {
            var sb = new StringBuilder();

            foreach (var s in @this)
            {
                sb.Append(s);
            }

            return sb.ToString();
        }

        /// <summary>An IEnumerable&lt;T&gt; extension method that concatenates.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="source">The source to act on.</param>
        /// <param name="func">The function.</param>
        /// <returns>A string.</returns>
        public static string Concatenate<T>(this IEnumerable<T> source, Func<T, string> func)
        {
            var sb = new StringBuilder();
            foreach (var item in source)
            {
                sb.Append(func(item));
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Concatenate a list of items using the provided separator.
        /// </summary>
        /// <param name="items">An enumerable collection of items to concatenate.</param>
        /// <param name="separator">The separator to use for the concatenation (defaults to ",").</param>
        /// <param name="formatString">An optional string formatter for the items in the output list.</param>
        /// <returns>The enumerable collection of items concatenated into a single string.</returns>
        /// <example>
        ///     <code>
        /// 		List&lt;double&gt; doubles = new List&lt;double&gt;() { 123.4567, 123.4, 123.0, 4, 5 };
        /// 		string concatenated = doubles.ConcatWith(":", "0.00");  // concatenated = 123.46:123.40:123.00:4.00:5.00
        /// 	</code>
        /// </example>
        /// <remarks>
        ///     Contributed by Joseph Eddy, http://www.codeplex.com/site/users/view/jceddy
        /// </remarks>
        public static string ConcatWith<T>(this IEnumerable<T> items, string separator = ",", string formatString = "")
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (separator == null) throw new ArgumentNullException(nameof(separator));

            // shortcut for string enumerable
            if (typeof(T) == typeof(string))
            {
                return String.Join(separator, ((IEnumerable<string>)items).ToArray());
            }

            if (String.IsNullOrEmpty(formatString))
            {
                formatString = "{0}";
            }
            else
            {
                formatString = String.Format("{{0:{0}}}", formatString);
            }

            return String.Join(separator, items.Select(x => String.Format(formatString, x)).ToArray());
        }

        public static bool Contains<T>(this IEnumerable<T> source, Func<T, bool> selector)
        {
            foreach (T item in source)
            {
                if (selector(item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     An IEnumerable&lt;T&gt; extension method that query if '@this' contains all.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool ContainsAll<T>(this IEnumerable<T> @this, params T[] values)
        {
            T[] list = @this.ToArray();
            foreach (T value in values)
            {
                if (!list.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     An IEnumerable&lt;T&gt; extension method that query if '@this' contains any.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool ContainsAny<T>(this IEnumerable<T> @this, params T[] values)
        {
            T[] list = @this.ToArray();
            foreach (T value in values)
            {
                if (list.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks whether an enumeration contains at least a certain number of items.
        /// </summary>
        public static bool ContainsAtLeast<T>(this IEnumerable<T> enumeration, int count)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));

            return (from t in enumeration.Take(count)
                    select t)
                   .Count() <= count;
        }

        /// <summary>
        /// Checks whether an enumeration contains at most a certain number of items.
        /// </summary>
        public static bool ContainsAtMost<T>(this IEnumerable<T> enumeration, int count)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));

            return (from t in enumeration.Take(count)
                    select t)
                   .Count() >= count;
        }

        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> source)
        {
            while (true)
            {
                foreach (var item in source)
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        ///     An IEnumerable&lt;DirectoryInfo&gt; extension method that deletes the given @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void Delete(this IEnumerable<DirectoryInfo> @this)
        {
            foreach (DirectoryInfo t in @this)
            {
                t.Delete();
            }
        }

        /// <summary>
        ///     An IEnumerable&lt;FileInfo&gt; extension method that deletes the given @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void Delete(this IEnumerable<FileInfo> @this)
        {
            foreach (FileInfo t in @this)
            {
                t.Delete();
            }
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, T, bool> equalityComparer)
        {
            int sourceCount = source.Count();
            for (int i = 0; i < sourceCount; i++)
            {
                bool found = false;
                for (int j = 0; j < i; j++)
                    if (equalityComparer(source.ElementAt(i), source.ElementAt(j))) // In some cases, it's better to convert source in List<T> (before first for)
                    {
                        found = true;
                        break;
                    }
                if (!found)
                    yield return source.ElementAt(i);
            }
        }

        public static IEnumerable<TKey> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            return source.GroupBy(selector).Select(x => x.Key);
        }

        public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> list, Func<T, object> propertySelector)
        {

            return list.GroupBy(propertySelector).Select(x => x.First());

        }

        private static IEnumerable<T> ElementsNotNullFrom<T>(IEnumerable<T> source)
        {
            return source.Where(x => x != null);
        }

        /// <summary>
        /// Returns and empty <see cref="IEnumerable{T}"/> collection if <paramref name="items"/> is null.
        /// </summary>
        /// <typeparam name="T">The item type of the items enumeration.</typeparam>
        /// <param name="items">The <see cref="IEnumerable{T}"/> instance on which the extension method is called.</param>
        /// <returns>The <paramref name="items"/> collection or an empty <see cref="IEnumerable{T}"/> collection.</returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> items)
        {
            return items ?? Enumerable.Empty<T>();
        }

        /// <summary>
        ///     Allows you to create a enumerable string list of the items name in the Enum.
        /// </summary>
        /// <typeparam name="T">Enum Type to enumerate</typeparam>
        /// <returns></returns>
        public static IEnumerable<string> EnumNamesToList<T>(this IEnumerable<T> collection)
        {
            var cls = typeof(T);

            var enumArrayList = cls.GetInterfaces();

            return (from objType in enumArrayList where objType.IsEnum select objType.Name).ToList();
        }

        /// <summary>
        ///     Allows you to create Enumerable List of the Enum's Values.
        /// </summary>
        /// <typeparam name="T">Enum Type to enumerate</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> EnumValuesToList<T>(this IEnumerable<T> collection)
        {
            var enumType = typeof(T);

            // Can't use generic type constraints on value types,
            // so have to do check like this
            // consider using - enumType.IsEnum()
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            var enumValArray = Enum.GetValues(enumType);
            var enumValList = new List<T>(enumValArray.Length);
            enumValList.AddRange(from int val in enumValArray select (T)Enum.Parse(enumType, val.ToString()));

            return enumValList;
        }

        /// <summary>
        /// Determines whether or not the item exists
        /// </summary>
        public static bool Exists<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            return list.Index(predicate) > -1;
        }

        /// <summary>
        /// Retuns a list of all items matching the predicate
        /// </summary>
        public static List<T> FindAll<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            var found = new List<T>();
            var enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    found.Add(enumerator.Current);
                }
            }
            return found;
        }

        public static T FirstOr<T>(this IEnumerable<T> @this,
                                                                    Func<T, bool> predicate,
                                                                    Func<T> onOr)
        {
            T found = @this.FirstOrDefault(predicate);

            if (found.Equals(default(T)))
            {
                found = onOr();
            }

            return found;
        }

        /// <summary>
        ///     Returns the first item or the <paramref name="defaultValue" /> if the <paramref name="source" />
        ///     does not contain any item.
        /// </summary>
        public static T FirstOrDefault<T>(this IEnumerable<T> source, T defaultValue)
        {
            return source.IsNotEmpty() ? source.First() : defaultValue;
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<int, T> action)
        {
            if (items != null)
            {
                int i = 0;

                foreach (T item in items)
                {
                    action(i++, item);
                }
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }

        /// <summary>
        ///     Enumerates for each in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="action">The action.</param>
        /// <returns>An enumerator that allows foreach to be used to process for each in this collection.</returns>
        public static void ForEach(this IEnumerable<DirectoryInfo> @this, Action<DirectoryInfo> action)
        {
            foreach (DirectoryInfo t in @this)
            {
                action(t);
            }
        }

        /// <summary>Enumerates for each in this collection.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T, int> action)
        {
            var array = @this.ToArray();

            for (var i = 0; i < array.Length; i++)
            {
                action(array[i], i);
            }
        }

        public static void ForEachReverse<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source.Reverse())
                action(item);
        }

        public static void ForEachReverse<T>(this IEnumerable<T> @this, Action<T, int> action)
        {
            var array = @this.ToArray();

            for (var i = array.Length - 1; i >= 0; i--)
            {
                action(array[i], i);
            }
        }

        public static IEnumerable<T> GetDuplicateItems<T>(this IEnumerable<T> list)
        {
            var duplicateKeys = list.GroupBy(x => x)
                        .Where(group => @group.Count() > 1)
                        .Select(group => @group.Key);
            return duplicateKeys;
        }

        public static IEnumerable<T[]> GroupEvery<T>(this IEnumerable<T> enumeration, int count)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException("enumeration");

            if (count <= 0)
                throw new ArgumentOutOfRangeException("count");

            int current = 0;
            T[] array = new T[count];

            foreach (var item in enumeration)
            {
                array[current++] = item;

                if (current == count)
                {
                    yield return array;
                    current = 0;
                    array = new T[count];
                }
            }

            if (current != 0)
            {
                yield return array;
            }
        }

        /// <summary>
        ///     Returns whether the sequence contains a certain amount of elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the input sequence.</typeparam>
        /// <param name="source">The source for this extension method.</param>
        /// <param name="count">The amount of elements the sequence should contain.</param>
        /// <returns>True when the sequence contains the specified amount of elements, false otherwise.</returns>
        public static bool HasCountOf<T>(this IEnumerable<T> source, int count)
        {
            return source.Take(count + 1).Count() == count;
        }

        /// <summary>
        ///     Returns enumerable object based on target, which does not contains null references.
        ///     If target is null reference, returns empty enumerable object.
        /// </summary>
        /// <typeparam name="T">Type of items in target.</typeparam>
        /// <param name="target">Target enumerable object. Can be null.</param>
        /// <example>
        ///     object[] items = null;
        ///     foreach(var item in items.NotNull()){
        ///     // result of items.NotNull() is empty but not null enumerable
        ///     }
        ///     object[] items = new object[]{ null, "Hello World!", null, "Good bye!" };
        ///     foreach(var item in items.NotNull()){
        ///     // result of items.NotNull() is enumerable with two strings
        ///     }
        /// </example>
        /// <remarks>
        ///     Contributed by tencokacistromy, http://www.codeplex.com/site/users/view/tencokacistromy
        /// </remarks>
        public static IEnumerable<T> IgnoreNulls<T>(this IEnumerable<T> target)
        {
            if (ReferenceEquals(target, null))
                yield break;

            foreach (var item in target.Where(item => !ReferenceEquals(item, null)))
                yield return item;
        }

        /// <summary>
        /// Finds the index of an item
        /// </summary>
        public static int Index<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            var enumerator = list.GetEnumerator();
            for (int i = 0; enumerator.MoveNext(); ++i)
            {
                if (predicate(enumerator.Current))
                {
                    return i;
                }
            }
            return -1;
        }

        public static int IndexOf<T>(this IEnumerable<T> items, T item, IEqualityComparer<T> comparer)
        {
            return IndexOf(items, item, comparer.Equals);
        }

        public static int IndexOf<T>(this IEnumerable<T> items, T item, Func<T, T, bool> predicate)
        {
            int index = 0;

            foreach (T instance in items)
            {
                if (predicate(item, instance))
                {
                    return index;
                }

                ++index;
            }

            return -1;
        }

        /// <summary>
        /// Gets the indices where the predicate is true.
        /// </summary>
        public static IEnumerable<int> IndicesWhere<T>(this IEnumerable<T> enumeration, Func<T, bool> predicate)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));

            // Check to see that predicate is not null
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            int i = 0;

            foreach (T item in enumeration)
            {
                if (predicate(item))
                    yield return i;

                i++;
            }
        }

        /// <summary>
        ///     An IEnumerable&lt;T&gt; extension method that query if 'collection' is empty.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The collection to act on.</param>
        /// <returns>true if empty, false if not.</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> @this)
        {
            return !@this.Any();
        }

        /// <summary>
        ///     An IEnumerable&lt;T&gt; extension method that queries if a not is empty.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The collection to act on.</param>
        /// <returns>true if a not is t>, false if not.</returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> @this)
        {
            return @this.Any();
        }

        /// <summary>
        ///     An IEnumerable&lt;T&gt; extension method that queries if a not null or is empty.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The collection to act on.</param>
        /// <returns>true if a not null or is t>, false if not.</returns>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> @this)
        {
            return @this != null && @this.Any();
        }

        public static bool IsNullOrEmpty(this IEnumerable sequence)
        {
            return (sequence == null) || !sequence.Cast<object>().Any();
        }

        /// <summary>
        ///     An IEnumerable&lt;T&gt; extension method that queries if a null or is empty.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The collection to act on.</param>
        /// <returns>true if a null or is t>, false if not.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> @this)
        {
            return @this == null || !@this.Any();
        }

        public static bool IsSingle<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (var enumerator = source.GetEnumerator())
                return enumerator.MoveNext() && !enumerator.MoveNext();
        }

        public static string Join<T>(this IEnumerable<T> collection, Func<T, string> func, string separator)
        {
            return String.Join(separator, collection.Select(func).ToArray());
        }

        public static bool Many<T>(this IEnumerable<T> source)
        {
            return source.Count() > 1;
        }

        public static bool Many<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Count(query) > 1;
        }

        /// <summary>
        ///     Returns the maximum item based on a provided selector.
        /// </summary>
        /// <typeparam name="TItem">The item type</typeparam>
        /// <typeparam name="TValue">The value item</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="maxValue">The max value as output parameter.</param>
        /// <returns>The maximum item</returns>
        /// <example>
        ///     <code>
        /// 		int age;
        /// 		var oldestPerson = persons.MaxItem(p =&gt; p.Age, out age);
        /// 	</code>
        /// </example>
        public static TItem MaxItem<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> selector,
                                                                    out TValue maxValue)
                                                                    where TItem : class
                                                                    where TValue : IComparable
        {
            TItem maxItem = null;
            maxValue = default(TValue);

            foreach (var item in items)
            {
                if (item == null)
                    continue;

                var itemValue = selector(item);

                if ((maxItem != null) && (itemValue.CompareTo(maxValue) <= 0))
                    continue;

                maxValue = itemValue;
                maxItem = item;
            }

            return maxItem;
        }

        /// <summary>
        ///     Returns the maximum item based on a provided selector.
        /// </summary>
        /// <typeparam name="TItem">The item type</typeparam>
        /// <typeparam name="TValue">The value item</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The maximum item</returns>
        /// <example>
        ///     <code>
        /// 		var oldestPerson = persons.MaxItem(p =&gt; p.Age);
        /// 	</code>
        /// </example>
        public static TItem MaxItem<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> selector)
                                                                    where TItem : class
                                                                    where TValue : IComparable
        {
            TValue maxValue;

            return items.MaxItem(selector, out maxValue);
        }

        /// <summary>Enumerates merge distinct inner enumerable in this collection.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process merge distinct inner
        ///     enumerable in this collection.
        /// </returns>
        public static IEnumerable<T> MergeDistinctInnerEnumerable<T>(this IEnumerable<IEnumerable<T>> @this)
        {
            List<IEnumerable<T>> listItem = @this.ToList();

            var list = new List<T>();

            foreach (var item in listItem)
            {
                list = list.Union(item).ToList();
            }

            return list;
        }

        /// <summary>Enumerates merge inner enumerable in this collection.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process merge inner enumerable in
        ///     this collection.
        /// </returns>
        public static IEnumerable<T> MergeInnerEnumerable<T>(this IEnumerable<IEnumerable<T>> @this)
        {
            List<IEnumerable<T>> listItem = @this.ToList();

            var list = new List<T>();

            foreach (var item in listItem)
            {
                list.AddRange(item);
            }

            return list;
        }

        /// <summary>
        ///     Returns the minimum item based on a provided selector.
        /// </summary>
        /// <typeparam name="TItem">The item type</typeparam>
        /// <typeparam name="TValue">The value item</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="minValue">The min value as output parameter.</param>
        /// <returns>The minimum item</returns>
        /// <example>
        ///     <code>
        /// 		int age;
        /// 		var youngestPerson = persons.MinItem(p =&gt; p.Age, out age);
        /// 	</code>
        /// </example>
        public static TItem MinItem<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> selector,
                                                                    out TValue minValue)
                                                                    where TItem : class
                                                                    where TValue : IComparable
        {
            TItem minItem = null;
            minValue = default(TValue);

            foreach (var item in items)
            {
                if (item == null)
                    continue;
                var itemValue = selector(item);

                if ((minItem != null) && (itemValue.CompareTo(minValue) >= 0))
                    continue;
                minValue = itemValue;
                minItem = item;
            }

            return minItem;
        }

        /// <summary>
        ///     Returns the minimum item based on a provided selector.
        /// </summary>
        /// <typeparam name="TItem">The item type</typeparam>
        /// <typeparam name="TValue">The value item</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum item</returns>
        /// <example>
        ///     <code>
        /// 		var youngestPerson = persons.MinItem(p =&gt; p.Age);
        /// 	</code>
        /// </example>
        public static TItem MinItem<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> selector)
                                                                    where TItem : class
                                                                    where TValue : IComparable
        {
            TValue minValue;

            return items.MinItem(selector, out minValue);
        }

        public static bool None<T>(this IEnumerable<T> source)
        {
            return source.Any() == false;
        }

        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Any(query) == false;
        }

        public static bool OneOf<T>(this IEnumerable<T> source)
        {
            return source.Count() == 1;
        }

        public static bool OneOf<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Count(query) == 1;
        }

        public static bool OnlyOne<T>(this IEnumerable<T> source, Func<T, bool> condition = null)
        {
            return source.Count(condition ?? (t => true)) == 1;
        }

        /// <summary>
        ///     An IEnumerable&lt;string&gt; extension method that combine all value to return a path.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The path.</returns>
        public static string PathCombine(this IEnumerable<string> @this)
        {
            return Path.Combine(@this.ToArray());
        }

        /// <summary>
        /// Returns a sequence of a specified size of random elements from the original sequence
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence</typeparam>
        /// <param name="sequence">The sequence from which to return random elements</param>
        /// <param name="subsetSize">The size of the random subset to return</param>
        /// <returns>A random sequence of elements in random order from the original sequence</returns>
        public static IEnumerable<T> RandomSubset<T>(this IEnumerable<T> sequence, int subsetSize)
        {
            return RandomSubset(sequence, subsetSize, new Random());
        }

        /// <summary>
        /// Returns a sequence of a specified size of random elements from the original sequence
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence</typeparam>
        /// <param name="sequence">The sequence from which to return random elements</param>
        /// <param name="subsetSize">The size of the random subset to return</param>
        /// <param name="rand">A random generator used as part of the selection algorithm</param>
        /// <returns>A random sequence of elements in random order from the original sequence</returns>
        public static IEnumerable<T> RandomSubset<T>(this IEnumerable<T> sequence, int subsetSize, Random rand)
        {
            if (rand == null) throw new ArgumentNullException(nameof(rand));
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (subsetSize < 0) throw new ArgumentOutOfRangeException(nameof(subsetSize));

            return RandomSubsetImpl(sequence, subsetSize, rand);
        }

        private static IEnumerable<T> RandomSubsetImpl<T>(IEnumerable<T> sequence, int subsetSize, Random rand)
        {
            // The simplest and most efficient way to return a random subet is to perform 
            // an in-place, partial Fisher-Yates shuffle of the sequence. While we could do 
            // a full shuffle, it would be wasteful in the cases where subsetSize is shorter
            // than the length of the sequence.
            // See: http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle

            var seqArray = sequence.ToArray();
            if (seqArray.Length < subsetSize)
                throw new ArgumentOutOfRangeException(nameof(subsetSize), "Subset size must be <= sequence.Count()");

            var m = 0;                // keeps track of count items shuffled
            var w = seqArray.Length;  // upper bound of shrinking swap range
            var g = w - 1;            // used to compute the second swap index

            // perform in-place, partial Fisher-Yates shuffle
            while (m < subsetSize)
            {
                var k = g - rand.Next(w);
                var tmp = seqArray[k];
                seqArray[k] = seqArray[m];
                seqArray[m] = tmp;
                ++m;
                --w;
            }

            // yield the random subet as a new sequence
            for (var i = 0; i < subsetSize; i++)
                yield return seqArray[i];
        }

        /// <summary>
        /// Creates list of non-null and empty values from the object passed to <paramref name="strings"/>.
        /// </summary>
        public static IEnumerable<string> RemoveEmptyElements(this IEnumerable<string> strings)
        {
            foreach (var s in strings)
            {
                if (!String.IsNullOrEmpty(s))
                {
                    yield return s;
                }
            }
        }

        /// <summary>
        ///     Removes matching items from a sequence
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        /// <remarks>
        ///     Renamed by James Curran, to match corresponding HashSet.RemoveWhere()
        /// </remarks>
        public static IEnumerable<T> RemoveWhere<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            if (source == null)
                yield break;

            foreach (var t in source)
                if (!predicate(t))
                    yield return t;
        }


        public static IEnumerable<T> ReplaceWhere<T>(this IEnumerable<T> enumerable, Predicate<T> predicate, Func<T> replacement)
        {

            if (enumerable == null)
            {
                yield break;
            }

            foreach (var item in enumerable)
            {
                if (predicate(item))
                {
                    yield return replacement();
                }
                else
                {
                    yield return item;
                }
            }

        }

        public static IEnumerable<T> ReplaceWhere<T>(this IEnumerable<T> enumerable, Predicate<T> predicate, T replacement)
        {


            if (enumerable == null)
            {
                yield break;
            }

            foreach (var item in enumerable)
            {
                if (predicate(item))
                {
                    yield return replacement;
                }
                else
                {
                    yield return item;
                }
            }

        }

        public static IEnumerable<string> FuzzyContains(this IEnumerable<string> list, string text)
        {
            var FuzzyService = new FuzzySearchService(list);
            var Results = FuzzyService.Search(text).OrderByDescending(r => r.Score).Select(x => x.TheResult);
            return Results;
        }

        /// <summary>
        ///     Overload the Select to allow null as a return
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="allowNull"></param>
        /// <returns>
        ///     An <see cref="IEnumerable{TResult}" /> using the selector containing null or non-null results based on
        ///     <see cref="allowNull" />.
        /// </returns>
        /// <example>
        ///     <code>
        /// var list = new List{object}{ new object(), null, null };
        /// var noNulls = list.Select(x => x, false);
        /// </code>
        /// </example>
        /// <remarks>
        ///     Contributed by thinktech_coder
        /// </remarks>
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source,
                                                                    Func<TSource, TResult> selector, bool allowNull = true)
        {
            foreach (var item in source)
            {
                var select = selector(item);
                if (allowNull || !Equals(@select, default(TSource)))
                    yield return @select;
            }
        }

        public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> source)
        {
            // Check to see that source is not null
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            foreach (var enumeration in source)
            {
                foreach (var item in enumeration)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> SelectMany<T>(this IEnumerable<T[]> source)
        {
            // Check to see that source is not null
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            foreach (var enumeration in source)
            {
                foreach (var item in enumeration)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> SelectManyAllInclusive<T>
                                                                       (this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            return source.Concat(source.SelectManyRecursive(selector));
        }

        public static IEnumerable<T> SelectManyRecursive<T>
                                                                (this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            var result = source.SelectMany(selector).ToList();
            if (result.Count == 0)
            {
                return result;
            }
            return result.Concat(result.SelectManyRecursive(selector));
        }

        public static bool SequenceEqual<T1, T2>(this IEnumerable<T1> left, IEnumerable<T2> right, Func<T1, T2, bool> comparer)
        {
            using (IEnumerator<T1> leftE = left.GetEnumerator())
            {
                using (IEnumerator<T2> rightE = right.GetEnumerator())
                {
                    bool leftNext = leftE.MoveNext(), rightNext = rightE.MoveNext();

                    while (leftNext && rightNext)
                    {
                        // If one of the items isn't the same...
                        if (!comparer(leftE.Current, rightE.Current))
                            return false;

                        leftNext = leftE.MoveNext();
                        rightNext = rightE.MoveNext();
                    }

                    // If left or right is longer
                    if (leftNext || rightNext)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether the enumeration is an ordered superset of a subset enumeration using the type's default comparer.
        /// </summary>
        /// <remarks>See http://weblogs.asp.net/okloeten/archive/2008/04/22/6121373.aspx for more details.</remarks>
        public static bool SequenceSuperset<T>(this IEnumerable<T> enumeration, IEnumerable<T> subset)
        {
            return SequenceSuperset(enumeration, subset, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Checks whether the enumeration is an ordered superset of a subset enumeration.
        /// </summary>
        /// <remarks>See http://weblogs.asp.net/okloeten/archive/2008/04/22/6121373.aspx for more details.</remarks>
        public static bool SequenceSuperset<T>(this IEnumerable<T> enumeration,
                                                                                                            IEnumerable<T> subset,
                                                                                                            Func<T, T, bool> equalityComparer)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException("enumeration");

            // Check to see that subset is not null
            if (subset == null)
                throw new ArgumentNullException("subset");

            // Check to see that comparer is not null
            if (equalityComparer == null)
                throw new ArgumentNullException("comparer");

            using (IEnumerator<T> big = enumeration.GetEnumerator(), small = subset.GetEnumerator())
            {
                big.Reset(); small.Reset();

                while (big.MoveNext())
                {
                    // End of subset, which means we've gone through it all and it's all equal.
                    if (!small.MoveNext())
                        return true;

                    if (!equalityComparer(big.Current, small.Current))
                    {
                        // Comparison failed. Let's try comparing with the first item.
                        small.Reset();

                        // There's more than one item in the small enumeration. Guess why I know this.
                        small.MoveNext();

                        // No go with the first item? Reset the collection and brace for the next iteration of the big loop.
                        if (!equalityComparer(big.Current, small.Current))
                            small.Reset();
                    }
                }

                // End of both, which means that the small is the end of the big.
                if (!small.MoveNext())
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Checks whether the enumerable to compare with is equal to the source enumerable, element wise. If elements are in a
        ///     different order, the
        ///     method will still return true. This is different from SequenceEqual which does take order into account
        /// </summary>
        /// <typeparam name="T">type of the element in the sequences to compare</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="toCompareWith">the sequence to compare with.</param>
        /// <returns>true if the source and the sequence to compare with have the same elements, regardless of ordering</returns>
        public static bool SetEqual<T>(this IEnumerable<T> source, IEnumerable<T> toCompareWith)
        {
            if ((source == null) || (toCompareWith == null))
            {
                return false;
            }
            return source.SetEqual(toCompareWith, null);
        }

        /// <summary>
        ///     Checks whether the enumerable to compare with is equal to the source enumerable, element wise. If elements are in a
        ///     different order, the
        ///     method will still return true. This is different from SequenceEqual which does take order into account
        /// </summary>
        /// <typeparam name="T">type of the element in the sequences to compare</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="toCompareWith">the sequence to compare with.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>
        ///     true if the source and the sequence to compare with have the same elements, regardless of ordering
        /// </returns>
        public static bool SetEqual<T>(this IEnumerable<T> source, IEnumerable<T> toCompareWith,
                                                                    IEqualityComparer<T> comparer)
        {
            if ((source == null) || (toCompareWith == null))
            {
                return false;
            }
            var countSource = source.Count();
            var countToCompareWith = toCompareWith.Count();
            if (countSource != countToCompareWith)
            {
                return false;
            }
            if (countSource == 0)
            {
                return true;
            }

            var comparerToUse = comparer ?? EqualityComparer<T>.Default;
            // check whether the intersection of both sequences has the same number of elements
            return source.Intersect(toCompareWith, comparerToUse).Count() == countSource;
        }

        /// <summary>
        /// Shuffles the elements of a <see cref="IEnumerable{T}"/> collection using a random generator.
        /// </summary>
        /// <typeparam name="T">The item type of the items enumeration.</typeparam>
        /// <param name="items">The <see cref="IEnumerable{T}"/> instance on which the extension method is called.</param>
        /// <returns>A new collection with the snuffled elements from <paramref name="items"/>.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            return items.ShuffleIterator();
        }

        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> items)
        {
            var buffer = items.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = Rnd.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

        public static IEnumerable<T> Slice<T>(this IEnumerable<T> items, int start, int end)
        {
            int index = 0;
            int count = 0;
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (items is ICollection<T>)
                count = ((ICollection<T>)items).Count;
            else if (items is ICollection)
                count = ((ICollection)items).Count;
            else
                count = items.Count();
            if (start < 0)
                start += count;
            if (end < 0)
                end += count;
            foreach (var item in items)
            {
                if (index >= end)
                    yield break;
                if (index >= start)
                    yield return item;
                ++index;
            }
        }

        /// <summary>
        ///     Concatenates all the elements of a IEnumerable, using the specified separator between each element.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">An IEnumerable that contains the elements to concatenate.</param>
        /// <param name="separator">
        ///     The string to use as a separator. separator is included in the returned string only if
        ///     value has more than one element.
        /// </param>
        /// <returns>
        ///     A string that consists of the elements in value delimited by the separator string. If value is an empty array,
        ///     the method returns String.Empty.
        /// </returns>
        public static string StringJoin<T>(this IEnumerable<T> @this, string separator)
        {
            return String.Join(separator, @this);
        }

        /// <summary>
        ///     Concatenates all the elements of a IEnumerable, using the specified separator between
        ///     each element.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="separator">
        ///     The string to use as a separator. separator is included in the
        ///     returned string only if value has more than one element.
        /// </param>
        /// <returns>
        ///     A string that consists of the elements in value delimited by the separator string. If
        ///     value is an empty array, the method returns String.Empty.
        /// </returns>
        public static string StringJoin<T>(this IEnumerable<T> @this, char separator)
        {
            return String.Join(separator.ToString(), @this);
        }

        /// <summary>
        ///     Computes the sum of a sequence of UInt32 values.
        /// </summary>
        /// <param name="source">A sequence of UInt32 values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        public static uint Sum(this IEnumerable<uint> source)
        {
            return source.Aggregate(0U, (current, number) => current + number);
        }

        /// <summary>
        ///     Computes the sum of a sequence of UInt64 values.
        /// </summary>
        /// <param name="source">A sequence of UInt64 values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        public static ulong Sum(this IEnumerable<ulong> source)
        {
            return source.Aggregate(0UL, (current, number) => current + number);
        }

        /// <summary>
        ///     Computes the sum of a sequence of nullable UInt32 values.
        /// </summary>
        /// <param name="source">A sequence of nullable UInt32 values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        public static uint? Sum(this IEnumerable<uint?> source)
        {
            return source.Where(nullable => nullable.HasValue)
                .Aggregate(0U, (current, nullable) => current + nullable.GetValueOrDefault());
        }

        /// <summary>
        ///     Computes the sum of a sequence of nullable UInt64 values.
        /// </summary>
        /// <param name="source">A sequence of nullable UInt64 values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        public static ulong? Sum(this IEnumerable<ulong?> source)
        {
            return source.Where(nullable => nullable.HasValue)
                .Aggregate(0UL, (current, nullable) => current + nullable.GetValueOrDefault());
        }

        /// <summary>
        ///     Computes the sum of a sequence of UInt32 values that are obtained by invoking a transformation function on each
        ///     element of the intput sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selection">A transformation function to apply to each element.</param>
        /// <returns>The sum of the projected values.</returns>
        public static uint Sum<T>(this IEnumerable<T> source, Func<T, uint> selection)
        {
            return ElementsNotNullFrom(source).Select(selection).Sum();
        }

        /// <summary>
        ///     Computes the sum of a sequence of nullable UInt32 values that are obtained by invoking a transformation function on
        ///     each element of the intput sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selection">A transformation function to apply to each element.</param>
        /// <returns>The sum of the projected values.</returns>
        public static uint? Sum<T>(this IEnumerable<T> source, Func<T, uint?> selection)
        {
            return ElementsNotNullFrom(source).Select(selection).Sum();
        }

        /// <summary>
        ///     Computes the sum of a sequence of UInt64 values that are obtained by invoking a transformation function on each
        ///     element of the intput sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transformation function to apply to each element.</param>
        /// <returns>The sum of the projected values.</returns>
        public static ulong Sum<T>(this IEnumerable<T> source, Func<T, ulong> selector)
        {
            return ElementsNotNullFrom(source).Select(selector).Sum();
        }

        /// <summary>
        ///     Computes the sum of a sequence of nullable UInt64 values that are obtained by invoking a transformation function on
        ///     each element of the intput sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transformation function to apply to each element.</param>
        /// <returns>The sum of the projected values.</returns>
        public static ulong? Sum<T>(this IEnumerable<T> source, Func<T, ulong?> selector)
        {
            return ElementsNotNullFrom(source).Select(selector).Sum();
        }

        /// <summary>
        /// Take items from 'startAt' every at 'hopLength' items.
        /// </summary>
        public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> enumeration, int startAt, int hopLength)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));

            int first = 0;
            int count = 0;

            foreach (T item in enumeration)
            {
                if (first < startAt)
                {
                    first++;
                }
                else if (first == startAt)
                {
                    yield return item;

                    first++;
                }
                else
                {
                    count++;

                    if (count == hopLength)
                    {
                        yield return item;

                        count = 0;
                    }
                }
            }
        }

        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> collection, Predicate<T> endCondition)
        {
            return collection.TakeWhile(item => !endCondition(item));
        }

        /// <summary>
        ///     Creates an Array from an IEnumerable&lt;T&gt; using the specified transform function.
        /// </summary>
        /// <typeparam name="TSource">The source data type</typeparam>
        /// <typeparam name="TResult">The target data type</typeparam>
        /// <param name="source">The source data.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>An Array of the target data type</returns>
        /// <example>
        ///     var integers = Enumerable.Range(1, 3);
        ///     var intStrings = values.ToArray(i => i.ToString());
        /// </example>
        /// <remarks>
        ///     This method is a shorthand for the frequently use pattern IEnumerable&lt;T&gt;.Select(Func).ToArray()
        /// </remarks>
        public static TResult[] ToArray<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select(selector).ToArray();
        }

        public static Collection<T> ToCollection<T>(this IEnumerable<T> enumerable)
        {
            var collection = new Collection<T>();
            foreach (var i in enumerable)
                collection.Add(i);
            return collection;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> varlist)
        {
            var dtReturn = new DataTable();
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                            colType = colType.GetGenericArguments()[0];
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        /// <summary>
        /// Recreates a dictionary from an enumeration of key-value pairs.
        /// </summary>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> enumeration)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));

            return enumeration.ToDictionary(item => item.Key, item => item.Value);
        }

        public static Dictionary<TKey, IEnumerable<TElement>> ToDictionary<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> enumeration)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));

            return enumeration.ToDictionary(item => item.Key, item => item.Cast<TElement>());
        }

        /// <summary>
        ///     Creates a new hashset and adds the source to it.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>Hashset with all the unique values in source</returns>
        public static HashSet<TDestination> ToHashSet<TDestination>(this IEnumerable<TDestination> source)
        {
            return new HashSet<TDestination>(source);
        }

        /// <summary>
        ///     Creates a List&lt;T&gt; from an IEnumerable&lt;T&gt; using the specified transform function.
        /// </summary>
        /// <typeparam name="TSource">The source data type</typeparam>
        /// <typeparam name="TResult">The target data type</typeparam>
        /// <param name="source">The source data.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>An IEnumerable&lt;T&gt; of the target data type</returns>
        /// <example>
        ///     var integers = Enumerable.Range(1, 3);
        ///     var intStrings = values.ToList(i => i.ToString());
        /// </example>
        /// <remarks>
        ///     This method is a shorthand for the frequently use pattern IEnumerable&lt;T&gt;.Select(Func).ToList()
        /// </remarks>
        public static List<TResult> ToList<TSource, TResult>(this IEnumerable<TSource> source,
                                                                    Func<TSource, TResult> selector)
        {
            return source.Select(selector).ToList();
        }

        public static string ToString(this IEnumerable<string> strs)
        {
            var text = strs.ToStringBuilder().ToString();
            return text;
        }

        public static StringBuilder ToStringBuilder(this IEnumerable<string> strs)
        {
            var sb = new StringBuilder();
            foreach (var str in strs)
            {
                sb.AppendLine(str);
            }
            return sb;
        }

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source)
                                                                    where T : class
        {
            return source.Where(x => x != null);
        }

        public static bool XOf<T>(this IEnumerable<T> source, int count)
        {
            return source.Count() == count;
        }

        public static bool XOf<T>(this IEnumerable<T> source, Func<T, bool> query, int count)
        {
            return source.Count(query) == count;
        }
    }
}
