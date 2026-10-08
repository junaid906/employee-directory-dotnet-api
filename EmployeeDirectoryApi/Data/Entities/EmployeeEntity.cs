namespace EmployeeDirectoryApi.Entities;

public class EmployeeEntity : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public long PositionId { get; private set; }
    public EmployeePositionEntity Position { get; private set; }
    public string? AvatarUrl { get; private set; }
    public int Role { get; private set; }
    
    public EmployeeEntity( ) {}

    public EmployeeEntity(
        string firstName,
        string lastName,
        string email,
        long positionId,
        string? avatarUrl,
        int role
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PositionId = positionId;
        AvatarUrl = avatarUrl;
        Role = role;
    }

    public void UpdatePositionId(long positionId)
    {
        PositionId = positionId;
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