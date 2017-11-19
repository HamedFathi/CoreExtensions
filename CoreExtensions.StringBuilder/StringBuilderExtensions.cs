using System;
using System.Collections.Generic;
using System.Text;

namespace CoreExtensions
{
    public static class StringBuilderExtensions
    {
        /// <summary>
        ///     Appends the string returned by processing a composite format string, which contains zero or more format items,
        ///     to the end of the current System.Text.StringBuilder object if a condition is true
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="condition"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static StringBuilder AppendFormatIf(this StringBuilder sb, bool condition, string format,
                                    params object[] args)
        {
            if (condition) sb.AppendFormat(format, args);
            return sb;
        }

        /// <summary>
        /// Apends the text if the condition is true.
        /// </summary>
        public static void AppendIf(this StringBuilder sb, bool condition, char text)
        {
            if (condition)
            {
                sb.Append(text);
            }
        }

        /// <summary>
        /// Apends the text if the condition is true.
        /// </summary>
        public static void AppendIf(this StringBuilder sb, bool condition, string text)
        {
            if (condition)
            {
                sb.Append(text);
            }
        }

        /// <summary>A StringBuilder extension method that appends a when.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>A StringBuilder.</returns>
        public static StringBuilder AppendIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (var value in values)
            {
                if (predicate(value))
                {
                    @this.Append(value);
                }
            }

            return @this;
        }

        /// <summary>
        ///     Appends the value of the object's System.Object.ToString() method to the end of the current
        ///     System.Text.StringBuilder object if a condition is true
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="condition"></param>
        /// <param name="value"></param>
        public static StringBuilder AppendIf(this StringBuilder sb, bool condition, object value)
        {
            if (condition) sb.Append(value);
            return sb;
        }

        /// <summary>A StringBuilder extension method that appends a join.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="values">The values.</param>
        public static StringBuilder AppendJoin<T>(this StringBuilder @this, string separator, IEnumerable<T> values)
        {
            @this.Append(string.Join(separator, values));

            return @this;
        }

        /// <summary>A StringBuilder extension method that appends a join.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="values">The values.</param>
        public static StringBuilder AppendJoin<T>(this StringBuilder @this, string separator, params T[] values)
        {
            @this.Append(string.Join(separator, values));

            return @this;
        }

        /// <summary>
        ///     AppendLine version with format string parameters.
        /// </summary>
        public static void AppendLine(this StringBuilder builder, string value, params object[] parameters)
        {
            builder.AppendLine(string.Format(value, parameters));
        }

        /// <summary>
        ///     A StringBuilder extension method that appends a line format.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="format">Describes the format to use.</param>
        /// <param name="args">A variable-length parameters list containing arguments.</param>
        public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, params object[] args)
        {
            @this.AppendLine(string.Format(format, args));

            return @this;
        }

        /// <summary>
        ///     A StringBuilder extension method that appends a line format.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="format">Describes the format to use.</param>
        /// <param name="args">A variable-length parameters list containing arguments.</param>
        public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, List<IEnumerable<object>> args)
        {
            @this.AppendLine(string.Format(format, args));

            return @this;
        }

        public static StringBuilder AppendLineIf(this StringBuilder sb, bool condition, string value)
        {
            if (condition) sb.AppendLine(value);
            return sb;
        }

        /// <summary>A StringBuilder extension method that appends a line when.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>A StringBuilder.</returns>
        public static StringBuilder AppendLineIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (var value in values)
            {
                if (predicate(value))
                {
                    @this.AppendLine(value.ToString());
                }
            }

            return @this;
        }

        /// <summary>
        ///     Appends the value of the object's System.Object.ToString() method followed by the default line terminator to the
        ///     end of the current
        ///     System.Text.StringBuilder object if a condition is true
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="condition">The conditional expression to evaluate.</param>
        /// <param name="value"></param>
        public static StringBuilder AppendLineIf(this StringBuilder sb, bool condition, object value)
        {
            if (condition) sb.AppendLine(value.ToString());
            return sb;
        }

        /// <summary>
        ///     Appends the string returned by processing a composite format string, which contains zero or more format items,
        ///     followed by the default
        ///     line terminator to the end of the current System.Text.StringBuilder object if a condition is true
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="condition">The conditional expression to evaluate.</param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static StringBuilder AppendLineIf(this StringBuilder sb, bool condition, string format, params object[] args)
        {
            if (condition) sb.AppendFormat(format, args).AppendLine();
            return sb;
        }

        /// <summary>A StringBuilder extension method that appends a line join.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="values">The values.</param>
        public static StringBuilder AppendLineJoin<T>(this StringBuilder @this, string separator, IEnumerable<T> values)
        {
            @this.AppendLine(string.Join(separator, values));

            return @this;
        }

        /// <summary>A StringBuilder extension method that appends a line join.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="values">The values.</param>
        public static StringBuilder AppendLineJoin(this StringBuilder @this, string separator, params object[] values)
        {
            @this.AppendLine(string.Join(separator, values));

            return @this;
        }

        /// <summary>
        /// Removes trailling spaces from the stringbuilder + 1 character
        /// </summary>      
        /// <example>
        /// StringBuilder sb = new StringBuilder();
        /// foreach(User user in users)
        /// {
        ///    sb.Append(user.Id);
        ///    sb.Append(", ");
        /// }
        /// return sb.Strip(); //this will strip the trailing ", "
        /// </example>
        /// <returns>
        /// Returns the string contents of the trimmed StringBuilder
        /// </returns>
        public static string Strip(this StringBuilder sb)
        {
            for (int i = sb.Length - 1; i >= 0 && sb[i] == ' '; --i)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        /// <summary>
        /// Removes the specified number of characters from the end of the string
        /// </summary>      
        /// <example>
        /// StringBuilder sb = new StringBuilder();
        /// foreach(User user in users)
        /// {
        ///    sb.Append(user.Id);
        ///    sb.Append("--");
        /// }
        /// return sb.Strip(2); //this will strip the trailing "--"
        /// </example>
        /// <returns>
        /// Returns the string contents of the trimmed StringBuilder
        /// </returns>
        public static string Strip(this StringBuilder sb, int length)
        {
            if (length > sb.Length || length < 1)
            {
                return sb.ToString();
            }
            sb.Remove(sb.Length - length, length);
            return sb.ToString();
        }

        /// <summary>A StringBuilder extension method that substrings.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns>A string.</returns>
        public static string Substring(this StringBuilder @this, int startIndex)
        {
            return @this.ToString(startIndex, @this.Length - startIndex);
        }

        /// <summary>A StringBuilder extension method that substrings.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length.</param>
        /// <returns>A string.</returns>
        public static string Substring(this StringBuilder @this, int startIndex, int length)
        {
            return @this.ToString(startIndex, length);
        }
    }
}
