using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeDirectoryApi.Entities.Configurations;

public class SubDepartmentEntityConfiguration : IEntityTypeConfiguration<SubDepartmentEntity>
{
    public void Configure(EntityTypeBuilder<SubDepartmentEntity> builder)
    {
        builder.ToTable("SubDepartments");
        
        builder.HasIndex(x => x.UniqueId)
            .IsUnique();
        
        builder.HasKey(x=> x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();


        builder.Property(x => x.SubDepartmentName)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("now()");
        
        builder.HasIndex(x => new { x.DepartmentId, x.SubDepartmentName })
            .IsUnique();
        
        builder.HasOne(x => x.Department)
            .WithMany()
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}