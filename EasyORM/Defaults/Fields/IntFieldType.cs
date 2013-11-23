using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyORM.Domain;

namespace EasyORM.Defaults.Fields
{
    public class IntFieldType : EasyFieldType, IEasyFieldType
    {
        public int? DefaultLength
        {
            get
            {
                return 11;
            }
        }

        public string Name
        {
            get
            {
                return "int";
            }
        }
    }
}
