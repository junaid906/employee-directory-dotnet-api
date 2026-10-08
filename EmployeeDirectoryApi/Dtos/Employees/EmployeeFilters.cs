namespace EmployeeDirectoryApi.Dtos.Employees;

public class EmployeeFilters
{
    public Guid? DepartmentUniqueId { get; set; }
    public Guid? SubDepartmentUniqueId { get; set; }
    public Guid? PositionUniqueId { get; set; }
    public int? Role { get; set; }
    public string? Search { get; set; }
    public bool? IsActive { get; set; }
}