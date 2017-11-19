using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreExtensions
{
    /// <summary>
    /// 	Extension methods for string[]
    /// </summary>
    public static class StringArrayExtensions
    {
        /// <summary>
        ///     Joins  the values of a string array if the values are not null or empty.
        /// </summary>
        /// <param name="objs">The string array used for joining.</param>
        /// <param name="separator">The separator to use in the joined string.</param>
        /// <returns></returns>
        public static string JoinNotNullOrEmpty(this string[] objs, string separator)
        {
            var items = new List<string>();
            foreach (var s in objs)
            {
                if (!string.IsNullOrEmpty(s))
                    items.Add(s);
            }
            return string.Join(separator, items.ToArray());
        }

        public static string[] RemoveEmptyElements(this string[] array)
        {
            if (array == null)
                return null;
            var arr = array.Where(str => !string.IsNullOrEmpty(str)).ToArray();
            if (arr.Length == 0)
                return null;
            return arr;
        }

        /// <summary>
        /// 	Returns a combined value of strings from a string array
        /// </summary>
        /// <param name = "values">The values.</param>
        /// <param name = "prefix">The prefix.</param>
        /// <param name = "suffix">The suffix.</param>
        /// <param name = "quotation">The quotation (or null).</param>
        /// <param name = "separator">The separator.</param>
        /// <returns>
        /// 	A <see cref = "System.String" /> that represents this instance.
        /// </returns>
        /// <remarks>
        /// 	Contributed by blaumeister, http://www.codeplex.com/site/users/view/blaumeiser
        /// </remarks>
        public static string ToString(this string[] values, string prefix = "(", string suffix = ")", string quotation = "\"", string separator = ",")
        {
            var sb = new StringBuilder();
            sb.Append(prefix);

            for (var i = 0; i < values.Length; i++)
            {
                if (i > 0)
                    sb.Append(separator);
                if (quotation != null)
                    sb.Append(quotation);
                sb.Append(values[i]);
                if (quotation != null)
                    sb.Append(quotation);
            }

            sb.Append(suffix);
            return sb.ToString();
        }
    }
}
