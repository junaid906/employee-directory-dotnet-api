using EmployeeDirectoryApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryApi.Data;

public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
        : base(options){}
    
    public DbSet<EmployeeEntity> Employees => Set<EmployeeEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeDbContext).Assembly);
    }   
}