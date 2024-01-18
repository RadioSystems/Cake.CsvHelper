using CsvHelper.Configuration;

namespace Cake.CsvHelper.Tests.Fixtures
{
    public sealed class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
#pragma warning disable MA0056 // Do not call overridable members in constructor
            Map(m => m.Id).Name("EmployeeId");
            Map(m => m.Name).Name("GivenName");
#pragma warning restore MA0056 // Do not call overridable members in constructor
        }
    }
}
