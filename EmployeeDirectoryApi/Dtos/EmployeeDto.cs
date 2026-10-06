namespace EmployeeDirectoryApi.Dtos;

public class EmployeeDto
{
    public required Guid UniqueId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public long? PositionId { get; init; }
    public string? AvatarUrl { get; init; }
}