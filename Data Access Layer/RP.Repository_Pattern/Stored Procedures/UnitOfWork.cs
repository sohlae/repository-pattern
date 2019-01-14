using RP.DataAccess.RepositoryPattern.Core;
using RP.DataAccess.RepositoryPattern.StoredProcedures.Repositories;

namespace RP.DataAccess.RepositoryPattern.StoredProcedures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        public UnitOfWork(Context context)
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
