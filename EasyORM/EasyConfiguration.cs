using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EasyORM.Domain;
using EasyORM.Extensions;
using EasyORM.Helpers;

namespace EasyORM
{
    public class EasyConfiguration
    {
        public IEasyContext Context { get; set; }
        public ICollection<EasyTableConfiguration> TableConfigurations;
        public ICollection<EasyTableReference> References;

        public EasyConfiguration(IEasyContext context)
        {
            Context = context;
            TableConfigurations = new List<EasyTableConfiguration>();
            References = new List<EasyTableReference>();
        }

        internal void Initialize()
        {
            // Loop over IQueryables in EasyContext and set it as a EasyQueryable
            //SetPrivateContext(queryables);
        }

        internal void RegisterReference(EasyTableReference reference)
        {
            if (reference == null)
            {
                throw new ArgumentNullException("reference");
            }

            References.Add(reference);
            //var propertyInfo = ReflectionHelper.GetPropertyInfo(property);
        }

        public EasyTableConfiguration<T> Configure<T>() where T : class
        {
            EasyTableConfiguration<T> tableConfiguration;

            if (TableConfigurations.Contains<T>())
            {
                //If the table configuration exisis, then let us just se that
                tableConfiguration = TableConfigurations.FindByType<T>();
            }
            else
            {
                //If the table configuration does not exists then lets create it
                tableConfiguration = new EasyTableConfiguration<T> { Configuration = this };

                //Add the configuration to the list
                TableConfigurations.Add(tableConfiguration);
            }

            return tableConfiguration;
        }
    }
}
