namespace EmployeeDirectoryApi.Dtos;

public class EmployeeDto
{
    public required long Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Department { get; init; }
    public required string SubDepartment { get; init; }
    public required string JobTitle { get; init; }
    public long? ReportingTo { get; init; }
    public int? SeatingPosition { get; init; }
    public string? AvatarUrl { get; init; }
}