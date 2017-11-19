using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CoreExtensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Closes the passed generic type with the provided type arguments and returns an instance of the newly constructed type.
        /// </summary>
        /// <typeparam name="T">The typed type to be returned.</typeparam>
        /// <param name="genericType">The open generic type.</param>
        /// <param name="typeArguments">The type arguments to close the generic type.</param>
        /// <returns>An instance of the constructed type casted to T.</returns>
        public static T CreateGenericTypeInstance<T>(this Type genericType, params Type[] typeArguments) where T : class
        {
            var constructedType = genericType.MakeGenericType(typeArguments);
            var instance = Activator.CreateInstance(constructedType);
            return (instance as T);
        }

        /// <summary>
        ///     A Type extension method that creates an instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The new instance.</returns>
        public static T CreateInstance<T>(this Type @this, Object[] args)
        {
            return (T)Activator.CreateInstance(@this, args);
        }

        /// <summary>
        ///     A Type extension method that creates an instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="activationAttributes">The activation attributes.</param>
        /// <returns>The new instance.</returns>
        public static T CreateInstance<T>(this Type @this, Object[] args, Object[] activationAttributes)
        {
            return (T)Activator.CreateInstance(@this, args, activationAttributes);
        }

        /// <summary>
        ///     A Type extension method that creates an instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The new instance.</returns>
        public static T CreateInstance<T>(this Type @this)
        {
            return (T)Activator.CreateInstance(@this);
        }

        /// <summary>
        ///     A Type extension method that creates an instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="nonPublic">true to non public.</param>
        /// <returns>The new instance.</returns>
        public static T CreateInstance<T>(this Type @this, Boolean nonPublic)
        {
            return (T)Activator.CreateInstance(@this, nonPublic);
        }

        /// <summary>
        ///     Creates an instance of the specified type using the constructor that best matches the specified parameters.
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <param name="args">
        ///     An array of arguments that match in number, order, and type the parameters of the constructor
        ///     to invoke. If  is an empty array or null, the constructor that takes no parameters (the default constructor) is
        ///     invoked.
        /// </param>
        /// <returns>A reference to the newly created object.</returns>
        public static Object CreateInstance(this Type type, Object[] args)
        {
            return Activator.CreateInstance(type, args);
        }

        /// <summary>
        ///     Creates an instance of the specified type using the constructor that best matches the specified parameters.
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <param name="args">
        ///     An array of arguments that match in number, order, and type the parameters of the constructor
        ///     to invoke. If  is an empty array or null, the constructor that takes no parameters (the default constructor) is
        ///     invoked.
        /// </param>
        /// <param name="activationAttributes">
        ///     An array of one or more attributes that can participate in activation. This
        ///     is typically an array that contains a single  object. The  specifies the URL that is required to activate a
        ///     remote object.
        /// </param>
        /// <returns>A reference to the newly created object.</returns>
        public static Object CreateInstance(this Type type, Object[] args, Object[] activationAttributes)
        {
            return Activator.CreateInstance(type, args, activationAttributes);
        }

        /// <summary>
        ///     Creates an instance of the specified type using that type&#39;s default constructor.
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <returns>A reference to the newly created object.</returns>
        public static Object CreateInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        ///     Creates an instance of the specified type using that type&#39;s default constructor.
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <param name="nonPublic">
        ///     true if a public or nonpublic default constructor can match; false if only a public
        ///     default constructor can match.
        /// </param>
        /// <returns>A reference to the newly created object.</returns>
        public static Object CreateInstance(this Type type, Boolean nonPublic)
        {
            return Activator.CreateInstance(type, nonPublic);
        }

        public static IDictionary<string, int> EnumToDictionary(this Type enumType)
        {
            if (enumType == null) throw new NullReferenceException();
            if (!enumType.IsEnum) throw new InvalidCastException("object is not an Enumeration");

            string[] names = Enum.GetNames(enumType);
            Array values = Enum.GetValues(enumType);

            return (from i in Enumerable.Range(0, names.Length)
                    select new { Key = names[i], Value = (int)values.GetValue(i) })
                .ToDictionary(k => k.Key, k => k.Value);
        }

        /// <summary>
        ///     Gets the default value for the type, e.g. 0 for int, empty guid for guid.
        /// </summary>
        /// <param name="typeToCreateValueFor">The type to create value for.</param>
        /// <param name="safeDefaults">
        ///     if set to true, the routine will return string.Empty for string and empty byte array for
        ///     byte[], otherwise null
        /// </param>
        /// <returns>
        ///     the default value for the type. It returns string.Empty for string, empty byte array for a byte array,
        ///     if safeDefaults is set to true
        /// </returns>
        public static object GetDefaultValue(this Type typeToCreateValueFor, bool safeDefaults)
        {
            var sourceType = typeToCreateValueFor;
            if (typeToCreateValueFor.IsNullableValueType())
            {
                sourceType = typeToCreateValueFor.GetGenericArguments()[0];
            }
            object toReturn = null;
            if (sourceType.IsValueType)
            {
                // produce default value for value type. 
                toReturn = Array.CreateInstance(sourceType, 1).GetValue(0);
            }
            else
            {
                if (safeDefaults)
                {
                    switch (sourceType.Name)
                    {
                        case "String":
                            toReturn = string.Empty;
                            break;
                        case "Byte[]":
                            toReturn = new byte[0];
                            break;
                    }
                }
            }
            return toReturn;
        }

        public static string[] GetEnumDescriptions(this Type type)
        {
            return type.GetFields()
                .Select(f => (DescriptionAttribute)f.GetCustomAttribute(typeof(DescriptionAttribute)))
                .Where(a => a != null)
                .Select(a => a.Description).ToArray();
        }

        public static string GetFixedName(this Type type)
        {
            var partialName = type.ToString().Replace("[", "<").Replace("]", ">");
            var name = Regex.Replace(partialName, "`[0-9]*", "");
            return name;
        }

        public static IEnumerable<string> GetFixedNames(this IEnumerable<Type> types)
        {
            var list = new List<string>();
            foreach (var type in types)
            {
                list.Add(type.GetFixedName());
            }
            return list;
        }

        /// <summary>
        ///     Gets the full type name, of the format: Type.Fullname, assembly name.
        ///     If the assembly is signed, the full assembly name is added, otherwise just the assembly name, not the version,
        ///     public key token or culture.
        /// </summary>
        /// <param name="type">The type of which the full name should be obtained.</param>
        /// <returns>
        ///     full type name. If the type is a .NET system type (e.g. is located in mscorlib or namespace starts with Microsoft.
        ///     or System.) the
        ///     FullTypeName is equal to the FullName of the type.
        /// </returns>
        /// <remarks>
        ///     Use this method if you need to store the type's full name in a string for re-instantiation later on with
        ///     Activator.CreateInstance.
        /// </remarks>
        public static string GetFullTypeName(this Type type)
        {
            if (type == null)
            {
                return string.Empty;
            }
            if (type.IsNetSystemType())
            {
                return type.FullName;
            }
            if (type.Assembly.GetName().GetPublicKeyToken().Length <= 0)
            {
                return string.Format("{0}, {1}", type.FullName, type.Assembly.GetName().Name);
            }
            // Signed assembly, get the full name, including version and public key token.
            return type.AssemblyQualifiedName;
        }

        public static List<Type> GetInnerTypes(this Type type, params Type[] simpleTypes)
        {
            var isPrimitive = type.IsPrimitive;
            var status = type.FullName.StartsWith("System.", StringComparison.Ordinal)
                && !type.IsDictionary()
                && !type.IsEnumerable()
                && !type.IsCollection()
                && !type.IsTuple()
                && !type.IsArray
                ;
            if (isPrimitive
                || status
                || type == typeof(string)
                || type.IsEnum
                || type.IsValueType
                || simpleTypes.Contains(type))
            {
                return new List<Type>() { type };
            }

            var list = new List<Type>();
            if (type.IsGenericType)
            {
                if (type.IsDictionary())
                {
                    Type keyType = type.GetGenericArguments()[0];
                    list.Add(keyType);
                    Type valueType = type.GetGenericArguments()[1];
                    list.Add(valueType);
                    list.AddRange(GetInnerTypes(keyType));
                    list.AddRange(GetInnerTypes(valueType));

                }
                if (type.IsEnumerable() || type.IsCollection())
                {
                    Type itemType = type.GetGenericArguments()[0];
                    list.Add(itemType);
                    list.AddRange(GetInnerTypes(itemType));

                }
                if (type.IsTuple())
                {
                    var args = type.GetGenericArguments();
                    foreach (var arg in args)
                    {
                        list.Add(arg);
                        list.AddRange(GetInnerTypes(arg));
                    }
                }
            }
            else
            {
                if (type.IsArray)
                {
                    var t = type.GetElementType();
                    list.Add(t);
                    list.AddRange(GetInnerTypes(t));

                }
                if (type.IsInterface)
                {
                    // TODO
                }
                if (type.IsClass)
                {
                    var ts = type.GetProperties().Select(x => x.PropertyType).ToList();
                    list.AddRange(ts);
                    foreach (var ttss in ts)
                        list.AddRange(GetInnerTypes(ttss));
                }
            }
            var lst = list.Distinct().ToList();
            return SortInnerTypes(lst, simpleTypes);
        }

        public static bool HasAttribute<TAttribute>(this Type type, bool inherit = false)
                                    where TAttribute : Attribute
        {
            return Attribute.GetCustomAttribute(type, typeof(TAttribute)) != null;
        }

        public static bool HasDefaultConstructor(this Type t)
        {
            return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;
        }

        public static bool HasInterface<T>(this Type type) => typeof(T).IsAssignableFrom(type);

        public static bool HasInterface(this Type type, Type interfaceType) => interfaceType.IsAssignableFrom(type);

        public static bool HasInterfaces(this Type type, params Type[] interfaceTypes)
        {
            foreach (var interfaceType in interfaceTypes)
                if (!interfaceType.IsAssignableFrom(type))
                    return false;
            return true;
        }

        /// <summary>
        /// 	Check if this is a base type
        /// </summary>
        /// <param name = "type"></param>
        /// <param name = "checkingType"></param>
        /// <returns></returns>
        ///  <remarks>
        ///  	Contributed by Michael T, http://about.me/MichaelTran
        ///  </remarks>
        public static bool IsBaseType(this Type type, Type checkingType)
        {
            while (type != typeof(object))
            {
                if (type == null)
                    continue;

                if (type == checkingType)
                    return true;

                type = type.BaseType;
            }
            return false;
        }

        public static bool IsCollection(this Type type)
        {
            return type.GetInterface("ICollection") != null;
        }

        public static bool IsDictionary(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
        }

        public static bool IsEnumerable(this Type type)
        {
            return type.GetInterface("IEnumerable") != null;
        }

        public static bool IsInstanceOfType(this Type type, object obj)
        {
            return obj != null && type.IsInstanceOfType(obj);
        }

        /// <summary>
        ///     Determines whether the type specified is a system type of .NET. System types are types in mscorlib, assemblies
        ///     which start with 'Microsoft.', 'System.'
        ///     or the System assembly itself.
        /// </summary>
        /// <param name="type">The type.</param>
        public static bool IsNetSystemType(this Type type)
        {
            if (type == null)
            {
                return false;
            }
            var nameToCheck = type.Assembly.GetName();
            var exceptions = new[] { "Microsoft.SqlServer.Types" };
            return new[] { "System", "mscorlib", "System.", "Microsoft." }
                .Any(s => (s.EndsWith(".") && nameToCheck.Name.StartsWith(s)) || (nameToCheck.Name == s)) &&
                   !exceptions.Any(s => nameToCheck.Name.StartsWith(s));
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        /// <param name="input">The <see cref="Type"/> instance on which the extension method is called.</param>
        /// <returns>True if the <paramref name="input"/> is nullable.</returns>
        public static bool IsNullable(this Type input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return !input.IsValueType ||
                   (input.IsGenericType && input.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        ///     Determines whether the type this method is called on is a nullable type of type Nullable(Of T)
        /// </summary>
        /// <param name="toCheck">The type to check.</param>
        /// <returns>true if toCheck is a Nullable(Of T) type, otherwise false</returns>
        public static bool IsNullableValueType(this Type toCheck)
        {
            if ((toCheck == null) || !toCheck.IsValueType)
            {
                return false;
            }
            return toCheck.IsGenericType && toCheck.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 	Check if this is a sub class generic type
        /// </summary>
        /// <param name = "generic"></param>
        /// <param name = "toCheck"></param>
        /// <returns></returns>
        ///  <remarks>
        ///  	Contributed by Michael T, http://about.me/MichaelTran
        ///  </remarks>
        public static bool IsSubclassOfRawGeneric(this Type generic, Type toCheck)
        {
            while (toCheck != typeof(object))
            {
                if (toCheck == null)
                    continue;

                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                    return true;
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        public static bool IsTuple(this Type type)
        {
            return type.FullName.StartsWith("System.Tuple`", StringComparison.Ordinal);
        }

        public static bool IsTypeOf<T>(this object obj)
        {
            return (obj.GetType() == typeof(T));
        }

        private static List<Type> SortInnerTypes(List<Type> types, params Type[] simpleTypes)
        {
            var primitives = new List<Type>();
            var systems = new List<Type>();
            var enums = new List<Type>();
            var str = new List<Type>();
            var simples = new List<Type>();
            var classes = new List<Type>();
            var dictionaries = new List<Type>();
            var enumerables = new List<Type>();
            var tuples = new List<Type>();
            var arrays = new List<Type>();
            var valueTypes = new List<Type>();

            foreach (var item in types)
            {
                if (item.IsPrimitive)
                    primitives.Add(item);
                if (item.FullName.StartsWith("System.", StringComparison.Ordinal))
                    systems.Add(item);
                if (item.IsValueType)
                    valueTypes.Add(item);
                if (item.IsEnum)
                    enums.Add(item);
                if (item == typeof(string))
                    str.Add(item);
                if (simpleTypes.Contains(item))
                    simples.Add(item);
                if (item.IsClass)
                    classes.Add(item);
                if (item.IsDictionary())
                    dictionaries.Add(item);
                if (item.IsEnumerable() || item.IsCollection())
                    enumerables.Add(item);
                if (item.IsTuple())
                    tuples.Add(item);
                if (item.IsArray)
                    arrays.Add(item);
            }

            var finalList = new List<Type>();

            finalList.AddRange(primitives);
            finalList.AddRange(valueTypes);
            finalList.AddRange(enums);
            finalList.AddRange(str);
            finalList.AddRange(simples);
            finalList.AddRange(classes);
            finalList.AddRange(systems);
            finalList.AddRange(arrays);
            finalList.AddRange(tuples);
            finalList.AddRange(dictionaries);
            finalList.AddRange(enumerables);

            return finalList;
        }

        public static System.Data.DbType ToDbType(this Type type)
        {
            var typeMap = new Dictionary<Type, System.Data.DbType>
            {
                [typeof(byte)] = System.Data.DbType.Byte,
                [typeof(sbyte)] = System.Data.DbType.SByte,
                [typeof(short)] = System.Data.DbType.Int16,
                [typeof(ushort)] = System.Data.DbType.UInt16,
                [typeof(int)] = System.Data.DbType.Int32,
                [typeof(uint)] = System.Data.DbType.UInt32,
                [typeof(long)] = System.Data.DbType.Int64,
                [typeof(ulong)] = System.Data.DbType.UInt64,
                [typeof(float)] = System.Data.DbType.Single,
                [typeof(double)] = System.Data.DbType.Double,
                [typeof(decimal)] = System.Data.DbType.Decimal,
                [typeof(bool)] = System.Data.DbType.Boolean,
                [typeof(string)] = System.Data.DbType.String,
                [typeof(char)] = System.Data.DbType.StringFixedLength,
                [typeof(Guid)] = System.Data.DbType.Guid,
                [typeof(DateTime)] = System.Data.DbType.DateTime,
                [typeof(DateTimeOffset)] = System.Data.DbType.DateTimeOffset,
                [typeof(byte[])] = System.Data.DbType.Binary,
                [typeof(byte?)] = System.Data.DbType.Byte,
                [typeof(sbyte?)] = System.Data.DbType.SByte,
                [typeof(short?)] = System.Data.DbType.Int16,
                [typeof(ushort?)] = System.Data.DbType.UInt16,
                [typeof(int?)] = System.Data.DbType.Int32,
                [typeof(uint?)] = System.Data.DbType.UInt32,
                [typeof(long?)] = System.Data.DbType.Int64,
                [typeof(ulong?)] = System.Data.DbType.UInt64,
                [typeof(float?)] = System.Data.DbType.Single,
                [typeof(double?)] = System.Data.DbType.Double,
                [typeof(decimal?)] = System.Data.DbType.Decimal,
                [typeof(bool?)] = System.Data.DbType.Boolean,
                [typeof(char?)] = System.Data.DbType.StringFixedLength,
                [typeof(Guid?)] = System.Data.DbType.Guid,
                [typeof(DateTime?)] = System.Data.DbType.DateTime,
                [typeof(DateTimeOffset?)] = System.Data.DbType.DateTimeOffset
                //,[typeof(System.Data.Linq.Binary)] = System.Data.DbType.Binary
            };
            return typeMap[type];
        }

        public static T? ToNullable<T>(this T value) where T : struct
        {
            return (value.Equals(default(T)) ? null : (T?)value);
        }
    }
}
