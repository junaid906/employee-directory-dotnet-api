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

        builder.HasOne<DepartmentEntity>()
            .WithMany()
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<SubDepartmentEntity>()
            .WithMany()
            .HasForeignKey(x => x.SubDepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<EmployeePositionEntity>()
            .WithMany()
            .HasForeignKey(x => x.ReportToPositionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}