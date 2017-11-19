using System;
using System.Globalization;

namespace CoreExtensions
{
    public static class CharExtensions
    {
        /// <summary>
        ///     Converts the value of a UTF-16 encoded surrogate pair into a Unicode code point.
        /// </summary>
        /// <param name="highSurrogate">A high surrogate code unit (that is, a code unit ranging from U+D800 through U+DBFF).</param>
        /// <param name="lowSurrogate">A low surrogate code unit (that is, a code unit ranging from U+DC00 through U+DFFF).</param>
        /// <returns>The 21-bit Unicode code point represented by the  and  parameters.</returns>
        public static Int32 ConvertToUtf32(this Char highSurrogate, Char lowSurrogate)
        {
            return Char.ConvertToUtf32(highSurrogate, lowSurrogate);
        }

        /// <summary>
        ///     Converts the specified numeric Unicode character to a double-precision floating point number.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <returns>The numeric value of  if that character represents a number; otherwise, -1.0.</returns>
        public static Double GetNumericValue(this Char c)
        {
            return Char.GetNumericValue(c);
        }

        /// <summary>
        ///     Categorizes a specified Unicode character into a group identified by one of the  values.
        /// </summary>
        /// <param name="c">The Unicode character to categorize.</param>
        /// <returns>A  value that identifies the group that contains .</returns>
        public static UnicodeCategory GetUnicodeCategory(this Char c)
        {
            return CharUnicodeInfo.GetUnicodeCategory(c);
        }

        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a control character.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a control character; otherwise, false.</returns>
        public static Boolean IsControl(this Char c)
        {
            return Char.IsControl(c);
        }

        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a decimal digit.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a decimal digit; otherwise, false.</returns>
        public static Boolean IsDigit(this Char c)
        {
            return Char.IsDigit(c);
        }

        /// <summary>
        ///     Indicates whether the specified  object is a high surrogate.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>
        ///     true if the numeric value of the  parameter ranges from U+D800 through U+DBFF; otherwise, false.
        /// </returns>
        public static Boolean IsHighSurrogate(this Char c)
        {
            return Char.IsHighSurrogate(c);
        }

        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a Unicode letter.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a letter; otherwise, false.</returns>
        public static Boolean IsLetter(this Char c)
        {
            return Char.IsLetter(c);
        }

        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a letter or a decimal digit.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a letter or a decimal digit; otherwise, false.</returns>
        public static Boolean IsLetterOrDigit(this Char c)
        {
            return Char.IsLetterOrDigit(c);
        }

        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a lowercase letter.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a lowercase letter; otherwise, false.</returns>
        public static Boolean IsLower(this Char c)
        {
            return Char.IsLower(c);
        }

        /// <summary>
        ///     Indicates whether the specified  object is a low surrogate.
        /// </summary>
        /// <param name="c">The character to evaluate.</param>
        /// <returns>
        ///     true if the numeric value of the  parameter ranges from U+DC00 through U+DFFF; otherwise, false.
        /// </returns>
        public static Boolean IsLowSurrogate(this Char c)
        {
            return Char.IsLowSurrogate(c);
        }

        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a number.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a number; otherwise, false.</returns>
        public static Boolean IsNumber(this Char c)
        {
            return Char.IsNumber(c);
        }

        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a punctuation mark.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a punctuation mark; otherwise, false.</returns>
        public static Boolean IsPunctuation(this Char c)
        {
            return Char.IsPunctuation(c);
        }

        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a separator character.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a separator character; otherwise, false.</returns>
        public static Boolean IsSeparator(this Char c)
        {
            return Char.IsSeparator(c);
        }

        public static bool IsSpecialChar(this char c)
        {
            return !Char.IsLetterOrDigit(c);
        }

        /// <summary>
        ///     Indicates whether the specified character has a surrogate code unit.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is either a high surrogate or a low surrogate; otherwise, false.</returns>
        public static Boolean IsSurrogate(this Char c)
        {
            return Char.IsSurrogate(c);
        }

        /// <summary>
        ///     Indicates whether the two specified  objects form a surrogate pair.
        /// </summary>
        /// <param name="highSurrogate">The character to evaluate as the high surrogate of a surrogate pair.</param>
        /// <param name="lowSurrogate">The character to evaluate as the low surrogate of a surrogate pair.</param>
        /// <returns>
        ///     true if the numeric value of the  parameter ranges from U+D800 through U+DBFF, and the numeric value of the
        ///     parameter ranges from U+DC00 through U+DFFF; otherwise, false.
        /// </returns>
        public static Boolean IsSurrogatePair(this Char highSurrogate, Char lowSurrogate)
        {
            return Char.IsSurrogatePair(highSurrogate, lowSurrogate);
        }

        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a symbol character.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a symbol character; otherwise, false.</returns>
        public static Boolean IsSymbol(this Char c)
        {
            return Char.IsSymbol(c);
        }

        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as an uppercase letter.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is an uppercase letter; otherwise, false.</returns>
        public static Boolean IsUpper(this Char c)
        {
            return Char.IsUpper(c);
        }

        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as white space.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is white space; otherwise, false.</returns>
        public static Boolean IsWhiteSpace(this Char c)
        {
            return Char.IsWhiteSpace(c);
        }

        /// <summary>
        ///     A char extension method that repeats a character the specified number of times.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="repeatCount">Number of repeats.</param>
        /// <returns>The repeated char.</returns>
        public static string Repeat(this char @this, int repeatCount)
        {
            return new string(@this, repeatCount);
        }

        /// <summary>
        ///     Converts the value of a Unicode character to its lowercase equivalent.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <returns>
        ///     The lowercase equivalent of , or the unchanged value of , if  is already lowercase or not alphabetic.
        /// </returns>
        public static Char ToLower(this Char c)
        {
            return Char.ToLower(c);
        }

        /// <summary>
        ///     Converts the value of a Unicode character to its lowercase equivalent using the casing rules of the invariant
        ///     culture.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <returns>
        ///     The lowercase equivalent of the  parameter, or the unchanged value of , if  is already lowercase or not
        ///     alphabetic.
        /// </returns>
        public static Char ToLowerInvariant(this Char c)
        {
            return Char.ToLowerInvariant(c);
        }

        /// <summary>
        ///     Converts the specified Unicode character to its equivalent string representation.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <returns>The string representation of the value of .</returns>
        public static String ToString(this Char c)
        {
            return Char.ToString(c);
        }

        /// <summary>
        ///     Converts the value of a Unicode character to its uppercase equivalent.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <returns>
        ///     The uppercase equivalent of , or the unchanged value of  if  is already uppercase, has no uppercase
        ///     equivalent, or is not alphabetic.
        /// </returns>
        public static Char ToUpper(this Char c)
        {
            return Char.ToUpper(c);
        }

        /// <summary>
        ///     Converts the value of a Unicode character to its uppercase equivalent using the casing rules of the invariant
        ///     culture.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <returns>
        ///     The uppercase equivalent of the  parameter, or the unchanged value of , if  is already uppercase or not
        ///     alphabetic.
        /// </returns>
        public static Char ToUpperInvariant(this Char c)
        {
            return Char.ToUpperInvariant(c);
        }
    }
}
