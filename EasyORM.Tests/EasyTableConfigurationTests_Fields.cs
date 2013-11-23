using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyORM.Defaults.Fields;
using EasyORM.Domain;
using EasyORM.Extensions;
using EasyORM.Tests.DbTestModels;
using Moq;
using Xunit;

namespace EasyORM.Tests
{
    public partial class EasyTableConfigurationTests
    {
        [Fact(DisplayName = "Should be able to map a property")]
        [Trait("EasyTableConfiguration", "Mappings")]
        public void ShouldBeAbleToMapAProperty()
        {
            //Arrange
            var context = new EmptyDbContext();
            var configuration = new EasyConfiguration(context);
            var expectedType = new NVarCharFieldType();
            
            //Act
            configuration.Configure<PersonModel>()
                .Map(x => x.FirstName);

            //Assert
            var tableConfiguration = configuration.TableConfigurations.FindByType<PersonModel>();
            var fields = tableConfiguration.Fields;
            var fieldConfiguraton = fields.GetByProperty<PersonModel, String>(x => x.FirstName);

            Assert.Equal(1, fields.Count);
            Assert.NotNull(fieldConfiguraton);
            Assert.Equal("FirstName", fieldConfiguraton.FieldName);
            Assert.Null(fieldConfiguraton.DefaultValue);
            //FieldType validation
            Assert.Equal(expectedType.Name, fieldConfiguraton.FieldType.Name);
            Assert.Equal(expectedType.DefaultLength, fieldConfiguraton.FieldType.DefaultLength);
        }

        [Fact(DisplayName = "Should be able to map a property with custom field name")]
        [Trait("EasyTableConfiguration", "Mappings")]
        public void ShouldBeAbleToMapAPropertyWithCustomFieldName()
        {
            const string fieldName = "Fornavn";

            //Arrange
            var context = new EmptyDbContext();
            var configuration = new EasyConfiguration(context);

            //Act
            configuration.Configure<PersonModel>()
                .Map(x => x.FirstName, field: fieldName);

            //Assert
            var tableConfiguration = configuration.TableConfigurations.FindByType<PersonModel>();
            var fields = tableConfiguration.Fields;
            var fieldConfiguraton = fields.GetByProperty<PersonModel, String>(x => x.FirstName);

            Assert.Equal(fieldName, fieldConfiguraton.FieldName);
        }

        [Fact(DisplayName = "Should be able to map a property with a default value")]
        [Trait("EasyTableConfiguration", "Mappings")]
        public void ShouldBeAbleToMapAPropertyWithADefaultValue()
        {
            const string defaultValue = "Oliver";

            //Arrange
            var context = new EmptyDbContext();
            var configuration = new EasyConfiguration(context);

            //Act
            configuration.Configure<PersonModel>()
                .Map(x => x.FirstName, defaultValue: defaultValue);

            //Assert
            var tableConfiguration = configuration.TableConfigurations.FindByType<PersonModel>();
            var fields = tableConfiguration.Fields;
            var fieldConfiguraton = fields.GetByProperty<PersonModel, String>(x => x.FirstName);

            Assert.Equal(defaultValue, fieldConfiguraton.DefaultValue);
        }

        [Fact(DisplayName = "Should be able to map a property with custom a custom length")]
        [Trait("EasyTableConfiguration", "Mappings")]
        public void ShouldBeAbleToMapAPropertyuWithSpecficLength()
        {
            const int fieldLength = 200;
            
            //Arrange
            var context = new EmptyDbContext();
            var configuration = new EasyConfiguration(context);
            
            //Act
            configuration.Configure<PersonModel>()
                .Map(x => x.FirstName, length: fieldLength);

            //Assert
            var tableConfiguration = configuration.TableConfigurations.FindByType<PersonModel>();
            var fields = tableConfiguration.Fields;
            var fieldConfiguraton = fields.GetByProperty<PersonModel, String>(x => x.FirstName);

            Assert.Equal(fieldLength, fieldConfiguraton.Length);
        }
    }
}
