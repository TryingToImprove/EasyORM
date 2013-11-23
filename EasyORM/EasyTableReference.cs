using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyORM
{
    public enum ReferenceType
    {
        OneToMany,
        ManyToOne
    }

    public class EasyTableReference
    {
        internal Type SingleType;
        internal Type ManyType;

        internal PropertyInfo SingleKey;
        internal PropertyInfo ManyKey;
    }
}
