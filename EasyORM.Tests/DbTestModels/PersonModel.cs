using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyORM.Tests.DbTestModels
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AddressId { get; set; }
        public AddressModel Address { get; set; }
        public EmployeeDetailsModel EmployeeDetails { get; set; }
    }
}
