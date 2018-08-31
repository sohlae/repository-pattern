using AutoMapper;
using RP.UI.Dependency_Injection;
using System.Reflection;
using Unity;

namespace RP.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var unity = new UnityConfiguration();
            var container = unity.RegisterDependencies();
            var program = container.Resolve<Initializer>();

            Mapper.Initialize(config => config.AddProfiles(Assembly.GetExecutingAssembly()));

            program.Run();
        }
    }
}
