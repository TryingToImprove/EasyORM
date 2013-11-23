using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyORM.Extensions
{
    public static class EasyConfigurationExtensions
    {
        public static EasyTableConfiguration<T> FindByType<T>(this ICollection<EasyTableConfiguration> configurations)
        {
            return configurations.Single(x => x.ClassType == typeof(T)) as EasyTableConfiguration<T>;
        }

        public static bool Contains<T>(this ICollection<EasyTableConfiguration> configurations)
        {
            return configurations.Any(x => x.ClassType == typeof(T));
        } 
    }
}
