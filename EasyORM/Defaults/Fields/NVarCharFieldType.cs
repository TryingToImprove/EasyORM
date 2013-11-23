using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyORM.Domain;

namespace EasyORM.Defaults.Fields
{
    public class NVarCharFieldType : EasyFieldType, IEasyFieldType
    {
        public int? DefaultLength
        {
            get
            {
                return 255;
            }
        }

        public string Name
        {
            get
            {
                return "nvarchar";
            }
        }
    }
}
