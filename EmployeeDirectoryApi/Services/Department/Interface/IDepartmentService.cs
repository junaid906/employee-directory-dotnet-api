using EmployeeDirectoryApi.Dtos.Departments;

namespace EmployeeDirectoryApi.Services.Department.Interface;

public interface IDepartmentService
{
    Task<List<DepartmentDto>> GetAllDepartments(CancellationToken ct = default);
}
