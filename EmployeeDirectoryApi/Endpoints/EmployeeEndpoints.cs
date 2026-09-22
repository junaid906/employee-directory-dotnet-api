namespace EmployeeDirectoryApi.Endpoints;

public static class EmployeeEndpoints
{
    public static IEndpointRouteBuilder MapEmployees(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/employees");
        
        group.MapGet("/", () => "List all employees");
        group.MapGet("/{id:int}", (int id) => $"Employee with id {id}");
        // group.MapPost("/" (CreateEm))
        
        return app;
    }
    
}
