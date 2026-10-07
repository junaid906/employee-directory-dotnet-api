using EmployeeDirectoryApi.Dtos.Departments;

namespace EmployeeDirectoryApi.Services.Department.Interfaces;

public interface IDepartmentService
{
    Task<List<DepartmentDto>> GetAllDepartments(CancellationToken ct = default);
}
