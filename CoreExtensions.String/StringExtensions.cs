using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace CoreExtensions
{
    public static class StringExtensions
    {
        [Flags]
        public enum CharTypes
        {
            Letters = 0x1,
            Digits = 0x2,
            XmlChar = 0x4,
            WhiteSpace = 0x8,

            LettersOrDigits = Letters | Digits
        }
        /// <summary>
        ///     Options to match the template with the original string
        /// </summary>
        public enum ComparsionTemplateOptions
        {
            /// <summary>
            ///     Free template comparsion
            /// </summary>
            Default,

            /// <summary>
            ///     Template compared from beginning of input string
            /// </summary>
            FromStart,

            /// <summary>
            ///     Template compared with the end of input string
            /// </summary>
            AtTheEnd,

            /// <summary>
            ///     Template compared whole with input string
            /// </summary>
            Whole
        }

        private const RegexOptions DefaultRegexOptions = RegexOptions.None;
        private const ComparsionTemplateOptions DefaultComparsionTemplateOptions = ComparsionTemplateOptions.Default;
        private static readonly string[] ReservedRegexOperators = { @"\", "^", "$", "*", "+", "?", ".", "(", ")" };

        public static IEnumerable<int> AllIndexesOf(this string str, string searchstring)
        {
            int minIndex = str.IndexOf(searchstring, StringComparison.Ordinal);
            while (minIndex != -1)
            {
                yield return minIndex;
                minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length, StringComparison.Ordinal);
            }
        }

        /// <summary>
        ///     Formats the specified formatted string. Usable on constant strings, without falling back to String.Format()
        /// </summary>
        /// <param name="formattedString">The formatted string.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public static string AsFormatted(this string formattedString, params object[] arguments)
        {
            return formattedString == null ? null : string.Format(formattedString, arguments);
        }

        /// <summary>
        ///     Convert a byte array into a hexadecimal string representation.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string BytesToHexString(this byte[] bytes)
        {
            var result = "";
            foreach (var b in bytes)
            {
                result += " " + b.ToString("X").PadLeft(2, '0');
            }
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }

        /// <summary>
        /// Strips the last specified chars from a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeFromEnd">The remove from end.</param>
        /// <returns></returns>
        public static string Chop(this string sourceString, int removeFromEnd)
        {
            string result = sourceString;
            if ((removeFromEnd > 0) && (sourceString.Length > removeFromEnd - 1))
                result = result.Remove(sourceString.Length - removeFromEnd, removeFromEnd);
            return result;
        }

        /// <summary>
        /// Strips the last specified chars from a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="backDownTo">The back down to.</param>
        /// <returns></returns>
        public static string Chop(this string sourceString, string backDownTo)
        {
            int removeDownTo = sourceString.LastIndexOf(backDownTo, StringComparison.Ordinal);
            int removeFromEnd = 0;
            if (removeDownTo > 0)
                removeFromEnd = sourceString.Length - removeDownTo;

            string result = sourceString;

            if (sourceString.Length > removeFromEnd - 1)
                result = result.Remove(removeDownTo, removeFromEnd);

            return result;
        }

        /// <summary>
        ///     Compares two specified  objects by evaluating the numeric values of the corresponding  objects in each string.
        /// </summary>
        /// <param name="strA">The first string to compare.</param>
        /// <param name="strB">The second string to compare.</param>
        /// <returns>
        ///     An integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero
        ///     is less than . Zero  and  are equal. Greater than zero  is greater than .
        /// </returns>
        public static Int32 CompareOrdinal(this String strA, String strB)
        {
            return String.CompareOrdinal(strA, strB);
        }

        /// <summary>
        ///     Compares substrings of two specified  objects by evaluating the numeric values of the corresponding  objects
        ///     in each substring.
        /// </summary>
        /// <param name="strA">The first string to use in the comparison.</param>
        /// <param name="indexA">The starting index of the substring in .</param>
        /// <param name="strB">The second string to use in the comparison.</param>
        /// <param name="indexB">The starting index of the substring in .</param>
        /// <param name="length">The maximum number of characters in the substrings to compare.</param>
        /// <returns>
        ///     A 32-bit signed integer that indicates the lexical relationship between the two comparands.ValueCondition
        ///     Less than zero The substring in  is less than the substring in . Zero The substrings are equal, or  is zero.
        ///     Greater than zero The substring in  is greater than the substring in .
        /// </returns>
        public static Int32 CompareOrdinal(this String strA, Int32 indexA, String strB, Int32 indexB,
                                                            Int32 length)
        {
            return String.CompareOrdinal(strA, indexA, strB, indexB, length);
        }

        /// <summary>
        ///     Concatenates two specified instances of .
        /// </summary>
        /// <param name="str0">The first string to concatenate.</param>
        /// <param name="str1">The second string to concatenate.</param>
        /// <returns>The concatenation of  and .</returns>
        public static String Concat(this String str0, String str1)
        {
            return String.Concat(str0, str1);
        }

        /// <summary>
        ///     Concatenates three specified instances of .
        /// </summary>
        /// <param name="str0">The first string to concatenate.</param>
        /// <param name="str1">The second string to concatenate.</param>
        /// <param name="str2">The third string to concatenate.</param>
        /// <returns>The concatenation of , , and .</returns>
        public static String Concat(this String str0, String str1, String str2)
        {
            return String.Concat(str0, str1, str2);
        }

        /// <summary>
        ///     Concatenates four specified instances of .
        /// </summary>
        /// <param name="str0">The first string to concatenate.</param>
        /// <param name="str1">The second string to concatenate.</param>
        /// <param name="str2">The third string to concatenate.</param>
        /// <param name="str3">The fourth string to concatenate.</param>
        /// <returns>The concatenation of , , , and .</returns>
        public static String Concat(this String str0, String str1, String str2,
                                                            String str3)
        {
            return String.Concat(str0, str1, str2, str3);
        }

        /// <summary>
        ///     A string extension method that concatenate with.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>A string.</returns>
        public static string ConcatWith(this string @this, params string[] values)
        {
            return string.Concat(@this, string.Concat(values));
        }

        public static bool Contains(this string source, string str, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(source))
                return true;
            return source.IndexOf(str, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0;
        }

        /// <summary>
        ///     A string extension method that query if this object contains the given value.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="value">The value.</param>
        /// <returns>true if the value is in the string, false if not.</returns>
        public static bool Contains(this string @this, string value)
        {
            return @this.IndexOf(value, StringComparison.Ordinal) != -1;
        }

        /// <summary>
        ///     A string extension method that query if this object contains the given value.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="value">The value.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns>true if the value is in the string, false if not.</returns>
        public static bool Contains(this string @this, string value, StringComparison comparisonType)
        {
            return @this.IndexOf(value, comparisonType) != -1;
        }

        /// <summary>
        ///     A string extension method that query if '@this' contains all values.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it contains all values, otherwise false.</returns>
        public static bool ContainsAll(this string @this, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, StringComparison.Ordinal) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        ///     A string extension method that query if this object contains the given @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it contains all values, otherwise false.</returns>
        public static bool ContainsAll(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ContainsAny(this string str, bool ignoreCase, params string[] strs)
        {
            str = ignoreCase ? str.ToLowerInvariant() : str;
            foreach (string needle in strs)
            {
                var n = ignoreCase ? needle.ToLowerInvariant() : needle;
                if (str.Contains(n))
                    return true;
            }
            return false;
        }

        /// <summary>
        ///     A string extension method that query if '@this' contains any values.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it contains any values, otherwise false.</returns>
        public static bool ContainsAny(this string @this, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, StringComparison.Ordinal) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     A string extension method that query if '@this' contains any values.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it contains any values, otherwise false.</returns>
        public static bool ContainsAny(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainsIgnoreCase(this string source, string target)
        {
            return source != null && target != null && source.IndexOf(target, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static bool ContainsIgnoreCase(this string source, string[] targets)
        {
            foreach (var item in targets)
            {
                if (source.ContainsIgnoreCase(item))
                    return true;
            }
            return false;
        }

        public static System.Drawing.Image ConvertBase64ToImage(this string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        /// <summary>
        /// Converts the input string encoded as the encoding specified to the target encoding
        /// </summary>
        /// <param name="input">The input string to check</param>
        /// <param name="targetEncoding">The desired encoding to convert to</param>
        /// <param name="sourceEncoding">The encoding of the input string</param>
        /// <returns>The input string converted in the targetEncoding</returns>
        /// <exception cref="System.ArgumentNullException">input is null</exception>
        /// <exception cref="System.ArgumentNullException">sourceEncoding is null</exception>
        /// <exception cref="System.ArgumentNullException">targetEncoding is null</exception>
        public static string ConvertEncodingTo(this string input, Encoding targetEncoding, Encoding sourceEncoding)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            if (targetEncoding == null)
                throw new ArgumentNullException("targetEncoding");
            if (sourceEncoding == null)
                throw new ArgumentNullException("sourceEncoding");

            byte[] sourceBytes = sourceEncoding.GetBytes(input);

            byte[] targetBytes = Encoding.Convert(sourceEncoding, targetEncoding, sourceBytes);

            string result = targetEncoding.GetString(targetBytes, 0, targetBytes.Length);

            return result;
        }

        /// <summary>
        /// Converts the input string to the target encoding
        /// </summary>
        /// <param name="input">The input string to check</param>
        /// <param name="targetEncoding">The desired encoding to convert to</param>
        /// <returns>The input string converted in the targetEncoding</returns>
        /// <exception cref="System.ArgumentNullException">input is null</exception>
        /// <exception cref="System.ArgumentNullException">targetEncoding is null</exception>
        public static string ConvertEncodingToUTF8(this string input, Encoding targetEncoding)
        {
            return ConvertEncodingTo(input, targetEncoding, Encoding.UTF8);
        }

        /// <summary>
        ///     Converts the value of a UTF-16 encoded character or surrogate pair at a specified position in a string into a
        ///     Unicode code point.
        /// </summary>
        /// <param name="s">A string that contains a character or surrogate pair.</param>
        /// <param name="index">The index position of the character or surrogate pair in .</param>
        /// <returns>
        ///     The 21-bit Unicode code point represented by the character or surrogate pair at the position in the parameter
        ///     specified by the  parameter.
        /// </returns>
        public static Int32 ConvertToUtf32(this String s, Int32 index)
        {
            return Char.ConvertToUtf32(s, index);
        }

        public static string CreateRawXsd(string xml, Encoding encoding = null)
        {
            var xsd = "";
            encoding = encoding ?? Encoding.UTF8;
            var stream = new MemoryStream(encoding.GetBytes(xml ?? ""));
            XmlReader reader = XmlReader.Create(stream);
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            XmlSchemaInference schema = new XmlSchemaInference();
            schemaSet = schema.InferSchema(reader);
            reader.Close();
            foreach (XmlSchema s in schemaSet.Schemas())
            {
                using (var stringWriter = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings
                    {
                        Indent = true,
                        Encoding = encoding
                    }))
                    {
                        s.Write(writer);
                    }

                    xsd = stringWriter.ToString();
                }
            }
            return xsd;
        }

        /// <summary>
        ///     A string extension method that decode a Base64 String.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The Base64 String decoded.</returns>
        public static string DecodeBase64(this string @this)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(@this));
        }

        /// <summary>
        ///     Decodes a Base 64 encoded value to a string using the supplied encoding.
        /// </summary>
        /// <param name="encodedValue">The Base 64 encoded value.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The decoded string</returns>
        public static string DecodeBase64(this string encodedValue, Encoding encoding)
        {
            encoding = encoding ?? Encoding.UTF8;
            var bytes = Convert.FromBase64String(encodedValue);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Returns an empty string if the current string object is null.
        /// </summary>
        public static string DefaultIfNull(this string s)
        {
            return s != null ? s : string.Empty;
        }

        /// <summary>
        /// Returns the specified default value if the current string object is null.
        /// </summary>
        public static string DefaultIfNull(this string s, string defaultValue)
        {
            return s != null ? s : (defaultValue ?? string.Empty);
        }

        /// <summary>
        /// Returns the specified default value if the current string object is null 
        /// or empty.
        /// </summary>
        public static string DefaultIfNullOrEmpty(this string s, string defaultValue)
        {
            if (string.IsNullOrEmpty(defaultValue))
                throw new ArgumentNullException(nameof(defaultValue));

            return string.IsNullOrEmpty(s) ? s : defaultValue;
        }

        /// <summary>
        ///     A string extension method that encode the string to Base64.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The encoded string to Base64.</returns>
        public static string EncodeBase64(this string @this)
        {
            return Convert.ToBase64String(Activator.CreateInstance<UTF8Encoding>().GetBytes(@this));
        }

        /// <summary>
        ///     Encodes the input value to a Base64 string using the supplied encoding.
        /// </summary>
        /// <param name="value">The input value.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The Base 64 encoded string</returns>
        public static string EncodeBase64(this string value, Encoding encoding)
        {
            encoding = encoding ?? Encoding.UTF8;
            var bytes = encoding.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        ///     Encodes the email address so that the link is still valid, but the email address is useless for email harvsters.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        public static string EncodeEmailAddress(this string emailAddress)
        {
            int i;
            string repl;
            var tempHtmlEncode = emailAddress;
            for (i = tempHtmlEncode.Length; i >= 1; i--)
            {
                var acode = Convert.ToInt32(tempHtmlEncode[i - 1]);
                if (acode == 32)
                {
                    repl = " ";
                }
                else if (acode == 34)
                {
                    repl = "\"";
                }
                else if (acode == 38)
                {
                    repl = "&";
                }
                else if (acode == 60)
                {
                    repl = "<";
                }
                else if (acode == 62)
                {
                    repl = ">";
                }
                else if (acode >= 32 && acode <= 127)
                {
                    repl = "&#" + Convert.ToString(acode) + ";";
                }
                else
                {
                    repl = "&#" + Convert.ToString(acode) + ";";
                }
                if (repl.Length > 0)
                {
                    tempHtmlEncode = tempHtmlEncode.Substring(0, i - 1) +
                                     repl + tempHtmlEncode.Substring(i);
                }
            }
            return tempHtmlEncode;
        }

        /// <summary>
        ///     Ensures that a string ends with a given suffix.
        /// </summary>
        /// <param name="value">The string value to check.</param>
        /// <param name="suffix">The suffix value to check for.</param>
        /// <returns>The string value including the suffix</returns>
        /// <example>
        ///     <code>
        /// 		var url = "http://www.pgk.de";
        /// 		url = url.EnsureEndsWith("/"));
        /// 	</code>
        /// </example>
        public static string EnsureEndsWith(this string value, string suffix)
        {
            return value.EndsWith(suffix) ? value : string.Concat(value, suffix);
        }

        /// <summary>
        ///     Ensures that a string starts with a given prefix.
        /// </summary>
        /// <param name="value">The string value to check.</param>
        /// <param name="prefix">The prefix value to check for.</param>
        /// <returns>The string value including the prefix</returns>
        /// <example>
        ///     <code>
        /// 		var extension = "txt";
        /// 		var fileName = string.Concat(file.Name, extension.EnsureStartsWith("."));
        /// 	</code>
        /// </example>
        public static string EnsureStartsWith(this string value, string prefix)
        {
            return value.StartsWith(prefix) ? value : string.Concat(prefix, value);
        }

        /// <summary>
        ///     Determines whether the string is equal to any of the provided values.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool EqualsAny(this string value, StringComparison comparisonType, params string[] values)
        {
            return values.Any(v => value.Equals(v, comparisonType));
        }

        /// <summary>
        ///     A string extension method that escape XML.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string EscapeXml(this string @this)
        {
            return
                @this.Replace("&", "&amp;")
                    .Replace("<", "&lt;")
                    .Replace(">", "&gt;")
                    .Replace("\"", "&quot;")
                    .Replace("'", "&apos;");
        }

        /// <summary>
        ///     Throws an <see cref="System.ArgumentException" /> if the string value is empty.
        /// </summary>
        /// <param name="obj">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        /// <param name="name">The name of the parameter being tested.</param>
        public static string ExceptionIfNullOrEmpty(this string obj, string message, string name)
        {
            if (string.IsNullOrEmpty(obj))
                throw new ArgumentException(message, name);
            return obj;
        }

        /// <summary>
        ///     A string extension method that extracts this object.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A string.</returns>
        public static string Extract(this string @this, Func<char, bool> predicate)
        {
            return new string(@this.ToCharArray().Where(predicate).ToArray());
        }

        /// <summary>
        ///     Extracts characters that exist within the given char array
        /// </summary>
        /// <param name="value"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string Extract(this string value, params char[] chars)
        {
            return value.Extract(c => Array.IndexOf(chars, c) > -1);
        }

        /// <summary>
        ///     Extracts characters that exist within the given char string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string Extract(this string value, string chars)
        {
            return value.Extract(chars.ToCharArray());
        }

        /// <summary>
        ///     Extract arguments from string by template
        /// </summary>
        /// <param name="value">The input string. For example, "My name is Aleksey".</param>
        /// <param name="template">Template with arguments in the format {№}. For example, "My name is {0} {1}.".</param>
        /// <param name="compareTemplateOptions">Template options for compare with input string.</param>
        /// <param name="regexOptions">Regex options.</param>
        /// <returns>Returns parsed arguments.</returns>
        /// <example>
        ///     <code>
        /// 		var str = "My name is Aleksey Nagovitsyn. I'm from Russia.";
        /// 		var args = str.ExtractArguments(@"My name is {1} {0}. I'm from {2}.");
        ///         // args[i] is [Nagovitsyn, Aleksey, Russia]
        /// 	</code>
        /// </example>
        /// <remarks>
        ///     Contributed by nagits, http://about.me/AlekseyNagovitsyn
        /// </remarks>
        public static IEnumerable<string> ExtractArguments(this string value, string template,
                                                            ComparsionTemplateOptions compareTemplateOptions = DefaultComparsionTemplateOptions,
                                                            RegexOptions regexOptions = DefaultRegexOptions)
        {
            return ExtractGroupArguments(value, template, compareTemplateOptions, regexOptions).Select(g => g.Value);
        }

        /// <summary>
        ///     A string extension method that extracts the Decimal from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted Decimal.</returns>
        public static decimal ExtractDecimal(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]) || @this[i] == '.')
                {
                    if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                    {
                        sb.Append('-');
                    }
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToDecimal(sb.ToString());
        }

        /// <summary>
        ///     Extracts all digits from a string.
        /// </summary>
        /// <param name="value">String containing digits to extract</param>
        /// <returns>
        ///     All digits contained within the input string
        /// </returns>
        /// <remarks>
        ///     Contributed by Kenneth Scott
        /// </remarks>
        public static string ExtractDigits(this string value)
        {
            return
                value.Where(char.IsDigit).Aggregate(new StringBuilder(value.Length), (sb, c) => sb.Append(c)).ToString();
        }

        /// <summary>
        ///     A string extension method that extracts the Double from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted Double.</returns>
        public static double ExtractDouble(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]) || @this[i] == '.')
                {
                    if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                    {
                        sb.Append('-');
                    }
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToDouble(sb.ToString());
        }

        /// <summary>
        ///     Extract arguments as regex groups from string by template
        /// </summary>
        /// <param name="value">The input string. For example, "My name is Aleksey".</param>
        /// <param name="template">Template with arguments in the format {№}. For example, "My name is {0} {1}.".</param>
        /// <param name="compareTemplateOptions">Template options for compare with input string.</param>
        /// <param name="regexOptions">Regex options.</param>
        /// <returns>Returns parsed arguments as regex groups.</returns>
        /// <example>
        ///     <code>
        /// 		var str = "My name is Aleksey Nagovitsyn. I'm from Russia.";
        /// 		var groupArgs = str.ExtractGroupArguments(@"My name is {1} {0}. I'm from {2}.");
        ///         // groupArgs[i].Value is [Nagovitsyn, Aleksey, Russia]
        /// 	</code>
        /// </example>
        /// <remarks>
        ///     Contributed by nagits, http://about.me/AlekseyNagovitsyn
        /// </remarks>
        public static IEnumerable<Group> ExtractGroupArguments(this string value, string template,
                                                            ComparsionTemplateOptions compareTemplateOptions = DefaultComparsionTemplateOptions,
                                                            RegexOptions regexOptions = DefaultRegexOptions)
        {
            var pattern = GetRegexPattern(template, compareTemplateOptions);
            var regex = new Regex(pattern, regexOptions);
            var match = regex.Match(value);

            return match.Groups.Cast<Group>().Skip(1);
        }

        /// <summary>
        ///     A string extension method that extracts the Int16 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted Int16.</returns>
        public static short ExtractInt16(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]))
                {
                    if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                    {
                        sb.Append('-');
                    }
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToInt16(sb.ToString());
        }

        /// <summary>
        ///     A string extension method that extracts the Int32 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted Int32.</returns>
        public static int ExtractInt32(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]))
                {
                    if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                    {
                        sb.Append('-');
                    }
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToInt32(sb.ToString());
        }

        /// <summary>
        ///     A string extension method that extracts the Int64 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted Int64.</returns>
        public static long ExtractInt64(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]))
                {
                    if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                    {
                        sb.Append('-');
                    }
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToInt64(sb.ToString());
        }

        /// <summary>
        ///     A string extension method that extracts the letter described by @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted letter.</returns>
        public static string ExtractLetter(this string @this)
        {
            return new string(@this.ToCharArray().Where(Char.IsLetter).ToArray());
        }

        /// <summary>
        ///     A string extension method that extracts the number described by @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted number.</returns>
        public static string ExtractNumber(this string @this)
        {
            return new string(@this.ToCharArray().Where(Char.IsNumber).ToArray());
        }

        /// <summary>
        ///     A string extension method that extracts the UInt16 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted UInt16.</returns>
        public static ushort ExtractUInt16(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]))
                {
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToUInt16(sb.ToString());
        }

        /// <summary>
        ///     A string extension method that extracts the UInt32 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted UInt32.</returns>
        public static uint ExtractUInt32(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]))
                {
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToUInt32(sb.ToString());
        }

        /// <summary>
        ///     A string extension method that extracts the UInt64 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted UInt64.</returns>
        public static ulong ExtractUInt64(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]))
                {
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToUInt64(sb.ToString());
        }

        public static List<int> FindAllIndexOf(this string str, string substr, bool ignoreCase)
        {
            if (string.IsNullOrWhiteSpace(str) ||
                string.IsNullOrWhiteSpace(substr))
            {
                throw new ArgumentException("String or substring is not specified.");
            }
            var indexes = new List<int>();
            var index = 0;
            while (
            (index =
                str.IndexOf(substr, index,
                    ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)) != -1)
            {
                indexes.Add(index++);
            }
            return indexes.ToList();
        }

        public static List<int> FindAllIndexOf(this string text, string pattern)
        {
            var indices = new List<int>();
            foreach (Match match in Regex.Matches(text, pattern))
                indices.Add(match.Index);

            return indices;
        }

        /// <summary>
        ///     Replaces the format item in a specified string with the string representation of a corresponding object in a
        ///     specified array.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>
        ///     A copy of  in which the format items have been replaced by the string representation of the corresponding
        ///     objects in .
        /// </returns>
        public static String Format(this String format, params Object[] args)
        {
            return String.Format(format, args);
        }

        public static string FormatXml(this string inputXml)
        {
            XmlDocument document = new XmlDocument();
            document.Load(new StringReader(inputXml));

            StringBuilder builder = new StringBuilder();
            using (XmlTextWriter writer = new XmlTextWriter(new StringWriter(builder)))
            {
                writer.Formatting = Formatting.Indented;
                document.Save(writer);
            }

            var data = builder.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

            return data;
        }

        /// <summary>
        ///     A string extension method that get the string after the specified string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="value">The value to search.</param>
        /// <returns>The string after the specified value.</returns>
        public static string GetAfter(this string @this, string value)
        {
            if (@this.IndexOf(value, StringComparison.Ordinal) == -1)
            {
                return "";
            }
            return @this.Substring(@this.IndexOf(value, StringComparison.Ordinal) + value.Length);
        }

        public static List<string> GetAllXmlTags(this string xml)
        {
            var list = new List<string>();
            var doc = xml.ToXmlDocument();
            foreach (var name in doc.ToXDocument().Root.DescendantNodes().OfType<XElement>().Select(x => x.Name).Distinct())
            {
                list.Add(name.ToString());
            }
            return list;
        }

        /// <summary>
        ///     A string extension method that get the string before the specified string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="value">The value to search.</param>
        /// <returns>The string before the specified value.</returns>
        public static string GetBefore(this string @this, string value)
        {
            if (@this.IndexOf(value, StringComparison.Ordinal) == -1)
            {
                return "";
            }
            return @this.Substring(0, @this.IndexOf(value, StringComparison.Ordinal));
        }

        /// <summary>
        ///     A string extension method that get the string between the two specified string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="before">The string before to search.</param>
        /// <param name="after">The string after to search.</param>
        /// <returns>The string between the two specified string.</returns>
        public static string GetBetween(this string @this, string before, string after)
        {
            int beforeStartIndex = @this.IndexOf(before, StringComparison.Ordinal);
            int startIndex = beforeStartIndex + before.Length;
            int afterStartIndex = @this.IndexOf(after, startIndex, StringComparison.Ordinal);

            if (beforeStartIndex == -1 || afterStartIndex == -1)
            {
                return "";
            }

            return @this.Substring(startIndex, afterStartIndex - startIndex);
        }

        public static byte[] GetBytes(this string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        public static byte[] GetBytes(this string data, Encoding encoding)
        {
            return encoding.GetBytes(data);
        }

        public static List<char> GetChars(this string input)
        {
            return input.Select(x => x).ToList();
        }

        public static List<string> GetLineByLineText(this string input)
        {
            return input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        /// <summary>
        ///     Uses regular expressions to determine all matches of a given regex pattern.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <returns>A collection of all matches</returns>
        public static MatchCollection GetMatches(this string value, string regexPattern)
        {
            return GetMatches(value, regexPattern, RegexOptions.None);
        }

        /// <summary>
        ///     Uses regular expressions to determine all matches of a given regex pattern.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <param name="options">The regular expression options.</param>
        /// <returns>A collection of all matches</returns>
        public static MatchCollection GetMatches(this string value, string regexPattern, RegexOptions options)
        {
            return Regex.Matches(value, regexPattern, options);
        }

        /// <summary>
        ///     Uses regular expressions to determine all matches of a given regex pattern and returns them as string enumeration.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <returns>An enumeration of matching strings</returns>
        /// <example>
        ///     <code>
        /// 		var s = "12345";
        /// 		foreach(var number in s.GetMatchingValues(@"\d")) {
        /// 		Console.WriteLine(number);
        /// 		}
        /// 	</code>
        /// </example>
        public static IEnumerable<string> GetMatchingValues(this string value, string regexPattern)
        {
            return GetMatchingValues(value, regexPattern, RegexOptions.None);
        }

        /// <summary>
        ///     Uses regular expressions to determine all matches of a given regex pattern and returns them as string enumeration.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <param name="options">The regular expression options.</param>
        /// <returns>An enumeration of matching strings</returns>
        /// <example>
        ///     <code>
        /// 		var s = "12345";
        /// 		foreach(var number in s.GetMatchingValues(@"\d")) {
        /// 		Console.WriteLine(number);
        /// 		}
        /// 	</code>
        /// </example>
        public static IEnumerable<string> GetMatchingValues(this string value, string regexPattern, RegexOptions options)
        {
            foreach (Match match in GetMatches(value, regexPattern, options))
            {
                if (match.Success) yield return match.Value;
            }
        }

        /// <summary>
        ///     Converts the numeric Unicode character at the specified position in a specified string to a double-precision
        ///     floating point number.
        /// </summary>
        /// <param name="s">A .</param>
        /// <param name="index">The character position in .</param>
        /// <returns>
        ///     The numeric value of the character at position  in  if that character represents a number; otherwise, -1.
        /// </returns>
        public static Double GetNumericValue(this String s, Int32 index)
        {
            return Char.GetNumericValue(s, index);
        }

        private static string GetRegexPattern(string template, ComparsionTemplateOptions compareTemplateOptions)
        {
            template = template.ReplaceAll(ReservedRegexOperators, v => @"\" + v);

            var comparingFromStart = compareTemplateOptions == ComparsionTemplateOptions.FromStart ||
                                     compareTemplateOptions == ComparsionTemplateOptions.Whole;
            var comparingAtTheEnd = compareTemplateOptions == ComparsionTemplateOptions.AtTheEnd ||
                                    compareTemplateOptions == ComparsionTemplateOptions.Whole;
            var pattern = new StringBuilder();

            if (comparingFromStart)
                pattern.Append("^");

            pattern.Append(Regex.Replace(template, @"\{[0-9]+\}",
                delegate (Match m)
                {
                    var argNum = m.ToString().Replace("{", "").Replace("}", "");
                    return string.Format("(?<{0}>.*?)", int.Parse(argNum) + 1);
                }
            ));

            if (comparingAtTheEnd ||
                (template.LastOrDefault() == '}' && compareTemplateOptions == ComparsionTemplateOptions.Default))
                pattern.Append("$");

            return pattern.ToString();
        }

        /// <summary>
        ///     Categorizes the character at the specified position in a specified string into a group identified by one of
        ///     the  values.
        /// </summary>
        /// <param name="s">A .</param>
        /// <param name="index">The character position in .</param>
        /// <returns>A  enumerated constant that identifies the group that contains the character at position  in .</returns>
        public static UnicodeCategory GetUnicodeCategory(this string s, Int32 index)
        {
            return CharUnicodeInfo.GetUnicodeCategory(s, index);
        }

        /// <summary>
        ///     Gets the nth "word" of a given string, where "words" are substrings separated by a given separator
        /// </summary>
        /// <param name="value">The string from which the word should be retrieved.</param>
        /// <param name="index">Index of the word (0-based).</param>
        /// <returns>
        ///     The word at position n of the string.
        ///     Trying to retrieve a word at a position lower than 0 or at a position where no word exists results in an exception.
        /// </returns>
        /// <remarks>
        ///     Originally contributed by MMathews
        /// </remarks>
        public static string GetWordByIndex(this string value, int index)
        {
            var words = value.GetWords();

            if ((index < 0) || (index > words.Length - 1))
                throw new IndexOutOfRangeException("The word number is out of range.");

            return words[index];
        }

        public static List<string> GetWords(this string input, string[] wordSeparators)
        {
            var ws = wordSeparators == null || wordSeparators.Length == 0 ? new[] { " " } : wordSeparators;
            return input.Split(ws, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        /// <summary>
        ///     Splits the given string into words and returns a string array.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>The splitted string array</returns>
        public static string[] GetWords(this string value)
        {
            return value.Split(@"\W");
        }

        public static bool HasNumber(this string input)
        {
            return input.Where(Char.IsDigit).Any();
        }

        public static bool HasSpecialChar(this string input)
        {
            return input.Any(ch => !Char.IsLetterOrDigit(ch));
        }

        /// <summary>
        ///     Convert this string containing hexadecimal into a byte array.
        /// </summary>
        /// <param name="str">The hexadecimal string to convert.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static byte[] HexStringToBytes(this string str)
        {
            str = str.Replace(" ", "");
            var max_byte = str.Length / 2 - 1;
            var bytes = new byte[max_byte + 1];
            for (var i = 0; i <= max_byte; i++)
            {
                bytes[i] = byte.Parse(str.Substring(2 * i, 2), NumberStyles.AllowHexSpecifier);
            }

            return bytes;
        }

        /// <summary>
        ///     A string extension method that if empty.
        /// </summary>
        /// <param name="value">The value to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A string.</returns>
        public static string IfEmpty(this string value, string defaultValue)
        {
            return (value.IsNotEmpty() ? value : defaultValue);
        }

        /// <summary>
        ///     Checks whether the string is null, empty or consists only of white-space characters and returns a default
        ///     value in case
        /// </summary>
        /// <param name="value">The string to check</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>Either the string or the default value</returns>
        public static string IfEmptyOrWhiteSpace(this string value, string defaultValue)
        {
            return value.IsEmptyOrWhiteSpace() ? defaultValue : value;
        }

        /// <summary>
        ///     A T extension method to determines whether the object is equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list contains the object, else false.</returns>
        public static bool In(this String @this, params String[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        /// <summary>
        /// Determines whether the specified eval string contains only alpha characters.
        /// </summary>
        /// <param name="evalString">The eval string.</param>
        /// <returns>
        /// 	<c>true</c> if the specified eval string is alpha; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlphabetic(this string evalString)
        {
            return !Regex.IsMatch(evalString, RegexPattern.ALPHABETIC);
        }

        /// <summary>
        /// Determines whether the specified eval string contains only alphanumeric characters
        /// </summary>
        /// <param name="evalString">The eval string.</param>
        /// <returns>
        /// 	<c>true</c> if the string is alphanumeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlphabeticNumeric(this string evalString)
        {
            return !Regex.IsMatch(evalString, RegexPattern.ALPHABETIC_NUMERIC);
        }

        /// <summary>
        /// Determines whether the specified eval string contains only alphanumeric characters
        /// </summary>
        /// <param name="evalString">The eval string.</param>
        /// <param name="allowSpaces">if set to <c>true</c> [allow spaces].</param>
        /// <returns>
        /// 	<c>true</c> if the string is alphanumeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlphabeticNumeric(this string evalString, bool allowSpaces)
        {
            if (allowSpaces)
                return !Regex.IsMatch(evalString, RegexPattern.ALPHABETIC_NUMERIC_SPACE);
            return IsAlphaNumeric(evalString);
        }

        /// <summary>
        ///     A string extension method that query if '@this' is Alphanumeric.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if Alphanumeric, false if not.</returns>
        public static bool IsAlphaNumeric(this string @this)
        {
            return !Regex.IsMatch(@this, "[^a-zA-Z0-9]");
        }

        /// <summary>
        ///     A string extension method that query if '@this' is anagram of other String.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="otherString">The other string</param>
        /// <returns>true if the @this is anagram of the otherString, false if not.</returns>
        public static bool IsAnagram(this string @this, string otherString)
        {
            return @this
                .OrderBy(c => c)
                .SequenceEqual(otherString.OrderBy(c => c));
        }

        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a control
        ///     character.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a control character; otherwise, false.</returns>
        public static Boolean IsControl(this String s, Int32 index)
        {
            return Char.IsControl(s, index);
        }

        public static bool IsDate(this string sdate)
        {
            bool isDate = true;

            try
            {
                DateTime.Parse(sdate);
            }
            catch
            {
                isDate = false;
            }

            return isDate;
        }

        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a decimal
        ///     digit.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a decimal digit; otherwise, false.</returns>
        public static Boolean IsDigit(this String s, Int32 index)
        {
            return Char.IsDigit(s, index);
        }

        /// <summary>
        /// Determines whether the specified email address string is valid based on regular expression evaluation.
        /// </summary>
        /// <param name="emailAddressString">The email address string.</param>
        /// <returns>
        /// 	<c>true</c> if the specified email address is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmail(this string emailAddressString)
        {
            return Regex.IsMatch(emailAddressString, RegexPattern.EMAIL);
        }

        /// <summary>
        ///     A string extension method that query if '@this' is empty.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if empty, false if not.</returns>
        public static bool IsEmpty(this string @this)
        {
            return @this == "";
        }

        /// <summary>Finds out if the specified string contains null, empty or consists only of white-space characters</summary>
        /// <param name="value">The input string</param>
        public static bool IsEmptyOrWhiteSpace(this string value)
        {
            return value.IsEmpty() || value.All(char.IsWhiteSpace);
        }

        /// <summary>
        /// Determines whether the specified string is a valid GUID.
        /// </summary>
        /// <param name="guid">The GUID.</param>
        /// <returns>
        /// 	<c>true</c> if the specified string is a valid GUID; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGuid(this string guid)
        {
            return Regex.IsMatch(guid, RegexPattern.GUID);
        }

        /// <summary>
        ///     Indicates whether the  object at the specified position in a string is a high surrogate.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>
        ///     true if the numeric value of the specified character in the  parameter ranges from U+D800 through U+DBFF;
        ///     otherwise, false.
        /// </returns>
        public static Boolean IsHighSurrogate(this String s, Int32 index)
        {
            return Char.IsHighSurrogate(s, index);
        }

        /// <summary>
        /// Determines whether the specified string is a valid IP address.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsIpAddress(this string ipAddress)
        {
            return Regex.IsMatch(ipAddress, RegexPattern.IP_ADDRESS);
        }

        /// <summary>
        ///     To check whether the data is defined in the given enum.
        /// </summary>
        /// <typeparam name="TEnum">The enum will use to check, the data defined.</typeparam>
        /// <param name="dataToCheck">To match against enum.</param>
        /// <returns>Anonoymous method for the condition.</returns>
        /// <remarks>
        ///     Contributed by Mohammad Rahman, http://mohammad-rahman.blogspot.com/
        /// </remarks>
        public static Func<bool> IsItemInEnum<TEnum>(this string dataToCheck)
                                                            where TEnum : struct
        {
            return () => string.IsNullOrEmpty(dataToCheck) || !Enum.IsDefined(typeof(TEnum), dataToCheck);

        }

        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a Unicode
        ///     letter.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a letter; otherwise, false.</returns>
        public static Boolean IsLetter(this String s, Int32 index)
        {
            return Char.IsLetter(s, index);
        }

        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a letter or
        ///     a decimal digit.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a letter or a decimal digit; otherwise, false.</returns>
        public static Boolean IsLetterOrDigit(this String s, Int32 index)
        {
            return Char.IsLetterOrDigit(s, index);
        }

        /// <summary>
        ///     A string extension method that query if '@this' satisfy the specified pattern.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="pattern">The pattern to use. Use '*' as wildcard string.</param>
        /// <returns>true if '@this' satisfy the specified pattern, false if not.</returns>
        public static bool IsLike(this string @this, string pattern)
        {
            // Turn the pattern into regex pattern, and match the whole string with ^$
            string regexPattern = "^" + Regex.Escape(pattern) + "$";

            // Escape special character ?, #, *, [], and [!]
            regexPattern = regexPattern.Replace(@"\[!", "[^")
                .Replace(@"\[", "[")
                .Replace(@"\]", "]")
                .Replace(@"\?", ".")
                .Replace(@"\*", ".*")
                .Replace(@"\#", @"\d");

            return Regex.IsMatch(@this, regexPattern);
        }

        /// <summary>
        ///     Wildcard comparison for any pattern
        /// </summary>
        /// <param name="value">The current <see cref="System.String" /> object</param>
        /// <param name="patterns">The array of string patterns</param>
        /// <returns></returns>
        public static bool IsLikeAny(this string value, params string[] patterns)
        {
            return patterns.Any(value.IsLike);
        }

        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a lowercase
        ///     letter.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a lowercase letter; otherwise, false.</returns>
        public static Boolean IsLower(this String s, Int32 index)
        {
            return Char.IsLower(s, index);
        }

        /// <summary>
        /// Determines whether the specified string is lower case.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>
        /// 	<c>true</c> if the specified string is lower case; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLowerCase(this string inputString)
        {
            return Regex.IsMatch(inputString, RegexPattern.LOWER_CASE);
        }

        /// <summary>
        ///     Indicates whether the  object at the specified position in a string is a low surrogate.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>
        ///     true if the numeric value of the specified character in the  parameter ranges from U+DC00 through U+DFFF;
        ///     otherwise, false.
        /// </returns>
        public static Boolean IsLowSurrogate(this String s, Int32 index)
        {
            return Char.IsLowSurrogate(s, index);
        }

        /// <summary>
        ///     Indicates whether the specified regular expression finds a match in the specified input string.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>true if the regular expression finds a match; otherwise, false.</returns>
        public static Boolean IsMatch(this String input, String pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        ///     Indicates whether the specified regular expression finds a match in the specified input string, using the
        ///     specified matching options.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
        /// <returns>true if the regular expression finds a match; otherwise, false.</returns>
        public static Boolean IsMatch(this String input, String pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }

        /// <summary>
        ///     Uses regular expressions to determine if the string matches to a given regex pattern.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <returns>
        ///     <c>true</c> if the value is matching to the specified pattern; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        ///     <code>
        /// 		var s = "12345";
        /// 		var isMatching = s.IsMatchingTo(@"^\d+$");
        /// 	</code>
        /// </example>
        public static bool IsMatchingTo(this string value, string regexPattern)
        {
            return IsMatchingTo(value, regexPattern, RegexOptions.None);
        }

        /// <summary>
        ///     Uses regular expressions to determine if the string matches to a given regex pattern.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <param name="options">The regular expression options.</param>
        /// <returns>
        ///     <c>true</c> if the value is matching to the specified pattern; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        ///     <code>
        /// 		var s = "12345";
        /// 		var isMatching = s.IsMatchingTo(@"^\d+$");
        /// 	</code>
        /// </example>
        public static bool IsMatchingTo(this string value, string regexPattern, RegexOptions options)
        {
            return Regex.IsMatch(value, regexPattern, options);
        }

        /// <summary>
        ///     A string extension method that queries if a not is empty.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if a not is empty, false if not.</returns>
        public static bool IsNotEmpty(this string @this)
        {
            return @this != "";
        }

        /// <summary>Determines whether the specified string is not null, empty or consists only of white-space characters</summary>
        /// <param name="value">The string value to check</param>
        public static bool IsNotEmptyOrWhiteSpace(this string value)
        {
            return value.IsEmptyOrWhiteSpace() == false;
        }

        /// <summary>
        ///     A T extension method that query if '@this' is not null.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if not null, false if not.</returns>
        public static bool IsNotNull(this String @this)
        {
            return @this != null;
        }

        /// <summary>
        ///     A string extension method that queries if '@this' is not (null or empty).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if '@this' is not (null or empty), false if not.</returns>
        public static bool IsNotNullOrEmpty(this string @this)
        {
            return !string.IsNullOrEmpty(@this);
        }

        /// <summary>
        ///     Indicates whether a specified string is not null, not empty, or not consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the  parameter is null or , or if  consists exclusively of white-space characters.</returns>
        public static Boolean IsNotNullOrWhiteSpace(this string value)
        {
            return !String.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        ///     A T extension method that query if '@this' is null.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if null, false if not.</returns>
        public static bool IsNull(this String @this)
        {
            return @this == null;
        }

        /// <summary>
        ///     A string extension method that queries if '@this' is null or is empty.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if '@this' is null or is empty, false if not.</returns>
        public static bool IsNullOrEmpty(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }

        /// <summary>
        ///     Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the  parameter is null or , or if  consists exclusively of white-space characters.</returns>
        public static Boolean IsNullOrWhiteSpace(this String value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a number.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a number; otherwise, false.</returns>
        public static Boolean IsNumber(this String s, Int32 index)
        {
            return Char.IsNumber(s, index);
        }

        /// <summary>
        /// Determines whether the specified eval string contains only numeric characters
        /// </summary>
        /// <param name="evalString">The eval string.</param>
        /// <returns>
        /// 	<c>true</c> if the string is numeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumeric(this string evalString)
        {
            return !Regex.IsMatch(evalString, RegexPattern.NUMERIC);
        }

        /// <summary>A string extension method that query if '@this' is palindrome.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if palindrome, false if not.</returns>
        public static bool IsPalindrome(this string @this)
        {
            // Keep only alphanumeric characters

            var rgx = new Regex("[^a-zA-Z0-9]");
            @this = rgx.Replace(@this, "");
            return @this.SequenceEqual(@this.Reverse());
        }

        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a
        ///     punctuation mark.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a punctuation mark; otherwise, false.</returns>
        public static Boolean IsPunctuation(this String s, Int32 index)
        {
            return Char.IsPunctuation(s, index);
        }

        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a separator
        ///     character.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a separator character; otherwise, false.</returns>
        public static Boolean IsSeparator(this String s, Int32 index)
        {
            return Char.IsSeparator(s, index);
        }

        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string has a surrogate code unit.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>
        ///     true if the character at position  in  is a either a high surrogate or a low surrogate; otherwise, false.
        /// </returns>
        public static Boolean IsSurrogate(this String s, Int32 index)
        {
            return Char.IsSurrogate(s, index);
        }

        /// <summary>
        ///     Indicates whether two adjacent  objects at a specified position in a string form a surrogate pair.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The starting position of the pair of characters to evaluate within .</param>
        /// <returns>
        ///     true if the  parameter includes adjacent characters at positions  and  + 1, and the numeric value of the
        ///     character at position  ranges from U+D800 through U+DBFF, and the numeric value of the character at position
        ///     +1 ranges from U+DC00 through U+DFFF; otherwise, false.
        /// </returns>
        public static Boolean IsSurrogatePair(this String s, Int32 index)
        {
            return Char.IsSurrogatePair(s, index);
        }

        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a symbol
        ///     character.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a symbol character; otherwise, false.</returns>
        public static Boolean IsSymbol(this String s, Int32 index)
        {
            return Char.IsSymbol(s, index);
        }

        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as an
        ///     uppercase letter.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is an uppercase letter; otherwise, false.</returns>
        public static Boolean IsUpper(this String s, Int32 index)
        {
            return Char.IsUpper(s, index);
        }

        /// <summary>
        /// Determines whether the specified string is upper case.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>
        /// 	<c>true</c> if the specified string is upper case; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUpperCase(this string inputString)
        {
            return Regex.IsMatch(inputString, RegexPattern.UPPER_CASE);
        }

        /// <summary>
        /// Determines whether the specified string is a valid URL string using the referenced regex string.
        /// </summary>
        /// <param name="url">The URL string.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUrl(this string url)
        {
            return Regex.IsMatch(url, RegexPattern.URL);
        }

        /// <summary>
        ///     A string extension method that query if 'obj' is valid email.
        /// </summary>
        /// <param name="obj">The obj to act on.</param>
        /// <returns>true if valid email, false if not.</returns>
        public static bool IsValidEmail(this string obj)
        {
            return Regex.IsMatch(obj,
                @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        ///     A string extension method that query if 'obj' is valid IP.
        /// </summary>
        /// <param name="obj">The obj to act on.</param>
        /// <returns>true if valid ip, false if not.</returns>
        public static bool IsValidIP(this string obj)
        {
            return Regex.IsMatch(obj,
                @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
        }

        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as white space.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is white space; otherwise, false.</returns>
        public static Boolean IsWhiteSpace(this String s, Int32 index)
        {
            return Char.IsWhiteSpace(s, index);
        }

        /// <summary>
        ///     Concatenates all the elements of a string array, using the specified separator between each element.
        /// </summary>
        /// <param name="separator">
        ///     The string to use as a separator.  is included in the returned string only if  has more
        ///     than one element.
        /// </param>
        /// <param name="value">An array that contains the elements to concatenate.</param>
        /// <returns>
        ///     A string that consists of the elements in  delimited by the  string. If  is an empty array, the method
        ///     returns .
        /// </returns>
        public static String Join(this String separator, String[] value)
        {
            return String.Join(separator, value);
        }

        /// <summary>
        ///     Concatenates the elements of an object array, using the specified separator between each element.
        /// </summary>
        /// <param name="separator">
        ///     The string to use as a separator.  is included in the returned string only if  has more
        ///     than one element.
        /// </param>
        /// <param name="values">An array that contains the elements to concatenate.</param>
        /// <returns>
        ///     A string that consists of the elements of  delimited by the  string. If  is an empty array, the method
        ///     returns .
        /// </returns>
        public static String Join(this String separator, Object[] values)
        {
            return String.Join(separator, values);
        }

        /// <summary>
        ///     A String extension method that joins.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="separator">
        ///     The string to use as a separator.  is included in the returned string only if  has more
        ///     than one element.
        /// </param>
        /// <param name="values">An array that contains the elements to concatenate.</param>
        /// <returns>A String.</returns>
        public static String Join<T>(this String separator, IEnumerable<T> values)
        {
            return String.Join(separator, values);
        }

        /// <summary>
        ///     Concatenates all the elements of a string array, using the specified separator between each element.
        /// </summary>
        /// <param name="separator">
        ///     The string to use as a separator.  is included in the returned string only if  has more
        ///     than one element.
        /// </param>
        /// <param name="values">An array that contains the elements to concatenate.</param>
        /// <returns>
        ///     A string that consists of the elements in  delimited by the  string. If  is an empty array, the method
        ///     returns .
        /// </returns>
        public static String Join(this String separator, IEnumerable<string> values)
        {
            return String.Join(separator, values);
        }

        /// <summary>
        ///     Concatenates the specified elements of a string array, using the specified separator between each element.
        /// </summary>
        /// <param name="separator">
        ///     The string to use as a separator.  is included in the returned string only if  has more
        ///     than one element.
        /// </param>
        /// <param name="value">An array that contains the elements to concatenate.</param>
        /// <param name="startIndex">The first element in  to use.</param>
        /// <param name="count">The number of elements of  to use.</param>
        /// <returns>
        ///     A string that consists of the strings in  delimited by the  string. -or- if  is zero,  has no elements, or
        ///     and all the elements of  are .
        /// </returns>
        public static String Join(this String separator, String[] value, Int32 startIndex,
                                                            Int32 count)
        {
            return String.Join(separator, value, startIndex, count);
        }

        /// <summary>
        ///     A string extension method that return the left part of the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="length">The length.</param>
        /// <returns>The left part.</returns>
        public static string Left(this string @this, int length)
        {
            return @this.Substring(0, length);
        }

        /// <summary>
        ///     A string extension method that left safe.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="length">The length.</param>
        /// <returns>A string.</returns>
        public static string LeftSafe(this string @this, int length)
        {
            return @this.Substring(0, Math.Min(length, @this.Length));
        }

        /// <summary>
        ///     Searches the specified input string for the first occurrence of the specified regular expression.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>An object that contains information about the match.</returns>
        public static Match Match(this String input, String pattern)
        {
            return Regex.Match(input, pattern);
        }

        /// <summary>
        ///     Searches the input string for the first occurrence of the specified regular expression, using the specified
        ///     matching options.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
        /// <returns>An object that contains information about the match.</returns>
        public static Match Match(this String input, String pattern, RegexOptions options)
        {
            return Regex.Match(input, pattern, options);
        }

        public static bool Matches(this string source, string compare)
        {
            return String.Equals(source, compare, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        ///     Searches the specified input string for all occurrences of a specified regular expression, using the
        ///     specified matching options.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
        /// <returns>
        ///     A collection of the  objects found by the search. If no matches are found, the method returns an empty
        ///     collection object.
        /// </returns>
        public static MatchCollection Matches(this String input, String pattern, RegexOptions options)
        {
            return Regex.Matches(input, pattern, options);
        }

        public static bool MatchesRegex(this string inputString, string matchPattern)
        {
            return Regex.IsMatch(inputString, matchPattern,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        }

        public static bool MatchesTrimmed(this string source, string compare)
        {
            return String.Equals(source.Trim(), compare.Trim(), StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        ///     A string extension method that newline 2 line break.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string Nl2Br(this string @this)
        {
            return @this.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }

        /// <summary>
        ///     A T extension method to determines whether the object is not equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list doesn't contains the object, else false.</returns>
        public static bool NotIn(this String @this, params String[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }

        /// <summary>
        ///     A string extension method that return null if the value is empty else the value.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>null if the value is empty, otherwise the value.</returns>
        public static string NullIfEmpty(this string @this)
        {
            return @this == "" ? null : @this;
        }

        /// <summary>
        ///     Centers a charters in this string, padding in both, left and right, by specified Unicode character,
        ///     for a specified total lenght.
        /// </summary>
        /// <param name="value">Instance value.</param>
        /// <param name="width">
        ///     The number of characters in the resulting string,
        ///     equal to the number of original characters plus any additional padding characters.
        /// </param>
        /// <param name="padChar">A Unicode padding character.</param>
        /// <param name="truncate">
        ///     Should get only the substring of specified width if string width is
        ///     more than the specified width.
        /// </param>
        /// <returns>
        ///     A new string that is equivalent to this instance,
        ///     but center-aligned with as many paddingChar characters as needed to create a
        ///     length of width paramether.
        /// </returns>
        public static string PadBoth(this string value, int width, char padChar, bool truncate = false)
        {
            var diff = width - value.Length;
            if (diff == 0 || diff < 0 && !truncate)
            {
                return value;
            }
            if (diff < 0)
            {
                return value.Substring(0, width);
            }
            return value.PadLeft(width - diff / 2, padChar).PadRight(width, padChar);
        }

        public static T Parse<T>(this string value)
        {
            // Get default value for type so if string
            // is empty then we can return default value.
            var result = default(T);
            if (!string.IsNullOrEmpty(value))
            {
                // we are not going to handle exception here
                // if you need SafeParse then you should create
                // another method specially for that.
                var tc = TypeDescriptor.GetConverter(typeof(T));
                result = (T)tc.ConvertFrom(value);
            }
            return result;
        }

        /// <summary>
        ///     Parse a string to a enum item if that string exists in the enum otherwise return the default enum item.
        /// </summary>
        /// <typeparam name="TEnum">The Enum type.</typeparam>
        /// <param name="dataToMatch">The data will use to convert into give enum</param>
        /// <param name="ignorecase">Whether the enum parser will ignore the given data's case or not.</param>
        /// <returns>Converted enum.</returns>
        /// <example>
        ///     <code>
        /// 		public enum EnumTwo {  None, One,}
        /// 		object[] items = new object[] { "One".ParseStringToEnum<EnumTwo>(), "Two".ParseStringToEnum<EnumTwo>() };
        /// 	</code>
        /// </example>
        /// <remarks>
        ///     Contributed by Mohammad Rahman, http://mohammad-rahman.blogspot.com/
        /// </remarks>
        public static TEnum ParseStringToEnum<TEnum>(this string dataToMatch, bool ignorecase = default(bool))
                                                            where TEnum : struct
        {
            return dataToMatch.IsItemInEnum<TEnum>()()
                ? default(TEnum)
                : (TEnum)Enum.Parse(typeof(TEnum), dataToMatch, default(bool));
        }

        /// <summary>
        ///     Combines multiples string into a path.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="paths">A variable-length parameters list containing paths.</param>
        /// <returns>
        ///     The combined paths. If one of the specified paths is a zero-length string, this method returns the other path.
        /// </returns>
        public static string PathCombine(this string @this, params string[] paths)
        {
            List<string> list = paths.ToList();
            list.Insert(0, @this);
            return Path.Combine(list.ToArray());
        }

        public static string Remove(this string text, int index, int length)
        {
            var s1 = text.Substring(0, index);
            var s2 = text.Substring(index + length);
            return s1 + s2;
        }

        /// <summary>
        ///     Removes characters that satisfy the given condition
        /// </summary>
        /// <param name="value"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string Remove(this string value, Func<char, bool> condition)
        {
            return value.Extract(v => !condition(v));
        }

        /// <summary>
        ///     Remove any instance of the given character from the current string.
        /// </summary>
        /// <param name="value">
        ///     The input.
        /// </param>
        /// <param name="removeCharc">
        ///     The remove char.
        /// </param>
        /// <remarks>
        ///     Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static string Remove(this string value, params char[] removeCharc)
        {
            var result = value;
            if (!string.IsNullOrEmpty(result) && removeCharc != null)
                foreach (var c in removeCharc)
                {
                    result.Remove(c.ToString());
                }
            return result;
        }

        /// <summary>
        ///     Remove any instance of the given string pattern from the current string.
        /// </summary>
        /// <param name="value">The input.</param>
        /// <param name="strings">The strings.</param>
        /// <returns></returns>
        /// <remarks>
        ///     Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static string Remove(this string value, params string[] strings)
        {
            return strings.Aggregate(value, (current, c) => current.Replace(c, string.Empty));
        }

        /// <summary>
        ///     Strips out all instances of the specified string from the source string
        /// </summary>
        /// <param name="source">The source string</param>
        /// <returns>The stripped string</returns>
        public static string RemoveAll(this string source, params string[] removeStrings)
        {
            var v = source;
            foreach (var s in removeStrings)
            {
                v = v.Replace(s, string.Empty);
            }
            return v;
        }

        /// <summary>
        ///     Removed all special characters from the string.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>The adjusted string.</returns>
        /// <remarks>
        ///     Contributed by Michael T, http://about.me/MichaelTran, James C, http://www.noveltheory.com
        ///     This implementation is roughly equal to the original in speed, and should be more robust, internationally.
        /// </remarks>
        public static string RemoveAllSpecialCharacters(this string value)
        {
            var sb = new StringBuilder(value.Length);
            foreach (var c in value.Where(char.IsLetterOrDigit))
                sb.Append(c);
            return sb.ToString();
        }

        public static string RemoveControlCharacters(this string input)
        {
            return
                input.Where(character => !char.IsControl(character))
                    .Aggregate(new StringBuilder(), (builder, character) => builder.Append(character))
                    .ToString();
        }

        /// <summary>
        ///     A string extension method that removes the diacritics character from the strings.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The string without diacritics character.</returns>
        public static string RemoveDiacritics(this string @this)
        {
            string normalizedString = @this.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char t in normalizedString)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(t);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(t);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string RemoveFirst(this string instr, int number)
        {
            return instr.Substring(number);
        }

        public static string RemoveFirstCharacter(this string text, Func<char, bool> predicate)
        {
            if (!string.IsNullOrEmpty(text) && text.Length > 1)
            {
                if (predicate != null && predicate(text[0]))
                {
                    return RemoveFirstCharacter(text);
                }
            }
            return text;
        }

        public static string RemoveFirstCharacter(this string instr)
        {
            return instr.Substring(1);
        }

        public static string RemoveHtmlTags(this string htmlString)
        {
            var noHtml = Regex.Replace(htmlString, @"<[^>]*(>|$)|&nbsp;|&zwnj;|&raquo;|&laquo;", string.Empty).Trim();
            return noHtml;
        }

        public static string RemoveLast(this string instr, int number)
        {
            return instr.Substring(0, instr.Length - number);
        }

        public static string RemoveLastCharacter(this string text, Func<char, bool> predicate)
        {
            if (!string.IsNullOrEmpty(text) && text.Length > 1)
            {
                if (predicate != null && predicate(text[text.Length - 1]))
                {
                    return RemoveLastCharacter(text);
                }
            }
            return text;
        }

        public static string RemoveLastCharacter(this string instr)
        {
            return instr.Substring(0, instr.Length - 1);
        }

        /// <summary>
        ///     Return the string with the leftmost number_of_characters characters removed.
        /// </summary>
        /// <param name="str">The string being extended</param>
        /// <param name="number_of_characters">The number of characters to remove.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string RemoveLeft(this string str, int number_of_characters)
        {
            if (str.Length <= number_of_characters) return "";
            return str.Substring(number_of_characters);
        }

        /// <summary>
        ///     A string extension method that removes the letter described by @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string RemoveLetter(this string @this)
        {
            return new string(@this.ToCharArray().Where(x => !Char.IsLetter(x)).ToArray());
        }

        /// <summary>
        ///     A string extension method that removes the number described by @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string RemoveNumber(this string @this)
        {
            return new string(@this.ToCharArray().Where(x => !Char.IsNumber(x)).ToArray());
        }

        /// <summary>
        ///     Return the string with the rightmost number_of_characters characters removed.
        /// </summary>
        /// <param name="str">The string being extended</param>
        /// <param name="number_of_characters">The number of characters to remove.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string RemoveRight(this string str, int number_of_characters)
        {
            if (str.Length <= number_of_characters) return "";
            return str.Substring(0, str.Length - number_of_characters);
        }

        /// <summary>
        ///     A string extension method that removes the letter.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A string.</returns>
        public static string RemoveWhere(this string @this, Func<char, bool> predicate)
        {
            return new string(@this.ToCharArray().Where(x => !predicate(x)).ToArray());
        }

        public static string RemoveWhiteSpaces(this string input)
        {
            return Regex.Replace(input, @"\s+", "");
        }

        /// <summary>
        ///     A string extension method that repeats the string a specified number of times.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="repeatCount">Number of repeats.</param>
        /// <returns>The repeated string.</returns>
        public static string Repeat(this string @this, int repeatCount)
        {
            if (@this.Length == 1)
            {
                return new string(@this[0], repeatCount);
            }

            var sb = new StringBuilder(repeatCount * @this.Length);
            while (repeatCount-- > 0)
            {
                sb.Append(@this);
            }

            return sb.ToString();
        }

        public static string Repeat(this string text, string separator, int count)
        {
            if (count <= 0)
                return "";
            var list = new List<string>();
            for (int i = 0; i < count; i++)
                list.Add(text);
            return list.Aggregate((a, b) => a + separator + b);
        }

        /// <summary>A string extension method that replaces.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length.</param>
        /// <param name="value">The value.</param>
        /// <returns>A string.</returns>
        public static string Replace(this string @this, int startIndex, int length, string value)
        {
            @this = @this.Remove(startIndex, length).Insert(startIndex, value);

            return @this;
        }

        /// <summary>
        ///     Replace all values in string
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="oldValues">List of old values, which must be replaced</param>
        /// <param name="replacePredicate">Function for replacement old values</param>
        /// <returns>Returns new string with the replaced values</returns>
        /// <example>
        ///     <code>
        ///         var str = "White Red Blue Green Yellow Black Gray";
        ///         var achromaticColors = new[] {"White", "Black", "Gray"};
        ///         str = str.ReplaceAll(achromaticColors, v => "[" + v + "]");
        ///         // str == "[White] Red Blue Green Yellow [Black] [Gray]"
        /// 	</code>
        /// </example>
        /// <remarks>
        ///     Contributed by nagits, http://about.me/AlekseyNagovitsyn
        /// </remarks>
        public static string ReplaceAll(this string value, IEnumerable<string> oldValues,
                                                            Func<string, string> replacePredicate)
        {
            var sbStr = new StringBuilder(value);
            foreach (var oldValue in oldValues)
            {
                var newValue = replacePredicate(oldValue);
                sbStr.Replace(oldValue, newValue);
            }

            return sbStr.ToString();
        }

        /// <summary>
        ///     Replace all values in string
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="oldValues">List of old values, which must be replaced</param>
        /// <param name="newValue">New value for all old values</param>
        /// <returns>Returns new string with the replaced values</returns>
        /// <example>
        ///     <code>
        ///         var str = "White Red Blue Green Yellow Black Gray";
        ///         var achromaticColors = new[] {"White", "Black", "Gray"};
        ///         str = str.ReplaceAll(achromaticColors, "[AchromaticColor]");
        ///         // str == "[AchromaticColor] Red Blue Green Yellow [AchromaticColor] [AchromaticColor]"
        /// 	</code>
        /// </example>
        /// <remarks>
        ///     Contributed by nagits, http://about.me/AlekseyNagovitsyn
        /// </remarks>
        public static string ReplaceAll(this string value, IEnumerable<string> oldValues, string newValue)
        {
            var sbStr = new StringBuilder(value);
            foreach (var oldValue in oldValues)
                sbStr.Replace(oldValue, newValue);

            return sbStr.ToString();
        }

        /// <summary>
        ///     Replace all values in string
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="oldValues">List of old values, which must be replaced</param>
        /// <param name="newValues">List of new values</param>
        /// <returns>Returns new string with the replaced values</returns>
        /// <example>
        ///     <code>
        ///         var str = "White Red Blue Green Yellow Black Gray";
        ///         var achromaticColors = new[] {"White", "Black", "Gray"};
        ///         var exquisiteColors = new[] {"FloralWhite", "Bistre", "DavyGrey"};
        ///         str = str.ReplaceAll(achromaticColors, exquisiteColors);
        ///         // str == "FloralWhite Red Blue Green Yellow Bistre DavyGrey"
        /// 	</code>
        /// </example>
        /// <remarks>
        ///     Contributed by nagits, http://about.me/AlekseyNagovitsyn
        /// </remarks>
        public static string ReplaceAll(this string value, IEnumerable<string> oldValues, IEnumerable<string> newValues)
        {
            var sbStr = new StringBuilder(value);
            var newValueEnum = newValues.GetEnumerator();
            foreach (var old in oldValues)
            {
                if (!newValueEnum.MoveNext())
                    throw new ArgumentOutOfRangeException(nameof(newValues),
                        "newValues sequence is shorter than oldValues sequence");
                sbStr.Replace(old, newValueEnum.Current);
            }
            if (newValueEnum.MoveNext())
                throw new ArgumentOutOfRangeException(nameof(newValues),
                    "newValues sequence is longer than oldValues sequence");

            return sbStr.ToString();
        }

        public static string ReplaceAt(this string input, int index, char newChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }

        public static string ReplaceAt(this string str, int index, string replace)
        {
            var length = replace.Length;
            return str.Remove(index, Math.Min(length, str.Length - index))
                .Insert(index, replace);
        }

        public static string ReplaceAt(this string str, int index, int length, string replace)
        {
            return str.Remove(index, Math.Min(length, str.Length - index))
                .Insert(index, replace);
        }

        public static string ReplaceAt(this string str, int startIndex, long endIndex, string replace)
        {
            var length = endIndex - startIndex;
            return str.Remove(startIndex, Math.Min(Convert.ToInt32(length), str.Length - startIndex))
                .Insert(startIndex, replace);
        }

        /// <summary>
        ///     A string extension method that replace all values specified by an empty string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>A string with all specified values replaced by an empty string.</returns>
        public static string ReplaceByEmpty(this string @this, params string[] values)
        {
            foreach (string value in values)
            {
                @this = @this.Replace(value, "");
            }

            return @this;
        }

        /// <summary>
        ///     A string extension method that replace first occurence.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the first occurence of old value replace by new value.</returns>
        public static string ReplaceFirst(this string @this, string oldValue, string newValue)
        {
            int startindex = @this.IndexOf(oldValue, StringComparison.Ordinal);

            if (startindex == -1)
            {
                return @this;
            }

            return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
        }

        /// <summary>
        ///     A string extension method that replace first number of occurences.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="number">Number of.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the numbers of occurences of old value replace by new value.</returns>
        public static string ReplaceFirst(this string @this, int number, string oldValue, string newValue)
        {
            List<string> list = @this.Split(oldValue).ToList();
            int old = number + 1;
            IEnumerable<string> listStart = list.Take(old);
            IEnumerable<string> listEnd = list.Skip(old);

            return string.Join(newValue, listStart) +
                   (listEnd.Any() ? oldValue : "") +
                   string.Join(oldValue, listEnd);
        }

        /// <summary>
        ///     A string extension method that replace last occurence.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the last occurence of old value replace by new value.</returns>
        public static string ReplaceLast(this string @this, string oldValue, string newValue)
        {
            int startindex = @this.LastIndexOf(oldValue, StringComparison.Ordinal);

            if (startindex == -1)
            {
                return @this;
            }

            return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
        }

        /// <summary>
        ///     A string extension method that replace last numbers occurences.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="number">Number of.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the last numbers occurences of old value replace by new value.</returns>
        public static string ReplaceLast(this string @this, int number, string oldValue, string newValue)
        {
            List<string> list = @this.Split(oldValue).ToList();
            int old = Math.Max(0, list.Count - number - 1);
            IEnumerable<string> listStart = list.Take(old);
            IEnumerable<string> listEnd = list.Skip(old);

            return string.Join(oldValue, listStart) +
                   (old > 0 ? oldValue : "") +
                   string.Join(newValue, listEnd);
        }

        /// <summary>
        ///     A string extension method that replace when equals.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The new value if the string equal old value; Otherwise old value.</returns>
        public static string ReplaceWhenEquals(this string @this, string oldValue, string newValue)
        {
            return @this == oldValue ? newValue : @this;
        }

        public static string ReplaceWhiteSpacesWithOne(this string input)
        {
            return Regex.Replace(input, @"\s+", " ");
        }

        /// <summary>
        ///     Uses regular expressions to replace parts of a string.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <param name="replaceValue">The replacement value.</param>
        /// <returns>The newly created string</returns>
        /// <example>
        ///     <code>
        /// 		var s = "12345";
        /// 		var replaced = s.ReplaceWith(@"\d", m => string.Concat(" -", m.Value, "- "));
        /// 	</code>
        /// </example>
        public static string ReplaceWith(this string value, string regexPattern, string replaceValue)
        {
            return ReplaceWith(value, regexPattern, replaceValue, RegexOptions.None);
        }

        /// <summary>
        ///     Uses regular expressions to replace parts of a string.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <param name="replaceValue">The replacement value.</param>
        /// <param name="options">The regular expression options.</param>
        /// <returns>The newly created string</returns>
        /// <example>
        ///     <code>
        /// 		var s = "12345";
        /// 		var replaced = s.ReplaceWith(@"\d", m => string.Concat(" -", m.Value, "- "));
        /// 	</code>
        /// </example>
        public static string ReplaceWith(this string value, string regexPattern, string replaceValue,
                                                            RegexOptions options)
        {
            return Regex.Replace(value, regexPattern, replaceValue, options);
        }

        /// <summary>
        ///     Uses regular expressions to replace parts of a string.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <param name="evaluator">The replacement method / lambda expression.</param>
        /// <returns>The newly created string</returns>
        /// <example>
        ///     <code>
        /// 		var s = "12345";
        /// 		var replaced = s.ReplaceWith(@"\d", m => string.Concat(" -", m.Value, "- "));
        /// 	</code>
        /// </example>
        public static string ReplaceWith(this string value, string regexPattern, MatchEvaluator evaluator)
        {
            return ReplaceWith(value, regexPattern, RegexOptions.None, evaluator);
        }

        /// <summary>
        ///     Uses regular expressions to replace parts of a string.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <param name="options">The regular expression options.</param>
        /// <param name="evaluator">The replacement method / lambda expression.</param>
        /// <returns>The newly created string</returns>
        /// <example>
        ///     <code>
        /// 		var s = "12345";
        /// 		var replaced = s.ReplaceWith(@"\d", m => string.Concat(" -", m.Value, "- "));
        /// 	</code>
        /// </example>
        public static string ReplaceWith(this string value, string regexPattern, RegexOptions options,
                                                            MatchEvaluator evaluator)
        {
            return Regex.Replace(value, regexPattern, evaluator, options);
        }

        /// <summary>
        ///     A string extension method that reverses the given string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The string reversed.</returns>
        public static string Reverse(this string @this)
        {
            if (@this.Length <= 1)
            {
                return @this;
            }

            char[] chars = @this.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        ///     A string extension method that return the right part of the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="length">The length.</param>
        /// <returns>The right part.</returns>
        public static string Right(this string @this, int length)
        {
            return @this.Substring(@this.Length - length);
        }

        /// <summary>
        ///     A string extension method that right safe.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="length">The length.</param>
        /// <returns>A string.</returns>
        public static string RightSafe(this string @this, int length)
        {
            return @this.Substring(Math.Max(0, @this.Length - length));
        }

        public static string SmartFormat(this string format, params Func<string>[] args)
        {
            var objs = new List<object>();
            foreach (var arg in args)
                objs.Add(arg());
            return args.Any() ? string.Format(format, objs.ToArray()) : string.Format(format);
        }

        /// <summary>
        ///     Add space on every upper character
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>The adjusted string.</returns>
        /// <remarks>
        ///     Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static string SpaceOnUpper(this string value)
        {
            return Regex.Replace(value, "([A-Z])(?=[a-z])|(?<=[a-z])([A-Z]|[0-9]+)", " $1$2").TrimStart();
        }

        /// <summary>
        ///     Returns a String array containing the substrings in this string that are delimited by elements of a specified
        ///     String array. A parameter specifies whether to return empty array elements.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="separator">A string that delimit the substrings in this string.</param>
        /// <param name="option">
        ///     (Optional) Specify RemoveEmptyEntries to omit empty array elements from the array returned,
        ///     or None to include empty array elements in the array returned.
        /// </param>
        /// <returns>
        ///     An array whose elements contain the substrings in this string that are delimited by the separator.
        /// </returns>
        public static string[] Split(this string @this, string separator, StringSplitOptions option)
        {
            return @this.Split(new[] { separator }, option);
        }

        public static string[] Split(this string @this, char delimiter, int max)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            return @this.Split(new[] { delimiter }, max);
        }

        public static string[] Split(this string str, int chunkSize)
        {
            return
                Enumerable.Range(0, str.Length / chunkSize).Select(i => str.Substring(i * chunkSize, chunkSize)).ToArray();
        }

        /// <summary>
        ///     Uses regular expressions to split a string into parts.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <returns>The splitted string array</returns>
        public static string[] Split(this string value, string regexPattern)
        {
            return value.Split(regexPattern, RegexOptions.None);
        }

        /// <summary>
        ///     Uses regular expressions to split a string into parts.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <param name="options">The regular expression options.</param>
        /// <returns>The splitted string array</returns>
        public static string[] Split(this string value, string regexPattern, RegexOptions options)
        {
            return Regex.Split(value, regexPattern, options);
        }

        public static SqlDbType SqlTypeNameToSqlDbType(this string @this)
        {
            switch (@this.ToLower())
            {
                case "image": // 34 | "image" | SqlDbType.Image
                    return SqlDbType.Image;

                case "text": // 35 | "text" | SqlDbType.Text
                    return SqlDbType.Text;

                case "uniqueidentifier": // 36 | "uniqueidentifier" | SqlDbType.UniqueIdentifier
                    return SqlDbType.UniqueIdentifier;

                case "date": // 40 | "date" | SqlDbType.Date
                    return SqlDbType.Date;

                case "time": // 41 | "time" | SqlDbType.Time
                    return SqlDbType.Time;

                case "datetime2": // 42 | "datetime2" | SqlDbType.DateTime2
                    return SqlDbType.DateTime2;

                case "datetimeoffset": // 43 | "datetimeoffset" | SqlDbType.DateTimeOffset
                    return SqlDbType.DateTimeOffset;

                case "tinyint": // 48 | "tinyint" | SqlDbType.TinyInt
                    return SqlDbType.TinyInt;

                case "smallint": // 52 | "smallint" | SqlDbType.SmallInt
                    return SqlDbType.SmallInt;

                case "int": // 56 | "int" | SqlDbType.Int
                    return SqlDbType.Int;

                case "smalldatetime": // 58 | "smalldatetime" | SqlDbType.SmallDateTime
                    return SqlDbType.SmallDateTime;

                case "real": // 59 | "real" | SqlDbType.Real
                    return SqlDbType.Real;

                case "money": // 60 | "money" | SqlDbType.Money
                    return SqlDbType.Money;

                case "datetime": // 61 | "datetime" | SqlDbType.DateTime
                    return SqlDbType.DateTime;

                case "float": // 62 | "float" | SqlDbType.Float
                    return SqlDbType.Float;

                case "sql_variant": // 98 | "sql_variant" | SqlDbType.Variant
                    return SqlDbType.Variant;

                case "ntext": // 99 | "ntext" | SqlDbType.NText
                    return SqlDbType.NText;

                case "bit": // 104 | "bit" | SqlDbType.Bit
                    return SqlDbType.Bit;

                case "decimal": // 106 | "decimal" | SqlDbType.Decimal
                    return SqlDbType.Decimal;

                case "numeric": // 108 | "numeric" | SqlDbType.Decimal
                    return SqlDbType.Decimal;

                case "smallmoney": // 122 | "smallmoney" | SqlDbType.SmallMoney
                    return SqlDbType.SmallMoney;

                case "bigint": // 127 | "bigint" | SqlDbType.BigInt
                    return SqlDbType.BigInt;

                case "varbinary": // 165 | "varbinary" | SqlDbType.VarBinary
                    return SqlDbType.VarBinary;

                case "varchar": // 167 | "varchar" | SqlDbType.VarChar
                    return SqlDbType.VarChar;

                case "binary": // 173 | "binary" | SqlDbType.Binary
                    return SqlDbType.Binary;

                case "char": // 175 | "char" | SqlDbType.Char
                    return SqlDbType.Char;

                case "timestamp": // 189 | "timestamp" | SqlDbType.Timestamp
                    return SqlDbType.Timestamp;

                case "nvarchar": // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                    return SqlDbType.NVarChar;
                case "sysname": // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                    return SqlDbType.NVarChar;

                case "nchar": // 239 | "nchar" | SqlDbType.NChar
                    return SqlDbType.NChar;

                case "hierarchyid": // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                    return SqlDbType.Udt;
                case "geometry": // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                    return SqlDbType.Udt;
                case "geography": // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                    return SqlDbType.Udt;

                case "xml": // 241 | "xml" | SqlDbType.Xml
                    return SqlDbType.Xml;

                default:
                    throw new Exception(
                        string.Format(
                            "Unsupported Type: {0}. Please let us know about this type and we will support it: sales@zzzprojects.com",
                            @this));
            }
        }

        public static string Substring(this string text, int startIndex, int endIndex)
        {
            var length = endIndex - startIndex;
            var result = text.Substring(startIndex, length);
            return result;
        }

        /// <summary>Returns the right part of the string from index.</summary>
        /// <param name="value">The original value.</param>
        /// <param name="index">The start index for substringing.</param>
        /// <returns>The right part.</returns>
        public static string SubstringFrom(this string value, int index)
        {
            return index < 0 ? value : value.Substring(index, value.Length - index);
        }

        /// <summary>
        /// Parses a boolean from a string (including "0" and "1")
        /// </summary>
        public static bool ToBoolean(this string @string)
        {
            if (@string == "0")
            {
                return false;
            }
            if (@string == "1")
            {
                return true;
            }
            return bool.Parse(@string);
        }

        /// <summary>
        /// Parses a byte from a string
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static byte ToByte(this string @string)
        {
            return byte.Parse(@string);
        }

        /// <summary>
        ///     Converts the string to a byte-array using the default encoding
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>The created byte array</returns>
        public static byte[] ToByteArray(this string value)
        {
            return value.ToByteArray(null);
        }

        /// <summary>
        ///     Converts the string to a byte-array using the supplied encoding
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="encoding">The encoding to be used.</param>
        /// <returns>The created byte array</returns>
        /// <example>
        ///     <code>
        /// 		var value = "Hello World";
        /// 		var ansiBytes = value.ToBytes(Encoding.GetEncoding(1252)); // 1252 = ANSI
        /// 		var utf8Bytes = value.ToBytes(Encoding.UTF8);
        /// 	</code>
        /// </example>
        public static byte[] ToByteArray(this string value, Encoding encoding)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetBytes(value);
        }

        /// <summary>
        /// Parses a decimal from a string
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string @string)
        {
            return decimal.Parse(@string);
        }

        /// <summary>
        ///     A string extension method that converts the @this to a directory information.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a DirectoryInfo.</returns>
        public static DirectoryInfo ToDirectoryInfo(this string @this)
        {
            return new DirectoryInfo(@this);
        }

        /// <summary>
        /// Parses a double from a string
        /// </summary>
        public static double ToDouble(this string @string)
        {
            return double.Parse(@string);
        }

        public static T ToEnum<T>(this string value, bool ignorecase)
        {
            if (value == null)
                throw new ArgumentNullException("Value");

            value = value.Trim();

            if (value.Length == 0)
                throw new ArgumentNullException("Must specify valid information for parsing in the string.", "value");

            Type t = typeof(T);
            if (!t.IsEnum)
                throw new ArgumentException("Type provided must be an Enum.", "T");

            return (T)Enum.Parse(t, value, ignorecase);
        }

        /// <summary>
        ///     A string extension method that converts the @this to an enum.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a T.</returns>
        public static T ToEnum<T>(this string @this)
        {
            Type enumType = typeof(T);
            return (T)Enum.Parse(enumType, @this);
        }

        /// <summary>
        ///     A string extension method that converts the @this to a file information.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a FileInfo.</returns>
        public static FileInfo ToFileInfo(this string @this)
        {
            return new FileInfo(@this);
        }

        /// <summary>
        /// Parses a float from a string
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static float ToFloat(this string @string)
        {
            return float.Parse(@string);
        }

        /// <summary>
        ///     Convert the provided string to a Guid value.
        /// </summary>
        /// <param name="value">The original string value.</param>
        /// <returns>The Guid</returns>
        public static Guid ToGuid(this string value)
        {
            return new Guid(value);
        }

        /// <summary>
        ///     Convert the provided string to a Guid value and returns Guid.Empty if conversion fails.
        /// </summary>
        /// <param name="value">The original string value.</param>
        /// <returns>The Guid</returns>
        public static Guid ToGuidSave(this string value)
        {
            return value.ToGuidSave(Guid.Empty);
        }

        /// <summary>
        ///     Convert the provided string to a Guid value and returns the provided default value if the conversion fails.
        /// </summary>
        /// <param name="value">The original string value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The Guid</returns>
        public static Guid ToGuidSave(this string value, Guid defaultValue)
        {
            if (value.IsEmpty())
                return defaultValue;

            try
            {
                return value.ToGuid();
            }
            catch
            {
            }

            return defaultValue;
        }

        /// <summary>
        /// Parses an integer from a string
        /// </summary>
        public static int ToInt(this string @string)
        {
            return int.Parse(@string);
        }

        /// <summary>
        /// Parses a long from a string
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static long ToLong(this string @string)
        {
            return long.Parse(@string);
        }

        /// <summary>
        ///     A string extension method that converts the @this to a MemoryStream.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a MemoryStream.</returns>
        public static Stream ToMemoryStream(this string @this)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(@this ?? ""));
        }

        public static Stream ToStream(this string str)
        {
            var byteArray = Encoding.UTF8.GetBytes(str);
            //byte[] byteArray = Encoding.ASCII.GetBytes(str);
            return new MemoryStream(byteArray);
        }

        /// <summary>
        ///     Returns a Stream representation of the String using the specified Encoding.
        /// </summary>
        /// <param name="str">Required. The String to convert to a Stream.</param>
        /// <param name="encoding"></param>
        /// <returns>Returns a Stream representation of the String.</returns>
        public static Stream ToStream(this string str, Encoding encoding)
        {
            return new MemoryStream(str.ToByteArray(encoding));
        }

        public static StringBuilder ToStringBuilder(this string str)
        {
            return new StringBuilder(str);
        }

        /// <summary>Uppercase First Letter</summary>
        /// <param name="value">The string value to process</param>
        public static string ToUpperFirstLetter(this string value)
        {
            if (value.IsEmptyOrWhiteSpace()) return string.Empty;

            var valueChars = value.ToCharArray();
            valueChars[0] = char.ToUpper(valueChars[0]);

            return new string(valueChars);
        }

        /// <summary>
        ///     A string extension method that converts the @this to a XDocument.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an XDocument.</returns>
        public static XDocument ToXDocument(this string @this)
        {
            Encoding encoding = Activator.CreateInstance<UTF8Encoding>();
            using (var ms = new MemoryStream(encoding.GetBytes(@this)))
            {
                return XDocument.Load(ms);
            }
        }

        /// <summary>
        ///     Loads the string into a LINQ to XML XElement
        /// </summary>
        /// <param name="xml">The XML string.</param>
        /// <returns>The XML element object model (XElement)</returns>
        public static XElement ToXElement(this string xml)
        {
            return XElement.Parse(xml);
        }

        /// <summary>
        ///     A string extension method that converts the @this to an XmlDocument.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an XmlDocument.</returns>
        public static XmlDocument ToXmlDocument(this string @this)
        {
            var doc = new XmlDocument();
            doc.LoadXml(@this);
            return doc;
        }

        /// <summary>
        ///     Loads the string into a XML DOM object (XmlDocument)
        /// </summary>
        /// <param name="xml">The XML string.</param>
        /// <returns>The XML document object model (XmlDocument)</returns>
        public static XmlDocument ToXmlDOM(this string xml)
        {
            var document = new XmlDocument();
            document.LoadXml(xml);
            return document;
        }

        public static XmlElement ToXmlElement(this string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlElement root = doc.DocumentElement;
            return root;
        }

        /// <summary>
        ///     Loads the string into a XML XPath DOM (XPathDocument)
        /// </summary>
        /// <param name="xml">The XML string.</param>
        /// <returns>The XML XPath document object model (XPathNavigator)</returns>
        public static XPathNavigator ToXPath(this string xml)
        {
            var document = new XPathDocument(new StringReader(xml));
            return document.CreateNavigator();
        }

        /// <summary>
        /// Removes the specified string value from the current string object at the
        /// beginning and the end.
        /// </summary>
        public static string Trim(this string s, string trimString)
        {
            return s.Trim(trimString.ToCharArray());
        }

        /// <summary>
        ///     Trims the text to a provided maximum length.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="maxLength">Maximum length.</param>
        /// <returns></returns>
        /// <remarks>
        ///     Proposed by Rene Schulte
        /// </remarks>
        public static string TrimToMaxLength(this string value, int maxLength)
        {
            return value == null || value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        /// <summary>
        ///     Trims the text to a provided maximum length and adds a suffix if required.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="maxLength">Maximum length.</param>
        /// <param name="suffix">The suffix.</param>
        /// <returns></returns>
        /// <remarks>
        ///     Proposed by Rene Schulte
        /// </remarks>
        public static string TrimToMaxLength(this string value, int maxLength, string suffix)
        {
            return value == null || value.Length <= maxLength
                ? value
                : string.Concat(value.Substring(0, maxLength), suffix);
        }

        /// <summary>
        ///     A string extension method that truncates.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>A string.</returns>
        public static string Truncate(this string @this, int maxLength)
        {
            const string suffix = "...";

            if (@this == null || @this.Length <= maxLength)
            {
                return @this;
            }

            int strLength = maxLength - suffix.Length;
            return @this.Substring(0, strLength) + suffix;
        }

        /// <summary>
        ///     A string extension method that truncates.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="suffix">The suffix.</param>
        /// <returns>A string.</returns>
        public static string Truncate(this string @this, int maxLength, string suffix)
        {
            if (@this == null || @this.Length <= maxLength)
            {
                return @this;
            }

            int strLength = maxLength - suffix.Length;
            return @this.Substring(0, strLength) + suffix;
        }

        public static T XmlDeserialize<T>(this string xml) where T : class, new()
        {
            if (xml == null) throw new ArgumentNullException(nameof(xml));

            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xml))
            {
                try { return (T)serializer.Deserialize(reader); }
                catch { return null; }
            }
        }

        public static string ReplaceAt(this string text, char target, string replace, ReplaceMode replaceMode = ReplaceMode.On)
        {
            var i = text.IndexOf(target);
            if (i > -1)
            {
                var part1 = text.Substring(0, i);
                var part2 = text.Substring(i + 1);
                switch (replaceMode)
                {
                    case ReplaceMode.On:
                        return part1 + replace + part2;
                    case ReplaceMode.Before:
                        return part1 + replace + target + part2;
                    case ReplaceMode.After:
                        return part1 + target + replace + part2;
                }
                return text;
            }
            else
            {
                return text;
            }

        }
    }

    public enum ReplaceMode
    {
        On,
        Before,
        After
    }
}
