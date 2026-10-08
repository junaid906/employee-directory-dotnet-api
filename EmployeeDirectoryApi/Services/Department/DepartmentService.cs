using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Dtos.Departments;
using EmployeeDirectoryApi.Services.Department.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryApi.Services.Department;

public class DepartmentService : IDepartmentService
{
    private readonly EmployeeDirectoryDbContext _dbContext;

    public DepartmentService(EmployeeDirectoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<DepartmentDto>> GetAllDepartments(CancellationToken ct = default)
    {
        return await _dbContext.Departments
            .AsNoTracking()
            .Where(d => d.Active)
            .Select(d => new DepartmentDto
            {
                UniqueId = d.UniqueId,
                DepartmentName = d.DepartmentName
            })
            .ToListAsync(ct);
    }
}
