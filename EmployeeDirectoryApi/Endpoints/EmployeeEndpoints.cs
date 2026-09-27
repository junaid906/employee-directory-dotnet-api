using EmployeeDirectoryApi.Services.Employee.Interfaces;

namespace EmployeeDirectoryApi.Endpoints;

public static class EmployeeEndpoints
{
    public static IEndpointRouteBuilder MapEmployees(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/employees");
        
        group.MapGet("/", async (IEmployeeService service, CancellationToken ct) => Results.Ok(await service.GetAllEmployees(ct)));
        group.MapGet("/{id:int}", (int id) => $"Employee with id {id}");
        // group.MapPost("/" (CreateEm))
        
        return app;
    }
    
}
