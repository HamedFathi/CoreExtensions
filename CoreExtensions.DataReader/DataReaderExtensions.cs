using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace CoreExtensions
{
    public static class DataReaderExtensions
    {
        public static IEnumerable<IDataRecord> AsEnumerable(this IDataReader reader)
        {
            while (reader.Read())
            {
                yield return reader;
            }
        }

        public static TReturn ColumnReader<TObject, TReturn>(this IDataReader reader, Expression<Func<TObject, object>> columnName)
                    where TObject : class
        {
            return reader.ColumnReader<TReturn>(columnName.GetPropertyName());
        }

        public static TReturn ColumnReader<TReturn>(this IDataReader reader, string columnName)
        {
            try
            {
                object retValue = null;
                var type = typeof(TReturn);
                var index = reader.GetOrdinal(columnName);
                if (!reader.IsDBNull(index))
                {
                    if (type == typeof(bool))
                        retValue = reader.GetBoolean(index);

                    if (type == typeof(byte))
                        retValue = reader.GetByte(index);

                    if (type == typeof(byte[]))
                    {
                        var byteArray = new byte[(reader.GetBytes(index, 0, null, 0, int.MaxValue))];
                        reader.GetBytes(index, 0, byteArray, 0, byteArray.Length);
                        retValue = byteArray;
                    }

                    if (type == typeof(char))
                        retValue = reader.GetChar(index);

                    if (type == typeof(char[]))
                    {
                        var charArray = new char[(reader.GetChars(index, 0, null, 0, int.MaxValue))];
                        reader.GetChars(index, 0, charArray, 0, charArray.Length);
                        retValue = charArray;
                    }

                    if (type == typeof(IDataReader))
                        retValue = reader.GetData(index);

                    if (type == typeof(DateTime))
                        retValue = reader.GetDateTime(index);

                    if (type == typeof(float))
                        retValue = reader.GetFloat(index);

                    if (type == typeof(decimal))
                        retValue = reader.GetDecimal(index);

                    if (type == typeof(double))
                        retValue = reader.GetDouble(index);

                    if (type == typeof(Type))
                        retValue = reader.GetFieldType(index);

                    if (type == typeof(Guid))
                        retValue = reader.GetGuid(index);

                    if (type == typeof(short))
                        retValue = reader.GetInt16(index);

                    if (type == typeof(int))
                        retValue = reader.GetInt32(index);

                    if (type == typeof(long))
                        retValue = reader.GetInt64(index);


                    if (type == typeof(string))
                        retValue = reader.GetString(index);

                    if (type == typeof(object))
                        retValue = reader.GetValue(index);

                    return (TReturn)retValue;
                }
                return default(TReturn);
            }
            catch (Exception ex)
            {
                if (ex is IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException("The column name does not exist.");
                }

                if (ex is InvalidCastException)
                {
                    throw new InvalidCastException("The return type, with the type of data in the database is not in compliance");
                }
                return default(TReturn);
            }
        }

        /// <summary>
        ///     An IDataReader extension method that query if '@this' contains column.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnIndex">Zero-based index of the column.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool ContainsColumn(this IDataReader @this, int columnIndex)
        {
            try
            {
                // Check if FieldCount is implemented first
                return @this.FieldCount > columnIndex;
            }
            catch (Exception)
            {
                try
                {
                    return @this[columnIndex] != null;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///     An IDataReader extension method that query if '@this' contains column.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool ContainsColumn(this IDataReader @this, string columnName)
        {
            try
            {
                // Check if GetOrdinal is implemented first
                return @this.GetOrdinal(columnName) != -1;
            }
            catch (Exception)
            {
                try
                {
                    return @this[columnName] != null;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <reremarks>found on http://www.base4.net/Blog.aspx?ID=409</reremarks>
        public static IEnumerable<IDataRecord> Enumerate(this IDataReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                    yield return reader;
            }
        }

        /// <summary>
        ///     An IDataReader extension method that applies an operation to all items in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="action">The action.</param>
        /// <returns>An IDataReader.</returns>
        public static IDataReader ForEach(this IDataReader @this, Action<IDataReader> action)
        {
            while (@this.Read())
            {
                action(@this);
            }

            return @this;
        }

        /// <summary>
        /// 	Gets the record value casted to the specified data type or the data types default value.
        /// </summary>
        /// <typeparam name = "T">The return data type</typeparam>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static T Get<T>(this IDataReader reader, string field)
        {
            return reader.Get(field, default(T));
        }

        /// <summary>
        /// 	Gets the record value casted to the specified data type or the specified default value.
        /// </summary>
        /// <typeparam name = "T">The return data type</typeparam>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static T Get<T>(this IDataReader reader, string field, T defaultValue)
        {
            var value = reader[field];
            if (value == DBNull.Value)
                return defaultValue;

            if (value is T)
                return (T)value;

            return value.To(defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as bool or false.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static bool GetBoolean(this IDataReader reader, string field)
        {
            return reader.GetBoolean(field, false);
        }

        /// <summary>
        /// 	Gets the record value casted as bool or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static bool GetBoolean(this IDataReader reader, string field, bool defaultValue)
        {
            var value = reader[field];
            return (value is bool ? (bool)value : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as byte array.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static byte[] GetBytes(this IDataReader reader, string field)
        {
            return (reader[field] as byte[]);
        }

        /// <summary>
        ///     Gets the column names in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>An enumerator that allows foreach to be used to get the column names in this collection.</returns>
        public static IEnumerable<string> GetColumnNames(this IDataRecord @this)
        {
            return Enumerable.Range(0, @this.FieldCount)
                .Select(@this.GetName)
                .ToList();
        }

        /// <summary>
        /// 	Gets the record value casted as DateTime or DateTime.MinValue.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static DateTime GetDateTime(this IDataReader reader, string field)
        {
            return reader.GetDateTime(field, DateTime.MinValue);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTime or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static DateTime GetDateTime(this IDataReader reader, string field, DateTime defaultValue)
        {
            var value = reader[field];
            return (value is DateTime ? (DateTime)value : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTimeOffset (UTC) or DateTime.MinValue.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static DateTimeOffset GetDateTimeOffset(this IDataReader reader, string field)
        {
            return new DateTimeOffset(reader.GetDateTime(field), TimeSpan.Zero);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTimeOffset (UTC) or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static DateTimeOffset GetDateTimeOffset(this IDataReader reader, string field, DateTimeOffset defaultValue)
        {
            var dt = reader.GetDateTime(field);
            return (dt != DateTime.MinValue ? new DateTimeOffset(dt, TimeSpan.Zero) : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as decimal or 0.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static decimal GetDecimal(this IDataReader reader, string field)
        {
            return reader.GetDecimal(field, 0);
        }

        /// <summary>
        /// 	Gets the record value casted as decimal or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static decimal GetDecimal(this IDataReader reader, string field, long defaultValue)
        {
            var value = reader[field];
            return (value is decimal ? (decimal)value : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as Guid or Guid.Empty.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static Guid GetGuid(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is Guid ? (Guid)value : Guid.Empty);
        }

        /// <summary>
        /// 	Gets the record value casted as int or 0.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static int GetInt32(this IDataReader reader, string field)
        {
            return reader.GetInt32(field, 0);
        }

        /// <summary>
        /// 	Gets the record value casted as int or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static int GetInt32(this IDataReader reader, string field, int defaultValue)
        {
            var value = reader[field];
            return (value is int ? (int)value : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as long or 0.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static long GetInt64(this IDataReader reader, string field)
        {
            return reader.GetInt64(field, 0);
        }

        /// <summary>
        /// 	Gets the record value casted as long or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static long GetInt64(this IDataReader reader, string field, int defaultValue)
        {
            var value = reader[field];
            return (value is long l ? l : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as bool or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static bool? GetNullableBoolean(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is bool ? (bool?)value : null);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTime or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static DateTime? GetNullableDateTime(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is DateTime ? (DateTime?)value : null);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTimeOffset (UTC) or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static DateTimeOffset? GetNullableDateTimeOffset(this IDataReader reader, string field)
        {
            var dt = reader.GetNullableDateTime(field);
            return (dt != null ? (DateTimeOffset?)new DateTimeOffset(dt.Value, TimeSpan.Zero) : null);
        }

        /// <summary>
        /// 	Gets the record value casted as decimal or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static decimal? GetNullableDecimal(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is decimal ? (decimal?)value : null);
        }

        /// <summary>
        /// 	Gets the record value casted as Guid? or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static Guid? GetNullableGuid(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is Guid ? (Guid?)value : null);
        }

        /// <summary>
        /// 	Gets the record value casted as int or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static int? GetNullableInt32(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is int ? (int?)value : null);
        }

        /// <summary>
        /// 	Gets the record value casted as long or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static long? GetNullableInt64(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is long ? (long?)value : null);
        }

        /// <summary>
        /// 	Gets the record value casted as string or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static string GetString(this IDataReader reader, string field)
        {
            return reader.GetString(field, null);
        }

        /// <summary>
        /// 	Gets the record value casted as string or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static string GetString(this IDataReader reader, string field, string defaultValue)
        {
            var value = reader[field];
            return (value is string ? (string)value : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value as Type class instance or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static Type GetType(this IDataReader reader, string field)
        {
            return reader.GetType(field, null);
        }

        /// <summary>
        /// 	Gets the record value as Type class instance or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static Type GetType(this IDataReader reader, string field, Type defaultValue)
        {
            var classType = reader.GetString(field);
            if (!string.IsNullOrEmpty(classType))
            {
                var type = Type.GetType(classType);
                if (type != null)
                    return type;
            }
            return defaultValue;
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static object GetTypeInstance(this IDataReader reader, string field)
        {
            return reader.GetTypeInstance(field, null);
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or the specified default type.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static object GetTypeInstance(this IDataReader reader, string field, Type defaultValue)
        {
            var type = reader.GetType(field, defaultValue);
            return (type != null ? Activator.CreateInstance(type) : null);
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or null.
        /// </summary>
        /// <typeparam name = "T">The type to be casted to</typeparam>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static T GetTypeInstance<T>(this IDataReader reader, string field) where T : class
        {
            return (reader.GetTypeInstance(field, null) as T);
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or the specified default type.
        /// </summary>
        /// <typeparam name = "T">The type to be casted to</typeparam>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "type">The type.</param>
        /// <returns>The record value</returns>
        public static T GetTypeInstanceSafe<T>(this IDataReader reader, string field, Type type) where T : class
        {
            var instance = (reader.GetTypeInstance(field, null) as T);
            return (instance ?? Activator.CreateInstance(type) as T);
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or an instance from the specified type.
        /// </summary>
        /// <typeparam name = "T">The type to be casted to</typeparam>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static T GetTypeInstanceSafe<T>(this IDataReader reader, string field) where T : class, new()
        {
            var instance = (reader.GetTypeInstance(field, null) as T);
            return (instance ?? new T());
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <returns>The value as.</returns>
        public static T GetValueAs<T>(this IDataReader @this, int index)
        {
            return (T)@this.GetValue(index);
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The value as.</returns>
        public static T GetValueAs<T>(this IDataReader @this, string columnName)
        {
            return (T)@this.GetValue(@this.GetOrdinal(columnName));
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <returns>The value as or default.</returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, int index)
        {
            try
            {
                return (T)@this.GetValue(index);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value as or default.</returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, int index, T defaultValue)
        {
            try
            {
                return (T)@this.GetValue(index);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The value as or default.</returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, int index, Func<IDataReader, int, T> defaultValueFactory)
        {
            try
            {
                return (T)@this.GetValue(index);
            }
            catch
            {
                return defaultValueFactory(@this, index);
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The value as or default.</returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, string columnName)
        {
            try
            {
                return (T)@this.GetValue(@this.GetOrdinal(columnName));
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value as or default.</returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, string columnName, T defaultValue)
        {
            try
            {
                return (T)@this.GetValue(@this.GetOrdinal(columnName));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The value as or default.</returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, string columnName, Func<IDataReader, string, T> defaultValueFactory)
        {
            try
            {
                return (T)@this.GetValue(@this.GetOrdinal(columnName));
            }
            catch
            {
                return defaultValueFactory(@this, columnName);
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <returns>The value to.</returns>
        public static T GetValueTo<T>(this IDataReader @this, int index)
        {
            return @this.GetValue(index).To<T>();
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The value to.</returns>
        public static T GetValueTo<T>(this IDataReader @this, string columnName)
        {
            return @this.GetValue(@this.GetOrdinal(columnName)).To<T>();
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <returns>The value to or default.</returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, int index)
        {
            try
            {
                return @this.GetValue(index).To<T>();
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value to or default.</returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, int index, T defaultValue)
        {
            try
            {
                return @this.GetValue(index).To<T>();
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The value to or default.</returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, int index, Func<IDataReader, int, T> defaultValueFactory)
        {
            try
            {
                return @this.GetValue(index).To<T>();
            }
            catch
            {
                return defaultValueFactory(@this, index);
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The value to or default.</returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, string columnName)
        {
            try
            {
                return @this.GetValue(@this.GetOrdinal(columnName)).To<T>();
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value to or default.</returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, string columnName, T defaultValue)
        {
            try
            {
                return @this.GetValue(@this.GetOrdinal(columnName)).To<T>();
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The value to or default.</returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, string columnName, Func<IDataReader, string, T> defaultValueFactory)
        {
            try
            {
                return @this.GetValue(@this.GetOrdinal(columnName)).To<T>();
            }
            catch
            {
                return defaultValueFactory(@this, columnName);
            }
        }

        /// <summary>
        /// Returns the index of a column by name (case insensitive) or -1
        /// </summary>
        /// <param name="this"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int IndexOf(this IDataRecord @this, string name)
        {
            for (int i = 0; i < @this.FieldCount; i++)
            {
                if (String.Compare(@this.GetName(i), name, StringComparison.OrdinalIgnoreCase) == 0) return i;
            }
            return -1;
        }

        /// <summary>
        /// 	Determines whether the record value is DBNull.Value
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>
        /// 	<c>true</c> if the value is DBNull.Value; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDbNull(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value == DBNull.Value);
        }

        /// <summary>
        /// 	Reads all all records from a data reader and performs an action for each.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "action">The action to be performed.</param>
        /// <returns>
        /// 	The count of actions that were performed.
        /// </returns>
        public static int ReadAll(this IDataReader reader, Action<IDataReader> action)
        {
            var count = 0;
            while (reader.Read())
            {
                action(reader);
                count++;
            }
            return count;
        }

        public static DataTable ToDataTable(this IDataReader dr)
        {
            DataTable result = new DataTable();
            result.Load(dr);
            return result;
        }
    }
}
