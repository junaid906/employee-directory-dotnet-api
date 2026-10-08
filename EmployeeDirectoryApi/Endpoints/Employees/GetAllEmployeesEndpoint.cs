using EmployeeDirectoryApi.Dtos.Employees;
using EmployeeDirectoryApi.Services.Employee;

namespace EmployeeDirectoryApi.Endpoints.Employees;

public sealed class GetAllEmployeesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/employees",
                async ([AsParameters] EmployeeFilters filters, EmployeeService service, CancellationToken ct)
                    => Results.Ok(await service.GetAllEmployees(filters, ct)))
            .WithName("GetAllEmployees")
            .WithTags("Employees")
            .Produces<List<EmployeeDto>>();
    }
}