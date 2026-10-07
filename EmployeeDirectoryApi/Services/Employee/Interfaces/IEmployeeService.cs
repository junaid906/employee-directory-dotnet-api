using EmployeeDirectoryApi.Dtos.Employees;

namespace EmployeeDirectoryApi.Services.Employee.Interfaces;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetAllEmployees(CancellationToken ct = default);
    Task<DetailedEmployeeDto?> GetEmployee(Guid uniqueId, CancellationToken ct = default);
    Task<EmployeeDto> CreateEmployee(CreateEmployeeDto createEmployeeDto, CancellationToken ct = default);
    Task<List<ManagerSummaryDto>> GetManagersSummarised(CancellationToken ct = default);
    Task<bool> DeactivateEmployee(Guid uniqueId, CancellationToken ct = default);
}
