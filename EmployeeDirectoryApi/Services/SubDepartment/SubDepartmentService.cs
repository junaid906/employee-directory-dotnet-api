using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Dtos.SubDepartments;
using EmployeeDirectoryApi.Services.SubDepartment.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryApi.Services.SubDepartment;

public class SubDepartmentService : ISubDepartmentService
{
    private readonly EmployeeDirectoryDbContext _dbContext;

    public SubDepartmentService(EmployeeDirectoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Get All SubDepartments

    public async Task<List<SubDepartmentDto>> GetAllSubDepartments(CancellationToken ct = default)
    {
        var subDepartments = await _dbContext.SubDepartments
            .AsNoTracking()
            .Where(s => s.Active)
            .ToListAsync(ct);

        var uniqueIdByDepartmentId = await _dbContext.Departments
            .AsNoTracking()
            .ToDictionaryAsync(d => d.Id, d => d.UniqueId, ct);

        return subDepartments
            .Select(s => new SubDepartmentDto
            {
                UniqueId = s.UniqueId,
                SubDepartmentName = s.SubDepartmentName,
                DepartmentUniqueId = uniqueIdByDepartmentId[s.DepartmentId]
            })
            .ToList();
    }

    #endregion
}
