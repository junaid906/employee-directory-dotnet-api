using EmployeeDirectoryApi.Dtos.Departments;
using EmployeeDirectoryApi.Services.Department;

namespace EmployeeDirectoryApi.Endpoints.Departments;

public sealed class GetAllDepartmentsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/departments",
                async (DepartmentService service, CancellationToken ct) => Results.Ok(await service.GetAllDepartments(ct)))
            .WithName("GetAllDepartments")
            .WithTags("Departments")
            .Produces<List<DepartmentDto>>();
    }
}
