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
        [Fact(DisplayName = "Should use the model name by default as the table name")]
        [Trait("EasyTableConfiguration", "TableName")]
        public void ShouldUseTheModelNameAsTableNameByDefault()
        {
            //Arrange
            var contextMock = new Mock<IEasyContext>();
            var configuration = new EasyConfiguration(contextMock.Object);

            //Act
            configuration.Configure<PersonModel>();

            //Assert
            var tableConfiguration = configuration.TableConfigurations.FindByType<PersonModel>();

            Assert.Equal("PersonModel", tableConfiguration.TableName);
        }

        [Fact(DisplayName = "Should be able to regster a custom table name")]
        [Trait("EasyTableConfiguration", "TableName")]
        public void ShouldBeAbleToRegisterACstomTableName()
        {
            //Arrange
            var contextMock = new Mock<IEasyContext>();
            var configuration = new EasyConfiguration(contextMock.Object);

            //Act
            configuration.Configure<PersonModel>()
                .WithTableName("Persons");

            //Assert
            var tableConfiguration = configuration.TableConfigurations.FindByType<PersonModel>();

            Assert.Equal("Persons", tableConfiguration.TableName);
        }

        [Fact(DisplayName = "If duplicate configuration of the table name, then use the latest")]
        [Trait("EasyTableConfiguration", "TableName")]
        public void ShouldUseTheLatestCustomTableName()
        {
            //Arrange
            var contextMock = new Mock<IEasyContext>();
            var configuration = new EasyConfiguration(contextMock.Object);

            //Act
            configuration.Configure<PersonModel>()
                .WithTableName("Persons");

            configuration.Configure<PersonModel>()
                .WithTableName("Personer");

            //Assert
            var tableConfiguration = configuration.TableConfigurations.FindByType<PersonModel>();

            Assert.Equal("Personer", tableConfiguration.TableName);
        }
    }
}
