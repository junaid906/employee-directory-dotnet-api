using EmployeeDirectoryApi.Entities;

namespace EmployeeDirectoryApi.Entities;

public class EmployeeEntity : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}