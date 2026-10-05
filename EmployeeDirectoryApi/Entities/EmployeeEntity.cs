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
    public long? ReportingToId { get; set; }
    public int? SeatingPosition { get; set; }
    public string? AvatarUrl { get; set; }
    
    public EmployeeEntity( ) {}

    public EmployeeEntity(
        string firstName,
        string lastName,
        string email,
        string department,
        string subDepartment,
        string jobTitle,
        int? seatingPosition,
        string? avatarUrl
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Department = department;
        SubDepartment = subDepartment;
        JobTitle = jobTitle;
        SeatingPosition = seatingPosition;
        AvatarUrl = avatarUrl;
    }
    
    public void UpdateDepartment(string department)
    {
        if (string.IsNullOrEmpty(department))
        {
            throw new ArgumentException("department cannot be null or empty");
        }
        
        Department = department;
    }
    
    public void UpdateSubDepartment(string subDepartment){
        if (string.IsNullOrEmpty(subDepartment))
        {
            throw new ArgumentException("subDepartment cannot be null or empty");
        }
        SubDepartment = subDepartment;
    }
    
}