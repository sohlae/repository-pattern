using RP.DataAccess.RepositoryPattern.Core;
using RP.DataAccess.RepositoryPattern.EF.Repositories;

namespace RP.DataAccess.RepositoryPattern.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RPContext _context;

        public UnitOfWork(RPContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
        }

        public IEmployeeRepository Employees { get; private set; }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
