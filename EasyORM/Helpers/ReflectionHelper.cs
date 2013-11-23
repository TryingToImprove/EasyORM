using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace EasyORM.Helpers
{
    internal class ReflectionHelper
    {
        internal static void ForEachProperty<T>(T obj, Action<PropertyInfo> action)
        {
            var typeInfo = obj.GetType();
            var properties = typeInfo.GetProperties();

            foreach (var propertyInfo in properties)
            {
                action.Invoke(propertyInfo);
            }

        }

        internal static PropertyInfo GetPropertyInfo<T, TProperty>(Expression<Func<T, TProperty>> property)
        {
            var member = property.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    property));

            var propertyInfo = member.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    property));

            var type = typeof (T);

            if (type != propertyInfo.ReflectedType && !type.IsSubclassOf(propertyInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    property,
                    type));

            return propertyInfo;
        }
    }
}
