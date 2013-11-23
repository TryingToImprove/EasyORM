using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EasyORM
{
    public static class EasyQueryableExtensions
    {
        public static IQueryable Include<T>(this IQueryable query, Expression<Func<T>> item)
        {
            return query;
        }
    }
}
