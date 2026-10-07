using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeDirectoryApi.Entities.Configurations;

public class EmployeePositionEntityConfiguration : IEntityTypeConfiguration<EmployeePositionEntity>
{
    public void Configure(EntityTypeBuilder<EmployeePositionEntity> builder)
    {
        builder.ToTable("EmployeePositions");
        
        builder.HasIndex(x => x.UniqueId)
            .IsUnique();
        
        builder.HasKey(x=> x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.JobTitle)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("now()");
        
        builder.HasOne(x => x.Department)
            .WithMany()
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.SubDepartment)
            .WithMany()
            .HasForeignKey(x => x.SubDepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.ReportToPosition)
            .WithMany(p => p.ChildPositions)
            .HasForeignKey(x => x.ReportToPositionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}