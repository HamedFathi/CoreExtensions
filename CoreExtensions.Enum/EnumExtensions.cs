using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace CoreExtensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Determins if a "flags" enum contains the given flag.
        /// </summary>
        public static bool ContainsFlagValue(this Enum e, string flagValue)
        {
            var enumType = e.GetType();

            if (Enum.IsDefined(enumType, flagValue))
            {
                var intEnumValue = Convert.ToInt32(e);
                var intFlagValue = (int)Enum.Parse(enumType, flagValue);

                return (intFlagValue & intEnumValue) == intFlagValue;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determins if a "flags" enum contains the given flag.
        /// </summary>
        public static bool ContainsFlagValue(this Enum e, Enum flagValue)
        {
            if (Enum.IsDefined(e.GetType(), flagValue))
            {
                var intFlagValue = Convert.ToInt32(flagValue);

                return (intFlagValue & Convert.ToInt32(e)) == intFlagValue;
            }
            else
            {
                return false;
            }
        }

        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name != null)
            {
                var field = type.GetField(name);
                if (field?.GetCustomAttribute(typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                {
                    return attr.Description;
                }
            }
            return null;
        }

        public static string GetName(this Enum value)
        {
            return value.ToString();
        }

        public static int GetValue(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        ///     A T extension method to determines whether the object is equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list contains the object, else false.</returns>
        public static bool In(this Enum @this, params Enum[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static bool Is<T>(this Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNot<T>(this Enum type, T value)
        {
            try
            {
                return (int)(object)type != (int)(object)value;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        ///     A T extension method to determines whether the object is not equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list doesn't contains the object, else false.</returns>
        public static bool NotIn(this Enum @this, params Enum[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }

        public static Dictionary<int, string> ToDictionary(this Enum @enum)
        {
            var type = @enum.GetType();
            return Enum.GetValues(type).Cast<int>().ToDictionary(e => e, e => Enum.GetName(type, e));
        }
    }
}
