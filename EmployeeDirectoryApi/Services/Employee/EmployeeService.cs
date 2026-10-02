using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Dtos;
using EmployeeDirectoryApi.Entities;
using EmployeeDirectoryApi.Services.Employee.Interfaces;

namespace EmployeeDirectoryApi.Services.Employee;

public class EmployeeService : IEmployeeService
{
    public Task<List<EmployeeDto>> GetAllEmployees(CancellationToken ct = default)
    {
        var employees = EmployeeSeed.Employees();
        var uniqueIdById = employees.ToDictionary(e => e.Id, e => e.UniqueId);

        var result = employees
            .Select(e => Map(e, uniqueIdById))
            .ToList();

        return Task.FromResult(result);
    }

    public async Task<EmployeeDto?> GetEmployee(Guid uniqueId, CancellationToken ct = default)
    {
        var employees = EmployeeSeed.Employees();
        var employee = employees.FirstOrDefault(e => e.UniqueId == uniqueId);
        if (employee is null)
        {
            return null;
        }

        var uniqueIdById = employees.ToDictionary(e => e.Id, e => e.UniqueId);
        return Map(employee, uniqueIdById);
    }

    private static EmployeeDto Map(EmployeeEntity employee, IReadOnlyDictionary<long, Guid> uniqueIdById) => new()
    {
        UniqueId = employee.UniqueId,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Email = employee.Email,
        Department = employee.Department,
        SubDepartment = employee.SubDepartment,
        JobTitle = employee.JobTitle,
        ReportingToUniqueId = employee.ReportingToId is { } managerId ? uniqueIdById[managerId] : null,
        SeatingPosition = employee.SeatingPosition,
        AvatarUrl = employee.AvatarUrl,
    };
}