namespace EmployeeDirectoryApi.Dtos.Departments;

public class DepartmentDto
{
    public required Guid UniqueId { get; init; }
    public required string DepartmentName { get; init; }
}
