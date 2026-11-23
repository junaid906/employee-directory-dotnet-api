using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeDirectoryApi.Entities.Configurations;

public class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");
        
        builder.HasIndex(x => x.UniqueId)
            .IsUnique();
        
        builder.HasKey(x=> x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();


        builder.Property(x => x.DepartmentName)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("now()");
    }
}