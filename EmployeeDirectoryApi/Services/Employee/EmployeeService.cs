using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Dtos;
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
        var employees = await _dbContext.Employees
            .AsNoTracking()
            .Where(e => e.Active)
            .ToListAsync(ct);

        var uniqueIdByPositionId = await _dbContext.EmployeePositions
            .AsNoTracking()
            .ToDictionaryAsync(p => p.Id, p => p.UniqueId, ct);

        return employees
            .Select(e => Map(e, e.PositionId is { } positionId ? uniqueIdByPositionId[positionId] : null))
            .ToList();
    }

    #endregion

    #region Get Employee

    public async Task<EmployeeDto?> GetEmployee(Guid uniqueId, CancellationToken ct = default)
    {
        var employee = await _dbContext.Employees
            .AsNoTracking()
            .Where(e => e.Active)
            .FirstOrDefaultAsync(e => e.UniqueId == uniqueId, ct);

        if (employee is null)
        {
            return null;
        }

        Guid? positionUniqueId = null;
        if (employee.PositionId is { } positionId)
        {
            positionUniqueId = await _dbContext.EmployeePositions
                .Where(p => p.Id == positionId)
                .Select(p => (Guid?)p.UniqueId)
                .FirstOrDefaultAsync(ct);
        }

        return Map(employee, positionUniqueId);
    }

    #endregion

    #region Get Managers

    public async Task<List<ManagerSummaryDto>> GetManagersSummarised(CancellationToken ct = default)
    {
        // positions that have at least one child position are management seats
        var parentPositionIds = _dbContext.EmployeePositions
            .Where(p => p.Active && p.ReportToPositionId != null)
            .Select(p => p.ReportToPositionId!.Value);

        return await _dbContext.Employees
            .AsNoTracking()
            .Where(e => e.Active
                        && e.PositionId != null
                        && parentPositionIds.Contains(e.PositionId.Value))
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
        long? positionId = null;

        if (createEmployeeDto.PositionUniqueId is { } positionUniqueId)
        {
            positionId = await _dbContext.EmployeePositions
                .Where(p => p.UniqueId == positionUniqueId)
                .Select(p => (long?)p.Id)
                .FirstOrDefaultAsync(ct);

            if (positionId is null)
            {
                throw new ArgumentException($"No position found for {positionUniqueId}");
            }
        }

        var employee = new EmployeeEntity(
            createEmployeeDto.FirstName,
            createEmployeeDto.LastName,
            createEmployeeDto.Email,
            createEmployeeDto.AvatarUrl
        );

        if (positionId is { } assignedPositionId)
        {
            employee.UpdatePositionId(assignedPositionId);
        }

        _dbContext.Employees.Add(employee);
        await _dbContext.SaveChangesAsync(ct);

        return Map(employee, createEmployeeDto.PositionUniqueId);
    }

    #endregion

    private static EmployeeDto Map(EmployeeEntity employee, Guid? positionUniqueId) => new()
    {
        UniqueId = employee.UniqueId,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Email = employee.Email,
        PositionUniqueId = positionUniqueId,
        AvatarUrl = employee.AvatarUrl
    };
}
