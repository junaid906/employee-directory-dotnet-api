using EmployeeDirectoryApi.Dtos;
using EmployeeDirectoryApi.Services.Employee.Interfaces;

namespace EmployeeDirectoryApi.Endpoints.Employees;

public sealed class PostEmployeeEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/employees/create-employee", 
                async (CreateEmployeeDto createEmployeeDto, IEmployeeService service, CancellationToken ct) => Results.Ok(await service.CreateEmployee(createEmployeeDto, ct)))
            .WithName("CreateEmployee")
            .WithTags("Employee")
            .Produces<EmployeeDto>(StatusCodes.Status201Created);
    }
}