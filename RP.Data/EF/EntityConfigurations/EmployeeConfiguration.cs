using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RP.Data.Entities;

namespace RP.Data.EF.EntityConfigurations
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
