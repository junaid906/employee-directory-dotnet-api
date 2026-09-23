namespace EmployeeDirectoryApi.Dtos;

public class EmployeeDto
{
    public required long Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
}