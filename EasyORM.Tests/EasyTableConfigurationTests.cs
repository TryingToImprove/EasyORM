using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyORM.Domain;
using EasyORM.Extensions;
using EasyORM.Tests.DbTestModels;
using Moq;
using Xunit;

namespace EasyORM.Tests
{
    public partial class EasyTableConfigurationTests
    {
        [Fact(DisplayName = "Should set default empty field list")]
        [Trait("EasyTableConfiguration", "")]
        public void ShouldSetADefaultEmptyListForFields()
        {
            //Arrange
            var contextMock = new Mock<IEasyContext>();
            var configuration = new EasyConfiguration(contextMock.Object);

            //Act
            configuration.Configure<PersonModel>();

            //Assert
            var tableConfiguration = configuration.TableConfigurations.FindByType<PersonModel>();

            Assert.NotNull(tableConfiguration.Fields);
            Assert.IsType(typeof(List<EasyTableFieldConfiguration>), tableConfiguration.Fields);
            Assert.Equal(0, tableConfiguration.Fields.Count);
        }

        [Fact(DisplayName = "If duplicate configuration then select the configuration instead of create a new")]
        [Trait("EasyTableConfiguration", "")]
        public void ShouldBeAbleToCreateADublicateConfiguration()
        {
            //Arrange
            var contextMock = new Mock<IEasyContext>();
            var configuration = new EasyConfiguration(contextMock.Object);

            //Act
            configuration.Configure<PersonModel>();
            configuration.Configure<PersonModel>();

            //Assert
            var numberOfConfigurations = configuration.TableConfigurations.Count;

            Assert.Equal(1, numberOfConfigurations);
        }

        [Fact(DisplayName = "Should be able to set a single primary key")]
        [Trait("EasyTableConfiguration", "")]
        public void ShouldBeAbleToSetASinglePrimary()
        {
            //Arrange
            var contextMock = new Mock<IEasyContext>();
            var configuration = new EasyConfiguration(contextMock.Object);

            //Act
            configuration.Configure<PersonModel>()
                .WithKey(x => new
                {
                    x.Id
                });

            //Assert
            var tableConfiguration = configuration.TableConfigurations.FindByType<PersonModel>();

            Assert.Equal(1, tableConfiguration.PrimaryKeys.Count());
            Assert.Equal("Id", tableConfiguration.PrimaryKeys.First().Name);
        }

        [Fact(DisplayName = "Should be able to set multiple primary keys")]
        [Trait("EasyTableConfiguration", "")]
        public void ShouldBeAbleToSetMultiplePrimaryKeys()
        {
            //Arrange
            var contextMock = new Mock<IEasyContext>();
            var configuration = new EasyConfiguration(contextMock.Object);

            //Act
            configuration.Configure<PersonModel>()
                .WithKey(x => new
                {
                    x.FirstName,
                    x.LastName
                });

            //Assert
            var tableConfiguration = configuration.TableConfigurations.FindByType<PersonModel>();

            Assert.Equal(2, tableConfiguration.PrimaryKeys.Count());
            Assert.True(tableConfiguration.PrimaryKeys.Any(x => x.Name == "FirstName"));
            Assert.True(tableConfiguration.PrimaryKeys.Any(x => x.Name == "LastName"));
        }
    }
}
