using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RP.Business;
using RP.Business.Core;
using RP.DataAccess.RepositoryPattern.Core;
using System.IO;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using ef = RP.DataAccess.RepositoryPattern.EF;
using sp = RP.DataAccess.RepositoryPattern.StoredProcedures;

namespace RP.UI.Dependency_Injection
{
    public class UnityConfiguration
    {
        public UnityContainer RegisterDependencies()
        {
            var container = new UnityContainer();

            #region Data Layer
            /* This container sets up the Entity Framework dependencies */
            container.RegisterType<IUnitOfWork, ef.UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmployeeRepository, ef.Repositories.EmployeeRepository>();

            /* This container sets up the Stored Procedure dependencies */
            container.RegisterType<IUnitOfWork, sp.UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmployeeRepository, sp.Repositories.EmployeeRepository>();
            #endregion

            #region Business Layer
            container.RegisterType<IEmployeeBusiness, EmployeeBusiness>();
            #endregion

            return container;
        }
    }
}
