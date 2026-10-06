using EmployeeDirectoryApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryApi.Data;

public class EmployeeDirectoryDbContext : DbContext
{
    public EmployeeDirectoryDbContext(DbContextOptions<EmployeeDirectoryDbContext> options)
        : base(options){}
    
    public DbSet<EmployeeEntity> Employees => Set<EmployeeEntity>();
    public DbSet<EmployeePositionEntity> EmployeePositions => Set<EmployeePositionEntity>();
    public DbSet<DepartmentEntity> Departments => Set<DepartmentEntity>();
    public DbSet<SubDepartmentEntity> SubDepartments => Set<SubDepartmentEntity>();
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeDirectoryDbContext).Assembly);
    }   
}