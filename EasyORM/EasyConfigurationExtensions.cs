using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyORM
{
    internal static class EasyConfigurationExtensions
    {
        // Get table reference by primary type
        internal static IEnumerable<EasyTableReference> GetPrimaryReferencesForTable<T>(this EasyConfiguration configuration)
        {
            return GetPrimaryReferencesForTable(configuration, typeof(T));
        }
        // Get table reference by primary type
        internal static IEnumerable<EasyTableReference> GetPrimaryReferencesForTable(this EasyConfiguration configuration, Type type)
        {
            return null;
        }

        // Get table reference by foreign type
        internal static IEnumerable<EasyTableReference> GetForeignReferencesForTable<T>(this EasyConfiguration configuration)
        {
            return GetForeignReferencesForTable(configuration, typeof(T));
        }

        // Get table reference by foreign type
        internal static IEnumerable<EasyTableReference> GetForeignReferencesForTable(this EasyConfiguration configuration, Type type)
        {
            return null;
        }
    }
}
