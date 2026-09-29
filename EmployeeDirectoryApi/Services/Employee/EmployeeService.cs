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

    public async Task<EmployeeDto?> GetEmployee(long id, CancellationToken ct = default)
    {
        var employee = EmployeeSeed.Employees().FirstOrDefault(e => e.Id == id);
        return employee is null ? null : Map(employee);
    }
    
    private static EmployeeDto Map(EmployeeEntity employee) => new()
    {
        Id = employee.Id,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Email = employee.Email,
        Department = employee.Department,
        SubDepartment = employee.SubDepartment,
        JobTitle = employee.JobTitle,
        ReportingTo = employee.ReportingTo,
        SeatingPosition = employee.SeatingPosition,
        AvatarUrl = employee.AvatarUrl,
    };
}