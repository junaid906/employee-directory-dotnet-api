using EmployeeDirectoryApi.Entities;

namespace EmployeeDirectoryApi.Entities;

public class EmployeeEntity : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Department { get; set; }
    public required string SubDepartment { get; set; }
    public required string JobTitle { get; set; }
    public long? ReportingTo { get; set; }
    public int? SeatingPosition { get; set; }
    public string? AvatarUrl { get; set; }
}