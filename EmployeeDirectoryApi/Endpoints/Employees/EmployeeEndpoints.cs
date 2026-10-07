namespace EmployeeDirectoryApi.Endpoints.Employees;

public static class EmployeeEndpoints
{
    public static IEndpointRouteBuilder MapEmployees(this IEndpointRouteBuilder endpoint)
    {
        new GetAllEmployeesEndpoint().MapEndpoint(endpoint);
        new GetEmployeeEndpoint().MapEndpoint(endpoint);
        new PostEmployeeEndpoint().MapEndpoint(endpoint);
        new GetManagersSummarisedEndpoint().MapEndpoint(endpoint);
        
        return endpoint;
    }
}