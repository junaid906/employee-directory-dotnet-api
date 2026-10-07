using System.Linq.Expressions;
using EmployeeDirectoryApi.Dtos.Departments;
using EmployeeDirectoryApi.Dtos.Positions;
using EmployeeDirectoryApi.Dtos.SubDepartments;
using EmployeeDirectoryApi.Entities;

namespace EmployeeDirectoryApi.Dtos.Employees;

public class DetailedEmployeeDto
{
    public required Guid UniqueId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public string? AvatarUrl { get; init; }
    
    public PositionDto? Position { get; init; }
    public DepartmentDto? Department { get; init; }
    public SubDepartmentDto? SubDepartment { get; init; }

    public static DetailedEmployeeDto FromEntity(
        EmployeeEntity employee,
        PositionDto? position,
        DepartmentDto? department,
        SubDepartmentDto? subDepartment) => new()
    {
        UniqueId = employee.UniqueId,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Email = employee.Email,
        AvatarUrl = employee.AvatarUrl,
        Position = position,
        Department = department,
        SubDepartment = subDepartment
    };

    public static Expression<Func<EmployeeEntity, DetailedEmployeeDto>> QueryProjection => e =>
        new DetailedEmployeeDto()
        {
            UniqueId = e.UniqueId,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            AvatarUrl = e.AvatarUrl,
            
            Position = e.Position == null ? null : new PositionDto
            {
                UniqueId = e.Position.UniqueId,
                JobTitle = e.Position.JobTitle,
                DepartmentUniqueId = e.Position.Department!.UniqueId,
                SubDepartmentUniqueId = e.Position.SubDepartment == null ? (Guid?)null : e.Position.SubDepartment.UniqueId,
                ReportToPositionUniqueId = e.Position.ReportToPosition == null ? (Guid?)null : e.Position.ReportToPosition.UniqueId,
                SeatingPosition = e.Position.SeatingPosition
            },
            
            Department = e.Position == null ? null : new DepartmentDto
            {
                UniqueId = e.Position.Department!.UniqueId,
                DepartmentName = e.Position.Department.DepartmentName
            },
            
            SubDepartment = e.Position == null || e.Position.SubDepartment == null ? null : new SubDepartmentDto
            {
                UniqueId = e.Position.SubDepartment.UniqueId,
                SubDepartmentName = e.Position.SubDepartment.SubDepartmentName,
                DepartmentUniqueId = e.Position.Department!.UniqueId
            }
        };
    
    
}
