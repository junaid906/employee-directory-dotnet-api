using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Dtos.Employees;
using EmployeeDirectoryApi.Entities;
using EmployeeDirectoryApi.Services.Employee.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryApi.Services.Employee;

public class EmployeeService : IEmployeeService
{
    private readonly EmployeeDirectoryDbContext _dbContext;

    public EmployeeService(EmployeeDirectoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Get All Employees

    public async Task<List<EmployeeDto>> GetAllEmployees(CancellationToken ct = default)
    {
        return await _dbContext.Employees
            .AsNoTracking()
            .Where(e => e.Active)
            .Select(EmployeeDto.QueryProjection)
            .ToListAsync(ct);
    }

    #endregion

    #region Get Employee

    public async Task<DetailedEmployeeDto?> GetEmployee(Guid uniqueId, CancellationToken ct = default)
    {
        return await _dbContext.Employees
            .AsNoTracking()
            .Where(e => e.UniqueId == uniqueId && e.Active)
            .Select(DetailedEmployeeDto.QueryProjection)
            .FirstOrDefaultAsync(ct);
    }

    #endregion

    #region Get Managers

    public async Task<List<ManagerSummaryDto>> GetManagersSummarised(CancellationToken ct = default)
    {
        var parentPositionIds = _dbContext.EmployeePositions
            .Where(p => p.Active && p.ReportToPositionId != null)
            .Select(p => p.ReportToPositionId!.Value);

        return await _dbContext.Employees
            .AsNoTracking()
            .Where(e => e.Active
                        && parentPositionIds.Contains(e.PositionId))
            .Select(e => new ManagerSummaryDto
            {
                UniqueId = e.UniqueId,
                FirstName = e.FirstName,
                LastName = e.LastName
            })
            .ToListAsync(ct);
    }

    #endregion

    #region Create Employee

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

    #endregion
    
    #region Deactivate Employee

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
    
    #endregion
}