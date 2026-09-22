namespace EmployeeDirectoryApi.Dtos;

public class EmployeeDto
{
    public long Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
}