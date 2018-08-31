using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RP.Data.EF
{
    public class RPContextDbFactory : IDesignTimeDbContextFactory<RPContext>
    {
        public RPContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<RPContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("RPContext"));

            return new RPContext(optionsBuilder.Options);
        }
    }
}
