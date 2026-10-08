using EmployeeDirectoryApi.Dtos.Employees;
using EmployeeDirectoryApi.Services.Employee;

namespace EmployeeDirectoryApi.Endpoints.Employees;

public sealed class PostCreateEmployeeEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/employees/create-employee", 
                async (CreateEmployeeDto createEmployeeDto, EmployeeService service, CancellationToken ct) => Results.Ok(await service.CreateEmployee(createEmployeeDto, ct)))
            .WithName("CreateEmployee")
            .WithTags("Employee")
            .Produces<EmployeeDto>(StatusCodes.Status201Created);
    }
}