using System.Linq.Expressions;
using EmployeeDirectoryApi.Entities;

namespace EmployeeDirectoryApi.Dtos.Departments;

public class DepartmentDto
{
    public required Guid UniqueId { get; init; }
    public required string DepartmentName { get; init; }

    public static Expression<Func<DepartmentEntity, DepartmentDto>> QueryProjection => department => new DepartmentDto
    {
        UniqueId = department.UniqueId,
        DepartmentName = department.DepartmentName
    };
}
