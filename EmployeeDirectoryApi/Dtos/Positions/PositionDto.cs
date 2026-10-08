using System.Linq.Expressions;
using EmployeeDirectoryApi.Entities;

namespace EmployeeDirectoryApi.Dtos.Positions;

public class PositionDto
{
    public required Guid UniqueId { get; init; }
    public required string JobTitle { get; init; }
    public required Guid DepartmentUniqueId { get; init; }
    public Guid? SubDepartmentUniqueId { get; init; }
    public Guid? ReportToPositionUniqueId { get; init; }
    public required int SeatingPosition { get; init; }

    public static Expression<Func<EmployeePositionEntity, PositionDto>> QueryProjection => position => new PositionDto
    {
        UniqueId = position.UniqueId,
        JobTitle = position.JobTitle,
        DepartmentUniqueId = position.Department!.UniqueId,
        SubDepartmentUniqueId = position.SubDepartment == null ? null : position.SubDepartment.UniqueId,
        ReportToPositionUniqueId = position.ReportToPosition == null ? null : position.ReportToPosition.UniqueId,
        SeatingPosition = position.SeatingPosition
    };

    public static PositionDto FromEntity(
        EmployeePositionEntity position,
        Guid departmentUniqueId,
        Guid? subDepartmentUniqueId,
        Guid? reportToPositionUniqueId) => new()
    {
        UniqueId = position.UniqueId,
        JobTitle = position.JobTitle,
        DepartmentUniqueId = departmentUniqueId,
        SubDepartmentUniqueId = subDepartmentUniqueId,
        ReportToPositionUniqueId = reportToPositionUniqueId,
        SeatingPosition = position.SeatingPosition
    };
}
