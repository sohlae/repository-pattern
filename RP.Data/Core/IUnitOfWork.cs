using System;

namespace RP.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }

        void Complete();
    }
}
