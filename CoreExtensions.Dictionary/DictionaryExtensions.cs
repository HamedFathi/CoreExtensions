using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;

namespace CoreExtensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Add a range of values to a dictionary object. The item will only be added if it is not already in the collection.
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <typeparam name="TU">type</typeparam>
        /// <param name="source">the dictionary</param>
        /// <param name="dictionary">the range to be added</param>
        public static void AddDistinctRange<T, TU>(this IDictionary<T, TU> source, IDictionary<T, TU> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                if (!source.ContainsKey(kvp.Key))
                {
                    source.Add(kvp);
                }
            }
        }

        /// <summary>
        ///     An IDictionary&lt;TKey,TValue&gt; extension method that adds if not contains key.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool AddIfNotContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, value);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     An IDictionary&lt;TKey,TValue&gt; extension method that adds if not contains key.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="key">The key.</param>
        /// <param name="valueFactory">The value factory.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool AddIfNotContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TValue> valueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, valueFactory());
                return true;
            }

            return false;
        }

        /// <summary>
        ///     An IDictionary&lt;TKey,TValue&gt; extension method that adds if not contains key.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="key">The key.</param>
        /// <param name="valueFactory">The value factory.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool AddIfNotContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TKey, TValue> valueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, valueFactory(key));
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Uses the specified functions to add a key/value pair to the IDictionary&lt;TKey, TValue&gt; if the key does
        ///     not already exist, or to update a key/value pair in the IDictionary&lt;TKey, TValue&gt;> if the key already
        ///     exists.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="key">The key to be added or whose value should be updated.</param>
        /// <param name="value">The value to be added or updated.</param>
        /// <returns>The new value for the key.</returns>
        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(new KeyValuePair<TKey, TValue>(key, value));
            }
            else
            {
                @this[key] = value;
            }

            return @this[key];
        }

        /// <summary>
        ///     Uses the specified functions to add a key/value pair to the IDictionary&lt;TKey, TValue&gt; if the key does
        ///     not already exist, or to update a key/value pair in the IDictionary&lt;TKey, TValue&gt;> if the key already
        ///     exists.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="key">The key to be added or whose value should be updated.</param>
        /// <param name="addValue">The value to be added for an absent key.</param>
        /// <param name="updateValueFactory">
        ///     The function used to generate a new value for an existing key based on the key's
        ///     existing value.
        /// </param>
        /// <returns>
        ///     The new value for the key. This will be either be addValue (if the key was absent) or the result of
        ///     updateValueFactory (if the key was present).
        /// </returns>
        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(new KeyValuePair<TKey, TValue>(key, addValue));
            }
            else
            {
                @this[key] = updateValueFactory(key, @this[key]);
            }

            return @this[key];
        }

        /// <summary>
        ///     Uses the specified functions to add a key/value pair to the IDictionary&lt;TKey, TValue&gt; if the key does
        ///     not already exist, or to update a key/value pair in the IDictionary&lt;TKey, TValue&gt;> if the key already
        ///     exists.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="key">The key to be added or whose value should be updated.</param>
        /// <param name="addValueFactory">The function used to generate a value for an absent key.</param>
        /// <param name="updateValueFactory">
        ///     The function used to generate a new value for an existing key based on the key's
        ///     existing value.
        /// </param>
        /// <returns>
        ///     The new value for the key. This will be either be the result of addValueFactory (if the key was absent) or
        ///     the result of updateValueFactory (if the key was present).
        /// </returns>
        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(new KeyValuePair<TKey, TValue>(key, addValueFactory(key)));
            }
            else
            {
                @this[key] = updateValueFactory(key, @this[key]);
            }

            return @this[key];
        }

        /// <summary>
        /// Add a range of items to a dictionary.
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <typeparam name="TU">type</typeparam>
        /// <param name="source">the dictionary</param>
        /// <param name="dictionary">the range to be added</param>
        public static void AddRange<T, TU>(this IDictionary<T, TU> source, IDictionary<T, TU> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                if (source.ContainsKey(kvp.Key))
                {
                    throw new ArgumentException("An item with the same key has already been added.");
                }
                source.Add(kvp);
            }
        }

        /// <summary>
        ///     Adds the range specified to the dictionary specified, using the key producer func to produce the key values.
        ///     If the key already exists, the key's value is overwritten with the value to add.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="keyProducerFunc">The key producer func.</param>
        /// <param name="rangeToAdd">The range to add.</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> container,
                    Func<TValue, TKey> keyProducerFunc, IEnumerable<TValue> rangeToAdd)
        {
            if ((container == null) || (rangeToAdd == null))
                return;

            if (keyProducerFunc == null)
                throw new ArgumentNullException(nameof(keyProducerFunc));

            foreach (var toAdd in rangeToAdd)
            {
                container[keyProducerFunc(toAdd)] = toAdd;
            }
        }

        /// <summary>
        ///     An IDictionary&lt;TKey,TValue&gt; extension method that query if '@this' contains all key.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="keys">A variable-length parameters list containing keys.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool ContainsAllKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, params TKey[] keys)
        {
            foreach (TKey value in keys)
            {
                if (!@this.ContainsKey(value))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     An IDictionary&lt;TKey,TValue&gt; extension method that query if '@this' contains any key.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="keys">A variable-length parameters list containing keys.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool ContainsAnyKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, params TKey[] keys)
        {
            foreach (TKey value in keys)
            {
                if (@this.ContainsKey(value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the value of the first entry found with one of the <paramref name="keys"/> received.
        /// <para>Returns <paramref name="defaultValue"/> if none of the keys exists in this collection </para>
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="defaultValue">Default value if none of the keys </param>
        /// <param name="keys"> keys to search for (in order) </param>
        public static TValue GetFirstValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue defaultValue, params TKey[] keys)
        {
            foreach (var key in keys)
            {
                if (dictionary.ContainsKey(key))
                    return dictionary[key];
            }
            return defaultValue;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> items, TKey key, Func<TValue> factory)
        {
            TValue value;

            if (!items.TryGetValue(key, out value))
            {
                value = factory();
                items.Add(key, value);
            }

            return value;
        }

        /// <summary>
        ///     Adds a key/value pair to the IDictionary&lt;TKey, TValue&gt; if the key does not already exist.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value to be added, if the key does not already exist.</param>
        /// <returns>
        ///     The value for the key. This will be either the existing value for the key if the key is already in the
        ///     dictionary, or the new value if the key was not in the dictionary.
        /// </returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(new KeyValuePair<TKey, TValue>(key, value));
            }

            return @this[key];
        }

        /// <summary>
        ///     Adds a key/value pair to the IDictionary&lt;TKey, TValue&gt; by using the specified function, if the key does
        ///     not already exist.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="valueFactory">TThe function used to generate a value for the key.</param>
        /// <returns>
        ///     The value for the key. This will be either the existing value for the key if the key is already in the
        ///     dictionary, or the new value for the key as returned by valueFactory if the key was not in the dictionary.
        /// </returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TKey, TValue> valueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(new KeyValuePair<TKey, TValue>(key, valueFactory(key)));
            }

            return @this[key];
        }

        /// <summary>
        /// Returns the value associated with the specified key, or a default value if no element is found.
        /// </summary>
        /// <typeparam name="TKey">The key data type</typeparam>
        /// <typeparam name="TValue">The value data type</typeparam>
        /// <param name="source">The source dictionary.</param>
        /// <param name="key">The key of interest.</param>
        /// <returns>The value associated with the specified key if the key is found, the default value for the value data type if the key is not found</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            return source.GetOrDefault(key, default(TValue));
        }

        /// <summary>
        /// Returns the value associated with the specified key, or the specified default value if no element is found.
        /// </summary>
        /// <typeparam name="TKey">The key data type</typeparam>
        /// <typeparam name="TValue">The value data type</typeparam>
        /// <param name="source">The source dictionary.</param>
        /// <param name="key">The key of interest.</param>
        /// <param name="defaultValue">The default value to return if the key is not found.</param>
        /// <returns>The value associated with the specified key if the key is found, the specified default value if the key is not found</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue defaultValue)
        {
            TValue value;
            return source.TryGetValue(key, out value) ? value : defaultValue;
        }

        /// <summary>
        /// Returns the value associated with the specified key, or throw the specified exception if no element is found.
        /// </summary>
        /// <typeparam name="TKey">The key data type</typeparam>
        /// <typeparam name="TValue">The value data type</typeparam>
        /// <param name="source">The source dictionary.</param>
        /// <param name="key">The key of interest.</param>
        /// <param name="exception">The exception to throw if the key is not found.</param>
        /// <returns>The value associated with the specified key if the key is found, the specified exception is thrown if the key is not found</returns>
        public static TValue GetOrThrow<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, Exception exception)
        {
            TValue value;
            if (source.TryGetValue(key, out value))
            {
                return value;
            }

            throw exception;
        }

        /// <summary>
        ///     Gets the value for the key from the dictionary specified, or null / default(TValue) if key not found or the key is
        ///     null.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        ///     the value for the key from the dictionary specified, or null / default(TValue) if key not found or the key is
        ///     null.
        /// </returns>
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if ((key == null) || !dictionary.TryGetValue(key, out var toReturn))
            {
                toReturn = default(TValue);
            }
            return toReturn;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value = default(TValue);

            if (dictionary != null)
            {
                dictionary.TryGetValue(key, out value);
            }

            return value;
        }

        /// <summary>
        /// Inverts the specified dictionary. (Creates a new dictionary with the values as key, and key as values)
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns></returns>
        public static IDictionary<TValue, TKey> Invert<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));
            return dictionary.ToDictionary(pair => pair.Value, pair => pair.Key);
        }

        /// <summary>
        /// Tests if the collection is empty.
        /// </summary>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is empty.</returns>
        public static bool IsEmpty(this IDictionary collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection", "The collection cannot be null.");
            return collection.Count == 0;
        }

        /// <summary>
        /// Tests if the IDictionary is empty.
        /// </summary>
        /// <typeparam name="TKey">The type of the key of 
        /// the IDictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values
        /// of the IDictionary.</typeparam>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is empty.</returns>
        public static bool IsEmpty<TKey, TValue>(this IDictionary<TKey, TValue> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection", "The collection cannot be null.");
            return collection.Count == 0;
        }

        /// <summary>
        ///     An IDictionary&lt;TKey,TValue&gt; extension method that removes if contains key.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="key">The key.</param>
        public static void RemoveIfContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key)
        {
            if (@this.ContainsKey(key))
            {
                @this.Remove(key);
            }
        }

        public static void RemoveRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            // Check to see that dictionary is not null
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            // Check to see that keys is not null
            if (keys == null)
                throw new ArgumentNullException("keys");

            foreach (var key in keys.ToArray())
            {
                dictionary.Remove(key);
            }
        }

        public static void RemoveValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            // Check to see that dictionary is not null
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            foreach (var key in (from pair in dictionary
                                 where System.Collections.Generic.EqualityComparer<TValue>.Default.Equals(value, pair.Value)
                                 select pair.Key).ToArray())
            {
                dictionary.Remove(key);
            }
        }

        public static void RemoveValueRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TValue> values)
        {
            // Check to see that dictionary is not null
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            // Check to see that values is not null
            if (values == null)
                throw new ArgumentNullException("values");

            foreach (var value in values.ToArray())
            {
                RemoveValue(dictionary, value);
            }
        }

        /// <summary>
        /// Sorts the specified dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            return new SortedDictionary<TKey, TValue>(dictionary);
        }

        /// <summary>
        /// Sorts the specified dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary to be sorted.</param>
        /// <param name="comparer">The comparer used to sort dictionary.</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return new SortedDictionary<TKey, TValue>(dictionary, comparer);
        }

        /// <summary>
        /// Sorts the dictionary by value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> SortByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return (new SortedDictionary<TKey, TValue>(dictionary)).OrderBy(kvp => kvp.Value).ToDictionary(item => item.Key, item => item.Value);
        }

        /// <summary>
        ///     An IDictionary&lt;string,object&gt; extension method that converts this object to a database parameters.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="command">The command.</param>
        /// <returns>The given data converted to a DbParameter[].</returns>
        public static DbParameter[] ToDbParameters(this IDictionary<string, object> @this, DbCommand command)
        {
            return @this.Select(x =>
            {
                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = x.Key;
                parameter.Value = x.Value;
                return parameter;
            }).ToArray();
        }

        /// <summary>
        ///     An IDictionary&lt;string,object&gt; extension method that converts this object to a database parameters.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>The given data converted to a DbParameter[].</returns>
        public static DbParameter[] ToDbParameters(this IDictionary<string, object> @this, DbConnection connection)
        {
            DbCommand command = connection.CreateCommand();

            return @this.Select(x =>
            {
                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = x.Key;
                parameter.Value = x.Value;
                return parameter;
            }).ToArray();
        }

        public static dynamic ToDynamicObject(this IDictionary<string, object> source)
        {
            ICollection<KeyValuePair<string, object>> someObject = new ExpandoObject();
            someObject.AddRange(source);
            return someObject;
        }

        /// <summary>
        ///     An IDictionary&lt;string,object&gt; extension method that converts the @this to an expando.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ExpandoObject.</returns>
        public static ExpandoObject ToExpando(this IDictionary<string, object> @this)
        {
            var expando = new ExpandoObject();
            var expandoDict = (IDictionary<string, object>)expando;

            foreach (var item in @this)
            {
                if (item.Value is IDictionary<string, object>)
                {
                    var d = (IDictionary<string, object>)item.Value;
                    expandoDict.Add(item.Key, d.ToExpando());
                }
                else
                {
                    expandoDict.Add(item);
                }
            }

            return expando;
        }

        /// <summary>
        ///     An IDictionary extension method that converts the @this to a hashtable.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a Hashtable.</returns>
        public static Hashtable ToHashtable(this IDictionary @this)
        {
            return new Hashtable(@this);
        }

        /// <summary>
        /// Creates a (non-generic) Hashtable from the Dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns></returns>
        public static Hashtable ToHashTable<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            var table = new Hashtable();

            foreach (var item in dictionary)
                table.Add(item.Key, item.Value);

            return table;
        }

        /// <summary>
        ///     An IDictionary&lt;string,string&gt; extension method that converts the @this to a name value collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a NameValueCollection.</returns>
        public static NameValueCollection ToNameValueCollection(this IDictionary<string, string> @this)
        {
            if (@this == null)
            {
                return null;
            }

            var col = new NameValueCollection();
            foreach (var item in @this)
            {
                col.Add(item.Key, item.Value);
            }
            return col;
        }

        /// <summary>
        ///     An IDictionary&lt;TKey,TValue&gt; extension method that converts the @this to a sorted dictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a SortedDictionary&lt;TKey,TValue&gt;</returns>
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IDictionary<TKey, TValue> @this)
        {
            return new SortedDictionary<TKey, TValue>(@this);
        }

        /// <summary>
        ///     An IDictionary&lt;TKey,TValue&gt; extension method that converts the @this to a sorted dictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>@this as a SortedDictionary&lt;TKey,TValue&gt;</returns>
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IDictionary<TKey, TValue> @this, IComparer<TKey> comparer)
        {
            return new SortedDictionary<TKey, TValue>(@this, comparer);
        }

        /// <summary>
        ///     An IDictionary&lt;string,object&gt; extension method that converts the @this to a SQL parameters.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a SqlParameter[].</returns>
        public static SqlParameter[] ToSqlParameters(this IDictionary<string, object> @this)
        {
            return @this.Select(x => new SqlParameter(x.Key, x.Value)).ToArray();
        }
    }
}
