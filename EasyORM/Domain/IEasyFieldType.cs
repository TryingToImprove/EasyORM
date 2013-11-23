using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyORM.Domain
{
    public interface IEasyFieldType
    {
        int? DefaultLength { get; }
        string Name { get; }
    }
}
