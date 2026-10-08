using EmployeeDirectoryApi.Services.Employee;

namespace EmployeeDirectoryApi.Endpoints.Employees;

public class PostDeactivateEmployeeEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/employees/deactivate-employee/{uniqueId:guid}",
            async (Guid uniqueId, EmployeeService service, CancellationToken ct) =>
            {
                var deactivatedEmployee = await service.DeactivateEmployee(uniqueId, ct);
                return deactivatedEmployee ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeactivateEmployee")
            .WithTags("Employee")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}