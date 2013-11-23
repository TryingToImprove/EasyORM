using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyORM.Tests.DbTestModels;
using Xunit;

namespace EasyORM.Tests
{
    public class DbContext : EasyContext
    {
        public override void Configure()
        {
            Configuration.Configure<PersonModel>()
                .WithTableName("Personer")
                .WithKey(x => new
                {
                    x.Id
                })
                .Map(x => x.Id, length: 11)
                .Map(x => x.FirstName, length: 50, field: "fornavn");
                //.RegisterReference(x=> x.EmployeeDetails)
                //    .FromForeign(x => x.PersonId)
                //.RegisterReference(x=> x.Address)
                //    .From(x => x.AddressId);

            Configuration.Configure<AddressModel>()
                .WithKey(x => x.AddressId)
                .Map(x => x.City)
                .Map(x => x.Street)
                .RegisterManyReference(x=> x.Persons)
                    .FromForeign(x=> x.AddressId);
        }
    }

    public class EmployeeDetailsModel
    {
        public int PersonId { get; set; }
    }

    public class AddressModel
    {
        public int AddressId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public ICollection<PersonModel> Persons { get; set; }
    }
    
    public class EasyContextTests
    {
        [Fact]
        public void CanCreateAContext()
        {
            var context = new DbContext();
        }
    }
}
