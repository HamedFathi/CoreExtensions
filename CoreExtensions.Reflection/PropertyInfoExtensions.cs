using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CoreExtensions
{
    public static class PropertyInfoExtensions
    {
        public static bool HasAttribute<TAttribute>(this PropertyInfo propertyInfo, bool inherit = false)
                    where TAttribute : Attribute
        {
            return Attribute.IsDefined(propertyInfo, typeof(TAttribute), inherit);
        }

        public static Expression<Func<T, object>> ToExpression<T>(this PropertyInfo propertyInfo)
        {
            ParameterExpression arg = Expression.Parameter(typeof(T), "x");
            Expression expr = Expression.Property(arg, propertyInfo);
            if (propertyInfo.PropertyType.IsValueType)
                expr = Expression.Convert(expr, typeof(object));

            return Expression.Lambda<Func<T, object>>(expr, arg);
        }
    }
}
