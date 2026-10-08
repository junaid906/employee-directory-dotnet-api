using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Dtos.Departments;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryApi.Services.Department;

public class DepartmentService
{
    private readonly EmployeeDirectoryDbContext _dbContext;

    public DepartmentService(EmployeeDirectoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<DepartmentDto>> GetAllDepartments(CancellationToken ct = default)
    {
        var departments =   await _dbContext.Departments
            .AsNoTracking()
            .Where(d => d.Active)
            .Select(DepartmentDto.QueryProjection)
            .ToListAsync(ct);
       
        return departments;
    }
}
