using EmployeeDirectoryApi.Dtos.SubDepartments;

namespace EmployeeDirectoryApi.Services.SubDepartment.Interface;

public interface ISubDepartmentService
{
    Task<List<SubDepartmentDto>> GetAllSubDepartments(CancellationToken ct = default);
}
