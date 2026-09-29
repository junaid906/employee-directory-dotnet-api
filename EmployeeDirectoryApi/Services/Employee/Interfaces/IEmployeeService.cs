using EmployeeDirectoryApi.Dtos;

namespace EmployeeDirectoryApi.Services.Employee.Interfaces;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetAllEmployees(CancellationToken ct = default);
    Task<EmployeeDto?> GetEmployee(long id, CancellationToken ct = default);
}
