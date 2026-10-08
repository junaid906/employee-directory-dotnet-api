using System.Linq.Expressions;
using EmployeeDirectoryApi.Entities;

namespace EmployeeDirectoryApi.Dtos.Employees;

public class EmployeeDto
{
    public required Guid UniqueId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required Guid PositionUniqueId { get; init; }
    public string? AvatarUrl { get; init; }

    public static Expression<Func<EmployeeEntity, EmployeeDto>> QueryProjection => employee => new EmployeeDto
    {
        UniqueId = employee.UniqueId,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Email = employee.Email,
        PositionUniqueId = employee.Position.UniqueId,
        AvatarUrl = employee.AvatarUrl
    };

    public static EmployeeDto FromEntity(EmployeeEntity employee, Guid positionUniqueId) => new()
    {
        UniqueId = employee.UniqueId,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Email = employee.Email,
        PositionUniqueId = positionUniqueId,
        AvatarUrl = employee.AvatarUrl
    };
}