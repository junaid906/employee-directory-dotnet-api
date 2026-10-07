using EmployeeDirectoryApi.Dtos.SubDepartments;
using EmployeeDirectoryApi.Services.SubDepartment.Interfaces;

namespace EmployeeDirectoryApi.Endpoints.SubDepartments;

public sealed class GetAllSubDepartmentsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/subdepartments",
                async (ISubDepartmentService service, CancellationToken ct) => Results.Ok(await service.GetAllSubDepartments(ct)))
            .WithName("GetAllSubDepartments")
            .WithTags("SubDepartments")
            .Produces<List<SubDepartmentDto>>();
    }
}
