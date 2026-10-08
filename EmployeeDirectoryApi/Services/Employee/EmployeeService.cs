using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Dtos.Employees;
using EmployeeDirectoryApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryApi.Services.Employee;

public class EmployeeService
{
    private readonly EmployeeDirectoryDbContext _dbContext;

    public EmployeeService(EmployeeDirectoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<EmployeeDto>> GetAllEmployees(EmployeeFilters filters, CancellationToken ct = default)
    {
        var query = _dbContext.Employees
            .AsNoTracking()
            .Where(e => e.Active);

        if (!string.IsNullOrWhiteSpace(filters.Search))
        {
            var tokens = filters.Search.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var token in tokens)
            {
                var pattern = $"%{token}%";
                query = query.Where(e => EF.Functions.ILike(e.FirstName, pattern)
                                         || EF.Functions.ILike(e.LastName, pattern)
                                         || EF.Functions.ILike(e.Email, pattern));
            }
        }
        
        // decision needed for if deactivated employees should be visible
        
        // if (filters.IsActive.HasValue)
        // {
        //     query = query.Where(e => e.Active == filters.IsActive);
        // }
        
        if (filters.DepartmentUniqueId is { } departmentUniqueId)
        {
            query = query.Where(e => e.Position.Department!.UniqueId == departmentUniqueId);
        }
        if (filters.PositionUniqueId is { } positionUniqueId)
        {
            query = query.Where(e => e.Position.UniqueId == positionUniqueId);
        }
        if (filters.SubDepartmentUniqueId is { } subDepartmentUniqueId)
        {
            query = query.Where(e => e.Position.SubDepartment!.UniqueId == subDepartmentUniqueId);
        }
        if (filters.Role is { } role)
        {
            query = query.Where(e => e.Role == role);
        }
        
        return await query
            .Select(EmployeeDto.QueryProjection)
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName)
            .ToListAsync(ct);
    }

    public async Task<DetailedEmployeeDto?> GetDetailedEmployee(Guid uniqueId, CancellationToken ct = default)
    {
        return await _dbContext.Employees
            .AsNoTracking()
            .Where(e => e.UniqueId == uniqueId && e.Active)
            .Select(DetailedEmployeeDto.QueryProjection)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<List<ManagerSummaryDto>> GetManagersSummarised(CancellationToken ct = default)
    {
        var parentPositionIds = _dbContext.EmployeePositions
            .Where(p => p.Active && p.ReportToPositionId != null)
            .Select(p => p.ReportToPositionId!.Value);

        return await _dbContext.Employees
            .AsNoTracking()
            .Where(e => e.Active
                        && parentPositionIds.Contains(e.PositionId))
            .Select(ManagerSummaryDto.QueryProjection)
            .ToListAsync(ct);
    }

    public async Task<EmployeeDto> CreateEmployee(CreateEmployeeDto createEmployeeDto, CancellationToken ct = default)
    {
        var positionId = await _dbContext.EmployeePositions
            .Where(p => p.UniqueId == createEmployeeDto.PositionUniqueId)
            .Select(p => (long?)p.Id)
            .FirstOrDefaultAsync(ct);

        if (positionId is null)
        {
            throw new ArgumentException($"No position found for {createEmployeeDto.PositionUniqueId}");
        }

        var employee = new EmployeeEntity(
            createEmployeeDto.FirstName,
            createEmployeeDto.LastName,
            createEmployeeDto.Email,
            positionId.Value,
            createEmployeeDto.AvatarUrl,
            createEmployeeDto.Role
            );

        _dbContext.Employees.Add(employee);
        await _dbContext.SaveChangesAsync(ct);

        return EmployeeDto.FromEntity(employee, createEmployeeDto.PositionUniqueId);
    }

    public async Task<bool> DeactivateEmployee(Guid uniqueId, CancellationToken ct = default)
    {
        var employee = await _dbContext.Employees
            .FirstOrDefaultAsync(e => e.UniqueId == uniqueId && e.Active, ct);

        if (employee is null)
        {
            return false;
        }
        
        employee.Deactivate();
        employee.IsUpdated();
        await _dbContext.SaveChangesAsync(ct);
        
        return true;
    }
}