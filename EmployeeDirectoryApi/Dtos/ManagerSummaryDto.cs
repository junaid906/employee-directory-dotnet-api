namespace EmployeeDirectoryApi.Dtos;

public class ManagerSummaryDto
{
    public required Guid UniqueId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}