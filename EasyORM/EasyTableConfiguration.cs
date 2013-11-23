using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using EasyORM.Helpers;

namespace EasyORM
{
    public abstract class EasyTableConfiguration
    {
        protected EasyTableConfiguration()
        {
            Fields = new List<EasyTableFieldConfiguration>();
        }

        public Type ClassType { get; set; }

        // There is a need for a reference to the configuration for the reference
        internal EasyConfiguration Configuration;

        public string TableName;
        public ICollection<EasyTableFieldConfiguration> Fields;
        public IEnumerable<PropertyInfo> PrimaryKeys;

        internal IEnumerable<EasyTableReference> GetForeignReferences()
        {
            return Configuration.GetForeignReferencesForTable(ClassType);
        }

        internal IEnumerable<EasyTableReference> GetPrimaryReferences()
        {
            return Configuration.GetPrimaryReferencesForTable(ClassType);
        }
    }

    public class EasyTableConfiguration<T> : EasyTableConfiguration
    {
        public EasyTableConfiguration()
        {
            //Set the class type, so we are able to find it in the configurations
            ClassType = typeof(T);

            //Default values
            TableName = ClassType.Name;
        }

        public EasyTableConfiguration<T> WithTableName(string tableName)
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(tableName);
            }

            TableName = tableName;

            return this;
        }

        public EasyTableConfiguration<T> Map<TProperty>(Expression<Func<T, TProperty>> property, string field = "",
            int? length = null, object defaultValue = null)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }

            var propertyInfo = ReflectionHelper.GetPropertyInfo(property);

            var fieldType = EasyFieldTypeResolver.Resolve(propertyInfo.PropertyType);

            if (length == null)
                length = fieldType.DefaultLength.GetValueOrDefault();

            // If there is not specified a custom field name then use the property name
            if (string.IsNullOrEmpty(field))
                field = propertyInfo.Name;

            var fieldConfiguration = new EasyTableFieldConfiguration
            {
                Property = propertyInfo,
                FieldType = fieldType,
                FieldName = field,
                Length = length
            };

            //If there is any default value then set it on the field configuration
            if (defaultValue != null)
            {
                fieldConfiguration.DefaultValue = defaultValue;
            }

            //Add the field to the configuration
            Fields.Add(fieldConfiguration);

            return this;
        }

        public EasyTableConfiguration<T> WithKey(Expression<Func<T, object>> key)
        {
            //Create a list for the primary keys
            var primaryKeys = new List<PropertyInfo>();

            //Compile the lambda expression
            var resultFunc = key.Compile();

            //Get the object created from the lambda expression
            var primaryKeyObject = resultFunc.Invoke(Activator.CreateInstance<T>());

            // Use reflection to loop over the properties in the key object
            // Find the property on the class by the key name properties and add them to the primaryKeys list
            ReflectionHelper.ForEachProperty(primaryKeyObject, primaryKeys.Add);

            //Set the primary keys on the table configuration    
            PrimaryKeys = primaryKeys;

            return this;
        }

        public EasyTableReferenceConfiguration<T, TObject> RegisterReference<TObject>(Expression<Func<T, TObject>> obj)
        {
            // Register the reference on the configuration for making it easier for the foreign table to 
            // know that there is a reference   
            var easyTableReferenceConfiguration = new EasyTableReferenceConfiguration<T, TObject>(this);
            easyTableReferenceConfiguration.SetObjectInfo(ReflectionHelper.GetPropertyInfo(obj));

            return easyTableReferenceConfiguration;
        }

        public EasyTableReferenceConfiguration<T, TObject> RegisterManyReference<TObject>(Expression<Func<T, ICollection<TObject>>> obj)
        {
            // Register the reference on the configuration for making it easier for the foreign table to 
            // know that there is a reference   
            var easyTableReferenceConfiguration = new EasyTableReferenceConfiguration<T, TObject>(this);
            easyTableReferenceConfiguration.SetObjectInfo(ReflectionHelper.GetPropertyInfo(obj));

            return easyTableReferenceConfiguration;
        }
    }

    public class EasyTableReferenceConfiguration<T, TObject>
    {
        private readonly EasyTableConfiguration<T> _tableConfiguration;
        private PropertyInfo _objectInfo;

        public EasyTableReferenceConfiguration(EasyTableConfiguration<T> tableConfiguration)
        {
            _tableConfiguration = tableConfiguration;
        }

        public EasyTableConfiguration<T> FromForeign<TKey>(Expression<Func<TObject, TKey>> key)
        {
            var keyInfo = ReflectionHelper.GetPropertyInfo(key);

            Debug.Assert(_tableConfiguration != null, "table configuration is null");
            Debug.Assert(_tableConfiguration.Configuration != null, "configuration is null");

            _tableConfiguration.Configuration.RegisterReference(new EasyTableReference
            {
                ManyKey = _objectInfo
            });

            return _tableConfiguration;
        }

        public EasyTableConfiguration<T> From<TKey>(Expression<Func<T, TKey>> key)
        {
            var keyInfo = ReflectionHelper.GetPropertyInfo(key);

            _tableConfiguration.Configuration.RegisterReference(new EasyTableReference
            {
                SingleKey = _objectInfo,
                ManyKey = keyInfo
            });

            return _tableConfiguration;
        }

        internal void SetObjectInfo(PropertyInfo objectInfo)
        {
            _objectInfo = objectInfo;
        }
    }
}
