using RP.DataAccess.RepositoryPattern.Entities;
using System.Collections.Generic;

namespace RP.DataAccess.RepositoryPattern.Core
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Employee> GetTop5Employees();
    }
}
