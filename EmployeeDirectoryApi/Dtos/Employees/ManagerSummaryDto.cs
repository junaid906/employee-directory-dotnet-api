namespace EmployeeDirectoryApi.Dtos.Employees;

public class ManagerSummaryDto
{
    public required Guid UniqueId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}