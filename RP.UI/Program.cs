using AutoMapper;
using RP.CompositionRoot;
using System.Reflection;

namespace RP.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var unity = new DependencyInjection<Initializer>();
            var program = unity.Resolve();

            Mapper.Initialize(config => config.AddProfiles(Assembly.GetExecutingAssembly()));

            program.Run();
        }
    }
}
