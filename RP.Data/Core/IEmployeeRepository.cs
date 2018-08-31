using RP.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RP.Data.Core
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Employee> GetTop5Employees();
    }
}
