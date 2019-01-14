using System.Collections.Generic;
using System.Linq;
using RP.DataAccess.RepositoryPattern.Core;
using RP.DataAccess.RepositoryPattern.Entities;

namespace RP.DataAccess.RepositoryPattern.EF.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RPContext context) : base (context) { }

        public IEnumerable<Employee> GetTop5Employees()
        {
            return RPContext.Employees
                .OrderBy(e => e.Id)
                .Take(5)
                .ToList();
        }

        private RPContext RPContext => Context as RPContext;
    }
}
