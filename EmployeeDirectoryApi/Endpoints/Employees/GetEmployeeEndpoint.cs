using EmployeeDirectoryApi.Dtos.Employees;
using EmployeeDirectoryApi.Services.Employee;

namespace EmployeeDirectoryApi.Endpoints.Employees;

public sealed class GetEmployeeEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/employees/{uniqueId:guid}", async (
                Guid uniqueId, EmployeeService service, CancellationToken ct) =>
            {
                var employee = await service.GetDetailedEmployee(uniqueId, ct);
                return employee is null ? Results.NotFound() : Results.Ok(employee);
            })
            .WithName("GetEmployee")
            .WithTags("Employees")
            .Produces<EmployeeDto>()
            .Produces(StatusCodes.Status404NotFound);
    }
}