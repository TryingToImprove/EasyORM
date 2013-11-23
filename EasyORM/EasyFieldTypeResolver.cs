using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyORM.Defaults.Fields;
using EasyORM.Domain;

namespace EasyORM
{
    public class EasyFieldTypeResolver
    {
        private static readonly Dictionary<Type, Func<Type, IEasyFieldType>> TypeDictionary = new Dictionary<Type, Func<Type, IEasyFieldType>>(){
            { typeof(string), x => new NVarCharFieldType() },
            { typeof(int), x => new IntFieldType() }
        };

        public static IEasyFieldType Resolve<T>()
        {
            return Resolve(typeof(T));
        }

        internal static IEasyFieldType Resolve(Type type)
        {
            IEasyFieldType result;
            if (!TryResolve(type, out result))
            {
                throw new InvalidCastException("Could not resolve EasyFieldType {" + type.Name + "}");
            }

            return result;
        }

        private static bool TryResolve(Type type, out IEasyFieldType result)
        {
            if (!TypeDictionary.ContainsKey(type))
            {
                result = null;
                return false;
            }

            result = TypeDictionary[type].Invoke(type);
            return true;
        }
    }
}
