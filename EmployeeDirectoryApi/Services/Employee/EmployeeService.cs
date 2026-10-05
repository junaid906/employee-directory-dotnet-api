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
    
    public async Task<List<EmployeeDto>> GetAllEmployees(CancellationToken ct = default)
    {
        var employees = await _dbContext.Employees
            .AsNoTracking()
            .ToListAsync(ct);
        
        var uniqueIdById = employees.ToDictionary(e => e.Id, e => e.UniqueId);

        var result = employees
            .Select(e => Map(e, e.ReportingToId is { } managerId ? uniqueIdById[managerId] : null))
            .ToList();

        return result;
    }

    public async Task<EmployeeDto?> GetEmployee(Guid uniqueId, CancellationToken ct = default)
    {
        var employee = await _dbContext.Employees
            .AsNoTracking()
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
                .Select(e => e.UniqueId)
                .FirstOrDefaultAsync(ct);
        }

        return Map(employee, reportingToUniqueId);
    }

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
            createEmployeeDto.Department,
            createEmployeeDto.SubDepartment,
            createEmployeeDto.JobTitle,
            createEmployeeDto.SeatingPosition,
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

    private static EmployeeDto Map(EmployeeEntity employee, Guid? reportingToUniqueId) => new()
    {
        UniqueId = employee.UniqueId,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Email = employee.Email,
        Department = employee.Department,
        SubDepartment = employee.SubDepartment,
        JobTitle = employee.JobTitle,
        ReportingToUniqueId = reportingToUniqueId,
        SeatingPosition = employee.SeatingPosition,
        AvatarUrl = employee.AvatarUrl
    };
}