using System;
using System.Reflection;

namespace GenericCSR
{
    public class PropertyTypeExtractor
    {
        public static string GetPropertyTypeAsString(Type type, string propertyPath)
        {
            GetPropertyType(type, propertyPath, out var propertyTypeName);
            return propertyTypeName;
        }

        private static void GetPropertyType(Type type, string propertyPath, out string propertyTypeName)
        {
            //nestedType
            if (propertyPath.Contains("."))
            {
                var temp = propertyPath.Split(new[] {'.'}, 2);
                var property = type.GetProperty(temp[0]);
                var propType = property.PropertyType;
                GetPropertyType(propType, temp[1], out propertyTypeName);
            }
            else
            {
                var property = type.GetProperty(propertyPath).PropertyType;

                propertyTypeName = property.Name.Contains("Nullable")
                    ? "Nullable." + property.GenericTypeArguments[0].Name
                    : property.Name;
            }
        }

        public static PropertyInfo GetPropertyInfo(Type type, string propertyPath)
        {
            GetPropertyInfo(type, propertyPath, out var propertyTypeName);
            var x = propertyTypeName;
            return propertyTypeName;
        }

        private static void GetPropertyInfo(Type type, string propertyPath, out PropertyInfo propertyTypeName)
        {
            //nestedType
            if (propertyPath.Contains("."))
            {
                var temp = propertyPath.Split(new[] {'.'}, 2);
                var property = type.GetProperty(temp[0]);
                var propType = property.PropertyType;
                GetPropertyInfo(propType, temp[1], out propertyTypeName);
            }
            else
            {
                var property = type.GetProperty(propertyPath).PropertyType;
                var x = type.GetProperty(propertyPath) as PropertyInfo;
                propertyTypeName = x;
            }
        }
    }
}