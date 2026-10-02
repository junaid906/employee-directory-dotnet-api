namespace EmployeeDirectoryApi.Endpoints.Employees;

public static class EmployeeEndpoints
{
    public static IEndpointRouteBuilder MapGetEmployees(this IEndpointRouteBuilder endpoint)
    {
        new GetAllEmployeesEndpoint().MapEndpoint(endpoint);
        new GetEmployeeEndpoint().MapEndpoint(endpoint);
        
        return endpoint;
    }
}