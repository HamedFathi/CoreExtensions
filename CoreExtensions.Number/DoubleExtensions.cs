using System;

namespace CoreExtensions
{
    public static class DoubleExtensions
    {
        /// <summary>
        ///     Returns the absolute value of a double-precision floating-point number.
        /// </summary>
        /// <param name="value">A number that is greater than or equal to , but less than or equal to .</param>
        /// <returns>A double-precision floating-point number, x, such that 0 ? x ?.</returns>
        public static Double Abs(this Double value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        ///     Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="d">
        ///     A number representing a cosine, where  must be greater than or equal to -1, but less than or
        ///     equal to 1.
        /// </param>
        /// <returns>An angle, ?, measured in radians, such that 0 ????-or-  if  &lt; -1 or  &gt; 1 or  equals .</returns>
        public static Double Acos(this Double d)
        {
            return Math.Acos(d);
        }

        /// <summary>
        ///     Returns the angle whose sine is the specified number.
        /// </summary>
        /// <param name="d">
        ///     A number representing a sine, where  must be greater than or equal to -1, but less than or equal
        ///     to 1.
        /// </param>
        /// <returns>
        ///     An angle, ?, measured in radians, such that -?/2 ????/2 -or-  if  &lt; -1 or  &gt; 1 or  equals .
        /// </returns>
        public static Double Asin(this Double d)
        {
            return Math.Asin(d);
        }

        /// <summary>
        ///     Returns the angle whose tangent is the specified number.
        /// </summary>
        /// <param name="d">A number representing a tangent.</param>
        /// <returns>
        ///     An angle, ?, measured in radians, such that -?/2 ????/2.-or-  if  equals , -?/2 rounded to double precision (-
        ///     1.5707963267949) if  equals , or ?/2 rounded to double precision (1.5707963267949) if  equals .
        /// </returns>
        public static Double Atan(this Double d)
        {
            return Math.Atan(d);
        }

        /// <summary>
        ///     Returns the angle whose tangent is the quotient of two specified numbers.
        /// </summary>
        /// <param name="y">The y coordinate of a point.</param>
        /// <param name="x">The x coordinate of a point.</param>
        /// <returns>
        ///     An angle, ?, measured in radians, such that -?????, and tan(?) =  / , where (, ) is a point in the Cartesian
        ///     plane. Observe the following: For (, ) in quadrant 1, 0 &lt; ? &lt; ?/2.For (, ) in quadrant 2, ?/2 &lt;
        ///     ???.For (, ) in quadrant 3, -? &lt; ? &lt; -?/2.For (, ) in quadrant 4, -?/2 &lt; ? &lt; 0.For points on the
        ///     boundaries of the quadrants, the return value is the following:If y is 0 and x is not negative, ? = 0.If y is
        ///     0 and x is negative, ? = ?.If y is positive and x is 0, ? = ?/2.If y is negative and x is 0, ? = -?/2.If  or
        ///     is , or if  and  are either  or , the method returns .
        /// </returns>
        public static Double Atan2(this Double y, Double x)
        {
            return Math.Atan2(y, x);
        }

        /// <summary>
        ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
        /// ###
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public static bool Between(this Double @this, Double minValue, Double maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        /// <summary>
        ///     Returns the smallest integral value that is greater than or equal to the specified double-precision floating-
        ///     point number.
        /// </summary>
        /// <param name="a">A double-precision floating-point number.</param>
        /// <returns>
        ///     The smallest integral value that is greater than or equal to . If  is equal to , , or , that value is
        ///     returned. Note that this method returns a  instead of an integral type.
        /// </returns>
        public static Double Ceiling(this Double a)
        {
            return Math.Ceiling(a);
        }

        /// <summary>
        ///     Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        /// <returns>The cosine of . If  is equal to , , or , this method returns .</returns>
        public static Double Cos(this Double d)
        {
            return Math.Cos(d);
        }

        /// <summary>
        ///     Returns the hyperbolic cosine of the specified angle.
        /// </summary>
        /// <param name="value">An angle, measured in radians.</param>
        /// <returns>The hyperbolic cosine of . If  is equal to  or ,  is returned. If  is equal to ,  is returned.</returns>
        public static Double Cosh(this Double value)
        {
            return Math.Cosh(value);
        }

        /// <summary>
        /// Gets a TimeSpan from a double number of days.
        /// </summary>
        /// <param name="days">The number of days the TimeSpan will contain.</param>
        /// <returns>A TimeSpan containing the specified number of days.</returns>
        /// <remarks>
        /// 	Contributed by jceddy
        /// </remarks>
        public static TimeSpan Days(this double days)
        {
            return TimeSpan.FromDays(days);
        }

        /// <summary>
        ///     Returns e raised to the specified power.
        /// </summary>
        /// <param name="d">A number specifying a power.</param>
        /// <returns>
        ///     The number e raised to the power . If  equals  or , that value is returned. If  equals , 0 is returned.
        /// </returns>
        public static Double Exp(this Double d)
        {
            return Math.Exp(d);
        }

        /// <summary>
        ///     Returns the largest integer less than or equal to the specified double-precision floating-point number.
        /// </summary>
        /// <param name="d">A double-precision floating-point number.</param>
        /// <returns>The largest integer less than or equal to . If  is equal to , , or , that value is returned.</returns>
        public static Double Floor(this Double d)
        {
            return Math.Floor(d);
        }

        /// <summary>
        ///     Returns a  that represents a specified number of days, where the specification is accurate to the nearest
        ///     millisecond.
        /// </summary>
        /// <param name="value">A number of days, accurate to the nearest millisecond.</param>
        /// <returns>An object that represents .</returns>
        public static TimeSpan FromDays(this Double value)
        {
            return TimeSpan.FromDays(value);
        }

        /// <summary>
        ///     Returns a  that represents a specified number of hours, where the specification is accurate to the nearest
        ///     millisecond.
        /// </summary>
        /// <param name="value">A number of hours accurate to the nearest millisecond.</param>
        /// <returns>An object that represents .</returns>
        public static TimeSpan FromHours(this Double value)
        {
            return TimeSpan.FromHours(value);
        }

        /// <summary>
        ///     Returns a  that represents a specified number of milliseconds.
        /// </summary>
        /// <param name="value">A number of milliseconds.</param>
        /// <returns>An object that represents .</returns>
        public static TimeSpan FromMilliseconds(this Double value)
        {
            return TimeSpan.FromMilliseconds(value);
        }

        /// <summary>
        ///     Returns a  that represents a specified number of minutes, where the specification is accurate to the nearest
        ///     millisecond.
        /// </summary>
        /// <param name="value">A number of minutes, accurate to the nearest millisecond.</param>
        /// <returns>An object that represents .</returns>
        public static TimeSpan FromMinutes(this Double value)
        {
            return TimeSpan.FromMinutes(value);
        }

        /// <summary>
        ///     Returns a  that represents a specified number of seconds, where the specification is accurate to the nearest
        ///     millisecond.
        /// </summary>
        /// <param name="value">A number of seconds, accurate to the nearest millisecond.</param>
        /// <returns>An object that represents .</returns>
        public static TimeSpan FromSeconds(this Double value)
        {
            return TimeSpan.FromSeconds(value);
        }

        /// <summary>
        /// Gets a TimeSpan from a double number of hours.
        /// </summary>
        /// <param name="hours"></param>
        /// <returns>A TimeSpan containing the specified number of hours.</returns>
        /// <remarks>
        /// 	Contributed by jceddy
        /// </remarks>
        public static TimeSpan Hours(this double hours)
        {
            return TimeSpan.FromHours(hours);
        }

        /// <summary>
        ///     Returns the remainder resulting from the division of a specified number by another specified number.
        /// </summary>
        /// <param name="x">A dividend.</param>
        /// <param name="y">A divisor.</param>
        /// <returns>
        ///     A number equal to  - ( Q), where Q is the quotient of  /  rounded to the nearest integer (if  /  falls
        ///     halfway between two integers, the even integer is returned).If  - ( Q) is zero, the value +0 is returned if
        ///     is positive, or -0 if  is negative.If  = 0,  is returned.
        /// </returns>
        public static Double IEEERemainder(this Double x, Double y)
        {
            return Math.IEEERemainder(x, y);
        }

        /// <summary>
        ///     A T extension method to determines whether the object is equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list contains the object, else false.</returns>
        /// ###
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public static bool In(this Double @this, params Double[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        /// <summary>
        ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
        /// ###
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public static bool InRange(this Double @this, Double minValue, Double maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        /// <summary>
        ///     Returns a value indicating whether the specified number evaluates to negative or positive infinity.
        /// </summary>
        /// <param name="d">A double-precision floating-point number.</param>
        /// <returns>true if  evaluates to  or ; otherwise, false.</returns>
        public static Boolean IsInfinity(this Double d)
        {
            return Double.IsInfinity(d);
        }

        /// <summary>
        ///     Returns a value that indicates whether the specified value is not a number ().
        /// </summary>
        /// <param name="d">A double-precision floating-point number.</param>
        /// <returns>true if  evaluates to ; otherwise, false.</returns>
        public static Boolean IsNaN(this Double d)
        {
            return Double.IsNaN(d);
        }

        /// <summary>
        ///     Returns a value indicating whether the specified number evaluates to negative infinity.
        /// </summary>
        /// <param name="d">A double-precision floating-point number.</param>
        /// <returns>true if  evaluates to ; otherwise, false.</returns>
        public static Boolean IsNegativeInfinity(this Double d)
        {
            return Double.IsNegativeInfinity(d);
        }

        /// <summary>
        ///     Returns a value indicating whether the specified number evaluates to positive infinity.
        /// </summary>
        /// <param name="d">A double-precision floating-point number.</param>
        /// <returns>true if  evaluates to ; otherwise, false.</returns>
        public static Boolean IsPositiveInfinity(this Double d)
        {
            return Double.IsPositiveInfinity(d);
        }

        /// <summary>
        ///     Returns the natural (base e) logarithm of a specified number.
        /// </summary>
        /// <param name="d">The number whose logarithm is to be found.</param>
        /// <returns>
        ///     One of the values in the following table.  parameterReturn value Positive The natural logarithm of ; that is,
        ///     ln , or log eZero Negative Equal to Equal to.
        /// </returns>
        public static Double Log(this Double d)
        {
            return Math.Log(d);
        }

        /// <summary>
        ///     Returns the logarithm of a specified number in a specified base.
        /// </summary>
        /// <param name="d">The number whose logarithm is to be found.</param>
        /// <param name="newBase">The base of the logarithm.</param>
        /// <returns>
        ///     One of the values in the following table. (+Infinity denotes , -Infinity denotes , and NaN denotes .)Return
        ///     value&gt; 0(0 &lt;&lt; 1) -or-(&gt; 1)lognewBase(a)&lt; 0(any value)NaN(any value)&lt; 0NaN != 1 = 0NaN != 1
        ///     = +InfinityNaN = NaN(any value)NaN(any value) = NaNNaN(any value) = 1NaN = 00 &lt;&lt; 1 +Infinity = 0&gt; 1-
        ///     Infinity =  +Infinity0 &lt;&lt; 1-Infinity =  +Infinity&gt; 1+Infinity = 1 = 00 = 1 = +Infinity0.
        /// </returns>
        /// ###
        /// <param name="a">The number whose logarithm is to be found.</param>
        public static Double Log(this Double d, Double newBase)
        {
            return Math.Log(d, newBase);
        }

        /// <summary>
        ///     Returns the base 10 logarithm of a specified number.
        /// </summary>
        /// <param name="d">A number whose logarithm is to be found.</param>
        /// <returns>
        ///     One of the values in the following table.  parameter Return value Positive The base 10 log of ; that is, log
        ///     10. Zero Negative Equal to Equal to.
        /// </returns>
        public static Double Log10(this Double d)
        {
            return Math.Log10(d);
        }

        /// <summary>
        ///     Returns the larger of two double-precision floating-point numbers.
        /// </summary>
        /// <param name="val1">The first of two double-precision floating-point numbers to compare.</param>
        /// <param name="val2">The second of two double-precision floating-point numbers to compare.</param>
        /// <returns>Parameter  or , whichever is larger. If , , or both  and  are equal to ,  is returned.</returns>
        public static Double Max(this Double val1, Double val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        /// Gets a TimeSpan from a double number of milliseconds.
        /// </summary>
        /// <param name="milliseconds">The number of milliseconds the TimeSpan will contain.</param>
        /// <returns>A TimeSpan containing the specified number of milliseconds.</returns>
        /// <remarks>
        /// 	Contributed by jceddy
        /// </remarks>
        public static TimeSpan Milliseconds(this double milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds);
        }

        /// <summary>
        ///     Returns the smaller of two double-precision floating-point numbers.
        /// </summary>
        /// <param name="val1">The first of two double-precision floating-point numbers to compare.</param>
        /// <param name="val2">The second of two double-precision floating-point numbers to compare.</param>
        /// <returns>Parameter  or , whichever is smaller. If , , or both  and  are equal to ,  is returned.</returns>
        public static Double Min(this Double val1, Double val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        /// Gets a TimeSpan from a double number of minutes.
        /// </summary>
        /// <param name="minutes">The number of minutes the TimeSpan will contain.</param>
        /// <returns>A TimeSpan containing the specified number of minutes.</returns>
        /// <remarks>
        /// 	Contributed by jceddy
        /// </remarks>
        public static TimeSpan Minutes(this double minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }

        /// <summary>
        ///     A T extension method to determines whether the object is not equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list doesn't contains the object, else false.</returns>
        /// ###
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public static bool NotIn(this Double @this, params Double[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }

        public static decimal PercentageOf(this double number, int percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentageOf(this double number, float percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentageOf(this double number, double percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentageOf(this double number, long percent)
        {
            return (decimal)(number * percent / 100);
        }

        /// <summary>
        ///     Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="x">A double-precision floating-point number to be raised to a power.</param>
        /// <param name="y">A double-precision floating-point number that specifies a power.</param>
        /// <returns>The number  raised to the power .</returns>
        public static Double Pow(this Double x, Double y)
        {
            return Math.Pow(x, y);
        }

        /// <summary>
        ///     Rounds a double-precision floating-point value to the nearest integral value.
        /// </summary>
        /// <param name="a">A double-precision floating-point number to be rounded.</param>
        /// <returns>
        ///     The integer nearest . If the fractional component of  is halfway between two integers, one of which is even
        ///     and the other odd, then the even number is returned. Note that this method returns a  instead of an integral
        ///     type.
        /// </returns>
        public static Double Round(this Double a)
        {
            return Math.Round(a);
        }

        /// <summary>
        ///     Rounds a double-precision floating-point value to a specified number of fractional digits.
        /// </summary>
        /// <param name="a">A double-precision floating-point number to be rounded.</param>
        /// <param name="digits">The number of fractional digits in the return value.</param>
        /// <returns>The number nearest to  that contains a number of fractional digits equal to .</returns>
        /// ###
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        public static Double Round(this Double a, Int32 digits)
        {
            return Math.Round(a, digits);
        }

        /// <summary>
        ///     Rounds a double-precision floating-point value to the nearest integer. A parameter specifies how to round the
        ///     value if it is midway between two numbers.
        /// </summary>
        /// <param name="a">A double-precision floating-point number to be rounded.</param>
        /// <param name="mode">Specification for how to round  if it is midway between two other numbers.</param>
        /// <returns>
        ///     The integer nearest . If  is halfway between two integers, one of which is even and the other odd, then
        ///     determines which of the two is returned.
        /// </returns>
        /// ###
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        public static Double Round(this Double a, MidpointRounding mode)
        {
            return Math.Round(a, mode);
        }

        /// <summary>
        ///     Rounds a double-precision floating-point value to a specified number of fractional digits. A parameter
        ///     specifies how to round the value if it is midway between two numbers.
        /// </summary>
        /// <param name="a">A double-precision floating-point number to be rounded.</param>
        /// <param name="digits">The number of fractional digits in the return value.</param>
        /// <param name="mode">Specification for how to round  if it is midway between two other numbers.</param>
        /// <returns>
        ///     The number nearest to  that has a number of fractional digits equal to . If  has fewer fractional digits than
        ///     ,  is returned unchanged.
        /// </returns>
        /// ###
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        public static Double Round(this Double a, Int32 digits, MidpointRounding mode)
        {
            return Math.Round(a, digits, mode);
        }

        /// <summary>
        /// Gets a TimeSpan from a double number of seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds the TimeSpan will contain.</param>
        /// <returns>A TimeSpan containing the specified number of seconds.</returns>
        /// <remarks>
        /// 	Contributed by jceddy
        /// </remarks>
        public static TimeSpan Seconds(this double seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }

        /// <summary>
        ///     Returns a value indicating the sign of a double-precision floating-point number.
        /// </summary>
        /// <param name="value">A signed number.</param>
        /// <returns>
        ///     A number that indicates the sign of , as shown in the following table.Return value Meaning -1  is less than
        ///     zero. 0  is equal to zero. 1  is greater than zero.
        /// </returns>
        public static Int32 Sign(this Double value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        ///     Returns the sine of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>The sine of . If  is equal to , , or , this method returns .</returns>
        public static Double Sin(this Double a)
        {
            return Math.Sin(a);
        }

        /// <summary>
        ///     Returns the hyperbolic sine of the specified angle.
        /// </summary>
        /// <param name="value">An angle, measured in radians.</param>
        /// <returns>The hyperbolic sine of . If  is equal to , , or , this method returns a  equal to .</returns>
        public static Double Sinh(this Double value)
        {
            return Math.Sinh(value);
        }

        /// <summary>
        ///     Returns the square root of a specified number.
        /// </summary>
        /// <param name="d">The number whose square root is to be found.</param>
        /// <returns>
        ///     One of the values in the following table.  parameter Return value Zero or positive The positive square root
        ///     of . Negative Equals Equals.
        /// </returns>
        public static Double Sqrt(this Double d)
        {
            return Math.Sqrt(d);
        }

        /// <summary>
        ///     Returns the tangent of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>The tangent of . If  is equal to , , or , this method returns .</returns>
        public static Double Tan(this Double a)
        {
            return Math.Tan(a);
        }

        /// <summary>
        ///     Returns the hyperbolic tangent of the specified angle.
        /// </summary>
        /// <param name="value">An angle, measured in radians.</param>
        /// <returns>
        ///     The hyperbolic tangent of . If  is equal to , this method returns -1. If value is equal to , this method
        ///     returns 1. If  is equal to , this method returns .
        /// </returns>
        public static Double Tanh(this Double value)
        {
            return Math.Tanh(value);
        }

        /// <summary>
        ///     A Double extension method that converts the @this to a money.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a Double.</returns>
        public static Double ToMoney(this Double @this)
        {
            return Math.Round(@this, 2);
        }

        /// <summary>
        ///     Calculates the integral part of a specified double-precision floating-point number.
        /// </summary>
        /// <param name="d">A number to truncate.</param>
        /// <returns>
        ///     The integral part of ; that is, the number that remains after any fractional digits have been discarded, or
        ///     one of the values listed in the following table. Return value.
        /// </returns>
        public static Double Truncate(this Double d)
        {
            return Math.Truncate(d);
        }
    }
}
