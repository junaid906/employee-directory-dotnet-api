namespace EmployeeDirectoryApi.Dtos.Positions;

public class ReparentPositionsDto
{
    public Guid? ReportToPositionUniqueId { get; init; }
    public List<Guid> ChildPositionUniqueIds { get; init; }
}