namespace EmployeeDirectoryApi.Entities;

public class EmployeeEntity : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Department { get; private set; }
    public string SubDepartment { get; private set; }
    public string JobTitle { get; private set; }
    public long? ReportingToId { get; private set; }
    public int? SeatingPosition { get; private set; }
    public string? AvatarUrl { get; private set; }
    
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
    
    public void UpdateSubDepartment(string subDepartment)
    {
        if (string.IsNullOrEmpty(subDepartment))
        {
            throw new ArgumentException("subDepartment cannot be null or empty");
        }
        
        SubDepartment = subDepartment;
    }
    
    public void UpdateJobTitle(string jobTitle)
    {
        if (string.IsNullOrEmpty(jobTitle))
        {
            throw new ArgumentException("jobTitle cannot be null or empty");
        }
        
        JobTitle = jobTitle;
    }

    public void ReportTo(long managerId)
    {
        ReportingToId = managerId;
    }

    public void UpdateSeatingPosition(int seatingPosition)
    {
        SeatingPosition = seatingPosition;
    }

    public void UpdateAvatarUrl(string avatarUrl)
    {
        if (string.IsNullOrEmpty(avatarUrl))
        {
            throw new ArgumentException("avatarUrl cannot be null or empty");
        }
        
        AvatarUrl = avatarUrl;
    }
}