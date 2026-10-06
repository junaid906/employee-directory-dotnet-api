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
            
        
        var uniqueIdById = employees.ToDictionary(e => e.Id, e => e.UniqueId);

        var result = employees
            .Select(e => Map(e, e.ReportingToId is { } managerId ? uniqueIdById[managerId] : null))
            .ToList();

        return result;
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

        Guid? reportingToUniqueId = null;
        if (employee.ReportingToId is { } managerId)
        {
            reportingToUniqueId = await _dbContext.Employees
                .Where(e => e.Id == managerId)
                .Where(e => e.Active)
                .Select(e => e.UniqueId)
                .FirstOrDefaultAsync(ct);
        }

        return Map(employee, reportingToUniqueId);
    }

    #endregion
    
    #region Get Managers

    public async Task<List<ManagerSummaryDto>> GetManagersSummarised(CancellationToken ct = default)
    {
        var managers = await _dbContext.Employees
            .AsNoTracking()
            .Where(e => _dbContext.Employees
                .Any(sub => sub.ReportingToId == e.Id))
            .Where(e => e.Active)
            .Select(e => new ManagerSummaryDto
            {
                UniqueId = e.UniqueId,
                FirstName = e.FirstName,
                LastName = e.LastName
            })
            .ToListAsync(ct);

        return managers;
    }
    
    #endregion

    #region Create Employee

    public async Task<EmployeeDto> CreateEmployee(CreateEmployeeDto createEmployeeDto, CancellationToken ct = default)
    {
        long? reportingToId = null;

        if (createEmployeeDto.ReportingToUniqueId is { } managerUniqueId)
        {
            reportingToId = await _dbContext.Employees
                .Where(e => e.UniqueId == managerUniqueId)
                .Select(e => (long?)e.Id)
                .FirstOrDefaultAsync(ct);

            if (reportingToId is null)
            {
                throw new ArgumentException($"No manager found for {managerUniqueId}");
            }
        }
        
        var employee = new EmployeeEntity(
            createEmployeeDto.FirstName,
            createEmployeeDto.LastName,
            createEmployeeDto.Email,
            
            createEmployeeDto.AvatarUrl
        );

        if (reportingToId is { } managerId)
        {
            employee.ReportTo(managerId);
        }
        
        _dbContext.Employees.Add(employee);
        await _dbContext.SaveChangesAsync(ct);

        return Map(employee, createEmployeeDto.ReportingToUniqueId);
    }

    #endregion
    

    private static EmployeeDto Map(EmployeeEntity employee, Guid? reportingToUniqueId) => new()
    {
        UniqueId = employee.UniqueId,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Email = employee.Email,
        AvatarUrl = employee.AvatarUrl
    };
}