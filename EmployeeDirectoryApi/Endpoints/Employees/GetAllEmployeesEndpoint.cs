using EmployeeDirectoryApi.Dtos;
using EmployeeDirectoryApi.Services.Employee.Interfaces;

namespace EmployeeDirectoryApi.Endpoints.Employees;

public sealed class GetAllEmployeesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/employees",
                async (IEmployeeService service, CancellationToken ct) => Results.Ok(await service.GetAllEmployees(ct)))
            .WithName("GetAllEmployees")
            .WithTags("Employees")
            .Produces<List<EmployeeDto>>();
    }
}