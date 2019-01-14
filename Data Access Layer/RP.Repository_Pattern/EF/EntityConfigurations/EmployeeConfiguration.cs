using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RP.DataAccess.RepositoryPattern.Entities;

namespace RP.DataAccess.RepositoryPattern.EF.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(e => e.BirthDate)
                .IsRequired();
        }
    }
}
