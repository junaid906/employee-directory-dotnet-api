namespace EmployeeDirectoryApi.Models;

public abstract class BaseModel
{
    public long Id { get;  protected set; }
    public Guid UniqueId { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
    public bool Active { get; protected set; } = true;
    
    // domain functions
    public void SetId(long id) => Id = id;
    public void SetUniqueId(Guid uniqueId) => UniqueId = uniqueId;
    public void IsUpdated() => UpdatedAt = DateTime.UtcNow;
    public void Deactivate() => Active = false;
    public void Activate() => Active = true;
}