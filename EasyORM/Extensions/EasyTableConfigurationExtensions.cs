using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EasyORM.Helpers;

namespace EasyORM.Extensions
{
    public static class EasyTableConfigurationExtensions
    {
        public static EasyTableFieldConfiguration GetByProperty<T, TProperty>(this ICollection<EasyTableFieldConfiguration> configurations, Expression<Func<T, TProperty>> property)
        {
            var type = ReflectionHelper.GetPropertyInfo(property);

            return configurations.Single(x => x.Property == type);
        }

    }
}
