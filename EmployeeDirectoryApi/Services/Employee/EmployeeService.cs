using EmployeeDirectoryApi.Dtos;
using EmployeeDirectoryApi.Services.Employee.Interfaces;

namespace EmployeeDirectoryApi.Services.Employee;

public class EmployeeService : IEmployeeService
{
    public Task<List<EmployeeDto>> GetAllEmployees(CancellationToken ct = default) 
        =>  Task.FromResult(new List<EmployeeDto>());
}