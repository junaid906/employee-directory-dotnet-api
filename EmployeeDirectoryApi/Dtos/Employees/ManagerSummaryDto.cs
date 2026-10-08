using System.Linq.Expressions;
using EmployeeDirectoryApi.Entities;

namespace EmployeeDirectoryApi.Dtos.Employees;

public class ManagerSummaryDto
{
    public required Guid UniqueId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }

    public static Expression<Func<EmployeeEntity, ManagerSummaryDto>> QueryProjection => managerSummary => new ManagerSummaryDto
    {
        UniqueId = managerSummary.UniqueId,
        FirstName = managerSummary.FirstName,
        LastName = managerSummary.LastName
    };
}