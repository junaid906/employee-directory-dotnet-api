namespace EmployeeDirectoryApi.Dtos;

public class ReportingToEmployeeDto
{
    public Guid? ReportingToUniqueId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}