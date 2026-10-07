namespace EmployeeDirectoryApi.Dtos.Employees;

public class CreateEmployeeDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required Guid PositionUniqueId { get; init; }
    public string? AvatarUrl { get; init; }
    public required int Role { get; init; }
}