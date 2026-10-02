using EmployeeDirectoryApi.Dtos;
using EmployeeDirectoryApi.Services.Employee.Interfaces;

namespace EmployeeDirectoryApi.Endpoints.Employees;

public sealed class GetEmployeeEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/employees/{uniqueId:guid}", async (
                Guid uniqueId, IEmployeeService service, CancellationToken ct) =>
            {
                var employee = await service.GetEmployee(uniqueId, ct);
                return employee is null ? Results.NotFound() : Results.Ok(employee);
            })
            .WithName("GetEmployee")
            .WithTags("Employees")
            .Produces<EmployeeDto>()
            .Produces(StatusCodes.Status404NotFound);
    }
}