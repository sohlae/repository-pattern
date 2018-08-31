using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RP.Business;
using RP.Business.Core;
using RP.Data.Core;
using RP.Data.EF;
using RP.Data.StoredProcedures;
using System.IO;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace RP.UI.Dependency_Injection
{
    public class UnityConfiguration
    {
        public UnityContainer RegisterDependencies()
        {
            var container = new UnityContainer();

            #region Data Layer
            /* This container sets up the Entity Framework dependencies */
            container.RegisterType<IContext, RPContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, Data.EF.UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmployeeRepository, Data.EF.Repositories.EmployeeRepository>();

            /* This container sets up the Stored Procedure dependencies */
            //container.RegisterType<IContext, Context>(new HierarchicalLifetimeManager());
            //container.RegisterType<IUnitOfWork, Data.StoredProcedures.UnitOfWork>(new HierarchicalLifetimeManager());
            //container.RegisterType<IEmployeeRepository, Data.StoredProcedures.Repositories.EmployeeRepository>();
            #endregion

            #region Business Layer
            container.RegisterType<IEmployeeBusiness, EmployeeBusiness>();
            #endregion

            return container;
        }
    }
}
