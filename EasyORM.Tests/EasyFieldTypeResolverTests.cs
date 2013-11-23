using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyORM.Defaults.Fields;
using EasyORM.Domain;
using Xunit;

namespace EasyORM.Tests
{
    public class EasyFieldTypeResolverTests
    {
        [Fact(DisplayName = "Should be able to resolve a string")]
        public void ShouldBeAbleToResolveAString()
        {
            //Arrange
            IEasyFieldType fieldType = null;
            IEasyFieldType expectedFieldType = new NVarCharFieldType();

            //Act
            Assert.DoesNotThrow(delegate { fieldType = EasyFieldTypeResolver.Resolve<String>(); });
            Assert.DoesNotThrow(delegate { fieldType = EasyFieldTypeResolver.Resolve<string>(); });

            //Assert
            Assert.NotNull(fieldType);
            Assert.Equal(expectedFieldType.Name, fieldType.Name);
            Assert.Equal(expectedFieldType.DefaultLength, fieldType.DefaultLength);
        }

        [Fact(DisplayName = "Should be able to resolve a int (int32)")]
        public void ShouldBeAbleToResolveAInt()
        {
            //Arrange
            IEasyFieldType fieldType = null;
            IEasyFieldType expectedFieldType = new IntFieldType();

            //Act
            Assert.DoesNotThrow(delegate { fieldType = EasyFieldTypeResolver.Resolve<int>(); });
            Assert.DoesNotThrow(delegate { fieldType = EasyFieldTypeResolver.Resolve<Int32>(); });

            //Assert
            Assert.NotNull(fieldType);
            Assert.Equal(expectedFieldType.Name, fieldType.Name);
            Assert.Equal(expectedFieldType.DefaultLength, fieldType.DefaultLength);
        }
    }
}
