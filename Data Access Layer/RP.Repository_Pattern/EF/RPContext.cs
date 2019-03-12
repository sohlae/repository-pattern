using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RP.DataAccess.RepositoryPattern.EF.EntityConfigurations;
using RP.DataAccess.RepositoryPattern.Entities;
using System;
using System.IO;

namespace RP.DataAccess.RepositoryPattern.EF
{
    public class RPContext : DbContext
    {
        private KeyVaultClient _client;
        
        public RPContext() : base() { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string username, password;

            try
            {
                _client = new KeyVaultClient(
                     new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));

                username = GetUsername();
                password = GetPassword();
            }

            catch (Exception)
            {
                var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

                var configuration = builder.Build();
                username = configuration["ConnectionStrings:Username"];
                password = configuration["ConnectionStrings:Password"];
            }

            var connectionString = $@"Server=poc-applications.database.windows.net,1433;
                    Initial Catalog=Repository-Pattern-DB;
                    Persist Security Info=False;
                    User ID={ username };
                    Password={ password };
                    MultipleActiveResultSets=False;
                    Encrypt=True;
                    TrustServerCertificate=False;
                    Connection Timeout=30;";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public new void Dispose()
        {
            base.Dispose();
        }

        public string GetUsername()
        {
            var username = _client
                .GetSecretAsync("https://generalassets.vault.azure.net/secrets/rpdatabase-login/2fe9799aa9334ee087f0647c8e3cc541")
                .Result;

            return username.Value;
        }

        public string GetPassword()
        {
            var password = _client
                .GetSecretAsync("https://generalassets.vault.azure.net/secrets/rpdatabase-password/bb30c143a54c4413b658c6d18745e27f")
                .Result;

            return password.Value;
        }
    }
}
