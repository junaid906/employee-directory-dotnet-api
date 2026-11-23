using EmployeeDirectoryApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryApi.Data;

public class EmployeeDirectoryDbContext : DbContext
{
    public EmployeeDirectoryDbContext(DbContextOptions<EmployeeDirectoryDbContext> options)
        : base(options){}
    
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<EmployeePosition> EmployeePositions => Set<EmployeePosition>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<SubDepartment> SubDepartments => Set<SubDepartment>();
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeDirectoryDbContext).Assembly);
    }   
}