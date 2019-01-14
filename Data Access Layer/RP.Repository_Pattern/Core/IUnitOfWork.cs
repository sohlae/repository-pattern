using System;

namespace RP.DataAccess.RepositoryPattern.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }

        void Complete();
    }
}
