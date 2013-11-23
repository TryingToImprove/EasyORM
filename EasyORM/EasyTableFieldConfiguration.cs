using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EasyORM.Domain;

namespace EasyORM
{
    public class EasyTableFieldConfiguration
    {
        public PropertyInfo Property { get; set; }
        public string FieldName { get; set; }
        public IEasyFieldType FieldType { get; set; }
        public int? Length { get; set; }
        public object DefaultValue { get; set; }
    }
}
