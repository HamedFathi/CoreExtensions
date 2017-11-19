using CoreUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreExtensions
{
    public static class ListExtensions
    {
        private static readonly Random Rnd = RandomUtility.GetUniqueRandom();

        /// <summary>
        ///     Adds the range specified to an IList(Of T) typed container.
        /// </summary>
        /// <typeparam name="T">type of the element in the list</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="rangeToAdd">The range to add.</param>
        public static void AddRange<T>(this IList<T> container, IEnumerable<T> rangeToAdd)
        {
            if ((container == null) || (rangeToAdd == null))
            {
                return;
            }
            foreach (var toAdd in rangeToAdd)
            {
                container.Add(toAdd);
            }
        }

        public static void AddRangeUnique<T>(this List<T> list, T[] items) where T : class
        {
            foreach (var item in items)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
        }

        public static void AddRangeUnique<T>(this List<T> list, IEnumerable<T> items) where T : class
        {
            foreach (var item in items)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
        }

        public static void AddUnique<T>(this List<T> list, T item) where T : class
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }

        public static bool AnyOrNotNull(this List<string> source)
        {
            var hasData = source.Aggregate((a, b) => a + b).Any();
            if (source != null && source.Any() && hasData)
                return true;
            else
                return false;
        }

        /// <summary>
        ///     Searches for the element specified in the sorted list specified using binary search
        ///     http://en.wikipedia.org/wiki/Binary_search. The algorithm
        ///     is re-implemented here to be able to search in any sorted IList implementing data structure (.NET's BCL only
        ///     implements BinarySearch on arrays and
        ///     List(Of T). If no IComparer(Of T) is available, try using Algorithmia's ComparisonBasedComparer,
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sortedList">The sorted list.</param>
        /// <param name="element">The element.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>
        ///     The index of the element searched or the bitwise complement of the index of the next element that is larger than
        ///     <i>element</i> or if there is no larger element the bitwise complement of Count. Bitwise complements have their
        ///     original bits negated. Use
        ///     the '~' operator in C# to get the real value. Bitwise complements are used to avoid returning a value which is in
        ///     the range of valid indices so
        ///     callers can't check whether the value returned is an index or if the element wasn't found. If the value returned is
        ///     negative, the bitwise complement
        ///     can be used as index to insert the element in the sorted list to keep the list sorted
        /// </returns>
        /// <remarks>
        ///     Assumes that sortedList is sorted ascending. If you pass in a descending sorted list, be sure the comparer is
        ///     adjusted as well.
        /// </remarks>
        public static int BinarySearch<T>(this IList sortedList, T element, IComparer<T> comparer)
        {
            if (sortedList == null)
                throw new ArgumentNullException(nameof(sortedList));

            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            if (sortedList.Count <= 0)
                return -1;

            var left = 0;
            var right = sortedList.Count - 1;
            while (left <= right)
            {
                // determine the index in the list to compare with. This is the middle of the segment we're searching in.
                var index = left + (right - left) / 2;
                var compareResult = comparer.Compare((T)sortedList[index], element);
                if (compareResult == 0)
                {
                    // found it, done. Return the index
                    return index;
                }
                if (compareResult < 0)
                {
                    // element is bigger than the element at index, so we can skip all elements at the left of index including the element at index.
                    left = index + 1;
                }
                else
                {
                    // element is smaller than the element at index, so we can skip all elements at the right of index including the element at index.
                    right = index - 1;
                }
            }
            return ~left;
        }

        /// <summary>
        /// 	Cast this list into a List
        /// </summary>
        /// <param name = "source"></param>
        /// <typeparam name = "T"></typeparam>
        /// <returns></returns>
        ///  <remarks>
        ///  	Contributed by Michael T, http://about.me/MichaelTran
        ///  </remarks>
        public static List<T> Cast<T>(this IList source)
        {
            var list = new List<T>();
            list.AddRange(source.OfType<T>());
            return list;
        }

        public static T GetRandomItem<T>(this List<T> list)
        {
            var count = list.Count;

            var i = Rnd.Next(0, count);
            return list[i];
        }

        public static IEnumerable<T> GetRandomItems<T>(this List<T> list, int count, bool uniqueItems = true)
        {
            var c = list.Count;
            var l = new List<T>();

            while (true)
            {
                var i = Rnd.Next(0, c);
                if (!l.Contains(list[i]) && uniqueItems)
                {
                    l.Add(list[i]);
                }
                if (!uniqueItems)
                {
                    l.Add(list[i]);
                }
                if (l.Count == count)
                    break;
            }
            return l;
        }

        public static bool HasDuplicates<T>(this IList<T> list)
        {
            var hs = new HashSet<T>();

            for (var i = 0; i < list.Count(); ++i)
            {
                if (!hs.Add(list[i])) return true;
            }
            return false;
        }

        /// <summary>
        /// 	Return the index of the first matching item or -1.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "list">The list.</param>
        /// <param name = "comparison">The comparison.</param>
        /// <returns>The item index</returns>
        public static int IndexOf<T>(this IList<T> list, Func<T, bool> comparison)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (comparison(list[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 	Inserts a range of items uniquely to a list starting at a given index and returns the amount of items inserted.
        /// </summary>
        /// <typeparam name = "T">The generic list item type.</typeparam>
        /// <param name = "list">The list to be inserted into.</param>
        /// <param name = "startIndex">The start index.</param>
        /// <param name = "items">The items to be inserted.</param>
        /// <returns>The amount if items that were inserted.</returns>
        public static int InsertRangeUnique<T>(this IList<T> list, int startIndex, IEnumerable<T> items)
        {
            var index = startIndex + items.Reverse().Count(item => list.InsertUnique(startIndex, item));
            return (index - startIndex);
        }

        /// <summary>
        /// 	Inserts an item uniquely to to a list and returns a value whether the item was inserted or not.
        /// </summary>
        /// <typeparam name = "T">The generic list item type.</typeparam>
        /// <param name = "list">The list to be inserted into.</param>
        /// <param name = "index">The index to insert the item at.</param>
        /// <param name = "item">The item to be added.</param>
        /// <returns>Indicates whether the item was inserted or not</returns>
        public static bool InsertUnique<T>(this IList<T> list, int index, T item)
        {
            if (list.Contains(item) == false)
            {
                list.Insert(index, item);
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Tests if the collection is empty.
        /// </summary>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is empty.</returns>
        public static bool IsEmpty(this IList list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list), "The list cannot be null.");

            return list.Count == 0;
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
        public static bool IsEmpty<T>(this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list), "The list cannot be null.");

            return list.Count == 0;
        }

        public static bool IsFirst<T>(this IList<T> list, T element)
        {
            return list.IndexOf(element) == 0;
        }

        public static bool IsLast<T>(this IList<T> list, T element)
        {
            return list.IndexOf(element) == list.Count - 1;
        }

        /// <summary>
        ///     Determines whether the passed in list is null or empty.
        /// </summary>
        /// <typeparam name="T">the type of the elements in the list to check</typeparam>
        /// <param name="toCheck">the list to check.</param>
        /// <returns>true if the passed in list is null or empty, false otherwise</returns>
        public static bool IsNullOrEmpty<T>(this IList<T> toCheck)
        {
            return (toCheck == null) || (toCheck.Count <= 0);
        }

        /// <summary>
        /// 	Join all the elements in the list and create a string seperated by the specified char.
        /// </summary>
        /// <param name = "list">
        /// 	The list.
        /// </param>
        /// <param name = "joinChar">
        /// 	The join char.
        /// </param>
        /// <typeparam name = "T">
        /// </typeparam>
        /// <returns>
        /// 	The resulting string of the elements in the list.
        /// </returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static string Join<T>(this IList<T> list, char joinChar)
        {
            return list.Join(joinChar.ToString());
        }

        /// <summary>
        /// 	Join all the elements in the list and create a string seperated by the specified string.
        /// </summary>
        /// <param name = "list">
        /// 	The list.
        /// </param>
        /// <param name = "joinString">
        /// 	The join string.
        /// </param>
        /// <typeparam name = "T">
        /// </typeparam>
        /// <returns>
        /// 	The resulting string of the elements in the list.
        /// </returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// 	Optimised by Mario Majcica
        /// </remarks>
        public static string Join<T>(this IList<T> list, string joinString)
        {
            if (list == null || !list.Any())
                return String.Empty;

            StringBuilder result = new StringBuilder();

            int listCount = list.Count;
            int listCountMinusOne = listCount - 1;

            if (listCount > 1)
            {
                for (var i = 0; i < listCount; i++)
                {
                    if (i != listCountMinusOne)
                    {
                        result.Append(list[i]);
                        result.Append(joinString);
                    }
                    else
                        result.Append(list[i]);
                }
            }
            else
                result.Append(list[0]);

            return result.ToString();
        }

        public static IList<K> Map<T, K>(this IList<T> list, Func<T, K> function)
        {
            var newList = new List<K>(list.Count);
            foreach (T t in list)
            {
                newList.Add(function(t));
            }
            return newList;
        }

        /// <summary>
        /// 	Using Relugar Expression, find the top matches for each item in the source specified by the arguments to search.
        /// </summary>
        /// <param name = "list">
        /// 	The source.
        /// </param>
        /// <param name = "searchString">
        /// 	The search string.
        /// </param>
        /// <param name = "top">
        /// 	The top.
        /// </param>
        /// <param name = "args">
        /// 	The args.
        /// </param>
        /// <typeparam name = "T">
        /// </typeparam>
        /// <returns>
        /// 	A List of top matches.
        /// </returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static List<T> Match<T>(this IList<T> list, string searchString, int top, params Expression<Func<T, object>>[] args)
        {
            // Create a new list of results and matches;
            var results = new List<T>();
            var matches = new Dictionary<T, int>();
            var maxMatch = 0;
            list.ForEach(s =>
            {
                // Generate the expression string from the argument.
                var regExp = string.Empty;
                if (args != null)
                {
                    foreach (var arg in args)
                    {
                        var property = arg.Compile();
                        // Attach the new property to the expression string
                        regExp += (string.IsNullOrEmpty(regExp) ? "(?:" : "|(?:") + property(s) + ")+?";
                    }
                }
                // Get the matches
                var match = Regex.Matches(searchString, regExp, RegexOptions.IgnoreCase);
                // If there are more than one match
                if (match.Count > 0)
                {
                    // Add it to the match dictionary, including the match count.
                    matches.Add(s, match.Count);
                }
                // Get the highest max matching
                maxMatch = match.Count > maxMatch ? match.Count : maxMatch;
            });
            // Convert the match dictionary into a list
            var matchList = matches.ToList();

            // Sort the list by decending match counts
            // matchList.Sort((s1, s2) => s2.Value.CompareTo(s1.Value));

            // Remove all matches that is less than the best match.
            matchList.RemoveAll(s => s.Value < maxMatch);

            // If the top value is set and is less than the number of matches
            var getTop = top > 0 && top < matchList.Count ? top : matchList.Count;

            // Add the maches into the result list.
            for (var i = 0; i < getTop; i++)
                results.Add(matchList[i].Key);

            return results;
        }

        /// <summary>The merge.</summary>
        /// <param name="lists">The lists.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static List<T> Merge<T>(params List<T>[] lists)
        {
            var merged = new List<T>();
            foreach (var list in lists) merged.Merge(list);
            return merged;
        }

        /// <summary>The merge.</summary>
        /// <param name="match">The match.</param>
        /// <param name="lists">The lists.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static List<T> Merge<T>(Expression<Func<T, object>> match, params List<T>[] lists)
        {
            var merged = new List<T>();
            foreach (var list in lists) merged.Merge(list, match);
            return merged;
        }

        /// <summary>The merge.</summary>
        /// <param name="list1">The list 1.</param>
        /// <param name="list2">The list 2.</param>
        /// <param name="match">The match.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static List<T> Merge<T>(this List<T> list1, List<T> list2, Expression<Func<T, object>> match)
        {
            if (list1 != null && list2 != null && match != null)
            {
                var matchFunc = match.Compile();
                foreach (var item in list2)
                {
                    var key = matchFunc(item);
                    if (!list1.Exists(i => matchFunc(i).Equals(key))) list1.Add(item);
                }
            }

            return list1;
        }

        /// <summary>The merge.</summary>
        /// <param name="list1">The list 1.</param>
        /// <param name="list2">The list 2.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static List<T> Merge<T>(this List<T> list1, List<T> list2)
        {
            if (list1 != null && list2 != null) foreach (var item in list2.Where(item => !list1.Contains(item))) list1.Add(item);
            return list1;
        }

        public static T Next<T>(this IList<T> list, ref Int32 Index)
        {
            Index = ++Index >= 0 && Index < list.Count ? Index : 0;
            return list[Index];
        }

        public static T Previous<T>(this IList<T> list, ref Int32 Index)
        {
            Index = --Index >= 0 && Index < list.Count ? Index : list.Count - 1;
            return list[Index];
        }

        /// <summary>
        /// Removes the last item in a list.
        /// </summary>
        /// <remarks>No action is performed if list is empty.</remarks>
        public static void RemoveLast<T>(this IList<T> list)
        {
            if (list.Count > 0)
            {
                list.RemoveAt(list.Count - 1);
            }
        }

        public static bool Replace<T>(this IList<T> thisList, int position, T item)
        {
            if (position > thisList.Count - 1)
                return false;
            thisList.RemoveAt(position);
            thisList.Insert(position, item);
            return true;
        }

        public static int Replace<T>(this IList<T> source, T oldValue, T newValue)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var index = source.IndexOf(oldValue);
            if (index != -1)
                source[index] = newValue;
            return index;
        }

        public static IEnumerable<T> Replace<T>(this IEnumerable<T> source, T oldValue, T newValue)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Select(x => EqualityComparer<T>.Default.Equals(x, oldValue) ? newValue : x);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Splits the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="Count">The count.</param>
        /// <returns></returns>
        public static IList<IList<T>> Split<T>(this IList<T> list, Int32 Count)
        {
            var currentList = new List<T>(Count);
            var returnList = new List<IList<T>>();
            for (int i = 0, j = list.Count; i < j; i++)
            {
                if (currentList.Count < Count)
                {
                    currentList.Add(list[i]);
                }
                else
                {
                    returnList.Add(currentList);
                    currentList = new List<T>(Count);
                    currentList.Add(list[i]);
                }
            }
            returnList.Add(currentList);
            return returnList;
        }

        public static IList<T> Swap<T>(this IList<T> list, Int32 IndexA, Int32 IndexB)
        {
            T Temp = list[IndexA];
            list[IndexA] = list[IndexB];
            list[IndexB] = Temp;
            return list;
        }

        public static IList<T> SwapLeft<T>(this IList<T> list, Int32 Index)
        {
            return list.Swap(Index, Index - 1);
        }

        public static IList<T> SwapRight<T>(this IList<T> list, Int32 Index)
        {
            return list.Swap(Index, Index + 1);
        }

        /// <summary>
        ///     Swaps the values at indexA and indexB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="indexA">The index for A.</param>
        /// <param name="indexB">The index for B.</param>
        public static void SwapValues<T>(this IList<T> source, int indexA, int indexB)
        {
            if ((indexA < 0) || (indexA >= source.Count))
            {
                throw new IndexOutOfRangeException("indexA is out of range");
            }
            if ((indexB < 0) || (indexB >= source.Count))
            {
                throw new IndexOutOfRangeException("indexB is out of range");
            }

            if (indexA == indexB)
            {
                return;
            }

            var tempValue = source[indexA];
            source[indexA] = source[indexB];
            source[indexB] = tempValue;
        }
    }
}
