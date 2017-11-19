using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CoreExtensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        ///     An object extension method that cast anonymous type to the specified type.
        /// </summary>
        /// <typeparam name="T">Generic type parameter. The specified type.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The object as the specified type.</returns>
        public static T As<T>(this object @this)
        {
            return (T)@this;
        }

        /// <summary>
        ///     If target is null, returns null.
        ///     Othervise returns string representation of target using invariant format provider.
        /// </summary>
        /// <param name="target">Target transforming to string representation. Can be null.</param>
        /// <example>
        ///     float? number = null;
        ///     string text1 = number.AsInvariantString();
        ///     number = 15.7892;
        ///     string text2 = number.AsInvariantString();
        /// </example>
        /// <remarks>
        ///     Contributed by tencokacistromy, http://www.codeplex.com/site/users/view/tencokacistromy
        /// </remarks>
        public static string AsInvariantString(this object target)
        {
            var result = string.Format(CultureInfo.InvariantCulture, "{0}", target);
            return result;
        }

        public static Lazy<T> AsLazy<T>(this T obj)
        {
            return new Lazy<T>();
        }

        /// <summary>
        ///     An object extension method that converts the @this to an or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A T.</returns>
        public static T AsOrDefault<T>(this object @this)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to an or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A T.</returns>
        public static T AsOrDefault<T>(this object @this, T defaultValue)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to an or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>A T.</returns>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_AsOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void AsOrDefault()
        ///               {
        ///                   // Type
        ///                   object intValue = 1;
        ///                   object invalidValue = &quot;Fizz&quot;;
        ///  
        ///                   // Exemples
        ///                   var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
        ///                   var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
        ///                   int result3 = invalidValue.AsOrDefault(3); // return 3;
        ///                   int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(0, result2);
        ///                   Assert.AreEqual(3, result3);
        ///                   Assert.AreEqual(4, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_AsOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void AsOrDefault()
        ///               {
        ///                   // Type
        ///                   object intValue = 1;
        ///                   object invalidValue = &quot;Fizz&quot;;
        ///  
        ///                   // Exemples
        ///                   var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
        ///                   var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
        ///                   int result3 = invalidValue.AsOrDefault(3); // return 3;
        ///                   int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(0, result2);
        ///                   Assert.AreEqual(3, result3);
        ///                   Assert.AreEqual(4, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_AsOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void AsOrDefault()
        ///                   {
        ///                       // Type
        ///                       object intValue = 1;
        ///                       object invalidValue = &quot;Fizz&quot;;
        ///           
        ///                       // Exemples
        ///                       var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
        ///                       var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
        ///                       int result3 = invalidValue.AsOrDefault(3); // return 3;
        ///                       int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(1, result1);
        ///                       Assert.AreEqual(0, result2);
        ///                       Assert.AreEqual(3, result3);
        ///                       Assert.AreEqual(4, result4);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static T AsOrDefault<T>(this object @this, Func<T> defaultValueFactory)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to an or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>A T.</returns>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_AsOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void AsOrDefault()
        ///               {
        ///                   // Type
        ///                   object intValue = 1;
        ///                   object invalidValue = &quot;Fizz&quot;;
        ///  
        ///                   // Exemples
        ///                   var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
        ///                   var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
        ///                   int result3 = invalidValue.AsOrDefault(3); // return 3;
        ///                   int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(0, result2);
        ///                   Assert.AreEqual(3, result3);
        ///                   Assert.AreEqual(4, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_AsOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void AsOrDefault()
        ///               {
        ///                   // Type
        ///                   object intValue = 1;
        ///                   object invalidValue = &quot;Fizz&quot;;
        ///  
        ///                   // Exemples
        ///                   var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
        ///                   var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
        ///                   int result3 = invalidValue.AsOrDefault(3); // return 3;
        ///                   int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(0, result2);
        ///                   Assert.AreEqual(3, result3);
        ///                   Assert.AreEqual(4, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_AsOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void AsOrDefault()
        ///                   {
        ///                       // Type
        ///                       object intValue = 1;
        ///                       object invalidValue = &quot;Fizz&quot;;
        ///           
        ///                       // Exemples
        ///                       var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
        ///                       var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
        ///                       int result3 = invalidValue.AsOrDefault(3); // return 3;
        ///                       int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(1, result1);
        ///                       Assert.AreEqual(0, result2);
        ///                       Assert.AreEqual(3, result3);
        ///                       Assert.AreEqual(4, result4);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static T AsOrDefault<T>(this object @this, Func<object, T> defaultValueFactory)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return defaultValueFactory(@this);
            }
        }

        /// <summary>
        ///     If target is null, returns null.
        ///     Othervise returns string representation of target using current culture format provider.
        /// </summary>
        /// <param name="target">Target transforming to string representation. Can be null.</param>
        /// <example>
        ///     float? number = null;
        ///     string text1 = number.AsString();
        ///     number = 15.7892;
        ///     string text2 = number.AsString();
        /// </example>
        /// <remarks>
        ///     Contributed by tencokacistromy, http://www.codeplex.com/site/users/view/tencokacistromy
        /// </remarks>
        public static string AsString(this object target)
        {
            return ReferenceEquals(target, null) ? null : string.Format("{0}", target);
        }

        /// <summary>
        ///     If target is null, returns null.
        ///     Othervise returns string representation of target using specified format provider.
        /// </summary>
        /// <param name="target">Target transforming to string representation. Can be null.</param>
        /// <param name="formatProvider">Format provider used to transformation target to string representation.</param>
        /// <example>
        ///     CultureInfo czech = new CultureInfo("cs-CZ");
        ///     float? number = null;
        ///     string text1 = number.AsString( czech );
        ///     number = 15.7892;
        ///     string text2 = number.AsString( czech );
        /// </example>
        /// <remarks>
        ///     Contributed by tencokacistromy, http://www.codeplex.com/site/users/view/tencokacistromy
        /// </remarks>
        public static string AsString(this object target, IFormatProvider formatProvider)
        {
            var result = string.Format(formatProvider, "{0}", target);
            return result;
        }

        /// <summary>
        ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
        public static bool Between<T>(this T @this, T minValue, T maxValue) where T : IComparable<T>
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        /// <summary>
        ///     Determines whether the value can (in theory) be converted to the specified target type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     <c>true</c> if this instance can be convert to the specified target type; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanConvertTo<T>(this object value)
        {
            if (value != null)
            {
                var targetType = typeof(T);

                var converter = TypeDescriptor.GetConverter(value);
                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                        return true;
                }

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(value.GetType()))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     Allows you to easily pass an Anonymous type from one function to another
        /// </summary>
        /// <typeparam name="T">The Anonymous Type</typeparam>
        /// <param name="obj">The Anonymous Type</param>
        /// <param name="targetType">The Object to "Cast As" the Anonymous type</param>
        /// <returns></returns>
        public static T CastAs<T>(this object obj, T targetType)
        {
            return (T)obj;
        }

        /// <summary>
        ///     Cast an object to the given type. Usefull especially for anonymous types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object to be cast</param>
        /// <returns>
        ///     the casted type or null if casting is not possible.
        /// </returns>
        /// <remarks>
        ///     Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static T CastAs<T>(this object obj) where T : class, new()
        {
            return obj as T;
        }

        /// <summary>
        ///     Cast an object to the given type. Usefull especially for anonymous types.
        /// </summary>
        /// <typeparam name="T">The type to cast to</typeparam>
        /// <param name="value">The object to case</param>
        /// <returns>
        ///     the casted type or null if casting is not possible.
        /// </returns>
        /// <remarks>
        ///     Contributed by blaumeister, http://www.codeplex.com/site/users/view/blaumeiser
        /// </remarks>
        public static T CastTo<T>(this object value)
        {
            if (!(value is T))
                return default(T);

            return (T)value;
        }

        /// <summary>
        ///     A T extension method that chains actions.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="action">The action.</param>
        /// <returns>The @this acted on.</returns>
        public static T Chain<T>(this T @this, Action<T> action)
        {
            action(@this);

            return @this;
        }

        private static string CleanName(IEnumerable<char> name, bool isArray)
        {
            var sb = new StringBuilder();
            foreach (var c in name.Where(c => char.IsLetterOrDigit(c) && c != '`').Select(c => c))
                sb.Append(c);
            if (isArray)
                sb.Append("Array");
            return sb.ToString();
        }

        public static T Clone<T>(this object item)
        {
            if (item != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();
                formatter.Serialize(stream, item);
                stream.Seek(0, SeekOrigin.Begin);
                T result = (T)formatter.Deserialize(stream);
                stream.Close();
                return result;
            }
            return default(T);
        }

        /// <summary>
        ///     A T extension method that that return the first not null value (including the @this).
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>The first not null value.</returns>
        public static T Coalesce<T>(this T @this, params T[] values) where T : class
        {
            if (@this != null)
            {
                return @this;
            }

            foreach (T value in values)
            {
                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }

        /// <summary>
        ///     A T extension method that that return the first not null value (including the @this) or a default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>The first not null value or a default value.</returns>
        public static T CoalesceOrDefault<T>(this T @this, params T[] values) where T : class
        {
            if (@this != null)
            {
                return @this;
            }

            foreach (T value in values)
            {
                if (value != null)
                {
                    return value;
                }
            }

            return default(T);
        }

        /// <summary>
        ///     A T extension method that that return the first not null value (including the @this) or a default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>The first not null value or a default value.</returns>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///  
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_CoalesceOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void CoalesceOrDefault()
        ///               {
        ///                   // Varable
        ///                   object nullObject = null;
        ///  
        ///                   // Type
        ///                   object @thisNull = null;
        ///                   object @thisNotNull = &quot;Fizz&quot;;
        ///  
        ///                   // Exemples
        ///                   object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
        ///                   object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result3 = @thisNull.CoalesceOrDefault((x) =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result2);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result3);
        ///                   Assert.AreEqual(&quot;Fizz&quot;, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_CoalesceOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void CoalesceOrDefault()
        ///               {
        ///                   // Varable
        ///                   object nullObject = null;
        ///  
        ///                   // Type
        ///                   object @thisNull = null;
        ///                   object @thisNotNull = &quot;Fizz&quot;;
        ///  
        ///                   // Exemples
        ///                   object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
        ///                   object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result3 = @thisNull.CoalesceOrDefault(x =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result2);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result3);
        ///                   Assert.AreEqual(&quot;Fizz&quot;, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_CoalesceOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void CoalesceOrDefault()
        ///                   {
        ///                       // Varable
        ///                       object nullObject = null;
        ///           
        ///                       // Type
        ///                       object @thisNull = null;
        ///                       object @thisNotNull = &quot;Fizz&quot;;
        ///           
        ///                       // Exemples
        ///                       object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
        ///                       object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                       object result3 = @thisNull.CoalesceOrDefault(x =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                       object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(&quot;Buzz&quot;, result1);
        ///                       Assert.AreEqual(&quot;Buzz&quot;, result2);
        ///                       Assert.AreEqual(&quot;Buzz&quot;, result3);
        ///                       Assert.AreEqual(&quot;Fizz&quot;, result4);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static T CoalesceOrDefault<T>(this T @this, Func<T> defaultValueFactory, params T[] values) where T : class
        {
            if (@this != null)
            {
                return @this;
            }

            foreach (T value in values)
            {
                if (value != null)
                {
                    return value;
                }
            }

            return defaultValueFactory();
        }

        /// <summary>
        ///     A T extension method that that return the first not null value (including the @this) or a default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>The first not null value or a default value.</returns>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///  
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_CoalesceOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void CoalesceOrDefault()
        ///               {
        ///                   // Varable
        ///                   object nullObject = null;
        ///  
        ///                   // Type
        ///                   object @thisNull = null;
        ///                   object @thisNotNull = &quot;Fizz&quot;;
        ///  
        ///                   // Exemples
        ///                   object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
        ///                   object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result3 = @thisNull.CoalesceOrDefault((x) =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result2);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result3);
        ///                   Assert.AreEqual(&quot;Fizz&quot;, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_CoalesceOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void CoalesceOrDefault()
        ///               {
        ///                   // Varable
        ///                   object nullObject = null;
        ///  
        ///                   // Type
        ///                   object @thisNull = null;
        ///                   object @thisNotNull = &quot;Fizz&quot;;
        ///  
        ///                   // Exemples
        ///                   object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
        ///                   object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result3 = @thisNull.CoalesceOrDefault(x =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                   object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result2);
        ///                   Assert.AreEqual(&quot;Buzz&quot;, result3);
        ///                   Assert.AreEqual(&quot;Fizz&quot;, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_CoalesceOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void CoalesceOrDefault()
        ///                   {
        ///                       // Varable
        ///                       object nullObject = null;
        ///           
        ///                       // Type
        ///                       object @thisNull = null;
        ///                       object @thisNotNull = &quot;Fizz&quot;;
        ///           
        ///                       // Exemples
        ///                       object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
        ///                       object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                       object result3 = @thisNull.CoalesceOrDefault(x =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
        ///                       object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(&quot;Buzz&quot;, result1);
        ///                       Assert.AreEqual(&quot;Buzz&quot;, result2);
        ///                       Assert.AreEqual(&quot;Buzz&quot;, result3);
        ///                       Assert.AreEqual(&quot;Fizz&quot;, result4);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static T CoalesceOrDefault<T>(this T @this, Func<T, T> defaultValueFactory, params T[] values) where T : class
        {
            if (@this != null)
            {
                return @this;
            }

            foreach (T value in values)
            {
                if (value != null)
                {
                    return value;
                }
            }

            return defaultValueFactory(null);
        }

        public static TResult ConvertTo<TInput, TResult>(this TInput source, Func<TInput, TResult> action)
        {
            if (source == null)
                throw new NullReferenceException("Convert can not be called by a null object");

            return action(source);
        }

        /// <summary>
        ///     Converts an object to the specified target type or returns the default value if
        ///     those 2 types are not convertible.
        ///     <para>
        ///         If the <paramref name="value" /> can't be convert even if the types are
        ///         convertible with each other, an exception is thrown.
        ///     </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The target type</returns>
        public static T ConvertTo<T>(this object value)
        {
            return value.ConvertTo(default(T));
        }

        /// <summary>
        ///     Converts an object to the specified target type or returns the default value if
        ///     those 2 types are not convertible.
        ///     <para>
        ///         If the <paramref name="value" /> can't be convert even if the types are
        ///         convertible with each other, an exception is thrown.
        ///     </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The target type</returns>
        public static T ConvertTo<T>(this object value, T defaultValue)
        {
            if (value != null)
            {
                var targetType = typeof(T);

                if (value.GetType() == targetType) return (T)value;

                var converter = TypeDescriptor.GetConverter(value);
                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                        return (T)converter.ConvertTo(value, targetType);
                }

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(value.GetType()))
                        return (T)converter.ConvertFrom(value);
                }
            }
            return defaultValue;
        }

        /// <summary>
        ///     Converts an object to the specified target type or returns the default value if
        ///     those 2 types are not convertible.
        ///     <para>Any exceptions are optionally ignored (<paramref name="ignoreException" />).</para>
        ///     <para>
        ///         If the exceptions are not ignored and the <paramref name="value" /> can't be convert even if
        ///         the types are convertible with each other, an exception is thrown.
        ///     </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="ignoreException">if set to <c>true</c> ignore any exception.</param>
        /// <returns>The target type</returns>
        public static T ConvertTo<T>(this object value, T defaultValue, bool ignoreException)
        {
            if (ignoreException)
            {
                try
                {
                    return value.ConvertTo<T>();
                }
                catch
                {
                    return defaultValue;
                }
            }
            return value.ConvertTo<T>();
        }

        /// <summary>
        ///     Converts an object to the specified target type or returns the default value.
        ///     <para>Any exceptions are ignored. </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The target type</returns>
        public static T ConvertToAndIgnoreException<T>(this object value)
        {
            return value.ConvertToAndIgnoreException(default(T));
        }

        /// <summary>
        ///     Converts an object to the specified target type or returns the default value.
        ///     <para>Any exceptions are ignored. </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The target type</returns>
        public static T ConvertToAndIgnoreException<T>(this object value, T defaultValue)
        {
            return value.ConvertTo(defaultValue, true);
        }

        /// <summary>
        ///     Returns an object of the specified Type and whoes value is equivalent to the specified object.
        /// </summary>
        /// <typeparam name="T">The Type to convert to.</typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ConvertType<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        /// <summary>
        ///     Copies the readable and writable public property values from the source object to the target
        /// </summary>
        /// <remarks>The source and target objects must be of the same type.</remarks>
        /// <param name="target">The target object</param>
        /// <param name="source">The source object</param>
        public static void CopyPropertiesFrom(this object target, object source)
        {
            CopyPropertiesFrom(target, source, string.Empty);
        }

        /// <summary>
        ///     Copies the readable and writable public property values from the source object to the target
        /// </summary>
        /// <remarks>The source and target objects must be of the same type.</remarks>
        /// <param name="target">The target object</param>
        /// <param name="source">The source object</param>
        /// <param name="ignoreProperty">A single property name to ignore</param>
        public static void CopyPropertiesFrom(this object target, object source, string ignoreProperty)
        {
            CopyPropertiesFrom(target, source, new[] { ignoreProperty });
        }

        /// <summary>
        ///     Copies the readable and writable public property values from the source object to the target
        /// </summary>
        /// <remarks>The source and target objects must be of the same type.</remarks>
        /// <param name="target">The target object</param>
        /// <param name="source">The source object</param>
        /// <param name="ignoreProperties">An array of property names to ignore</param>
        public static void CopyPropertiesFrom(this object target, object source, string[] ignoreProperties)
        {
            // Get and check the object types
            var type = source.GetType();
            if (target.GetType() != type)
            {
                throw new ArgumentException("The source type must be the same as the target");
            }

            // Build a clean list of property names to ignore
            var ignoreList = new List<string>();
            foreach (var item in ignoreProperties)
            {
                if (!string.IsNullOrEmpty(item) && !ignoreList.Contains(item))
                {
                    ignoreList.Add(item);
                }
            }

            // Copy the properties
            foreach (var property in type.GetProperties())
            {
                if (property.CanWrite
                    && property.CanRead
                    && !ignoreList.Contains(property.Name))
                {
                    var val = property.GetValue(source, null);
                    property.SetValue(target, val, null);
                }
            }
        }

        /// <summary>
        ///     Counts and returns the recursive execution of the passed function until it returns null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item to start peforming on.</param>
        /// <param name="function">The function to be executed.</param>
        /// <returns>The number of executions until the function returned null</returns>
        public static int CountLoopsToNull<T>(this T item, Func<T, T> function) where T : class
        {
            var num = 0;
            while ((item = function(item)) != null)
            {
                num++;
            }
            return num;
        }

        public static T DeepClone<T>(this T original)
        {
            return original.DeepCopyByExpressionTree();
        }

        public static void DisposeIfNotNull(this IDisposable source)
        {
            source?.Dispose();
        }

        /// <summary>
        ///     Cast an object to the given type. Usefull especially for anonymous types.
        /// </summary>
        /// <param name="obj">The object to be cast</param>
        /// <param name="targetType">The type to cast to</param>
        /// <returns>
        ///     the casted type or null if casting is not possible.
        /// </returns>
        /// <remarks>
        ///     Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static object DynamicCast(this object obj, Type targetType)
        {
            // First, it might be just a simple situation
            if (targetType.IsInstanceOfType(obj))
                return obj;

            // If not, we need to find a cast operator. The operator
            // may be explicit or implicit and may be included in
            // either of the two types...
            const BindingFlags pubStatBinding = BindingFlags.Public | BindingFlags.Static;
            var originType = obj.GetType();
            string[] names = { "op_Implicit", "op_Explicit" };

            var castMethod =
                targetType.GetMethods(pubStatBinding).Union(originType.GetMethods(pubStatBinding)).FirstOrDefault(
                    itm =>
                        itm.ReturnType == targetType && itm.GetParameters().Length == 1 &&
                        itm.GetParameters()[0].ParameterType.IsAssignableFrom(originType) && names.Contains(itm.Name));
            if (null != castMethod)
                return castMethod.Invoke(null, new[] { obj });
            throw new InvalidOperationException(
                string.Format(
                    "No matching cast operator found from {0} to {1}.",
                    originType.Name,
                    targetType.Name));
        }

        /// <summary>
        ///     Determines whether the object is equal to any of the provided values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object to be compared.</param>
        /// <param name="values">The values to compare with the object.</param>
        /// <returns></returns>
        public static bool EqualsAny<T>(this T obj, params T[] values)
        {
            return Array.IndexOf(values, obj) != -1;
        }

        /// <summary>
        ///     Determines whether the object is equal to none of the provided values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object to be compared.</param>
        /// <param name="values">The values to compare with the object.</param>
        /// <returns></returns>
        public static bool EqualsNone<T>(this T obj, params T[] values)
        {
            return obj.EqualsAny(values) == false;
        }

        /// <summary>
        ///     Throws an <see cref="System.ArgumentNullException" />
        ///     if the the value is null.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        /// <param name="name">The name of the parameter being tested.</param>
        public static void ExceptionIfNullOrEmpty(this object value, string message, string name)
        {
            if (value == null)
                throw new ArgumentNullException(name, message);
        }

        public static List<int> FindAllIndexOf<T>(this T[] a, Predicate<T> match) where T : class
        {
            var subArray = Array.FindAll(a, match);
            return (from T item in subArray select Array.IndexOf(a, item)).ToList();
        }

        /// <summary>
        ///     Finds a type instance using a recursive call. The method is useful to find specific parents for example.
        /// </summary>
        /// <typeparam name="T">The source type to perform on.</typeparam>
        /// <typeparam name="K">The targte type to be returned</typeparam>
        /// <param name="item">The item to start performing on.</param>
        /// <param name="function">The function to be executed.</param>
        /// <returns>An target type instance or null.</returns>
        /// <example>
        ///     <code>
        /// var tree = ...
        /// var node = tree.FindNodeByValue("");
        /// var parentByType = node.FindTypeByRecursion%lt;TheType&gt;(n => n.Parent);
        /// </code>
        /// </example>
        public static K FindTypeByRecursion<T, K>(this T item, Func<T, T> function)
                            where T : class
                            where K : class, T
        {
            do
            {
                if (item is K) return (K)item;
            } while ((item = function(item)) != null);
            return null;
        }

        public static byte[] FromBinaryString(this string s)
        {
            int count = s.Length / 8;
            var b = new byte[count];
            for (int i = 0; i < count; i++)
                b[i] = Convert.ToByte(s.Substring(i * 8, 8), 2);

            return b;
        }

        /// <summary>
        ///     Returns the  for the specified object.
        /// </summary>
        /// <param name="value">An object that implements the  interface.</param>
        /// <returns>The  for , or  if  is null.</returns>
        public static TypeCode GetTypeCode(this Object value)
        {
            return Convert.GetTypeCode(value);
        }

        /// <summary>
        ///     Gets the type default value for the underlying data type, in case of reference types: null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The default value</returns>
        public static T GetTypeDefaultValue<T>(this T value)
        {
            return default(T);
        }

        /// <summary>
        ///     A T extension method that gets value or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <returns>The value or default.</returns>
        public static TResult GetValueOrDefault<T, TResult>(this T @this, Func<T, TResult> func)
        {
            try
            {
                return func(@this);
            }
            catch (Exception)
            {
                return default(TResult);
            }
        }

        /// <summary>
        ///     A T extension method that gets value or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value or default.</returns>
        public static TResult GetValueOrDefault<T, TResult>(this T @this, Func<T, TResult> func, TResult defaultValue)
        {
            try
            {
                return func(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     A T extension method that gets value or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The value or default.</returns>
        /// <example>
        ///     <code>
        ///       using System.Xml;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///  
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_GetValueOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void GetValueOrDefault()
        ///               {
        ///                   // Type
        ///                   var @this = new XmlDocument();
        ///  
        ///                   // Exemples
        ///                   string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
        ///                   string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System.Xml;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_GetValueOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void GetValueOrDefault()
        ///               {
        ///                   // Type
        ///                   var @this = new XmlDocument();
        ///  
        ///                   // Exemples
        ///                   string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
        ///                   string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using System.Xml;
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_GetValueOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void GetValueOrDefault()
        ///                   {
        ///                       // Type
        ///                       var @this = new XmlDocument();
        ///           
        ///                       // Exemples
        ///                       string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
        ///                       string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
        ///                       Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static TResult GetValueOrDefault<T, TResult>(this T @this, Func<T, TResult> func, Func<TResult> defaultValueFactory)
        {
            try
            {
                return func(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     A T extension method that gets value or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The value or default.</returns>
        /// <example>
        ///     <code>
        ///       using System.Xml;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///  
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_GetValueOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void GetValueOrDefault()
        ///               {
        ///                   // Type
        ///                   var @this = new XmlDocument();
        ///  
        ///                   // Exemples
        ///                   string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
        ///                   string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System.Xml;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_GetValueOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void GetValueOrDefault()
        ///               {
        ///                   // Type
        ///                   var @this = new XmlDocument();
        ///  
        ///                   // Exemples
        ///                   string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
        ///                   string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
        ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using System.Xml;
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_GetValueOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void GetValueOrDefault()
        ///                   {
        ///                       // Type
        ///                       var @this = new XmlDocument();
        ///           
        ///                       // Exemples
        ///                       string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
        ///                       string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
        ///                       Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static TResult GetValueOrDefault<T, TResult>(this T @this, Func<T, TResult> func, Func<T, TResult> defaultValueFactory)
        {
            try
            {
                return func(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory(@this);
            }
        }

        public static T IfNotNull<T>(this T obj, Action action)
        {
            if (obj != null)
            {
                action();
            }

            return obj;
        }

        /// <summary>A T extension method that execute an action when the value is not null.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="action">The action.</param>
        public static void IfNotNull<T>(this T @this, Action<T> action) where T : class
        {
            if (@this != null)
            {
                action(@this);
            }
        }

        /// <summary>
        ///     A T extension method that the function result if not null otherwise default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <returns>The function result if @this is not null otherwise default value.</returns>
        public static TResult IfNotNull<T, TResult>(this T @this, Func<T, TResult> func) where T : class
        {
            return @this != null ? func(@this) : default(TResult);
        }

        /// <summary>
        ///     A T extension method that the function result if not null otherwise default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The function result if @this is not null otherwise default value.</returns>
        public static TResult IfNotNull<T, TResult>(this T @this, Func<T, TResult> func, TResult defaultValue) where T : class
        {
            return @this != null ? func(@this) : defaultValue;
        }

        /// <summary>
        ///     A T extension method that the function result if not null otherwise default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="func">The function.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The function result if @this is not null otherwise default value.</returns>
        public static TResult IfNotNull<T, TResult>(this T @this, Func<T, TResult> func, Func<TResult> defaultValueFactory) where T : class
        {
            return @this != null ? func(@this) : defaultValueFactory();
        }

        public static T IfNull<T>(this T obj, Action action)
        {
            if (obj == null)
            {
                action();
            }

            return obj;
        }

        public static T IfNull<T>(this T obj, Func<T> func)
        {
            if (obj == null)
            {
                return func();
            }

            return obj;
        }

        /// <summary>
        ///     A T extension method to determines whether the object is equal to any of the provided values.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list contains the object, else false.</returns>
        public static bool In<T>(this T @this, params T[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        /// <summary>
        ///     A T extension method to determines whether the object is equal to any of the provided values.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list contains the object, else false.</returns>
        public static bool In<T>(this T @this, IEnumerable<T> values)
        {
            return Array.IndexOf(values.ToArray(), @this) != -1;
        }

        /// <summary>
        ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
        public static bool InRange<T>(this T @this, T minValue, T maxValue) where T : IComparable<T>
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        /// <summary>
        ///     A T extension method that query if '@this' is array.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if array, false if not.</returns>
        public static bool IsArray<T>(this T @this)
        {
            return @this.GetType().IsArray;
        }

        /// <summary>
        ///     Returns a Boolean value indicating whether a variable points to a System.Array.
        /// </summary>
        /// <param name="obj">Required. Object variable.</param>
        /// <returns>Returns a Boolean value indicating whether a variable points to a System.Array.</returns>
        public static bool IsArray(this object obj)
        {
            return obj.GetType().IsArray;
        }

        /// <summary>
        ///     An object extension method that query if '@this' is assignable from.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if assignable from, false if not.</returns>
        public static bool IsAssignableFrom<T>(this object @this)
        {
            Type type = @this.GetType();
            return type.IsAssignableFrom(typeof(T));
        }

        /// <summary>
        ///     An object extension method that query if '@this' is assignable from.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns>true if assignable from, false if not.</returns>
        public static bool IsAssignableFrom(this object @this, Type targetType)
        {
            Type type = @this.GetType();
            return type.IsAssignableFrom(targetType);
        }

        /// <summary>
        ///     Determines whether the object is assignable to the passed generic type.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>
        ///     <c>true</c> if the object is assignable to the specified type; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAssignableTo<T>(this object obj)
        {
            return obj.IsAssignableTo(typeof(T));
        }

        /// <summary>
        ///     Determines whether the object is assignable to the passed type.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <param name="type">The target type.</param>
        /// <returns>
        ///     <c>true</c> if the object is assignable to the specified type; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAssignableTo(this object obj, Type type)
        {
            var objectType = obj.GetType();
            return type.IsAssignableFrom(objectType);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is attribute defined.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>true if attribute defined, false if not.</returns>
        public static bool IsAttributeDefined(this object @this, Type attributeType, bool inherit)
        {
            return @this.GetType().GetCustomAttribute(attributeType) != null;
        }

        /// <summary>
        ///     An object extension method that query if '@this' is attribute defined.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>true if attribute defined, false if not.</returns>
        public static bool IsAttributeDefined<T>(this object @this, bool inherit) where T : Attribute
        {
            return @this.GetType().GetCustomAttribute(typeof(T)) != null;
        }

        /// <summary>
        /// 	Determines whether the specified value is between the the defined minimum and maximum range (including those values).
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value">The value.</param>
        /// <param name = "minValue">The minimum value.</param>
        /// <param name = "maxValue">The maximum value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is between min and max; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// 	var value = 5;
        /// 	if(value.IsBetween(1, 10)) { 
        /// 	// ... 
        /// 	}
        /// </example>
        public static bool IsBetween<T>(this T value, T minValue, T maxValue) where T : IComparable<T>
        {
            return IsBetween(value, minValue, maxValue, Comparer<T>.Default);
        }

        /// <summary>
        /// 	Determines whether the specified value is between the the defined minimum and maximum range (including those values).
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value">The value.</param>
        /// <param name = "minValue">The minimum value.</param>
        /// <param name = "maxValue">The maximum value.</param>
        /// <param name = "comparer">An optional comparer to be used instead of the types default comparer.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is between min and max; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// 	var value = 5;
        /// 	if(value.IsBetween(1, 10)) {
        /// 	// ...
        /// 	}
        /// </example>
        /// <remarks>
        /// Note that this does an "inclusive" comparison:  The high & low values themselves are considered "in between".  
        /// However, in some context, a exclusive comparion -- only values greater than the low end and lesser than the high end 
        /// are "in between" -- is desired; in other contexts, values can be greater or equal to the low end, but only less than the high end.
        /// </remarks>
        public static bool IsBetween<T>(this T value, T minValue, T maxValue, IComparer<T> comparer) where T : IComparable<T>
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            var minMaxCompare = comparer.Compare(minValue, maxValue);
            if (minMaxCompare < 0)
                return ((comparer.Compare(value, minValue) >= 0) && (comparer.Compare(value, maxValue) <= 0));
            //else if (minMaxCompare == 0)				// unnecessary  'else' below handles this case.
            //    return (comparer.Compare(value, minValue) == 0);
            else
                return ((comparer.Compare(value, maxValue) >= 0) && (comparer.Compare(value, minValue) <= 0));
        }

        /// <summary>
        ///     A T extension method that query if '@this' is class.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if class, false if not.</returns>
        public static bool IsClass<T>(this T @this)
        {
            return @this.GetType().IsClass;
        }

        /// <summary>
        ///     Returns a Boolean value indicating whether a variable points to a DateTime object.
        /// </summary>
        /// <param name="obj">Required. Object variable.</param>
        /// <returns>Returns a Boolean value indicating whether a variable points to a DateTime object.</returns>
        public static bool IsDate(this object obj)
        {
            return obj.IsType(typeof(DateTime));
        }

        /// <summary>
        ///     Returns a Boolean value indicating whether an expression evaluates to the DBNull class.
        ///     Extension Added by dotNetExt.Object
        /// </summary>
        /// <param name="obj">Required. Object expression.</param>
        /// <returns>Returns a Boolean value indicating whether an expression evaluates to the DBNull class.</returns>
        public static bool IsDBNull(this object obj)
        {
            return obj.IsType(typeof(DBNull));
        }

        /// <summary>
        ///     A T extension method that query if 'source' is the default value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="source">The source to act on.</param>
        /// <returns>true if default, false if not.</returns>
        public static bool IsDefault<T>(this T source)
        {
            return source.Equals(default(T));
        }

        /// <summary>
        ///     Determines whether the specified value is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     <c>true</c> if the specified value is empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmpty<T>(this T value) where T : struct
        {
            return value.Equals(default(T));
        }

        /// <summary>
        ///     A T extension method that query if '@this' is enum.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if enum, false if not.</returns>
        public static bool IsEnum<T>(this T @this)
        {
            return @this.GetType().IsEnum;
        }

        public static bool IsExactlyOneOf<T>(this T value, IList<T> values)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            return values.Contains(value);
        }

        public static bool IsExactlyOneOf<T>(this T value, params T[] values)
        {
            return IsExactlyOneOf(value, values.ToList());
        }

        /// <summary>
        ///     Returns TRUE, if specified target reference is equals with null reference.
        ///     Othervise returns FALSE.
        /// </summary>
        /// <param name="target">Target reference. Can be null.</param>
        /// <remarks>
        ///     Some types has overloaded '==' and '!=' operators.
        ///     So the code "null == ((MyClass)null)" can returns <c>false</c>.
        ///     The most correct way how to test for null reference is using "System.Object.ReferenceEquals(object, object)"
        ///     method.
        ///     However the notation with ReferenceEquals method is long and uncomfortable - this extension method solve it.
        ///     Contributed by tencokacistromy, http://www.codeplex.com/site/users/view/tencokacistromy
        /// </remarks>
        /// <example>
        ///     object someObject = GetSomeObject();
        ///     if ( someObject.IsNotNull() ) { /* the someObject is not null */ }
        ///     else { /* the someObject is null */ }
        /// </example>
        public static bool IsNotNull(this object target)
        {
            var ret = IsNotNull<object>(target);
            return ret;
        }

        /// <summary>
        ///     Returns TRUE, if specified target reference is equals with null reference.
        ///     Othervise returns FALSE.
        /// </summary>
        /// <typeparam name="T">Type of target.</typeparam>
        /// <param name="target">Target reference. Can be null.</param>
        /// <remarks>
        ///     Some types has overloaded '==' and '!=' operators.
        ///     So the code "null == ((MyClass)null)" can returns <c>false</c>.
        ///     The most correct way how to test for null reference is using "System.Object.ReferenceEquals(object, object)"
        ///     method.
        ///     However the notation with ReferenceEquals method is long and uncomfortable - this extension method solve it.
        ///     Contributed by tencokacistromy, http://www.codeplex.com/site/users/view/tencokacistromy
        /// </remarks>
        /// <example>
        ///     MyClass someObject = GetSomeObject();
        ///     if ( someObject.IsNotNull() ) { /* the someObject is not null */ }
        ///     else { /* the someObject is null */ }
        /// </example>
        public static bool IsNotNull<T>(this T target)
        {
            var result = !ReferenceEquals(target, null);
            return result;
        }

        /// <summary>
        ///     Returns TRUE, if specified target reference is equals with null reference.
        ///     Othervise returns FALSE.
        /// </summary>
        /// <param name="target">Target reference. Can be null.</param>
        /// <remarks>
        ///     Some types has overloaded '==' and '!=' operators.
        ///     So the code "null == ((MyClass)null)" can returns <c>false</c>.
        ///     The most correct way how to test for null reference is using "System.Object.ReferenceEquals(object, object)"
        ///     method.
        ///     However the notation with ReferenceEquals method is long and uncomfortable - this extension method solve it.
        ///     Contributed by tencokacistromy, http://www.codeplex.com/site/users/view/tencokacistromy
        /// </remarks>
        /// <example>
        ///     object someObject = GetSomeObject();
        ///     if ( someObject.IsNull() ) { /* the someObject is null */ }
        ///     else { /* the someObject is not null */ }
        /// </example>
        public static bool IsNull(this object target)
        {
            var ret = IsNull<object>(target);
            return ret;
        }

        /// <summary>
        ///     Returns TRUE, if specified target reference is equals with null reference.
        ///     Othervise returns FALSE.
        /// </summary>
        /// <typeparam name="T">Type of target.</typeparam>
        /// <param name="target">Target reference. Can be null.</param>
        /// <remarks>
        ///     Some types has overloaded '==' and '!=' operators.
        ///     So the code "null == ((MyClass)null)" can returns <c>false</c>.
        ///     The most correct way how to test for null reference is using "System.Object.ReferenceEquals(object, object)"
        ///     method.
        ///     However the notation with ReferenceEquals method is long and uncomfortable - this extension method solve it.
        ///     Contributed by tencokacistromy, http://www.codeplex.com/site/users/view/tencokacistromy
        /// </remarks>
        /// <example>
        ///     MyClass someObject = GetSomeObject();
        ///     if ( someObject.IsNull() ) { /* the someObject is null */ }
        ///     else { /* the someObject is not null */ }
        /// </example>
        public static bool IsNull<T>(this T target)
        {
            var result = ReferenceEquals(target, null);
            return result;
        }

        /// <summary>
        ///     Determines whether the object is exactly of the passed generic type.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>
        ///     <c>true</c> if the object is of the specified type; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOfType<T>(this object obj)
        {
            return obj.IsOfType(typeof(T));
        }

        /// <summary>
        ///     Determines whether the object is excactly of the passed type
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <param name="type">The target type.</param>
        /// <returns>
        ///     <c>true</c> if the object is of the specified type; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOfType(this object obj, Type type)
        {
            return obj.GetType() == type;
        }

        /// <summary>
        ///     Determines whether the object is of the passed generic type or inherits from it.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>
        ///     <c>true</c> if the object is of the specified type; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOfTypeOrInherits<T>(this object obj)
        {
            return obj.IsOfTypeOrInherits(typeof(T));
        }

        /// <summary>
        ///     Determines whether the object is of the passed type or inherits from it.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <param name="type">The target type.</param>
        /// <returns>
        ///     <c>true</c> if the object is of the specified type; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOfTypeOrInherits(this object obj, Type type)
        {
            var objectType = obj.GetType();

            do
            {
                if (objectType == type)
                    return true;
                if ((objectType == objectType.BaseType) || (objectType.BaseType == null))
                    return false;
                objectType = objectType.BaseType;
            } while (true);
        }

        public static bool IsSameType(this object obj1, object obj2)
        {
            if (obj1.GetType().IsInstanceOfType(obj2) || obj2.GetType().IsInstanceOfType(obj1))
                return true;
            return false;
        }

        /// <summary>
        ///     A T extension method that query if '@this' is subclass of.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="type">The Type to process.</param>
        /// <returns>true if subclass of, false if not.</returns>
        public static bool IsSubclassOf<T>(this T @this, Type type)
        {
            return @this.GetType().IsSubclassOf(type);
        }

        /// <summary>
        ///     Returns a Boolean value indicating whether a variable is of the indicated Type
        /// </summary>
        /// <param name="obj">Required. Object variable.</param>
        /// <param name="type">The Type to check the object against.</param>
        /// <returns>Returns a Boolean value indicating whether a variable is of the indicated Type</returns>
        public static bool IsType(this object obj, Type type)
        {
            return obj.GetType() == type;
        }

        /// <summary>
        ///     A T extension method that query if '@this' is type of.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="type">The type.</param>
        /// <returns>true if type of, false if not.</returns>
        public static bool IsTypeOf<T>(this T @this, Type type)
        {
            return @this.GetType() == type;
        }

        /// <summary>
        ///     A T extension method that query if '@this' is type or inherits of.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="type">The type.</param>
        /// <returns>true if type or inherits of, false if not.</returns>
        public static bool IsTypeOrInheritsOf<T>(this T @this, Type type)
        {
            Type objectType = @this.GetType();

            while (true)
            {
                if (objectType == type)
                {
                    return true;
                }

                if ((objectType == objectType.BaseType) || (objectType.BaseType == null))
                {
                    return false;
                }

                objectType = objectType.BaseType;
            }
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid bool.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid bool, false if not.</returns>
        public static bool IsValidBoolean(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return bool.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid byte.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid byte, false if not.</returns>
        public static bool IsValidByte(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return byte.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid char.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid char, false if not.</returns>
        public static bool IsValidChar(this object @this)
        {
            return char.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid System.DateTime.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid System.DateTime, false if not.</returns>
        public static bool IsValidDateTime(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return DateTime.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid System.DateTimeOffset.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid System.DateTimeOffset, false if not.</returns>
        public static bool IsValidDateTimeOffSet(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return DateTimeOffset.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid decimal.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid decimal, false if not.</returns>
        public static bool IsValidDecimal(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return decimal.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid double.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid double, false if not.</returns>
        public static bool IsValidDouble(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return double.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid float.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid float, false if not.</returns>
        public static bool IsValidFloat(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return float.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid System.Guid.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid System.Guid, false if not.</returns>
        public static bool IsValidGuid(this object @this)
        {
            return Guid.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid short.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid short, false if not.</returns>
        public static bool IsValidInt16(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return short.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid int.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid int, false if not.</returns>
        public static bool IsValidInt32(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return int.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid long.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid long, false if not.</returns>
        public static bool IsValidInt64(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return long.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid long.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid long, false if not.</returns>
        public static bool IsValidLong(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return long.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid sbyte.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid sbyte, false if not.</returns>
        public static bool IsValidSByte(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return sbyte.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid short.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid short, false if not.</returns>
        public static bool IsValidShort(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return short.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid float.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid float, false if not.</returns>
        public static bool IsValidSingle(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return float.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid string, false if not.</returns>
        public static bool IsValidString(this object @this)
        {
            return true;
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid ushort.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid ushort, false if not.</returns>
        public static bool IsValidUInt16(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return ushort.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid uint.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid uint, false if not.</returns>
        public static bool IsValidUInt32(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return uint.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid ulong.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid ulong, false if not.</returns>
        public static bool IsValidUInt64(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return ulong.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid ulong.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid ulong, false if not.</returns>
        public static bool IsValidULong(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return ulong.TryParse(@this.ToString(), out _);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is valid ushort.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid ushort, false if not.</returns>
        public static bool IsValidUShort(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return ushort.TryParse(@this.ToString(), out _);
        }

        public static TResult Merge<TInput, TResult>(this TInput source, TResult Object, Func<TInput, TResult, TResult> action)
        {
            if (source == null)
                throw new NullReferenceException("Convert can not be called by a null object");

            if (Object == null)
                throw new ArgumentNullException("Object can not be null");

            return action(source, Object);
        }

        private static bool NeedRecursion(Type type, object o)
        {
            return o != null && !type.IsPrimitive &&
                   !(o is string || o is DateTime || o is DateTimeOffset || o is TimeSpan || o is Delegate || o is Enum ||
                     o is decimal || o is Guid);
        }

        /// <summary>
        ///     A T extension method to determines whether the object is not equal to any of the provided values.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list doesn't contains the object, else false.</returns>
        public static bool NotIn<T>(this T @this, params T[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }

        /// <summary>
        ///     If target is null reference, returns notNullValue.
        ///     Othervise returns target.
        /// </summary>
        /// <typeparam name="T">Type of target.</typeparam>
        /// <param name="target">Target which is maybe null. Can be null.</param>
        /// <param name="notNullValue">Value used instead of null.</param>
        /// <example>
        ///     const int DEFAULT_NUMBER = 123;
        ///     int? number = null;
        ///     int notNullNumber1 = number.NotNull( DEFAULT_NUMBER ).Value; // returns 123
        ///     number = 57;
        ///     int notNullNumber2 = number.NotNull( DEFAULT_NUMBER ).Value; // returns 57
        /// </example>
        /// <remarks>
        ///     Contributed by tencokacistromy, http://www.codeplex.com/site/users/view/tencokacistromy
        /// </remarks>
        public static T NotNull<T>(this T target, T notNullValue)
        {
            return ReferenceEquals(target, null) ? notNullValue : target;
        }

        /// <summary>
        ///     If target is null reference, returns result from notNullValueProvider.
        ///     Othervise returns target.
        /// </summary>
        /// <typeparam name="T">Type of target.</typeparam>
        /// <param name="target">Target which is maybe null. Can be null.</param>
        /// <param name="notNullValueProvider">Delegate which return value is used instead of null.</param>
        /// <example>
        ///     int? number = null;
        ///     int notNullNumber1 = number.NotNull( ()=> GetRandomNumber(10, 20) ).Value; // returns random number from 10 to 20
        ///     number = 57;
        ///     int notNullNumber2 = number.NotNull( ()=> GetRandomNumber(10, 20) ).Value; // returns 57
        /// </example>
        /// <remarks>
        ///     Contributed by tencokacistromy, http://www.codeplex.com/site/users/view/tencokacistromy
        /// </remarks>
        public static T NotNull<T>(this T target, Func<T> notNullValueProvider)
        {
            return ReferenceEquals(target, null) ? notNullValueProvider() : target;
        }

        /// <summary>
        ///     A T extension method that null if.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A T.</returns>
        public static T NullIf<T>(this T @this, Func<T, bool> predicate) where T : class
        {
            if (predicate(@this))
            {
                return null;
            }
            return @this;
        }

        /// <summary>
        ///     A T extension method that null if equals.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="value">The value.</param>
        /// <returns>A T.</returns>
        public static T NullIfEquals<T>(this T @this, T value) where T : class
        {
            if (@this.Equals(value))
            {
                return null;
            }
            return @this;
        }

        /// <summary>
        ///     A T extension method that null if equals any.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>A T.</returns>
        public static T NullIfEqualsAny<T>(this T @this, params T[] values) where T : class
        {
            if (Array.IndexOf(values, @this) != -1)
            {
                return null;
            }
            return @this;
        }

        /// <summary>
        ///     A T extension method that shallow copy.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A T.</returns>
        public static T ShallowCopy<T>(this T @this)
        {
            MethodInfo method = @this.GetType().GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)method.Invoke(@this, null);
        }

        public static void ThrowIfNotNull<T>(this T obj, string parameterName)
                            where T : class
        {
            if (obj != null) throw new ArgumentNullException(parameterName);
        }

        public static void ThrowIfNull<T>(this T obj, string parameterName)
                            where T : class
        {
            if (obj == null) throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Converts an <see cref="IConvertible"/> object to the <see cref="T"/> type.
        /// </summary>
        /// <typeparam name="T">Type to be converted to.</typeparam>
        /// <param name="obj">Object to be converted.</param>
        /// <returns>The converted object.</returns>
        public static T To<T>(this IConvertible obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        /// <summary>
        ///     Converts an object to the specified target type or returns the default value if
        ///     those 2 types are not convertible.
        ///     <para>
        ///         If the <paramref name="value" /> can't be convert even if the types are
        ///         convertible with each other, an exception is thrown.
        ///     </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The target type</returns>
        public static T To<T>(this object value, T defaultValue)
        {
            if (value != null)
            {
                var targetType = typeof(T);
                if (value.GetType() == targetType) return (T)value;
                var converter = TypeDescriptor.GetConverter(value);
                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                        return (T)converter.ConvertTo(value, targetType);
                }
                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(value.GetType()))
                        return (T)converter.ConvertFrom(value);
                }
            }
            return defaultValue;
        }

        public static T To<T>(this Object @this)
        {
            if (@this != null)
            {
                Type targetType = typeof(T);
                if (@this.GetType() == targetType)
                {
                    return (T)@this;
                }
                TypeConverter converter = TypeDescriptor.GetConverter(@this);
                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                        return (T)converter.ConvertTo(@this, targetType);
                }
                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(@this.GetType()))
                        return (T)converter.ConvertFrom(@this);
                }
                if (@this == DBNull.Value)
                {
                    return (T)(object)null;
                }
            }
            return (T)@this;
        }

        /// <summary>
        ///     A System.Object extension method that toes the given this.
        /// </summary>
        /// <param name="this">this.</param>
        /// <param name="type">The type.</param>
        /// <returns>An object.</returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///  
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_To
        ///           {
        ///               [TestMethod]
        ///               public void To()
        ///               {
        ///                   string nullValue = null;
        ///                   string value = &quot;1&quot;;
        ///                   object dbNullValue = DBNull.Value;
        ///  
        ///                   // Exemples
        ///                   var result1 = value.To&lt;int&gt;(); // return 1;
        ///                   var result2 = value.To&lt;int?&gt;(); // return 1;
        ///                   var result3 = nullValue.To&lt;int?&gt;(); // return null;
        ///                   var result4 = dbNullValue.To&lt;int?&gt;(); // return null;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(1, result2.Value);
        ///                   Assert.IsFalse(result3.HasValue);
        ///                   Assert.IsFalse(result4.HasValue);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_To
        ///           {
        ///               [TestMethod]
        ///               public void To()
        ///               {
        ///                   string nullValue = null;
        ///                   string value = &quot;1&quot;;
        ///                   object dbNullValue = DBNull.Value;
        ///  
        ///                   // Exemples
        ///                   var result1 = value.To&lt;int&gt;(); // return 1;
        ///                   var result2 = value.To&lt;int?&gt;(); // return 1;
        ///                   var result3 = nullValue.To&lt;int?&gt;(); // return null;
        ///                   var result4 = dbNullValue.To&lt;int?&gt;(); // return null;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(1, result2.Value);
        ///                   Assert.IsFalse(result3.HasValue);
        ///                   Assert.IsFalse(result4.HasValue);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        public static object To(this Object @this, Type type)
        {
            if (@this != null)
            {
                Type targetType = type;

                if (@this.GetType() == targetType)
                {
                    return @this;
                }

                TypeConverter converter = TypeDescriptor.GetConverter(@this);
                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                    {
                        return converter.ConvertTo(@this, targetType);
                    }
                }

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(@this.GetType()))
                    {
                        return converter.ConvertFrom(@this);
                    }
                }

                if (@this == DBNull.Value)
                {
                    return null;
                }
            }

            return @this;
        }

        public static string ToBinaryString(this byte[] array)
        {
            var s = new StringBuilder();
            foreach (byte b in array)
                s.Append(Convert.ToString(b, 2));

            return s.ToString();
        }

        /// <summary>
        ///     An object extension method that converts the @this to a boolean.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a bool.</returns>
        public static bool ToBoolean(this object @this)
        {
            return Convert.ToBoolean(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a bool.</returns>
        public static bool ToBooleanOrDefault(this object @this)
        {
            try
            {
                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return default(bool);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">true to default value.</param>
        /// <returns>The given data converted to a bool.</returns>
        public static bool ToBooleanOrDefault(this object @this, bool defaultValue)
        {
            try
            {
                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a bool.</returns>
        public static bool ToBooleanOrDefault(this object @this, Func<bool> defaultValueFactory)
        {
            try
            {
                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a byte.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a byte.</returns>
        public static byte ToByte(this object @this)
        {
            return Convert.ToByte(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a byte.</returns>
        public static byte ToByteOrDefault(this object @this)
        {
            try
            {
                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return default(byte);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a byte.</returns>
        public static byte ToByteOrDefault(this object @this, byte defaultValue)
        {
            try
            {
                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a byte.</returns>
        public static byte ToByteOrDefault(this object @this, Func<byte> defaultValueFactory)
        {
            try
            {
                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a character.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a char.</returns>
        public static char ToChar(this object @this)
        {
            return Convert.ToChar(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a char.</returns>
        public static char ToCharOrDefault(this object @this)
        {
            try
            {
                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return default(char);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a char.</returns>
        public static char ToCharOrDefault(this object @this, char defaultValue)
        {
            try
            {
                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a char.</returns>
        public static char ToCharOrDefault(this object @this, Func<char> defaultValueFactory)
        {
            try
            {
                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     Converts the specified value to a database value and returns DBNull.Value if the value equals its default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object ToDatabaseValue<T>(this T value)
        {
            return value.Equals(value.GetTypeDefaultValue()) ? DBNull.Value : (object)value;
        }

        /// <summary>
        ///     An object extension method that converts the @this to a date time.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a DateTime.</returns>
        public static DateTime ToDateTime(this object @this)
        {
            return Convert.ToDateTime(@this);
        }

        /// <summary>
        ///     An object extension method that converts the @this to a date time off set.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a DateTimeOffset.</returns>
        public static DateTimeOffset ToDateTimeOffSet(this object @this)
        {
            return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
        }

        /// <summary>
        ///     An object extension method that converts this object to a date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a DateTimeOffset.</returns>
        public static DateTimeOffset ToDateTimeOffSetOrDefault(this object @this)
        {
            try
            {
                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return default(DateTimeOffset);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a DateTimeOffset.</returns>
        public static DateTimeOffset ToDateTimeOffSetOrDefault(this object @this, DateTimeOffset defaultValue)
        {
            try
            {
                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a DateTimeOffset.</returns>
        public static DateTimeOffset ToDateTimeOffSetOrDefault(this object @this, Func<DateTimeOffset> defaultValueFactory)
        {
            try
            {
                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a DateTime.</returns>
        public static DateTime ToDateTimeOrDefault(this object @this)
        {
            try
            {
                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return default(DateTime);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a DateTime.</returns>
        public static DateTime ToDateTimeOrDefault(this object @this, DateTime defaultValue)
        {
            try
            {
                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a DateTime.</returns>
        public static DateTime ToDateTimeOrDefault(this object @this, Func<DateTime> defaultValueFactory)
        {
            try
            {
                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a decimal.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a decimal.</returns>
        public static decimal ToDecimal(this object @this)
        {
            return Convert.ToDecimal(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a decimal.</returns>
        public static decimal ToDecimalOrDefault(this object @this)
        {
            try
            {
                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return default(decimal);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a decimal.</returns>
        public static decimal ToDecimalOrDefault(this object @this, decimal defaultValue)
        {
            try
            {
                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a decimal.</returns>
        public static decimal ToDecimalOrDefault(this object @this, Func<decimal> defaultValueFactory)
        {
            try
            {
                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     Returns a Dictionary containing Key/Value pairs that match the objects properties and their values
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this object obj)
        {
            if (obj is IDictionary<string, object> objects)
            {
                return objects;
            }
            if (obj is NameValueCollection)
            {
                return ((NameValueCollection)obj).ToDictionary();
            }

            var d = new Dictionary<string, object>();
            foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(obj))
            {
                d.Add(p.Name, p.GetValue(obj));
            }
            return d;
        }

        /// <summary>
        ///     An object extension method that converts the @this to a double.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a double.</returns>
        public static double ToDouble(this object @this)
        {
            return Convert.ToDouble(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a double.</returns>
        public static double ToDoubleOrDefault(this object @this)
        {
            try
            {
                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return default(double);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a double.</returns>
        public static double ToDoubleOrDefault(this object @this, double defaultValue)
        {
            try
            {
                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a double.</returns>
        public static double ToDoubleOrDefault(this object @this, Func<double> defaultValueFactory)
        {
            try
            {
                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        public static IEnumerable<T> ToEnumerableObject<T>(this T obj)
        {
            var list = new List<T>();
            list.Add(obj);
            return list;
        }

        /// <summary>
        ///     An object extension method that converts the @this to a float.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a float.</returns>
        public static float ToFloat(this object @this)
        {
            return Convert.ToSingle(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToFloatOrDefault(this object @this)
        {
            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return default(float);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToFloatOrDefault(this object @this, float defaultValue)
        {
            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToFloatOrDefault(this object @this, Func<float> defaultValueFactory)
        {
            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a unique identifier.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a GUID.</returns>
        public static Guid ToGuid(this object @this)
        {
            return new Guid(@this.ToString());
        }

        /// <summary>
        ///     An object extension method that converts this object to a unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a GUID.</returns>
        public static Guid ToGuidOrDefault(this object @this)
        {
            try
            {
                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a GUID.</returns>
        public static Guid ToGuidOrDefault(this object @this, Guid defaultValue)
        {
            try
            {
                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a GUID.</returns>
        public static Guid ToGuidOrDefault(this object @this, Func<Guid> defaultValueFactory)
        {
            try
            {
                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to an int 16.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a short.</returns>
        public static short ToInt16(this object @this)
        {
            return Convert.ToInt16(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to an int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToInt16OrDefault(this object @this)
        {
            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return default(short);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToInt16OrDefault(this object @this, short defaultValue)
        {
            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToInt16OrDefault(this object @this, Func<short> defaultValueFactory)
        {
            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to an int 32.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an int.</returns>
        public static int ToInt32(this object @this)
        {
            return Convert.ToInt32(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to an int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an int.</returns>
        public static int ToInt32OrDefault(this object @this)
        {
            try
            {
                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return default(int);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an int.</returns>
        public static int ToInt32OrDefault(this object @this, int defaultValue)
        {
            try
            {
                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an int.</returns>
        public static int ToInt32OrDefault(this object @this, Func<int> defaultValueFactory)
        {
            try
            {
                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to an int 64.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a long.</returns>
        public static long ToInt64(this object @this)
        {
            return Convert.ToInt64(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to an int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToInt64OrDefault(this object @this)
        {
            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return default(long);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToInt64OrDefault(this object @this, long defaultValue)
        {
            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToInt64OrDefault(this object @this, Func<long> defaultValueFactory)
        {
            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a long.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a long.</returns>
        public static long ToLong(this object @this)
        {
            return Convert.ToInt64(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToLongOrDefault(this object @this)
        {
            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return default(long);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToLongOrDefault(this object @this, long defaultValue)
        {
            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a long.</returns>
        public static long ToLongOrDefault(this object @this, Func<long> defaultValueFactory)
        {
            try
            {
                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable boolean.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a bool?</returns>
        public static bool? ToNullableBoolean(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToBoolean(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a bool?</returns>
        public static bool? ToNullableBooleanOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return default(bool);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a bool?</returns>
        public static bool? ToNullableBooleanOrDefault(this object @this, bool? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable boolean or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a bool?</returns>
        public static bool? ToNullableBooleanOrDefault(this object @this, Func<bool?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToBoolean(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable byte.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a byte?</returns>
        public static byte? ToNullableByte(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToByte(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a byte?</returns>
        public static byte? ToNullableByteOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return default(byte);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a byte?</returns>
        public static byte? ToNullableByteOrDefault(this object @this, byte? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a byte?</returns>
        public static byte? ToNullableByteOrDefault(this object @this, Func<byte?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToByte(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable character.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a char?</returns>
        public static char? ToNullableChar(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToChar(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a char?</returns>
        public static char? ToNullableCharOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return default(char);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a char?</returns>
        public static char? ToNullableCharOrDefault(this object @this, char? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable character or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a char?</returns>
        public static char? ToNullableCharOrDefault(this object @this, Func<char?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToChar(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable date time.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a DateTime?</returns>
        public static DateTime? ToNullableDateTime(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDateTime(@this);
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable date time off set.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a DateTimeOffset?</returns>
        public static DateTimeOffset? ToNullableDateTimeOffSet(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a DateTimeOffset?</returns>
        public static DateTimeOffset? ToNullableDateTimeOffSetOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return default(DateTimeOffset);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a DateTimeOffset?</returns>
        public static DateTimeOffset? ToNullableDateTimeOffSetOrDefault(this object @this, DateTimeOffset? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable date time off set or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a DateTimeOffset?</returns>
        public static DateTimeOffset? ToNullableDateTimeOffSetOrDefault(this object @this, Func<DateTimeOffset?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a DateTime?</returns>
        public static DateTime? ToNullableDateTimeOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return default(DateTime);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a DateTime?</returns>
        public static DateTime? ToNullableDateTimeOrDefault(this object @this, DateTime? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable date time or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a DateTime?</returns>
        public static DateTime? ToNullableDateTimeOrDefault(this object @this, Func<DateTime?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDateTime(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable decimal.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a decimal?</returns>
        public static decimal? ToNullableDecimal(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDecimal(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a decimal?</returns>
        public static decimal? ToNullableDecimalOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return default(decimal);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a decimal?</returns>
        public static decimal? ToNullableDecimalOrDefault(this object @this, decimal? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable decimal or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a decimal?</returns>
        public static decimal? ToNullableDecimalOrDefault(this object @this, Func<decimal?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDecimal(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable double.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a double?</returns>
        public static double? ToNullableDouble(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDouble(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a double?</returns>
        public static double? ToNullableDoubleOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return default(double);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a double?</returns>
        public static double? ToNullableDoubleOrDefault(this object @this, double? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable double or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a double?</returns>
        public static double? ToNullableDoubleOrDefault(this object @this, Func<double?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToDouble(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable float.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a float?</returns>
        public static float? ToNullableFloat(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSingle(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a float?</returns>
        public static float? ToNullableFloatOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return default(float);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a float?</returns>
        public static float? ToNullableFloatOrDefault(this object @this, float? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable float or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a float?</returns>
        public static float? ToNullableFloatOrDefault(this object @this, Func<float?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable unique identifier.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a Guid?</returns>
        public static Guid? ToNullableGuid(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return new Guid(@this.ToString());
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a Guid?</returns>
        public static Guid? ToNullableGuidOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a Guid?</returns>
        public static Guid? ToNullableGuidOrDefault(this object @this, Guid? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable unique identifier or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a Guid?</returns>
        public static Guid? ToNullableGuidOrDefault(this object @this, Func<Guid?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return new Guid(@this.ToString());
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable int 16.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a short?</returns>
        public static short? ToNullableInt16(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt16(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a short?</returns>
        public static short? ToNullableInt16OrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return default(short);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a short?</returns>
        public static short? ToNullableInt16OrDefault(this object @this, short? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a short?</returns>
        public static short? ToNullableInt16OrDefault(this object @this, Func<short?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable int 32.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an int?</returns>
        public static int? ToNullableInt32(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt32(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an int?</returns>
        public static int? ToNullableInt32OrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return default(int);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an int?</returns>
        public static int? ToNullableInt32OrDefault(this object @this, int? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an int?</returns>
        public static int? ToNullableInt32OrDefault(this object @this, Func<int?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt32(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable int 64.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a long?</returns>
        public static long? ToNullableInt64(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt64(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a long?</returns>
        public static long? ToNullableInt64OrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return default(long);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a long?</returns>
        public static long? ToNullableInt64OrDefault(this object @this, long? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a long?</returns>
        public static long? ToNullableInt64OrDefault(this object @this, Func<long?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable long.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a long?</returns>
        public static long? ToNullableLong(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt64(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a long?</returns>
        public static long? ToNullableLongOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return default(long);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a long?</returns>
        public static long? ToNullableLongOrDefault(this object @this, long? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a long?</returns>
        public static long? ToNullableLongOrDefault(this object @this, Func<long?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable s byte.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a sbyte?</returns>
        public static sbyte? ToNullableSByte(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSByte(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a sbyte?</returns>
        public static sbyte? ToNullableSByteOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return default(sbyte);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a sbyte?</returns>
        public static sbyte? ToNullableSByteOrDefault(this object @this, sbyte? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a sbyte?</returns>
        public static sbyte? ToNullableSByteOrDefault(this object @this, Func<sbyte?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable short.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a short?</returns>
        public static short? ToNullableShort(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt16(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a short?</returns>
        public static short? ToNullableShortOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return default(short);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a short?</returns>
        public static short? ToNullableShortOrDefault(this object @this, short? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a short?</returns>
        public static short? ToNullableShortOrDefault(this object @this, Func<short?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable single.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a float?</returns>
        public static float? ToNullableSingle(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSingle(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a float?</returns>
        public static float? ToNullableSingleOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return default(float);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a float?</returns>
        public static float? ToNullableSingleOrDefault(this object @this, float? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a float?</returns>
        public static float? ToNullableSingleOrDefault(this object @this, Func<float?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable u int 16.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ushort?</returns>
        public static ushort? ToNullableUInt16(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt16(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ushort?</returns>
        public static ushort? ToNullableUInt16OrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return default(ushort);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ushort?</returns>
        public static ushort? ToNullableUInt16OrDefault(this object @this, ushort? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ushort?</returns>
        public static ushort? ToNullableUInt16OrDefault(this object @this, Func<ushort?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable u int 32.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an uint?</returns>
        public static uint? ToNullableUInt32(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt32(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an uint?</returns>
        public static uint? ToNullableUInt32OrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return default(uint);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an uint?</returns>
        public static uint? ToNullableUInt32OrDefault(this object @this, uint? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an uint?</returns>
        public static uint? ToNullableUInt32OrDefault(this object @this, Func<uint?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable u int 64.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ulong?</returns>
        public static ulong? ToNullableUInt64(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt64(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ulong?</returns>
        public static ulong? ToNullableUInt64OrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return default(ulong);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ulong?</returns>
        public static ulong? ToNullableUInt64OrDefault(this object @this, ulong? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ulong?</returns>
        public static ulong? ToNullableUInt64OrDefault(this object @this, Func<ulong?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable u long.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ulong?</returns>
        public static ulong? ToNullableULong(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt64(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ulong?</returns>
        public static ulong? ToNullableULongOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return default(ulong);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ulong?</returns>
        public static ulong? ToNullableULongOrDefault(this object @this, ulong? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ulong?</returns>
        public static ulong? ToNullableULongOrDefault(this object @this, Func<ulong?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a nullable u short.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ushort?</returns>
        public static ushort? ToNullableUShort(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt16(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ushort?</returns>
        public static ushort? ToNullableUShortOrDefault(this object @this)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return default(ushort);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ushort?</returns>
        public static ushort? ToNullableUShortOrDefault(this object @this, ushort? defaultValue)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a nullable u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ushort?</returns>
        public static ushort? ToNullableUShortOrDefault(this object @this, Func<ushort?> defaultValueFactory)
        {
            try
            {
                if (@this == null || @this == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// Converts an <see cref="IConvertible"/> object to the <see cref="T"/> type or returns the default value of the <see cref="T"/> type.
        /// </summary>
        /// <typeparam name="T">Type to be converted to.</typeparam>
        /// <param name="obj">Object to be converted.</param>
        /// <returns>The converted object or its default value.</returns>
        public static T ToOrDefault<T>(this IConvertible obj)
        {
            try
            {
                return To<T>(obj);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Converts an <see cref="IConvertible"/> object to the <see cref="T"/> type and assigns it to the <paramref name="newObj"/> or assigns the default value of the <see cref="T"/> type.
        /// </summary>
        /// <typeparam name="T">Type to be converted to.</typeparam>
        /// <param name="obj">Object to be converted.</param>
        /// <param name="newObj">Object to which the result is assigned.</param>
        /// <returns>True if conversion is successful or false otherwise.</returns>
        public static bool ToOrDefault<T>(this IConvertible obj, out T newObj)
        {
            try
            {
                newObj = To<T>(obj);
                return true;
            }
            catch
            {
                newObj = default(T);
                return false;
            }
        }

        /// <summary>
        ///     A System.Object extension method that converts this object to an or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">this.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a T.</returns>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_ToOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void ToOrDefault()
        ///               {
        ///                   // Type
        ///                   object intValue = &quot;1&quot;;
        ///                   object invalidValue = &quot;Fizz&quot;;
        ///  
        ///                   // Exemples
        ///                   var result1 = intValue.ToOrDefault&lt;int&gt;(); // return 1;
        ///                   var result2 = invalidValue.ToOrDefault&lt;int&gt;(); // return 0;
        ///                   int result3 = invalidValue.ToOrDefault(3); // return 3;
        ///                   int result4 = invalidValue.ToOrDefault(() =&gt; 4); // return 4;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(0, result2);
        ///                   Assert.AreEqual(3, result3);
        ///                   Assert.AreEqual(4, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_ToOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void ToOrDefault()
        ///               {
        ///                   // Type
        ///                   object intValue = &quot;1&quot;;
        ///                   object invalidValue = &quot;Fizz&quot;;
        ///  
        ///                   // Exemples
        ///                   var result1 = intValue.ToOrDefault&lt;int&gt;(); // return 1;
        ///                   var result2 = invalidValue.ToOrDefault&lt;int&gt;(); // return 0;
        ///                   int result3 = invalidValue.ToOrDefault(3); // return 3;
        ///                   int result4 = invalidValue.ToOrDefault(() =&gt; 4); // return 4;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(0, result2);
        ///                   Assert.AreEqual(3, result3);
        ///                   Assert.AreEqual(4, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_ToOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void ToOrDefault()
        ///                   {
        ///                       // Type
        ///                       object intValue = &quot;1&quot;;
        ///                       object invalidValue = &quot;Fizz&quot;;
        ///           
        ///                       // Exemples
        ///                       var result1 = intValue.ToOrDefault&lt;int&gt;(); // return 1;
        ///                       var result2 = invalidValue.ToOrDefault&lt;int&gt;(); // return 0;
        ///                       int result3 = invalidValue.ToOrDefault(3); // return 3;
        ///                       int result4 = invalidValue.ToOrDefault(() =&gt; 4); // return 4;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(1, result1);
        ///                       Assert.AreEqual(0, result2);
        ///                       Assert.AreEqual(3, result3);
        ///                       Assert.AreEqual(4, result4);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static T ToOrDefault<T>(this Object @this, Func<object, T> defaultValueFactory)
        {
            try
            {
                if (@this != null)
                {
                    Type targetType = typeof(T);

                    if (@this.GetType() == targetType)
                    {
                        return (T)@this;
                    }

                    TypeConverter converter = TypeDescriptor.GetConverter(@this);
                    if (converter != null)
                    {
                        if (converter.CanConvertTo(targetType))
                        {
                            return (T)converter.ConvertTo(@this, targetType);
                        }
                    }

                    converter = TypeDescriptor.GetConverter(targetType);
                    if (converter != null)
                    {
                        if (converter.CanConvertFrom(@this.GetType()))
                        {
                            return (T)converter.ConvertFrom(@this);
                        }
                    }

                    if (@this == DBNull.Value)
                    {
                        return (T)(object)null;
                    }
                }

                return (T)@this;
            }
            catch (Exception)
            {
                return defaultValueFactory(@this);
            }
        }

        /// <summary>
        ///     A System.Object extension method that converts this object to an or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">this.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a T.</returns>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_ToOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void ToOrDefault()
        ///               {
        ///                   // Type
        ///                   object intValue = &quot;1&quot;;
        ///                   object invalidValue = &quot;Fizz&quot;;
        ///  
        ///                   // Exemples
        ///                   var result1 = intValue.ToOrDefault&lt;int&gt;(); // return 1;
        ///                   var result2 = invalidValue.ToOrDefault&lt;int&gt;(); // return 0;
        ///                   int result3 = invalidValue.ToOrDefault(3); // return 3;
        ///                   int result4 = invalidValue.ToOrDefault(() =&gt; 4); // return 4;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(0, result2);
        ///                   Assert.AreEqual(3, result3);
        ///                   Assert.AreEqual(4, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        ///  
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_ToOrDefault
        ///           {
        ///               [TestMethod]
        ///               public void ToOrDefault()
        ///               {
        ///                   // Type
        ///                   object intValue = &quot;1&quot;;
        ///                   object invalidValue = &quot;Fizz&quot;;
        ///  
        ///                   // Exemples
        ///                   var result1 = intValue.ToOrDefault&lt;int&gt;(); // return 1;
        ///                   var result2 = invalidValue.ToOrDefault&lt;int&gt;(); // return 0;
        ///                   int result3 = invalidValue.ToOrDefault(3); // return 3;
        ///                   int result4 = invalidValue.ToOrDefault(() =&gt; 4); // return 4;
        ///  
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(0, result2);
        ///                   Assert.AreEqual(3, result3);
        ///                   Assert.AreEqual(4, result4);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///           using Z.ExtensionMethods.Object;
        ///           
        ///           namespace ExtensionMethods.Examples
        ///           {
        ///               [TestClass]
        ///               public class System_Object_ToOrDefault
        ///               {
        ///                   [TestMethod]
        ///                   public void ToOrDefault()
        ///                   {
        ///                       // Type
        ///                       object intValue = &quot;1&quot;;
        ///                       object invalidValue = &quot;Fizz&quot;;
        ///           
        ///                       // Exemples
        ///                       var result1 = intValue.ToOrDefault&lt;int&gt;(); // return 1;
        ///                       var result2 = invalidValue.ToOrDefault&lt;int&gt;(); // return 0;
        ///                       int result3 = invalidValue.ToOrDefault(3); // return 3;
        ///                       int result4 = invalidValue.ToOrDefault(() =&gt; 4); // return 4;
        ///           
        ///                       // Unit Test
        ///                       Assert.AreEqual(1, result1);
        ///                       Assert.AreEqual(0, result2);
        ///                       Assert.AreEqual(3, result3);
        ///                       Assert.AreEqual(4, result4);
        ///                   }
        ///               }
        ///           }
        ///     </code>
        /// </example>
        public static T ToOrDefault<T>(this Object @this, Func<T> defaultValueFactory)
        {
            return @this.ToOrDefault(x => defaultValueFactory());
        }

        /// <summary>
        ///     A System.Object extension method that converts this object to an or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">this.</param>
        /// <returns>The given data converted to a T.</returns>
        public static T ToOrDefault<T>(this Object @this)
        {
            return @this.ToOrDefault(x => default(T));
        }

        /// <summary>
        ///     A System.Object extension method that converts this object to an or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">this.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a T.</returns>
        public static T ToOrDefault<T>(this Object @this, T defaultValue)
        {
            return @this.ToOrDefault(x => defaultValue);
        }

        /// <summary>
        /// Converts an <see cref="IConvertible"/> object to the <see cref="T"/> type or returns null.
        /// </summary>
        /// <typeparam name="T">Type to be converted to.</typeparam>
        /// <param name="obj">Object to be converted.</param>
        /// <returns>The converted object or null.</returns>
        public static T ToOrNull<T>(this IConvertible obj)
                            where T : class
        {
            try
            {
                return To<T>(obj);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts an <see cref="IConvertible"/> object to the <see cref="T"/> type and assigns the result to <paramref name="newObj"/>. If that is not possible assigns null.
        /// </summary>
        /// <typeparam name="T">Type to be converted to.</typeparam>
        /// <param name="obj">Object to be converted.</param>
        /// <param name="newObj">Object to which the result is assigned.</param>
        /// <returns>True if conversion is successful or false otherwise.</returns>
        public static bool ToOrNull<T>(this IConvertible obj, out T newObj)
                            where T : class
        {
            try
            {
                newObj = To<T>(obj);
                return true;
            }
            catch
            {
                newObj = null;
                return false;
            }
        }

        /// <summary>
        /// Converts an <see cref="IConvertible"/> object to the <see cref="T"/> type or returns the <paramref name="other"/> object.
        /// </summary>
        /// <typeparam name="T">Type to be converted to.</typeparam>
        /// <param name="obj">Object to be converted.</param>
        /// <param name="other">Object to be returned in case of failure.</param>
        /// <returns>The converted object of type <see cref="T"/> or its alternative <paramref name="other"/></returns>
        public static T ToOrOther<T>(this IConvertible obj, T other)
        {
            try
            {
                return To<T>(obj);
            }
            catch
            {
                return other;
            }
        }

        /// <summary>
        /// Converts an <see cref="IConvertible"/> object to the <see cref="T"/> type and assigns it to the <paramref name="newObj"/>. If that is not possible assigns the <paramref name="other"/> to <paramref name="newObj"/>.
        /// </summary>
        /// <typeparam name="T">Type to be converted to.</typeparam>
        /// <param name="obj">Object to be converted.</param>
        /// <param name="newObj">Object to which the result is assigned.</param>
        /// <param name="other">Object to be assigned in case of failure.</param>
        /// <returns>True if conversion is successful or false otherwise.</returns>
        public static bool ToOrOther<T>(this IConvertible obj, out T newObj, T other)
        {
            try
            {
                newObj = To<T>(obj);
                return true;
            }
            catch
            {
                newObj = other;
                return false;
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to the s byte.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a sbyte.</returns>
        public static sbyte ToSByte(this object @this)
        {
            return Convert.ToSByte(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to the s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a sbyte.</returns>
        public static sbyte ToSByteOrDefault(this object @this)
        {
            try
            {
                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return default(sbyte);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to the s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a sbyte.</returns>
        public static sbyte ToSByteOrDefault(this object @this, sbyte defaultValue)
        {
            try
            {
                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to the s byte or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a sbyte.</returns>
        public static sbyte ToSByteOrDefault(this object @this, Func<sbyte> defaultValueFactory)
        {
            try
            {
                return Convert.ToSByte(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a short.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a short.</returns>
        public static short ToShort(this object @this)
        {
            return Convert.ToInt16(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to a short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToShortOrDefault(this object @this)
        {
            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return default(short);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToShortOrDefault(this object @this, short defaultValue)
        {
            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a short.</returns>
        public static short ToShortOrDefault(this object @this, Func<short> defaultValueFactory)
        {
            try
            {
                return Convert.ToInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to a single.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a float.</returns>
        public static float ToSingle(this object @this)
        {
            return Convert.ToSingle(@this);
        }

        /// <summary>
        /// Creates an Array containing this item as it's only memeber.
        /// </summary>
        public static T[] ToSingleItemArray<T>(this T obj)
        {
            return new[] { obj };
        }

        /// <summary>
        /// Creates an Enumerable containing this item as it's only memeber.
        /// </summary>
        public static IEnumerable<T> ToSingleItemEnumerable<T>(this T obj)
        {
            yield return obj;
        }

        /// <summary>
        /// Creates a List containing this item as it's only memeber.
        /// </summary>
        public static IList<T> ToSingleItemList<T>(this T obj)
        {
            return new List<T> { obj };
        }

        /// <summary>
        ///     An object extension method that converts this object to a single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToSingleOrDefault(this object @this)
        {
            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return default(float);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToSingleOrDefault(this object @this, float defaultValue)
        {
            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to a single or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to a float.</returns>
        public static float ToSingleOrDefault(this object @this, Func<float> defaultValueFactory)
        {
            try
            {
                return Convert.ToSingle(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to an u int 16.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ushort.</returns>
        public static ushort ToUInt16(this object @this)
        {
            return Convert.ToUInt16(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to an u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUInt16OrDefault(this object @this)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return default(ushort);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUInt16OrDefault(this object @this, ushort defaultValue)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an u int 16 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUInt16OrDefault(this object @this, Func<ushort> defaultValueFactory)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to an u int 32.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an uint.</returns>
        public static uint ToUInt32(this object @this)
        {
            return Convert.ToUInt32(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to an u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an uint.</returns>
        public static uint ToUInt32OrDefault(this object @this)
        {
            try
            {
                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return default(uint);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an uint.</returns>
        public static uint ToUInt32OrDefault(this object @this, uint defaultValue)
        {
            try
            {
                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an u int 32 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an uint.</returns>
        public static uint ToUInt32OrDefault(this object @this, Func<uint> defaultValueFactory)
        {
            try
            {
                return Convert.ToUInt32(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to an u int 64.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ulong.</returns>
        public static ulong ToUInt64(this object @this)
        {
            return Convert.ToUInt64(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to an u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToUInt64OrDefault(this object @this)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return default(ulong);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToUInt64OrDefault(this object @this, ulong defaultValue)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an u int 64 or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToUInt64OrDefault(this object @this, Func<ulong> defaultValueFactory)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to an u long.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ulong.</returns>
        public static ulong ToULong(this object @this)
        {
            return Convert.ToUInt64(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to an u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToULongOrDefault(this object @this)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return default(ulong);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToULongOrDefault(this object @this, ulong defaultValue)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an u long or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ulong.</returns>
        public static ulong ToULongOrDefault(this object @this, Func<ulong> defaultValueFactory)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        ///     An object extension method that converts the @this to an u short.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an ushort.</returns>
        public static ushort ToUShort(this object @this)
        {
            return Convert.ToUInt16(@this);
        }

        /// <summary>
        ///     An object extension method that converts this object to an u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUShortOrDefault(this object @this)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return default(ushort);
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUShortOrDefault(this object @this, ushort defaultValue)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An object extension method that converts this object to an u short or default.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The given data converted to an ushort.</returns>
        public static ushort ToUShortOrDefault(this object @this, Func<ushort> defaultValueFactory)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>A TType extension method that tries.</summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryFunction">The try function.</param>
        /// <returns>A TResult.</returns>
        public static TResult Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction)
        {
            try
            {
                return tryFunction(@this);
            }
            catch
            {
                return default(TResult);
            }
        }

        /// <summary>A TType extension method that tries.</summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryFunction">The try function.</param>
        /// <param name="catchValue">The catch value.</param>
        /// <returns>A TResult.</returns>
        public static TResult Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, TResult catchValue)
        {
            try
            {
                return tryFunction(@this);
            }
            catch
            {
                return catchValue;
            }
        }

        /// <summary>A TType extension method that tries.</summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryFunction">The try function.</param>
        /// <param name="catchValueFactory">The catch value factory.</param>
        /// <returns>A TResult.</returns>
        public static TResult Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, Func<TType, TResult> catchValueFactory)
        {
            try
            {
                return tryFunction(@this);
            }
            catch
            {
                return catchValueFactory(@this);
            }
        }

        /// <summary>A TType extension method that tries.</summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryFunction">The try function.</param>
        /// <param name="result">[out] The result.</param>
        /// <returns>A TResult.</returns>
        public static bool Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, out TResult result)
        {
            try
            {
                result = tryFunction(@this);
                return true;
            }
            catch
            {
                result = default(TResult);
                return false;
            }
        }

        /// <summary>A TType extension method that tries.</summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryFunction">The try function.</param>
        /// <param name="catchValue">The catch value.</param>
        /// <param name="result">[out] The result.</param>
        /// <returns>A TResult.</returns>
        public static bool Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, TResult catchValue, out TResult result)
        {
            try
            {
                result = tryFunction(@this);
                return true;
            }
            catch
            {
                result = catchValue;
                return false;
            }
        }

        /// <summary>A TType extension method that tries.</summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryFunction">The try function.</param>
        /// <param name="catchValueFactory">The catch value factory.</param>
        /// <param name="result">[out] The result.</param>
        /// <returns>A TResult.</returns>
        public static bool Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, Func<TType, TResult> catchValueFactory, out TResult result)
        {
            try
            {
                result = tryFunction(@this);
                return true;
            }
            catch
            {
                result = catchValueFactory(@this);
                return false;
            }
        }

        /// <summary>A TType extension method that attempts to action from the given data.</summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryAction">The try action.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool Try<TType>(this TType @this, Action<TType> tryAction)
        {
            try
            {
                tryAction(@this);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>A TType extension method that attempts to action from the given data.</summary>
        /// <typeparam name="TType">Type of the type.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="tryAction">The try action.</param>
        /// <param name="catchAction">The catch action.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool Try<TType>(this TType @this, Action<TType> tryAction, Action<TType> catchAction)
        {
            try
            {
                tryAction(@this);
                return true;
            }
            catch
            {
                catchAction(@this);
                return false;
            }
        }

        public static bool TryDispose(this object toDispose)
        {
            if (!(toDispose is IDisposable disposable))
                return false;

            disposable.Dispose();
            return true;
        }

        public static string XmlSerialize<T>(this T obj) where T : class, new()
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }
    }
}
