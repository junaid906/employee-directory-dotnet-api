namespace EmployeeDirectoryApi.Dtos.Positions;

public class InsertPositionDto
{
    public required string JobTitle { get; init; }
    public required Guid DepartmentUniqueId { get; init; }
    public Guid? SubDepartmentUniqueId { get; init; }
    public required int SeatingPosition { get; init; }
    public Guid? ReportToPositionUniqueId { get; init; }
    public List<Guid> ChildPositionUniqueIds { get; init; } = [];
}
