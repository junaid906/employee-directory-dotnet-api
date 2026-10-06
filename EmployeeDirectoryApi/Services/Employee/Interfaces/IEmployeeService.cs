using EmployeeDirectoryApi.Dtos;

namespace EmployeeDirectoryApi.Services.Employee.Interfaces;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetAllEmployees(CancellationToken ct = default);
    Task<EmployeeDto?> GetEmployee(Guid uniqueId, CancellationToken ct = default);
    Task<EmployeeDto> CreateEmployee(CreateEmployeeDto createEmployeeDto, CancellationToken ct = default);
    Task<List<ManagerSummaryDto>> GetManagersSummarised(CancellationToken ct = default);
}
