namespace EmployeeDirectoryApi.Dtos.SubDepartments;

public class SubDepartmentDto
{
    public required Guid UniqueId { get; init; }
    public required string SubDepartmentName { get; init; }
    public required Guid DepartmentUniqueId { get; init; }
}
