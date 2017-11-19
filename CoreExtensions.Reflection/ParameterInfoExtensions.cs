using System;

namespace CoreExtensions
{
    public static class ParameterInfoExtensions
    {
        /// <summary>
        ///     Retrieves a custom attribute applied to a method parameter. Parameters specify the method parameter, and the
        ///     type of the custom attribute to search for.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <returns>
        ///     A reference to the single custom attribute of type  that is applied to , or null if there is no such
        ///     attribute.
        /// </returns>
        public static Attribute GetCustomAttribute(this System.Reflection.ParameterInfo element, Type attributeType)
        {
            return element.GetCustomAttribute(attributeType);
        }

        /// <summary>
        ///     Retrieves a custom attribute applied to a method parameter. Parameters specify the method parameter, the type
        ///     of the custom attribute to search for, and whether to search ancestors of the method parameter.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <param name="inherit">If true, specifies to also search the ancestors of  for custom attributes.</param>
        /// <returns>
        ///     A reference to the single custom attribute of type  that is applied to , or null if there is no such
        ///     attribute.
        /// </returns>
        public static Attribute GetCustomAttribute(this System.Reflection.ParameterInfo element, Type attributeType, Boolean inherit)
        {
            return element.GetCustomAttribute(attributeType, inherit);
        }

        /// <summary>
        ///     Retrieves an array of the custom attributes applied to a method parameter. Parameters specify the method
        ///     parameter, and the type of the custom attribute to search for.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <returns>
        ///     An  array that contains the custom attributes of type  applied to , or an empty array if no such custom
        ///     attributes exist.
        /// </returns>
        public static Attribute[] GetCustomAttributes(this System.Reflection.ParameterInfo element, Type attributeType)
        {
            return element.GetCustomAttributes(attributeType);
        }

        /// <summary>
        ///     Retrieves an array of the custom attributes applied to a method parameter. Parameters specify the method
        ///     parameter, the type of the custom attribute to search for, and whether to search ancestors of the method
        ///     parameter.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <param name="inherit">If true, specifies to also search the ancestors of  for custom attributes.</param>
        /// <returns>
        ///     An  array that contains the custom attributes of type  applied to , or an empty array if no such custom
        ///     attributes exist.
        /// </returns>
        public static Attribute[] GetCustomAttributes(this System.Reflection.ParameterInfo element, Type attributeType, Boolean inherit)
        {
            return (Attribute[])element.GetCustomAttributes(attributeType, inherit);
        }

        /// <summary>
        ///     Retrieves an array of the custom attributes applied to a method parameter. Parameters specify the method
        ///     parameter, and whether to search ancestors of the method parameter.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="inherit">If true, specifies to also search the ancestors of  for custom attributes.</param>
        /// <returns>
        ///     An  array that contains the custom attributes applied to , or an empty array if no such custom attributes
        ///     exist.
        /// </returns>
        public static Attribute[] GetCustomAttributes(this System.Reflection.ParameterInfo element, Boolean inherit)
        {
            return (Attribute[])element.GetCustomAttributes(inherit);
        }

        /// <summary>
        ///     Determines whether any custom attributes are applied to a method parameter. Parameters specify the method
        ///     parameter, and the type of the custom attribute to search for.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <returns>true if a custom attribute of type  is applied to ; otherwise, false.</returns>
        public static Boolean IsDefined(this System.Reflection.ParameterInfo element, Type attributeType)
        {
            return element.GetCustomAttribute(attributeType) != null;
        }

        /// <summary>
        ///     Determines whether any custom attributes are applied to a method parameter. Parameters specify the method
        ///     parameter, the type of the custom attribute to search for, and whether to search ancestors of the method
        ///     parameter.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <param name="inherit">If true, specifies to also search the ancestors of  for custom attributes.</param>
        /// <returns>true if a custom attribute of type  is applied to ; otherwise, false.</returns>
        public static Boolean IsDefined(this System.Reflection.ParameterInfo element, Type attributeType, Boolean inherit)
        {
            return element.GetCustomAttribute(attributeType, inherit) != null;
        }
    }
}
