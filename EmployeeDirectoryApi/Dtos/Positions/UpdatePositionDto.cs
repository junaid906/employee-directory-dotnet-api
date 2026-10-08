namespace EmployeeDirectoryApi.Dtos.Positions;

public class UpdatePositionDto
{
    public required string JobTitle { get; init; }
    public required Guid DepartmentUniqueId { get; init; }
    public Guid? SubDepartmentUniqueId { get; init; }
    public Guid? ReportToPositionUniqueId { get; init; }
    public required int SeatingPosition { get; init; }
}
