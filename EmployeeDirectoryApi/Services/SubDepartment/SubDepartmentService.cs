using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Dtos.SubDepartments;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryApi.Services.SubDepartment;

public class SubDepartmentService
{
    private readonly EmployeeDirectoryDbContext _dbContext;

    public SubDepartmentService(EmployeeDirectoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<SubDepartmentDto>> GetAllSubDepartments(CancellationToken ct = default)
    {
        var subDepartments = await _dbContext.SubDepartments
            .AsNoTracking()
            .Where(s => s.Active)
            .Select(SubDepartmentDto.QueryProjection)
            .ToListAsync(ct);

        return subDepartments;
    }
}
