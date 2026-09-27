using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Dtos;
using EmployeeDirectoryApi.Entities;
using EmployeeDirectoryApi.Services.Employee.Interfaces;

namespace EmployeeDirectoryApi.Services.Employee;

public class EmployeeService : IEmployeeService
{
    public Task<List<EmployeeDto>> GetAllEmployees(CancellationToken ct = default)
    {
        var employees = EmployeeSeed.Employees().Select(Map).ToList();
        return Task.FromResult(employees);
    }

    private static EmployeeDto Map(EmployeeEntity employee) => new()
    {
        Id = employee.Id,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Email = employee.Email,
    };
}