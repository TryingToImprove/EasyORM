using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EasyORM
{
    internal class EasyQueryable : IQueryable
    {
        // There is a a need for the context to get the configuration about the 
        // table structure
        private EasyContext _context;

        internal EasyQueryable(EasyContext context)
        {
            _context = context;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public Expression Expression { get; private set; }
        public Type ElementType { get; private set; }
        public IQueryProvider Provider { get; private set; }
    }
}
