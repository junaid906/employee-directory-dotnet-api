using System.Linq.Expressions;
using EmployeeDirectoryApi.Entities;

namespace EmployeeDirectoryApi.Dtos.SubDepartments;

public class SubDepartmentDto
{
    public required Guid UniqueId { get; init; }
    public required string SubDepartmentName { get; init; }
    public required Guid DepartmentUniqueId { get; init; }

    public static Expression<Func<SubDepartmentEntity, SubDepartmentDto>> QueryProjection => subDepartment => new SubDepartmentDto
    {
        UniqueId = subDepartment.UniqueId,
        SubDepartmentName = subDepartment.SubDepartmentName,
        DepartmentUniqueId = subDepartment.Department!.UniqueId
    };
}
