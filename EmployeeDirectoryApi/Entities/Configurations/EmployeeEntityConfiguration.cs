using EmployeeDirectoryApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeDirectoryApi.Data.Configurations;

public class EmployeeEntityConfiguration : IEntityTypeConfiguration<EmployeeEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
    {
        builder.ToTable("Employees");
        
        builder.HasIndex(x => x.UniqueId)
            .IsUnique();
        
        builder.HasKey(x=> x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.PositionId)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.AvatarUrl)
            .HasMaxLength(500);
        
        
        
        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("now()");
    }
}