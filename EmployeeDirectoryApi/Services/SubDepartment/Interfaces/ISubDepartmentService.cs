using EmployeeDirectoryApi.Dtos.SubDepartments;

namespace EmployeeDirectoryApi.Services.SubDepartment.Interfaces;

public interface ISubDepartmentService
{
    Task<List<SubDepartmentDto>> GetAllSubDepartments(CancellationToken ct = default);
}
